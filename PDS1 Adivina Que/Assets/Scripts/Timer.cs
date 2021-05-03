using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public ControladorMemorama memorama;

    float timeRemaining;
    bool timerIsRunning;
    bool juegoEnProgreso;

    private void Start()
    {
        timeRemaining = DataMantainer.Tiempo+5;
        timerIsRunning = DataMantainer.Contrarreloj;
        juegoEnProgreso = true;

        if (!timerIsRunning)
        {
            timeText.text = " ";
        }
        else
        {
            DisplayTime(DataMantainer.Tiempo);
        }
    }

    void Update()
    {
        juegoEnProgreso = memorama.juegoEnProgreso;
        if (juegoEnProgreso)
        {
            if (timerIsRunning)
            {
                if (timeRemaining > DataMantainer.Tiempo)
                {
                    timeRemaining -= Time.deltaTime;
                }
                else if (timeRemaining > 1)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                else
                {
                    Debug.Log("Time has run out!");
                    timeRemaining = 0;
                    timerIsRunning = false;
                    memorama.terminoPartida();
                }
            }
        }
        
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
