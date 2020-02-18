using UnityEngine;

public class UVArea : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "UV") other.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true; // Показываем улику
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "UV") other.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false; // Скрываем
    }
}
