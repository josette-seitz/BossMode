using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HeldObject : MonoBehaviour {

    //Which object is being held by
    [HideInInspector]
    public ViveController parent;

    public bool dropOnRelease;
}