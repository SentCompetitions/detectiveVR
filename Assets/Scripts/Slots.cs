using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    public GameObject VrCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(VrCamera.transform.position.x, 0f, VrCamera.transform.position.z);

        var angles = transform.rotation.eulerAngles;
        angles.y = VrCamera.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(angles);
    }
}
