using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem; // 新しいInput SystemのAPIを使用するために追加

public class PressEnter : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Enterキーが押されたとき
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadMainScene(); // シーンを遷移するメソッドを呼び出す
        }
    }

    private void LoadMainScene()
    {
        // "main"シーンを読み込む
        SceneManager.LoadScene("Main");
    }
}
