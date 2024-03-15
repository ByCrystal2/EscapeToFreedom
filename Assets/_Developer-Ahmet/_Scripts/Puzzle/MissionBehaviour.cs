using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionBehaviour : MonoBehaviour
{
    [SerializeField] public Toggle ComplateToggle;
    [SerializeField] Image imgFiller;
    [SerializeField] TextMeshProUGUI txtMessage;
    [SerializeField] GameObject ComplateStrikeObj;

    private GameMission MyMission;

    private void Awake()
    {
        ComplateToggle.onValueChanged.AddListener((bool _value) =>
        {
            if (_value)
            {
                Tween _dOMove = UIManager.instance.StartDOMoveMissionPanel();
                _dOMove.OnUpdate(() =>
                {
                    // update
                }).OnComplete(() => StartCoroutine(ComplateAndAddMissionCoroutine()));
            }        
        });
    }
    private void OnEnable()
    {
        if (MyMission != null)
        {
            ComplateToggle.isOn = false;
            imgFiller.fillAmount = 0;
            ComplateStrikeObj.SetActive(false);
            txtMessage.text = MyMission.Description;
            Color _newColor = txtMessage.color;
            _newColor.a = 255f;
            txtMessage.color = _newColor;
        }
        else
            Debug.Log(name + " Adli gorev objesi'nin mission classi null.");
    }
    private IEnumerator ComplateAndAddMissionCoroutine()
    {
        yield return ComplateMission();
        PuzzleManager.instance.AddMissionCoroutine(null); // Buraya ekleme yapýlacak
    }
    IEnumerator ComplateMission()
    {
        int timer = 3;
        float currentTime = 0f;
        while (currentTime < timer)
        {
            currentTime += Time.deltaTime;
            imgFiller.fillAmount += currentTime / timer;
            yield return new WaitForEndOfFrame();
        }
        ComplateStrikeObj.SetActive(true);
        Color _newColor = txtMessage.color;
        _newColor.a = 40f;
        txtMessage.color = _newColor;
        yield return new WaitForSeconds(0.1f);
        PuzzleManager.instance.MissionCompateManipulation(this);
    }
    public GameMission GetMyMission()
    {
        return MyMission;
    }
    public void SetMyMission(GameMission _mission)
    {
        MyMission = _mission;
    }
}
