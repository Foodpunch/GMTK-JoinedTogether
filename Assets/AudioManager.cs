using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public enum SoundType
    {
        //ENEMY SOUNDS
        BIKEREV,
        ENEMYDIE,

        //ENEMY SHOOT SOUNDS
        GUNLOAD,
        BURSTFIRE,
        ROCKETWHIZZ,
        ROCKETEXPLOSION,

        //BIRD SOUNDS
        SHOOTBIRD,
        PICKBIRD,
        BIRDCAGE,
        CAGEBREAK,


    }
    [System.Serializable]
    public class SoundFile
    {
        public SoundType soundType;
        public AudioClip clip;      //1 clip for now, variations handled through pitch shifting
        public float Volume = 0.2f;
        public bool PitchVariance;
        public bool is2D = false;
    }
    public SoundFile[] soundFiles;

    Dictionary<SoundType, float> soundTimerDictionary;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        soundTimerDictionary = new Dictionary<SoundType, float>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySound(SoundType _soundType, Vector2 pos)
    {
        GameObject soundGO = new GameObject("Sound");               //pool it later
        AudioSource _source = soundGO.AddComponent<AudioSource>();
        SoundFile soundToPlay = GetSoundFile(_soundType);
        _source.volume = soundToPlay.Volume;
        if (soundToPlay.PitchVariance) _source.pitch = Random.Range(0.8f, 2.5f);    //adjust the pitch values later
        if (!soundToPlay.is2D)
        {
            soundGO.transform.position = pos;
            _source.spatialBlend = 1;
        }
        _source.PlayOneShot(soundToPlay.clip);
    }
    public SoundFile GetSoundFile(SoundType _soundType)
    {
        foreach (SoundFile _sound in soundFiles)
        {
            if (_sound.soundType == _soundType)
            {
                return _sound;
            }
        }
        Debug.LogError("Sound " + _soundType + " does not exist!");
        return null;
    }
    public AudioClip GetAudioClip(SoundType _soundType)
    {
        foreach (SoundFile _sound in soundFiles)
        {
            if (_sound.soundType == _soundType)
            {
                return _sound.clip;
            }
        }
        Debug.LogError("Sound " + _soundType + " does not exist!");
        return null;
    }
    public bool CanPlaySound(SoundType _sound)  //for checking and only allowing ONE instance of the sound to be played
    {
        switch (_sound)
        {
            default:
                return true;
            //case SoundType.PLAYER_BUMP:         //might need to tweak delay
            //    if (soundTimerDictionary.ContainsKey(_sound))
            //    {
            //        float lastTimePlayed = soundTimerDictionary[_sound];
            //        float delay = 0.05f;
            //        if (lastTimePlayed + delay < Time.time)
            //        {
            //            soundTimerDictionary[_sound] = Time.time;
            //            return true;
            //        }
            //        else return false;
            //    }
            //    else return true;
        }
    }
}
