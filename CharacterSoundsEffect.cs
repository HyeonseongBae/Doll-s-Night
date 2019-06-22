using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundsEffect : MonoBehaviour
{
    static CharacterSoundsEffect instance;

    [SerializeField]
    Object[] soundsArray;

    AudioSource source;


    float sourceVolume;
    float curVolume;
    float lerpTime = 0f;
    bool isLerp = false;

    static public CharacterSoundsEffect Getinstance()
    {
        if (instance == null)
        {
            Debug.Log("CharacterSoundsEffect havn't instance!");
        }
        return instance;
    }

    void Awake()
    {
        if (instance == null) instance = this;
        source = GetComponent<AudioSource>();
        soundsArray = Resources.LoadAll("Sounds/Character/Effect", typeof(AudioClip));
        sourceVolume = source.volume;
        curVolume = sourceVolume;
    }

    void Start()
    {

    }

    void Update()
    {
        VolumeLerp();
    }

    void VolumeLerp()
    {
        if (isLerp)
        {
            curVolume = Mathf.Lerp(curVolume, 0, lerpTime);
            source.volume = curVolume;
            if (curVolume < 0.01f)
            {
                source.Stop();
                isLerp = false;
                curVolume = sourceVolume;
                source.volume = sourceVolume;
            }
        }
    }

    public void PlayEffect(int _index, float _delay = 0, bool _breath = false)
    {
        AudioClip clip = soundsArray[_index] as AudioClip;
        source.clip = clip;
        source.PlayDelayed(_delay);
        if (_breath) CharacterBreathEffect.Getinstance().PlayBreath();
    }
}
