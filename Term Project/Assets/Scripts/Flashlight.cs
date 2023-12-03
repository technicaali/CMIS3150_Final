using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public static Flashlight Instance { get; set; }

    public GameObject flashlight;
    public TMP_Text text;

    public TMP_Text batteryText;

    public float lifetime = 100;

    public float batteries = 0;

    //public AudioSource flashON;
    //public AudioSource flashOFF;

    private bool on;
    private bool off;
    private bool isPickedUp = false;

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

    void Start()
    {
        off = true;
        flashlight.SetActive(false);
        text.GetComponent<TextMeshProUGUI>().enabled = false;
        batteryText.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    void Update()
    {
        text.text = "BATTERY: " + lifetime.ToString("0") + "%";
        batteryText.text = "BATTERIES: " + batteries.ToString();

        if (on && isPickedUp)
        {
            lifetime -= 2 * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.F))
            {
                FlashlightOff(flashlight);
            }
        }
        else if (off && isPickedUp)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                FlashlightOn(flashlight);
            }
        }

        if (lifetime <= 0)
        {
            flashlight.SetActive(false);
            on = false;
            off = true;
            lifetime = 0;
        }

        if (lifetime >= 100)
        {
            lifetime = 100;
        }

        if (Input.GetKeyDown(KeyCode.Q) && batteries >= 1)
        {
            batteries -= 1;
            lifetime += 50;
        }

        if (Input.GetKeyDown(KeyCode.Q) && batteries == 0)
        {
            return;
        }

        if (batteries <= 0)
        {
            batteries = 0;
        }
    }

    public void FlashlightOn(GameObject flashlight)
    {
        //flashON.Play();
        flashlight.SetActive(true);
        text.GetComponent<TextMeshProUGUI>().enabled = true;
        batteryText.GetComponent<TextMeshProUGUI>().enabled = true;
        on = true;
        off = false;
    }

    public void FlashlightOff(GameObject flashlight)
    {
        //flashOFF.Play();
        flashlight.SetActive(false);
        text.GetComponent<TextMeshProUGUI>().enabled = false;
        batteryText.GetComponent<TextMeshProUGUI>().enabled = false;
        on = false;
        off = true;
    }

    public void SetPickedUp()
    {
        isPickedUp = true;
    }
}
