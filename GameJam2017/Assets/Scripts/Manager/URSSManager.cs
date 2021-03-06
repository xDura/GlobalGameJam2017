﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class URSSManager : MonoBehaviour {
    public static SweepLine sweepLine;
    public static List<Seat> playerSeats;

    public enum STATE
    {
        READY = 0,
        COUNTER = 1,
        IN_WAVE = 2,
        WAVE_FINISHED = 3,
        END_GAME = 4
    }

    private void OnDestroy()
    {
        if(playerSeats == null)
            playerSeats.Clear();
    }

    STATE urssState = STATE.COUNTER;

    public List<PlayerController> controllers;

    public Seat[] seats;

    public List<Sprite> caras;
    public List<Sprite> camisetas;
    public List<Sprite> pantalones;
    public List<Sprite> l_arms;
    public List<Sprite> r_arms;

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

    public GameObject crossHair;

    public GameObject bloodPrefab;
    public GameObject currentBlood;
    public SpriteRenderer ball;

    public SpriteRenderer backGround;

    public Text timerText;

    public void Awake()
    {
        if (caras == null)
            caras = new List<Sprite>();
        if (camisetas == null)
            camisetas = new List<Sprite>();
        if (pantalones == null)
            pantalones = new List<Sprite>();
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
            case STATE.END_GAME:
                UpdateEndState();
                break;
        }

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
        sweepLine.StartWave();
        ball.enabled = true;
        ChangeState(STATE.IN_WAVE);
    }

    public void RestoreScenario()
    {
        backGround.color = Color.white;
        audioController.victorySong.Stop();
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
        audioController.Stadium_Ambience.DOFade(0.6f, 0.5f);
        audioController.tensionSong.DOFade(0.0f, 0.5f);
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
                    currentPlayerController.SetKeyCode(KeyCode.Return); break;
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
        int maxRange = camisetas.Count;
        if (waveNum == 0)
            maxRange = 2;
        if (waveNum == 1)
            maxRange = 4;
        for (int seatId = 0; seatId < seats.Length; seatId++)
        {
            int camisetaId = Mathf.Clamp(Random.Range(0, maxRange), 0, camisetas.Count - 1);
            int caraId = Mathf.Clamp(Random.Range(0, maxRange), 0, caras.Count - 1);
            int pantalonId = Mathf.Clamp(Random.Range(0, maxRange), 0, pantalones.Count - 1);

            if (!CheckIdsConsistency(camisetaId, caraId, pantalonId))
            {
                caraId = 0;
                camisetaId = 1;
                pantalonId = 2;
            }

            Debug.Log("Ints generated: " + camisetaId + caraId + pantalonId + " - Range: [0," + maxRange + ")");
            Seat currentSeat = seats[seatId];
            if (currentSeat.takenBy != null)
                continue;

            GameObject npcObject = Instantiate(npcPrefab, currentSeat.transform);
            npcObject.name = "NPC_" + seatId;
            npcObject.transform.parent = npcsTransform;
            npcObject.transform.position = currentSeat.transform.position;
            NPCController npcController = npcObject.GetComponent<NPCController>();
            currentSeat.takenBy = npcController;
            npcController.SetSprites(caras[caraId], camisetas[camisetaId], pantalones[pantalonId], l_arms[camisetaId], r_arms[camisetaId]);
            npcController.SetLayer(currentSeat.GetComponent<SpriteRenderer>().sortingLayerName);
        }
    }

    public bool CheckIdsConsistency(int cara, int camiseta, int pantalon)
    {
        if (camiseta != cara)
            return true;
        if (camiseta != pantalon)
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

    void UpdateEndState()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            lastScreenSetup.RestartGame();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            lastScreenSetup.Quit();
        }
    }

    IEnumerator WaveFinished() //OMG THE WORST FUNCTION EVAH =D SOME DAY WE WILL WE EXECUTED =)
    {
        if (controllers == null || controllers.Count == 0) yield break;

        int worstPlayer = 0;
        float worstDistance = controllers[worstPlayer].distanceScore;
        int worstPlayerInt = 0;
        for (int i = 0; i < controllers.Count; i++)
        {
            PlayerController currentPlayer = controllers[i];
            if (playersCross[currentPlayer.id].activeInHierarchy)
                continue;
            if (worstDistance <= currentPlayer.distanceScore)
            {
                worstPlayer = currentPlayer.id;
                worstDistance = currentPlayer.distanceScore;
                worstPlayerInt = i;
            }
        }

        yield return new WaitForSeconds(endWaveWaitTime);

        currentWaitTime = 0f;
        ball.enabled = false;
        mainLight.DOIntensity(0.0f, 1f);
        audioController.Stadium_Ambience.DOFade(0.0f, 0.5f);
        audioController.tensionSong.Play();
        audioController.tensionSong.DOFade(0.6f, 0.5f);
        for (int i = 0; i < controllers.Count; i++)
            controllers[i].OpenLight();

        crossHair.SetActive(true);
        for (int i = 0; i < controllers.Count; i++)
        {
            Vector2 position = controllers[i].transform.position;
            crossHair.transform.DOMove(position, 2f).SetEase(Ease.InOutQuint);
            yield return new WaitForSeconds(2);
        }

        Vector2 _position = controllers[worstPlayerInt].transform.position;
        crossHair.transform.DOMove(_position, 2f).SetEase(Ease.InOutQuint);

        yield return new WaitForSeconds(3);

        Vector3 mainPosition = crossHair.transform.position;
        controllers[worstPlayerInt].BleedOut(bloodPrefab);

        Color mainColor = backGround.color;

        playersCross[worstPlayer].SetActive(true);
        audioController.tensionSong.Stop();
        audioController.PlayShot();
        crossHair.transform.DOMove(mainPosition + (Vector3.up * 0.2f), 0.1f).SetEase(Ease.InOutQuint);
        backGround.DOColor(Color.red, 0.1f);

        timerText.text = "031 e da HOOD";

        controllers[worstPlayerInt].bodyReference.DOLocalRotate(Vector3.forward * 90, 0.3f);
        controllers[worstPlayerInt].bodyReference.DOLocalMove(Vector3.up * -0.75f , 0.3f);

        yield return new WaitForSeconds(0.1f);

        crossHair.transform.DOMove(mainPosition, 0.1f);
        backGround.DOColor(mainColor, 0.4f);

        yield return new WaitForSeconds(2f);

        controllers[worstPlayerInt].currentBlood = null;
        Fader.FadeOut();

        yield return new WaitForSeconds(endWaveWaitTime);
        crossHair.SetActive(false);

        NextWave();
        yield return null;
    }

    IEnumerator WaitForStartWave()
    {
        ChangeState(STATE.COUNTER);
        mainLight.DOIntensity(1.0f, 1f);

        yield return new WaitForSeconds(1f);

        Fader.FadeIn();

        yield return new WaitForSeconds(1f);

        audioController.PlayCountDown();

        yield return null; //ns saltamos un framesito para que encaje un poco mejor

        timerText.text = "3";
        yield return new WaitForSeconds(1);

        timerText.text = "2";
        yield return new WaitForSeconds(1);

        timerText.text = "1";
        yield return new WaitForSeconds(1);

        timerText.text = "!";

        audioController.PlayHorn();

        StartWave();

    }

    IEnumerator GameFinished()
    {
        Init();

        audioController.VictorySong();

        ball.enabled = false;
        ChangeState(STATE.END_GAME);

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

        yield return new WaitForSeconds(6);

        if (!p1) lastScreenSetup.Kill(0);
        if (!p2) lastScreenSetup.Kill(1);
        if (!p3) lastScreenSetup.Kill(2);
        if (!p4) lastScreenSetup.Kill(3);
        audioController.PlayShot();
        audioController.victorySong.Stop();
        backGround.DOColor(Color.red, 0.35f);

        //se podria hacer con un oncomplete en el Docolor
        yield return new WaitForSeconds(0.35f);
        lastScreenSetup.EnableButtons();

    }

}