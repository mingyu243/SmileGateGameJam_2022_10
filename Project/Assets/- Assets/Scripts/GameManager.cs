using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    [SerializeField] NarrationPlayer _narrationPlayer;
    [SerializeField] BGMPlayer _BGMPlayer;

    [Header("UI")]
    [SerializeField] GameObject _blockSightObject;
    [SerializeField] TMP_Text _successText;
    [SerializeField] GameObject _titleObject;

    public async UniTaskVoid Start()
    {
        // 초기화.
        _titleObject.SetActive(false);
        _playerController.SetCanMove(false);
        _playerController.Init();

        // 연출 시작.
        await UniTask.Delay(TimeSpan.FromSeconds(3));

        await StartTitle();

        await UniTask.Delay(TimeSpan.FromSeconds(2));

        await StartIntro();

        _playerController.SetCanMove(true);
        _BGMPlayer.Play();
    }

    public void Success(int index)
    {
        if (index == 1)
        {
            _successText.text = "엔딩 1\nDream";
        }
        else if (index == 2)
        {
            _successText.text = "엔딩 2\nDespair";
        }
        else if (index == 3)
        {
            _successText.text = "엔딩 3\nDeath";
        }

        _playerController.SetCanMove(false);
        Run().Forget();

        async UniTaskVoid Run()
        {
            for (float t = 0; t < 0.5f; t += Time.deltaTime)
            {
                _successText.color = new Color(1, 1, 1, t / 0.5f);
                await UniTask.Yield();
            }
            _successText.color = new Color(1, 1, 1, 1);

            await UniTask.Delay(TimeSpan.FromSeconds(2));

            for (float t = 0; t < 0.5f; t += Time.deltaTime)
            {
                _successText.color = new Color(1, 1, 1, 1 - (t / 0.5f));
                await UniTask.Yield();
            }
            _successText.color = new Color(1, 1, 1, 0);
        }
    }

    async UniTask StartTitle()
    {
        _titleObject.SetActive(true);

        _BGMPlayer.Play();
        for (float t = 0; t < 2f; t += Time.deltaTime)
        {
            _titleObject.GetComponent<Image>().color = new Color(1, 1, 1, t / 2);
            await UniTask.Yield();
        }
        _titleObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        await _narrationPlayer.PlayTitleAsync();
        await UniTask.Delay(TimeSpan.FromSeconds(1));

        _BGMPlayer.Stop();

        for (float t = 0; t < 2f; t += Time.deltaTime)
        {
            _titleObject.GetComponent<Image>().color = new Color(1, 1, 1, 1 - (t / 2));
            await UniTask.Yield();
        }
        _titleObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        _titleObject.SetActive(false);
    }

    async UniTask StartIntro()
    {
        await _narrationPlayer.PlayIntroAsync();
    }

    public void OnClickDarkModeButton() // Bind Button Click Event.
    {
        _blockSightObject.SetActive(!_blockSightObject.activeSelf);
    }
}
