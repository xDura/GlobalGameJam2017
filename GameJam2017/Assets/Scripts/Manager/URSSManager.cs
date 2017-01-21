using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class URSSManager : MonoBehaviour {
    public static SweepLine sweepLine;

    public enum STATE
    {
        READY = 0,
        COUNTER = 1,
        IN_WAVE = 2,
        WAVE_FINISHED = 3,
        END_GAME = 4
    }

    STATE urssState = STATE.COUNTER;

    public List<PlayerController> controllers;

    public Seat[] seats;
    public List<Seat> playerSeats;

    public List<Sprite> gorros;
    public List<Sprite> caras;
    public List<Sprite> gafas;
    public List<Sprite> camisetas;
    public List<Sprite> rayas;

    public GameObject npcPrefab;
    public List<GameObject> playersPrefabs;

    public Transform playersTransform;
    public Transform npcsTransform;

    public int waveNum = 0;

    public List<GameObject> playersCross;

    public float currentWaitTime;
    public float startWaveWaitTime;
    public float endWaveWaitTime;
    public float shotWaitTime;

    public AudioController audioController;

    public Light mainLight;

    public LastScreenSetup lastScreenSetup;
    public List<GameObject> StadiumObjects;

    public void Awake()
    {
        if (gorros == null)
            gorros = new List<Sprite>();
        if (caras == null)
            caras = new List<Sprite>();
        if (gafas == null)
            gafas = new List<Sprite>();
        if (camisetas == null)
            camisetas = new List<Sprite>();
        if (rayas == null)
            rayas = new List<Sprite>();
        if (controllers == null)
            controllers = new List<PlayerController>();


        if (sweepLine == null)
            sweepLine = FindObjectOfType<SweepLine>();

        FillSeats();
        waveNum = 0;
    }

    public void Update()
    {
        if (controllers == null) return;

        switch (urssState)
        {
            case STATE.IN_WAVE:
                for (int i = 0; i < controllers.Count; i++)
                    controllers[i].UpdateManually();
                break;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            StartWave();

    }

    public void ChangeState(STATE newState)
    {
        currentWaitTime = 0f;
        urssState = newState;
    }

    public void EndWave()
    {
        ChangeState(STATE.WAVE_FINISHED);
        StartCoroutine(WaveFinished());
    }

    public void StartWave()
    {
        sweepLine.StartWave(0.08f);
        ChangeState(STATE.IN_WAVE);
    }

    public void RestoreScenario()
    {
        lastScreenSetup.Restore();
        for (int i = 0; i < StadiumObjects.Count; i++)
            StadiumObjects[i].SetActive(true);
        for (int i = 0; i < playersCross.Count; i++)
            playersCross[i].SetActive(false);
    }

    public void Start()
    {
        RestoreScenario();
        InitWave();
        StartCoroutine(WaitForStartWave());
    }

    private void InitWave()
    {
        Init();
        SitPlayers();
        SitNPCs();
        sweepLine.Init();
    }

    public void NextWave()
    {
        if (GetAlivePlayers() <= 1)
        {
            StartCoroutine(GameFinished());
        }
        else
        {
            waveNum = waveNum + 1;
            InitWave();
            StartCoroutine(WaitForStartWave());
        }
    }

    public void FillSeats()
    {
        seats = null;
        seats = FindObjectsOfType<Seat>(); //ya lo pilla desordenado
        if (playerSeats == null)
            playerSeats = new List<Seat>();
        playerSeats.Clear();
        for (int i = 0; i < seats.Length; i++)
        {
            if (seats[i].canBeTookedByPlayer)
                playerSeats.Add(seats[i]);
        }
    }

    public int GetFreePlayerSeat()
    {
        int seatId = Random.Range(0, playerSeats.Count);
        if (playerSeats[seatId].takenBy != null)
            return GetFreePlayerSeat();
        return seatId;
    }

    public void SitPlayers()
    {
        controllers.Clear();
        for (int playerId = 0; playerId < playersPrefabs.Count; playerId++)
        {
            if (playersCross[playerId].activeInHierarchy)
                continue;

            int seat = GetFreePlayerSeat();
            Seat currentSeat = playerSeats[seat];

            GameObject playerGO = Instantiate(playersPrefabs[playerId], currentSeat.transform);
            playerGO.name = "Player_Id:" + playerId + "_Seat:" + seat.ToString();
            playerGO.transform.parent = playersTransform;
            playerGO.transform.position = currentSeat.transform.position;

            PlayerController currentPlayerController = playerGO.GetComponent<PlayerController>();
            currentSeat.takenBy = currentPlayerController;
            controllers.Add(currentPlayerController);
            currentPlayerController.SetLayer(currentSeat.GetComponent<SpriteRenderer>().sortingLayerName);
            
            switch (playerId)
            {
                case 0:
                    currentPlayerController.SetKeyCode(KeyCode.Space); break;
                case 1:
                    currentPlayerController.SetKeyCode(KeyCode.KeypadEnter); break;
                case 2:
                    currentPlayerController.SetKeyCode(KeyCode.Q); break;
                case 3:
                    currentPlayerController.SetKeyCode(KeyCode.P); break;
            }

            currentPlayerController.id = playerId;
        }
    }

    public void Init()
    {
        if (seats == null)
        {
            Debug.LogError("Error! Seats is null!");
            return;
        }
        for (int i = 0; i < seats.Length; i++)
        {
            Seat currentSeat = seats[i];
            if (currentSeat.takenBy != null)
            {
                Destroy(currentSeat.takenBy.gameObject);
                currentSeat.takenBy = null;
            }
        }
    }

    public void SitNPCs()
    {
        for (int seatId = 0; seatId < seats.Length; seatId++)
        {
            int currentWave = (waveNum == 0) ? 1 : waveNum; //para que en la primera wave genere randoms entre 0 y 1, si no genera todos a 0 (Player_1!)
            currentWave = Mathf.Clamp(currentWave, 0, 4);
            int camisetaId = Mathf.Clamp(Random.Range(0, currentWave + 1), 0, camisetas.Count);
            int gorroId = Mathf.Clamp(Random.Range(0, currentWave + 1), 0, gorros.Count);
            int caraId = Mathf.Clamp(Random.Range(0, currentWave + 1), 0, caras.Count);
            int gafaId = Mathf.Clamp(Random.Range(0, currentWave + 1), 0, gafas.Count);
            int rayaId = Mathf.Clamp(Random.Range(0, currentWave + 1), 0, rayas.Count);

            if (!CheckIdsConsistency(camisetaId, gorroId, caraId, gafaId, rayaId))
            {
                camisetaId = 0;
                gorroId = 1;
                caraId = 2;
                gafaId = 3;
                rayaId = 4;
            }

            //Debug.Log("Ints generated: " + camisetaId + gorroId + caraId + gafaId + rayaId);
            Seat currentSeat = seats[seatId];
            if (currentSeat.takenBy != null)
                continue;

            GameObject npcObject = Instantiate(npcPrefab, currentSeat.transform);
            npcObject.name = "NPC_" + seatId;
            npcObject.transform.parent = npcsTransform;
            npcObject.transform.position = currentSeat.transform.position;
            NPCController npcController = npcObject.GetComponent<NPCController>();
            currentSeat.takenBy = npcController;
            npcController.SetSprites(gorros[gorroId], caras[caraId], gafas[gafaId], camisetas[camisetaId], rayas[rayaId]);
            npcController.SetLayer(currentSeat.GetComponent<SpriteRenderer>().sortingLayerName);
        }
    }

    public bool CheckIdsConsistency(int camiseta, int gorro, int cara, int gafa, int raya)
    {
        if (camiseta != gorro)
            return true;
        if (camiseta != cara)
            return true;
        if (camiseta != gafa)
            return true;
        if (gafa != raya)
            return true;
        return false;
    }

    int GetAlivePlayers()
    {
        int alivePlayers = 0;
        for (int i = 0; i < playersCross.Count; i++)
        {
            if (playersCross[i].activeInHierarchy == false)
                alivePlayers++;
        }
        return alivePlayers;
    }

    IEnumerator WaveFinished()
    {
        if (controllers == null || controllers.Count == 0) yield break;

        int worstPlayer = 0;
        float worstDistance = controllers[worstPlayer].distanceScore;

        for (int i = 0; i < controllers.Count; i++)
        {
            PlayerController currentPlayer = controllers[i];
            if (playersCross[currentPlayer.id].activeInHierarchy)
                continue;
            if (worstDistance <= currentPlayer.distanceScore)
            {
                worstPlayer = currentPlayer.id;
                worstDistance = currentPlayer.distanceScore;
            }
        }

        yield return new WaitForSeconds(endWaveWaitTime);

        currentWaitTime = 0f;
        mainLight.DOIntensity(0.0f, 1f);
        for (int i = 0; i < controllers.Count; i++)
            controllers[i].OpenLight();

        yield return new WaitForSeconds(shotWaitTime);

        playersCross[worstPlayer].SetActive(true);
        audioController.PlayShot();

        yield return new WaitForSeconds(2);

        Fader.FadeOut();

        yield return new WaitForSeconds(endWaveWaitTime);

        NextWave();
        yield return null;
    }

    IEnumerator WaitForStartWave()
    {
        ChangeState(STATE.COUNTER);
        mainLight.DOIntensity(1.0f, 1f);

        Fader.FadeIn();
        yield return new WaitForSeconds(startWaveWaitTime);

        StartWave();

    }

    IEnumerator GameFinished()
    {
        Init();

        bool p1, p2, p3, p4;
        p1 = playersCross[0].activeInHierarchy;
        p2 = playersCross[1].activeInHierarchy;
        p3 = playersCross[2].activeInHierarchy;
        p4 = playersCross[3].activeInHierarchy;

        for (int i = 0; i < StadiumObjects.Count; i++)
            StadiumObjects[i].SetActive(false);

        lastScreenSetup.SetUp(p1, p2, p3, p4);

        yield return new WaitForSeconds(2);

        Fader.FadeIn();

        yield return new WaitForSeconds(4);

        if (!p1) lastScreenSetup.Kill(0);
        if (!p2) lastScreenSetup.Kill(1);
        if (!p3) lastScreenSetup.Kill(2);
        if (!p4) lastScreenSetup.Kill(3);

        while (!Input.GetKeyDown(KeyCode.R))
            yield return null;

        Fader.FadeOut();

        yield return new WaitForSeconds(3);

        Start();

    }

}