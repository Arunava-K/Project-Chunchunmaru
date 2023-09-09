using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameRateCounter : MonoBehaviour
{
    public Text fpsText;
    public float updateInterval = 0.5f; // How often to update the FPS display

    private float accum = 0f;
    private int frames = 0;
    private float timeLeft;

    private void Start()
    {
        if (fpsText == null)
        {
            Debug.LogError("Please assign a Text object to the FPS Counter script.");
            enabled = false; // Disable the script to prevent errors
            return;
        }

        timeLeft = updateInterval;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        // Update the FPS display every updateInterval seconds
        if (timeLeft <= 0)
        {
            float fps = accum / frames;
            fpsText.text = "FPS: " + Mathf.Round(fps);
            timeLeft = updateInterval;
            accum = 0f;
            frames = 0;
        }
    }
}
