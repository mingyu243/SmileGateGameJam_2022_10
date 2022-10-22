using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static NarrationPlayer;

public class TriggerEvent : MonoBehaviour
{
    public AudioSource audioSource;

    public NarrationList narrations;

    public bool isFirst = true;

    public UnityEvent OnTriggerEvent;

    public void PlayAudio()
    {
        PlayAudioClips().Forget();
    }

    public async UniTask PlayAudioClips()
    {
        for (int i = 0; i < narrations.Data.Length; i++)
        {
            Narration n = narrations.Data[i];

            audioSource.clip = n._clip;
            //AudioSource.PlayClipAtPoint(n._clip, transform.position);
            audioSource.Play();

            if (n._waitEnd)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(n._clip.length));
            }
            await UniTask.Delay(TimeSpan.FromSeconds(n._afterDelay));
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(isFirst)
            {
                isFirst = false;
                OnTriggerEvent?.Invoke();
            }
        }
    }
}
