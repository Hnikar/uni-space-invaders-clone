using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    public static SFXManager instance;
    
    public float sfxVolume = 1f;
    [SerializeField] private AudioSource soundFXObject;

    private void Awake(){
        if(instance == null){
            instance = this;
        } 
    }

    public void PlaySFX(AudioClip audioClip, Transform spawnTransform){
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = sfxVolume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }    
}
