using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private static Dictionary<string, Sound> listSound;

    public Sound[] sounds;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            listSound = new Dictionary<string, Sound>(sounds.Length);
            DontDestroyOnLoad(gameObject);
            if (enabled)
                LoadSound();
        }

        void LoadSound()
        {
            foreach (Sound s in sounds)
                AddSound(s);

            void AddSound(Sound s)
            {
                if (!listSound.ContainsKey(s.name))
                {
                    s.source = instance.gameObject.AddComponent<AudioSource>();
                    s.source.clip = s.clip;
                    s.source.loop = s.loop;
                    s.source.volume = s.volume;
                    s.source.pitch = s.pitch;
                    listSound.Add(s.name, s);
                }
            }
        }
    }


    public void Play(string sound)
    {
        _Play(sound);
    }

    public void PlayDelayed(string sound, float delay)
    {
        DOVirtual.DelayedCall(delay, () => _Play(sound));
    }

    void _Play(string sound)
    {
        Sound s = listSound[sound];
        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        s.source.Play();
    }
}
