using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBreathEffect : MonoBehaviour
{

    static CharacterBreathEffect instance;
    AudioSource source;
    static public CharacterBreathEffect Getinstance()
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
    }

    public void PlayBreath()
    {
        source.PlayDelayed(0f);
    }
}
