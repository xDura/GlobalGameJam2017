using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URSSManager : MonoBehaviour {

    public int players = 4;
    public Seat[] seats;
    public List<Seat> playerSeats;

    public List<Sprite> gorro;
    public List<Sprite> cara;
    public List<Sprite> gafas;
    public List<Sprite> camiseta;
    public List<Sprite> raya;

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
    }

    public void RandomizePlayers()
    {
        int camiseta = Random.Range(0, 4);
    }
        

}
