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
        GameText t1 = new GameText(1, "Sonunda uyandýn! Hoþ geldin, kardeþim.", "Konuþan Klozet");
        GameText t2 = new GameText(2, "Merhaba! Bu okulda sýradan bir gün deðil.", "Konuþan Klozet");
        GameText t3 = new GameText(3, "Etrafa dikkatlice bak, her köþede bir sýr saklý.", "Konuþan Klozet");
        GameText t4 = new GameText(4, "Görevimiz zorlu, ama birlikte baþarabiliriz.", "Konuþan Klozet");
        GameText t5 = new GameText(5, "Hazýrsan, maceraya baþlamaya ne dersin?", "Konuþan Klozet");
        GameText t6 = new GameText(6, "Unutma, cesaret ve zeka en büyük silahlarýmýz.", "Konuþan Klozet");
        GameText t7 = new GameText(7, "Yolculuðumuz henüz baþladý, daha ne zorluklar bizi bekliyor.", "Konuþan Klozet");
        GameText t8 = new GameText(8, "Ýleriye doðru adým at, biz seninle birlikteyiz.", "Konuþan Klozet");
        GameText t9 = new GameText(9, "Heyecanlý mýsýn? Þimdi gerçek maceraya baþlayacaðýz.", "Konuþan Klozet");
        GameText t10 = new GameText(10, "Korkma, senin yanýndayýz ve bu macerada birlikte ilerleyeceðiz.", "Konuþan Klozet");


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
