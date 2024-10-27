using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem; // �V����Input System��API���g�p���邽�߂ɒǉ�

public class PressEnter : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Enter�L�[�������ꂽ�Ƃ�
        if (Input.GetKeyDown(KeyCode.Return))
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
