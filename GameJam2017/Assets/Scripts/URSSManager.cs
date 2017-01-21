﻿using System.Collections;
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

    public List<NPCController> NPCConrtollers;
    public List<PlayerController> PlayerControllers;


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

    public void FillSeats()
    {
        seats = null;
        seats = FindObjectsOfType<Seat>();
        playerSeats.Clear();
        for (int i = 0; i < seats.Length; i++)
        {
            if (seats[i].canBeTookedByPlayer)
                playerSeats.Add(seats[i]);
        }

        //Desordenar Listas de seats
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
        for (int playerId = 0; playerId < PlayerControllers.Count; playerId++)
        {
            int seat = GetFreeSeat(true);
            playerSeats[seat].takenBy = PlayerControllers[playerId];
        }
    }

    public void RandomizeNPCs()
    {
        int camisetaId = Random.Range(0, camisetas.Count);
        int gorroId = Random.Range(0, gorros.Count);
        int caraId = Random.Range(0, caras.Count);
        int gafaId = Random.Range(0, gafas.Count);
        int rayaId = Random.Range(0, rayas.Count);

        if (!CheckIdsConsistency(camisetaId, gorroId, caraId, gafaId, rayaId))
            RandomizeNPCs();

        
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