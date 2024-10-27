using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // �V����Input System��API���g�p���邽�߂ɒǉ�

public class PressEnter : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // �V����Input System���g�p����Enter�L�[�̓��͂��`�F�b�N
        if (Keyboard.current.enterKey.wasPressedThisFrame) 
        {
            LoadMainScene(); // �V�[����J�ڂ��郁�\�b�h���Ăяo��
        }
    }

    private void LoadMainScene()
    {

        // "main"�V�[����ǂݍ���
        SceneManager.LoadScene("Main");
    }
}
