using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camcorder : MonoBehaviour
{
    public static Camcorder Instance { get; set; }

    public GameObject nightVision;

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

    public void PickUpCamcorder(GameObject pickedUpCamcorder)
    {
        nightVision.SetActive(true);
        nightVision.GetComponent<NightVision>().ToggleNightVision();
        nightVision.GetComponent<NightVision>().SetPickedUp();

    }
}
