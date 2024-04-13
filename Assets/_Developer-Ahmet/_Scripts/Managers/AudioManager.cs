using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] MenuSources;
    [SerializeField] AudioSource[] GameSources;
    [SerializeField] AudioSource PressSource;
    [SerializeField] AudioSource MouseRoomSource;
    [SerializeField] AudioSource DoorCreakingSource;
    [SerializeField] AudioSource LockOpenSource;
    [SerializeField] AudioSource PersonnelSource;
    [SerializeField] AudioSource CollectableSource;

    [SerializeField] AudioClip KeyPressSoound;
    [SerializeField] AudioClip MouseRoomSound;
    [SerializeField] AudioClip DoorCreakingSound;
    [SerializeField] AudioClip LockOpenSound;

    [Header("Personnel")]
    [SerializeField] AudioClip ISeeYouSound;
    [SerializeField] AudioClip DeathSound;
    [SerializeField] AudioClip LaughSound;
    [Header("Collectables")]
    [SerializeField] AudioClip KeySound;
    [SerializeField] AudioClip PaperSound;
    [SerializeField] AudioClip FlowerSound;
    [SerializeField] AudioClip BookSound;
    private AudioSource[] allSources;

    AudioSource currentGameSource;
    bool allAudioStop = false;
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
        allSources = FindObjectsOfType<AudioSource>();
    }
    private void Update()
    {
        if (currentGameSource == null) return;
        if (!currentGameSource.isPlaying && !allAudioStop)
        {
            StartGameSource();
        }
    }
    public void SourcesInit()
    {
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
        currentGameSource = GameSources[index];
        currentGameSource.Play();
    }
    public void PlayOneShotKeyPress()
    {
        PressSource.PlayOneShot(KeyPressSoound);
    }
    public void PlayOneShotDoorCreaking()
    {
        DoorCreakingSource.PlayOneShot(DoorCreakingSound);
    }
    public void PlayOneShotLockOpen()
    {
        LockOpenSource.PlayOneShot(LockOpenSound);
    }
    public void PlayOrStopMouseRoom(bool _play)
    {
        if (_play)
        {
            AllAudioStop(true);
            MouseRoomSource.clip = MouseRoomSound;
            MouseRoomSource.DOPlayBackwards();
        }
        else
        {
            AllAudioStop(false);
            MouseRoomSource.Stop();
        }
        
    }
    public void PlayPersonnelSound(bool _play, PersonnelSoundType _soundType)
    {
        
        if (_play)
        {
            switch (_soundType)
            {
                case PersonnelSoundType.ISeeYou:
                    if (PersonnelSource.isPlaying) return;
                    PersonnelSource.PlayOneShot(ISeeYouSound);
                    break;
                case PersonnelSoundType.Death:
                    PersonnelSource.PlayOneShot(DeathSound);
                    break;
                case PersonnelSoundType.Laugh:
                    PersonnelSource.PlayOneShot(LaughSound);
                    break;
                default:
                    break;
            }
        }
    }
    public void PlayCollectableSound(bool _play, CollectType _type)
    {
        if(_play)
        {
            switch (_type)
            {
                case CollectType.None:
                    break;
                case CollectType.FriendPaper:
                    CollectableSource.PlayOneShot(PaperSound);
                    break;
                case CollectType.Key:
                    CollectableSource.PlayOneShot(KeySound);
                    break;
                case CollectType.Mushroom:
                    CollectableSource.PlayOneShot(FlowerSound);
                    break;
                case CollectType.Flower:
                    CollectableSource.PlayOneShot(FlowerSound);
                    break;
                case CollectType.Crowbar:
                    break;
                case CollectType.Book:
                    CollectableSource.PlayOneShot(BookSound);
                    break;
                case CollectType.Knife:
                    break;
                case CollectType.WalkieTalkie:
                    break;
                case CollectType.Apple:
                    break;
                default:
                    break;
            }
        }
    }
    public void AllAudioStop(bool _stop)
    {
        if (_stop)
        {
            allAudioStop = true;
            int length = allSources.Length;
            for (int i = 0; i < length; i++)
            {
                allSources[i].transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            allAudioStop = false;
            int length = allSources.Length;
            for (int i = 0; i < length; i++)
            {
                allSources[i].transform.GetChild(i).gameObject.SetActive(true);
                currentGameSource.Play();
            }
        }
    }
}
public enum PersonnelSoundType
{
    ISeeYou,
    Death,
    Laugh
}
