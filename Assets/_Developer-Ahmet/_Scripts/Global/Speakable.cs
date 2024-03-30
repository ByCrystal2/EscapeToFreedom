using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speakable : MonoBehaviour, ISpeakable
{
    private bool isSpeak;
    public void Speak()
    {
        throw new System.NotImplementedException();
    }
    public bool GetIsSpeak()
    {
        return isSpeak;
    }
    public void SetIsSpeak(bool _speak)
    {
        isSpeak = _speak;
    }
}
public interface ISpeakable
{
    public void Speak();
}