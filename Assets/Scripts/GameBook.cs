using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GameBook : MonoBehaviour
{
    [Header("Action")]
    // a reference to the action
    public SteamVR_Action_Boolean menu;
    // a reference to the hand
    public SteamVR_Input_Sources left;
    public SteamVR_Input_Sources right;

    [Header("Teleportation")]
    public GameObject VR_Camera;

    [Range(0f, 1f)]
    public float yOffset = 0.1f;
    [Range(0f, 5f)]
    public float distance = 0.5f;

    [Header("Text")]
    public Text body;
    

    void Start()
    {
        menu.AddOnStateDownListener(MenuUp, left);
        menu.AddOnStateUpListener(MenuDown, left);
        menu.AddOnStateDownListener(MenuUp, right);
        menu.AddOnStateUpListener(MenuDown, right);
        
        Storage.gameBook = this;        
    }
    public void MenuUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Called menu!");

        Vector3 pos = VR_Camera.transform.position + VR_Camera.transform.forward * distance;
        pos.y -= yOffset;
        transform.position = pos;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;


        Vector3 targetDirection = VR_Camera.transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 360f, 360f);
        Debug.DrawRay(transform.position, newDirection, Color.red);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
    public void MenuDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {

    }

    public void Next()
    {
        Debug.Log("[GameBook] Next");
    }

    public void UpdateMission(Mission mission)
    {
        if (mission.maxProgress != 1) body.text = mission.text + " (прогресс: " + mission.progress + ")";
        else body.text = mission.text;
    }
}
