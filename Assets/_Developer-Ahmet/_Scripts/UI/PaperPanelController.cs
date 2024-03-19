using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PaperPanelController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtHeader;
    [SerializeField] TextMeshProUGUI txtMessage;
    public float TextPrintingSpeed = 250f;
    List<char> messageChars;
    public void StartPaperText(string _header, string _message)
    {
        txtHeader.text = _header;
        txtMessage.text = "";
        txtMessage.paragraphSpacing = 100f;
        txtMessage.wordSpacing = 7f;
        messageChars = _message.ToList();
    }
    float timer = 2f;
    float currentTime = 0f;
    private void Update()
    {        
        if (messageChars != null && messageChars.Count > 0 )
        {
            if(currentTime <= timer )
            {
                currentTime += Time.deltaTime * TextPrintingSpeed;
            }
            else
            {                
                currentTime = 0f;
                txtMessage.text += messageChars[0];
                messageChars.Remove(messageChars[0]);
            }
        }
        else
        {            
            messageChars = null;
            gameObject.SetActive(false);
            PlayerManager.instance.PlayerUnlock();
        }
    }
}
