using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PaperPanelController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtHeader;
    [SerializeField] TextMeshProUGUI txtMessage;

    [SerializeField] GameObject closeThisPanelInputInfo;
    [SerializeField] GameObject skipNoteInputInfo;
    [SerializeField] float TextPrintingSpeed = 500f;
    List<char> messageChars;
    bool startPaper = false;

    bool isSkipped = false;

    string currentMessage;
    public void StartPaperText(string _header, string _message)
    {
        closeThisPanelInputInfo.SetActive(false);
        txtHeader.text = _header;
        txtMessage.text = "";
        txtMessage.paragraphSpacing = 100f;
        txtMessage.wordSpacing = 7f;
        messageChars = _message.ToList();
        currentMessage = _message;
        startPaper = true;
        isSkipped = false;
        skipNoteInputInfo.SetActive(true);
    }
    float timer = 0.005f;
    float currentTime = 0f;
    private void Update()
    {
        if (!startPaper) return;
        if (!isSkipped && Input.GetKeyDown(KeyCode.R))
        {
            isSkipped = true;           
            txtMessage.text = currentMessage;
            skipNoteInputInfo.SetActive(false);            
        }
        if ((messageChars != null && messageChars.Count > 0) && !isSkipped)
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