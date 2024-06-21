using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Entered {other.name}");
        door.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Exited {other.name}");
        door.SetActive(true);
    }
}
