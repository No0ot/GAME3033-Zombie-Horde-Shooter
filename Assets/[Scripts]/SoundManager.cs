using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Serializable]
    public struct EditorDictionary
    {
        public string name;
        public AudioClip clip;
    }

    [SerializeField]
    public EditorDictionary[] clips;

    public Dictionary<string, AudioClip> dictionaryClips = new Dictionary<string, AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(EditorDictionary ed in clips)
        {
            dictionaryClips.Add(ed.name, ed.clip);
        }
    }

    public AudioClip GetSound(string name)
    {
        return dictionaryClips[name];
    }
}
