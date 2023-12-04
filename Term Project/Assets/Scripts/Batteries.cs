using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batteries : MonoBehaviour
{
    public static Batteries Instance { get; set; }

    public GameObject flashlight;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PickupBatteries(GameObject pickedupBatteries)
    {
        flashlight.GetComponent<Flashlight>().batteries += 1;
        // pickup sound
    }
}
