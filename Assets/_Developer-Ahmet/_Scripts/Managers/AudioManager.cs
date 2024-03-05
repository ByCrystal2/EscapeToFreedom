using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] MenuSources;
    [SerializeField] AudioSource[] GameSources;

    public static AudioManager instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        StartMenuSource();
    }
    public void StartMenuSource()
    {
        int length1 = MenuSources.Length;
        for (int i = 0; i < length1; i++)
        {
            if (MenuSources[i].isPlaying)
            {
                MenuSources[i].Stop();
                MenuSources[i].loop = false;
            }
        }
        int length = GameSources.Length;
        for (int i = 0; i < length; i++)
        {
            if (GameSources[i].isPlaying)
            {
                GameSources[i].Stop();
            }
        }
        int index = Random.Range(0,MenuSources.Length);
        MenuSources[index].Play();
        MenuSources[index].loop = true;
    }
    public void StartGameSource()
    {
        int length1 = GameSources.Length;
        for (int i = 0; i < length1; i++)
        {
            if (GameSources[i].isPlaying)
            {
                GameSources[i].Stop();
                GameSources[i].loop = false;
            }
        }
        int length = MenuSources.Length;
        for (int i = 0; i < length; i++)
        {
            if (MenuSources[i].isPlaying)
            {
                MenuSources[i].Stop();
            }
        }
        int index = Random.Range(0, GameSources.Length);
        GameSources[index].Play();
        GameSources[index].loop = true;
    }
}
