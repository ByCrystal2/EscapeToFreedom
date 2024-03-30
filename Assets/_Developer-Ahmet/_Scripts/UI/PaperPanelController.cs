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

    [SerializeField] GameObject closeThisPanelInputInfo;
    [SerializeField] float TextPrintingSpeed = 500f;
    List<char> messageChars;
    bool startPaper = false;
    public void StartPaperText(string _header, string _message)
    {
        txtHeader.text = _header;
        txtMessage.text = "";
        txtMessage.paragraphSpacing = 100f;
        txtMessage.wordSpacing = 7f;
        messageChars = _message.ToList();
        startPaper = true;
    }
    float timer = 0.05f;
    float currentTime = 0f;
    private void Update()
    {
        if (!startPaper) return;
        if (messageChars != null && messageChars.Count > 0)
        {
            if (currentTime <= timer)
            {
                currentTime += Time.deltaTime * TextPrintingSpeed;
            }
            else
            {
                currentTime = 0f;
                txtMessage.text += messageChars[0];
                messageChars.RemoveAt(0); // Doðrudan listeden öðeyi kaldýrýr.
            }
        }
        else
        {
            closeThisPanelInputInfo.SetActive(true);
            // Eðer mesaj karakterleri null ise, zaten listede bir þey yok demektir.
            // Dolayýsýyla, messageChars'ýn null olup olmadýðýný kontrol etmeye gerek yok.
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameObject.SetActive(false);
                PlayerManager.instance.PlayerUnlock();
                startPaper = false;
            }
            
        }
    }
}
