using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class CTypeEngine
{
    private readonly KeyCode[] keyCodes = Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().ToArray();
    protected int StrPosK = 0;
    protected int KanaPos = 0;
    public string NowInputText = string.Empty;
    public int TextPos = 0, InputPos = 0;
    public bool TypeMissFlag = false;

    string[,] Cw = new string[48, 3];
    protected void SetChangeWord()
    {
        // �L�[�{�[�h�P�s��
        Cw[0, 0] = "Alpha1"; Cw[0, 1] = "1"; Cw[0, 2] = "!";
        Cw[1, 0] = "Alpha2"; Cw[1, 1] = "2"; Cw[1, 2] = "\"";
        Cw[2, 0] = "Alpha3"; Cw[2, 1] = "3"; Cw[2, 2] = "#";
        Cw[3, 0] = "Alpha4"; Cw[3, 1] = "4"; Cw[3, 2] = "$";
        Cw[4, 0] = "Alpha5"; Cw[4, 1] = "5"; Cw[4, 2] = "%";
        Cw[5, 0] = "Alpha6"; Cw[5, 1] = "6"; Cw[5, 2] = "&";
        Cw[6, 0] = "Alpha7"; Cw[6, 1] = "7"; Cw[6, 2] = "'";
        Cw[7, 0] = "Alpha8"; Cw[7, 1] = "8"; Cw[7, 2] = "(";
        Cw[8, 0] = "Alpha9"; Cw[8, 1] = "9"; Cw[8, 2] = ")";
        Cw[9, 0] = "Alpha0"; Cw[9, 1] = "0"; Cw[9, 2] = "�";
        Cw[10, 0] = "Minus"; Cw[10, 1] = "-"; Cw[10, 2] = "=";
        Cw[11, 0] = "Quote"; Cw[11, 1] = "^"; Cw[11, 2] = "~";
        Cw[12, 0] = "Yen"; Cw[12, 1] = "\\"; Cw[12, 2] = "|";

        // �L�[�{�[�h�Q�s��
        Cw[13, 0] = "Q"; Cw[13, 1] = "q"; Cw[13, 2] = "Q";
        Cw[14, 0] = "W"; Cw[14, 1] = "w"; Cw[14, 2] = "W";
        Cw[15, 0] = "E"; Cw[15, 1] = "e"; Cw[15, 2] = "E";
        Cw[16, 0] = "R"; Cw[16, 1] = "r"; Cw[16, 2] = "R";
        Cw[17, 0] = "T"; Cw[17, 1] = "t"; Cw[17, 2] = "T";
        Cw[18, 0] = "Y"; Cw[18, 1] = "y"; Cw[18, 2] = "Y";
        Cw[19, 0] = "U"; Cw[19, 1] = "u"; Cw[19, 2] = "U";
        Cw[20, 0] = "I"; Cw[20, 1] = "i"; Cw[20, 2] = "I";
        Cw[21, 0] = "O"; Cw[21, 1] = "o"; Cw[21, 2] = "O";
        Cw[22, 0] = "P"; Cw[22, 1] = "p"; Cw[22, 2] = "P";
        Cw[23, 0] = "BackQuote"; Cw[23, 1] = "@"; Cw[23, 2] = "`";
        Cw[24, 0] = "LeftBracket"; Cw[24, 1] = "["; Cw[24, 2] = "{";

        // �L�[�{�[�h�R�s��
        Cw[25, 0] = "A"; Cw[25, 1] = "a"; Cw[25, 2] = "A";
        Cw[26, 0] = "S"; Cw[26, 1] = "s"; Cw[26, 2] = "S";
        Cw[27, 0] = "D"; Cw[27, 1] = "d"; Cw[27, 2] = "D";
        Cw[28, 0] = "F"; Cw[28, 1] = "f"; Cw[28, 2] = "F";
        Cw[29, 0] = "G"; Cw[29, 1] = "g"; Cw[29, 2] = "G";
        Cw[30, 0] = "H"; Cw[30, 1] = "h"; Cw[30, 2] = "H";
        Cw[31, 0] = "J"; Cw[31, 1] = "j"; Cw[31, 2] = "J";
        Cw[32, 0] = "K"; Cw[32, 1] = "k"; Cw[32, 2] = "K";
        Cw[33, 0] = "L"; Cw[33, 1] = "l"; Cw[33, 2] = "L";
        Cw[34, 0] = "Colon"; Cw[34, 1] = ";"; Cw[34, 2] = "+";
        Cw[35, 0] = "Semicolon"; Cw[35, 1] = ":"; Cw[35, 2] = "*";
        Cw[36, 0] = "RightBracket"; Cw[36, 1] = "]"; Cw[36, 2] = "}";

        // �L�[�{�[�h�S�s��
        Cw[37, 0] = "Z"; Cw[37, 1] = "z"; Cw[37, 2] = "Z";
        Cw[38, 0] = "X"; Cw[38, 1] = "x"; Cw[38, 2] = "X";
        Cw[39, 0] = "C"; Cw[39, 1] = "c"; Cw[39, 2] = "C";
        Cw[40, 0] = "V"; Cw[40, 1] = "v"; Cw[40, 2] = "V";
        Cw[41, 0] = "B"; Cw[41, 1] = "b"; Cw[41, 2] = "B";
        Cw[42, 0] = "N"; Cw[42, 1] = "n"; Cw[42, 2] = "N";
        Cw[43, 0] = "M"; Cw[43, 1] = "m"; Cw[43, 2] = "M";
        Cw[44, 0] = "Comma"; Cw[44, 1] = ","; Cw[44, 2] = "<";
        Cw[45, 0] = "Period"; Cw[45, 1] = "."; Cw[45, 2] = ">";
        Cw[46, 0] = "Slash"; Cw[46, 1] = "/"; Cw[46, 2] = "?";
        Cw[47, 0] = "BackSlash"; Cw[47, 1] = "\\"; Cw[47, 2] = "_";
    }
    public string GetKeyString(string input_str, bool shift)
    {
        for (int i = 0; i < 48; ++i)
        {
            if (input_str == Cw[i, 0])
            {
                return !shift ? Cw[i, 1] : Cw[i, 2];
            }
        }
        return null;
    }
    public virtual List<string> MakeSearchStr(string input_textk, int str_posk) { return null; }
    public virtual string MakeInputText(string input_textk) { return null; }
    protected virtual bool InputJudge(string[] input_textk, string key, bool shift)
    {
        return false;
    }
    public bool KeyPress(string[] input_textk, Event e, bool shift)
    {
        bool type_end = false;
        foreach (KeyCode key_code in keyCodes)
        {
            if (e.keyCode == key_code)
            {
                if ((e.keyCode == KeyCode.Backslash && e.functionKey) || e.keyCode.ToString() == "226")
                {
                    // ろ
                    type_end = InputJudge(input_textk, "ろ", shift);
                }
                else if (e.keyCode == KeyCode.Backslash)
                {
                    // ー
                    type_end = InputJudge(input_textk,"ー", shift);
                }
                else
                {
                    type_end = InputJudge(input_textk, key_code.ToString(), shift);
                }
                break;
            }
        }
        return type_end;
    }
}
