using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URSSManager : MonoBehaviour {

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
        FillSeats();
        waveNum = 0;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            nextWave();
    }

    public void Start()
    {
        InitWave();
    }

    private void InitWave()
    {
        Init();
        SitPlayers();
        SitNPCs();
    }

    public void nextWave()
    {
        //waveNum = waveNum + 1;
        InitWave();
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
        for (int playerId = 0; playerId < playersPrefabs.Count; playerId++)
        {
            int seat = GetFreePlayerSeat();
            Seat currentSeat = playerSeats[seat];

            GameObject playerGO = Instantiate(playersPrefabs[playerId], currentSeat.transform);
            playerGO.name = "Player_" + seat.ToString();
            playerGO.transform.parent = playersTransform;
            playerGO.transform.position = currentSeat.transform.position;
            currentSeat.takenBy = playerGO.GetComponent<PlayerController>();
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
                DestroyImmediate(currentSeat.takenBy.gameObject);
        }
    }

    public void SitNPCs()
    {
        for (int seatId = 0; seatId < seats.Length; seatId++)
        {
            int currentWave = (waveNum == 0) ? 1 : waveNum; //para que en la primera wave genere randoms entre 0 y 1, si no genera todos a 0 (Player_1!)
            currentWave = Mathf.Clamp(currentWave, 0, 4);
            int camisetaId = Mathf.Clamp(Random.Range(0, currentWave + 1), 0, 4/*camisetas.Count*/);
            int gorroId = Mathf.Clamp(Random.Range(0, currentWave + 1), 0, 4/*gorros.Count*/);
            int caraId = Mathf.Clamp(Random.Range(0, currentWave + 1), 0, 4/*caras.Count*/);
            int gafaId = Mathf.Clamp(Random.Range(0, currentWave + 1), 0, 4/*gafas.Count*/);
            int rayaId = Mathf.Clamp(Random.Range(0, currentWave + 1), 0, 4/*rayas.Count*/);

            if (!CheckIdsConsistency(camisetaId, gorroId, caraId, gafaId, rayaId))
            {
                camisetaId = 0;
                gorroId = 1;
                caraId = 2;
                gafaId = 3;
                rayaId = 4;
            }

            Debug.Log("Ints generated: " + camisetaId + gorroId + caraId + gafaId + rayaId);
            Seat currentSeat = seats[seatId];
            if (currentSeat.takenBy != null)
                continue;

            GameObject npcObject = Instantiate(npcPrefab, currentSeat.transform);
            npcObject.name = "NPC_" + seatId;
            npcObject.transform.parent = npcsTransform;
            npcObject.transform.position = currentSeat.transform.position;
            NPCController npcController = npcObject.GetComponent<NPCController>();
            currentSeat.takenBy = npcController;
            //npcController.SetSprites(gorros[gorroId], caras[caraId], gafas[gafaId], camisetas[camisetaId], rayas[rayaId]);
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

}
