using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class SpeakingPanelController : MonoBehaviour
{
    [SerializeField] Image imgIcon;
    [SerializeField] Text txtHeader;
    [SerializeField] Text txtMessage;
    static PlayableDirector toiletSpeakingPlayable;
    [SerializeField] Animator toiletAnimator;
    [SerializeField] Sprite[] speakingSprites; // [0] => Player, [1] => Closet ... => WhoNext Enum;

    Coroutine waitCoroutine;
    private void OnEnable()
    {
        if (toiletSpeakingPlayable == null)
        {
            toiletSpeakingPlayable = GetComponentInChildren<PlayableDirector>();
        }
    }
    bool isSkipped;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isSkipped = !isSkipped;
            if (isSkipped)
            {
                if (waitCoroutine!=null)
                    StopCoroutine(waitCoroutine);
                TextManager.instance.SetWaitSetup(true);
                WriteSkippedText();
            }
            else
            {
                TextManager.instance.GetTheNextText();
                TextManager.instance.SetWaitSetup(false);
            }
        }
    }
    public static void PlayToiletSpeakingPlayable()
    {
        toiletSpeakingPlayable.Play();
    }
    public static void StopToiletSpeakingPlayable()
    {
        toiletSpeakingPlayable.Stop();
    }
    private void WriteSkippedText()
    {
        txtMessage.text = TextManager.instance.GetCurrentText().Text;
    }
    public void SetupUIs() // SpeakingSetupUIs Signal
    {
        if (TextManager.instance.GetWaitSetup()) return;
        GameText currentText = TextManager.instance.GetCurrentText();
        if (true)
        {

        }
        if (waitCoroutine != null)
            StopCoroutine(waitCoroutine);
        TextManager.instance.SetWaitSetup(true);
        imgIcon.sprite = GetSpeakingSprite(currentText);
        txtHeader.text = currentText.Title;
        txtMessage.text = "";
        // Coroutine ile metni yazdýr
        waitCoroutine = StartCoroutine(WriteTextRoutine());
    }
    public void StartOrStopAnimator(bool _start)
    {
        toiletAnimator.enabled = _start;
        // PlayableAsset içindeki tüm output track'leri döngüye al
        //foreach (var binding in toiletSpeakingPlayable.playableAsset.outputs)
        //{
        //    // Eðer track'in sourceObject'i istediðiniz türdeyse, onu alýn
        //    if (binding.sourceObject is Animator)
        //    {
        //        Animator animator = toiletSpeakingPlayable.GetGenericBinding(binding.sourceObject) as Animator;
        //        animator.enabled = _start;
        //    }
        //}
    } // sýrada ne var personel anahtarýný bulduk
    WaitForSecondsRealtime frame = new WaitForSecondsRealtime(0.05f);
    private IEnumerator WriteTextRoutine()
    {
        int length = TextManager.instance.GetCurrentText().Text.Length;
        for (int i = 0; i < length; i++)
        {
            char _currentChar = WriteText(TextManager.instance.GetTheNextLetter());
            if (_currentChar == ',' || _currentChar == '.' || _currentChar == '?')
                frame.waitTime = 0.2f;
            else
                frame.waitTime = 0.05f;
            yield return frame;
        }
        GameText currentText = TextManager.instance.GetTheNextText();
        if (currentText != null)
        {
            if (currentText.Who == WhosNext.Closet)
            {
                StartOrStopAnimator(true);
            }
            else
            {
                StartOrStopAnimator(false);
            }
        }        
        TextManager.instance.SetWaitSetup(false);
    }
    
    public char WriteText(char _c)
    {
        txtMessage.text += _c;
        AudioManager.instance.PlayOneShotKeyPress();
        return _c;
    }
    public Sprite GetSpeakingSprite(GameText _currentText)
    {
        Sprite currentSprite = (_currentText.Who) switch
        {
            WhosNext.Player => speakingSprites[(int)WhosNext.Player],
            WhosNext.Closet => speakingSprites[(int)WhosNext.Closet],
            _ => null
        };
        return currentSprite;
    }
}
