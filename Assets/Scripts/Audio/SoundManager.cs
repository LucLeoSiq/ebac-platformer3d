using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sfxSetups;
}

public enum MusicType
{
    TYPE_01,
    TYPE_02,
    TYPE_03,
}

[System.Serializable]
public class MusicSetup
{
    public SFXType sfxStyp;
    public AudioClip audioClip;
}

public enum SFXType
{
    TYPE_01,
    TYPE_02,
    TYPE_03,
}

public enum SFXSetup
{
    TYPE_01,
    TYPE_02,
    TYPE_03,
}