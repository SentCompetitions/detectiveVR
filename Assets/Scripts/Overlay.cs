using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Overlay : MonoBehaviour
{
    public Text body;
    public Text header;

    Queue<string> bodyW;
    Queue<string> headerW;

    public void SetText(string bodyS, string headerS)
    {
        try
        {
            bodyW.Enqueue(bodyS);
            headerW.Enqueue(headerS);
        } catch (NullReferenceException)
        {
            bodyW = new Queue<string>();
            headerW = new Queue<string>();
            bodyW.Enqueue(bodyS);
            headerW.Enqueue(headerS);
        }
        if (!gameObject.activeSelf)
        {
            Display();
        }     
    }

    public IEnumerator HideOverlay(GameObject overlay)
    {
        yield return new WaitForSeconds(5f);
        overlay.SetActive(false);
        Display();
        yield return true;
    }

    private void Display()
    {
        body.text = bodyW.Dequeue();
        header.text = headerW.Dequeue();
        gameObject.SetActive(true);
        StartCoroutine("HideOverlay", gameObject);
    }
}
