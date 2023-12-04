using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Letter : MonoBehaviour
{
    public static Letter Instance { get; set; }

    public PlayerMovement player;

    [Header("UI Text")]
    [SerializeField] private GameObject letterCanvas;
    [SerializeField] private TMP_Text letterTextArea;

    [Space(10)]
    [SerializeField] [TextArea] private string letterText;

    [Space(10)]
    [SerializeField] private UnityEvent openEvent;
    private bool isOpen = false;

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
    public void ReadLetter(GameObject letter)
    {
        letterTextArea.text = letterText;
        letterCanvas.SetActive(true);
        DisablePlayer(true);
        isOpen = true;
    }

    void DisableLetter()
    {
        letterCanvas.SetActive(false);
        DisablePlayer(false);
        isOpen = false;
    }

    void DisablePlayer(bool disable)
    {
        player.enabled = !disable;
    }

    private void Update()
    {
        if (isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DisableLetter();
            }
        }
    }
}
