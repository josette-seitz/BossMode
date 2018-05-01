using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(ViveController))]
public class Hand : MonoBehaviour {

    GameObject heldObject;
    ViveController controllerObj;
    Rigidbody simulator;
    //Can select via broken GUI :(
    public EVRButtonId pickUpButton;
    public EVRButtonId dropButton;
    
	void Start () {
        simulator = new GameObject().AddComponent<Rigidbody>();
        simulator.name = "simulator";
        simulator.transform.parent = transform.parent;
        //initialize ViveController
        controllerObj = GetComponent<ViveController>();
	}
	
	void Update () {
        //Check if there's a held object already
        if (heldObject)
        {
            simulator.velocity = (transform.position - simulator.position) * 50f;
            if (controllerObj.controller.GetPressDown(pickUpButton) && heldObject.GetComponent<HeldObject>().dropOnRelease || controllerObj.controller.GetPressDown(dropButton) && !heldObject.GetComponent<HeldObject>().dropOnRelease)
            {
                heldObject.transform.parent = null;
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject.GetComponent<Rigidbody>().velocity = simulator.velocity;
                heldObject.GetComponent<HeldObject>().parent = null;
                heldObject = null;
            }
        }
        //if there isn't a held object
        else
        {
            //Check if the controller trigger has been pressed
            if (controllerObj.controller.GetPressDown(pickUpButton))
            {
                //anything small radius(0.1f) of the controller
                Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);
                foreach (Collider coll in colliders)
                {
                    //make sure is not held
                    if (heldObject == null && coll.GetComponent<HeldObject>() && coll.GetComponent<HeldObject>().parent == null)
                    {
                        heldObject = coll.gameObject;
                        heldObject.transform.parent = transform;
                        heldObject.transform.localPosition = Vector3.zero;
                        heldObject.transform.localRotation = Quaternion.identity;
                        heldObject.GetComponent<Rigidbody>().isKinematic = true;
                        //set parent of held object
                        heldObject.GetComponent<HeldObject>().parent = controllerObj;
                    }
                }
            }

        }
		
	}
}
