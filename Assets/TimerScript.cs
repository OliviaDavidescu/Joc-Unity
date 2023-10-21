using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f;
    private bool playing = false;

    private bool isItOver()
    {
        GameObject finalNode = GameObject.FindGameObjectWithTag("Finish");
        MazeNode Nodescript = finalNode.GetComponent<MazeNode>();
        return Nodescript.gameEnd();
    }
    void Update()
    {
        playing = !isItOver();
        if (playing)
        {
            elapsedTime += Time.deltaTime;
            double seconds = Math.Round(elapsedTime, 1);
            string timerString = string.Format("{0}", seconds) + " seconds";
            timerText.text = timerString;
        }
        else
        {
            timerText.text = "YOU WON!";
            
        }
    }
}