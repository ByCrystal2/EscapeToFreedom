using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    Coroutine GameTimeCoroutine;
    [SerializeField] int totalGameTime = 3600;
    public static TimeManager instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public IEnumerator TimeControl(int totalSeconds, TextMeshProUGUI _text, Puzzle _puzzle = null, PersonnelBehaviour _currentPersonnel = null)
    {
        while (totalSeconds >= 0)
        {
            if (_puzzle != null)
            {
                if (_puzzle.GetIsComplate())
                {
                    
                    break;
                }
            }
            string timeText;
            TimeSpan timeSpan = TimeSpan.FromSeconds(Mathf.Max(totalSeconds, 0));            
            if (timeSpan.Hours > 0)
            {
                timeText = string.Format("{0:D2}:{1:D2}:{2:D2}",
                timeSpan.Hours,
                timeSpan.Minutes,
                timeSpan.Seconds);
            }
            else if (timeSpan.Minutes > 0)
            {
                timeText = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
            }
            else
            {                
                timeText = string.Format("{0:D2}", timeSpan.Seconds);
            }
            //countdownText.text = timeText;
            _text.text = timeText;
            yield return new WaitForSeconds(1);            
            totalSeconds--;
            if (totalSeconds == 0)
            {
                if (_puzzle != null)
                {
                    PuzzleManager.instance.PuzzleBehaviour.TimeEnding = true;
                    GameManager.instance.GameOver(GameEndType.PuzzleTimeEnding, _currentPersonnel);
                }
                else
                {
                    GameManager.instance.GameOver(GameEndType.GameTimeEnding);
                }
            }
        }
    }
    public void StartGameTime()
    {
        UIManager.instance.SetActivationGameTimePanel(true);
        if (GameTimeCoroutine == null)
        {
            GameTimeCoroutine = StartCoroutine(TimeControl(totalGameTime, UIManager.instance.txtGameTime));
        }
    }
    public void StopGameTimeCoroutine()
    {
        StopCoroutine(GameTimeCoroutine);
    }
    enum TimeType
    {
        seconds, minutes, clocks
    }
}
