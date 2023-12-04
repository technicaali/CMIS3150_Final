using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class NightVision : MonoBehaviour
{
    [SerializeField] private Color defaultLightColor;
    [SerializeField] private Color boostedLightColor;

    public Canvas StaticCamCanvas;

    private bool isNightVisionEnabled = false;
    private bool isPickedUp = false;

    private PostProcessVolume volume;

    private void Start()
    {
        RenderSettings.ambientLight = defaultLightColor;

        StaticCamCanvas.enabled = false;

        volume = gameObject.GetComponent<PostProcessVolume>();
        volume.weight = 0;
    }

    private void Update()
    {
        if (isPickedUp && Input.GetKeyDown(KeyCode.C))
        {
            ToggleNightVision();
        }
    }

    public void ToggleNightVision()
    {
        isNightVisionEnabled = !isNightVisionEnabled;
        if (isNightVisionEnabled)
        {
            RenderSettings.ambientLight = boostedLightColor;
            volume.weight = 1;
            StaticCamCanvas.enabled = true;
        }
        else
        {
            RenderSettings.ambientLight = defaultLightColor;
            volume.weight = 0;
            StaticCamCanvas.enabled = false;
        }
    }

    public void SetPickedUp()
    {
        isPickedUp = true;
    }
}
