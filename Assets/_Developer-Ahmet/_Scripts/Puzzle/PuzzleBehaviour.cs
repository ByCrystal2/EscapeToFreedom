using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzleBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtHeader;
    [SerializeField] TextMeshProUGUI txtTime;
    public bool TimeEnding;    
    public void SetPuzzleUI(Puzzle _puzzle, int _step)
    {
        if (_step == 0)
        {
            StartCoroutine(TimeManager.instance.TimeControl(_puzzle.TimeInterval, txtTime, null,GameManager.instance.CurrentCathedPlayerPersonel));
            UIManager.instance.StartDOMovePuzzlePanel();
        }
    }
}
