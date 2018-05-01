using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(HeldObject))]
public class GunScript : MonoBehaviour {
    public GameObject projectile;
    public float speed;
    public Transform fire;
    private EVRButtonId shootButton;
    HeldObject heldObject;

    void Start () {
        heldObject = GetComponent<HeldObject>();
        shootButton = EVRButtonId.k_EButton_SteamVR_Trigger;
    }
	
	void Update () {
        if (heldObject.parent != null && heldObject.parent.controller.GetPressDown(shootButton))
        {
            GameObject projectiles = Instantiate(projectile, fire.position, fire.rotation);
            projectiles.GetComponent<Rigidbody>().velocity = fire.forward * speed;
        }
		
	}
}
