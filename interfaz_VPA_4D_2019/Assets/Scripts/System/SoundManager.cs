using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Controller Sound")]
    [SerializeField]
    Button muteButton;
    [SerializeField]
    bool mute;
    [SerializeField]
    Sprite[] songIcons;
    public AudioMixer audioMixer;
    public List<Sound> songs = new List<Sound>();
    
    [Header("Provicional Stats Sound Controller")]
    public List<Sound> provicionalLevelSound = new List<Sound>();
    List<Sound> randomSound = new List<Sound>();
    public List<AudioSource> pauseSoundProvicional = new List<AudioSource>();
    float currentVolumen;

    public void CreateSoundsLevel(MusicLevel musicLevel)
    {
        foreach (Sound song in songs)
        {
            if (song.levelSound == musicLevel)
            {
                song.source = gameObject.AddComponent<AudioSource>();
                provicionalLevelSound.Add(song);
                song.source.clip = song.song;
                song.source.volume = song.volume;
                song.source.pitch = song.pitch;
                song.source.loop = song.loop;
                song.source.outputAudioMixerGroup = song.mixerGroup;
            }
        }
    }
    
    public void DeleteSoundsLevel()
    {
        foreach (Sound s in provicionalLevelSound)
        {
            Destroy(s.source);
        }

        provicionalLevelSound.Clear();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(Tags.VOLUMENMASTER_TAG, volume);
    }

    public void MuteAudio()
    {
        mute = !mute;

        if (mute)
        {
            muteButton.image.sprite = songIcons[1];
            audioMixer.GetFloat(Tags.VOLUMENMASTER_TAG, out currentVolumen);
            audioMixer.SetFloat(Tags.VOLUMENMASTER_TAG, 0);
        }
        else
        {
            muteButton.image.sprite = songIcons[0];
            audioMixer.SetFloat(Tags.VOLUMENMASTER_TAG, currentVolumen);
        }
    }

    public void PlayNewSound(string name)
    {
        Sound s = songs.Find(sounds => sounds.name == name);

        if (s != null)
        {
            s.source.Play();
        }
        else
        {
            Debug.LogError("No Existe Track" + name);
        }
    }

    public void RandomBackGroundSound()
    {
        foreach (Sound randomS in provicionalLevelSound)
        {
            if (randomS.musicType == MusicType.BACKGROUND)
            {
                randomSound.Add(randomS);
            }
        }

        PlayNewSound(randomSound[Random.Range(0, randomSound.Count)].name);

        randomSound.Clear();
    }

    public void PauseAllSounds(bool validation)
    {
        if (validation)
        {
            foreach (Sound s in provicionalLevelSound)
            {
                if (s.source.isPlaying)
                {
                    pauseSoundProvicional.Add(s.source);
                    s.source.Pause();
                }
            }
        }
        else
        {
            for (int i = 0; i < pauseSoundProvicional.Count; i++)
            {
                pauseSoundProvicional[i].Play();
            }

            pauseSoundProvicional.Clear();
        }
    }

    public void EndSound(string name)
    {
        Sound s = songs.Find(sounds => sounds.name == name);

        if (s != null)
        {
            s.source.Stop();
        }
        else
        {
            Debug.LogError("Track No Found" + name);
        }
    }
}


