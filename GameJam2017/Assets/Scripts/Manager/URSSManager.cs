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
    }

    public void Start()
    {
        InitWave();
    }

    public void InitWave()
    {
        Init();
        SitPlayers();
        SitNPCs();
    }

    public void FillSeats()
    {
        seats = null;
        seats = FindObjectsOfType<Seat>(); //ya lo pilla desordenado
        playerSeats.Clear();
        for (int i = 0; i < seats.Length; i++)
        {
            if (seats[i].canBeTookedByPlayer)
                playerSeats.Add(seats[i]);
        }
    }

    public int GetFreeSeat(bool isPlayer)
    {
        int seatId = Random.Range(0, playerSeats.Count);
        if (playerSeats[seatId].takenBy != null)
            GetFreeSeat(isPlayer);
        return seatId;
    }

    public void SitPlayers()
    {
        for (int playerId = 0; playerId < playersPrefabs.Count; playerId++)
        {
            int seat = GetFreeSeat(true);
            Seat currentSeat = seats[seat];

            GameObject playerGO = Instantiate(playersPrefabs[playerId], currentSeat.transform);
            playerGO.name = "Player_" + seat.ToString();
            playerGO.transform.parent = playersTransform;
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
            if (currentSeat != null && currentSeat.takenBy != null)
                DestroyImmediate(currentSeat.gameObject);
        }
    }

    public void SitNPCs()
    {
        int camisetaId = 2; // Random.Range(0, camisetas.Count);
        int gorroId = Random.Range(0, gorros.Count);
        int caraId = Random.Range(0, caras.Count);
        int gafaId = Random.Range(0, gafas.Count);
        int rayaId = Random.Range(0, rayas.Count);

        if (!CheckIdsConsistency(camisetaId, gorroId, caraId, gafaId, rayaId))
            SitNPCs();

        for (int seatId = 0; seatId < seats.Length; seatId++)
        {
            Seat currentSeat = seats[seatId];
            if (currentSeat.takenBy != null)
                continue;

            GameObject npcObject = Instantiate(npcPrefab, currentSeat.transform);
            npcObject.name = "NPC_" + seatId;
            npcObject.transform.parent = npcsTransform;
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
