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
            TimeManager.instance.StartPuzzleTime(_puzzle.TimeInterval, txtTime, _puzzle,GameManager.instance.CurrentCathedPlayerPersonel);
            UIManager.instance.StartDOMovePuzzlePanel();
        }
    }
}
