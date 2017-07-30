using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    [SerializeField]
    private AudioSource audioSourceFX;

    Dictionary<string, AudioClip> audioClipList = new Dictionary<string, AudioClip>();

    void Awake() {
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>("Audio") as AudioClip[];
        foreach (AudioClip audioClip in audioClips) {
            audioClipList.Add(audioClip.name, audioClip);
        }
    }
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayAudioClipFX(string name) {
        audioSourceFX.PlayOneShot(audioClipList[name]);
    }
}
