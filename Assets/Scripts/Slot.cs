using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
using System.Linq;
using UnityEngine.Events;
using System;

public class Slot : MonoBehaviour
{
    GameObject objectToStorage;
    UnityAction action;

    private Color startColor;
    public Color storageColor;

    // Start is called before the first frame update
    void Start()
    {
        startColor = gameObject.GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnHandHoverBegin(Hand hand)
    {
        //Debug.Log("Interactive");

        Interactable[] myItems = FindObjectsOfType(typeof(Interactable)) as Interactable[];
        myItems = myItems.Where(val => val != gameObject.GetComponent<Interactable>()).ToArray();

        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        float radius = Mathf.Max(sphereCollider.transform.lossyScale.x, sphereCollider.transform.lossyScale.x, sphereCollider.transform.lossyScale.x) * sphereCollider.radius - 0.00f;    

        try { objectToStorage = Utility.GetNearestInteractable<Interactable>(hand.gameObject.transform.position, myItems, radius).gameObject; }
        catch (NullReferenceException) { return; }

        try { objectToStorage.GetComponent<Rigidbody>(); }
        catch (MissingComponentException) { return; }

        if (objectToStorage == gameObject) return;

        Debug.Log("Storage object " + objectToStorage);
        objectToStorage.GetComponent<Rigidbody>().isKinematic = true;
        objectToStorage.transform.SetParent(transform);     
        objectToStorage.transform.localPosition = new Vector3(0, 0, 0);

        sphereCollider.enabled = false;
        gameObject.GetComponent<MeshRenderer>().material.color = storageColor;

        action = delegate { onUse(objectToStorage, action, gameObject); };

        objectToStorage.GetComponent<Throwable>().onPickUp.AddListener(action);
        }
    void OnHandHoverEnd(Hand hand)
    {
        
    }

    void onUse(GameObject gameObject, UnityAction action, GameObject slot)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.transform.SetParent(null);
        gameObject.GetComponent<Throwable>().onPickUp.RemoveListener(action);

        slot.GetComponent<SphereCollider>().enabled = true;
        slot.GetComponent<MeshRenderer>().material.color = startColor;
    }
}
