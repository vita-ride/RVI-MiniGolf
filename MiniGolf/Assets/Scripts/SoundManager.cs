using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] CrowdClips;
    [SerializeField] AudioClip[] BallInHoleClips;
    [SerializeField] AudioClip[] PuttClips;
    [SerializeField] AudioClip BallCollisionClip;
    private AudioSource AudioSource;
    static SoundManager instance;
    void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        AudioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
    }
    public static SoundManager GetInstance()
    {
        return instance;
    }
    public void PlayCrowdSound()
    {
        AudioClip clip = CrowdClips[UnityEngine.Random.Range(0, CrowdClips.Length)];
        AudioSource.PlayOneShot(clip);
    }

    public void PlayBallInHoleSound()
    {
        AudioClip clip = BallInHoleClips[UnityEngine.Random.Range(0, BallInHoleClips.Length)];
        AudioSource.PlayOneShot(clip);
    }

    public void PlayPuttSound()
    {
        AudioClip clip = PuttClips[UnityEngine.Random.Range(0, PuttClips.Length)];
        AudioSource.PlayOneShot(clip);
    }

    public void PlayBallCollisionSound()
    {
        AudioSource.PlayOneShot(BallCollisionClip);
    }

}
