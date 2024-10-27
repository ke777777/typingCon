using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem; // 新しいInput SystemのAPIを使用するために追加

public class CGameManager : MonoBehaviour
{
    CTypeEngine TypeEngine;

    string[] ViewText = new string[] { "隣の客はよく柿食う客だ","三軒茶屋のマリトッツォ", "東京理科大学", "五箇条の御誓文" };

    string[] InputText = new string[] { "となりのきゃくはよくかきくうきゃくだ","さんげんぢゃやのまりとっつぉ", "とうきょうりかだいがく", "ごかじょうのごせいもん" };

    //string[] ViewText = new string[]{"多摩"};

    //string[] InputText = new string[]{"たま"};

    [SerializeField] private TextMeshProUGUI ViewTextMesh;
    [SerializeField] private TextMeshProUGUI InputTextMesh;
    [SerializeField] private TextMeshProUGUI InputCountText; // 入力回数表示用のTextMeshProUGUI
    [SerializeField] private TextMeshProUGUI IncorrectInputCountText; // 誤った入力回数表示用のTextMeshProUGUI
    [SerializeField] private TextMeshProUGUI AccuracyText; // 正答率表示用のTextMeshProUGUI

    void Start()
    {
        TypeEngine = new CRomaTypeEngine(); // ここをnew CKanaTypeEngine()にするとかな入力になる
        string input_text = TypeEngine.MakeInputText(InputText[0]); // かな表示文章の1行目を入れる
        Debug.Log(input_text);
        foreach (var str in TypeEngine.MakeSearchStr(InputText[0], 0))
        {
            Debug.Log(str);
        }

        ViewTextMesh.text = ViewText[0];
        InputTextMesh.text = input_text;

        // OVRManagerの状態を確認するデバッグログ
        Debug.Log("OVRManager isHmdPresent: " + OVRManager.isHmdPresent);
        // 入力回数を初期化して表示
        UpdateInputCountText();
        // 誤った入力回数を初期化
        UpdateIncorrectInputCountText();
        //正答率初期化
        UpdateAccuracyText();

    }
    bool LeftShift = false, RightShift = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            LeftShift = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            LeftShift = false;
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            RightShift = true;
        }
        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            RightShift = false;
        }
    }
    private void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.type != EventType.KeyUp && e.keyCode != KeyCode.None
            && e.keyCode != KeyCode.LeftShift && e.keyCode != KeyCode.RightShift)
        {
            if (TypeEngine.KeyPress(InputText, e, LeftShift || RightShift))
            {
                ViewTextMesh.text = "終了";
                InputTextMesh.text = "";
                 // 最後の入力カウントを更新する
                UpdateInputCountText();
            }
            else
            {
                // 漢字文字を表示させる
                ViewTextMesh.text = ViewText[TypeEngine.TextPos];
                // すでに打ち終わった文字
                string str1 = "<color=#D0D0D0>" + TypeEngine.NowInputText.Substring(0, TypeEngine.InputPos) + "</color>";
                // これから打つ１文字
                string str2 = string.Empty;
                if (TypeEngine.TypeMissFlag)
                {
                    str2 = "<color=#FF0000>" + TypeEngine.NowInputText.Substring(TypeEngine.InputPos, 1) + "</color>";
                }
                else
                {
                    str2 = "<color=#0000FF>" + TypeEngine.NowInputText.Substring(TypeEngine.InputPos, 1) + "</color>";
                }
                // これから打つ1文字より後ろの最期の文字まで
                string str3 = "<color=#FFFFFF>" + TypeEngine.NowInputText.Substring(TypeEngine.InputPos + 1) + "</color>";
                // 入力文字に反映させる
                InputTextMesh.text = str1 + str2 + str3;
                // 入力回数を更新して表示
                UpdateInputCountText();
                 // 誤った入力回数を更新
                UpdateIncorrectInputCountText();
                UpdateAccuracyText();

            }
        }
    }
    private void UpdateInputCountText()
    {
        var romaTypeEngine = TypeEngine as CRomaTypeEngine;
        if (romaTypeEngine != null) // キャストが成功した場合
        {
            int inputCount = romaTypeEngine.GetKeyPressCount(); // CRomaTypeEngineから入力回数を取得
            InputCountText.text = "入力回数: " + inputCount; // 入力回数をテキストに設定
        }
    }
    private void UpdateIncorrectInputCountText()
    {
        var romaTypeEngine = TypeEngine as CRomaTypeEngine;
        if (romaTypeEngine != null) // キャストが成功した場合
        {
            int incorrectCount = romaTypeEngine.GetIncorrectInputCount(); // CRomaTypeEngineから誤った入力回数を取得
            IncorrectInputCountText.text = "ミスタイプ回数: " + incorrectCount; // 誤った入力回数をテキストに設定
        }
        else
        {
            IncorrectInputCountText.text = "ミスタイプ回数: 0"; // キャストが失敗した場合もデフォルト値を設定
        }
    }
    private void UpdateAccuracyText()
    {
        var romaTypeEngine = TypeEngine as CRomaTypeEngine;
        if (romaTypeEngine != null) // キャストが成功した場合
        {
            int inputCount = romaTypeEngine.GetKeyPressCount();
            int incorrectCount = romaTypeEngine.GetIncorrectInputCount(); 
            if (inputCount > 0) // 0で割らないようにチェック
            {
                float accuracy = ((float)(inputCount - incorrectCount) / inputCount) * 100;
                AccuracyText.text = "正答率: " + accuracy.ToString("F2") + "%"; // 小数点以下2桁で表示
            }
            else
            {
                AccuracyText.text = "正答率: N/A"; // 入力がまだない場合
            }
        }
    }
}