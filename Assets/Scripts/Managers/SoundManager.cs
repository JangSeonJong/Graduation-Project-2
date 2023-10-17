using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource[] audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>(); // path 를 키로 사용해 AduioClip 저장

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            Init();
        }
        else
            Destroy(gameObject);
    }

    void Init()
    {
        string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
        for (int i = 0; i < soundNames.Length - 1; i++)
        {
            GameObject go = new GameObject { name = soundNames[i] };
            audioSources[i] = go.AddComponent<AudioSource>();
            go.transform.parent = instance.transform;
        }

        audioSources[(int)Define.Sound.Bgm].loop = true;
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        audioClips.Clear();
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);

        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = audioSources[(int)Define.Sound.Bgm];

            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        else
        {
            AudioSource audioSource = audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    // Dictionary 에 재생할 clip 이 있으면 가져옴, 없으면 메모리에서 불러오고 Dictionary에 저장
    AudioClip GetOrAddAudioClip(string path, Define.Sound tpye = Define.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (tpye == Define.Sound.Bgm)
        {
            audioClip = Resources.Load<AudioClip>(path);
        }

        else
        {
            if (audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"Audio Clip Missing ({path})");

        return audioClip;
    }

    public AudioClip GetAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        return GetOrAddAudioClip(path,type);
    }
}
