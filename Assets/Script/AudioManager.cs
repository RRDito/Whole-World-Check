using System;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    //this creates an array of the custom class sound
    public Sound [] sounds;
	
	// Start is called before the first frame update
    void Awake()
    {		
		
		//this takes every element of the array and creates an AudioSource for each
        foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
		}
    }

    void Start()
	{
		if (SceneManager.GetActiveScene().name!="MainMenu"&&SceneManager.GetActiveScene().name!="Campaign") {PlayerPrefs.SetFloat("MusicTime", 0);}
		//this is to reset the music timer for scenes other than MainMenu and Campaign
	}

    //the multiplicators are the volume in the settings, while the s.volume is modified in the inspector for each sound, depending on their base volume
    public void PlaySoundRndPitch(string xname, float xmin, float xmax)
    {		
        Sound s = Array.Find(sounds, y => y.name == xname);	
        float sx = PlayerPrefs.GetFloat("SoundMultiplicator");	
        s.source.volume = s.volume * sx;		
		s.source.pitch = UnityEngine.Random.Range(xmin, xmax); //every sound so far gets a randomized pitch
				
		s.source.Play();		
    }
	
	public void PlaySound(string xname) 
    {		
        Sound s = Array.Find(sounds, y => y.name == xname);
        float sx = PlayerPrefs.GetFloat("SoundMultiplicator");	
        s.source.volume = s.volume * sx;		
		s.source.Play();		
    }
	
	public void PlayMusic(string xname, bool xloop)
    {		
        Sound s = Array.Find(sounds, y => y.name == xname);
        float mx = PlayerPrefs.GetFloat("MusicMultiplicator");	
        s.source.volume = s.volume * mx;
        s.source.loop = xloop;		
		s.source.Play();		
    }
	
	public void ContinueMusic(string xname, bool xloop, float xtime)
    {		
        Sound s = Array.Find(sounds, y => y.name == xname);
        float mx = PlayerPrefs.GetFloat("MusicMultiplicator");	
        s.source.volume = s.volume * mx;
        s.source.loop = xloop;
        s.source.time = xtime;		
		s.source.Play();		
    }
	
	public void UpdateMusicVolume(string xname) //this is to update the volume of the music on the fly
	{
		Sound s = Array.Find(sounds, y => y.name == xname);
        float mx = PlayerPrefs.GetFloat("MusicMultiplicator");	
        s.source.volume = s.volume * mx;
	}
	
	public float GetMusicTime(string xname) //this returns the playback time of the music selected
	{
		Sound s = Array.Find(sounds, y => y.name == xname);        	
        return s.source.time;
	}
	
    public void ClicSound()
	{
		PlaySoundRndPitch("Clic", 0.75f, 1.25f);
	}	
    
}
