using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioSource _audioSource;

    float backupVolume;

    private void Start()
    {
        backupVolume = _audioSource.volume;
    }

    public void Play(float fade = 1f)
    {
        _audioSource.Play();
        StartCoroutine(Fade());

        IEnumerator Fade()
        {
            for (float t = 0; t < fade; t += Time.deltaTime)
            {
                _audioSource.volume = Mathf.Lerp(0, backupVolume, t / fade);
                yield return null;
            }

            _audioSource.volume = backupVolume;
        }
    }

    public void Stop(float fade = 1f)
    {
        StartCoroutine(Fade());

        IEnumerator Fade()
        {
            for (float t = 0; t < fade; t += Time.deltaTime)
            {
                _audioSource.volume = Mathf.Lerp(0, backupVolume, 1 - (t / fade));
                yield return null;
            }

            _audioSource.volume = 0;
            _audioSource.Stop();
        }
    }
}
