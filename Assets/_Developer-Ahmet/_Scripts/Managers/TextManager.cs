using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TextManager : MonoBehaviour
{
    List<GameText> ToiletSpeakingTexts = new List<GameText>();
    public static TextManager instance { get; private set; }
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
    public void TextsInit()
    {
        ToiletSpeakingTextsInit();
    }
    private void ToiletSpeakingTextsInit()
    {
        GameText t1 = new GameText(1, "Sonunda uyand�n! Ho� geldin, karde�im.", "Konu�an Klozet");
        GameText t2 = new GameText(2, "Merhaba! Bu okulda s�radan bir g�n de�il.", "Konu�an Klozet");
        GameText t3 = new GameText(3, "Etrafa dikkatlice bak, her k��ede bir s�r sakl�.", "Konu�an Klozet");
        GameText t4 = new GameText(4, "G�revimiz zorlu, ama birlikte ba�arabiliriz.", "Konu�an Klozet");
        GameText t5 = new GameText(5, "Haz�rsan, maceraya ba�lamaya ne dersin?", "Konu�an Klozet");
        GameText t6 = new GameText(6, "Unutma, cesaret ve zeka en b�y�k silahlar�m�z.", "Konu�an Klozet");
        GameText t7 = new GameText(7, "Yolculu�umuz hen�z ba�lad�, daha ne zorluklar bizi bekliyor.", "Konu�an Klozet");
        GameText t8 = new GameText(8, "�leriye do�ru ad�m at, biz seninle birlikteyiz.", "Konu�an Klozet");
        GameText t9 = new GameText(9, "Heyecanl� m�s�n? �imdi ger�ek maceraya ba�layaca��z.", "Konu�an Klozet");
        GameText t10 = new GameText(10, "Korkma, senin yan�nday�z ve bu macerada birlikte ilerleyece�iz.", "Konu�an Klozet");


        ToiletSpeakingTexts.Add(t1);
        ToiletSpeakingTexts.Add(t2);
        ToiletSpeakingTexts.Add(t3);
        ToiletSpeakingTexts.Add(t4);
        ToiletSpeakingTexts.Add(t5);
        ToiletSpeakingTexts.Add(t6);
        ToiletSpeakingTexts.Add(t7);
        ToiletSpeakingTexts.Add(t8);
        ToiletSpeakingTexts.Add(t9);
        ToiletSpeakingTexts.Add(t10);
    }
    public List<GameText> GetToiletTexts()
    {
        return ToiletSpeakingTexts;
    }
    public GameText GetTheNextText()
    {
        ToiletSpeakingTexts.Remove(ToiletSpeakingTexts[0]);
        return ToiletSpeakingTexts[0];
    }
}
public class GameText
{
    public int ID;
    public string Text;
    public string Title;
    public string Description;
    public GameText(int _id, string _text, string _title, string _descriptipn = "")
    {
        ID = _id;
        Text = _text;
        Title = _title;
        Description = _descriptipn;
    }
}
