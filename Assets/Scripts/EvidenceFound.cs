using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System;
using UnityEngine.UI;

public class EvidenceFound : MonoBehaviour
{
    public GameObject point;
    public GameObject notePrefab;
    [Space]
    public string evidenceExplontation;
    public Overlay overlay;
    [Space]
    public MissionObject missionObject;

    private bool isDetected;

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            if (!GetComponent<MeshRenderer>().enabled) return;
            if (!GetComponentInChildren<MeshRenderer>().enabled) return;
        } catch (MissingComponentException)
        {

        }
        //Debug.Log(other);
        GameObject hand;
        try
        {
            hand = other.GetComponentInParent<HandCollider>().hand.gameObject;
        } catch (NullReferenceException)
        {
            try
            {
                hand = other.GetComponentInParent<Interactable>().attachedToHand.gameObject;
            } catch (NullReferenceException)
            {
                return;
            }
        }

        if(hand != null && !isDetected)
        {
            Debug.Log("[Evidence] " + hand.GetComponent<Hand>() + " interact with " + gameObject);

            GameObject note = Instantiate(notePrefab, point.transform.position, point.transform.rotation);
            note.GetComponent<NoteLink>().text.text = Storage.EvidenceCount.ToString();

            overlay.SetText(evidenceExplontation, "Улика найденна:");

            missionObject.mission.AddProgress();

            isDetected = true;
        }
    }

    public IEnumerator HideOverlay(GameObject overlay)
    {
        yield return new WaitForSeconds(5f);
        overlay.SetActive(false);
        yield return true;
    }
}
