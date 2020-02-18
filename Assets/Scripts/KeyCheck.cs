using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using System;

public class KeyCheck : MonoBehaviour
{
    public GameObject key;
    public GameObject door;

    public MissionObject missionObject;

    private bool isDoorOpen = false;

    void Update()
    {
        if(isDoorOpen && GetComponent<AudioSource>().isPlaying == false)
        {
            door.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<KeyCheck>().enabled = false;

            if (missionObject != null) missionObject.EndMission();
        }  
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == key && isDoorOpen == false)
        {
            isDoorOpen = true;
            GetComponent<AudioSource>().Play();
        }  
    }
}
