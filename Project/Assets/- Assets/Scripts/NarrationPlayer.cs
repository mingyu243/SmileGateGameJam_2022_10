using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NarrationPlayer : MonoBehaviour
{
    [Serializable]
    public class Narration
    {
        public AudioClip _clip;
        public bool _waitEnd;
        public float _afterDelay;
    }

    [SerializeField] AudioSource _audioSource;

    [Header("Sounds")]
    [SerializeField] NarrationList _title;
    [SerializeField] NarrationList _intro;


    public async UniTask PlayTitleAsync() => await PlayNarrations(_title);
    public async UniTask PlayIntroAsync() => await PlayNarrations(_intro);


    async UniTask PlayNarrations(NarrationList narrations)
    {
        for (int i = 0; i < narrations.Data.Length; i++)
        {
            Narration n = narrations.Data[i];

            _audioSource.clip = n._clip;
            AudioSource.PlayClipAtPoint(n._clip, transform.position);
            //_audioSource.Play();

            if(n._waitEnd)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(n._clip.length));
            }
            await UniTask.Delay(TimeSpan.FromSeconds(n._afterDelay));
        }
    }
}
