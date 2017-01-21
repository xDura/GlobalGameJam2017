using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour {

    public GameObject takenBy; //objeto x el que esta ocupado: si ocupedBy == null -> Sin ocupar
    public bool canBeTookedByPlayer = false;
    
}
