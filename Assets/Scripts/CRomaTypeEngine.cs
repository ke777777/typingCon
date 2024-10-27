using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRomaTypeEngine : CTypeEngine
{
    public struct TagSearchStrings
    {
        public TagSearchStrings(string str, int num)
        {
            SearchStr = str;
            SearchNum = num;
        }
        public string SearchStr;
        public int SearchNum;
    }
    List<TagSearchStrings> SearchStrs = new List<TagSearchStrings>();
    public string StrBuf = string.Empty;
    bool StrChangedFlag = false;
    int EnglishPos = 0;
    int EnglishLen = 0; // SearchStr[i]
    int StrPosE = 0;
    public int keyPressCount = 0; //入力回数
    private int incorrectInputCount = 0; // 誤った入力回数
    #region p
    const int ST_COL = 308;
    const int ST_ROW = 5;
    const int STEX_COL = 244;
    const int STEX_ROW = 7;
    const int STT_COL = 144;
    const int STT_ROW = 8;
    const int STTEX_COL = 234;
    const int STTEX_ROW = 7;
    const int STN_COL = 136;
    const int STN_ROW = 9;
    const int STNEX_COL = 232;
    const int STNEX_ROW = 7;
    const int STNT_COL = 144;
    const int STNT_ROW = 11;
    const int STNTEX_COL = 234;
    const int STNTEX_ROW = 13;
    const int SEARCH_STR_LENGHT = 34;
    string[,] St = new string[ST_COL, ST_ROW];
    string[,] Stex = new string[STEX_COL, STEX_ROW];
    string[,] Stt = new string[STT_COL, STT_ROW];
    string[,] Sttex = new string[STTEX_COL, STTEX_ROW];
    string[,] Stn = new string[STN_COL, STN_ROW];
    string[,] Stnex = new string[STNEX_COL, STNEX_ROW];
    string[,] Stnt = new string[STNT_COL, STNT_ROW];
    string[,] Stntex = new string[STNTEX_COL, STNTEX_ROW];
    #endregion

    // キーボードの押下回数をインクリメントするメソッド
    /*public void IncrementKeyPressCount()
    {
        keyPressCount++;
        Debug.Log("キーが押された回数: " + keyPressCount);
    }
*/

    public CRomaTypeEngine()
    {
        SetSt();
        SetStex();
        SetStt();
        SetSttex();
        SetStn();
        SetStnex();
        SetStnt();
        SetStntex();

        SetJI(1, 1);
        SetHU(1, 1);
        SetTI(1, 1);
        SetSI(1, 1);
        SetTU(1, 1);
        SetN(1);
        SetSYA(1, 1);
        SetTYA(1, 1);
        SetJA(1, 1);

        SetChangeWord();
        keyPressCount = 0;
        incorrectInputCount = 0;
    }

   public void Update()
    {
        // どのキーが押されてもカウントする
        if (Input.anyKeyDown)
        {
            keyPressCount++;
            Debug.Log("キーが押された回数: " + keyPressCount);
        }
    }
        void SetSt()
    {
        for (int i = 0; i < ST_COL; i++)
        {
            for (int k = 0; k < ST_ROW; k++)
            {
                St[i, k] = string.Empty;
            }
        }
        St[0, 0] = "あ"; St[0, 1] = "a";
        St[1, 0] = "い"; St[1, 1] = "i"; St[1, 2] = "yi";
        St[2, 0] = "う"; St[2, 1] = "u"; St[2, 2] = "whu"; St[2, 3] = "wu";
        St[3, 0] = "え"; St[3, 1] = "e";
        St[4, 0] = "お"; St[4, 1] = "o";
        St[5, 0] = "か"; St[5, 1] = "ka"; St[5, 2] = "ca";
        St[6, 0] = "き"; St[6, 1] = "ki";
        St[7, 0] = "く"; St[7, 1] = "ku"; St[7, 2] = "cu"; St[7, 3] = "qu";
        St[8, 0] = "け"; St[8, 1] = "ke";
        St[9, 0] = "こ"; St[9, 1] = "ko"; St[9, 2] = "co";
        St[10, 0] = "さ"; St[10, 1] = "sa";
        St[11, 0] = "し"; St[11, 1] = "si"; St[11, 2] = "shi"; St[11, 3] = "ci";
        St[12, 0] = "す"; St[12, 1] = "su";
        St[13, 0] = "せ"; St[13, 1] = "se"; St[13, 2] = "ce";
        St[14, 0] = "そ"; St[14, 1] = "so";
        St[15, 0] = "た"; St[15, 1] = "ta";
        St[16, 0] = "ち"; St[16, 1] = "ti"; St[16, 2] = "chi";
        St[17, 0] = "つ"; St[17, 1] = "tu"; St[17, 2] = "tsu";
        St[18, 0] = "て"; St[18, 1] = "te";
        St[19, 0] = "と"; St[19, 1] = "to";
        St[20, 0] = "な"; St[20, 1] = "na";
        St[21, 0] = "に"; St[21, 1] = "ni";
        St[22, 0] = "ぬ"; St[22, 1] = "nu";
        St[23, 0] = "ね"; St[23, 1] = "ne";
        St[24, 0] = "の"; St[24, 1] = "no";
        St[25, 0] = "は"; St[25, 1] = "ha";
        St[26, 0] = "ひ"; St[26, 1] = "hi";
        St[27, 0] = "ふ"; St[27, 1] = "hu"; St[27, 2] = "fu";
        St[28, 0] = "へ"; St[28, 1] = "he";
        St[29, 0] = "ほ"; St[29, 1] = "ho";
        St[30, 0] = "ま"; St[30, 1] = "ma";
        St[31, 0] = "み"; St[31, 1] = "mi";
        St[32, 0] = "む"; St[32, 1] = "mu";
        St[33, 0] = "め"; St[33, 1] = "me";
        St[34, 0] = "も"; St[34, 1] = "mo";
        St[35, 0] = "や"; St[35, 1] = "ya";
        St[36, 0] = "ゆ"; St[36, 1] = "yu";
        St[37, 0] = "よ"; St[37, 1] = "yo";
        St[38, 0] = "ら"; St[38, 1] = "ra";
        St[39, 0] = "り"; St[39, 1] = "ri";
        St[40, 0] = "る"; St[40, 1] = "ru";
        St[41, 0] = "れ"; St[41, 1] = "re";
        St[42, 0] = "ろ"; St[42, 1] = "ro";
        St[43, 0] = "わ"; St[43, 1] = "wa";
        St[44, 0] = "を"; St[44, 1] = "wo";
        St[45, 0] = "ん"; St[45, 1] = "nn"; St[45, 2] = "xn";
        St[46, 0] = "ぁ"; St[46, 1] = "la"; St[46, 2] = "xa";
        St[47, 0] = "ぃ"; St[47, 1] = "li"; St[47, 2] = "xi"; St[47, 3] = "lyi"; St[47, 4] = "xyi";
        St[48, 0] = "ぅ"; St[48, 1] = "lu"; St[48, 2] = "xu";
        St[49, 0] = "ぇ"; St[49, 1] = "le"; St[49, 2] = "xe"; St[49, 3] = "lye"; St[49, 4] = "xye";
        St[50, 0] = "ぉ"; St[50, 1] = "lo"; St[50, 2] = "xo";
        St[51, 0] = "っ"; St[51, 1] = "ltu"; St[51, 2] = "xtu"; St[51, 3] = "ltsu";
        St[52, 0] = "ゃ"; St[52, 1] = "lya"; St[52, 2] = "xya";
        St[53, 0] = "ゅ"; St[53, 1] = "lyu"; St[53, 2] = "xyu";
        St[54, 0] = "ょ"; St[54, 1] = "lyo"; St[54, 2] = "xyo";
        St[55, 0] = "ゎ"; St[55, 1] = "lwa"; St[55, 2] = "xwa";
        St[56, 0] = "ヵ"; St[56, 1] = "lka"; St[56, 2] = "xka";
        St[57, 0] = "ヶ"; St[57, 1] = "lke"; St[57, 2] = "xke";
        St[58, 0] = "が"; St[58, 1] = "ga";
        St[59, 0] = "ぎ"; St[59, 1] = "gi";
        St[60, 0] = "ぐ"; St[60, 1] = "gu";
        St[61, 0] = "げ"; St[61, 1] = "ge";
        St[62, 0] = "ご"; St[62, 1] = "go";
        St[63, 0] = "ざ"; St[63, 1] = "za";
        St[64, 0] = "じ"; St[64, 1] = "ji"; St[64, 2] = "zi";
        St[65, 0] = "ず"; St[65, 1] = "zu";
        St[66, 0] = "ぜ"; St[66, 1] = "ze";
        St[67, 0] = "ぞ"; St[67, 1] = "zo";
        St[68, 0] = "だ"; St[68, 1] = "da";
        St[69, 0] = "ぢ"; St[69, 1] = "di";
        St[70, 0] = "づ"; St[70, 1] = "du";
        St[71, 0] = "で"; St[71, 1] = "de";
        St[72, 0] = "ど"; St[72, 1] = "do";
        St[73, 0] = "ば"; St[73, 1] = "ba";
        St[74, 0] = "び"; St[74, 1] = "bi";
        St[75, 0] = "ぶ"; St[75, 1] = "bu";
        St[76, 0] = "べ"; St[76, 1] = "be";
        St[77, 0] = "ぼ"; St[77, 1] = "bo";
        St[78, 0] = "ぱ"; St[78, 1] = "pa";
        St[79, 0] = "ぴ"; St[79, 1] = "pi";
        St[80, 0] = "ぷ"; St[80, 1] = "pu";
        St[81, 0] = "ぺ"; St[81, 1] = "pe";
        St[82, 0] = "ぽ"; St[82, 1] = "po";
        St[83, 0] = "ア"; St[83, 1] = "a";
        St[84, 0] = "イ"; St[84, 1] = "i"; St[84, 2] = "yi";
        St[85, 0] = "ウ"; St[85, 1] = "u"; St[85, 2] = "whu"; St[85, 3] = "wu";
        St[86, 0] = "エ"; St[86, 1] = "e";
        St[87, 0] = "オ"; St[87, 1] = "o";
        St[88, 0] = "カ"; St[88, 1] = "ka"; St[88, 2] = "ca";
        St[89, 0] = "キ"; St[89, 1] = "ki";
        St[90, 0] = "ク"; St[90, 1] = "ku"; St[90, 2] = "cu"; St[90, 3] = "qu";
        St[91, 0] = "ケ"; St[91, 1] = "ke";
        St[92, 0] = "コ"; St[92, 1] = "ko"; St[92, 2] = "co";
        St[93, 0] = "サ"; St[93, 1] = "sa";
        St[94, 0] = "シ"; St[94, 1] = "si"; St[94, 2] = "shi"; St[94, 3] = "ci";
        St[95, 0] = "ス" ;St[95, 1] = "su";
        St[96, 0] = "セ"; St[96, 1] = "se"; St[96, 2] = "ce";
        St[97, 0] = "ソ"; St[97, 1] = "so";
        St[98, 0] = "タ"; St[98, 1] = "ta";
        St[99, 0] = "チ"; St[99, 1] = "ti"; St[99, 2] = "chi";
        St[100, 0] = "ツ"; St[100, 1] = "tu"; St[100, 2] = "tsu";
        St[101, 0] = "テ"; St[101, 1] = "te";
        St[102, 0] = "ト"; St[102, 1] = "to";
        St[103, 0] = "ナ"; St[103, 1] = "na";
        St[104, 0] = "ニ"; St[104, 1] = "ni";
        St[105, 0] = "ヌ"; St[105, 1] = "nu";
        St[106, 0] = "ネ"; St[106, 1] = "ne";
        St[107, 0] = "ノ"; St[107, 1] = "no";
        St[108, 0] = "ハ"; St[108, 1] = "ha";
        St[109, 0] = "ヒ"; St[109, 1] = "hi";
        St[110, 0] = "フ"; St[110, 1] = "hu"; St[110, 2] = "fu";
        St[111, 0] = "ヘ"; St[111, 1] = "he";
        St[112, 0] = "ホ"; St[112, 1] = "ho";
        St[113, 0] = "マ"; St[113, 1] = "ma";
        St[114, 0] = "ミ"; St[114, 1] = "mi";
        St[115, 0] = "ム"; St[115, 1] = "mu";
        St[116, 0] = "メ"; St[116, 1] = "me";
        St[117, 0] = "モ"; St[117, 1] = "mo";
        St[118, 0] = "ヤ"; St[118, 1] = "ya";
        St[119, 0] = "ユ"; St[119, 1] = "yu";
        St[120, 0] = "ヨ"; St[120, 1] = "yo";
        St[121, 0] = "ラ"; St[121, 1] = "ra";
        St[122, 0] = "リ"; St[122, 1] = "ri";
        St[123, 0] = "ル"; St[123, 1] = "ru";
        St[124, 0] = "レ"; St[124, 1] = "re";
        St[125, 0] = "ロ"; St[125, 1] = "ro";
        St[126, 0] = "ワ"; St[126, 1] = "wa";
        St[127, 0] = "ヲ"; St[127, 1] = "wo";
        St[128, 0] = "ン"; St[128, 1] = "nn"; St[128, 2] = "xn";
        St[129, 0] = "ァ"; St[129, 1] = "la"; St[129, 2] = "xa";
        St[130, 0] = "ィ"; St[130, 1] = "li"; St[130, 2] = "xi"; St[130, 3] = "lyi"; St[130, 4] = "xyi";
        St[131, 0] = "ゥ"; St[131, 1] = "lu"; St[131, 2] = "xu";
        St[132, 0] = "ェ"; St[132, 1] = "le"; St[132, 2] = "xe"; St[132, 3] = "lye"; St[132, 4] = "xye";
        St[133, 0] = "ォ"; St[133, 1] = "lo"; St[133, 2] = "xo";
        St[134, 0] = "ッ"; St[134, 1] = "ltu"; St[134, 2] = "xtu"; St[134, 3] = "ltsu";
        St[135, 0] = "ャ"; St[135, 1] = "lya"; St[135, 2] = "xya";
        St[136, 0] = "ュ"; St[136, 1] = "lyu"; St[136, 2] = "xyu";
        St[137, 0] = "ョ"; St[137, 1] = "lyo"; St[137, 2] = "xyo";
        St[138, 0] = "ヮ"; St[138, 1] = "lwa"; St[138, 2] = "xwa";
        St[139, 0] = "ガ"; St[139, 1] = "ga";
        St[140, 0] = "ギ"; St[140, 1] = "gi";
        St[141, 0] = "グ"; St[141, 1] = "gu";
        St[142, 0] = "ゲ"; St[142, 1] = "ge";
        St[143, 0] = "ゴ"; St[143, 1] = "go";
        St[144, 0] = "ザ"; St[144, 1] = "za";
        St[145, 0] = "ジ"; St[145, 1] = "ji"; St[145, 2] = "zi";
        St[146, 0] = "ズ"; St[146, 1] = "zu";
        St[147, 0] = "ゼ"; St[147, 1] = "ze";
        St[148, 0] = "ゾ"; St[148, 1] = "zo";
        St[149, 0] = "ダ"; St[149, 1] = "da";
        St[150, 0] = "ヂ"; St[150, 1] = "di";
        St[151, 0] = "ヅ"; St[151, 1] = "du";
        St[152, 0] = "デ"; St[152, 1] = "de";
        St[153, 0] = "ド"; St[153, 1] = "do";
        St[154, 0] = "バ"; St[154, 1] = "ba";
        St[155, 0] = "ビ"; St[155, 1] = "bi";
        St[156, 0] = "ブ"; St[156, 1] = "bu";
        St[157, 0] = "ベ"; St[157, 1] = "be";
        St[158, 0] = "ボ"; St[158, 1] = "bo";
        St[159, 0] = "パ"; St[159, 1] = "pa";
        St[160, 0] = "ピ"; St[160, 1] = "pi";
        St[161, 0] = "プ"; St[161, 1] = "pu";
        St[162, 0] = "ペ"; St[162, 1] = "pe";
        St[163, 0] = "ポ"; St[163, 1] = "po";
        St[164, 0] = "１"; St[164, 1] = "1";
        St[165, 0] = "２"; St[165, 1] = "2";
        St[166, 0] = "３"; St[166, 1] = "3";
        St[167, 0] = "４"; St[167, 1] = "4";
        St[168, 0] = "５"; St[168, 1] = "5";
        St[169, 0] = "６"; St[169, 1] = "6";
        St[170, 0] = "７"; St[170, 1] = "7";
        St[171, 0] = "８"; St[171, 1] = "8";
        St[172, 0] = "９"; St[172, 1] = "9";
        St[173, 0] = "０"; St[173, 1] = "0";
        St[174, 0] = "ー"; St[174, 1] = "-";
        St[175, 0] = "＾"; St[175, 1] = "^";
        St[176, 0] = "￥"; St[176, 1] = "\\";
        St[177, 0] = "1"; St[177, 1] = "1";
        St[178, 0] = "2"; St[178, 1] = "2";
        St[179, 0] = "3"; St[179, 1] = "3";
        St[180, 0] = "4"; St[180, 1] = "4";
        St[181, 0] = "5"; St[181, 1] = "5";
        St[182, 0] = "6"; St[182, 1] = "6";
        St[183, 0] = "7"; St[183, 1] = "7";
        St[184, 0] = "8"; St[184, 1] = "8";
        St[185, 0] = "9"; St[185, 1] = "9";
        St[186, 0] = "0"; St[186, 1] = "0";
        St[187, 0] = "-"; St[187, 1] = "-";
        St[188, 0] = "^"; St[188, 1] = "^";
        St[189, 0] = "\\"; St[189, 1] = "\\";
        St[190, 0] = "！"; St[190, 1] = "!";
        St[191, 0] = "?h"; St[191, 1] = "\"";
        St[192, 0] = "＃"; St[192, 1] = "#";
        St[193, 0] = "＄"; St[193, 1] = "$";
        St[194, 0] = "％"; St[194, 1] = "%";
        St[195, 0] = "＆"; St[195, 1] = "&";
        St[196, 0] = "’"; St[196, 1] = "'";
        St[197, 0] = "（"; St[197, 1] = "(";
        St[198, 0] = "）"; St[198, 1] = ")";
        St[199, 0] = "＝"; St[199, 1] = "=";
        St[200, 0] = "～"; St[200, 1] = "~";
        St[201, 0] = "｜"; St[201, 1] = "|";
        St[202, 0] = "!"; St[202, 1] = "!";
        St[203, 0] = "\""; St[203, 1] = "\"";
        St[204, 0] = "#"; St[204, 1] = "#";
        St[205, 0] = "$"; St[205, 1] = "$";
        St[206, 0] = "%"; St[206, 1] = "%";
        St[207, 0] = "&"; St[207, 1] = "&";
        St[208, 0] = "'"; St[208, 1] = "'";
        St[209, 0] = "("; St[209, 1] = "(";
        St[210, 0] = ")"; St[210, 1] = ")";
        St[211, 0] = "="; St[211, 1] = "=";
        St[212, 0] = "~"; St[212, 1] = "~";
        St[213, 0] = "|"; St[213, 1] = "|";
        St[214, 0] = "＠"; St[214, 1] = "@";
        St[215, 0] = "「"; St[215, 1] = "[";
        St[216, 0] = "；"; St[216, 1] = ";";
        St[217, 0] = "："; St[217, 1] = ":";
        St[218, 0] = "」"; St[218, 1] = "]";
        St[219, 0] = "?A"; St[219, 1] = "?";
        St[220, 0] = "?B"; St[220, 1] = "?";
        St[221, 0] = "・"; St[221, 1] = "/";
        St[222, 0] = "‘"; St[222, 1] = "`";
        St[223, 0] = "｛"; St[223, 1] = "{";
        St[224, 0] = "＋"; St[224, 1] = "+";
        St[225, 0] = "＊"; St[225, 1] = "*";
        St[226, 0] = "｝"; St[226, 1] = "}";
        St[227, 0] = "＜"; St[227, 1] = "<"; // ?C???@2014/12/08
        St[228, 0] = "＞"; St[228, 1] = ">"; // ?C???@2014/12/08
        St[229, 0] = "？"; St[229, 1] = "?";
        St[230, 0] = "＿"; St[230, 1] = "_";
        St[231, 0] = "@"; St[231, 1] = "@";
        St[232, 0] = "["; St[232, 1] = "[";
        St[233, 0] = ";"; St[233, 1] = ";";
        St[234, 0] = ":"; St[234, 1] = ":";
        St[235, 0] = "]"; St[235, 1] = "]";
        St[236, 0] = ","; St[236, 1] = ",";
        St[237, 0] = "."; St[237, 1] = ".";
        St[238, 0] = "/"; St[238, 1] = "/";
        St[239, 0] = "`"; St[239, 1] = "`";
        St[240, 0] = "{"; St[240, 1] = "{";
        St[241, 0] = "+"; St[241, 1] = "+";
        St[242, 0] = "*"; St[242, 1] = "*";
        St[243, 0] = "}"; St[243, 1] = "}";
        St[244, 0] = "<"; St[244, 1] = "<"; // ?C???@2014/12/08
        St[245, 0] = ">"; St[245, 1] = ">"; // ?C???@2014/12/08
        St[246, 0] = "?"; St[246, 1] = "?";
        St[247, 0] = "_"; St[247, 1] = "_";
        St[248, 0] = "　"; St[248, 1] = " ";
        St[249, 0] = " "; St[249, 1] = " ";
        St[250, 0] = "a"; St[250, 1] = "a";
        St[251, 0] = "b"; St[251, 1] = "b";
        St[252, 0] = "c"; St[252, 1] = "c";
        St[253, 0] = "d"; St[253, 1] = "d";
        St[254, 0] = "e"; St[254, 1] = "e";
        St[255, 0] = "f"; St[255, 1] = "f";
        St[256, 0] = "g"; St[256, 1] = "g";
        St[257, 0] = "h"; St[257, 1] = "h";
        St[258, 0] = "i"; St[258, 1] = "i";
        St[259, 0] = "j"; St[259, 1] = "j";
        St[260, 0] = "k"; St[260, 1] = "k";
        St[261, 0] = "l"; St[261, 1] = "l";
        St[262, 0] = "m"; St[262, 1] = "m";
        St[263, 0] = "n"; St[263, 1] = "n";
        St[264, 0] = "o"; St[264, 1] = "o";
        St[265, 0] = "p"; St[265, 1] = "p";
        St[266, 0] = "q"; St[266, 1] = "q";
        St[267, 0] = "r"; St[267, 1] = "r";
        St[268, 0] = "s"; St[268, 1] = "s";
        St[269, 0] = "t"; St[269, 1] = "t";
        St[270, 0] = "u"; St[270, 1] = "u";
        St[271, 0] = "v"; St[271, 1] = "v";
        St[272, 0] = "w"; St[272, 1] = "w";
        St[273, 0] = "x"; St[273, 1] = "x";
        St[274, 0] = "y"; St[274, 1] = "y";
        St[275, 0] = "z"; St[275, 1] = "z";
        St[276, 0] = "A"; St[276, 1] = "A";
        St[277, 0] = "B"; St[277, 1] = "B";
        St[278, 0] = "C"; St[278, 1] = "C";
        St[279, 0] = "D"; St[279, 1] = "D";
        St[280, 0] = "E"; St[280, 1] = "E";
        St[281, 0] = "F"; St[281, 1] = "F";
        St[282, 0] = "G"; St[282, 1] = "G";
        St[283, 0] = "H"; St[283, 1] = "H";
        St[284, 0] = "I"; St[284, 1] = "I";
        St[285, 0] = "J"; St[285, 1] = "J";
        St[286, 0] = "K"; St[286, 1] = "K";
        St[287, 0] = "L"; St[287, 1] = "L";
        St[288, 0] = "M"; St[288, 1] = "M";
        St[289, 0] = "N"; St[289, 1] = "N";
        St[290, 0] = "O"; St[290, 1] = "O";
        St[291, 0] = "P"; St[291, 1] = "P";
        St[292, 0] = "Q"; St[292, 1] = "Q";
        St[293, 0] = "R"; St[293, 1] = "R";
        St[294, 0] = "S"; St[294, 1] = "S";
        St[295, 0] = "T"; St[295, 1] = "T";
        St[296, 0] = "U"; St[296, 1] = "U";
        St[297, 0] = "V"; St[297, 1] = "V";
        St[298, 0] = "W"; St[298, 1] = "W";
        St[299, 0] = "X"; St[299, 1] = "X";
        St[300, 0] = "Y"; St[300, 1] = "Y";
        St[301, 0] = "Z"; St[301, 1] = "Z";
        St[302, 0] = "ヴ"; St[302, 1] = "vu";

        St[303, 0] = "?"; St[303, 1] = "<";
        St[304, 0] = "?"; St[304, 1] = ">";
        St[305, 0] = "?A"; St[305, 1] = "<";
        St[306, 0] = "?B"; St[306, 1] = ">";
        St[307, 0] = "?C"; St[307, 1] = "<";
    }
    void SetStex()
    {
        for (int i = 0; i < STEX_COL; i++)
        {
            for (int k = 0; k < STEX_ROW; k++)
            {
                Stex[i, k] = string.Empty;
            }
        }
        Stex[0, 0] = "いぇ"; Stex[0, 1] = "ye"; Stex[0, 2] = "ile";
        Stex[1, 0] = "うぁ"; Stex[1, 1] = "wha"; Stex[1, 2] = "ula";
        Stex[2, 0] = "うぃ"; Stex[2, 1] = "wi"; Stex[2, 2] = "whi"; Stex[2, 3] = "uli";
        Stex[3, 0] = "うぇ"; Stex[3, 1] = "we"; Stex[3, 2] = "whe"; Stex[3, 3] = "ule";
        Stex[4, 0] = "うぉ"; Stex[4, 1] = "who"; Stex[4, 2] = "ulo";
        Stex[5, 0] = "きぃ"; Stex[5, 1] = "kyi"; Stex[5, 2] = "kili";
        Stex[6, 0] = "きぇ"; Stex[6, 1] = "kye"; Stex[6, 2] = "kile";
        Stex[7, 0] = "きゃ"; Stex[7, 1] = "kya"; Stex[7, 2] = "kilya";
        Stex[8, 0] = "きゅ"; Stex[8, 1] = "kyu"; Stex[8, 2] = "kilyu";
        Stex[9, 0] = "きょ"; Stex[9, 1] = "kyo"; Stex[9, 2] = "kilyo";
        Stex[10, 0] = "ぎぃ"; Stex[10, 1] = "gyi"; Stex[10, 2] = "gili";
        Stex[11, 0] = "ぎぇ"; Stex[11, 1] = "gye"; Stex[11, 2] = "gile";
        Stex[12, 0] = "ぎゃ"; Stex[12, 1] = "gya"; Stex[12, 2] = "gilya";
        Stex[13, 0] = "ぎゅ"; Stex[13, 1] = "gyu"; Stex[13, 2] = "gilyu";
        Stex[14, 0] = "ぎょ"; Stex[14, 1] = "gyo"; Stex[14, 2] = "gilyo";
        Stex[15, 0] = "くぁ"; Stex[15, 1] = "qa"; Stex[15, 2] = "qwa"; Stex[15, 3] = "kwa"; Stex[15, 4] = "kula"; Stex[15, 5] = "cula";
        Stex[16, 0] = "くぃ"; Stex[16, 1] = "qi"; Stex[16, 2] = "qwi"; Stex[16, 3] = "qyi"; Stex[16, 4] = "kuli"; Stex[16, 5] = "culi";
        Stex[17, 0] = "くぅ"; Stex[17, 1] = "qwu"; Stex[17, 2] = "kulu"; Stex[17, 3] = "culu";
        Stex[18, 0] = "くぇ"; Stex[18, 1] = "qe"; Stex[18, 2] = "qwe"; Stex[18, 3] = "qye"; Stex[18, 4] = "kule"; Stex[18, 5] = "cule";
        Stex[19, 0] = "くぉ"; Stex[19, 1] = "qo"; Stex[19, 2] = "qwo"; Stex[19, 3] = "kulo"; Stex[19, 4] = "culo";
        Stex[20, 0] = "くゃ"; Stex[20, 1] = "qya"; Stex[20, 2] = "kulya"; Stex[20, 3] = "culya";
        Stex[21, 0] = "くゅ"; Stex[21, 1] = "qyu"; Stex[21, 2] = "kulyu"; Stex[21, 3] = "culyu";
        Stex[22, 0] = "くょ"; Stex[22, 1] = "qyo"; Stex[22, 2] = "kulyo"; Stex[22, 3] = "culyo";
        Stex[23, 0] = "ぐぁ"; Stex[23, 1] = "gwa"; Stex[23, 2] = "gula";
        Stex[24, 0] = "ぐぃ"; Stex[24, 1] = "gwi"; Stex[24, 2] = "guli";
        Stex[25, 0] = "ぐぅ"; Stex[25, 1] = "gwu"; Stex[25, 2] = "gulu";
        Stex[26, 0] = "ぐぇ"; Stex[26, 1] = "gwe"; Stex[26, 2] = "gule";
        Stex[27, 0] = "ぐぉ"; Stex[27, 1] = "gwo"; Stex[27, 2] = "gulo";
        Stex[28, 0] = "しぃ"; Stex[28, 1] = "syi"; Stex[28, 2] = "sili"; Stex[28, 3] = "shili";
        Stex[29, 0] = "しぇ"; Stex[29, 1] = "sye"; Stex[29, 2] = "she"; Stex[29, 3] = "sile"; Stex[29, 4] = "shile";
        Stex[30, 0] = "しゃ"; Stex[30, 1] = "sya"; Stex[30, 2] = "sha"; Stex[30, 3] = "silya"; Stex[30, 4] = "shilya"; Stex[30, 5] = "sixya"; Stex[30, 6] = "shixya";
        Stex[31, 0] = "しゅ"; Stex[31, 1] = "syu"; Stex[31, 2] = "shu"; Stex[31, 3] = "silyu"; Stex[31, 4] = "shilyu"; Stex[31, 5] = "sixyu"; Stex[31, 6] = "shixyu";
        Stex[32, 0] = "しょ"; Stex[32, 1] = "syo"; Stex[32, 2] = "sho"; Stex[32, 3] = "silyo"; Stex[32, 4] = "shilyo"; Stex[32, 5] = "sixyo"; Stex[32, 6] = "shixyo";
        Stex[33, 0] = "じぃ"; Stex[33, 1] = "jyi"; Stex[33, 2] = "zyi"; Stex[33, 3] = "jili"; Stex[33, 4] = "zili";
        Stex[34, 0] = "じぇ"; Stex[34, 1] = "je"; Stex[34, 2] = "jye"; Stex[34, 3] = "zye"; Stex[34, 4] = "jile"; Stex[34, 5] = "zile";
        Stex[35, 0] = "じゃ"; Stex[35, 1] = "ja"; Stex[35, 2] = "jya"; Stex[35, 3] = "zya"; Stex[35, 4] = "jilya"; Stex[35, 5] = "zilya";
        Stex[36, 0] = "じゅ"; Stex[36, 1] = "ju"; Stex[36, 2] = "jyu"; Stex[36, 3] = "zyu"; Stex[36, 4] = "jilyu"; Stex[36, 5] = "zilyu";
        Stex[37, 0] = "じょ"; Stex[37, 1] = "jo"; Stex[37, 2] = "jyo"; Stex[37, 3] = "zyo"; Stex[37, 4] = "jilyo"; Stex[37, 5] = "zilyo";
        Stex[38, 0] = "すぁ"; Stex[38, 1] = "swa"; Stex[38, 2] = "sula";
        Stex[39, 0] = "すぃ"; Stex[39, 1] = "swi"; Stex[39, 2] = "suli";
        Stex[40, 0] = "すぅ"; Stex[40, 1] = "swu"; Stex[40, 2] = "sulu"; Stex[40, 3] = "suxu";
        Stex[41, 0] = "すぇ"; Stex[41, 1] = "swe"; Stex[41, 2] = "sule";
        Stex[42, 0] = "すぉ"; Stex[42, 1] = "swo"; Stex[42, 2] = "sulo";
        Stex[43, 0] = "ちぃ"; Stex[43, 1] = "tyi"; Stex[43, 2] = "cyi"; Stex[43, 3] = "tili"; Stex[43, 4] = "chili";
        Stex[44, 0] = "ちぇ"; Stex[44, 1] = "tye"; Stex[44, 2] = "cye"; Stex[44, 3] = "che"; Stex[44, 4] = "tile"; Stex[44, 5] = "chile";
        Stex[45, 0] = "ちゃ"; Stex[45, 1] = "tya"; Stex[45, 2] = "cya"; Stex[45, 3] = "cha"; Stex[45, 4] = "tilya"; Stex[45, 5] = "chilya";
        Stex[46, 0] = "ちゅ"; Stex[46, 1] = "tyu"; Stex[46, 2] = "cyu"; Stex[46, 3] = "chu"; Stex[46, 4] = "tilyu"; Stex[46, 5] = "chilyu";
        Stex[47, 0] = "ちょ"; Stex[47, 1] = "tyo"; Stex[47, 2] = "cyo"; Stex[47, 3] = "cho"; Stex[47, 4] = "tilyo"; Stex[47, 5] = "chilyo";
        Stex[48, 0] = "ぢぃ"; Stex[48, 1] = "dyi"; Stex[48, 2] = "dili";
        Stex[49, 0] = "ぢぇ"; Stex[49, 1] = "dye"; Stex[49, 2] = "dile";
        Stex[50, 0] = "ぢゃ"; Stex[50, 1] = "dya"; Stex[50, 2] = "dilya";
        Stex[51, 0] = "ぢゅ"; Stex[51, 1] = "dyu"; Stex[51, 2] = "dilyu";
        Stex[52, 0] = "ぢょ"; Stex[52, 1] = "dyo"; Stex[52, 2] = "dilyo";
        Stex[53, 0] = "つぁ"; Stex[53, 1] = "tsa"; Stex[53, 2] = "tula";
        Stex[54, 0] = "つぃ"; Stex[54, 1] = "tsi"; Stex[54, 2] = "tuli";
        Stex[55, 0] = "つぇ"; Stex[55, 1] = "tse"; Stex[55, 2] = "tule";
        Stex[56, 0] = "つぉ"; Stex[56, 1] = "tso"; Stex[56, 2] = "tulo";
        Stex[57, 0] = "てぃ"; Stex[57, 1] = "thi"; Stex[57, 2] = "teli";
        Stex[58, 0] = "てぇ"; Stex[58, 1] = "the"; Stex[58, 2] = "tele";
        Stex[59, 0] = "てゃ"; Stex[59, 1] = "tha"; Stex[59, 2] = "telya";
        Stex[60, 0] = "てゅ"; Stex[60, 1] = "thu"; Stex[60, 2] = "telyu";
        Stex[61, 0] = "てょ"; Stex[61, 1] = "tho"; Stex[61, 2] = "telyo";
        Stex[62, 0] = "でぃ"; Stex[62, 1] = "dhi"; Stex[62, 2] = "deli";
        Stex[63, 0] = "でぇ"; Stex[63, 1] = "dhe"; Stex[63, 2] = "dele";
        Stex[64, 0] = "でゃ"; Stex[64, 1] = "dha"; Stex[64, 2] = "delya";
        Stex[65, 0] = "でゅ"; Stex[65, 1] = "dhu"; Stex[65, 2] = "delyu";
        Stex[66, 0] = "でょ"; Stex[66, 1] = "dho"; Stex[66, 2] = "delyo";
        Stex[67, 0] = "とぁ"; Stex[67, 1] = "twa"; Stex[67, 2] = "tola";
        Stex[68, 0] = "とぃ"; Stex[68, 1] = "twi"; Stex[68, 2] = "toli";
        Stex[69, 0] = "とぅ"; Stex[69, 1] = "twu"; Stex[69, 2] = "tolu"; Stex[69, 3] = "toxu";
        Stex[70, 0] = "とぇ"; Stex[70, 1] = "twe"; Stex[70, 2] = "tole";
        Stex[71, 0] = "とぉ"; Stex[71, 1] = "two"; Stex[71, 2] = "tolo";
        Stex[72, 0] = "どぁ"; Stex[72, 1] = "dwa"; Stex[72, 2] = "dola";
        Stex[73, 0] = "どぃ"; Stex[73, 1] = "dwi"; Stex[73, 2] = "doli";
        Stex[74, 0] = "どぅ"; Stex[74, 1] = "dwu"; Stex[74, 2] = "dolu";
        Stex[75, 0] = "どぇ"; Stex[75, 1] = "dwe"; Stex[75, 2] = "dole";
        Stex[76, 0] = "どぉ"; Stex[76, 1] = "dwo"; Stex[76, 2] = "dolo";
        Stex[77, 0] = "にぃ"; Stex[77, 1] = "nyi"; Stex[77, 2] = "nili";
        Stex[78, 0] = "にぇ"; Stex[78, 1] = "nye"; Stex[78, 2] = "nile";
        Stex[79, 0] = "にゃ"; Stex[79, 1] = "nya"; Stex[79, 2] = "nilya";
        Stex[80, 0] = "にゅ"; Stex[80, 1] = "nyu"; Stex[80, 2] = "nilyu";
        Stex[81, 0] = "にょ"; Stex[81, 1] = "nyo"; Stex[81, 2] = "nilyo";
        Stex[82, 0] = "ひぃ"; Stex[82, 1] = "hyi"; Stex[82, 2] = "hili";
        Stex[83, 0] = "ひぇ"; Stex[83, 1] = "hye"; Stex[83, 2] = "hile";
        Stex[84, 0] = "ひゃ"; Stex[84, 1] = "hya"; Stex[84, 2] = "hilya";
        Stex[85, 0] = "ひゅ"; Stex[85, 1] = "hyu"; Stex[85, 2] = "hilyu";
        Stex[86, 0] = "ひょ"; Stex[86, 1] = "hyo"; Stex[86, 2] = "hilyo";
        Stex[87, 0] = "びぃ"; Stex[87, 1] = "byi"; Stex[87, 2] = "bili";
        Stex[88, 0] = "びぇ"; Stex[88, 1] = "bye"; Stex[88, 2] = "bile";
        Stex[89, 0] = "びゃ"; Stex[89, 1] = "bya"; Stex[89, 2] = "bilya";
        Stex[90, 0] = "びゅ"; Stex[90, 1] = "byu"; Stex[90, 2] = "bilyu";
        Stex[91, 0] = "びょ"; Stex[91, 1] = "byo"; Stex[91, 2] = "bilyo";
        Stex[92, 0] = "ぴぃ"; Stex[92, 1] = "pyi"; Stex[92, 2] = "pili";
        Stex[93, 0] = "ぴぇ"; Stex[93, 1] = "pye"; Stex[93, 2] = "pile";
        Stex[94, 0] = "ぴゃ"; Stex[94, 1] = "pya"; Stex[94, 2] = "pilya";
        Stex[95, 0] = "ぴゅ"; Stex[95, 1] = "pyu"; Stex[95, 2] = "pilyu";
        Stex[96, 0] = "ぴょ"; Stex[96, 1] = "pyo"; Stex[96, 2] = "pilyo";
        Stex[97, 0] = "ふぁ"; Stex[97, 1] = "fa"; Stex[97, 2] = "fwa"; Stex[97, 3] = "hula"; Stex[97, 4] = "fula";
        Stex[98, 0] = "ふぃ"; Stex[98, 1] = "fi"; Stex[98, 2] = "fyi"; Stex[98, 3] = "fwi"; Stex[98, 4] = "huli"; Stex[98, 5] = "fuli";
        Stex[99, 0] = "ふぅ"; Stex[99, 1] = "fwu"; Stex[99, 2] = "hulu"; Stex[99, 3] = "fulu";
        Stex[100, 0] = "ふぇ"; Stex[100, 1] = "fe"; Stex[100, 2] = "fye"; Stex[100, 3] = "fwe"; Stex[100, 4] = "hule"; Stex[100, 5] = "fule";
        Stex[101, 0] = "ふぉ"; Stex[101, 1] = "fo"; Stex[101, 2] = "fwo"; Stex[101, 3] = "hulo"; Stex[101, 4] = "fulo";
        Stex[102, 0] = "ふゃ"; Stex[102, 1] = "fya"; Stex[102, 2] = "hulya"; Stex[102, 3] = "fulya";
        Stex[103, 0] = "ふゅ"; Stex[103, 1] = "fyu"; Stex[103, 2] = "hulyu"; Stex[103, 3] = "fulyu";
        Stex[104, 0] = "ふょ"; Stex[104, 1] = "fyo"; Stex[104, 2] = "hulyo"; Stex[104, 3] = "fulyo";
        Stex[105, 0] = "みぃ"; Stex[105, 1] = "myi"; Stex[105, 2] = "mili";
        Stex[106, 0] = "みぇ"; Stex[106, 1] = "mye"; Stex[106, 2] = "mile";
        Stex[107, 0] = "みゃ"; Stex[107, 1] = "mya"; Stex[107, 2] = "milya";
        Stex[108, 0] = "みゅ"; Stex[108, 1] = "myu"; Stex[108, 2] = "milyu";
        Stex[109, 0] = "みょ"; Stex[109, 1] = "myo"; Stex[109, 2] = "milyo";
        Stex[110, 0] = "りぇ"; Stex[110, 1] = "ryi"; Stex[110, 2] = "rili";
        Stex[111, 0] = "りぇ"; Stex[111, 1] = "rye"; Stex[111, 2] = "rile";
        Stex[112, 0] = "りゃ"; Stex[112, 1] = "rya"; Stex[112, 2] = "rilya";
        Stex[113, 0] = "りゅ"; Stex[113, 1] = "ryu"; Stex[113, 2] = "rilyu";
        Stex[114, 0] = "りょ"; Stex[114, 1] = "ryo"; Stex[114, 2] = "rilyo";
        Stex[115, 0] = "ヴぁ"; Stex[115, 1] = "va"; Stex[115, 2] = "vula";
        Stex[116, 0] = "ヴぃ"; Stex[116, 1] = "vi"; Stex[116, 2] = "vyi"; Stex[116, 3] = "vuli";
        Stex[117, 0] = "ヴぇ"; Stex[117, 1] = "ve"; Stex[117, 2] = "vye"; Stex[117, 3] = "vule";
        Stex[118, 0] = "ヴぉ"; Stex[118, 1] = "vo"; Stex[118, 2] = "vulo";
        Stex[119, 0] = "ヴゃ"; Stex[119, 1] = "vya"; Stex[119, 2] = "vulya";
        Stex[120, 0] = "ヴゅ"; Stex[120, 1] = "vyu"; Stex[120, 2] = "vulyu";
        Stex[121, 0] = "ヴょ"; Stex[121, 1] = "vyo"; Stex[121, 2] = "vulyo";
        Stex[122, 0] = "?C?F";
        Stex[123, 0] = "?E?@";
        Stex[124, 0] = "?E?B";
        Stex[125, 0] = "?E?F";
        Stex[126, 0] = "?E?H";
        Stex[127, 0] = "?L?B";
        Stex[128, 0] = "?L?F";
        Stex[129, 0] = "?L??";
        Stex[130, 0] = "?L??";
        Stex[131, 0] = "?L??";
        Stex[132, 0] = "?M?B";
        Stex[133, 0] = "?M?F";
        Stex[134, 0] = "?M??";
        Stex[135, 0] = "?M??";
        Stex[136, 0] = "?M??";
        Stex[137, 0] = "?N?@";
        Stex[138, 0] = "?N?B";
        Stex[139, 0] = "?N?D";
        Stex[140, 0] = "?N?F";
        Stex[141, 0] = "?N?H";
        Stex[142, 0] = "?N??";
        Stex[143, 0] = "?N??";
        Stex[144, 0] = "?N??";
        Stex[145, 0] = "?O?@";
        Stex[146, 0] = "?O?B";
        Stex[147, 0] = "?O?D";
        Stex[148, 0] = "?O?F";
        Stex[149, 0] = "?O?H";
        Stex[150, 0] = "?V?B";
        Stex[151, 0] = "?V?F";
        Stex[152, 0] = "?V??";
        Stex[153, 0] = "?V??";
        Stex[154, 0] = "?V??";
        Stex[155, 0] = "?W?B";
        Stex[156, 0] = "?W?F";
        Stex[157, 0] = "?W??";
        Stex[158, 0] = "?W??";
        Stex[159, 0] = "?W??";
        Stex[160, 0] = "?X?@";
        Stex[161, 0] = "?X?B";
        Stex[162, 0] = "?X?D";
        Stex[163, 0] = "?X?F";
        Stex[164, 0] = "?X?H";
        Stex[165, 0] = "?`?B";
        Stex[166, 0] = "?`?F";
        Stex[167, 0] = "?`??";
        Stex[168, 0] = "?`??";
        Stex[169, 0] = "?`??";
        Stex[170, 0] = "?a?B";
        Stex[171, 0] = "?a?F";
        Stex[172, 0] = "?a??";
        Stex[173, 0] = "?a??";
        Stex[174, 0] = "?a??";
        Stex[175, 0] = "?c?@";
        Stex[176, 0] = "?c?B";
        Stex[177, 0] = "?c?F";
        Stex[178, 0] = "?c?H";
        Stex[179, 0] = "?e?B";
        Stex[180, 0] = "?e?F";
        Stex[181, 0] = "?e??";
        Stex[182, 0] = "?e??";
        Stex[183, 0] = "?e??";
        Stex[184, 0] = "?f?B";
        Stex[185, 0] = "?f?F";
        Stex[186, 0] = "?f??";
        Stex[187, 0] = "?f??";
        Stex[188, 0] = "?f??";
        Stex[189, 0] = "?g?@";
        Stex[190, 0] = "?g?B";
        Stex[191, 0] = "?g?D";
        Stex[192, 0] = "?g?F";
        Stex[193, 0] = "?g?H";
        Stex[194, 0] = "?h?@";
        Stex[195, 0] = "?h?B";
        Stex[196, 0] = "?h?H";
        Stex[197, 0] = "?h?F";
        Stex[198, 0] = "?h?H";
        Stex[199, 0] = "?j?B";
        Stex[200, 0] = "?j?F";
        Stex[201, 0] = "?j??";
        Stex[202, 0] = "?j??";
        Stex[203, 0] = "?j??";
        Stex[204, 0] = "?q?B";
        Stex[205, 0] = "?q?F";
        Stex[206, 0] = "?q??";
        Stex[207, 0] = "?q??";
        Stex[208, 0] = "?q??";
        Stex[209, 0] = "?r?B";
        Stex[210, 0] = "?r?F";
        Stex[211, 0] = "?r??";
        Stex[212, 0] = "?r??";
        Stex[213, 0] = "?r??";
        Stex[214, 0] = "?s?B";
        Stex[215, 0] = "?s?F";
        Stex[216, 0] = "?s??";
        Stex[217, 0] = "?s??";
        Stex[218, 0] = "?s??";
        Stex[219, 0] = "?t?@";
        Stex[220, 0] = "?t?B";
        Stex[221, 0] = "?t?D";
        Stex[222, 0] = "?t?F";
        Stex[223, 0] = "?t?H";
        Stex[224, 0] = "?t??";
        Stex[225, 0] = "?t??";
        Stex[226, 0] = "?t??";
        Stex[227, 0] = "?~?B";
        Stex[228, 0] = "?~?F";
        Stex[229, 0] = "?~??";
        Stex[230, 0] = "?~??";
        Stex[231, 0] = "?~??";
        Stex[232, 0] = "???B";
        Stex[233, 0] = "???F";
        Stex[234, 0] = "????";
        Stex[235, 0] = "????";
        Stex[236, 0] = "????";
        Stex[237, 0] = "???@";
        Stex[238, 0] = "???B";
        Stex[239, 0] = "???F";
        Stex[240, 0] = "???H";
        Stex[241, 0] = "????";
        Stex[242, 0] = "????";
        Stex[243, 0] = "????";
        for (int i = STEX_COL / 2; i < STEX_COL; i++)
        {
            for (int k = 1; k < STEX_ROW; k++)
            {
                Stex[i, k] = Stex[i - (STEX_COL / 2), k];
            }
        }
    }


    void SetStt()
    {
        for (int i = 0; i < STT_COL; i++)
        {
            for (int k = 0; k < STT_ROW; k++)
            {
                Stt[i, k] = string.Empty;
            }
        }
        Stt[0, 0] = "っか"; Stt[0, 1] = "kka"; Stt[0, 2] = "cca"; Stt[0, 3] = "ltuka"; Stt[0, 4] = "ltuca"; Stt[0, 5] = "xtuka"; Stt[0, 6] = "xtuca";
        Stt[1, 0] = "っき"; Stt[1, 1] = "kki"; Stt[1, 2] = "ltuki"; Stt[1, 3] = "xtuki";
        Stt[2, 0] = "っく"; Stt[2, 1] = "kku"; Stt[2, 2] = "ccu"; Stt[2, 3] = "qqu"; Stt[2, 4] = "ltuku"; Stt[2, 5] = "ltucu"; Stt[2, 6] = "xtuku"; Stt[2, 7] = "xtucu";
        Stt[3, 0] = "っけ"; Stt[3, 1] = "kke"; Stt[3, 2] = "ltuke"; Stt[3, 3] = "xtuke";
        Stt[4, 0] = "っこ"; Stt[4, 1] = "kko"; Stt[4, 2] = "cco"; Stt[4, 3] = "ltuko"; Stt[4, 4] = "ltuco"; Stt[4, 5] = "xtuko"; Stt[4, 6] = "xtuco";
        Stt[5, 0] = "っさ"; Stt[5, 1] = "ssa"; Stt[5, 2] = "ltusa"; Stt[5, 3] = "xtusa";
        Stt[6, 0] = "っし"; Stt[6, 1] = "ssi"; Stt[6, 2] = "sshi"; Stt[6, 3] = "cci"; Stt[6, 4] = "ltusi"; Stt[6, 5] = "ltushi"; Stt[6, 6] = "xtusi"; Stt[6, 7] = "xtushi";
        Stt[7, 0] = "っす"; Stt[7, 1] = "ssu"; Stt[7, 2] = "ltusu"; Stt[7, 3] = "xtusu";
        Stt[8, 0] = "っせ"; Stt[8, 1] = "sse"; Stt[8, 2] = "cce"; Stt[8, 3] = "ltuse"; Stt[8, 4] = "ltuce"; Stt[8, 5] = "xtuse"; Stt[8, 6] = "xtuce";
        Stt[9, 0] = "っそ"; Stt[9, 1] = "sso"; Stt[9, 2] = "ltuso"; Stt[9, 3] = "xtuso";
        Stt[10, 0] = "った"; Stt[10, 1] = "tta"; Stt[10, 2] = "ltuta"; Stt[10, 3] = "xtuta";
        Stt[11, 0] = "っち"; Stt[11, 1] = "tti"; Stt[11, 2] = "cchi"; Stt[11, 3] = "ltuti"; Stt[11, 4] = "ltuchi"; Stt[11, 5] = "xtuti"; Stt[11, 6] = "xtuchi";
        Stt[12, 0] = "っつ"; Stt[12, 1] = "ttu"; Stt[12, 2] = "ttsu"; Stt[12, 3] = "ltutu"; Stt[12, 4] = "ltutsu";
        Stt[13, 0] = "って"; Stt[13, 1] = "tte"; Stt[13, 2] = "ltute"; Stt[13, 3] = "xtute";
        Stt[14, 0] = "っと"; Stt[14, 1] = "tto"; Stt[14, 2] = "ltuto"; Stt[14, 3] = "xtuto";
        Stt[15, 0] = "っは"; Stt[15, 1] = "hha"; Stt[15, 2] = "ltuha"; Stt[15, 3] = "xtuha";
        Stt[16, 0] = "っひ"; Stt[16, 1] = "hhi"; Stt[16, 2] = "ltuhi"; Stt[16, 3] = "xtuhi";
        Stt[17, 0] = "っふ"; Stt[17, 1] = "hhu"; Stt[17, 2] = "ffu"; Stt[17, 3] = "ltuhu"; Stt[17, 4] = "ltufu"; Stt[17, 5] = "xtuhu"; Stt[17, 6] = "xtufu";
        Stt[18, 0] = "っへ"; Stt[18, 1] = "hhe"; Stt[18, 2] = "ltuhe"; Stt[18, 3] = "xtuhe";
        Stt[19, 0] = "っほ"; Stt[19, 1] = "hho"; Stt[19, 2] = "ltuho"; Stt[19, 3] = "xtuho";
        Stt[20, 0] = "っま"; Stt[20, 1] = "mma"; Stt[20, 2] = "ltuma"; Stt[20, 3] = "xtuma";
        Stt[21, 0] = "っみ"; Stt[21, 1] = "mmi"; Stt[21, 2] = "ltumi"; Stt[21, 3] = "xtumi";
        Stt[22, 0] = "っむ"; Stt[22, 1] = "mmu"; Stt[22, 2] = "ltumu"; Stt[22, 3] = "xtumu";
        Stt[23, 0] = "っめ"; Stt[23, 1] = "mme"; Stt[23, 2] = "ltume"; Stt[23, 3] = "xtume";
        Stt[24, 0] = "っも"; Stt[24, 1] = "mmo"; Stt[24, 2] = "ltumo"; Stt[24, 3] = "xtumo";
        Stt[25, 0] = "っや"; Stt[25, 1] = "yya"; Stt[25, 2] = "ltuya"; Stt[25, 3] = "xtuya";
        Stt[26, 0] = "っゆ"; Stt[26, 1] = "yyu"; Stt[26, 2] = "ltuyu"; Stt[26, 3] = "xtuyu";
        Stt[27, 0] = "っよ"; Stt[27, 1] = "yyo"; Stt[27, 2] = "ltuyo"; Stt[27, 3] = "xtuyo";
        Stt[28, 0] = "っら"; Stt[28, 1] = "rra"; Stt[28, 2] = "ltura"; Stt[28, 3] = "xtura";
        Stt[29, 0] = "っり"; Stt[29, 1] = "rri"; Stt[29, 2] = "lturi"; Stt[29, 3] = "xturi";
        Stt[30, 0] = "っる"; Stt[30, 1] = "rru"; Stt[30, 2] = "lturu"; Stt[30, 3] = "xturu";
        Stt[31, 0] = "っれ"; Stt[31, 1] = "rre"; Stt[31, 2] = "lture"; Stt[31, 3] = "xture";
        Stt[32, 0] = "っろ"; Stt[32, 1] = "rro"; Stt[32, 2] = "lturo"; Stt[32, 3] = "xturo";
        Stt[33, 0] = "っわ"; Stt[33, 1] = "wwa"; Stt[33, 2] = "ltuwa"; Stt[33, 3] = "xtuwa";
        Stt[34, 0] = "っを"; Stt[34, 1] = "wwo"; Stt[34, 2] = "ltuwo"; Stt[34, 3] = "xtuwo";
        Stt[35, 0] = "っぁ"; Stt[35, 1] = "lla"; Stt[35, 2] = "xxa"; Stt[35, 3] = "ltula"; Stt[35, 4] = "xtula";
        Stt[36, 0] = "っぃ"; Stt[36, 1] = "lli"; Stt[36, 2] = "xxi"; Stt[36, 3] = "llyi"; Stt[36, 4] = "xxyi"; Stt[36, 5] = "ltuli"; Stt[36, 6] = "xtuli";
        Stt[37, 0] = "っぅ"; Stt[37, 1] = "llu"; Stt[37, 2] = "xxu"; Stt[37, 3] = "ltulu"; Stt[37, 4] = "xtulu";
        Stt[38, 0] = "っぇ"; Stt[38, 1] = "lle"; Stt[38, 2] = "xxe"; Stt[38, 3] = "llye"; Stt[38, 4] = "xxye"; Stt[38, 5] = "ltule"; Stt[38, 6] = "xtule";
        Stt[39, 0] = "っぉ"; Stt[39, 1] = "llo"; Stt[39, 2] = "xxo"; Stt[39, 3] = "ltulo"; Stt[39, 4] = "xtulo";
        Stt[40, 0] = "っっ"; Stt[40, 1] = "lltu"; Stt[40, 2] = "xxtu"; Stt[40, 3] = "lltsu"; Stt[40, 4] = "ltultu"; Stt[40, 5] = "xtuxtu"; Stt[40, 6] = "ltuxtu"; Stt[40, 7] = "xtultu";
        Stt[41, 0] = "っゃ"; Stt[41, 1] = "llya"; Stt[41, 2] = "xxya"; Stt[41, 3] = "ltulya"; Stt[41, 4] = "xtulya";
        Stt[42, 0] = "っゅ"; Stt[42, 1] = "llyu"; Stt[42, 2] = "xxyu"; Stt[42, 3] = "ltulyu"; Stt[42, 4] = "xtulyu";
        Stt[43, 0] = "っょ"; Stt[43, 1] = "llyo"; Stt[43, 2] = "xxyo"; Stt[43, 3] = "ltulyo"; Stt[43, 4] = "xtulyo";
        Stt[44, 0] = "っゎ"; Stt[44, 1] = "llwa"; Stt[44, 2] = "xxwa"; Stt[44, 3] = "ltulwa"; Stt[44, 4] = "xtulwa";
        Stt[45, 0] = "っヵ"; Stt[45, 1] = "llka"; Stt[45, 2] = "xxka"; Stt[45, 3] = "ltulka"; Stt[45, 4] = "xtulka";
        Stt[46, 0] = "っヶ"; Stt[46, 1] = "llke"; Stt[46, 2] = "xxke"; Stt[46, 3] = "ltulke"; Stt[46, 4] = "xtulke";
        Stt[47, 0] = "っが"; Stt[47, 1] = "gga"; Stt[47, 2] = "ltuga"; Stt[47, 3] = "xtuga";
        Stt[48, 0] = "っぎ"; Stt[48, 1] = "ggi"; Stt[48, 2] = "ltugi"; Stt[48, 3] = "xtugi";
        Stt[49, 0] = "っぐ"; Stt[49, 1] = "ggu"; Stt[49, 2] = "ltugu"; Stt[49, 3] = "xtugu";
        Stt[50, 0] = "っげ"; Stt[50, 1] = "gge"; Stt[50, 2] = "ltuge"; Stt[50, 3] = "xtuge";
        Stt[51, 0] = "っご"; Stt[51, 1] = "ggo"; Stt[51, 2] = "ltugo"; Stt[51, 3] = "xtugo";
        Stt[52, 0] = "っざ"; Stt[52, 1] = "zza"; Stt[52, 2] = "ltuza"; Stt[52, 3] = "xtuza";
        Stt[53, 0] = "っじ"; Stt[53, 1] = "jji"; Stt[53, 2] = "zzi"; Stt[53, 3] = "ltuji"; Stt[53, 4] = "ltuzi"; Stt[53, 5] = "xtuji"; Stt[53, 6] = "xtuzi";
        Stt[54, 0] = "っず"; Stt[54, 1] = "zzu"; Stt[54, 2] = "ltuzu"; Stt[54, 3] = "xtuzu";
        Stt[55, 0] = "っぜ"; Stt[55, 1] = "zze"; Stt[55, 2] = "ltuze"; Stt[55, 3] = "xtuze";
        Stt[56, 0] = "っぞ"; Stt[56, 1] = "zzo"; Stt[56, 2] = "ltuzo"; Stt[56, 3] = "xtuzo";
        Stt[57, 0] = "っだ"; Stt[57, 1] = "dda"; Stt[57, 2] = "ltuda"; Stt[57, 3] = "xtuda";
        Stt[58, 0] = "っぢ"; Stt[58, 1] = "ddi"; Stt[58, 2] = "ltudi"; Stt[58, 3] = "xtudi";
        Stt[59, 0] = "っづ"; Stt[59, 1] = "ddu"; Stt[59, 2] = "ltudu"; Stt[59, 3] = "xtudu";
        Stt[60, 0] = "っで"; Stt[60, 1] = "dde"; Stt[60, 2] = "ltude"; Stt[60, 3] = "xtude";
        Stt[61, 0] = "っど"; Stt[61, 1] = "ddo"; Stt[61, 2] = "ltudo"; Stt[61, 3] = "xtudo";
        Stt[62, 0] = "っば"; Stt[62, 1] = "bba"; Stt[62, 2] = "ltuba"; Stt[62, 3] = "xtuba";
        Stt[63, 0] = "っび"; Stt[63, 1] = "bbi"; Stt[63, 2] = "ltubi"; Stt[63, 3] = "xtubi";
        Stt[64, 0] = "っぶ"; Stt[64, 1] = "bbu"; Stt[64, 2] = "ltubu"; Stt[64, 3] = "xtubu";
        Stt[65, 0] = "っべ"; Stt[65, 1] = "bbe"; Stt[65, 2] = "ltube"; Stt[65, 3] = "xtube";
        Stt[66, 0] = "っぼ"; Stt[66, 1] = "bbo"; Stt[66, 2] = "ltubo"; Stt[66, 3] = "xtubo";
        Stt[67, 0] = "っぱ"; Stt[67, 1] = "ppa"; Stt[67, 2] = "ltupa"; Stt[67, 3] = "xtupa";
        Stt[68, 0] = "っぴ"; Stt[68, 1] = "ppi"; Stt[68, 2] = "ltupi"; Stt[68, 3] = "xtupi";
        Stt[69, 0] = "っぷ"; Stt[69, 1] = "ppu"; Stt[69, 2] = "ltupu"; Stt[69, 3] = "xtupu";
        Stt[70, 0] = "っぺ"; Stt[70, 1] = "ppe"; Stt[70, 2] = "ltupe"; Stt[70, 3] = "xtupe";
        Stt[71, 0] = "っぽ"; Stt[71, 1] = "ppo"; Stt[71, 2] = "ltupo"; Stt[71, 3] = "xtupo";
        Stt[72, 0] = "?b?J";
        Stt[73, 0] = "?b?L";
        Stt[74, 0] = "?b?N";
        Stt[75, 0] = "?b?P";
        Stt[76, 0] = "?b?R";
        Stt[77, 0] = "?b?T";
        Stt[78, 0] = "?b?V";
        Stt[79, 0] = "?b?X";
        Stt[80, 0] = "?b?Z";
        Stt[81, 0] = "?b?";
        Stt[82, 0] = "?b?^";
        Stt[83, 0] = "?b?`";
        Stt[84, 0] = "?b?c";
        Stt[85, 0] = "?b?e";
        Stt[86, 0] = "?b?g";
        Stt[87, 0] = "?b?n";
        Stt[88, 0] = "?b?q";
        Stt[89, 0] = "?b?t";
        Stt[90, 0] = "?b?w";
        Stt[91, 0] = "?b?z";
        Stt[92, 0] = "?b?}";
        Stt[93, 0] = "?b?~";
        Stt[94, 0] = "?b??";
        Stt[95, 0] = "?b??";
        Stt[96, 0] = "?b??";
        Stt[97, 0] = "?b??";
        Stt[98, 0] = "?b??";
        Stt[99, 0] = "?b??";
        Stt[100, 0] = "?b??";
        Stt[101, 0] = "?b??";
        Stt[102, 0] = "?b??";
        Stt[103, 0] = "?b??";
        Stt[104, 0] = "?b??";
        Stt[105, 0] = "?b??";
        Stt[106, 0] = "?b??";
        Stt[107, 0] = "?b?@";
        Stt[108, 0] = "?b?B";
        Stt[109, 0] = "?b?D";
        Stt[110, 0] = "?b?F";
        Stt[111, 0] = "?b?H";
        Stt[112, 0] = "?b?b";
        Stt[113, 0] = "?b??";
        Stt[114, 0] = "?b??";
        Stt[115, 0] = "?b??";
        Stt[116, 0] = "?b??";
        Stt[117, 0] = "?b??";
        Stt[118, 0] = "?b??";
        Stt[119, 0] = "?b?K";
        Stt[120, 0] = "?b?M";
        Stt[121, 0] = "?b?O";
        Stt[122, 0] = "?b?Q";
        Stt[123, 0] = "?b?S";
        Stt[124, 0] = "?b?U";
        Stt[125, 0] = "?b?W";
        Stt[126, 0] = "?b?Y";
        Stt[127, 0] = "?b?[";
        Stt[128, 0] = "?b?]";
        Stt[129, 0] = "?b?_";
        Stt[130, 0] = "?b?a";
        Stt[131, 0] = "?b?d";
        Stt[132, 0] = "?b?f";
        Stt[133, 0] = "?b?h";
        Stt[134, 0] = "?b?o";
        Stt[135, 0] = "?b?r";
        Stt[136, 0] = "?b?u";
        Stt[137, 0] = "?b?x";
        Stt[138, 0] = "?b?{";
        Stt[139, 0] = "?b?p";
        Stt[140, 0] = "?b?s";
        Stt[141, 0] = "?b?v";
        Stt[142, 0] = "?b?y";
        Stt[143, 0] = "?b?|";

        for (int i = STT_COL / 2; i < STT_COL; i++)
        {
            for (int k = 1; k < STT_ROW; k++)
            {
                Stt[i, k] = Stt[i - (STT_COL / 2), k];
            }
        }
    }
    void SetSttex()
    {
        for (int i = 0; i < STTEX_COL; i++)
        {
            for (int k = 0; k < STTEX_ROW; k++)
            {
                Sttex[i, k] = string.Empty;
            }
        }
        Sttex[0, 0] = "っいぇ"; Sttex[0, 1] = "yye"; Sttex[0, 2] = "ltuye"; Sttex[0, 3] = "xtuye";
        Sttex[1, 0] = "っうぁ"; Sttex[1, 1] = "wwha"; Sttex[1, 2] = "ltuwha"; Sttex[1, 3] = "xtuwha";
        Sttex[2, 0] = "っうぃ"; Sttex[2, 1] = "wwi"; Sttex[2, 2] = "wwhi"; Sttex[2, 3] = "ltuwi"; Sttex[2, 4] = "ltuwhi"; Sttex[2, 5] = "xtuwi"; Sttex[2, 6] = "xtuwhi";
        Sttex[3, 0] = "っうぇ"; Sttex[3, 1] = "wwe"; Sttex[3, 2] = "wwhe"; Sttex[3, 3] = "ltuwe"; Sttex[3, 4] = "ltuwhe"; Sttex[3, 5] = "xtuwe"; Sttex[3, 6] = "xtuwhe";
        Sttex[4, 0] = "っうぉ"; Sttex[4, 1] = "wwho"; Sttex[4, 2] = "ltuwho"; Sttex[4, 3] = "xtuwho";
        Sttex[5, 0] = "っきぃ"; Sttex[5, 1] = "kkyi"; Sttex[5, 2] = "ltukyi"; Sttex[5, 3] = "xtukyi";
        Sttex[6, 0] = "っきぇ"; Sttex[6, 1] = "kkye"; Sttex[6, 2] = "ltukye"; Sttex[6, 3] = "xtukye";
        Sttex[7, 0] = "っきゃ"; Sttex[7, 1] = "kkya"; Sttex[7, 2] = "ltukya"; Sttex[7, 3] = "xtukya";
        Sttex[8, 0] = "っきゅ"; Sttex[8, 1] = "kkyu"; Sttex[8, 2] = "ltukyu"; Sttex[8, 3] = "xtukyu";
        Sttex[9, 0] = "っきょ"; Sttex[9, 1] = "kkyo"; Sttex[9, 2] = "ltukyo"; Sttex[9, 3] = "xtukyo";
        Sttex[10, 0] = "っぎぃ"; Sttex[10, 1] = "ggyi"; Sttex[10, 2] = "ltugyi"; Sttex[10, 3] = "xtugyi";
        Sttex[11, 0] = "っぎぇ"; Sttex[11, 1] = "ggye"; Sttex[11, 2] = "ltugye"; Sttex[11, 3] = "xtugye";
        Sttex[12, 0] = "っぎゃ"; Sttex[12, 1] = "ggya"; Sttex[12, 2] = "ltugya"; Sttex[12, 3] = "xtugya";
        Sttex[13, 0] = "っぎゅ"; Sttex[13, 1] = "ggyu"; Sttex[13, 2] = "ltugyu"; Sttex[13, 3] = "xtugyu";
        Sttex[14, 0] = "っぎょ"; Sttex[14, 1] = "ggyo"; Sttex[14, 2] = "ltugyo"; Sttex[14, 3] = "xtugyo";
        Sttex[15, 0] = "っくぁ"; Sttex[15, 1] = "qqa"; Sttex[15, 2] = "qqwa"; Sttex[15, 3] = "kkwa"; Sttex[15, 4] = "ltuqa"; Sttex[15, 5] = "ltuqwa"; Sttex[15, 6] = "xtuqa";
        Sttex[16, 0] = "っくぃ"; Sttex[16, 1] = "qqi"; Sttex[16, 2] = "qqwi"; Sttex[16, 3] = "qqyi"; Sttex[16, 4] = "ltuqi"; Sttex[16, 5] = "ltuqwi"; Sttex[16, 6] = "xtuqi";
        Sttex[17, 0] = "っくぅ"; Sttex[17, 1] = "qqwu"; Sttex[17, 2] = "ltuqwu"; Sttex[17, 3] = "xtuqwu";
        Sttex[18, 0] = "っくぇ"; Sttex[18, 1] = "qqe"; Sttex[18, 2] = "qqwe"; Sttex[18, 3] = "qqye"; Sttex[18, 4] = "ltuqe"; Sttex[18, 5] = "ltuqye"; Sttex[18, 6] = "xtuqe";
        Sttex[19, 0] = "っくぉ"; Sttex[19, 1] = "qqo"; Sttex[19, 2] = "qqwo"; Sttex[19, 3] = "ltuqo"; Sttex[19, 4] = "ltuqwo"; Sttex[19, 5] = "xtuqo"; Sttex[19, 6] = "xtuqwo";
        Sttex[20, 0] = "っくゃ"; Sttex[20, 1] = "qqya"; Sttex[20, 2] = "ltuqya"; Sttex[20, 3] = "xtuqya";
        Sttex[21, 0] = "っくゅ"; Sttex[21, 1] = "qqyu"; Sttex[21, 2] = "ltuqyu"; Sttex[21, 3] = "xtuqyu";
        Sttex[22, 0] = "っくょ"; Sttex[22, 1] = "qqyo"; Sttex[22, 2] = "ltuqyo"; Sttex[22, 3] = "xtuqyo";
        Sttex[23, 0] = "っぐぁ"; Sttex[23, 1] = "ggwa"; Sttex[23, 2] = "ltugwa"; Sttex[23, 3] = "xtugwa";
        Sttex[24, 0] = "っぐぃ"; Sttex[24, 1] = "ggwi"; Sttex[24, 2] = "ltugwi"; Sttex[24, 3] = "xtugwi";
        Sttex[25, 0] = "っぐぅ"; Sttex[25, 1] = "ggwu"; Sttex[25, 2] = "ltugwu"; Sttex[25, 3] = "xtugwu";
        Sttex[26, 0] = "っぐぇ"; Sttex[26, 1] = "ggwe"; Sttex[26, 2] = "ltugwe"; Sttex[26, 3] = "xtugwe";
        Sttex[27, 0] = "っぐぉ"; Sttex[27, 1] = "ggwo"; Sttex[27, 2] = "ltugwo"; Sttex[27, 3] = "xtugwo";
        Sttex[28, 0] = "っしぃ"; Sttex[28, 1] = "ssyi"; Sttex[28, 2] = "ltusyi"; Sttex[28, 3] = "xtusyi";
        Sttex[29, 0] = "っしぇ"; Sttex[29, 1] = "ssye"; Sttex[29, 2] = "sshe"; Sttex[29, 3] = "ltusye"; Sttex[29, 4] = "ltushe"; Sttex[29, 5] = "xtusye"; Sttex[29, 6] = "xtushe";
        Sttex[30, 0] = "っしゃ"; Sttex[30, 1] = "ssya"; Sttex[30, 2] = "ssha"; Sttex[30, 3] = "ltusya"; Sttex[30, 4] = "ltusha"; Sttex[30, 5] = "xtusya"; Sttex[30, 6] = "xtusha";
        Sttex[31, 0] = "っしゅ"; Sttex[31, 1] = "ssyu"; Sttex[31, 2] = "sshu"; Sttex[31, 3] = "ltusyu"; Sttex[31, 4] = "ltushu"; Sttex[31, 5] = "xtusyu"; Sttex[31, 6] = "xtushu";
        Sttex[32, 0] = "っしょ"; Sttex[32, 1] = "ssyo"; Sttex[32, 2] = "ssho"; Sttex[32, 3] = "ltusyo"; Sttex[32, 4] = "ltusho"; Sttex[32, 5] = "xtusyo"; Sttex[32, 6] = "xtusho";
        Sttex[33, 0] = "っじぃ"; Sttex[33, 1] = "jjyi"; Sttex[33, 2] = "zzyi"; Sttex[33, 3] = "ltujyi"; Sttex[33, 4] = "ltuzyi"; Sttex[33, 5] = "xtujyi"; Sttex[33, 6] = "xtuzyi";
        Sttex[34, 0] = "っじぇ"; Sttex[34, 1] = "jje"; Sttex[34, 2] = "jjye"; Sttex[34, 3] = "zzye"; Sttex[34, 4] = "ltuje"; Sttex[34, 5] = "ltujye"; Sttex[34, 6] = "ltuzye";
        Sttex[35, 0] = "っじゃ"; Sttex[35, 1] = "jja"; Sttex[35, 2] = "jjya"; Sttex[35, 3] = "zzya"; Sttex[35, 4] = "ltuja"; Sttex[35, 5] = "ltujya"; Sttex[35, 6] = "ltuzya";
        Sttex[36, 0] = "っじゅ"; Sttex[36, 1] = "jju"; Sttex[36, 2] = "jjyu"; Sttex[36, 3] = "zzyu"; Sttex[36, 4] = "ltuju"; Sttex[36, 5] = "ltujyu"; Sttex[36, 6] = "ltuzyu";
        Sttex[37, 0] = "っじょ"; Sttex[37, 1] = "jjo"; Sttex[37, 2] = "jjyo"; Sttex[37, 3] = "zzyo"; Sttex[37, 4] = "ltujo"; Sttex[37, 5] = "ltujyo"; Sttex[37, 6] = "ltuzyo";
        Sttex[38, 0] = "っすぁ"; Sttex[38, 1] = "sswa"; Sttex[38, 2] = "ltuswa"; Sttex[38, 3] = "xtuswa";
        Sttex[39, 0] = "っすぃ"; Sttex[39, 1] = "sswi"; Sttex[39, 2] = "ltuswi"; Sttex[39, 3] = "xtuswi";
        Sttex[40, 0] = "っすぅ"; Sttex[40, 1] = "sswu"; Sttex[40, 2] = "ltuswu"; Sttex[40, 3] = "xtuswu";
        Sttex[41, 0] = "っすぇ"; Sttex[41, 1] = "sswe"; Sttex[41, 2] = "ltuswe"; Sttex[41, 3] = "xtuswe";
        Sttex[42, 0] = "っすぉ"; Sttex[42, 1] = "sswo"; Sttex[42, 2] = "ltuswo"; Sttex[42, 3] = "xtuswo";
        Sttex[43, 0] = "っちぃ"; Sttex[43, 1] = "ttyi"; Sttex[43, 2] = "ccyi"; Sttex[43, 3] = "ltutyi"; Sttex[43, 4] = "ltucyi"; Sttex[43, 5] = "xtutyi"; Sttex[43, 6] = "xtucyi";
        Sttex[44, 0] = "っちぇ"; Sttex[44, 1] = "ttye"; Sttex[44, 2] = "ccye"; Sttex[44, 3] = "cche"; Sttex[44, 4] = "ltutye"; Sttex[44, 5] = "ltucye"; Sttex[44, 6] = "ltuche";
        Sttex[45, 0] = "っちゃ"; Sttex[45, 1] = "ttya"; Sttex[45, 2] = "ccya"; Sttex[45, 3] = "ccha"; Sttex[45, 4] = "ltutya"; Sttex[45, 5] = "ltucya"; Sttex[45, 6] = "ltucha";
        Sttex[46, 0] = "っちゅ"; Sttex[46, 1] = "ttyu"; Sttex[46, 2] = "ccyu"; Sttex[46, 3] = "cchu"; Sttex[46, 4] = "ltutyu"; Sttex[46, 5] = "ltucyu"; Sttex[46, 6] = "ltuchu";
        Sttex[47, 0] = "っちょ"; Sttex[47, 1] = "ttyo"; Sttex[47, 2] = "ccyo"; Sttex[47, 3] = "ccho"; Sttex[47, 4] = "ltutyo"; Sttex[47, 5] = "ltucyo"; Sttex[47, 6] = "ltucho";
        Sttex[48, 0] = "っぢぃ"; Sttex[48, 1] = "ddyi"; Sttex[48, 2] = "ltudyi"; Sttex[48, 3] = "xtudyi";
        Sttex[49, 0] = "っぢぇ"; Sttex[49, 1] = "ddye"; Sttex[49, 2] = "ltudye"; Sttex[49, 3] = "xtudye";
        Sttex[50, 0] = "っぢゃ"; Sttex[50, 1] = "ddya"; Sttex[50, 2] = "ltudya"; Sttex[50, 3] = "xtudya";
        Sttex[51, 0] = "っぢゅ"; Sttex[51, 1] = "ddyu"; Sttex[51, 2] = "ltudyu"; Sttex[51, 3] = "xtudyu";
        Sttex[52, 0] = "っぢょ"; Sttex[52, 1] = "ddyo"; Sttex[52, 2] = "ltudyo"; Sttex[52, 3] = "xtudyo";
        Sttex[53, 0] = "っつぁ"; Sttex[53, 1] = "ttsa"; Sttex[53, 2] = "ltutsa"; Sttex[53, 3] = "xtutsa";
        Sttex[54, 0] = "っつぃ"; Sttex[54, 1] = "ttsi"; Sttex[54, 2] = "ltutsi"; Sttex[54, 3] = "xtutsi";
        Sttex[55, 0] = "っつぇ"; Sttex[55, 1] = "ttse"; Sttex[55, 2] = "ltutse"; Sttex[55, 3] = "xtutse";
        Sttex[56, 0] = "っつぉ"; Sttex[56, 1] = "ttso"; Sttex[56, 2] = "ltutso"; Sttex[56, 3] = "xtutso";
        Sttex[57, 0] = "ってぃ"; Sttex[57, 1] = "tthi"; Sttex[57, 2] = "ltuthi"; Sttex[57, 3] = "xtuthi";
        Sttex[58, 0] = "ってぇ"; Sttex[58, 1] = "tthe"; Sttex[58, 2] = "ltuthe"; Sttex[58, 3] = "xtuthe";
        Sttex[59, 0] = "ってゃ"; Sttex[59, 1] = "ttha"; Sttex[59, 2] = "ltutha"; Sttex[59, 3] = "xtutha";
        Sttex[60, 0] = "ってゅ"; Sttex[60, 1] = "tthu"; Sttex[60, 2] = "ltuthu"; Sttex[60, 3] = "xtuthu";
        Sttex[61, 0] = "ってょ"; Sttex[61, 1] = "ttho"; Sttex[61, 2] = "ltutho"; Sttex[61, 3] = "xtutho";
        Sttex[62, 0] = "っでぃ"; Sttex[62, 1] = "ddhi"; Sttex[62, 2] = "ltudhi"; Sttex[62, 3] = "xtudhi";
        Sttex[63, 0] = "っでぇ"; Sttex[63, 1] = "ddhe"; Sttex[63, 2] = "ltudhe"; Sttex[63, 3] = "xtudhe";
        Sttex[64, 0] = "っでゃ"; Sttex[64, 1] = "ddha"; Sttex[64, 2] = "ltudha"; Sttex[64, 3] = "xtudha";
        Sttex[65, 0] = "っでゅ"; Sttex[65, 1] = "ddhu"; Sttex[65, 2] = "ltudhu"; Sttex[65, 3] = "xtudhu";
        Sttex[66, 0] = "っでょ"; Sttex[66, 1] = "ddho"; Sttex[66, 2] = "ltudho"; Sttex[66, 3] = "xtudho";
        Sttex[67, 0] = "っとぁ"; Sttex[67, 1] = "ttwa"; Sttex[67, 2] = "ltutwa"; Sttex[67, 3] = "xtutwa";
        Sttex[68, 0] = "っとぃ"; Sttex[68, 1] = "ttwi"; Sttex[68, 2] = "ltutwi"; Sttex[68, 3] = "xtutwi";
        Sttex[69, 0] = "っとぅ"; Sttex[69, 1] = "ttwu"; Sttex[69, 2] = "ltutwu"; Sttex[69, 3] = "xtutwu";
        Sttex[70, 0] = "っとぇ"; Sttex[70, 1] = "ttwe"; Sttex[70, 2] = "ltutwe"; Sttex[70, 3] = "xtutwe";
        Sttex[71, 0] = "っとぉ"; Sttex[71, 1] = "ttwo"; Sttex[71, 2] = "ltutwo"; Sttex[71, 3] = "xtutwo";
        Sttex[72, 0] = "っどぁ"; Sttex[72, 1] = "ddwa"; Sttex[72, 2] = "ltudwa"; Sttex[72, 3] = "xtudwa";
        Sttex[73, 0] = "っどぃ"; Sttex[73, 1] = "ddwi"; Sttex[73, 2] = "ltudwi"; Sttex[73, 3] = "xtudwi";
        Sttex[74, 0] = "っどぅ"; Sttex[74, 1] = "ddwu"; Sttex[74, 2] = "ltudwu"; Sttex[74, 3] = "xtudwu";
        Sttex[75, 0] = "っどぇ"; Sttex[75, 1] = "ddwe"; Sttex[75, 2] = "ltudwe"; Sttex[75, 3] = "xtudwe";
        Sttex[76, 0] = "っどぉ"; Sttex[76, 1] = "ddwo"; Sttex[76, 2] = "ltudwo"; Sttex[76, 3] = "xtudwo";
        Sttex[77, 0] = "っひぃ"; Sttex[77, 1] = "hhyi"; Sttex[77, 2] = "ltuhyi"; Sttex[77, 3] = "xtuhyi";
        Sttex[78, 0] = "っひぇ"; Sttex[78, 1] = "hhye"; Sttex[78, 2] = "ltuhye"; Sttex[78, 3] = "xtuhye";
        Sttex[79, 0] = "っひゃ"; Sttex[79, 1] = "hhya"; Sttex[79, 2] = "ltuhya"; Sttex[79, 3] = "xtuhya";
        Sttex[80, 0] = "っひゅ"; Sttex[80, 1] = "hhyu"; Sttex[80, 2] = "ltuhyu"; Sttex[80, 3] = "xtuhyu";
        Sttex[81, 0] = "っひょ"; Sttex[81, 1] = "hhyo"; Sttex[81, 2] = "ltuhyo"; Sttex[81, 3] = "xtuhyo";
        Sttex[82, 0] = "っびぃ"; Sttex[82, 1] = "bbyi"; Sttex[82, 2] = "ltubyi"; Sttex[82, 3] = "xtubyi";
        Sttex[83, 0] = "っびぇ"; Sttex[83, 1] = "bbye"; Sttex[83, 2] = "ltubye"; Sttex[83, 3] = "xtubye";
        Sttex[84, 0] = "っびゃ"; Sttex[84, 1] = "bbya"; Sttex[84, 2] = "ltubya"; Sttex[84, 3] = "xtubya";
        Sttex[85, 0] = "っびゅ"; Sttex[85, 1] = "bbyu"; Sttex[85, 2] = "ltubyu"; Sttex[85, 3] = "xtubyu";
        Sttex[86, 0] = "っびょ"; Sttex[86, 1] = "bbyo"; Sttex[86, 2] = "ltubyo"; Sttex[86, 3] = "xtubyo";
        Sttex[87, 0] = "っぴぃ"; Sttex[87, 1] = "ppyi"; Sttex[87, 2] = "ltupyi"; Sttex[87, 3] = "xtupyi";
        Sttex[88, 0] = "っぴぇ"; Sttex[88, 1] = "ppye"; Sttex[88, 2] = "ltupye"; Sttex[88, 3] = "xtupye";
        Sttex[89, 0] = "っぴゃ"; Sttex[89, 1] = "ppya"; Sttex[89, 2] = "ltupya"; Sttex[89, 3] = "xtupya";
        Sttex[90, 0] = "っぴゅ"; Sttex[90, 1] = "ppyu"; Sttex[90, 2] = "ltupyu"; Sttex[90, 3] = "xtupyu";
        Sttex[91, 0] = "っぴょ"; Sttex[91, 1] = "ppyo"; Sttex[91, 2] = "ltupyo"; Sttex[91, 3] = "xtupyo";
        Sttex[92, 0] = "っふぁ"; Sttex[92, 1] = "ffa"; Sttex[92, 2] = "ffwa"; Sttex[92, 3] = "ltufa"; Sttex[92, 4] = "ltufwa";
        Sttex[93, 0] = "っふぃ"; Sttex[93, 1] = "ffi"; Sttex[93, 2] = "ffyi"; Sttex[93, 3] = "ffwi"; Sttex[93, 4] = "ltufi"; Sttex[93, 5] = "ltufyi"; Sttex[93, 6] = "ltufwi";
        Sttex[94, 0] = "っふぅ"; Sttex[94, 1] = "ffwu"; Sttex[94, 2] = "ltufwu"; Sttex[94, 3] = "xtufwu";
        Sttex[95, 0] = "っふぇ"; Sttex[95, 1] = "ffe"; Sttex[95, 2] = "ffye"; Sttex[95, 3] = "ffwe"; Sttex[95, 4] = "ltufe"; Sttex[95, 5] = "ltufye"; Sttex[95, 6] = "ltufwe";
        Sttex[96, 0] = "っふぉ"; Sttex[96, 1] = "ffo"; Sttex[96, 2] = "ffwo"; Sttex[96, 3] = "ltufo"; Sttex[96, 4] = "ltufwo"; Sttex[96, 5] = "xtufo"; Sttex[96, 6] = "xtufwo";
        Sttex[97, 0] = "っふゃ"; Sttex[97, 1] = "ffya"; Sttex[97, 2] = "ltufya"; Sttex[97, 3] = "xtufya";
        Sttex[98, 0] = "っふゅ"; Sttex[98, 1] = "ffyu"; Sttex[98, 2] = "ltufyu"; Sttex[98, 3] = "xtufyu";
        Sttex[99, 0] = "っふょ"; Sttex[99, 1] = "ffyo"; Sttex[99, 2] = "ltufyo"; Sttex[99, 3] = "xtufyo";
        Sttex[100, 0] = "っみぃ"; Sttex[100, 1] = "mmyi"; Sttex[100, 2] = "ltumyi"; Sttex[100, 3] = "xtumyi";
        Sttex[101, 0] = "っみぇ"; Sttex[101, 1] = "mmye"; Sttex[101, 2] = "ltumye"; Sttex[101, 3] = "xtumye";
        Sttex[102, 0] = "っみゃ"; Sttex[102, 1] = "mmya"; Sttex[102, 2] = "ltumya"; Sttex[102, 3] = "xtumya";
        Sttex[103, 0] = "っみゅ"; Sttex[103, 1] = "mmyu"; Sttex[103, 2] = "ltumyu"; Sttex[103, 3] = "xtumyu";
        Sttex[104, 0] = "っみょ"; Sttex[104, 1] = "mmyo"; Sttex[104, 2] = "ltumyo"; Sttex[104, 3] = "xtumyo";
        Sttex[105, 0] = "っりぃ"; Sttex[105, 1] = "rryi"; Sttex[105, 2] = "lturyi"; Sttex[105, 3] = "xturyi";
        Sttex[106, 0] = "っりぇ"; Sttex[106, 1] = "rrye"; Sttex[106, 2] = "lturye"; Sttex[106, 3] = "xturye";
        Sttex[107, 0] = "っりゃ"; Sttex[107, 1] = "rrya"; Sttex[107, 2] = "lturya"; Sttex[107, 3] = "xturya";
        Sttex[108, 0] = "っりゅ"; Sttex[108, 1] = "rryu"; Sttex[108, 2] = "lturyu"; Sttex[108, 3] = "xturyu";
        Sttex[109, 0] = "っりょ"; Sttex[109, 1] = "rryo"; Sttex[109, 2] = "lturyo"; Sttex[109, 3] = "xturyo";
        Sttex[110, 0] = "っヴぁ"; Sttex[110, 1] = "vva"; Sttex[110, 2] = "ltuva"; Sttex[110, 3] = "xtuva";
        Sttex[111, 0] = "っヴぃ"; Sttex[111, 1] = "vvi"; Sttex[111, 2] = "vvyi"; Sttex[111, 3] = "ltuvi"; Sttex[111, 4] = "ltuvyi"; Sttex[111, 5] = "xtuvi"; Sttex[111, 6] = "xtuvyi";
        Sttex[112, 0] = "っヴぇ"; Sttex[112, 1] = "vve"; Sttex[112, 2] = "vvye"; Sttex[112, 3] = "ltuve"; Sttex[112, 4] = "ltuvye"; Sttex[112, 5] = "xtuve"; Sttex[112, 6] = "xtuvye";
        Sttex[113, 0] = "っヴぉ"; Sttex[113, 1] = "vvo"; Sttex[113, 2] = "ltuvo"; Sttex[113, 3] = "xtuvo";
        Sttex[114, 0] = "っヴゃ"; Sttex[114, 1] = "vvya"; Sttex[114, 2] = "ltuvya"; Sttex[114, 3] = "xtuvya";
        Sttex[115, 0] = "っヴゅ"; Sttex[115, 1] = "vvyu"; Sttex[115, 2] = "ltuvyu"; Sttex[115, 3] = "xtuvyu";
        Sttex[116, 0] = "っヴょ"; Sttex[116, 1] = "vvyo"; Sttex[116, 2] = "ltuvyo"; Sttex[116, 3] = "xtuvyo";
        Sttex[117, 0] = "?b?C?F";
        Sttex[118, 0] = "?b?E?@";
        Sttex[119, 0] = "?b?E?B";
        Sttex[120, 0] = "?b?E?F";
        Sttex[121, 0] = "?b?E?H";
        Sttex[122, 0] = "?b?L?B";
        Sttex[123, 0] = "?b?L?F";
        Sttex[124, 0] = "?b?L??";
        Sttex[125, 0] = "?b?L??";
        Sttex[126, 0] = "?b?L??";
        Sttex[127, 0] = "?b?M?B";
        Sttex[128, 0] = "?b?M?F";
        Sttex[129, 0] = "?b?M??";
        Sttex[130, 0] = "?b?M??";
        Sttex[131, 0] = "?b?M??";
        Sttex[132, 0] = "?b?N?@";
        Sttex[133, 0] = "?b?N?B";
        Sttex[134, 0] = "?b?N?D";
        Sttex[135, 0] = "?b?N?F";
        Sttex[136, 0] = "?b?N?H";
        Sttex[137, 0] = "?b?N??";
        Sttex[138, 0] = "?b?N??";
        Sttex[139, 0] = "?b?N??";
        Sttex[140, 0] = "?b?O?@";
        Sttex[141, 0] = "?b?O?B";
        Sttex[142, 0] = "?b?O?D";
        Sttex[143, 0] = "?b?O?F";
        Sttex[144, 0] = "?b?O?H";
        Sttex[145, 0] = "?b?V?B";
        Sttex[146, 0] = "?b?V?F";
        Sttex[147, 0] = "?b?V??";
        Sttex[148, 0] = "?b?V??";
        Sttex[149, 0] = "?b?V??";
        Sttex[150, 0] = "?b?W?B";
        Sttex[151, 0] = "?b?W?F";
        Sttex[152, 0] = "?b?W??";
        Sttex[153, 0] = "?b?W??";
        Sttex[154, 0] = "?b?W??";
        Sttex[155, 0] = "?b?X?@";
        Sttex[156, 0] = "?b?X?B";
        Sttex[157, 0] = "?b?X?D";
        Sttex[158, 0] = "?b?X?F";
        Sttex[159, 0] = "?b?X?H";
        Sttex[160, 0] = "?b?`?B";
        Sttex[161, 0] = "?b?`?F";
        Sttex[162, 0] = "?b?`??";
        Sttex[163, 0] = "?b?`??";
        Sttex[164, 0] = "?b?`??";
        Sttex[165, 0] = "?b?a?B";
        Sttex[166, 0] = "?b?a?F";
        Sttex[167, 0] = "?b?a??";
        Sttex[168, 0] = "?b?a??";
        Sttex[169, 0] = "?b?a??";
        Sttex[170, 0] = "?b?c?@";
        Sttex[171, 0] = "?b?c?B";
        Sttex[172, 0] = "?b?c?F";
        Sttex[173, 0] = "?b?c?H";
        Sttex[174, 0] = "?b?e?B";
        Sttex[175, 0] = "?b?e?F";
        Sttex[176, 0] = "?b?e??";
        Sttex[177, 0] = "?b?e??";
        Sttex[178, 0] = "?b?e??";
        Sttex[179, 0] = "?b?f?B";
        Sttex[180, 0] = "?b?f?F";
        Sttex[181, 0] = "?b?f??";
        Sttex[182, 0] = "?b?f??";
        Sttex[183, 0] = "?b?f??";
        Sttex[184, 0] = "?b?g?@";
        Sttex[185, 0] = "?b?g?B";
        Sttex[186, 0] = "?b?g?D";
        Sttex[187, 0] = "?b?g?F";
        Sttex[188, 0] = "?b?g?H";
        Sttex[189, 0] = "?b?h?@";
        Sttex[190, 0] = "?b?h?B";
        Sttex[191, 0] = "?b?h?H";
        Sttex[192, 0] = "?b?h?F";
        Sttex[193, 0] = "?b?h?H";
        Sttex[194, 0] = "?b?q?B";
        Sttex[195, 0] = "?b?q?F";
        Sttex[196, 0] = "?b?q??";
        Sttex[197, 0] = "?b?q??";
        Sttex[198, 0] = "?b?q??";
        Sttex[199, 0] = "?b?r?B";
        Sttex[200, 0] = "?b?r?F";
        Sttex[201, 0] = "?b?r??";
        Sttex[202, 0] = "?b?r??";
        Sttex[203, 0] = "?b?r??";
        Sttex[204, 0] = "?b?s?B";
        Sttex[205, 0] = "?b?s?F";
        Sttex[206, 0] = "?b?s??";
        Sttex[207, 0] = "?b?s??";
        Sttex[208, 0] = "?b?s??";
        Sttex[209, 0] = "?b?t?@";
        Sttex[210, 0] = "?b?t?B";
        Sttex[211, 0] = "?b?t?D";
        Sttex[212, 0] = "?b?t?F";
        Sttex[213, 0] = "?b?t?H";
        Sttex[214, 0] = "?b?t??";
        Sttex[215, 0] = "?b?t??";
        Sttex[216, 0] = "?b?t??";
        Sttex[217, 0] = "?b?~?B";
        Sttex[218, 0] = "?b?~?F";
        Sttex[219, 0] = "?b?~??";
        Sttex[220, 0] = "?b?~??";
        Sttex[221, 0] = "?b?~??";
        Sttex[222, 0] = "?b???B";
        Sttex[223, 0] = "?b???F";
        Sttex[224, 0] = "?b????";
        Sttex[225, 0] = "?b????";
        Sttex[226, 0] = "?b????";
        Sttex[227, 0] = "?b???@";
        Sttex[228, 0] = "?b???B";
        Sttex[229, 0] = "?b???F";
        Sttex[230, 0] = "?b???H";
        Sttex[231, 0] = "?b????";
        Sttex[232, 0] = "?b????";
        Sttex[233, 0] = "?b????";
        for (int i = STTEX_COL / 2; i < STTEX_COL; i++)
        {
            for (int k = 1; k < STTEX_ROW; k++)
            {
                Sttex[i, k] = Sttex[i - (STTEX_COL / 2), k];
            }
        }
    }
    void SetStn()
    {
        for (int i = 0; i < STN_COL; i++)
        {
            for (int k = 0; k < STN_ROW; k++)
            {
                Stn[i, k] = string.Empty;
            }
        }
        Stn[0, 0] = "んか"; Stn[0, 1] = "nnka"; Stn[0, 2] = "nnca"; Stn[0, 5] = "nka"; Stn[0, 6] = "nca";
        Stn[1, 0] = "んき"; Stn[1, 1] = "nnki"; Stn[1, 5] = "nki";
        Stn[2, 0] = "んく"; Stn[2, 1] = "nnku"; Stn[2, 2] = "nncu"; Stn[2, 3] = "nnqu"; Stn[2, 5] = "nku"; Stn[2, 6] = "ncu"; Stn[2, 7] = "nqu";
        Stn[3, 0] = "んけ"; Stn[3, 1] = "nnke"; Stn[3, 5] = "nke";
        Stn[4, 0] = "んこ"; Stn[4, 1] = "nnko"; Stn[4, 2] = "nnco"; Stn[4, 5] = "nko"; Stn[4, 6] = "nco";
        Stn[5, 0] = "んさ"; Stn[5, 1] = "nnsa"; Stn[5, 5] = "nsa";
        Stn[6, 0] = "んし"; Stn[6, 1] = "nnsi"; Stn[6, 2] = "nnshi"; Stn[6, 3] = "nnci"; Stn[6, 5] = "nsi"; Stn[6, 6] = "nshi"; Stn[6, 7] = "nci";
        Stn[7, 0] = "んす"; Stn[7, 1] = "nnsu"; Stn[7, 5] = "nsu";
        Stn[8, 0] = "んせ"; Stn[8, 1] = "nnse"; Stn[8, 2] = "nnce"; Stn[8, 5] = "nse"; Stn[8, 6] = "nce";
        Stn[9, 0] = "んそ"; Stn[9, 1] = "nnso"; Stn[9, 5] = "nso";
        Stn[10, 0] = "んた"; Stn[10, 1] = "nnta"; Stn[10, 5] = "nta";
        Stn[11, 0] = "んち"; Stn[11, 1] = "nnti"; Stn[11, 2] = "nnchi"; Stn[11, 5] = "nti"; Stn[11, 6] = "nchi";
        Stn[12, 0] = "んつ"; Stn[12, 1] = "nntu"; Stn[12, 2] = "nntsu"; Stn[12, 5] = "ntu"; Stn[12, 6] = "ntsu";
        Stn[13, 0] = "んて"; Stn[13, 1] = "nnte"; Stn[13, 5] = "nte";
        Stn[14, 0] = "んと"; Stn[14, 1] = "nnto"; Stn[14, 5] = "nto";
        Stn[15, 0] = "んは"; Stn[15, 1] = "nnha"; Stn[15, 5] = "nha";
        Stn[16, 0] = "んひ"; Stn[16, 1] = "nnhi"; Stn[16, 5] = "nhi";
        Stn[17, 0] = "んふ"; Stn[17, 1] = "nnhu"; Stn[17, 2] = "nnfu"; Stn[17, 5] = "nhu"; Stn[17, 6] = "nfu";
        Stn[18, 0] = "んへ"; Stn[18, 1] = "nnhe"; Stn[18, 5] = "nhe";
        Stn[19, 0] = "んほ"; Stn[19, 1] = "nnho"; Stn[19, 5] = "nho";
        Stn[20, 0] = "んま"; Stn[20, 1] = "nnma"; Stn[20, 5] = "nma";
        Stn[21, 0] = "んみ"; Stn[21, 1] = "nnmi"; Stn[21, 5] = "nmi";
        Stn[22, 0] = "んむ"; Stn[22, 1] = "nnmu"; Stn[22, 5] = "nmu";
        Stn[23, 0] = "んめ"; Stn[23, 1] = "nnme"; Stn[23, 5] = "nme";
        Stn[24, 0] = "んも"; Stn[24, 1] = "nnmo"; Stn[24, 5] = "nmo";
        Stn[25, 0] = "んら"; Stn[25, 1] = "nnra"; Stn[25, 5] = "nra";
        Stn[26, 0] = "んり"; Stn[26, 1] = "nnri"; Stn[26, 5] = "nri";
        Stn[27, 0] = "んる"; Stn[27, 1] = "nnru"; Stn[27, 5] = "nru";
        Stn[28, 0] = "んれ"; Stn[28, 1] = "nnre"; Stn[28, 5] = "nre";
        Stn[29, 0] = "んろ"; Stn[29, 1] = "nnro"; Stn[29, 5] = "nro";
        Stn[30, 0] = "んわ"; Stn[30, 1] = "nnwa"; Stn[30, 5] = "nwa";
        Stn[31, 0] = "んを"; Stn[31, 1] = "nnwo"; Stn[31, 5] = "nwo";
        Stn[32, 0] = "んぁ"; Stn[32, 1] = "nnla"; Stn[32, 2] = "nnxa"; Stn[32, 5] = "nla"; Stn[32, 6] = "nxa";
        Stn[33, 0] = "んぃ"; Stn[33, 1] = "nnli"; Stn[33, 2] = "nnxi"; Stn[33, 3] = "nnlyi"; Stn[33, 4] = "nnxyi"; Stn[33, 5] = "nli"; Stn[33, 6] = "nxi"; Stn[33, 7] = "nlyi"; Stn[33, 8] = "nxyi";
        Stn[34, 0] = "んぅ"; Stn[34, 1] = "nnlu"; Stn[34, 2] = "nnxu"; Stn[34, 5] = "nlu"; Stn[34, 6] = "nxu";
        Stn[35, 0] = "んぇ"; Stn[35, 1] = "nnle"; Stn[35, 2] = "nnxe"; Stn[35, 3] = "nnlye"; Stn[35, 4] = "nnxye"; Stn[35, 5] = "nle"; Stn[35, 6] = "nxe"; Stn[35, 7] = "nlye"; Stn[35, 8] = "nxye";
        Stn[36, 0] = "んぉ"; Stn[36, 1] = "nnlo"; Stn[36, 2] = "nnxo"; Stn[36, 5] = "nlo"; Stn[36, 6] = "nxo";
        Stn[37, 0] = "んゃ"; Stn[37, 1] = "nnlya"; Stn[37, 2] = "nnxya"; Stn[37, 5] = "nlya"; Stn[37, 6] = "nxya";
        Stn[38, 0] = "んゅ"; Stn[38, 1] = "nnlyu"; Stn[38, 2] = "nnxyu"; Stn[38, 5] = "nlyu"; Stn[38, 6] = "nxyu";
        Stn[39, 0] = "んょ"; Stn[39, 1] = "nnlyo"; Stn[39, 2] = "nnxyo"; Stn[39, 5] = "nlyo"; Stn[39, 6] = "nxyo";
        Stn[40, 0] = "んゎ"; Stn[40, 1] = "nnlwa"; Stn[40, 2] = "nnxwa"; Stn[40, 5] = "nlwa"; Stn[40, 6] = "nxwa";
        Stn[41, 0] = "んヵ"; Stn[41, 1] = "nnlka"; Stn[41, 2] = "nnxka"; Stn[41, 5] = "nlka"; Stn[41, 6] = "nxka";
        Stn[42, 0] = "んヶ"; Stn[42, 1] = "nnlke"; Stn[42, 2] = "nnxke"; Stn[42, 5] = "nlke"; Stn[42, 6] = "nxke";
        Stn[43, 0] = "んが"; Stn[43, 1] = "nnga"; Stn[43, 5] = "nga";
        Stn[44, 0] = "んぎ"; Stn[44, 1] = "nngi"; Stn[44, 5] = "ngi";
        Stn[45, 0] = "んぐ"; Stn[45, 1] = "nngu"; Stn[45, 5] = "ngu";
        Stn[46, 0] = "んげ"; Stn[46, 1] = "nnge"; Stn[46, 5] = "nge";
        Stn[47, 0] = "んご"; Stn[47, 1] = "nngo"; Stn[47, 5] = "ngo";
        Stn[48, 0] = "んざ"; Stn[48, 1] = "nnza"; Stn[48, 5] = "nza";
        Stn[49, 0] = "んじ"; Stn[49, 1] = "nnji"; Stn[49, 2] = "nnzi"; Stn[49, 5] = "nji"; Stn[49, 6] = "nzi";
        Stn[50, 0] = "んず"; Stn[50, 1] = "nnzu"; Stn[50, 5] = "nzu";
        Stn[51, 0] = "んぜ"; Stn[51, 1] = "nnze"; Stn[51, 5] = "nze";
        Stn[52, 0] = "んぞ"; Stn[52, 1] = "nnzo"; Stn[52, 5] = "nzo";
        Stn[53, 0] = "んだ"; Stn[53, 1] = "nnda"; Stn[53, 5] = "nda";
        Stn[54, 0] = "んぢ"; Stn[54, 1] = "nndi"; Stn[54, 5] = "ndi";
        Stn[55, 0] = "んづ"; Stn[55, 1] = "nndu"; Stn[55, 5] = "ndu";
        Stn[56, 0] = "んで"; Stn[56, 1] = "nnde"; Stn[56, 5] = "nde";
        Stn[57, 0] = "んど"; Stn[57, 1] = "nndo"; Stn[57, 5] = "ndo";
        Stn[58, 0] = "んば"; Stn[58, 1] = "nnba"; Stn[58, 5] = "nba";
        Stn[59, 0] = "んび"; Stn[59, 1] = "nnbi"; Stn[59, 5] = "nbi";
        Stn[60, 0] = "んぶ"; Stn[60, 1] = "nnbu"; Stn[60, 5] = "nbu";
        Stn[61, 0] = "んべ"; Stn[61, 1] = "nnbe"; Stn[61, 5] = "nbe";
        Stn[62, 0] = "んぼ"; Stn[62, 1] = "nnbo"; Stn[62, 5] = "nbo";
        Stn[63, 0] = "んぱ"; Stn[63, 1] = "nnpa"; Stn[63, 5] = "npa";
        Stn[64, 0] = "んぴ"; Stn[64, 1] = "nnpi"; Stn[64, 5] = "npi";
        Stn[65, 0] = "んぷ"; Stn[65, 1] = "nnpu"; Stn[65, 5] = "npu";
        Stn[66, 0] = "んぺ"; Stn[66, 1] = "nnpe"; Stn[66, 5] = "npe";
        Stn[67, 0] = "んぽ"; Stn[67, 1] = "nnpo"; Stn[67, 5] = "npo";
        Stn[68, 0] = "???J";
        Stn[69, 0] = "???L";
        Stn[70, 0] = "???N";
        Stn[71, 0] = "???P";
        Stn[72, 0] = "???R";
        Stn[73, 0] = "???T";
        Stn[74, 0] = "???V";
        Stn[75, 0] = "???X";
        Stn[76, 0] = "???Z";
        Stn[77, 0] = "???";
        Stn[78, 0] = "???^";
        Stn[79, 0] = "???`";
        Stn[80, 0] = "???c";
        Stn[81, 0] = "???e";
        Stn[82, 0] = "???g";
        Stn[83, 0] = "???n";
        Stn[84, 0] = "???q";
        Stn[85, 0] = "???t";
        Stn[86, 0] = "???w";
        Stn[87, 0] = "???z";
        Stn[88, 0] = "???}";
        Stn[89, 0] = "???~";
        Stn[90, 0] = "????";
        Stn[91, 0] = "????";
        Stn[92, 0] = "????";
        Stn[93, 0] = "????";
        Stn[94, 0] = "????";
        Stn[95, 0] = "????";
        Stn[96, 0] = "????";
        Stn[97, 0] = "????";
        Stn[98, 0] = "????";
        Stn[99, 0] = "????";
        Stn[100, 0] = "???@";
        Stn[101, 0] = "???B";
        Stn[102, 0] = "???D";
        Stn[103, 0] = "???F";
        Stn[104, 0] = "???H";
        Stn[105, 0] = "????";
        Stn[106, 0] = "????";
        Stn[107, 0] = "????";
        Stn[108, 0] = "????";
        Stn[109, 0] = "????";
        Stn[110, 0] = "????";
        Stn[111, 0] = "???K";
        Stn[112, 0] = "???M";
        Stn[113, 0] = "???O";
        Stn[114, 0] = "???Q";
        Stn[115, 0] = "???S";
        Stn[116, 0] = "???U";
        Stn[117, 0] = "???W";
        Stn[118, 0] = "???Y";
        Stn[119, 0] = "???[";
        Stn[120, 0] = "???]";
        Stn[121, 0] = "???_";
        Stn[122, 0] = "???a";
        Stn[123, 0] = "???d";
        Stn[124, 0] = "???f";
        Stn[125, 0] = "???h";
        Stn[126, 0] = "???o";
        Stn[127, 0] = "???r";
        Stn[128, 0] = "???u";
        Stn[129, 0] = "???x";
        Stn[130, 0] = "???{";
        Stn[131, 0] = "???p";
        Stn[132, 0] = "???s";
        Stn[133, 0] = "???v";
        Stn[134, 0] = "???y";
        Stn[135, 0] = "???|";
        for (int i = STN_COL / 2; i < STN_COL; i++)
        {
            for (int k = 1; k < STN_ROW; k++)
            {
                Stn[i, k] = Stn[i - (STN_COL / 2), k];
            }
        }
    }
    void SetStnex()
    {
        for (int i = 0; i < STNEX_COL; i++)
        {
            for (int j = 0; j < STNEX_ROW; j++)
            {
                Stnex[i, j] = string.Empty;
            }
        }
        Stnex[0, 0] = "んうぁ"; Stnex[0, 1] = "nnwha"; Stnex[0, 4] = "nwha";
        Stnex[1, 0] = "んうぃ"; Stnex[1, 1] = "nnwi"; Stnex[1, 2] = "nnwhi"; Stnex[1, 4] = "nwi"; Stnex[1, 5] = "nwhi";
        Stnex[2, 0] = "んうぇ"; Stnex[2, 1] = "nnwe"; Stnex[2, 2] = "nnwhe"; Stnex[2, 4] = "nwe"; Stnex[2, 5] = "nwhe";
        Stnex[3, 0] = "んうぉ"; Stnex[3, 1] = "nnwho"; Stnex[3, 4] = "nwho";
        Stnex[4, 0] = "んきぃ"; Stnex[4, 1] = "nnkyi"; Stnex[4, 4] = "nkyi";
        Stnex[5, 0] = "んきぇ"; Stnex[5, 1] = "nnkye"; Stnex[5, 4] = "nkye";
        Stnex[6, 0] = "んきゃ"; Stnex[6, 1] = "nnkya"; Stnex[6, 4] = "nkya";
        Stnex[7, 0] = "んきゅ"; Stnex[7, 1] = "nnkyu"; Stnex[7, 4] = "nkyu";
        Stnex[8, 0] = "んきょ"; Stnex[8, 1] = "nnkyo"; Stnex[8, 4] = "nkyo";
        Stnex[9, 0] = "んぎぃ"; Stnex[9, 1] = "nngyi"; Stnex[9, 4] = "ngyi";
        Stnex[10, 0] = "んぎぇ"; Stnex[10, 1] = "nngye"; Stnex[10, 4] = "ngye";
        Stnex[11, 0] = "んぎゃ"; Stnex[11, 1] = "nngya"; Stnex[11, 4] = "ngya";
        Stnex[12, 0] = "んぎゅ"; Stnex[12, 1] = "nngyu"; Stnex[12, 4] = "ngyu";
        Stnex[13, 0] = "んぎょ"; Stnex[13, 1] = "nngyo"; Stnex[13, 4] = "ngyo";
        Stnex[14, 0] = "んくぁ"; Stnex[14, 1] = "nnqa"; Stnex[14, 2] = "nnqwa"; Stnex[14, 3] = "nnkwa"; Stnex[14, 4] = "nqa"; Stnex[14, 5] = "nqwa"; Stnex[14, 6] = "nkwa";
        Stnex[15, 0] = "んくぃ"; Stnex[15, 1] = "nnqi"; Stnex[15, 2] = "nnqwi"; Stnex[15, 3] = "nnqyi"; Stnex[15, 4] = "nqi"; Stnex[15, 5] = "nqwi"; Stnex[15, 6] = "nqyi";
        Stnex[16, 0] = "んくぅ"; Stnex[16, 1] = "nnqwu"; Stnex[16, 4] = "nqwu";
        Stnex[17, 0] = "んくぇ"; Stnex[17, 1] = "nnqe"; Stnex[17, 2] = "nnqwe"; Stnex[17, 3] = "nnqye"; Stnex[17, 4] = "nqe"; Stnex[17, 5] = "nqwe"; Stnex[17, 6] = "nqye";
        Stnex[18, 0] = "んくぉ"; Stnex[18, 1] = "nnqo"; Stnex[18, 2] = "nnqwo"; Stnex[18, 4] = "nqo"; Stnex[18, 5] = "nqwo";
        Stnex[19, 0] = "んくゃ"; Stnex[19, 1] = "nnqya"; Stnex[19, 4] = "nqya";
        Stnex[20, 0] = "んくゅ"; Stnex[20, 1] = "nnqyu"; Stnex[20, 4] = "nqyu";
        Stnex[21, 0] = "んくょ"; Stnex[21, 1] = "nnqyo"; Stnex[21, 4] = "nqyo";
        Stnex[22, 0] = "んぐぁ"; Stnex[22, 1] = "nngwa"; Stnex[22, 4] = "ngwa";
        Stnex[23, 0] = "んぐぃ"; Stnex[23, 1] = "nngwi"; Stnex[23, 4] = "ngwi";
        Stnex[24, 0] = "んぐぅ"; Stnex[24, 1] = "nngwu"; Stnex[24, 4] = "ngwu";
        Stnex[25, 0] = "んぐぇ"; Stnex[25, 1] = "nngwe"; Stnex[25, 4] = "ngwe";
        Stnex[26, 0] = "んぐぉ"; Stnex[26, 1] = "nngwo"; Stnex[26, 4] = "ngwo";
        Stnex[27, 0] = "んしぃ"; Stnex[27, 1] = "nnsyi"; Stnex[27, 4] = "nsyi";
        Stnex[28, 0] = "んしぇ"; Stnex[28, 1] = "nnsye"; Stnex[28, 2] = "nnshe"; Stnex[28, 4] = "nsye"; Stnex[28, 5] = "nshe";
        Stnex[29, 0] = "んしゃ"; Stnex[29, 1] = "nnsya"; Stnex[29, 2] = "nnsha"; Stnex[29, 4] = "nsya"; Stnex[29, 5] = "nsha";
        Stnex[30, 0] = "んしゅ"; Stnex[30, 1] = "nnsyu"; Stnex[30, 2] = "nnshu"; Stnex[30, 4] = "nsyu"; Stnex[30, 5] = "nshu";
        Stnex[31, 0] = "んしょ"; Stnex[31, 1] = "nnsyo"; Stnex[31, 2] = "nnsho"; Stnex[31, 4] = "nsyo"; Stnex[31, 5] = "nsho";
        Stnex[32, 0] = "んじぃ"; Stnex[32, 1] = "nnjyi"; Stnex[32, 2] = "nnzyi"; Stnex[32, 4] = "njyi"; Stnex[32, 5] = "nzyi";
        Stnex[33, 0] = "んじぇ"; Stnex[33, 1] = "nnje"; Stnex[33, 2] = "nnjye"; Stnex[33, 3] = "nnzye"; Stnex[33, 4] = "nje"; Stnex[33, 5] = "njye"; Stnex[33, 6] = "nzye";
        Stnex[34, 0] = "んじゃ"; Stnex[34, 1] = "nnja"; Stnex[34, 2] = "nnjya"; Stnex[34, 3] = "nnzya"; Stnex[34, 4] = "nja"; Stnex[34, 5] = "njya"; Stnex[34, 6] = "nzya";
        Stnex[35, 0] = "んじゅ"; Stnex[35, 1] = "nnju"; Stnex[35, 2] = "nnjyu"; Stnex[35, 3] = "nnzyu"; Stnex[35, 4] = "nju"; Stnex[35, 5] = "njyu"; Stnex[35, 6] = "nzyu";
        Stnex[36, 0] = "んじょ"; Stnex[36, 1] = "nnjo"; Stnex[36, 2] = "nnjyo"; Stnex[36, 3] = "nnzyo"; Stnex[36, 4] = "njo"; Stnex[36, 5] = "njyo"; Stnex[36, 6] = "nzyo";
        Stnex[37, 0] = "んすぁ"; Stnex[37, 1] = "nnswa"; Stnex[37, 4] = "nswa";
        Stnex[38, 0] = "んすぃ"; Stnex[38, 1] = "nnswi"; Stnex[38, 4] = "nswi";
        Stnex[39, 0] = "んすぅ"; Stnex[39, 1] = "nnswu"; Stnex[39, 4] = "nswu";
        Stnex[40, 0] = "んすぇ"; Stnex[40, 1] = "nnswe"; Stnex[40, 4] = "nswe";
        Stnex[41, 0] = "んすぉ"; Stnex[41, 1] = "nnswo"; Stnex[41, 4] = "nswo";
        Stnex[42, 0] = "んちぃ"; Stnex[42, 1] = "nntyi"; Stnex[42, 2] = "nncyi"; Stnex[42, 4] = "ntyi"; Stnex[42, 5] = "ncyi";
        Stnex[43, 0] = "んちぇ"; Stnex[43, 1] = "nntye"; Stnex[43, 2] = "nncye"; Stnex[43, 3] = "nnche"; Stnex[43, 4] = "ntye"; Stnex[43, 5] = "ncye"; Stnex[43, 6] = "nche";
        Stnex[44, 0] = "んちゃ"; Stnex[44, 1] = "nntya"; Stnex[44, 2] = "nncya"; Stnex[44, 3] = "nncha"; Stnex[44, 4] = "ntya"; Stnex[44, 5] = "ncya"; Stnex[44, 6] = "ncha";
        Stnex[45, 0] = "んちゅ"; Stnex[45, 1] = "nntyu"; Stnex[45, 2] = "nncyu"; Stnex[45, 3] = "nnchu"; Stnex[45, 4] = "ntyu"; Stnex[45, 5] = "ncyu"; Stnex[45, 6] = "nchu";
        Stnex[46, 0] = "んちょ"; Stnex[46, 1] = "nntyo"; Stnex[46, 2] = "nncyo"; Stnex[46, 3] = "nncho"; Stnex[46, 4] = "ntyo"; Stnex[46, 5] = "ncyo"; Stnex[46, 6] = "ncho";
        Stnex[47, 0] = "んぢぃ"; Stnex[47, 1] = "nndyi"; Stnex[47, 4] = "ndyi";
        Stnex[48, 0] = "んぢぇ"; Stnex[48, 1] = "nndye"; Stnex[48, 4] = "ndye";
        Stnex[49, 0] = "んぢゃ"; Stnex[49, 1] = "nndya"; Stnex[49, 4] = "ndya";
        Stnex[50, 0] = "んぢゅ"; Stnex[50, 1] = "nndyu"; Stnex[50, 4] = "ndyu";
        Stnex[51, 0] = "んぢょ"; Stnex[51, 1] = "nndyo"; Stnex[51, 4] = "ndyo";
        Stnex[52, 0] = "んつぁ"; Stnex[52, 1] = "nntsa"; Stnex[52, 4] = "ntsa";
        Stnex[53, 0] = "んつぃ"; Stnex[53, 1] = "nntsi"; Stnex[53, 4] = "ntsi";
        Stnex[54, 0] = "んつぇ"; Stnex[54, 1] = "nntse"; Stnex[54, 4] = "ntse";
        Stnex[55, 0] = "んつぉ"; Stnex[55, 1] = "nntso"; Stnex[55, 4] = "ntso";
        Stnex[56, 0] = "んてぃ"; Stnex[56, 1] = "nnthi"; Stnex[56, 4] = "nthi";
        Stnex[57, 0] = "んてぇ"; Stnex[57, 1] = "nnthe"; Stnex[57, 4] = "nthe";
        Stnex[58, 0] = "んてゃ"; Stnex[58, 1] = "nntha"; Stnex[58, 4] = "ntha";
        Stnex[59, 0] = "んてゅ"; Stnex[59, 1] = "nnthu"; Stnex[59, 4] = "nthu";
        Stnex[60, 0] = "んてょ"; Stnex[60, 1] = "nntho"; Stnex[60, 4] = "ntho";
        Stnex[61, 0] = "んでぃ"; Stnex[61, 1] = "nndhi"; Stnex[61, 4] = "ndhi";
        Stnex[62, 0] = "んでぇ"; Stnex[62, 1] = "nndhe"; Stnex[62, 4] = "ndhe";
        Stnex[63, 0] = "んでゃ"; Stnex[63, 1] = "nndha"; Stnex[63, 4] = "ndha";
        Stnex[64, 0] = "んでゅ"; Stnex[64, 1] = "nndhu"; Stnex[64, 4] = "ndhu";
        Stnex[65, 0] = "んでょ"; Stnex[65, 1] = "nndho"; Stnex[65, 4] = "ndho";
        Stnex[66, 0] = "んとぁ"; Stnex[66, 1] = "nntwa"; Stnex[66, 4] = "ntwa";
        Stnex[67, 0] = "んとぃ"; Stnex[67, 1] = "nntwi"; Stnex[67, 4] = "ntwi";
        Stnex[68, 0] = "んとぅ"; Stnex[68, 1] = "nntwu"; Stnex[68, 2] = "nntoxu"; Stnex[68, 4] = "ntwu"; Stnex[68, 5] = "ntoxu";
        Stnex[69, 0] = "んとぇ"; Stnex[69, 1] = "nntwe"; Stnex[69, 4] = "ntwe";
        Stnex[70, 0] = "んとぉ"; Stnex[70, 1] = "nntwo"; Stnex[70, 4] = "ntwo";
        Stnex[71, 0] = "んどぁ"; Stnex[71, 1] = "nndwa"; Stnex[71, 4] = "ndwa";
        Stnex[72, 0] = "んどぃ"; Stnex[72, 1] = "nndwi"; Stnex[72, 4] = "ndwi";
        Stnex[73, 0] = "んどぅ"; Stnex[73, 1] = "nndwu"; Stnex[73, 4] = "ndwu";
        Stnex[74, 0] = "んどぇ"; Stnex[74, 1] = "nndwe"; Stnex[74, 4] = "ndwe";
        Stnex[75, 0] = "んどぉ"; Stnex[75, 1] = "nndwo"; Stnex[75, 4] = "ndwo";
        Stnex[76, 0] = "んひぃ"; Stnex[76, 1] = "nnhyi"; Stnex[76, 4] = "nhyi";
        Stnex[77, 0] = "んひぇ"; Stnex[77, 1] = "nnhye"; Stnex[77, 4] = "nhye";
        Stnex[78, 0] = "んひゃ"; Stnex[78, 1] = "nnhya"; Stnex[78, 4] = "nhya";
        Stnex[79, 0] = "んひゅ"; Stnex[79, 1] = "nnhyu"; Stnex[79, 4] = "nhyu";
        Stnex[80, 0] = "んひょ"; Stnex[80, 1] = "nnhyo"; Stnex[80, 4] = "nhyo";
        Stnex[81, 0] = "んびぃ"; Stnex[81, 1] = "nnbyi"; Stnex[81, 4] = "nbyi";
        Stnex[82, 0] = "んびぇ"; Stnex[82, 1] = "nnbye"; Stnex[82, 4] = "nbye";
        Stnex[83, 0] = "んびゃ"; Stnex[83, 1] = "nnbya"; Stnex[83, 4] = "nbya";
        Stnex[84, 0] = "んびゅ"; Stnex[84, 1] = "nnbyu"; Stnex[84, 4] = "nbyu";
        Stnex[85, 0] = "んびょ"; Stnex[85, 1] = "nnbyo"; Stnex[85, 4] = "nbyo";
        Stnex[86, 0] = "んぴぃ"; Stnex[86, 1] = "nnpyi"; Stnex[86, 4] = "npyi";
        Stnex[87, 0] = "んぴぇ"; Stnex[87, 1] = "nnpye"; Stnex[87, 4] = "npye";
        Stnex[88, 0] = "んぴゃ"; Stnex[88, 1] = "nnpya"; Stnex[88, 4] = "npya";
        Stnex[89, 0] = "んぴゅ"; Stnex[89, 1] = "nnpyu"; Stnex[89, 4] = "npyu";
        Stnex[90, 0] = "んぴょ"; Stnex[90, 1] = "nnpyo"; Stnex[90, 4] = "npyo";
        Stnex[91, 0] = "んふぁ"; Stnex[91, 1] = "nnfa"; Stnex[91, 2] = "nnfwa"; Stnex[91, 4] = "nfa"; Stnex[91, 5] = "nfwa";
        Stnex[92, 0] = "んふぃ"; Stnex[92, 1] = "nnfi"; Stnex[92, 2] = "nnfyi"; Stnex[92, 3] = "nnfwi"; Stnex[92, 4] = "nfi"; Stnex[92, 5] = "nfyi"; Stnex[92, 6] = "nfwi";
        Stnex[93, 0] = "んふぅ"; Stnex[93, 1] = "nnfwu"; Stnex[93, 4] = "nfwu";
        Stnex[94, 0] = "んふぇ"; Stnex[94, 1] = "nnfe"; Stnex[94, 2] = "nnfye"; Stnex[94, 3] = "nnfwe"; Stnex[94, 4] = "nfe"; Stnex[94, 5] = "nfye"; Stnex[94, 6] = "nfwe";
        Stnex[95, 0] = "んふぉ"; Stnex[95, 1] = "nnfo"; Stnex[95, 2] = "nnfwo"; Stnex[95, 4] = "nfo"; Stnex[95, 5] = "nfwo";
        Stnex[96, 0] = "んふゃ"; Stnex[96, 1] = "nnfya"; Stnex[96, 4] = "nfya";
        Stnex[97, 0] = "んふゅ"; Stnex[97, 1] = "nnfyu"; Stnex[97, 4] = "nfyu";
        Stnex[98, 0] = "んふょ"; Stnex[98, 1] = "nnfyo"; Stnex[98, 4] = "nfyo";
        Stnex[99, 0] = "んみぃ"; Stnex[99, 1] = "nnmyi"; Stnex[99, 4] = "nmyi";
        Stnex[100, 0] = "んみぇ"; Stnex[100, 1] = "nnmye"; Stnex[100, 4] = "nmye";
        Stnex[101, 0] = "んみゃ"; Stnex[101, 1] = "nnmya"; Stnex[101, 4] = "nmya";
        Stnex[102, 0] = "んみゅ"; Stnex[102, 1] = "nnmyu"; Stnex[102, 4] = "nmyu";
        Stnex[103, 0] = "んみょ"; Stnex[103, 1] = "nnmyo"; Stnex[103, 4] = "nmyo";
        Stnex[104, 0] = "んりぃ"; Stnex[104, 1] = "nnryi"; Stnex[104, 4] = "nryi";
        Stnex[105, 0] = "んりぇ"; Stnex[105, 1] = "nnrye"; Stnex[105, 4] = "nrye";
        Stnex[106, 0] = "んりゃ"; Stnex[106, 1] = "nnrya"; Stnex[106, 4] = "nrya";
        Stnex[107, 0] = "んりゅ"; Stnex[107, 1] = "nnryu"; Stnex[107, 4] = "nryu";
        Stnex[108, 0] = "んりょ"; Stnex[108, 1] = "nnryo"; Stnex[108, 4] = "nryo";
        Stnex[109, 0] = "んヴぁ"; Stnex[109, 1] = "nnva"; Stnex[109, 4] = "nva";
        Stnex[110, 0] = "んヴぃ"; Stnex[110, 1] = "nnvi"; Stnex[110, 2] = "nnvyi"; Stnex[110, 4] = "nvi"; Stnex[110, 5] = "nvyi";
        Stnex[111, 0] = "んヴぇ"; Stnex[111, 1] = "nnve"; Stnex[111, 2] = "nnvye"; Stnex[111, 4] = "nve"; Stnex[111, 5] = "nvye";
        Stnex[112, 0] = "んヴぉ"; Stnex[112, 1] = "nnvo"; Stnex[112, 4] = "nvo";
        Stnex[113, 0] = "んヴゃ"; Stnex[113, 1] = "nnvya"; Stnex[113, 4] = "nvya";
        Stnex[114, 0] = "んヴゅ"; Stnex[114, 1] = "nnvyu"; Stnex[114, 4] = "nvyu";
        Stnex[115, 0] = "んヴょ"; Stnex[115, 1] = "nnvyo"; Stnex[115, 4] = "nvyo";
        Stnex[116, 0] = "???E?@";
        Stnex[117, 0] = "???E?B";
        Stnex[118, 0] = "???E?F";
        Stnex[119, 0] = "???E?H";
        Stnex[120, 0] = "???L?B";
        Stnex[121, 0] = "???L?F";
        Stnex[122, 0] = "???L??";
        Stnex[123, 0] = "???L??";
        Stnex[124, 0] = "???L??";
        Stnex[125, 0] = "???M?B";
        Stnex[126, 0] = "???M?F";
        Stnex[127, 0] = "???M??";
        Stnex[128, 0] = "???M??";
        Stnex[129, 0] = "???M??";
        Stnex[130, 0] = "???N?@";
        Stnex[131, 0] = "???N?B";
        Stnex[132, 0] = "???N?D";
        Stnex[133, 0] = "???N?F";
        Stnex[134, 0] = "???N?H";
        Stnex[135, 0] = "???N??";
        Stnex[136, 0] = "???N??";
        Stnex[137, 0] = "???N??";
        Stnex[138, 0] = "???O?@";
        Stnex[139, 0] = "???O?B";
        Stnex[140, 0] = "???O?D";
        Stnex[141, 0] = "???O?F";
        Stnex[142, 0] = "???O?H";
        Stnex[143, 0] = "???V?B";
        Stnex[144, 0] = "???V?F";
        Stnex[145, 0] = "???V??";
        Stnex[146, 0] = "???V??";
        Stnex[147, 0] = "???V??";
        Stnex[148, 0] = "???W?B";
        Stnex[149, 0] = "???W?F";
        Stnex[150, 0] = "???W??";
        Stnex[151, 0] = "???W??";
        Stnex[152, 0] = "???W??";
        Stnex[153, 0] = "???X?@";
        Stnex[154, 0] = "???X?B";
        Stnex[155, 0] = "???X?D";
        Stnex[156, 0] = "???X?F";
        Stnex[157, 0] = "???X?H";
        Stnex[158, 0] = "???`?B";
        Stnex[159, 0] = "???`?F";
        Stnex[160, 0] = "???`??";
        Stnex[161, 0] = "???`??";
        Stnex[162, 0] = "???`??";
        Stnex[163, 0] = "???a?B";
        Stnex[164, 0] = "???a?F";
        Stnex[165, 0] = "???a??";
        Stnex[166, 0] = "???a??";
        Stnex[167, 0] = "???a??";
        Stnex[168, 0] = "???c?@";
        Stnex[169, 0] = "???c?B";
        Stnex[170, 0] = "???c?F";
        Stnex[171, 0] = "???c?H";
        Stnex[172, 0] = "???e?B";
        Stnex[173, 0] = "???e?F";
        Stnex[174, 0] = "???e??";
        Stnex[175, 0] = "???e??";
        Stnex[176, 0] = "???e??";
        Stnex[177, 0] = "???f?B";
        Stnex[178, 0] = "???f?F";
        Stnex[179, 0] = "???f??";
        Stnex[180, 0] = "???f??";
        Stnex[181, 0] = "???f??";
        Stnex[182, 0] = "???g?@";
        Stnex[183, 0] = "???g?B";
        Stnex[184, 0] = "???g?D";
        Stnex[185, 0] = "???g?F";
        Stnex[186, 0] = "???g?H";
        Stnex[187, 0] = "???h?@";
        Stnex[188, 0] = "???h?B";
        Stnex[189, 0] = "???h?D";
        Stnex[190, 0] = "???h?F";
        Stnex[191, 0] = "???h?H";
        Stnex[192, 0] = "???q?B";
        Stnex[193, 0] = "???q?F";
        Stnex[194, 0] = "???q??";
        Stnex[195, 0] = "???q??";
        Stnex[196, 0] = "???q??";
        Stnex[197, 0] = "???r?B";
        Stnex[198, 0] = "???r?F";
        Stnex[199, 0] = "???r??";
        Stnex[200, 0] = "???r??";
        Stnex[201, 0] = "???r??";
        Stnex[202, 0] = "???s?B";
        Stnex[203, 0] = "???s?F";
        Stnex[204, 0] = "???s??";
        Stnex[205, 0] = "???s??";
        Stnex[206, 0] = "???s??";
        Stnex[207, 0] = "???t?@";
        Stnex[208, 0] = "???t?B";
        Stnex[209, 0] = "???t?D";
        Stnex[210, 0] = "???t?F";
        Stnex[211, 0] = "???t?H";
        Stnex[212, 0] = "???t??";
        Stnex[213, 0] = "???t??";
        Stnex[214, 0] = "???t??";
        Stnex[215, 0] = "???~?B";
        Stnex[216, 0] = "???~?F";
        Stnex[217, 0] = "???~??";
        Stnex[218, 0] = "???~??";
        Stnex[219, 0] = "???~??";
        Stnex[220, 0] = "?????B";
        Stnex[221, 0] = "?????F";
        Stnex[222, 0] = "";
        Stnex[223, 0] = "";
        Stnex[224, 0] = "";
        Stnex[225, 0] = "?????@";
        Stnex[226, 0] = "?????B";
        Stnex[227, 0] = "?????F";
        Stnex[228, 0] = "?????H";
        Stnex[229, 0] = "";
        Stnex[230, 0] = "";
        Stnex[231, 0] = "";
        for (int i = STNEX_COL / 2; i < STNEX_COL; i++)
        {
            for (int k = 1; k < STNEX_ROW; k++)
            {
                Stnex[i, k] = Stnex[i - (STNEX_COL / 2), k];
            }
        }
    }
    void SetStnt()
    {
        for (int i = 0; i < STNT_COL; i++)
        {
            for (int j = 0; j < STNT_ROW; j++)
            {
                Stnt[i, j] = string.Empty;
            }
        }
        Stnt[0, 0] = "んっか"; Stnt[0, 1] = "nnkka"; Stnt[0, 2] = "nncca"; Stnt[0, 3] = "nnltuka"; Stnt[0, 4] = "nnltuca"; Stnt[0, 5] = "nkka"; Stnt[0, 6] = "ncca"; Stnt[0, 7] = "nltuka"; Stnt[0, 8] = "nltuca";
        Stnt[1, 0] = "んっき"; Stnt[1, 1] = "nnkki"; Stnt[1, 2] = "nnltuki"; Stnt[1, 3] = "nnkki"; Stnt[1, 4] = "nnltuki"; Stnt[1, 5] = "nkki"; Stnt[1, 6] = "nltuki"; Stnt[1, 7] = "nkki"; Stnt[1, 8] = "nltuki";
        Stnt[2, 0] = "んっく"; Stnt[2, 1] = "nnkku"; Stnt[2, 2] = "nnccu"; Stnt[2, 3] = "nnqqu"; Stnt[2, 4] = "nnltuku"; Stnt[2, 5] = "nnltucu"; Stnt[2, 6] = "nkku"; Stnt[2, 7] = "nccu"; Stnt[2, 8] = "nqqu"; Stnt[2, 9] = "nltuku"; Stnt[2, 10] = "nltucu";
        Stnt[3, 0] = "んっけ"; Stnt[3, 1] = "nnkke"; Stnt[3, 2] = "nnltuke"; Stnt[3, 3] = "nkke"; Stnt[3, 4] = "nltuke";
        Stnt[4, 0] = "んっこ"; Stnt[4, 1] = "nnkko"; Stnt[4, 2] = "nncco"; Stnt[4, 3] = "nnltuko"; Stnt[4, 4] = "nnltuco"; Stnt[4, 5] = "nkko"; Stnt[4, 6] = "ncco"; Stnt[4, 7] = "nltuko"; Stnt[4, 8] = "nltuco";
        Stnt[5, 0] = "んっさ"; Stnt[5, 1] = "nnssa"; Stnt[5, 2] = "nnltusa"; Stnt[5, 3] = "nssa"; Stnt[5, 4] = "nltusa";
        Stnt[6, 0] = "んっし"; Stnt[6, 1] = "nnssi"; Stnt[6, 2] = "nnsshi"; Stnt[6, 3] = "nncci"; Stnt[6, 4] = "nnltusi"; Stnt[6, 5] = "nnltushi"; Stnt[6, 6] = "nssi"; Stnt[6, 7] = "nsshi"; Stnt[6, 8] = "ncci"; Stnt[6, 9] = "nltusi"; Stnt[6, 10] = "nltushi";
        Stnt[7, 0] = "んっす"; Stnt[7, 1] = "nnssu"; Stnt[7, 2] = "nnltusu"; Stnt[7, 3] = "nssu"; Stnt[7, 4] = "nltusu";
        Stnt[8, 0] = "んっせ"; Stnt[8, 1] = "nnsse"; Stnt[8, 2] = "nncce"; Stnt[8, 3] = "nnltuse"; Stnt[8, 4] = "nnltuce"; Stnt[8, 5] = "nsse"; Stnt[8, 6] = "ncce"; Stnt[8, 7] = "nltuse"; Stnt[8, 8] = "nltuce";
        Stnt[9, 0] = "んっそ"; Stnt[9, 1] = "nnsso"; Stnt[9, 2] = "nnltuso"; Stnt[9, 3] = "nsso"; Stnt[9, 4] = "nltuso";
        Stnt[10, 0] = "んった"; Stnt[10, 1] = "nntta"; Stnt[10, 2] = "nnltuta"; Stnt[10, 3] = "ntta"; Stnt[10, 4] = "nltuta";
        Stnt[11, 0] = "んっち"; Stnt[11, 1] = "nntti"; Stnt[11, 2] = "nncchi"; Stnt[11, 3] = "nnltuti"; Stnt[11, 4] = "nnltuchi"; Stnt[11, 5] = "ntti"; Stnt[11, 6] = "ncchi"; Stnt[11, 7] = "nltuti"; Stnt[11, 8] = "nltuchi";
        Stnt[12, 0] = "んっつ"; Stnt[12, 1] = "nnttu"; Stnt[12, 2] = "nnttsu"; Stnt[12, 3] = "nnltutu"; Stnt[12, 4] = "nnltutsu"; Stnt[12, 5] = "nttu"; Stnt[12, 6] = "nttsu"; Stnt[12, 7] = "nltutu"; Stnt[12, 8] = "nltutsu";
        Stnt[13, 0] = "んって"; Stnt[13, 1] = "nntte"; Stnt[13, 2] = "nnltute"; Stnt[13, 3] = "ntte"; Stnt[13, 4] = "nltute";
        Stnt[14, 0] = "んっと"; Stnt[14, 1] = "nntto"; Stnt[14, 2] = "nnltuto"; Stnt[14, 3] = "ntto"; Stnt[14, 4] = "nltuto";
        Stnt[15, 0] = "んっは"; Stnt[15, 1] = "nnhha"; Stnt[15, 2] = "nnltuha"; Stnt[15, 3] = "nhha"; Stnt[15, 4] = "nltuha";
        Stnt[16, 0] = "んっひ"; Stnt[16, 1] = "nnhhi"; Stnt[16, 2] = "nnltuhi"; Stnt[16, 3] = "nhhi"; Stnt[16, 4] = "nltuhi";
        Stnt[17, 0] = "んっふ"; Stnt[17, 1] = "nnhhu"; Stnt[17, 2] = "nnffu"; Stnt[17, 3] = "nnltuhu"; Stnt[17, 4] = "nnltufu"; Stnt[17, 5] = "nhhu"; Stnt[17, 6] = "nffu"; Stnt[17, 7] = "nltuhu"; Stnt[17, 8] = "nltufu";
        Stnt[18, 0] = "んっへ"; Stnt[18, 1] = "nnhhe"; Stnt[18, 2] = "nnltuhe"; Stnt[18, 3] = "nhhe"; Stnt[18, 4] = "nltuhe";
        Stnt[19, 0] = "んっほ"; Stnt[19, 1] = "nnhho"; Stnt[19, 2] = "nnltuho"; Stnt[19, 3] = "nhho"; Stnt[19, 4] = "nltuho";
        Stnt[20, 0] = "んっま"; Stnt[20, 1] = "nnmma"; Stnt[20, 2] = "nnltuma"; Stnt[20, 3] = "nmma"; Stnt[20, 4] = "nltuma";
        Stnt[21, 0] = "んっみ"; Stnt[21, 1] = "nnmmi"; Stnt[21, 2] = "nnltumi"; Stnt[21, 3] = "nmmi"; Stnt[21, 4] = "nltumi";
        Stnt[22, 0] = "んっむ"; Stnt[22, 1] = "nnmmu"; Stnt[22, 2] = "nnltumu"; Stnt[22, 3] = "nmmu"; Stnt[22, 4] = "nltumu";
        Stnt[23, 0] = "んっめ"; Stnt[23, 1] = "nnmme"; Stnt[23, 2] = "nnltume"; Stnt[23, 3] = "nmme"; Stnt[23, 4] = "nltume";
        Stnt[24, 0] = "んっも"; Stnt[24, 1] = "nnmmo"; Stnt[24, 2] = "nnltumo"; Stnt[24, 3] = "nmmo"; Stnt[24, 4] = "nltumo";
        Stnt[25, 0] = "んっや"; Stnt[25, 1] = "nnyya"; Stnt[25, 2] = "nnltuya"; Stnt[25, 3] = "nyya"; Stnt[25, 4] = "nltuya";
        Stnt[26, 0] = "んっゆ"; Stnt[26, 1] = "nnyyu"; Stnt[26, 2] = "nnltuyu"; Stnt[26, 3] = "nyyu"; Stnt[26, 4] = "nltuyu";
        Stnt[27, 0] = "んっよ"; Stnt[27, 1] = "nnyyo"; Stnt[27, 2] = "nnltuyo"; Stnt[27, 3] = "nyyo"; Stnt[27, 4] = "nltuyo";
        Stnt[28, 0] = "んっら"; Stnt[28, 1] = "nnrra"; Stnt[28, 2] = "nnltura"; Stnt[28, 3] = "nrra"; Stnt[28, 4] = "nltura";
        Stnt[29, 0] = "んっり"; Stnt[29, 1] = "nnrri"; Stnt[29, 2] = "nnlturi"; Stnt[29, 3] = "nrri"; Stnt[29, 4] = "nlturi";
        Stnt[30, 0] = "んっる"; Stnt[30, 1] = "nnrru"; Stnt[30, 2] = "nnlturu"; Stnt[30, 3] = "nrru"; Stnt[30, 4] = "nlturu";
        Stnt[31, 0] = "んっれ"; Stnt[31, 1] = "nnrre"; Stnt[31, 2] = "nnlture"; Stnt[31, 3] = "nrre"; Stnt[31, 4] = "nlture";
        Stnt[32, 0] = "んっろ"; Stnt[32, 1] = "nnrro"; Stnt[32, 2] = "nnlturo"; Stnt[32, 3] = "nrro"; Stnt[32, 4] = "nlturo";
        Stnt[33, 0] = "んっわ"; Stnt[33, 1] = "nnwwa"; Stnt[33, 2] = "nnltuwa"; Stnt[33, 3] = "nwwa"; Stnt[33, 4] = "nltuwa";
        Stnt[34, 0] = "んっを"; Stnt[34, 1] = "nnwwo"; Stnt[34, 2] = "nnltuwo"; Stnt[34, 3] = "nwwo"; Stnt[34, 4] = "nltuwo";
        Stnt[35, 0] = "んっぁ"; Stnt[35, 1] = "nnlla"; Stnt[35, 2] = "nnxxa"; Stnt[35, 3] = "nnltula"; Stnt[35, 4] = "nlla"; Stnt[35, 5] = "nxxa"; Stnt[35, 6] = "nltula";
        Stnt[36, 0] = "んっぃ"; Stnt[36, 1] = "nnlli"; Stnt[36, 2] = "nnxxi"; Stnt[36, 3] = "nnllyi"; Stnt[36, 4] = "nnxxyi"; Stnt[36, 5] = "nnltuli"; Stnt[36, 6] = "nlli"; Stnt[36, 7] = "nxxi"; Stnt[36, 8] = "nllyi"; Stnt[36, 9] = "nxxyi"; Stnt[36, 10] = "nltuli";
        Stnt[37, 0] = "んっぅ"; Stnt[37, 1] = "nnllu"; Stnt[37, 2] = "nnxxu"; Stnt[37, 3] = "nnltulu"; Stnt[37, 4] = "nllu"; Stnt[37, 5] = "nxxu"; Stnt[37, 6] = "nltulu";
        Stnt[38, 0] = "んっぇ"; Stnt[38, 1] = "nnlle"; Stnt[38, 2] = "nnxxe"; Stnt[38, 3] = "nnllye"; Stnt[38, 4] = "nnxxye"; Stnt[38, 5] = "nnltule"; Stnt[38, 6] = "nlle"; Stnt[38, 7] = "nxxe"; Stnt[38, 8] = "nllye"; Stnt[38, 9] = "nxxye"; Stnt[38, 10] = "nltule";
        Stnt[39, 0] = "んっぉ"; Stnt[39, 1] = "nnllo"; Stnt[39, 2] = "nnxxo"; Stnt[39, 3] = "nnltulo"; Stnt[39, 4] = "nllo"; Stnt[39, 5] = "nxxo"; Stnt[39, 6] = "nltulo";
        Stnt[40, 0] = "んっっ"; Stnt[40, 1] = "nnlltu"; Stnt[40, 2] = "nnxxtu"; Stnt[40, 3] = "nnlltsu"; Stnt[40, 4] = "nnltultu"; Stnt[40, 5] = "nlltu"; Stnt[40, 6] = "nxxtu"; Stnt[40, 7] = "nlltsu"; Stnt[40, 8] = "nltultu";
        Stnt[41, 0] = "んっゃ"; Stnt[41, 1] = "nnllya"; Stnt[41, 2] = "nnxxya"; Stnt[41, 3] = "nnltulya"; Stnt[41, 4] = "nllya"; Stnt[41, 5] = "nxxya"; Stnt[41, 6] = "nltulya";
        Stnt[42, 0] = "んっゅ"; Stnt[42, 1] = "nnllyu"; Stnt[42, 2] = "nnxxyu"; Stnt[42, 3] = "nnltulyu"; Stnt[42, 4] = "nllyu"; Stnt[42, 5] = "nxxyu"; Stnt[42, 6] = "nltulyu";
        Stnt[43, 0] = "んっょ"; Stnt[43, 1] = "nnllyo"; Stnt[43, 2] = "nnxxyo"; Stnt[43, 3] = "nnltulyo"; Stnt[43, 4] = "nllyo"; Stnt[43, 5] = "nxxyo"; Stnt[43, 6] = "nltulyo";
        Stnt[44, 0] = "んっゎ"; Stnt[44, 1] = "nnllwa"; Stnt[44, 2] = "nnxxwa"; Stnt[44, 3] = "nnltulwa"; Stnt[44, 4] = "nllwa"; Stnt[44, 5] = "nxxwa"; Stnt[44, 6] = "nltulwa";
        Stnt[45, 0] = "んっヵ"; Stnt[45, 1] = "nnllka"; Stnt[45, 2] = "nnxxka"; Stnt[45, 3] = "nnltulka"; Stnt[45, 4] = "nllka"; Stnt[45, 5] = "nxxka"; Stnt[45, 6] = "nltulka";
        Stnt[46, 0] = "んっヶ"; Stnt[46, 1] = "nnllke"; Stnt[46, 2] = "nnxxke"; Stnt[46, 3] = "nnltulke"; Stnt[46, 4] = "nllke"; Stnt[46, 5] = "nxxke"; Stnt[46, 6] = "nltulke";
        Stnt[47, 0] = "んっが"; Stnt[47, 1] = "nngga"; Stnt[47, 2] = "nnltuga"; Stnt[47, 3] = "ngga"; Stnt[47, 4] = "nltuga";
        Stnt[48, 0] = "んっぎ"; Stnt[48, 1] = "nnggi"; Stnt[48, 2] = "nnltugi"; Stnt[48, 3] = "nggi"; Stnt[48, 4] = "nltugi";
        Stnt[49, 0] = "んっぐ"; Stnt[49, 1] = "nnggu"; Stnt[49, 2] = "nnltugu"; Stnt[49, 3] = "nggu"; Stnt[49, 4] = "nltugu";
        Stnt[50, 0] = "んっげ"; Stnt[50, 1] = "nngge"; Stnt[50, 2] = "nnltuge"; Stnt[50, 3] = "ngge"; Stnt[50, 4] = "nltuge";
        Stnt[51, 0] = "んっご"; Stnt[51, 1] = "nnggo"; Stnt[51, 2] = "nnltugo"; Stnt[51, 3] = "nggo"; Stnt[51, 4] = "nltugo";
        Stnt[52, 0] = "んっざ"; Stnt[52, 1] = "nnzza"; Stnt[52, 2] = "nnltuza"; Stnt[52, 3] = "nzza"; Stnt[52, 4] = "nltuza";
        Stnt[53, 0] = "んっじ"; Stnt[53, 1] = "nnjji"; Stnt[53, 2] = "nnzzi"; Stnt[53, 3] = "nnltuji"; Stnt[53, 4] = "nnltuzi"; Stnt[53, 5] = "njji"; Stnt[53, 6] = "nzzi"; Stnt[53, 7] = "nltuji"; Stnt[53, 8] = "nltuzi";
        Stnt[54, 0] = "んっず"; Stnt[54, 1] = "nnzzu"; Stnt[54, 2] = "nnltuzu"; Stnt[54, 3] = "nzzu"; Stnt[54, 4] = "nltuzu";
        Stnt[55, 0] = "んっぜ"; Stnt[55, 1] = "nnzze"; Stnt[55, 2] = "nnltuze"; Stnt[55, 3] = "nzze"; Stnt[55, 4] = "nltuze";
        Stnt[56, 0] = "んっぞ"; Stnt[56, 1] = "nnzzo"; Stnt[56, 2] = "nnltuzo"; Stnt[56, 3] = "nzzo"; Stnt[56, 4] = "nltuzo";
        Stnt[57, 0] = "んっだ"; Stnt[57, 1] = "nndda"; Stnt[57, 2] = "nnltuda"; Stnt[57, 3] = "ndda"; Stnt[57, 4] = "nltuda";
        Stnt[58, 0] = "んっぢ"; Stnt[58, 1] = "nnddi"; Stnt[58, 2] = "nnltudi"; Stnt[58, 3] = "nddi"; Stnt[58, 4] = "nltudi";
        Stnt[59, 0] = "んっづ"; Stnt[59, 1] = "nnddu"; Stnt[59, 2] = "nnltudu"; Stnt[59, 3] = "nddu"; Stnt[59, 4] = "nltudu";
        Stnt[60, 0] = "んっで"; Stnt[60, 1] = "nndde"; Stnt[60, 2] = "nnltude"; Stnt[60, 3] = "ndde"; Stnt[60, 4] = "nltude";
        Stnt[61, 0] = "んっど"; Stnt[61, 1] = "nnddo"; Stnt[61, 2] = "nnltudo"; Stnt[61, 3] = "nddo"; Stnt[61, 4] = "nltudo";
        Stnt[62, 0] = "んっば"; Stnt[62, 1] = "nnbba"; Stnt[62, 2] = "nnltuba"; Stnt[62, 3] = "nbba"; Stnt[62, 4] = "nltuba";
        Stnt[63, 0] = "んっび"; Stnt[63, 1] = "nnbbi"; Stnt[63, 2] = "nnltubi"; Stnt[63, 3] = "nbbi"; Stnt[63, 4] = "nltubi";
        Stnt[64, 0] = "んっぶ"; Stnt[64, 1] = "nnbbu"; Stnt[64, 2] = "nnltubu"; Stnt[64, 3] = "nbbu"; Stnt[64, 4] = "nltubu";
        Stnt[65, 0] = "んっべ"; Stnt[65, 1] = "nnbbe"; Stnt[65, 2] = "nnltube"; Stnt[65, 3] = "nbbe"; Stnt[65, 4] = "nltube";
        Stnt[66, 0] = "んっぼ"; Stnt[66, 1] = "nnbbo"; Stnt[66, 2] = "nnltubo"; Stnt[66, 3] = "nbbo"; Stnt[66, 4] = "nltubo";
        Stnt[67, 0] = "んっぱ"; Stnt[67, 1] = "nnppa"; Stnt[67, 2] = "nnltupa"; Stnt[67, 3] = "nppa"; Stnt[67, 4] = "nltupa";
        Stnt[68, 0] = "んっぴ"; Stnt[68, 1] = "nnppi"; Stnt[68, 2] = "nnltupi"; Stnt[68, 3] = "nppi"; Stnt[68, 4] = "nltupi";
        Stnt[69, 0] = "んっぷ"; Stnt[69, 1] = "nnppu"; Stnt[69, 2] = "nnltupu"; Stnt[69, 3] = "nppu"; Stnt[69, 4] = "nltupu";
        Stnt[70, 0] = "んっぺ"; Stnt[70, 1] = "nnppe"; Stnt[70, 2] = "nnltupe"; Stnt[70, 3] = "nppe"; Stnt[70, 4] = "nltupe";
        Stnt[71, 0] = "んっぽ"; Stnt[71, 1] = "nnppo"; Stnt[71, 2] = "nnltupo"; Stnt[71, 3] = "nppo"; Stnt[71, 4] = "nltupo";
        Stnt[72, 0] = "???b?J";
        Stnt[73, 0] = "???b?L";
        Stnt[74, 0] = "???b?N";
        Stnt[75, 0] = "???b?P";
        Stnt[76, 0] = "???b?R";
        Stnt[77, 0] = "???b?T";
        Stnt[78, 0] = "???b?V";
        Stnt[79, 0] = "???b?X";
        Stnt[80, 0] = "???b?Z";
        Stnt[81, 0] = "???b?";
        Stnt[82, 0] = "???b?^";
        Stnt[83, 0] = "???b?`";
        Stnt[84, 0] = "???b?c";
        Stnt[85, 0] = "???b?e";
        Stnt[86, 0] = "???b?g";
        Stnt[87, 0] = "???b?n";
        Stnt[88, 0] = "???b?q";
        Stnt[89, 0] = "???b?t";
        Stnt[90, 0] = "???b?w";
        Stnt[91, 0] = "???b?z";
        Stnt[92, 0] = "???b?}";
        Stnt[93, 0] = "???b?~";
        Stnt[94, 0] = "???b??";
        Stnt[95, 0] = "???b??";
        Stnt[96, 0] = "???b??";
        Stnt[97, 0] = "???b??";
        Stnt[98, 0] = "???b??";
        Stnt[99, 0] = "???b??";
        Stnt[100, 0] = "???b??";
        Stnt[101, 0] = "???b??";
        Stnt[102, 0] = "???b??";
        Stnt[103, 0] = "???b??";
        Stnt[104, 0] = "???b??";
        Stnt[105, 0] = "???b??";
        Stnt[106, 0] = "???b??";
        Stnt[107, 0] = "???b?@";
        Stnt[108, 0] = "???b?B";
        Stnt[109, 0] = "???b?D";
        Stnt[110, 0] = "???b?F";
        Stnt[111, 0] = "???b?H";
        Stnt[112, 0] = "???b?b";
        Stnt[113, 0] = "???b??";
        Stnt[114, 0] = "???b??";
        Stnt[115, 0] = "???b??";
        Stnt[116, 0] = "???b??";
        Stnt[117, 0] = "???b??";
        Stnt[118, 0] = "???b??";
        Stnt[119, 0] = "???b?K";
        Stnt[120, 0] = "???b?M";
        Stnt[121, 0] = "???b?O";
        Stnt[122, 0] = "???b?Q";
        Stnt[123, 0] = "???b?S";
        Stnt[124, 0] = "???b?U";
        Stnt[125, 0] = "???b?W";
        Stnt[126, 0] = "???b?Y";
        Stnt[127, 0] = "???b?[";
        Stnt[128, 0] = "???b?]";
        Stnt[129, 0] = "???b?_";
        Stnt[130, 0] = "???b?a";
        Stnt[131, 0] = "???b?d";
        Stnt[132, 0] = "???b?f";
        Stnt[133, 0] = "???b?h";
        Stnt[134, 0] = "???b?o";
        Stnt[135, 0] = "???b?r";
        Stnt[136, 0] = "???b?u";
        Stnt[137, 0] = "???b?x";
        Stnt[138, 0] = "???b?{";
        Stnt[139, 0] = "???b?p";
        Stnt[140, 0] = "???b?s";
        Stnt[141, 0] = "???b?v";
        Stnt[142, 0] = "???b?y";
        Stnt[143, 0] = "???b?|";
        for (int i = STNT_COL / 2; i < STNT_COL; i++)
        {
            for (int k = 1; k < STNT_ROW; k++)
            {
                Stnt[i, k] = Stnt[i - (STNT_COL / 2), k];
            }
        }
    }
    void SetStntex()
    {
        for (int i = 0; i < STNTEX_COL; i++)
        {
            for (int j = 0; j < STNTEX_ROW; j++)
            {
                Stntex[i, j] = string.Empty;
            }
        }
        Stntex[0, 0] = "んっいぇ"; Stntex[0, 1] = "nnyye"; Stntex[0, 2] = "nnltuye"; Stntex[0, 3] = "nyye"; Stntex[0, 4] = "nltuye";
        Stntex[1, 0] = "んっうぁ"; Stntex[1, 1] = "nnwwha"; Stntex[1, 2] = "nnltuwha"; Stntex[1, 3] = "nwwha"; Stntex[1, 4] = "nltuwha";
        Stntex[2, 0] = "んっうぃ"; Stntex[2, 1] = "nnwwi"; Stntex[2, 2] = "nnwwhi"; Stntex[2, 3] = "nnltuwi"; Stntex[2, 4] = "nnltuwhi"; Stntex[2, 5] = "nwwi"; Stntex[2, 6] = "nwwhi"; Stntex[2, 7] = "nltuwi"; Stntex[2, 8] = "nltuwhi";
        Stntex[3, 0] = "んっうぇ"; Stntex[3, 1] = "nnwwe"; Stntex[3, 2] = "nnwwhe"; Stntex[3, 3] = "nnltuwe"; Stntex[3, 4] = "nnltuwhe"; Stntex[3, 5] = "nwwe"; Stntex[3, 6] = "nwwhe"; Stntex[3, 7] = "nltuwe"; Stntex[3, 8] = "nltuwhe";
        Stntex[4, 0] = "んっうぉ"; Stntex[4, 1] = "nnwwho"; Stntex[4, 2] = "nnltuwho"; Stntex[4, 3] = "nwwho"; Stntex[4, 4] = "nltuwho";
        Stntex[5, 0] = "んっきぃ"; Stntex[5, 1] = "nnkkyi"; Stntex[5, 2] = "nnltukyi"; Stntex[5, 3] = "nkkyi"; Stntex[5, 4] = "nltukyi";
        Stntex[6, 0] = "んっきぇ"; Stntex[6, 1] = "nnkkye"; Stntex[6, 2] = "nnltukye"; Stntex[6, 3] = "nkkye"; Stntex[6, 4] = "nltukye";
        Stntex[7, 0] = "んっきゃ"; Stntex[7, 1] = "nnkkya"; Stntex[7, 2] = "nnltukya"; Stntex[7, 3] = "nkkya"; Stntex[7, 4] = "nltukya";
        Stntex[8, 0] = "んっきゅ"; Stntex[8, 1] = "nnkkyu"; Stntex[8, 2] = "nnltukyu"; Stntex[8, 3] = "nkkyu"; Stntex[8, 4] = "nltukyu";
        Stntex[9, 0] = "んっきょ"; Stntex[9, 1] = "nnkkyo"; Stntex[9, 2] = "nnltukyo"; Stntex[9, 3] = "nkkyo"; Stntex[9, 4] = "nltukyo";
        Stntex[10, 0] = "んっぎぃ"; Stntex[10, 1] = "nnggyi"; Stntex[10, 2] = "nnltugyi"; Stntex[10, 3] = "nggyi"; Stntex[10, 4] = "nltugyi";
        Stntex[11, 0] = "んっぎぇ"; Stntex[11, 1] = "nnggye"; Stntex[11, 2] = "nnltugye"; Stntex[11, 3] = "nggye"; Stntex[11, 4] = "nltugye";
        Stntex[12, 0] = "んっぎゃ"; Stntex[12, 1] = "nnggya"; Stntex[12, 2] = "nnltugya"; Stntex[12, 3] = "nggya"; Stntex[12, 4] = "nltugya";
        Stntex[13, 0] = "んっぎゅ"; Stntex[13, 1] = "nnggyu"; Stntex[13, 2] = "nnltugyu"; Stntex[13, 3] = "nggyu"; Stntex[13, 4] = "nltugyu";
        Stntex[14, 0] = "んっぎょ"; Stntex[14, 1] = "nnggyo"; Stntex[14, 2] = "nnltugyo"; Stntex[14, 3] = "nggyo"; Stntex[14, 4] = "nltugyo";
        Stntex[15, 0] = "んっくぁ"; Stntex[15, 1] = "nnqqa"; Stntex[15, 2] = "nnqqwa"; Stntex[15, 3] = "nnkkwa"; Stntex[15, 4] = "nnltuqa"; Stntex[15, 5] = "nnltuqwa"; Stntex[15, 6] = "nqqa"; Stntex[15, 7] = "nqqwa"; Stntex[15, 8] = "nkkwa"; Stntex[15, 9] = "nltuqa"; Stntex[15, 10] = "nltuqwa";
        Stntex[16, 0] = "んっくぃ"; Stntex[16, 1] = "nnqqi"; Stntex[16, 2] = "nnqqwi"; Stntex[16, 3] = "nnqqyi"; Stntex[16, 4] = "nnltuqi"; Stntex[16, 5] = "nnltuqwi"; Stntex[16, 6] = "nqqi"; Stntex[16, 7] = "nqqwi"; Stntex[16, 8] = "nqqyi"; Stntex[16, 9] = "nltuqi"; Stntex[16, 10] = "nltuqwi";
        Stntex[17, 0] = "んっくぅ"; Stntex[17, 1] = "nnqqwu"; Stntex[17, 2] = "nnltuqwu"; Stntex[17, 3] = "nqqwu"; Stntex[17, 4] = "nltuqwu";
        Stntex[18, 0] = "んっくぇ"; Stntex[18, 1] = "nnqqe"; Stntex[18, 2] = "nnqqwe"; Stntex[18, 3] = "nnqqye"; Stntex[18, 4] = "nnltuqe"; Stntex[18, 5] = "nnltuqye"; Stntex[18, 6] = "nqqe"; Stntex[18, 7] = "nqqwe"; Stntex[18, 8] = "nqqye"; Stntex[18, 9] = "nltuqe"; Stntex[18, 10] = "nltuqye";
        Stntex[19, 0] = "んっくぉ"; Stntex[19, 1] = "nnqqo"; Stntex[19, 2] = "nnqqwo"; Stntex[19, 3] = "nnltuqo"; Stntex[19, 4] = "nnltuqwo"; Stntex[19, 5] = "nqqo"; Stntex[19, 6] = "nqqwo"; Stntex[19, 7] = "nltuqo"; Stntex[19, 8] = "nltuqwo";
        Stntex[20, 0] = "んっくゃ"; Stntex[20, 1] = "nnqqya"; Stntex[20, 2] = "nnltuqya"; Stntex[20, 3] = "nqqya"; Stntex[20, 4] = "nltuqya";
        Stntex[21, 0] = "んっくゅ"; Stntex[21, 1] = "nnqqyu"; Stntex[21, 2] = "nnltuqyu"; Stntex[21, 3] = "nqqyu"; Stntex[21, 4] = "nltuqyu";
        Stntex[22, 0] = "んっくょ"; Stntex[22, 1] = "nnqqyo"; Stntex[22, 2] = "nnltuqyo"; Stntex[22, 3] = "nqqyo"; Stntex[22, 4] = "nltuqyo";
        Stntex[23, 0] = "んっぐぁ"; Stntex[23, 1] = "nnggwa"; Stntex[23, 2] = "nnltugwa"; Stntex[23, 3] = "nggwa"; Stntex[23, 4] = "nltugwa";
        Stntex[24, 0] = "んっぐぃ"; Stntex[24, 1] = "nnggwi"; Stntex[24, 2] = "nnltugwi"; Stntex[24, 3] = "nggwi"; Stntex[24, 4] = "nltugwi";
        Stntex[25, 0] = "んっぐぅ"; Stntex[25, 1] = "nnggwu"; Stntex[25, 2] = "nnltugwu"; Stntex[25, 3] = "nggwu"; Stntex[25, 4] = "nltugwu";
        Stntex[26, 0] = "んっぐぇ"; Stntex[26, 1] = "nnggwe"; Stntex[26, 2] = "nnltugwe"; Stntex[26, 3] = "nggwe"; Stntex[26, 4] = "nltugwe";
        Stntex[27, 0] = "んっぐぉ"; Stntex[27, 1] = "nnggwo"; Stntex[27, 2] = "nnltugwo"; Stntex[27, 3] = "nggwo"; Stntex[27, 4] = "nltugwo";
        Stntex[28, 0] = "んっしぃ"; Stntex[28, 1] = "nnssyi"; Stntex[28, 2] = "nnltusyi"; Stntex[28, 3] = "nssyi"; Stntex[28, 4] = "nltusyi";
        Stntex[29, 0] = "んっしぇ"; Stntex[29, 1] = "nnssye"; Stntex[29, 2] = "nnsshe"; Stntex[29, 3] = "nnltusye"; Stntex[29, 4] = "nnltushe"; Stntex[29, 5] = "nssye"; Stntex[29, 6] = "nsshe"; Stntex[29, 7] = "nltusye"; Stntex[29, 8] = "nltushe";
        Stntex[30, 0] = "んっしゃ"; Stntex[30, 1] = "nnssya"; Stntex[30, 2] = "nnssha"; Stntex[30, 3] = "nnltusya"; Stntex[30, 4] = "nnltusha"; Stntex[30, 5] = "nssya"; Stntex[30, 6] = "nssha"; Stntex[30, 7] = "nltusya"; Stntex[30, 8] = "nltusha";
        Stntex[31, 0] = "んっしゅ"; Stntex[31, 1] = "nnssyu"; Stntex[31, 2] = "nnsshu"; Stntex[31, 3] = "nnltusyu"; Stntex[31, 4] = "nnltushu"; Stntex[31, 5] = "nssyu"; Stntex[31, 6] = "nsshu"; Stntex[31, 7] = "nltusyu"; Stntex[31, 8] = "nltushu";
        Stntex[32, 0] = "んっしょ"; Stntex[32, 1] = "nnssyo"; Stntex[32, 2] = "nnssho"; Stntex[32, 3] = "nnltusyo"; Stntex[32, 4] = "nnltusho"; Stntex[32, 5] = "nssyo"; Stntex[32, 6] = "nssho"; Stntex[32, 7] = "nltusyo"; Stntex[32, 8] = "nltusho";
        Stntex[33, 0] = "んっじぃ"; Stntex[33, 1] = "nnjjyi"; Stntex[33, 2] = "nnzzyi"; Stntex[33, 3] = "nnltujyi"; Stntex[33, 4] = "nnltuzyi"; Stntex[33, 5] = "njjyi"; Stntex[33, 6] = "nzzyi"; Stntex[33, 7] = "nltujyi"; Stntex[33, 8] = "nltuzyi";
        Stntex[34, 0] = "んっじぇ"; Stntex[34, 1] = "nnjje"; Stntex[34, 2] = "nnjjye"; Stntex[34, 3] = "nnzzye"; Stntex[34, 4] = "nnltuje"; Stntex[34, 5] = "nnltujye"; Stntex[34, 6] = "nnltuzye"; Stntex[34, 7] = "njje"; Stntex[34, 8] = "njjye"; Stntex[34, 9] = "nzzye"; Stntex[34, 10] = "nltuje"; Stntex[34, 11] = "nltujye"; Stntex[34, 12] = "nltuzye";
        Stntex[35, 0] = "んっじゃ"; Stntex[35, 1] = "nnjja"; Stntex[35, 2] = "nnjjya"; Stntex[35, 3] = "nnzzya"; Stntex[35, 4] = "nnltuja"; Stntex[35, 5] = "nnltujya"; Stntex[35, 6] = "nnltuzya"; Stntex[35, 7] = "njja"; Stntex[35, 8] = "njjya"; Stntex[35, 9] = "nzzya"; Stntex[35, 10] = "nltuja"; Stntex[35, 11] = "nltujya"; Stntex[35, 12] = "nltuzya";
        Stntex[36, 0] = "んっじゅ"; Stntex[36, 1] = "nnjju"; Stntex[36, 2] = "nnjjyu"; Stntex[36, 3] = "nnzzyu"; Stntex[36, 4] = "nnltuju"; Stntex[36, 5] = "nnltujyu"; Stntex[36, 6] = "nnltuzyu"; Stntex[36, 7] = "njju"; Stntex[36, 8] = "njjyu"; Stntex[36, 9] = "nzzyu"; Stntex[36, 10] = "nltuju"; Stntex[36, 11] = "nltujyu"; Stntex[36, 12] = "nltuzyu";
        Stntex[37, 0] = "んっじょ"; Stntex[37, 1] = "nnjjo"; Stntex[37, 2] = "nnjjyo"; Stntex[37, 3] = "nnzzyo"; Stntex[37, 4] = "nnltujo"; Stntex[37, 5] = "nnltujyo"; Stntex[37, 6] = "nnltuzyo"; Stntex[37, 7] = "njjo"; Stntex[37, 8] = "njjyo"; Stntex[37, 9] = "nzzyo"; Stntex[37, 10] = "nltujo"; Stntex[37, 11] = "nltujyo"; Stntex[37, 12] = "nltuzyo";
        Stntex[38, 0] = "んっすぁ"; Stntex[38, 1] = "nnsswa"; Stntex[38, 2] = "nnltuswa"; Stntex[38, 3] = "nsswa"; Stntex[38, 4] = "nltuswa";
        Stntex[39, 0] = "んっすぃ"; Stntex[39, 1] = "nnsswi"; Stntex[39, 2] = "nnltuswi"; Stntex[39, 3] = "nsswi"; Stntex[39, 4] = "nltuswi";
        Stntex[40, 0] = "んっすぅ"; Stntex[40, 1] = "nnsswu"; Stntex[40, 2] = "nnltuswu"; Stntex[40, 3] = "nsswu"; Stntex[40, 4] = "nltuswu";
        Stntex[41, 0] = "んっすぇ"; Stntex[41, 1] = "nnsswe"; Stntex[41, 2] = "nnltuswe"; Stntex[41, 3] = "nsswe"; Stntex[41, 4] = "nltuswe";
        Stntex[42, 0] = "んっすぉ"; Stntex[42, 1] = "nnsswo"; Stntex[42, 2] = "nnltuswo"; Stntex[42, 3] = "nsswo"; Stntex[42, 4] = "nltuswo";
        Stntex[43, 0] = "んっちぃ"; Stntex[43, 1] = "nnttyi"; Stntex[43, 2] = "nnccyi"; Stntex[43, 3] = "nnltutyi"; Stntex[43, 4] = "nnltucyi"; Stntex[43, 5] = "nttyi"; Stntex[43, 6] = "nccyi"; Stntex[43, 7] = "nltutyi"; Stntex[43, 8] = "nltucyi";
        Stntex[44, 0] = "んっちぇ"; Stntex[44, 1] = "nnttye"; Stntex[44, 2] = "nnccye"; Stntex[44, 3] = "nncche"; Stntex[44, 4] = "nnltutye"; Stntex[44, 5] = "nnltucye"; Stntex[44, 6] = "nnltuche"; Stntex[44, 7] = "nttye"; Stntex[44, 8] = "nccye"; Stntex[44, 9] = "ncche"; Stntex[44, 10] = "nltutye"; Stntex[44, 11] = "nltucye"; Stntex[44, 12] = "nltuche";
        Stntex[45, 0] = "んっちゃ"; Stntex[45, 1] = "nnttya"; Stntex[45, 2] = "nnccya"; Stntex[45, 3] = "nnccha"; Stntex[45, 4] = "nnltutya"; Stntex[45, 5] = "nnltucya"; Stntex[45, 6] = "nnltucha"; Stntex[45, 7] = "nttya"; Stntex[45, 8] = "nccya"; Stntex[45, 9] = "nccha"; Stntex[45, 10] = "nltutya"; Stntex[45, 11] = "nltucya"; Stntex[45, 12] = "nltucha";
        Stntex[46, 0] = "んっちゅ"; Stntex[46, 1] = "nnttyu"; Stntex[46, 2] = "nnccyu"; Stntex[46, 3] = "nncchu"; Stntex[46, 4] = "nnltutyu"; Stntex[46, 5] = "nnltucyu"; Stntex[46, 6] = "nnltuchu"; Stntex[46, 7] = "nttyu"; Stntex[46, 8] = "nccyu"; Stntex[46, 9] = "ncchu"; Stntex[46, 10] = "nltutyu"; Stntex[46, 11] = "nltucyu"; Stntex[46, 12] = "nltuchu";
        Stntex[47, 0] = "んっちょ"; Stntex[47, 1] = "nnttyo"; Stntex[47, 2] = "nnccyo"; Stntex[47, 3] = "nnccho"; Stntex[47, 4] = "nnltutyo"; Stntex[47, 5] = "nnltucyo"; Stntex[47, 6] = "nnltucho"; Stntex[47, 7] = "nttyo"; Stntex[47, 8] = "nccyo"; Stntex[47, 9] = "nccho"; Stntex[47, 10] = "nltutyo"; Stntex[47, 11] = "nltucyo"; Stntex[47, 12] = "nltucho";
        Stntex[48, 0] = "んっぢぃ"; Stntex[48, 1] = "nnddyi"; Stntex[48, 2] = "nnltudyi"; Stntex[48, 3] = "nddyi"; Stntex[48, 4] = "nltudyi";
        Stntex[49, 0] = "んっぢぇ"; Stntex[49, 1] = "nnddye"; Stntex[49, 2] = "nnltudye"; Stntex[49, 3] = "nddye"; Stntex[49, 4] = "nltudye";
        Stntex[50, 0] = "んっぢゃ"; Stntex[50, 1] = "nnddya"; Stntex[50, 2] = "nnltudya"; Stntex[50, 3] = "nddya"; Stntex[50, 4] = "nltudya";
        Stntex[51, 0] = "んっぢゅ"; Stntex[51, 1] = "nnddyu"; Stntex[51, 2] = "nnltudyu"; Stntex[51, 3] = "nddyu"; Stntex[51, 4] = "nltudyu";
        Stntex[52, 0] = "んっぢょ"; Stntex[52, 1] = "nnddyo"; Stntex[52, 2] = "nnltudyo"; Stntex[52, 3] = "nddyo"; Stntex[52, 4] = "nltudyo";
        Stntex[53, 0] = "んっつぁ"; Stntex[53, 1] = "nnttsa"; Stntex[53, 2] = "nnltutsa"; Stntex[53, 3] = "nttsa"; Stntex[53, 4] = "nltutsa";
        Stntex[54, 0] = "んっつぃ"; Stntex[54, 1] = "nnttsi"; Stntex[54, 2] = "nnltutsi"; Stntex[54, 3] = "nttsi"; Stntex[54, 4] = "nltutsi";
        Stntex[55, 0] = "んっつぇ"; Stntex[55, 1] = "nnttse"; Stntex[55, 2] = "nnltutse"; Stntex[55, 3] = "nttse"; Stntex[55, 4] = "nltutse";
        Stntex[56, 0] = "んっつぉ"; Stntex[56, 1] = "nnttso"; Stntex[56, 2] = "nnltutso"; Stntex[56, 3] = "nttso"; Stntex[56, 4] = "nltutso";
        Stntex[57, 0] = "んってぃ"; Stntex[57, 1] = "nntthi"; Stntex[57, 2] = "nnltuthi"; Stntex[57, 3] = "ntthi"; Stntex[57, 4] = "nltuthi";
        Stntex[58, 0] = "んってぇ"; Stntex[58, 1] = "nntthe"; Stntex[58, 2] = "nnltuthe"; Stntex[58, 3] = "ntthe"; Stntex[58, 4] = "nltuthe";
        Stntex[59, 0] = "んってゃ"; Stntex[59, 1] = "nnttha"; Stntex[59, 2] = "nnltutha"; Stntex[59, 3] = "nttha"; Stntex[59, 4] = "nltutha";
        Stntex[60, 0] = "んってゅ"; Stntex[60, 1] = "nntthu"; Stntex[60, 2] = "nnltuthu"; Stntex[60, 3] = "ntthu"; Stntex[60, 4] = "nltuthu";
        Stntex[61, 0] = "んってょ"; Stntex[61, 1] = "nnttho"; Stntex[61, 2] = "nnltutho"; Stntex[61, 3] = "nttho"; Stntex[61, 4] = "nltutho";
        Stntex[62, 0] = "んっでぃ"; Stntex[62, 1] = "nnddhi"; Stntex[62, 2] = "nnltudhi"; Stntex[62, 3] = "nddhi"; Stntex[62, 4] = "nltudhi";
        Stntex[63, 0] = "んっでぇ"; Stntex[63, 1] = "nnddhe"; Stntex[63, 2] = "nnltudhe"; Stntex[63, 3] = "nddhe"; Stntex[63, 4] = "nltudhe";
        Stntex[64, 0] = "んっでゃ"; Stntex[64, 1] = "nnddha"; Stntex[64, 2] = "nnltudha"; Stntex[64, 3] = "nddha"; Stntex[64, 4] = "nltudha";
        Stntex[65, 0] = "んっでゅ"; Stntex[65, 1] = "nnddhu"; Stntex[65, 2] = "nnltudhu"; Stntex[65, 3] = "nddhu"; Stntex[65, 4] = "nltudhu";
        Stntex[66, 0] = "んっでょ"; Stntex[66, 1] = "nnddho"; Stntex[66, 2] = "nnltudho"; Stntex[66, 3] = "nddho"; Stntex[66, 4] = "nltudho";
        Stntex[67, 0] = "んっとぁ"; Stntex[67, 1] = "nnttwa"; Stntex[67, 2] = "nnltutwa"; Stntex[67, 3] = "nttwa"; Stntex[67, 4] = "nltutwa";
        Stntex[68, 0] = "んっとぃ"; Stntex[68, 1] = "nnttwi"; Stntex[68, 2] = "nnltutwi"; Stntex[68, 3] = "nttwi"; Stntex[68, 4] = "nltutwi";
        Stntex[69, 0] = "んっとぅ"; Stntex[69, 1] = "nnttwu"; Stntex[69, 2] = "nnltutwu"; Stntex[69, 3] = "nttwu"; Stntex[69, 4] = "nltutwu";
        Stntex[70, 0] = "んっとぇ"; Stntex[70, 1] = "nnttwe"; Stntex[70, 2] = "nnltutwe"; Stntex[70, 3] = "nttwe"; Stntex[70, 4] = "nltutwe";
        Stntex[71, 0] = "んっとぉ"; Stntex[71, 1] = "nnttwo"; Stntex[71, 2] = "nnltutwo"; Stntex[71, 3] = "nttwo"; Stntex[71, 4] = "nltutwo";
        Stntex[72, 0] = "んっどぁ"; Stntex[72, 1] = "nnddwa"; Stntex[72, 2] = "nnltudwa"; Stntex[72, 3] = "nddwa"; Stntex[72, 4] = "nltudwa";
        Stntex[73, 0] = "んっどぃ"; Stntex[73, 1] = "nnddwi"; Stntex[73, 2] = "nnltudwi"; Stntex[73, 3] = "nddwi"; Stntex[73, 4] = "nltudwi";
        Stntex[74, 0] = "んっどぅ"; Stntex[74, 1] = "nnddwu"; Stntex[74, 2] = "nnltudwu"; Stntex[74, 3] = "nddwu"; Stntex[74, 4] = "nltudwu";
        Stntex[75, 0] = "んっどぇ"; Stntex[75, 1] = "nnddwe"; Stntex[75, 2] = "nnltudwe"; Stntex[75, 3] = "nddwe"; Stntex[75, 4] = "nltudwe";
        Stntex[76, 0] = "んっどぉ"; Stntex[76, 1] = "nnddwo"; Stntex[76, 2] = "nnltudwo"; Stntex[76, 3] = "nddwo"; Stntex[76, 4] = "nltudwo";
        Stntex[77, 0] = "んっひぃ"; Stntex[77, 1] = "nnhhyi"; Stntex[77, 2] = "nnltuhyi"; Stntex[77, 3] = "nhhyi"; Stntex[77, 4] = "nltuhyi";
        Stntex[78, 0] = "んっひぇ"; Stntex[78, 1] = "nnhhye"; Stntex[78, 2] = "nnltuhye"; Stntex[78, 3] = "nhhye"; Stntex[78, 4] = "nltuhye";
        Stntex[79, 0] = "んっひゃ"; Stntex[79, 1] = "nnhhya"; Stntex[79, 2] = "nnltuhya"; Stntex[79, 3] = "nhhya"; Stntex[79, 4] = "nltuhya";
        Stntex[80, 0] = "んっひゅ"; Stntex[80, 1] = "nnhhyu"; Stntex[80, 2] = "nnltuhyu"; Stntex[80, 3] = "nhhyu"; Stntex[80, 4] = "nltuhyu";
        Stntex[81, 0] = "んっひょ"; Stntex[81, 1] = "nnhhyo"; Stntex[81, 2] = "nnltuhyo"; Stntex[81, 3] = "nhhyo"; Stntex[81, 4] = "nltuhyo";
        Stntex[82, 0] = "んっびぃ"; Stntex[82, 1] = "nnbbyi"; Stntex[82, 2] = "nnltubyi"; Stntex[82, 3] = "nbbyi"; Stntex[82, 4] = "nltubyi";
        Stntex[83, 0] = "んっびぇ"; Stntex[83, 1] = "nnbbye"; Stntex[83, 2] = "nnltubye"; Stntex[83, 3] = "nbbye"; Stntex[83, 4] = "nltubye";
        Stntex[84, 0] = "んっびゃ"; Stntex[84, 1] = "nnbbya"; Stntex[84, 2] = "nnltubya"; Stntex[84, 3] = "nbbya"; Stntex[84, 4] = "nltubya";
        Stntex[85, 0] = "んっびゅ"; Stntex[85, 1] = "nnbbyu"; Stntex[85, 2] = "nnltubyu"; Stntex[85, 3] = "nbbyu"; Stntex[85, 4] = "nltubyu";
        Stntex[86, 0] = "んっびょ"; Stntex[86, 1] = "nnbbyo"; Stntex[86, 2] = "nnltubyo"; Stntex[86, 3] = "nbbyo"; Stntex[86, 4] = "nltubyo";
        Stntex[87, 0] = "んっぴぃ"; Stntex[87, 1] = "nnppyi"; Stntex[87, 2] = "nnltupyi"; Stntex[87, 3] = "nppyi"; Stntex[87, 4] = "nltupyi";
        Stntex[88, 0] = "んっぴぇ"; Stntex[88, 1] = "nnppye"; Stntex[88, 2] = "nnltupye"; Stntex[88, 3] = "nppye"; Stntex[88, 4] = "nltupye";
        Stntex[89, 0] = "んっぴゃ"; Stntex[89, 1] = "nnppya"; Stntex[89, 2] = "nnltupya"; Stntex[89, 3] = "nppya"; Stntex[89, 4] = "nltupya";
        Stntex[90, 0] = "んっぴゅ"; Stntex[90, 1] = "nnppyu"; Stntex[90, 2] = "nnltupyu"; Stntex[90, 3] = "nppyu"; Stntex[90, 4] = "nltupyu";
        Stntex[91, 0] = "んっぴょ"; Stntex[91, 1] = "nnppyo"; Stntex[91, 2] = "nnltupyo"; Stntex[91, 3] = "nppyo"; Stntex[91, 4] = "nltupyo";
        Stntex[92, 0] = "んっふぁ"; Stntex[92, 1] = "nnffa"; Stntex[92, 2] = "nnffwa"; Stntex[92, 3] = "nnltufa"; Stntex[92, 4] = "nnltufwa"; Stntex[92, 5] = "nffa"; Stntex[92, 6] = "nffwa"; Stntex[92, 7] = "nltufa"; Stntex[92, 8] = "nltufwa";
        Stntex[93, 0] = "んっふぃ"; Stntex[93, 1] = "nnffi"; Stntex[93, 2] = "nnffyi"; Stntex[93, 3] = "nnffwi"; Stntex[93, 4] = "nnltufi"; Stntex[93, 5] = "nnltufyi"; Stntex[93, 6] = "nnltufwi"; Stntex[93, 7] = "nffi"; Stntex[93, 8] = "nffyi"; Stntex[93, 9] = "nffwi"; Stntex[93, 10] = "nltufi"; Stntex[93, 11] = "nltufyi"; Stntex[93, 12] = "nltufwi";
        Stntex[94, 0] = "んっふぅ"; Stntex[94, 1] = "nnffwu"; Stntex[94, 2] = "nnltufwu"; Stntex[94, 3] = "nffwu"; Stntex[94, 4] = "nltufwu";
        Stntex[95, 0] = "んっふぇ"; Stntex[95, 1] = "nnffe"; Stntex[95, 2] = "nnffye"; Stntex[95, 3] = "nnffwe"; Stntex[95, 4] = "nnltufe"; Stntex[95, 5] = "nnltufye"; Stntex[95, 6] = "nnltufwe"; Stntex[95, 7] = "nffe"; Stntex[95, 8] = "nffye"; Stntex[95, 9] = "nffwe"; Stntex[95, 10] = "nltufe"; Stntex[95, 11] = "nltufye"; Stntex[95, 12] = "nltufwe";
        Stntex[96, 0] = "んっふぉ"; Stntex[96, 1] = "nnffo"; Stntex[96, 2] = "nnffwo"; Stntex[96, 3] = "nnltufo"; Stntex[96, 4] = "nnltufwo"; Stntex[96, 5] = "nffo"; Stntex[96, 6] = "nffwo"; Stntex[96, 7] = "nltufo"; Stntex[96, 8] = "nltufwo";
        Stntex[97, 0] = "んっふゃ"; Stntex[97, 1] = "nnffya"; Stntex[97, 2] = "nnltufya"; Stntex[97, 3] = "nffya"; Stntex[97, 4] = "nltufya";
        Stntex[98, 0] = "んっふゅ"; Stntex[98, 1] = "nnffyu"; Stntex[98, 2] = "nnltufyu"; Stntex[98, 3] = "nffyu"; Stntex[98, 4] = "nltufyu";
        Stntex[99, 0] = "んっふょ"; Stntex[99, 1] = "nnffyo"; Stntex[99, 2] = "nnltufyo"; Stntex[99, 3] = "nffyo"; Stntex[99, 4] = "nltufyo";
        Stntex[100, 0] = "んっみぃ"; Stntex[100, 1] = "nnmmyi"; Stntex[100, 2] = "nnltumyi"; Stntex[100, 3] = "nmmyi"; Stntex[100, 4] = "nltumyi";
        Stntex[101, 0] = "んっみぇ"; Stntex[101, 1] = "nnmmye"; Stntex[101, 2] = "nnltumye"; Stntex[101, 3] = "nmmye"; Stntex[101, 4] = "nltumye";
        Stntex[102, 0] = "んっみゃ"; Stntex[102, 1] = "nnmmya"; Stntex[102, 2] = "nnltumya"; Stntex[102, 3] = "nmmya"; Stntex[102, 4] = "nltumya";
        Stntex[103, 0] = "んっみゅ"; Stntex[103, 1] = "nnmmyu"; Stntex[103, 2] = "nnltumyu"; Stntex[103, 3] = "nmmyu"; Stntex[103, 4] = "nltumyu";
        Stntex[104, 0] = "んっみょ"; Stntex[104, 1] = "nnmmyo"; Stntex[104, 2] = "nnltumyo"; Stntex[104, 3] = "nmmyo"; Stntex[104, 4] = "nltumyo";
        Stntex[105, 0] = "んっりぃ"; Stntex[105, 1] = "nnrryi"; Stntex[105, 2] = "nnlturyi"; Stntex[105, 3] = "nrryi"; Stntex[105, 4] = "nlturyi";
        Stntex[106, 0] = "んっりぇ"; Stntex[106, 1] = "nnrrye"; Stntex[106, 2] = "nnlturye"; Stntex[106, 3] = "nrrye"; Stntex[106, 4] = "nlturye";
        Stntex[107, 0] = "んっりゃ"; Stntex[107, 1] = "nnrrya"; Stntex[107, 2] = "nnlturya"; Stntex[107, 3] = "nrrya"; Stntex[107, 4] = "nlturya";
        Stntex[108, 0] = "んっりゅ"; Stntex[108, 1] = "nnrryu"; Stntex[108, 2] = "nnlturyu"; Stntex[108, 3] = "nrryu"; Stntex[108, 4] = "nlturyu";
        Stntex[109, 0] = "んっりょ"; Stntex[109, 1] = "nnrryo"; Stntex[109, 2] = "nnlturyo"; Stntex[109, 3] = "nrryo"; Stntex[109, 4] = "nlturyo";
        Stntex[110, 0] = "んっヴぁ"; Stntex[110, 1] = "nnvva"; Stntex[110, 2] = "nnltuva"; Stntex[110, 3] = "nvva"; Stntex[110, 4] = "nltuva";
        Stntex[111, 0] = "んっヴぃ"; Stntex[111, 1] = "nnvvi"; Stntex[111, 2] = "nnvvyi"; Stntex[111, 3] = "nnltuvi"; Stntex[111, 4] = "nnltuvyi"; Stntex[111, 5] = "nvvi"; Stntex[111, 6] = "nvvyi"; Stntex[111, 7] = "nltuvi"; Stntex[111, 8] = "nltuvyi";
        Stntex[112, 0] = "んっヴぇ"; Stntex[112, 1] = "nnvve"; Stntex[112, 2] = "nnvvye"; Stntex[112, 3] = "nnltuve"; Stntex[112, 4] = "nnltuvye"; Stntex[112, 5] = "nvve"; Stntex[112, 6] = "nvvye"; Stntex[112, 7] = "nltuve"; Stntex[112, 8] = "nltuvye";
        Stntex[113, 0] = "んっヴぉ"; Stntex[113, 1] = "nnvvo"; Stntex[113, 2] = "nnltuvo"; Stntex[113, 3] = "nvvo"; Stntex[113, 4] = "nltuvo";
        Stntex[114, 0] = "んっヴゃ"; Stntex[114, 1] = "nnvvya"; Stntex[114, 2] = "nnltuvya"; Stntex[114, 3] = "nvvya"; Stntex[114, 4] = "nltuvya";
        Stntex[115, 0] = "んっヴゅ"; Stntex[115, 1] = "nnvvyu"; Stntex[115, 2] = "nnltuvyu"; Stntex[115, 3] = "nvvyu"; Stntex[115, 4] = "nltuvyu";
        Stntex[116, 0] = "んっヴょ"; Stntex[116, 1] = "nnvvyo"; Stntex[116, 2] = "nnltuvyo"; Stntex[116, 3] = "nvvyo"; Stntex[116, 4] = "nltuvyo";
        Stntex[117, 0] = "???b?C?F";
        Stntex[118, 0] = "???b?E?@";
        Stntex[119, 0] = "???b?E?B";
        Stntex[120, 0] = "???b?E?F";
        Stntex[121, 0] = "???b?E?H";
        Stntex[122, 0] = "???b?L?B";
        Stntex[123, 0] = "???b?L?F";
        Stntex[124, 0] = "???b?L??";
        Stntex[125, 0] = "???b?L??";
        Stntex[126, 0] = "???b?L??";
        Stntex[127, 0] = "???b?M?B";
        Stntex[128, 0] = "???b?M?F";
        Stntex[129, 0] = "???b?M??";
        Stntex[130, 0] = "???b?M??";
        Stntex[131, 0] = "???b?M??";
        Stntex[132, 0] = "???b?N?@";
        Stntex[133, 0] = "???b?N?B";
        Stntex[134, 0] = "???b?N?D";
        Stntex[135, 0] = "???b?N?F";
        Stntex[136, 0] = "???b?N?H";
        Stntex[137, 0] = "???b?N??";
        Stntex[138, 0] = "???b?N??";
        Stntex[139, 0] = "???b?N??";
        Stntex[140, 0] = "???b?O?@";
        Stntex[141, 0] = "???b?O?B";
        Stntex[142, 0] = "???b?O?D";
        Stntex[143, 0] = "???b?O?F";
        Stntex[144, 0] = "???b?O?H";
        Stntex[145, 0] = "???b?V?B";
        Stntex[146, 0] = "???b?V?F";
        Stntex[147, 0] = "???b?V??";
        Stntex[148, 0] = "???b?V??";
        Stntex[149, 0] = "???b?V??";
        Stntex[150, 0] = "???b?W?B";
        Stntex[151, 0] = "???b?W?F";
        Stntex[152, 0] = "???b?W??";
        Stntex[153, 0] = "???b?W??";
        Stntex[154, 0] = "???b?W??";
        Stntex[155, 0] = "???b?X?@";
        Stntex[156, 0] = "???b?X?B";
        Stntex[157, 0] = "???b?X?D";
        Stntex[158, 0] = "???b?X?F";
        Stntex[159, 0] = "???b?X?H";
        Stntex[160, 0] = "???b?`?B";
        Stntex[161, 0] = "???b?`?F";
        Stntex[162, 0] = "???b?`??";
        Stntex[163, 0] = "???b?`??";
        Stntex[164, 0] = "???b?`??";
        Stntex[165, 0] = "???b?a?B";
        Stntex[166, 0] = "???b?a?F";
        Stntex[167, 0] = "???b?a??";
        Stntex[168, 0] = "???b?a??";
        Stntex[169, 0] = "???b?a??";
        Stntex[170, 0] = "???b?c?@";
        Stntex[171, 0] = "???b?c?B";
        Stntex[172, 0] = "???b?c?F";
        Stntex[173, 0] = "???b?c?H";
        Stntex[174, 0] = "???b?e?B";
        Stntex[175, 0] = "???b?e?F";
        Stntex[176, 0] = "???b?e??";
        Stntex[177, 0] = "???b?e??";
        Stntex[178, 0] = "???b?e??";
        Stntex[179, 0] = "???b?f?B";
        Stntex[180, 0] = "???b?f?F";
        Stntex[181, 0] = "???b?f??";
        Stntex[182, 0] = "???b?f??";
        Stntex[183, 0] = "???b?f??";
        Stntex[184, 0] = "???b?g?@";
        Stntex[185, 0] = "???b?g?B";
        Stntex[186, 0] = "???b?g?D";
        Stntex[187, 0] = "???b?g?F";
        Stntex[188, 0] = "???b?g?H";
        Stntex[189, 0] = "???b?h?@";
        Stntex[190, 0] = "???b?h?B";
        Stntex[191, 0] = "???b?h?H";
        Stntex[192, 0] = "???b?h?F";
        Stntex[193, 0] = "???b?h?H";
        Stntex[194, 0] = "???b?q?B";
        Stntex[195, 0] = "???b?q?F";
        Stntex[196, 0] = "???b?q??";
        Stntex[197, 0] = "???b?q??";
        Stntex[198, 0] = "???b?q??";
        Stntex[199, 0] = "???b?r?B";
        Stntex[200, 0] = "???b?r?F";
        Stntex[201, 0] = "???b?r??";
        Stntex[202, 0] = "???b?r??";
        Stntex[203, 0] = "???b?r??";
        Stntex[204, 0] = "???b?s?B";
        Stntex[205, 0] = "???b?s?F";
        Stntex[206, 0] = "???b?s??";
        Stntex[207, 0] = "???b?s??";
        Stntex[208, 0] = "???b?s??";
        Stntex[209, 0] = "???b?t?@";
        Stntex[210, 0] = "???b?t?B";
        Stntex[211, 0] = "???b?t?D";
        Stntex[212, 0] = "???b?t?F";
        Stntex[213, 0] = "???b?t?H";
        Stntex[214, 0] = "???b?t??";
        Stntex[215, 0] = "???b?t??";
        Stntex[216, 0] = "???b?t??";
        Stntex[217, 0] = "???b?~?B";
        Stntex[218, 0] = "???b?~?F";
        Stntex[219, 0] = "???b?~??";
        Stntex[220, 0] = "???b?~??";
        Stntex[221, 0] = "???b?~??";
        Stntex[222, 0] = "???b???B";
        Stntex[223, 0] = "???b???F";
        Stntex[224, 0] = "???b????";
        Stntex[225, 0] = "???b????";
        Stntex[226, 0] = "???b????";
        Stntex[227, 0] = "???b???@";
        Stntex[228, 0] = "???b???B";
        Stntex[229, 0] = "???b???F";
        Stntex[230, 0] = "???b???H";
        Stntex[231, 0] = "???b????";
        Stntex[232, 0] = "???b????";
        Stntex[233, 0] = "???b????";
        for (int i = STNTEX_COL / 2; i < STNTEX_COL; i++)
        {
            for (int k = 1; k < STNTEX_ROW; k++)
            {
                Stntex[i, k] = Stntex[i - (STNTEX_COL / 2), k];
            }
        }
    }
    public void SetJI(int type, int nn)
    {
        if (type == 1)
        {
            //JI
            St[64, 1] = "ji"; St[64, 2] = "zi";
            St[145, 1] = "ji"; St[145, 2] = "zi";

            Stt[53, 1] = "jji"; Stt[53, 2] = "zzi"; Stt[53, 3] = "ltuji"; Stt[53, 4] = "ltuzi";
            Stt[125, 1] = "jji"; Stt[125, 2] = "zzi"; Stt[125, 3] = "ltuji"; Stt[125, 4] = "ltuzi";

            Stn[49, 1] = "nnji"; Stn[49, 2] = "nnzi"; Stn[49, 5] = "nji"; Stn[49, 6] = "nzi";
            Stn[117, 1] = "nnji"; Stn[117, 2] = "nnzi"; Stn[117, 5] = "nji"; Stn[117, 6] = "nzi";

            Stnt[53, 1] = "nnjji"; Stnt[53, 2] = "nnzzi"; Stnt[53, 3] = "nnltuji"; Stnt[53, 4] = "nnltuzi"; Stnt[53, 5] = "njji"; Stnt[53, 6] = "nzzi"; Stnt[53, 7] = "nltuji"; Stnt[53, 8] = "nltuzi";
            Stnt[125, 1] = "nnjji"; Stnt[125, 2] = "nnzzi"; Stnt[125, 3] = "nnltuji"; Stnt[125, 4] = "nnltuzi"; Stnt[125, 5] = "njji"; Stnt[125, 6] = "nzzi"; Stnt[125, 7] = "nltuji"; Stnt[125, 8] = "nltuzi";

            if (nn == 1)
            {
                Stn[49, 1] = "nji"; Stn[49, 2] = "nzi"; Stn[49, 5] = "nnji"; Stn[49, 6] = "nnzi";
                Stn[117, 1] = "nji"; Stn[117, 2] = "nzi"; Stn[117, 5] = "nnji"; Stn[117, 6] = "nnzi";

                Stnt[53, 1] = "njji"; Stnt[53, 2] = "nzzi"; Stnt[53, 3] = "nltuji"; Stnt[53, 4] = "nltuzi"; Stnt[53, 5] = "nnjji"; Stnt[53, 6] = "nnzzi"; Stnt[53, 7] = "nnltuji"; Stnt[53, 8] = "nnltuzi";
                Stnt[125, 1] = "njji"; Stnt[125, 2] = "nzzi"; Stnt[125, 3] = "nltuji"; Stnt[125, 4] = "nltuzi"; Stnt[125, 5] = "nnjji"; Stnt[125, 6] = "nnzzi"; Stnt[125, 7] = "nnltuji"; Stnt[125, 8] = "nnltuzi";
            }
        }
        else
        {
            //ZI
            St[64, 1] = "zi"; St[64, 2] = "ji";
            St[145, 1] = "zi"; St[145, 2] = "ji";

            Stt[53, 1] = "zzi"; Stt[53, 2] = "jji"; Stt[53, 3] = "ltuzi"; Stt[53, 4] = "ltuji";
            Stt[125, 1] = "zzi"; Stt[125, 2] = "jji"; Stt[125, 3] = "ltuzi"; Stt[125, 4] = "ltuji";

            Stn[49, 1] = "nnzi"; Stn[49, 2] = "nnji"; Stn[49, 5] = "nzi"; Stn[49, 6] = "nji";
            Stn[117, 1] = "nnzi"; Stn[117, 2] = "nnji"; Stn[117, 5] = "nzi"; Stn[117, 6] = "nji";

            Stnt[53, 1] = "nnzzi"; Stnt[53, 2] = "nnjji"; Stnt[53, 3] = "nnltuzi"; Stnt[53, 4] = "nnltuji"; Stnt[53, 5] = "nzzi"; Stnt[53, 6] = "njji"; Stnt[53, 7] = "nltuzi"; Stnt[53, 8] = "nltuji";
            Stnt[125, 1] = "nnzzi"; Stnt[125, 2] = "nnjji"; Stnt[125, 3] = "nnltuzi"; Stnt[125, 4] = "nnltuji"; Stnt[125, 5] = "nzzi"; Stnt[125, 6] = "njji"; Stnt[125, 7] = "nltuzi"; Stnt[125, 8] = "nltuji";

            if (nn == 1)
            {
                Stn[49, 1] = "nzi"; Stn[49, 2] = "nji"; Stn[49, 5] = "nnzi"; Stn[49, 6] = "nnji";
                Stn[117, 1] = "nzi"; Stn[117, 2] = "nji"; Stn[117, 5] = "nnzi"; Stn[117, 6] = "nnji";

                Stnt[53, 1] = "nzzi"; Stnt[53, 2] = "njji"; Stnt[53, 3] = "nltuzi"; Stnt[53, 4] = "nltuji"; Stnt[53, 5] = "nnzzi"; Stnt[53, 6] = "nnjji"; Stnt[53, 7] = "nnltuzi"; Stnt[53, 8] = "nnltuji";
                Stnt[125, 1] = "nzzi"; Stnt[125, 2] = "njji"; Stnt[125, 3] = "nltuzi"; Stnt[125, 4] = "nltuji"; Stnt[125, 5] = "nnzzi"; Stnt[125, 6] = "nnjji"; Stnt[125, 7] = "nnltuzi"; Stnt[125, 8] = "nnltuji";
            }
        }
    }
    public void SetHU(int type, int nn)
    {
        if (type == 1)
        {
            //HU
            St[27, 1] = "hu"; St[27, 2] = "fu";
            St[110, 1] = "hu"; St[110, 2] = "fu";
            Stt[17, 1] = "hhu"; Stt[17, 2] = "ffu"; Stt[17, 3] = "ltuhu"; Stt[17, 4] = "ltufu";
            Stt[89, 1] = "hhu"; Stt[89, 2] = "ffu"; Stt[89, 3] = "ltuhu"; Stt[89, 4] = "ltufu";
            Stn[17, 1] = "nnhu"; Stn[17, 2] = "nnfu"; Stn[17, 5] = "nhu"; Stn[17, 6] = "nfu";
            Stn[85, 1] = "nnhu"; Stn[85, 2] = "nnfu"; Stn[85, 5] = "nhu"; Stn[85, 6] = "nfu";
            Stnt[17, 1] = "nnhhu"; Stnt[17, 2] = "nnffu"; Stnt[17, 3] = "nnltuhu"; Stnt[17, 4] = "nnltufu"; Stnt[17, 5] = "nhhu"; Stnt[17, 6] = "nffu"; Stnt[17, 7] = "nltuhu"; Stnt[17, 8] = "nltufu";
            Stnt[89, 1] = "nnhhu"; Stnt[89, 2] = "nnffu"; Stnt[17, 3] = "nnltuhu"; Stnt[89, 4] = "nnltufu"; Stnt[89, 5] = "nhhu"; Stnt[89, 6] = "nffu"; Stnt[89, 7] = "nltuhu"; Stnt[89, 8] = "nltufu";
            if (nn == 1)
            {
                Stn[17, 1] = "nhu"; Stn[17, 2] = "nfu"; Stn[17, 5] = "nnhu"; Stn[17, 6] = "nnfu";
                Stn[85, 1] = "nhu"; Stn[85, 2] = "nfu"; Stn[85, 5] = "nnhu"; Stn[85, 6] = "nnfu";
                Stnt[17, 1] = "nhhu"; Stnt[17, 2] = "nffu"; Stnt[17, 3] = "nltuhu"; Stnt[17, 4] = "nltufu"; Stnt[17, 5] = "nnhhu"; Stnt[17, 6] = "nnffu"; Stnt[17, 7] = "nnltuhu"; Stnt[17, 8] = "nnltufu";
                Stnt[89, 1] = "nhhu"; Stnt[89, 2] = "nffu"; Stnt[17, 3] = "nltuhu"; Stnt[89, 4] = "nltufu"; Stnt[89, 5] = "nnhhu"; Stnt[89, 6] = "nnffu"; Stnt[89, 7] = "nnltuhu"; Stnt[89, 8] = "nnltufu";
            }
        }
        else
        {
            //FU
            St[27, 1] = "fu"; St[27, 2] = "hu";
            St[110, 1] = "fu"; St[110, 2] = "hu";
            Stt[17, 1] = "ffu"; Stt[17, 2] = "hhu"; Stt[17, 3] = "ltufu"; Stt[17, 4] = "ltuhu";
            Stt[89, 1] = "ffu"; Stt[89, 2] = "hhu"; Stt[89, 3] = "ltufu"; Stt[89, 4] = "ltuhu";
            Stn[17, 1] = "nnfu"; Stn[17, 2] = "nnhu"; Stn[17, 5] = "nfu"; Stn[17, 6] = "nhu";
            Stn[85, 1] = "nnfu"; Stn[85, 2] = "nnhu"; Stn[85, 5] = "nfu"; Stn[85, 6] = "nhu";
            Stnt[17, 1] = "nnffu"; Stnt[17, 2] = "nnhhu"; Stnt[17, 3] = "nnltufu"; Stnt[17, 4] = "nnltuhu"; Stnt[17, 5] = "nffu"; Stnt[17, 6] = "nhhu"; Stnt[17, 7] = "nltufu"; Stnt[17, 8] = "nltuhu";
            Stnt[89, 1] = "nnffu"; Stnt[89, 2] = "nnhhu"; Stnt[89, 3] = "nnltufu"; Stnt[17, 4] = "nnltuhu"; Stnt[89, 5] = "nffu"; Stnt[89, 6] = "nhhu"; Stnt[89, 7] = "nltufu"; Stnt[89, 8] = "nltuhu";
            if (nn == 1)
            {
                Stn[17, 1] = "nfu"; Stn[17, 2] = "nhu"; Stn[17, 5] = "nnfu"; Stn[17, 6] = "nnhu";
                Stn[85, 1] = "nfu"; Stn[85, 2] = "nhu"; Stn[85, 5] = "nnfu"; Stn[85, 6] = "nnhu";
                Stnt[17, 1] = "nffu"; Stnt[17, 2] = "nhhu"; Stnt[17, 3] = "nltufu"; Stnt[17, 4] = "nltuhu"; Stnt[17, 5] = "nnffu"; Stnt[17, 6] = "nnhhu"; Stnt[17, 7] = "nnltufu"; Stnt[17, 8] = "nnltuhu";
                Stnt[89, 1] = "nffu"; Stnt[89, 2] = "nhhu"; Stnt[89, 3] = "nltufu"; Stnt[17, 4] = "nltuhu"; Stnt[89, 5] = "nnffu"; Stnt[89, 6] = "nnhhu"; Stnt[89, 7] = "nnltufu"; Stnt[89, 8] = "nnltuhu";
            }
        }
    }
    public void SetTI(int type, int nn)
    {
        if (type == 1)
        {
            //TI
            St[16, 1] = "ti"; St[16, 2] = "chi";
            St[99, 1] = "ti"; St[99, 2] = "chi";

            Stt[11, 1] = "tti"; Stt[11, 2] = "cchi"; Stt[11, 3] = "ltuti"; Stt[11, 4] = "ltuchi";
            Stt[83, 1] = "tti"; Stt[83, 2] = "cchi"; Stt[83, 3] = "ltuti"; Stt[83, 4] = "ltuchi";

            Stn[11, 1] = "nnti"; Stn[11, 2] = "nnchi"; Stn[11, 5] = "nti"; Stn[11, 6] = "nchi";
            Stn[79, 1] = "nnti"; Stn[79, 2] = "nnchi"; Stn[79, 5] = "nti"; Stn[79, 6] = "nchi";

            Stnt[11, 1] = "nntti"; Stnt[11, 2] = "nncchi"; Stnt[11, 3] = "nnltuti"; Stnt[11, 4] = "nnltuchi"; Stnt[11, 5] = "ntti"; Stnt[11, 6] = "ncchi"; Stnt[11, 7] = "nltuti"; Stnt[11, 8] = "nltuchi";
            Stnt[83, 1] = "nntti"; Stnt[83, 2] = "nncchi"; Stnt[83, 3] = "nnltuti"; Stnt[83, 4] = "nnltuchi"; Stnt[83, 5] = "ntti"; Stnt[83, 6] = "ncchi"; Stnt[83, 7] = "nltuti"; Stnt[83, 8] = "nltuchi";
            if (nn == 1)
            {
                Stn[11, 1] = "nti"; Stn[11, 2] = "nchi"; Stn[11, 5] = "nnti"; Stn[11, 6] = "nnchi";
                Stn[79, 1] = "nti"; Stn[79, 2] = "nchi"; Stn[79, 5] = "nnti"; Stn[79, 6] = "nnchi";

                Stnt[11, 1] = "ntti"; Stnt[11, 2] = "ncchi"; Stnt[11, 3] = "nltuti"; Stnt[11, 4] = "nltuchi"; Stnt[11, 5] = "nntti"; Stnt[11, 6] = "nncchi"; Stnt[11, 7] = "nnltuti"; Stnt[11, 8] = "nnltuchi";
                Stnt[83, 1] = "ntti"; Stnt[83, 2] = "ncchi"; Stnt[83, 3] = "nltuti"; Stnt[83, 4] = "nltuchi"; Stnt[83, 5] = "nntti"; Stnt[83, 6] = "nncchi"; Stnt[83, 7] = "nnltuti"; Stnt[83, 8] = "nnltuchi";
            }
        }
        else
        {
            //CHI
            St[16, 1] = "chi"; St[16, 2] = "ti";
            St[99, 1] = "chi"; St[99, 2] = "ti";

            Stt[11, 1] = "cchi"; Stt[11, 2] = "tti"; Stt[11, 3] = "ltuchi"; Stt[11, 4] = "ltuti";
            Stt[83, 1] = "cchi"; Stt[83, 2] = "tti"; Stt[83, 3] = "ltuchi"; Stt[83, 4] = "ltuti";

            Stn[11, 1] = "nnchi"; Stn[11, 2] = "nnti"; Stn[11, 5] = "nchi"; Stn[11, 6] = "nti";
            Stn[79, 1] = "nnchi"; Stn[79, 2] = "nnti"; Stn[79, 5] = "nchi"; Stn[79, 6] = "nti";

            Stnt[11, 1] = "nncchi"; Stnt[11, 2] = "nntti"; Stnt[11, 3] = "nnltuchi"; Stnt[11, 4] = "nnltuti"; Stnt[11, 5] = "ncchi"; Stnt[11, 6] = "ntti"; Stnt[11, 7] = "nltuchi"; Stnt[11, 8] = "nltuti";
            Stnt[83, 1] = "nncchi"; Stnt[83, 2] = "nntti"; Stnt[83, 3] = "nnltuchi"; Stnt[83, 4] = "nnltuti"; Stnt[83, 5] = "ncchi"; Stnt[83, 6] = "ntti"; Stnt[83, 7] = "nltuchi"; Stnt[83, 8] = "nltuti";
            if (nn == 1)
            {
                Stn[11, 1] = "nchi"; Stn[11, 2] = "nti"; Stn[11, 5] = "nnchi"; Stn[11, 6] = "nnti";
                Stn[79, 1] = "nchi"; Stn[79, 2] = "nti"; Stn[79, 5] = "nnchi"; Stn[79, 6] = "nnti";

                Stnt[11, 1] = "ncchi"; Stnt[11, 2] = "ntti"; Stnt[11, 3] = "nltuchi"; Stnt[11, 4] = "nltuti"; Stnt[11, 5] = "nncchi"; Stnt[11, 6] = "nntti"; Stnt[11, 7] = "nnltuchi"; Stnt[11, 8] = "nnltuti";
                Stnt[83, 1] = "ncchi"; Stnt[83, 2] = "ntti"; Stnt[83, 3] = "nltuchi"; Stnt[83, 4] = "nltuti"; Stnt[83, 5] = "nncchi"; Stnt[83, 6] = "nntti"; Stnt[83, 7] = "nnltuchi"; Stnt[83, 8] = "nnltuti";
            }
        }
    }
    public void SetSI(int type, int nn)
    {
        if (type == 1)
        {
            //SI
            St[11, 1] = "si"; St[11, 2] = "shi"; St[11, 3] = "ci";
            St[94, 1] = "si"; St[94, 2] = "shi"; St[94, 3] = "ci";

            Stt[6, 1] = "ssi"; Stt[6, 2] = "sshi"; Stt[6, 3] = "cci"; Stt[6, 4] = "ltusi"; Stt[6, 5] = "ltushi";
            Stt[78, 1] = "ssi"; Stt[78, 2] = "sshi"; Stt[78, 3] = "cci"; Stt[78, 4] = "ltusi"; Stt[78, 5] = "ltushi";

            Stn[6, 1] = "nnsi"; Stn[6, 2] = "nnshi"; Stn[6, 3] = "nnci"; Stn[6, 5] = "nsi"; Stn[6, 6] = "nshi"; Stn[6, 7] = "nci";
            Stn[74, 1] = "nnsi"; Stn[74, 2] = "nnshi"; Stn[74, 3] = "nnci"; Stn[74, 5] = "nsi"; Stn[74, 6] = "nshi"; Stn[74, 7] = "nci";

            Stnt[6, 1] = "nnssi"; Stnt[6, 2] = "nnsshi"; Stnt[6, 3] = "nncci"; Stnt[6, 4] = "nnltusi"; Stnt[6, 5] = "nnltushi"; Stnt[6, 6] = "nssi"; Stnt[6, 7] = "nsshi"; Stnt[6, 8] = "ncci"; Stnt[6, 9] = "nltusi"; Stnt[6, 10] = "nltushi";
            Stnt[78, 1] = "nnssi"; Stnt[78, 2] = "nnsshi"; Stnt[78, 3] = "nncci"; Stnt[78, 4] = "nnltusi"; Stnt[78, 5] = "nnltushi"; Stnt[78, 6] = "nssi"; Stnt[78, 7] = "nsshi"; Stnt[78, 8] = "ncci"; Stnt[78, 9] = "nltusi"; Stnt[78, 10] = "nltushi";
            if (nn == 1)
            {
                Stn[6, 1] = "nsi"; Stn[6, 2] = "nshi"; Stn[6, 3] = "nci"; Stn[6, 5] = "nnsi"; Stn[6, 6] = "nnshi"; Stn[6, 7] = "nnci";
                Stn[74, 1] = "nsi"; Stn[74, 2] = "nshi"; Stn[74, 3] = "nci"; Stn[74, 5] = "nnsi"; Stn[74, 6] = "nnshi"; Stn[74, 7] = "nnci";

                Stnt[6, 1] = "nssi"; Stnt[6, 2] = "nsshi"; Stnt[6, 3] = "ncci"; Stnt[6, 4] = "nltusi"; Stnt[6, 5] = "nltushi"; Stnt[6, 6] = "nnssi"; Stnt[6, 7] = "nnsshi"; Stnt[6, 8] = "nncci"; Stnt[6, 9] = "nnltusi"; Stnt[6, 10] = "nnltushi";
                Stnt[78, 1] = "nssi"; Stnt[78, 2] = "nsshi"; Stnt[78, 3] = "ncci"; Stnt[78, 4] = "nltusi"; Stnt[78, 5] = "nltushi"; Stnt[78, 6] = "nnssi"; Stnt[78, 7] = "nnsshi"; Stnt[78, 8] = "nncci"; Stnt[78, 9] = "nnltusi"; Stnt[78, 10] = "nnltushi";
            }
        }
        else
        {
            //SHI
            St[11, 1] = "shi"; St[11, 2] = "si"; St[11, 3] = "ci";
            St[94, 1] = "shi"; St[94, 2] = "si"; St[94, 3] = "ci";

            Stt[6, 1] = "sshi"; Stt[6, 2] = "ssi"; Stt[6, 3] = "cci"; Stt[6, 4] = "ltushi"; Stt[6, 5] = "ltusi";
            Stt[78, 1] = "sshi"; Stt[78, 2] = "ssi"; Stt[78, 3] = "cci"; Stt[78, 4] = "ltushi"; Stt[78, 5] = "ltusi";

            Stn[6, 1] = "nnshi"; Stn[6, 2] = "nnsi"; Stn[6, 3] = "nnci"; Stn[6, 5] = "nshi"; Stn[6, 6] = "nsi"; Stn[6, 7] = "nci";
            Stn[74, 1] = "nnshi"; Stn[74, 2] = "nnsi"; Stn[74, 3] = "nnci"; Stn[74, 5] = "nshi"; Stn[74, 6] = "nsi"; Stn[74, 7] = "nci";

            Stnt[6, 1] = "nnsshi"; Stnt[6, 2] = "nnssi"; Stnt[6, 3] = "nncci"; Stnt[6, 4] = "nnltushi"; Stnt[6, 5] = "nnltusi"; Stnt[6, 6] = "nsshi"; Stnt[6, 7] = "nssi"; Stnt[6, 8] = "ncci"; Stnt[6, 9] = "nltushi"; Stnt[6, 10] = "nltusi";
            Stnt[78, 1] = "nnsshi"; Stnt[78, 2] = "nnssi"; Stnt[78, 3] = "nncci"; Stnt[78, 4] = "nnltushi"; Stnt[78, 5] = "nnltusi"; Stnt[78, 6] = "nsshi"; Stnt[78, 7] = "nssi"; Stnt[78, 8] = "ncci"; Stnt[78, 9] = "nltushi"; Stnt[78, 10] = "nltusi";
            if (nn == 1)
            {
                Stn[6, 1] = "nshi"; Stn[6, 2] = "nsi"; Stn[6, 3] = "nci"; Stn[6, 5] = "nnshi"; Stn[6, 6] = "nnsi"; Stn[6, 7] = "nnci";
                Stn[74, 1] = "nshi"; Stn[74, 2] = "nsi"; Stn[74, 3] = "nci"; Stn[74, 5] = "nnshi"; Stn[74, 6] = "nnsi"; Stn[74, 7] = "nnci";

                Stnt[6, 1] = "nsshi"; Stnt[6, 2] = "nssi"; Stnt[6, 3] = "ncci"; Stnt[6, 4] = "nltushi"; Stnt[6, 5] = "nltusi"; Stnt[6, 6] = "nnsshi"; Stnt[6, 7] = "nnssi"; Stnt[6, 8] = "nncci"; Stnt[6, 9] = "nnltushi"; Stnt[6, 10] = "nnltusi";
                Stnt[78, 1] = "nsshi"; Stnt[78, 2] = "nssi"; Stnt[78, 3] = "ncci"; Stnt[78, 4] = "nltushi"; Stnt[78, 5] = "nltusi"; Stnt[78, 6] = "nnsshi"; Stnt[78, 7] = "nnssi"; Stnt[78, 8] = "nncci"; Stnt[78, 9] = "nnltushi"; Stnt[78, 10] = "nnltusi";
            }
        }
    }
    public void SetTU(int type, int nn)
    {
        if (type == 1)
        {
            //TU
            St[17, 1] = "tu"; St[17, 2] = "tsu";
            St[100, 1] = "tu"; St[100, 2] = "tsu";

            Stt[12, 1] = "ttu"; Stt[12, 2] = "ttsu"; Stt[12, 3] = "ltutu"; Stt[12, 4] = "ltutsu";
            Stt[84, 1] = "ttu"; Stt[84, 2] = "ttsu"; Stt[84, 3] = "ltutu"; Stt[84, 4] = "ltutsu";

            Stn[12, 1] = "nntu"; Stn[12, 2] = "nntsu"; Stn[12, 5] = "ntu"; Stn[12, 6] = "ntsu";
            Stn[80, 1] = "nntu"; Stn[80, 2] = "nntsu"; Stn[80, 5] = "ntu"; Stn[80, 6] = "ntsu";

            Stnt[12, 1] = "nnttu"; Stnt[12, 2] = "nnttsu"; Stnt[12, 3] = "nnltutu"; Stnt[12, 4] = "nnltutsu"; Stnt[12, 5] = "nttu"; Stnt[12, 6] = "nttsu"; Stnt[12, 7] = "nltutu"; Stnt[12, 8] = "nltutsu";
            Stnt[84, 1] = "nnttu"; Stnt[84, 2] = "nnttsu"; Stnt[84, 3] = "nnltutu"; Stnt[84, 4] = "nnltutsu"; Stnt[84, 5] = "nttu"; Stnt[84, 6] = "nttsu"; Stnt[84, 7] = "nltutu"; Stnt[84, 8] = "nltutsu";

            if (nn == 1)
            {
                Stn[12, 1] = "ntu"; Stn[12, 2] = "ntsu"; Stn[12, 5] = "nntu"; Stn[12, 6] = "nntsu";
                Stn[80, 1] = "ntu"; Stn[80, 2] = "ntsu"; Stn[80, 5] = "nntu"; Stn[80, 6] = "nntsu";

                Stnt[12, 1] = "nttu"; Stnt[12, 2] = "nttsu"; Stnt[12, 3] = "nltutu"; Stnt[12, 4] = "nltutsu"; Stnt[12, 5] = "nnttu"; Stnt[12, 6] = "nnttsu"; Stnt[12, 7] = "nnltutu"; Stnt[12, 8] = "nnltutsu";
                Stnt[84, 1] = "nttu"; Stnt[84, 2] = "nttsu"; Stnt[84, 3] = "nltutu"; Stnt[84, 4] = "nltutsu"; Stnt[84, 5] = "nnttu"; Stnt[84, 6] = "nnttsu"; Stnt[84, 7] = "nnltutu"; Stnt[84, 8] = "nnltutsu";
            }
        }
        else
        {
            //TSU
            St[17, 1] = "tsu"; St[17, 2] = "tu";
            St[100, 1] = "tsu"; St[100, 2] = "tu";

            Stt[12, 1] = "ttsu"; Stt[12, 2] = "ttu"; Stt[12, 3] = "ltutsu"; Stt[12, 4] = "ltutu";
            Stt[84, 1] = "ttsu"; Stt[84, 2] = "ttu"; Stt[84, 3] = "ltutsu"; Stt[84, 4] = "ltutu";

            Stn[12, 1] = "nntsu"; Stn[12, 2] = "nntu"; Stn[12, 5] = "ntsu"; Stn[12, 6] = "ntu";
            Stn[80, 1] = "nntsu"; Stn[80, 2] = "nntu"; Stn[80, 5] = "ntsu"; Stn[80, 6] = "ntu";

            Stnt[12, 1] = "nnttsu"; Stnt[12, 2] = "nnttu"; Stnt[12, 3] = "nnltutsu"; Stnt[12, 4] = "nnltutu"; Stnt[12, 5] = "nttsu"; Stnt[12, 6] = "nttu"; Stnt[12, 7] = "nltutsu"; Stnt[12, 8] = "nltutu";
            Stnt[84, 1] = "nnttsu"; Stnt[84, 2] = "nnttu"; Stnt[84, 3] = "nnltutsu"; Stnt[84, 4] = "nnltutu"; Stnt[84, 5] = "nttsu"; Stnt[84, 6] = "nttu"; Stnt[84, 7] = "nltutsu"; Stnt[84, 8] = "nltutu";

            if (nn == 1)
            {
                Stn[12, 1] = "ntsu"; Stn[12, 2] = "ntu"; Stn[12, 5] = "nntsu"; Stn[12, 6] = "nntu";
                Stn[80, 1] = "ntsu"; Stn[80, 2] = "ntu"; Stn[80, 5] = "nntsu"; Stn[80, 6] = "nntu";

                Stnt[12, 1] = "nttsu"; Stnt[12, 2] = "nttu"; Stnt[12, 3] = "nltutsu"; Stnt[12, 4] = "nltutu"; Stnt[12, 5] = "nnttsu"; Stnt[12, 6] = "nnttu"; Stnt[12, 7] = "nnltutsu"; Stnt[12, 8] = "nnltutu";
                Stnt[84, 1] = "nttsu"; Stnt[84, 2] = "nttu"; Stnt[84, 3] = "nltutsu"; Stnt[84, 4] = "nltutu"; Stnt[84, 5] = "nnttsu"; Stnt[84, 6] = "nnttu"; Stnt[84, 7] = "nnltutsu"; Stnt[84, 8] = "nnltutu";
            }
        }
    }
    public void SetN(int type)
    {
        if (type == 1)
        {
            //N?i?q????O?j
            Stn[0, 1] = "nka"; Stn[0, 2] = "nca"; Stn[0, 5] = "nnka"; Stn[0, 6] = "nnca";
            Stn[1, 1] = "nki"; Stn[1, 5] = "nnki";
            Stn[2, 1] = "nku"; Stn[2, 2] = "ncu"; Stn[2, 3] = "nqu"; Stn[2, 5] = "nnku"; Stn[2, 6] = "nncu"; Stn[2, 7] = "nnqu";
            Stn[3, 1] = "nke"; Stn[3, 5] = "nnke";
            Stn[4, 1] = "nko"; Stn[4, 2] = "nco"; Stn[4, 5] = "nnko"; Stn[4, 6] = "nnco";
            Stn[5, 1] = "nsa"; Stn[5, 5] = "nnsa";
            Stn[6, 1] = "nsi"; Stn[6, 2] = "nshi"; Stn[6, 3] = "nci"; Stn[6, 5] = "nnsi"; Stn[6, 6] = "nnshi"; Stn[6, 7] = "nnci";
            Stn[7, 1] = "nsu"; Stn[7, 5] = "nnsu";
            Stn[8, 1] = "nse"; Stn[8, 2] = "nce"; Stn[8, 5] = "nnse"; Stn[8, 6] = "nnce";
            Stn[9, 1] = "nso"; Stn[9, 5] = "nnso";
            Stn[10, 1] = "nta"; Stn[10, 5] = "nnta";
            Stn[11, 1] = "nti"; Stn[11, 2] = "nchi"; Stn[11, 5] = "nnti"; Stn[11, 6] = "nnchi";
            Stn[12, 1] = "ntu"; Stn[12, 2] = "ntsu"; Stn[12, 5] = "nntu"; Stn[12, 6] = "nntsu";
            Stn[13, 1] = "nte"; Stn[13, 5] = "nnte";
            Stn[14, 1] = "nto"; Stn[14, 5] = "nnto";
            Stn[15, 1] = "nha"; Stn[15, 5] = "nnha";
            Stn[16, 1] = "nhi"; Stn[16, 5] = "nnhi";
            Stn[17, 1] = "nhu"; Stn[17, 2] = "nfu"; Stn[17, 5] = "nnhu"; Stn[17, 6] = "nnfu";
            Stn[18, 1] = "nhe"; Stn[18, 5] = "nnhe";
            Stn[19, 1] = "nho"; Stn[19, 5] = "nnho";
            Stn[20, 1] = "nma"; Stn[20, 5] = "nnma";
            Stn[21, 1] = "nmi"; Stn[21, 5] = "nnmi";
            Stn[22, 1] = "nmu"; Stn[22, 5] = "nnmu";
            Stn[23, 1] = "nme"; Stn[23, 5] = "nnme";
            Stn[24, 1] = "nmo"; Stn[24, 5] = "nnmo";
            Stn[25, 1] = "nra"; Stn[25, 5] = "nnra";
            Stn[26, 1] = "nri"; Stn[26, 5] = "nnri";
            Stn[27, 1] = "nru"; Stn[27, 5] = "nnru";
            Stn[28, 1] = "nre"; Stn[28, 5] = "nnre";
            Stn[29, 1] = "nro"; Stn[29, 5] = "nnro";
            Stn[30, 1] = "nwa"; Stn[30, 5] = "nnwa";
            Stn[31, 1] = "nwo"; Stn[31, 5] = "nnwo";
            Stn[32, 1] = "nla"; Stn[32, 2] = "nxa"; Stn[32, 5] = "nnla"; Stn[32, 6] = "nnxa";
            Stn[33, 1] = "nli"; Stn[33, 2] = "nxi"; Stn[33, 3] = "nlyi"; Stn[33, 4] = "nxyi"; Stn[33, 5] = "nnli"; Stn[33, 6] = "nnxi"; Stn[33, 7] = "nnlyi"; Stn[33, 8] = "nnxyi";
            Stn[34, 1] = "nlu"; Stn[34, 2] = "nxu"; Stn[34, 5] = "nnlu"; Stn[34, 6] = "nnxu";
            Stn[35, 1] = "nle"; Stn[35, 2] = "nxe"; Stn[35, 3] = "nlye"; Stn[35, 4] = "nxye"; Stn[35, 5] = "nnle"; Stn[35, 6] = "nnxe"; Stn[35, 7] = "nnlye"; Stn[35, 8] = "nnxye";
            Stn[36, 1] = "nlo"; Stn[36, 2] = "nxo"; Stn[36, 5] = "nnlo"; Stn[36, 6] = "nnxo";
            Stn[37, 1] = "nlya"; Stn[37, 2] = "nxya"; Stn[37, 5] = "nnlya"; Stn[37, 6] = "nnxya";
            Stn[38, 1] = "nlyu"; Stn[38, 2] = "nxyu"; Stn[38, 5] = "nnlyu"; Stn[38, 6] = "nnxyu";
            Stn[39, 1] = "nlyo"; Stn[39, 2] = "nxyo"; Stn[39, 5] = "nnlyo"; Stn[39, 6] = "nnxyo";
            Stn[40, 1] = "nlwa"; Stn[40, 2] = "nxwa"; Stn[40, 5] = "nnlwa"; Stn[40, 6] = "nnxwa";
            Stn[41, 1] = "nlka"; Stn[41, 2] = "nxka"; Stn[41, 5] = "nnlka"; Stn[41, 6] = "nnxka";
            Stn[42, 1] = "nlke"; Stn[42, 2] = "nxke"; Stn[42, 5] = "nnlke"; Stn[42, 6] = "nnxke";
            Stn[43, 1] = "nga"; Stn[43, 5] = "nnga";
            Stn[44, 1] = "ngi"; Stn[44, 5] = "nngi";
            Stn[45, 1] = "ngu"; Stn[45, 5] = "nngu";
            Stn[46, 1] = "nge"; Stn[46, 5] = "nnge";
            Stn[47, 1] = "ngo"; Stn[47, 5] = "nngo";
            Stn[48, 1] = "nza"; Stn[48, 5] = "nnza";
            Stn[49, 1] = "nji"; Stn[49, 2] = "nzi"; Stn[49, 5] = "nnji"; Stn[49, 6] = "nnzi";
            Stn[50, 1] = "nzu"; Stn[50, 5] = "nnzu";
            Stn[51, 1] = "nze"; Stn[51, 5] = "nnze";
            Stn[52, 1] = "nzo"; Stn[52, 5] = "nnzo";
            Stn[53, 1] = "nda"; Stn[53, 5] = "nnda";
            Stn[54, 1] = "ndi"; Stn[54, 5] = "nndi";
            Stn[55, 1] = "ndu"; Stn[55, 5] = "nndu";
            Stn[56, 1] = "nde"; Stn[56, 5] = "nnde";
            Stn[57, 1] = "ndo"; Stn[57, 5] = "nndo";
            Stn[58, 1] = "nba"; Stn[58, 5] = "nnba";
            Stn[59, 1] = "nbi"; Stn[59, 5] = "nnbi";
            Stn[60, 1] = "nbu"; Stn[60, 5] = "nnbu";
            Stn[61, 1] = "nbe"; Stn[61, 5] = "nnbe";
            Stn[62, 1] = "nbo"; Stn[62, 5] = "nnbo";
            Stn[63, 1] = "npa"; Stn[63, 5] = "nnpa";
            Stn[64, 1] = "npi"; Stn[64, 5] = "nnpi";
            Stn[65, 1] = "npu"; Stn[65, 5] = "nnpu";
            Stn[66, 1] = "npe"; Stn[66, 5] = "nnpe";
            Stn[67, 1] = "npo"; Stn[67, 5] = "nnpo";
            for (int i = STN_COL / 2; i < STN_COL; i++)
            {
                for (int k = 1; k < STN_ROW; k++)
                {
                    Stn[i, k] = Stn[i - (STN_COL / 2), k];
                }
            }

            Stnex[0, 1] = "nwha"; Stnex[0, 4] = "nnwha";
            Stnex[1, 1] = "nwi"; Stnex[1, 2] = "nwhi"; Stnex[1, 4] = "nnwi"; Stnex[1, 5] = "nnwhi";
            Stnex[2, 1] = "nwe"; Stnex[2, 2] = "nwhe"; Stnex[2, 4] = "nnwe"; Stnex[2, 5] = "nnwhe";
            Stnex[3, 1] = "nwho"; Stnex[3, 4] = "nnwho";
            Stnex[4, 1] = "nkyi"; Stnex[4, 4] = "nnkyi";
            Stnex[5, 1] = "nkye"; Stnex[5, 4] = "nnkye";
            Stnex[6, 1] = "nkya"; Stnex[6, 4] = "nnkya";
            Stnex[7, 1] = "nkyu"; Stnex[7, 4] = "nnkyu";
            Stnex[8, 1] = "nkyo"; Stnex[8, 4] = "nnkyo";
            Stnex[9, 1] = "ngyi"; Stnex[9, 4] = "nngyi";
            Stnex[10, 1] = "ngye"; Stnex[10, 4] = "nngye";
            Stnex[11, 1] = "ngya"; Stnex[11, 4] = "nngya";
            Stnex[12, 1] = "ngyu"; Stnex[12, 4] = "nngyu";
            Stnex[13, 1] = "ngyo"; Stnex[13, 4] = "nngyo";
            Stnex[14, 1] = "nqa"; Stnex[14, 2] = "nqwa"; Stnex[14, 3] = "nkwa"; Stnex[14, 4] = "nnqa"; Stnex[14, 5] = "nnqwa"; Stnex[14, 6] = "nnkwa";
            Stnex[15, 1] = "nqi"; Stnex[15, 2] = "nqwi"; Stnex[15, 3] = "nqyi"; Stnex[15, 4] = "nnqi"; Stnex[15, 5] = "nnqwi"; Stnex[15, 6] = "nnqyi";
            Stnex[16, 1] = "nqwu"; Stnex[16, 4] = "nnqwu";
            Stnex[17, 1] = "nqe"; Stnex[17, 2] = "nqwe"; Stnex[17, 3] = "nqye"; Stnex[17, 4] = "nnqe"; Stnex[17, 5] = "nnqwe"; Stnex[17, 6] = "nnqye";
            Stnex[18, 1] = "nqo"; Stnex[18, 2] = "nqwo"; Stnex[18, 4] = "nnqo"; Stnex[18, 5] = "nnqwo";
            Stnex[19, 1] = "nqya"; Stnex[19, 4] = "nnqya";
            Stnex[20, 1] = "nqyu"; Stnex[20, 4] = "nnqyu";
            Stnex[21, 1] = "nqyo"; Stnex[21, 4] = "nnqyo";
            Stnex[22, 1] = "ngwa"; Stnex[22, 4] = "nngwa";
            Stnex[23, 1] = "ngwi"; Stnex[23, 4] = "nngwi";
            Stnex[24, 1] = "ngwu"; Stnex[24, 4] = "nngwu";
            Stnex[25, 1] = "ngwe"; Stnex[25, 4] = "nngwe";
            Stnex[26, 1] = "ngwo"; Stnex[26, 4] = "nngwo";
            Stnex[27, 1] = "nsyi"; Stnex[27, 4] = "nnsyi";
            Stnex[28, 1] = "nsye"; Stnex[28, 2] = "nshe"; Stnex[28, 4] = "nnsye"; Stnex[28, 5] = "nnshe";
            Stnex[29, 1] = "nsya"; Stnex[29, 2] = "nsha"; Stnex[29, 4] = "nnsya"; Stnex[29, 5] = "nnsha";
            Stnex[30, 1] = "nsyu"; Stnex[30, 2] = "nshu"; Stnex[30, 4] = "nnsyu"; Stnex[30, 5] = "nnshu";
            Stnex[31, 1] = "nsyo"; Stnex[31, 2] = "nsho"; Stnex[31, 4] = "nnsyo"; Stnex[31, 5] = "nnsho";
            Stnex[32, 1] = "njyi"; Stnex[32, 2] = "nzyi"; Stnex[32, 4] = "nnjyi"; Stnex[32, 5] = "nnzyi";
            Stnex[33, 1] = "nje"; Stnex[33, 2] = "njye"; Stnex[33, 3] = "nzye"; Stnex[33, 4] = "nnje"; Stnex[33, 5] = "nnjye"; Stnex[33, 6] = "nnzye";
            Stnex[34, 1] = "nja"; Stnex[34, 2] = "njya"; Stnex[34, 3] = "nzya"; Stnex[34, 4] = "nnja"; Stnex[34, 5] = "nnjya"; Stnex[34, 6] = "nnzya";
            Stnex[35, 1] = "nju"; Stnex[35, 2] = "njyu"; Stnex[35, 3] = "nzyu"; Stnex[35, 4] = "nnju"; Stnex[35, 5] = "nnjyu"; Stnex[35, 6] = "nnzyu";
            Stnex[36, 1] = "njo"; Stnex[36, 2] = "njyo"; Stnex[36, 3] = "nzyo"; Stnex[36, 4] = "nnjo"; Stnex[36, 5] = "nnjyo"; Stnex[36, 6] = "nnzyo";
            Stnex[37, 1] = "nswa"; Stnex[37, 4] = "nnswa";
            Stnex[38, 1] = "nswi"; Stnex[38, 4] = "nnswi";
            Stnex[39, 1] = "nswu"; Stnex[39, 4] = "nnswu";
            Stnex[40, 1] = "nswe"; Stnex[40, 4] = "nnswe";
            Stnex[41, 1] = "nswo"; Stnex[41, 4] = "nnswo";
            Stnex[42, 1] = "ntyi"; Stnex[42, 2] = "ncyi"; Stnex[42, 4] = "nntyi"; Stnex[42, 5] = "nncyi";
            Stnex[43, 1] = "ntye"; Stnex[43, 2] = "ncye"; Stnex[43, 3] = "nche"; Stnex[43, 4] = "nntye"; Stnex[43, 5] = "nncye"; Stnex[43, 6] = "nnche";
            Stnex[44, 1] = "ntya"; Stnex[44, 2] = "ncya"; Stnex[44, 3] = "ncha"; Stnex[44, 4] = "nntya"; Stnex[44, 5] = "nncya"; Stnex[44, 6] = "nncha";
            Stnex[45, 1] = "ntyu"; Stnex[45, 2] = "ncyu"; Stnex[45, 3] = "nchu"; Stnex[45, 4] = "nntyu"; Stnex[45, 5] = "nncyu"; Stnex[45, 6] = "nnchu";
            Stnex[46, 1] = "ntyo"; Stnex[46, 2] = "ncyo"; Stnex[46, 3] = "ncho"; Stnex[46, 4] = "nntyo"; Stnex[46, 5] = "nncyo"; Stnex[46, 6] = "nncho";
            Stnex[47, 1] = "ndyi"; Stnex[47, 4] = "nndyi";
            Stnex[48, 1] = "ndye"; Stnex[48, 4] = "nndye";
            Stnex[49, 1] = "ndya"; Stnex[49, 4] = "nndya";
            Stnex[50, 1] = "ndyu"; Stnex[50, 4] = "nndyu";
            Stnex[51, 1] = "ndyo"; Stnex[51, 4] = "nndyo";
            Stnex[52, 1] = "ntsa"; Stnex[52, 4] = "nntsa";
            Stnex[53, 1] = "ntsi"; Stnex[53, 4] = "nntsi";
            Stnex[54, 1] = "ntse"; Stnex[54, 4] = "nntse";
            Stnex[55, 1] = "ntso"; Stnex[55, 4] = "nntso";
            Stnex[56, 1] = "nthi"; Stnex[56, 4] = "nnthi";
            Stnex[57, 1] = "nthe"; Stnex[57, 4] = "nnthe";
            Stnex[58, 1] = "ntha"; Stnex[58, 4] = "nntha";
            Stnex[59, 1] = "nthu"; Stnex[59, 4] = "nnthu";
            Stnex[60, 1] = "ntho"; Stnex[60, 4] = "nntho";
            Stnex[61, 1] = "ndhi"; Stnex[61, 4] = "nndhi";
            Stnex[62, 1] = "ndhe"; Stnex[62, 4] = "nndhe";
            Stnex[63, 1] = "ndha"; Stnex[63, 4] = "nndha";
            Stnex[64, 1] = "ndhu"; Stnex[64, 4] = "nndhu";
            Stnex[65, 1] = "ndho"; Stnex[65, 4] = "nndho";
            Stnex[66, 1] = "ntwa"; Stnex[66, 4] = "nntwa";
            Stnex[67, 1] = "ntwi"; Stnex[67, 4] = "nntwi";
            Stnex[68, 1] = "ntwu"; Stnex[68, 4] = "nntwu";
            Stnex[69, 1] = "ntwe"; Stnex[69, 4] = "nntwe";
            Stnex[70, 1] = "ntwo"; Stnex[70, 4] = "nntwo";
            Stnex[71, 1] = "ndwa"; Stnex[71, 4] = "nndwa";
            Stnex[72, 1] = "ndwi"; Stnex[72, 4] = "nndwi";
            Stnex[73, 1] = "ndwu"; Stnex[73, 4] = "nndwu";
            Stnex[74, 1] = "ndwe"; Stnex[74, 4] = "nndwe";
            Stnex[75, 1] = "ndwo"; Stnex[75, 4] = "nndwo";
            Stnex[76, 1] = "nhyi"; Stnex[76, 4] = "nnhyi";
            Stnex[77, 1] = "nhye"; Stnex[77, 4] = "nnhye";
            Stnex[78, 1] = "nhya"; Stnex[78, 4] = "nnhya";
            Stnex[79, 1] = "nhyu"; Stnex[79, 4] = "nnhyu";
            Stnex[80, 1] = "nhyo"; Stnex[80, 4] = "nnhyo";
            Stnex[81, 1] = "nbyi"; Stnex[81, 4] = "nnbyi";
            Stnex[82, 1] = "nbye"; Stnex[82, 4] = "nnbye";
            Stnex[83, 1] = "nbya"; Stnex[83, 4] = "nnbya";
            Stnex[84, 1] = "nbyu"; Stnex[84, 4] = "nnbyu";
            Stnex[85, 1] = "nbyo"; Stnex[85, 4] = "nnbyo";
            Stnex[86, 1] = "npyi"; Stnex[86, 4] = "nnpyi";
            Stnex[87, 1] = "npye"; Stnex[87, 4] = "nnpye";
            Stnex[88, 1] = "npya"; Stnex[88, 4] = "nnpya";
            Stnex[89, 1] = "npyu"; Stnex[89, 4] = "nnpyu";
            Stnex[90, 1] = "npyo"; Stnex[90, 4] = "nnpyo";
            Stnex[91, 1] = "nfa"; Stnex[91, 2] = "nfwa"; Stnex[91, 4] = "nnfa"; Stnex[91, 5] = "nnfwa";
            Stnex[92, 1] = "nfi"; Stnex[92, 2] = "nfyi"; Stnex[92, 3] = "nfwi"; Stnex[92, 4] = "nnfi"; Stnex[92, 5] = "nnfyi"; Stnex[92, 6] = "nnfwi";
            Stnex[93, 1] = "nfwu"; Stnex[93, 4] = "nnfwu";
            Stnex[94, 1] = "nfe"; Stnex[94, 2] = "nfye"; Stnex[94, 3] = "nfwe"; Stnex[94, 4] = "nnfe"; Stnex[94, 5] = "nnfye"; Stnex[94, 6] = "nnfwe";
            Stnex[95, 1] = "nfo"; Stnex[95, 2] = "nfwo"; Stnex[95, 4] = "nnfo"; Stnex[95, 5] = "nnfwo";
            Stnex[96, 1] = "nfya"; Stnex[96, 4] = "nnfya";
            Stnex[97, 1] = "nfyu"; Stnex[97, 4] = "nnfyu";
            Stnex[98, 1] = "nfyo"; Stnex[98, 4] = "nnfyo";
            Stnex[99, 1] = "nmyi"; Stnex[99, 4] = "nnmyi";
            Stnex[100, 1] = "nmye"; Stnex[100, 4] = "nnmye";
            Stnex[101, 1] = "nmya"; Stnex[101, 4] = "nnmya";
            Stnex[102, 1] = "nmyu"; Stnex[102, 4] = "nnmyu";
            Stnex[103, 1] = "nmyo"; Stnex[103, 4] = "nnmyo";
            Stnex[104, 1] = "nryi"; Stnex[104, 4] = "nnryi";
            Stnex[105, 1] = "nrye"; Stnex[105, 4] = "nnrye";
            Stnex[106, 1] = "nrya"; Stnex[106, 4] = "nnrya";
            Stnex[107, 1] = "nryu"; Stnex[107, 4] = "nnryu";
            Stnex[108, 1] = "nryo"; Stnex[108, 4] = "nnryo";
            Stnex[109, 1] = "nva"; Stnex[109, 4] = "nnva";
            Stnex[110, 1] = "nvi"; Stnex[110, 2] = "nvyi"; Stnex[110, 4] = "nnvi"; Stnex[110, 5] = "nnvyi";
            Stnex[111, 1] = "nve"; Stnex[111, 2] = "nvye"; Stnex[111, 4] = "nnve"; Stnex[111, 5] = "nnvye";
            Stnex[112, 1] = "nvo"; Stnex[112, 4] = "nnvo";
            Stnex[113, 1] = "nvya"; Stnex[113, 4] = "nnvya";
            Stnex[114, 1] = "nvyu"; Stnex[114, 4] = "nnvyu";
            Stnex[115, 1] = "nvyo"; Stnex[115, 4] = "nnvyo";
            for (int i = STNEX_COL / 2; i < STNEX_COL; i++)
            {
                for (int k = 1; k < STNEX_ROW; k++)
                {
                    Stnex[i, k] = Stnex[i - (STNEX_COL / 2), k];
                }
            }

            Stnt[0, 1] = "nkka"; Stnt[0, 2] = "ncca"; Stnt[0, 3] = "nltuka"; Stnt[0, 4] = "nltuca"; Stnt[0, 5] = "nnkka"; Stnt[0, 6] = "nncca"; Stnt[0, 7] = "nnltuka"; Stnt[0, 8] = "nnltuca";
            Stnt[1, 1] = "nkki"; Stnt[1, 2] = "nltuki"; Stnt[1, 3] = "nkki"; Stnt[1, 4] = "nltuki"; Stnt[1, 5] = "nnkki"; Stnt[1, 6] = "nnltuki"; Stnt[1, 7] = "nnkki"; Stnt[1, 8] = "nnltuki";
            Stnt[2, 1] = "nkku"; Stnt[2, 2] = "nccu"; Stnt[2, 3] = "nqqu"; Stnt[2, 4] = "nltuku"; Stnt[2, 5] = "nltucu"; Stnt[2, 6] = "nnkku"; Stnt[2, 7] = "nnccu"; Stnt[2, 8] = "nnqqu"; Stnt[2, 9] = "nnltuku"; Stnt[2, 10] = "nnltucu";
            Stnt[3, 1] = "nkke"; Stnt[3, 2] = "nltuke"; Stnt[3, 3] = "nnkke"; Stnt[3, 4] = "nnltuke";
            Stnt[4, 1] = "nkko"; Stnt[4, 2] = "ncco"; Stnt[4, 3] = "nltuko"; Stnt[4, 4] = "nltuco"; Stnt[4, 5] = "nnkko"; Stnt[4, 6] = "nncco"; Stnt[4, 7] = "nnltuko"; Stnt[4, 8] = "nnltuco";
            Stnt[5, 1] = "nssa"; Stnt[5, 2] = "nltusa"; Stnt[5, 3] = "nnssa"; Stnt[5, 4] = "nnltusa";
            Stnt[6, 1] = "nssi"; Stnt[6, 2] = "nsshi"; Stnt[6, 3] = "ncci"; Stnt[6, 4] = "nltusi"; Stnt[6, 5] = "nltushi"; Stnt[6, 6] = "nnssi"; Stnt[6, 7] = "nnsshi"; Stnt[6, 8] = "nncci"; Stnt[6, 9] = "nnltusi"; Stnt[6, 10] = "nnltushi";
            Stnt[7, 1] = "nssu"; Stnt[7, 2] = "nltusu"; Stnt[7, 3] = "nnssu"; Stnt[7, 4] = "nnltusu";
            Stnt[8, 1] = "nsse"; Stnt[8, 2] = "ncce"; Stnt[8, 3] = "nltuse"; Stnt[8, 4] = "nltuce"; Stnt[8, 5] = "nnsse"; Stnt[8, 6] = "nncce"; Stnt[8, 7] = "nnltuse"; Stnt[8, 8] = "nnltuce";
            Stnt[9, 1] = "nsso"; Stnt[9, 2] = "nltuso"; Stnt[9, 3] = "nnsso"; Stnt[9, 4] = "nnltuso";
            Stnt[10, 1] = "ntta"; Stnt[10, 2] = "nltuta"; Stnt[10, 3] = "nntta"; Stnt[10, 4] = "nnltuta";
            Stnt[11, 1] = "ntti"; Stnt[11, 2] = "ncchi"; Stnt[11, 3] = "nltuti"; Stnt[11, 4] = "nltuchi"; Stnt[11, 5] = "nntti"; Stnt[11, 6] = "nncchi"; Stnt[11, 7] = "nnltuti"; Stnt[11, 8] = "nnltuchi";
            Stnt[12, 1] = "nttu"; Stnt[12, 2] = "nttsu"; Stnt[12, 3] = "nltutu"; Stnt[12, 4] = "nltutsu"; Stnt[12, 5] = "nnttu"; Stnt[12, 6] = "nnttsu"; Stnt[12, 7] = "nnltutu"; Stnt[12, 8] = "nnltutsu";
            Stnt[13, 1] = "ntte"; Stnt[13, 2] = "nltute"; Stnt[13, 3] = "nntte"; Stnt[13, 4] = "nnltute";
            Stnt[14, 1] = "ntto"; Stnt[14, 2] = "nltuto"; Stnt[14, 3] = "nntto"; Stnt[14, 4] = "nnltuto";
            Stnt[15, 1] = "nhha"; Stnt[15, 2] = "nltuha"; Stnt[15, 3] = "nnhha"; Stnt[15, 4] = "nnltuha";
            Stnt[16, 1] = "nhhi"; Stnt[16, 2] = "nltuhi"; Stnt[16, 3] = "nnhhi"; Stnt[16, 4] = "nnltuhi";
            Stnt[17, 1] = "nhhu"; Stnt[17, 2] = "nffu"; Stnt[17, 3] = "nltuhu"; Stnt[17, 4] = "nltufu"; Stnt[17, 5] = "nnhhu"; Stnt[17, 6] = "nnffu"; Stnt[17, 7] = "nnltuhu"; Stnt[17, 8] = "nnltufu";
            Stnt[18, 1] = "nhhe"; Stnt[18, 2] = "nltuhe"; Stnt[18, 3] = "nnhhe"; Stnt[18, 4] = "nnltuhe";
            Stnt[19, 1] = "nhho"; Stnt[19, 2] = "nltuho"; Stnt[19, 3] = "nnhho"; Stnt[19, 4] = "nnltuho";
            Stnt[20, 1] = "nmma"; Stnt[20, 2] = "nltuma"; Stnt[20, 3] = "nnmma"; Stnt[20, 4] = "nnltuma";
            Stnt[21, 1] = "nmmi"; Stnt[21, 2] = "nltumi"; Stnt[21, 3] = "nnmmi"; Stnt[21, 4] = "nnltumi";
            Stnt[22, 1] = "nmmu"; Stnt[22, 2] = "nltumu"; Stnt[22, 3] = "nnmmu"; Stnt[22, 4] = "nnltumu";
            Stnt[23, 1] = "nmme"; Stnt[23, 2] = "nltume"; Stnt[23, 3] = "nnmme"; Stnt[23, 4] = "nnltume";
            Stnt[24, 1] = "nmmo"; Stnt[24, 2] = "nltumo"; Stnt[24, 3] = "nnmmo"; Stnt[24, 4] = "nnltumo";
            Stnt[25, 1] = "nyya"; Stnt[25, 2] = "nltuya"; Stnt[25, 3] = "nnyya"; Stnt[25, 4] = "nnltuya";
            Stnt[26, 1] = "nyyu"; Stnt[26, 2] = "nltuyu"; Stnt[26, 3] = "nnyyu"; Stnt[26, 4] = "nnltuyu";
            Stnt[27, 1] = "nyyo"; Stnt[27, 2] = "nltuyo"; Stnt[27, 3] = "nnyyo"; Stnt[27, 4] = "nnltuyo";
            Stnt[28, 1] = "nrra"; Stnt[28, 2] = "nltura"; Stnt[28, 3] = "nnrra"; Stnt[28, 4] = "nnltura";
            Stnt[29, 1] = "nrri"; Stnt[29, 2] = "nlturi"; Stnt[29, 3] = "nnrri"; Stnt[29, 4] = "nnlturi";
            Stnt[30, 1] = "nrru"; Stnt[30, 2] = "nlturu"; Stnt[30, 3] = "nnrru"; Stnt[30, 4] = "nnlturu";
            Stnt[31, 1] = "nrre"; Stnt[31, 2] = "nlture"; Stnt[31, 3] = "nnrre"; Stnt[31, 4] = "nnlture";
            Stnt[32, 1] = "nrro"; Stnt[32, 2] = "nlturo"; Stnt[32, 3] = "nnrro"; Stnt[32, 4] = "nnlturo";
            Stnt[33, 1] = "nwwa"; Stnt[33, 2] = "nltuwa"; Stnt[33, 3] = "nnwwa"; Stnt[33, 4] = "nnltuwa";
            Stnt[34, 1] = "nwwo"; Stnt[34, 2] = "nltuwo"; Stnt[34, 3] = "nnwwo"; Stnt[34, 4] = "nnltuwo";
            Stnt[35, 1] = "nlla"; Stnt[35, 2] = "nxxa"; Stnt[35, 3] = "nltula"; Stnt[35, 4] = "nnlla"; Stnt[35, 5] = "nnxxa"; Stnt[35, 6] = "nnltula";
            Stnt[36, 1] = "nlli"; Stnt[36, 2] = "nxxi"; Stnt[36, 3] = "nllyi"; Stnt[36, 4] = "nxxyi"; Stnt[36, 5] = "nltuli"; Stnt[36, 6] = "nnlli"; Stnt[36, 7] = "nnxxi"; Stnt[36, 8] = "nnllyi"; Stnt[36, 9] = "nnxxyi"; Stnt[36, 10] = "nnltuli";
            Stnt[37, 1] = "nllu"; Stnt[37, 2] = "nxxu"; Stnt[37, 3] = "nltulu"; Stnt[37, 4] = "nnllu"; Stnt[37, 5] = "nnxxu"; Stnt[37, 6] = "nnltulu";
            Stnt[38, 1] = "nlle"; Stnt[38, 2] = "nxxe"; Stnt[38, 3] = "nllye"; Stnt[38, 4] = "nxxye"; Stnt[38, 5] = "nltule"; Stnt[38, 6] = "nnlle"; Stnt[38, 7] = "nnxxe"; Stnt[38, 8] = "nnllye"; Stnt[38, 9] = "nnxxye"; Stnt[38, 10] = "nnltule";
            Stnt[39, 1] = "nllo"; Stnt[39, 2] = "nxxo"; Stnt[39, 3] = "nltulo"; Stnt[39, 4] = "nnllo"; Stnt[39, 5] = "nnxxo"; Stnt[39, 6] = "nnltulo";
            Stnt[40, 1] = "nlltu"; Stnt[40, 2] = "nxxtu"; Stnt[40, 3] = "nlltsu"; Stnt[40, 4] = "nltultu"; Stnt[40, 5] = "nnlltu"; Stnt[40, 6] = "nnxxtu"; Stnt[40, 7] = "nnlltsu"; Stnt[40, 8] = "nnltultu";
            Stnt[41, 1] = "nllya"; Stnt[41, 2] = "nxxya"; Stnt[41, 3] = "nltulya"; Stnt[41, 4] = "nnllya"; Stnt[41, 5] = "nnxxya"; Stnt[41, 6] = "nnltulya";
            Stnt[42, 1] = "nllyu"; Stnt[42, 2] = "nxxyu"; Stnt[42, 3] = "nltulyu"; Stnt[42, 4] = "nnllyu"; Stnt[42, 5] = "nnxxyu"; Stnt[42, 6] = "nnltulyu";
            Stnt[43, 1] = "nllyo"; Stnt[43, 2] = "nxxyo"; Stnt[43, 3] = "nltulyo"; Stnt[43, 4] = "nnllyo"; Stnt[43, 5] = "nnxxyo"; Stnt[43, 6] = "nnltulyo";
            Stnt[44, 1] = "nllwa"; Stnt[44, 2] = "nxxwa"; Stnt[44, 3] = "nltulwa"; Stnt[44, 4] = "nnllwa"; Stnt[44, 5] = "nnxxwa"; Stnt[44, 6] = "nnltulwa";
            Stnt[45, 1] = "nllka"; Stnt[45, 2] = "nxxka"; Stnt[45, 3] = "nltulka"; Stnt[45, 4] = "nnllka"; Stnt[45, 5] = "nnxxka"; Stnt[45, 6] = "nnltulka";
            Stnt[46, 1] = "nllke"; Stnt[46, 2] = "nxxke"; Stnt[46, 3] = "nltulke"; Stnt[46, 4] = "nnllke"; Stnt[46, 5] = "nnxxke"; Stnt[46, 6] = "nnltulke";
            Stnt[47, 1] = "ngga"; Stnt[47, 2] = "nltuga"; Stnt[47, 3] = "nngga"; Stnt[47, 4] = "nnltuga";
            Stnt[48, 1] = "nggi"; Stnt[48, 2] = "nltugi"; Stnt[48, 3] = "nnggi"; Stnt[48, 4] = "nnltugi";
            Stnt[49, 1] = "nggu"; Stnt[49, 2] = "nltugu"; Stnt[49, 3] = "nnggu"; Stnt[49, 4] = "nnltugu";
            Stnt[50, 1] = "ngge"; Stnt[50, 2] = "nltuge"; Stnt[50, 3] = "nngge"; Stnt[50, 4] = "nnltuge";
            Stnt[51, 1] = "nggo"; Stnt[51, 2] = "nltugo"; Stnt[51, 3] = "nnggo"; Stnt[51, 4] = "nnltugo";
            Stnt[52, 1] = "nzza"; Stnt[52, 2] = "nltuza"; Stnt[52, 3] = "nnzza"; Stnt[52, 4] = "nnltuza";
            Stnt[53, 1] = "njji"; Stnt[53, 2] = "nzzi"; Stnt[53, 3] = "nltuji"; Stnt[53, 4] = "nltuzi"; Stnt[53, 5] = "nnjji"; Stnt[53, 6] = "nnzzi"; Stnt[53, 7] = "nnltuji"; Stnt[53, 8] = "nnltuzi";
            Stnt[54, 1] = "nzzu"; Stnt[54, 2] = "nltuzu"; Stnt[54, 3] = "nnzzu"; Stnt[54, 4] = "nnltuzu";
            Stnt[55, 1] = "nzze"; Stnt[55, 2] = "nltuze"; Stnt[55, 3] = "nnzze"; Stnt[55, 4] = "nnltuze";
            Stnt[56, 1] = "nzzo"; Stnt[56, 2] = "nltuzo"; Stnt[56, 3] = "nnzzo"; Stnt[56, 4] = "nnltuzo";
            Stnt[57, 1] = "ndda"; Stnt[57, 2] = "nltuda"; Stnt[57, 3] = "nndda"; Stnt[57, 4] = "nnltuda";
            Stnt[58, 1] = "nddi"; Stnt[58, 2] = "nltudi"; Stnt[58, 3] = "nnddi"; Stnt[58, 4] = "nnltudi";
            Stnt[59, 1] = "nddu"; Stnt[59, 2] = "nltudu"; Stnt[59, 3] = "nnddu"; Stnt[59, 4] = "nnltudu";
            Stnt[60, 1] = "ndde"; Stnt[60, 2] = "nltude"; Stnt[60, 3] = "nndde"; Stnt[60, 4] = "nnltude";
            Stnt[61, 1] = "nddo"; Stnt[61, 2] = "nltudo"; Stnt[61, 3] = "nnddo"; Stnt[61, 4] = "nnltudo";
            Stnt[62, 1] = "nbba"; Stnt[62, 2] = "nltuba"; Stnt[62, 3] = "nnbba"; Stnt[62, 4] = "nnltuba";
            Stnt[63, 1] = "nbbi"; Stnt[63, 2] = "nltubi"; Stnt[63, 3] = "nnbbi"; Stnt[63, 4] = "nnltubi";
            Stnt[64, 1] = "nbbu"; Stnt[64, 2] = "nltubu"; Stnt[64, 3] = "nnbbu"; Stnt[64, 4] = "nnltubu";
            Stnt[65, 1] = "nbbe"; Stnt[65, 2] = "nltube"; Stnt[65, 3] = "nnbbe"; Stnt[65, 4] = "nnltube";
            Stnt[66, 1] = "nbbo"; Stnt[66, 2] = "nltubo"; Stnt[66, 3] = "nnbbo"; Stnt[66, 4] = "nnltubo";
            Stnt[67, 1] = "nppa"; Stnt[67, 2] = "nltupa"; Stnt[67, 3] = "nnppa"; Stnt[67, 4] = "nnltupa";
            Stnt[68, 1] = "nppi"; Stnt[68, 2] = "nltupi"; Stnt[68, 3] = "nnppi"; Stnt[68, 4] = "nnltupi";
            Stnt[69, 1] = "nppu"; Stnt[69, 2] = "nltupu"; Stnt[69, 3] = "nnppu"; Stnt[69, 4] = "nnltupu";
            Stnt[70, 1] = "nppe"; Stnt[70, 2] = "nltupe"; Stnt[70, 3] = "nnppe"; Stnt[70, 4] = "nnltupe";
            Stnt[71, 1] = "nppo"; Stnt[71, 2] = "nltupo"; Stnt[71, 3] = "nnppo"; Stnt[71, 4] = "nnltupo";
            for (int i = STNT_COL / 2; i < STNT_COL; i++)
            {
                for (int k = 1; k < STNT_ROW; k++)
                {
                    Stnt[i, k] = Stnt[i - (STNT_COL / 2), k];
                }
            }

            Stntex[0, 1] = "nyye"; Stntex[0, 2] = "nltuye"; Stntex[0, 3] = "nnyye"; Stntex[0, 4] = "nnltuye";
            Stntex[1, 1] = "nwwha"; Stntex[1, 2] = "nltuwha"; Stntex[1, 3] = "nnwwha"; Stntex[1, 4] = "nnltuwha";
            Stntex[2, 1] = "nwwi"; Stntex[2, 2] = "nwwhi"; Stntex[2, 3] = "nltuwi"; Stntex[2, 4] = "nltuwhi"; Stntex[2, 5] = "nnwwi"; Stntex[2, 6] = "nnwwhi"; Stntex[2, 7] = "nnltuwi"; Stntex[2, 8] = "nnltuwhi";
            Stntex[3, 1] = "nwwe"; Stntex[3, 2] = "nwwhe"; Stntex[3, 3] = "nltuwe"; Stntex[3, 4] = "nltuwhe"; Stntex[3, 5] = "nnwwe"; Stntex[3, 6] = "nnwwhe"; Stntex[3, 7] = "nnltuwe"; Stntex[3, 8] = "nnltuwhe";
            Stntex[4, 1] = "nwwho"; Stntex[4, 2] = "nltuwho"; Stntex[4, 3] = "nnwwho"; Stntex[4, 4] = "nnltuwho";
            Stntex[5, 1] = "nkkyi"; Stntex[5, 2] = "nltukyi"; Stntex[5, 3] = "nnkkyi"; Stntex[5, 4] = "nnltukyi";
            Stntex[6, 1] = "nkkye"; Stntex[6, 2] = "nltukye"; Stntex[6, 3] = "nnkkye"; Stntex[6, 4] = "nnltukye";
            Stntex[7, 1] = "nkkya"; Stntex[7, 2] = "nltukya"; Stntex[7, 3] = "nnkkya"; Stntex[7, 4] = "nnltukya";
            Stntex[8, 1] = "nkkyu"; Stntex[8, 2] = "nltukyu"; Stntex[8, 3] = "nnkkyu"; Stntex[8, 4] = "nnltukyu";
            Stntex[9, 1] = "nkkyo"; Stntex[9, 2] = "nltukyo"; Stntex[9, 3] = "nnkkyo"; Stntex[9, 4] = "nnltukyo";
            Stntex[10, 1] = "nggyi"; Stntex[10, 2] = "nltugyi"; Stntex[10, 3] = "nnggyi"; Stntex[10, 4] = "nnltugyi";
            Stntex[11, 1] = "nggye"; Stntex[11, 2] = "nltugye"; Stntex[11, 3] = "nnggye"; Stntex[11, 4] = "nnltugye";
            Stntex[12, 1] = "nggya"; Stntex[12, 2] = "nltugya"; Stntex[12, 3] = "nnggya"; Stntex[12, 4] = "nnltugya";
            Stntex[13, 1] = "nggyu"; Stntex[13, 2] = "nltugyu"; Stntex[13, 3] = "nnggyu"; Stntex[13, 4] = "nnltugyu";
            Stntex[14, 1] = "nggyo"; Stntex[14, 2] = "nltugyo"; Stntex[14, 3] = "nnggyo"; Stntex[14, 4] = "nnltugyo";
            Stntex[15, 1] = "nqqa"; Stntex[15, 2] = "nqqwa"; Stntex[15, 3] = "nkkwa"; Stntex[15, 4] = "nltuqa"; Stntex[15, 5] = "nltuqwa"; Stntex[15, 6] = "nnqqa"; Stntex[15, 7] = "nnqqwa"; Stntex[15, 8] = "nnkkwa"; Stntex[15, 9] = "nnltuqa"; Stntex[15, 10] = "nnltuqwa";
            Stntex[16, 1] = "nqqi"; Stntex[16, 2] = "nqqwi"; Stntex[16, 3] = "nqqyi"; Stntex[16, 4] = "nltuqi"; Stntex[16, 5] = "nltuqwi"; Stntex[16, 6] = "nnqqi"; Stntex[16, 7] = "nnqqwi"; Stntex[16, 8] = "nnqqyi"; Stntex[16, 9] = "nnltuqi"; Stntex[16, 10] = "nnltuqwi";
            Stntex[17, 1] = "nqqwu"; Stntex[17, 2] = "nltuqwu"; Stntex[17, 3] = "nnqqwu"; Stntex[17, 4] = "nnltuqwu";
            Stntex[18, 1] = "nqqe"; Stntex[18, 2] = "nqqwe"; Stntex[18, 3] = "nqqye"; Stntex[18, 4] = "nltuqe"; Stntex[18, 5] = "nltuqye"; Stntex[18, 6] = "nnqqe"; Stntex[18, 7] = "nnqqwe"; Stntex[18, 8] = "nnqqye"; Stntex[18, 9] = "nnltuqe"; Stntex[18, 10] = "nnltuqye";
            Stntex[19, 1] = "nqqo"; Stntex[19, 2] = "nqqwo"; Stntex[19, 3] = "nltuqo"; Stntex[19, 4] = "nltuqwo"; Stntex[19, 5] = "nnqqo"; Stntex[19, 6] = "nnqqwo"; Stntex[19, 7] = "nnltuqo"; Stntex[19, 8] = "nnltuqwo";
            Stntex[20, 1] = "nqqya"; Stntex[20, 2] = "nltuqya"; Stntex[20, 3] = "nnqqya"; Stntex[20, 4] = "nnltuqya";
            Stntex[21, 1] = "nqqyu"; Stntex[21, 2] = "nltuqyu"; Stntex[21, 3] = "nnqqyu"; Stntex[21, 4] = "nnltuqyu";
            Stntex[22, 1] = "nqqyo"; Stntex[22, 2] = "nltuqyo"; Stntex[22, 3] = "nnqqyo"; Stntex[22, 4] = "nnltuqyo";
            Stntex[23, 1] = "nggwa"; Stntex[23, 2] = "nltugwa"; Stntex[23, 3] = "nnggwa"; Stntex[23, 4] = "nnltugwa";
            Stntex[24, 1] = "nggwi"; Stntex[24, 2] = "nltugwi"; Stntex[24, 3] = "nnggwi"; Stntex[24, 4] = "nnltugwi";
            Stntex[25, 1] = "nggwu"; Stntex[25, 2] = "nltugwu"; Stntex[25, 3] = "nnggwu"; Stntex[25, 4] = "nnltugwu";
            Stntex[26, 1] = "nggwe"; Stntex[26, 2] = "nltugwe"; Stntex[26, 3] = "nnggwe"; Stntex[26, 4] = "nnltugwe";
            Stntex[27, 1] = "nggwo"; Stntex[27, 2] = "nltugwo"; Stntex[27, 3] = "nnggwo"; Stntex[27, 4] = "nnltugwo";
            Stntex[28, 1] = "nssyi"; Stntex[28, 2] = "nltusyi"; Stntex[28, 3] = "nnssyi"; Stntex[28, 4] = "nnltusyi";
            Stntex[29, 1] = "nssye"; Stntex[29, 2] = "nsshe"; Stntex[29, 3] = "nltusye"; Stntex[29, 4] = "nltushe"; Stntex[29, 5] = "nnssye"; Stntex[29, 6] = "nnsshe"; Stntex[29, 7] = "nnltusye"; Stntex[29, 8] = "nnltushe";
            Stntex[30, 1] = "nssya"; Stntex[30, 2] = "nssha"; Stntex[30, 3] = "nltusya"; Stntex[30, 4] = "nltusha"; Stntex[30, 5] = "nnssya"; Stntex[30, 6] = "nnssha"; Stntex[30, 7] = "nnltusya"; Stntex[30, 8] = "nnltusha";
            Stntex[31, 1] = "nssyu"; Stntex[31, 2] = "nsshu"; Stntex[31, 3] = "nltusyu"; Stntex[31, 4] = "nltushu"; Stntex[31, 5] = "nnssyu"; Stntex[31, 6] = "nnsshu"; Stntex[31, 7] = "nnltusyu"; Stntex[31, 8] = "nnltushu";
            Stntex[32, 1] = "nssyo"; Stntex[32, 2] = "nssho"; Stntex[32, 3] = "nltusyo"; Stntex[32, 4] = "nltusho"; Stntex[32, 5] = "nnssyo"; Stntex[32, 6] = "nnssho"; Stntex[32, 7] = "nnltusyo"; Stntex[32, 8] = "nnltusho";
            Stntex[33, 1] = "njjyi"; Stntex[33, 2] = "nzzyi"; Stntex[33, 3] = "nltujyi"; Stntex[33, 4] = "nltuzyi"; Stntex[33, 5] = "nnjjyi"; Stntex[33, 6] = "nnzzyi"; Stntex[33, 7] = "nnltujyi"; Stntex[33, 8] = "nnltuzyi";
            Stntex[34, 1] = "njje"; Stntex[34, 2] = "njjye"; Stntex[34, 3] = "nzzye"; Stntex[34, 4] = "nltuje"; Stntex[34, 5] = "nltujye"; Stntex[34, 6] = "nltuzye"; Stntex[34, 7] = "nnjje"; Stntex[34, 8] = "nnjjye"; Stntex[34, 9] = "nnzzye"; Stntex[34, 10] = "nnltuje"; Stntex[34, 11] = "nnltujye"; Stntex[34, 12] = "nnltuzye";
            Stntex[35, 1] = "njja"; Stntex[35, 2] = "njjya"; Stntex[35, 3] = "nzzya"; Stntex[35, 4] = "nltuja"; Stntex[35, 5] = "nltujya"; Stntex[35, 6] = "nltuzya"; Stntex[35, 7] = "nnjja"; Stntex[35, 8] = "nnjjya"; Stntex[35, 9] = "nnzzya"; Stntex[35, 10] = "nnltuja"; Stntex[35, 11] = "nnltujya"; Stntex[35, 12] = "nnltuzya";
            Stntex[36, 1] = "njju"; Stntex[36, 2] = "njjyu"; Stntex[36, 3] = "nzzyu"; Stntex[36, 4] = "nltuju"; Stntex[36, 5] = "nltujyu"; Stntex[36, 6] = "nltuzyu"; Stntex[36, 7] = "nnjju"; Stntex[36, 8] = "nnjjyu"; Stntex[36, 9] = "nnzzyu"; Stntex[36, 10] = "nnltuju"; Stntex[36, 11] = "nnltujyu"; Stntex[36, 12] = "nnltuzyu";
            Stntex[37, 1] = "njjo"; Stntex[37, 2] = "njjyo"; Stntex[37, 3] = "nzzyo"; Stntex[37, 4] = "nltujo"; Stntex[37, 5] = "nltujyo"; Stntex[37, 6] = "nltuzyo"; Stntex[37, 7] = "nnjjo"; Stntex[37, 8] = "nnjjyo"; Stntex[37, 9] = "nnzzyo"; Stntex[37, 10] = "nnltujo"; Stntex[37, 11] = "nnltujyo"; Stntex[37, 12] = "nnltuzyo";
            Stntex[38, 1] = "nsswa"; Stntex[38, 2] = "nltuswa"; Stntex[38, 3] = "nnsswa"; Stntex[38, 4] = "nnltuswa";
            Stntex[39, 1] = "nsswi"; Stntex[39, 2] = "nltuswi"; Stntex[39, 3] = "nnsswi"; Stntex[39, 4] = "nnltuswi";
            Stntex[40, 1] = "nsswu"; Stntex[40, 2] = "nltuswu"; Stntex[40, 3] = "nnsswu"; Stntex[40, 4] = "nnltuswu";
            Stntex[41, 1] = "nsswe"; Stntex[41, 2] = "nltuswe"; Stntex[41, 3] = "nnsswe"; Stntex[41, 4] = "nnltuswe";
            Stntex[42, 1] = "nsswo"; Stntex[42, 2] = "nltuswo"; Stntex[42, 3] = "nnsswo"; Stntex[42, 4] = "nnltuswo";
            Stntex[43, 1] = "nttyi"; Stntex[43, 2] = "nccyi"; Stntex[43, 3] = "nltutyi"; Stntex[43, 4] = "nltucyi"; Stntex[43, 5] = "nnttyi"; Stntex[43, 6] = "nnccyi"; Stntex[43, 7] = "nnltutyi"; Stntex[43, 8] = "nnltucyi";
            Stntex[44, 1] = "nttye"; Stntex[44, 2] = "nccye"; Stntex[44, 3] = "ncche"; Stntex[44, 4] = "nltutye"; Stntex[44, 5] = "nltucye"; Stntex[44, 6] = "nltuche"; Stntex[44, 7] = "nnttye"; Stntex[44, 8] = "nnccye"; Stntex[44, 9] = "nncche"; Stntex[44, 10] = "nnltutye"; Stntex[44, 11] = "nnltucye"; Stntex[44, 12] = "nnltuche";
            Stntex[45, 1] = "nttya"; Stntex[45, 2] = "nccya"; Stntex[45, 3] = "nccha"; Stntex[45, 4] = "nltutya"; Stntex[45, 5] = "nltucya"; Stntex[45, 6] = "nltucha"; Stntex[45, 7] = "nnttya"; Stntex[45, 8] = "nnccya"; Stntex[45, 9] = "nnccha"; Stntex[45, 10] = "nnltutya"; Stntex[45, 11] = "nnltucya"; Stntex[45, 12] = "nnltucha";
            Stntex[46, 1] = "nttyu"; Stntex[46, 2] = "nccyu"; Stntex[46, 3] = "ncchu"; Stntex[46, 4] = "nltutyu"; Stntex[46, 5] = "nltucyu"; Stntex[46, 6] = "nltuchu"; Stntex[46, 7] = "nnttyu"; Stntex[46, 8] = "nnccyu"; Stntex[46, 9] = "nncchu"; Stntex[46, 10] = "nnltutyu"; Stntex[46, 11] = "nnltucyu"; Stntex[46, 12] = "nnltuchu";
            Stntex[47, 1] = "nttyo"; Stntex[47, 2] = "nccyo"; Stntex[47, 3] = "nccho"; Stntex[47, 4] = "nltutyo"; Stntex[47, 5] = "nltucyo"; Stntex[47, 6] = "nltucho"; Stntex[47, 7] = "nnttyo"; Stntex[47, 8] = "nnccyo"; Stntex[47, 9] = "nnccho"; Stntex[47, 10] = "nnltutyo"; Stntex[47, 11] = "nnltucyo"; Stntex[47, 12] = "nnltucho";
            Stntex[48, 1] = "nddyi"; Stntex[48, 2] = "nltudyi"; Stntex[48, 3] = "nnddyi"; Stntex[48, 4] = "nnltudyi";
            Stntex[49, 1] = "nddye"; Stntex[49, 2] = "nltudye"; Stntex[49, 3] = "nnddye"; Stntex[49, 4] = "nnltudye";
            Stntex[50, 1] = "nddya"; Stntex[50, 2] = "nltudya"; Stntex[50, 3] = "nnddya"; Stntex[50, 4] = "nnltudya";
            Stntex[51, 1] = "nddyu"; Stntex[51, 2] = "nltudyu"; Stntex[51, 3] = "nnddyu"; Stntex[51, 4] = "nnltudyu";
            Stntex[52, 1] = "nddyo"; Stntex[52, 2] = "nltudyo"; Stntex[52, 3] = "nnddyo"; Stntex[52, 4] = "nnltudyo";
            Stntex[53, 1] = "nttsa"; Stntex[53, 2] = "nltutsa"; Stntex[53, 3] = "nnttsa"; Stntex[53, 4] = "nnltutsa";
            Stntex[54, 1] = "nttsi"; Stntex[54, 2] = "nltutsi"; Stntex[54, 3] = "nnttsi"; Stntex[54, 4] = "nnltutsi";
            Stntex[55, 1] = "nttse"; Stntex[55, 2] = "nltutse"; Stntex[55, 3] = "nnttse"; Stntex[55, 4] = "nnltutse";
            Stntex[56, 1] = "nttso"; Stntex[56, 2] = "nltutso"; Stntex[56, 3] = "nnttso"; Stntex[56, 4] = "nnltutso";
            Stntex[57, 1] = "ntthi"; Stntex[57, 2] = "nltuthi"; Stntex[57, 3] = "nntthi"; Stntex[57, 4] = "nnltuthi";
            Stntex[58, 1] = "ntthe"; Stntex[58, 2] = "nltuthe"; Stntex[58, 3] = "nntthe"; Stntex[58, 4] = "nnltuthe";
            Stntex[59, 1] = "nttha"; Stntex[59, 2] = "nltutha"; Stntex[59, 3] = "nnttha"; Stntex[59, 4] = "nnltutha";
            Stntex[60, 1] = "ntthu"; Stntex[60, 2] = "nltuthu"; Stntex[60, 3] = "nntthu"; Stntex[60, 4] = "nnltuthu";
            Stntex[61, 1] = "nttho"; Stntex[61, 2] = "nltutho"; Stntex[61, 3] = "nnttho"; Stntex[61, 4] = "nnltutho";
            Stntex[62, 1] = "nddhi"; Stntex[62, 2] = "nltudhi"; Stntex[62, 3] = "nnddhi"; Stntex[62, 4] = "nnltudhi";
            Stntex[63, 1] = "nddhe"; Stntex[63, 2] = "nltudhe"; Stntex[63, 3] = "nnddhe"; Stntex[63, 4] = "nnltudhe";
            Stntex[64, 1] = "nddha"; Stntex[64, 2] = "nltudha"; Stntex[64, 3] = "nnddha"; Stntex[64, 4] = "nnltudha";
            Stntex[65, 1] = "nddhu"; Stntex[65, 2] = "nltudhu"; Stntex[65, 3] = "nnddhu"; Stntex[65, 4] = "nnltudhu";
            Stntex[66, 1] = "nddho"; Stntex[66, 2] = "nltudho"; Stntex[66, 3] = "nnddho"; Stntex[66, 4] = "nnltudho";
            Stntex[67, 1] = "nttwa"; Stntex[67, 2] = "nltutwa"; Stntex[67, 3] = "nnttwa"; Stntex[67, 4] = "nnltutwa";
            Stntex[68, 1] = "nttwi"; Stntex[68, 2] = "nltutwi"; Stntex[68, 3] = "nnttwi"; Stntex[68, 4] = "nnltutwi";
            Stntex[69, 1] = "nttwu"; Stntex[69, 2] = "nltutwu"; Stntex[69, 3] = "nnttwu"; Stntex[69, 4] = "nnltutwu";
            Stntex[70, 1] = "nttwe"; Stntex[70, 2] = "nltutwe"; Stntex[70, 3] = "nnttwe"; Stntex[70, 4] = "nnltutwe";
            Stntex[71, 1] = "nttwo"; Stntex[71, 2] = "nltutwo"; Stntex[71, 3] = "nnttwo"; Stntex[71, 4] = "nnltutwo";
            Stntex[72, 1] = "nddwa"; Stntex[72, 2] = "nltudwa"; Stntex[72, 3] = "nnddwa"; Stntex[72, 4] = "nnltudwa";
            Stntex[73, 1] = "nddwi"; Stntex[73, 2] = "nltudwi"; Stntex[73, 3] = "nnddwi"; Stntex[73, 4] = "nnltudwi";
            Stntex[74, 1] = "nddwu"; Stntex[74, 2] = "nltudwu"; Stntex[74, 3] = "nnddwu"; Stntex[74, 4] = "nnltudwu";
            Stntex[75, 1] = "nddwe"; Stntex[75, 2] = "nltudwe"; Stntex[75, 3] = "nnddwe"; Stntex[75, 4] = "nnltudwe";
            Stntex[76, 1] = "nddwo"; Stntex[76, 2] = "nltudwo"; Stntex[76, 3] = "nnddwo"; Stntex[76, 4] = "nnltudwo";
            Stntex[77, 1] = "nhhyi"; Stntex[77, 2] = "nltuhyi"; Stntex[77, 3] = "nnhhyi"; Stntex[77, 4] = "nnltuhyi";
            Stntex[78, 1] = "nhhye"; Stntex[78, 2] = "nltuhye"; Stntex[78, 3] = "nnhhye"; Stntex[78, 4] = "nnltuhye";
            Stntex[79, 1] = "nhhya"; Stntex[79, 2] = "nltuhya"; Stntex[79, 3] = "nnhhya"; Stntex[79, 4] = "nnltuhya";
            Stntex[80, 1] = "nhhyu"; Stntex[80, 2] = "nltuhyu"; Stntex[80, 3] = "nnhhyu"; Stntex[80, 4] = "nnltuhyu";
            Stntex[81, 1] = "nhhyo"; Stntex[81, 2] = "nltuhyo"; Stntex[81, 3] = "nnhhyo"; Stntex[81, 4] = "nnltuhyo";
            Stntex[82, 1] = "nbbyi"; Stntex[82, 2] = "nltubyi"; Stntex[82, 3] = "nnbbyi"; Stntex[82, 4] = "nnltubyi";
            Stntex[83, 1] = "nbbye"; Stntex[83, 2] = "nltubye"; Stntex[83, 3] = "nnbbye"; Stntex[83, 4] = "nnltubye";
            Stntex[84, 1] = "nbbya"; Stntex[84, 2] = "nltubya"; Stntex[84, 3] = "nnbbya"; Stntex[84, 4] = "nnltubya";
            Stntex[85, 1] = "nbbyu"; Stntex[85, 2] = "nltubyu"; Stntex[85, 3] = "nnbbyu"; Stntex[85, 4] = "nnltubyu";
            Stntex[86, 1] = "nbbyo"; Stntex[86, 2] = "nltubyo"; Stntex[86, 3] = "nnbbyo"; Stntex[86, 4] = "nnltubyo";
            Stntex[87, 1] = "nppyi"; Stntex[87, 2] = "nltupyi"; Stntex[87, 3] = "nnppyi"; Stntex[87, 4] = "nnltupyi";
            Stntex[88, 1] = "nppye"; Stntex[88, 2] = "nltupye"; Stntex[88, 3] = "nnppye"; Stntex[88, 4] = "nnltupye";
            Stntex[89, 1] = "nppya"; Stntex[89, 2] = "nltupya"; Stntex[89, 3] = "nnppya"; Stntex[89, 4] = "nnltupya";
            Stntex[90, 1] = "nppyu"; Stntex[90, 2] = "nltupyu"; Stntex[90, 3] = "nnppyu"; Stntex[90, 4] = "nnltupyu";
            Stntex[91, 1] = "nppyo"; Stntex[91, 2] = "nltupyo"; Stntex[91, 3] = "nnppyo"; Stntex[91, 4] = "nnltupyo";
            Stntex[92, 1] = "nffa"; Stntex[92, 2] = "nffwa"; Stntex[92, 3] = "nltufa"; Stntex[92, 4] = "nltufwa"; Stntex[92, 5] = "nnffa"; Stntex[92, 6] = "nnffwa"; Stntex[92, 7] = "nnltufa"; Stntex[92, 8] = "nnltufwa";
            Stntex[93, 1] = "nffi"; Stntex[93, 2] = "nffyi"; Stntex[93, 3] = "nffwi"; Stntex[93, 4] = "nltufi"; Stntex[93, 5] = "nltufyi"; Stntex[93, 6] = "nltufwi"; Stntex[93, 7] = "nnffi"; Stntex[93, 8] = "nnffyi"; Stntex[93, 9] = "nnffwi"; Stntex[93, 10] = "nnltufi"; Stntex[93, 11] = "nnltufyi"; Stntex[93, 12] = "nnltufwi";
            Stntex[94, 1] = "nffwu"; Stntex[94, 2] = "nltufwu"; Stntex[94, 3] = "nnffwu"; Stntex[94, 4] = "nnltufwu";
            Stntex[95, 1] = "nffe"; Stntex[95, 2] = "nffye"; Stntex[95, 3] = "nffwe"; Stntex[95, 4] = "nltufe"; Stntex[95, 5] = "nltufye"; Stntex[95, 6] = "nltufwe"; Stntex[95, 7] = "nnffe"; Stntex[95, 8] = "nnffye"; Stntex[95, 9] = "nnffwe"; Stntex[95, 10] = "nnltufe"; Stntex[95, 11] = "nnltufye"; Stntex[95, 12] = "nnltufwe";
            Stntex[96, 1] = "nffo"; Stntex[96, 2] = "nffwo"; Stntex[96, 3] = "nltufo"; Stntex[96, 4] = "nltufwo"; Stntex[96, 5] = "nnffo"; Stntex[96, 6] = "nnffwo"; Stntex[96, 7] = "nnltufo"; Stntex[96, 8] = "nnltufwo";
            Stntex[97, 1] = "nffya"; Stntex[97, 2] = "nltufya"; Stntex[97, 3] = "nnffya"; Stntex[97, 4] = "nnltufya";
            Stntex[98, 1] = "nffyu"; Stntex[98, 2] = "nltufyu"; Stntex[98, 3] = "nnffyu"; Stntex[98, 4] = "nnltufyu";
            Stntex[99, 1] = "nffyo"; Stntex[99, 2] = "nltufyo"; Stntex[99, 3] = "nnffyo"; Stntex[99, 4] = "nnltufyo";
            Stntex[100, 1] = "nmmyi"; Stntex[100, 2] = "nltumyi"; Stntex[100, 3] = "nnmmyi"; Stntex[100, 4] = "nnltumyi";
            Stntex[101, 1] = "nmmye"; Stntex[101, 2] = "nltumye"; Stntex[101, 3] = "nnmmye"; Stntex[101, 4] = "nnltumye";
            Stntex[102, 1] = "nmmya"; Stntex[102, 2] = "nltumya"; Stntex[102, 3] = "nnmmya"; Stntex[102, 4] = "nnltumya";
            Stntex[103, 1] = "nmmyu"; Stntex[103, 2] = "nltumyu"; Stntex[103, 3] = "nnmmyu"; Stntex[103, 4] = "nnltumyu";
            Stntex[104, 1] = "nmmyo"; Stntex[104, 2] = "nltumyo"; Stntex[104, 3] = "nnmmyo"; Stntex[104, 4] = "nnltumyo";
            Stntex[105, 1] = "nrryi"; Stntex[105, 2] = "nlturyi"; Stntex[105, 3] = "nnrryi"; Stntex[105, 4] = "nnlturyi";
            Stntex[106, 1] = "nrrye"; Stntex[106, 2] = "nlturye"; Stntex[106, 3] = "nnrrye"; Stntex[106, 4] = "nnlturye";
            Stntex[107, 1] = "nrrya"; Stntex[107, 2] = "nlturya"; Stntex[107, 3] = "nnrrya"; Stntex[107, 4] = "nnlturya";
            Stntex[108, 1] = "nrryu"; Stntex[108, 2] = "nlturyu"; Stntex[108, 3] = "nnrryu"; Stntex[108, 4] = "nnlturyu";
            Stntex[109, 1] = "nrryo"; Stntex[109, 2] = "nlturyo"; Stntex[109, 3] = "nnrryo"; Stntex[109, 4] = "nnlturyo";
            Stntex[110, 1] = "nvva"; Stntex[110, 2] = "nltuva"; Stntex[110, 3] = "nnvva"; Stntex[110, 4] = "nnltuva";
            Stntex[111, 1] = "nvvi"; Stntex[111, 2] = "nvvyi"; Stntex[111, 3] = "nltuvi"; Stntex[111, 4] = "nltuvyi"; Stntex[111, 5] = "nnvvi"; Stntex[111, 6] = "nnvvyi"; Stntex[111, 7] = "nnltuvi"; Stntex[111, 8] = "nnltuvyi";
            Stntex[112, 1] = "nvve"; Stntex[112, 2] = "nvvye"; Stntex[112, 3] = "nltuve"; Stntex[112, 4] = "nltuvye"; Stntex[112, 5] = "nnvve"; Stntex[112, 6] = "nnvvye"; Stntex[112, 7] = "nnltuve"; Stntex[112, 8] = "nnltuvye";
            Stntex[113, 1] = "nvvo"; Stntex[113, 2] = "nltuvo"; Stntex[113, 3] = "nnvvo"; Stntex[113, 4] = "nnltuvo";
            Stntex[114, 1] = "nvvya"; Stntex[114, 2] = "nltuvya"; Stntex[114, 3] = "nnvvya"; Stntex[114, 4] = "nnltuvya";
            Stntex[115, 1] = "nvvyu"; Stntex[115, 2] = "nltuvyu"; Stntex[115, 3] = "nnvvyu"; Stntex[115, 4] = "nnltuvyu";
            Stntex[116, 1] = "nvvyo"; Stntex[116, 2] = "nltuvyo"; Stntex[116, 3] = "nnvvyo"; Stntex[116, 4] = "nnltuvyo";
            for (int i = STNEX_COL / 2; i < STNEX_COL; i++)
            {
                for (int k = 1; k < STNEX_ROW; k++)
                {
                    Stnex[i, k] = Stnex[i - (STNEX_COL / 2), k];
                }
            }
        }
        else
        {
            //NN
            Stn[0, 1] = "nnka"; Stn[0, 2] = "nnca"; Stn[0, 5] = "nka"; Stn[0, 6] = "nca";
            Stn[1, 1] = "nnki"; Stn[1, 5] = "nki";
            Stn[2, 1] = "nnku"; Stn[2, 2] = "nncu"; Stn[2, 3] = "nnqu"; Stn[2, 5] = "nku"; Stn[2, 6] = "ncu"; Stn[2, 7] = "nqu";
            Stn[3, 1] = "nnke"; Stn[3, 5] = "nke";
            Stn[4, 1] = "nnko"; Stn[4, 2] = "nnco"; Stn[4, 5] = "nko"; Stn[4, 6] = "nco";
            Stn[5, 1] = "nnsa"; Stn[5, 5] = "nsa";
            Stn[6, 1] = "nnsi"; Stn[6, 2] = "nnshi"; Stn[6, 3] = "nnci"; Stn[6, 5] = "nsi"; Stn[6, 6] = "nshi"; Stn[6, 7] = "nci";
            Stn[7, 1] = "nnsu"; Stn[7, 5] = "nsu";
            Stn[8, 1] = "nnse"; Stn[8, 2] = "nnce"; Stn[8, 5] = "nse"; Stn[8, 6] = "nce";
            Stn[9, 1] = "nnso"; Stn[9, 5] = "nso";
            Stn[10, 1] = "nnta"; Stn[10, 5] = "nta";
            Stn[11, 1] = "nnti"; Stn[11, 2] = "nnchi"; Stn[11, 5] = "nti"; Stn[11, 6] = "nchi";
            Stn[12, 1] = "nntu"; Stn[12, 2] = "nntsu"; Stn[12, 5] = "ntu"; Stn[12, 6] = "ntsu";
            Stn[13, 1] = "nnte"; Stn[13, 5] = "nte";
            Stn[14, 1] = "nnto"; Stn[14, 5] = "nto";
            Stn[15, 1] = "nnha"; Stn[15, 5] = "nha";
            Stn[16, 1] = "nnhi"; Stn[16, 5] = "nhi";
            Stn[17, 1] = "nnhu"; Stn[17, 2] = "nnfu"; Stn[17, 5] = "nhu"; Stn[17, 6] = "nfu";
            Stn[18, 1] = "nnhe"; Stn[18, 5] = "nhe";
            Stn[19, 1] = "nnho"; Stn[19, 5] = "nho";
            Stn[20, 1] = "nnma"; Stn[20, 5] = "nma";
            Stn[21, 1] = "nnmi"; Stn[21, 5] = "nmi";
            Stn[22, 1] = "nnmu"; Stn[22, 5] = "nmu";
            Stn[23, 1] = "nnme"; Stn[23, 5] = "nme";
            Stn[24, 1] = "nnmo"; Stn[24, 5] = "nmo";
            Stn[25, 1] = "nnra"; Stn[25, 5] = "nra";
            Stn[26, 1] = "nnri"; Stn[26, 5] = "nri";
            Stn[27, 1] = "nnru"; Stn[27, 5] = "nru";
            Stn[28, 1] = "nnre"; Stn[28, 5] = "nre";
            Stn[29, 1] = "nnro"; Stn[29, 5] = "nro";
            Stn[30, 1] = "nnwa"; Stn[30, 5] = "nwa";
            Stn[31, 1] = "nnwo"; Stn[31, 5] = "nwo";
            Stn[32, 1] = "nnla"; Stn[32, 2] = "nnxa"; Stn[32, 5] = "nla"; Stn[32, 6] = "nxa";
            Stn[33, 1] = "nnli"; Stn[33, 2] = "nnxi"; Stn[33, 3] = "nnlyi"; Stn[33, 4] = "nnxyi"; Stn[33, 5] = "nli"; Stn[33, 6] = "nxi"; Stn[33, 7] = "nlyi"; Stn[33, 8] = "nxyi";
            Stn[34, 1] = "nnlu"; Stn[34, 2] = "nnxu"; Stn[34, 5] = "nlu"; Stn[34, 6] = "nxu";
            Stn[35, 1] = "nnle"; Stn[35, 2] = "nnxe"; Stn[35, 3] = "nnlye"; Stn[35, 4] = "nnxye"; Stn[35, 5] = "nle"; Stn[35, 6] = "nxe"; Stn[35, 7] = "nlye"; Stn[35, 8] = "nxye";
            Stn[36, 1] = "nnlo"; Stn[36, 2] = "nnxo"; Stn[36, 5] = "nlo"; Stn[36, 6] = "nxo";
            Stn[37, 1] = "nnlya"; Stn[37, 2] = "nnxya"; Stn[37, 5] = "nlya"; Stn[37, 6] = "nxya";
            Stn[38, 1] = "nnlyu"; Stn[38, 2] = "nnxyu"; Stn[38, 5] = "nlyu"; Stn[38, 6] = "nxyu";
            Stn[39, 1] = "nnlyo"; Stn[39, 2] = "nnxyo"; Stn[39, 5] = "nlyo"; Stn[39, 6] = "nxyo";
            Stn[40, 1] = "nnlwa"; Stn[40, 2] = "nnxwa"; Stn[40, 5] = "nlwa"; Stn[40, 6] = "nxwa";
            Stn[41, 1] = "nnlka"; Stn[41, 2] = "nnxka"; Stn[41, 5] = "nlka"; Stn[41, 6] = "nxka";
            Stn[42, 1] = "nnlke"; Stn[42, 2] = "nnxke"; Stn[42, 5] = "nlke"; Stn[42, 6] = "nxke";
            Stn[43, 1] = "nnga"; Stn[43, 5] = "nga";
            Stn[44, 1] = "nngi"; Stn[44, 5] = "ngi";
            Stn[45, 1] = "nngu"; Stn[45, 5] = "ngu";
            Stn[46, 1] = "nnge"; Stn[46, 5] = "nge";
            Stn[47, 1] = "nngo"; Stn[47, 5] = "ngo";
            Stn[48, 1] = "nnza"; Stn[48, 5] = "nza";
            Stn[49, 1] = "nnji"; Stn[49, 2] = "nnzi"; Stn[49, 5] = "nji"; Stn[49, 6] = "nzi";
            Stn[50, 1] = "nnzu"; Stn[50, 5] = "nzu";
            Stn[51, 1] = "nnze"; Stn[51, 5] = "nze";
            Stn[52, 1] = "nnzo"; Stn[52, 5] = "nzo";
            Stn[53, 1] = "nnda"; Stn[53, 5] = "nda";
            Stn[54, 1] = "nndi"; Stn[54, 5] = "ndi";
            Stn[55, 1] = "nndu"; Stn[55, 5] = "ndu";
            Stn[56, 1] = "nnde"; Stn[56, 5] = "nde";
            Stn[57, 1] = "nndo"; Stn[57, 5] = "ndo";
            Stn[58, 1] = "nnba"; Stn[58, 5] = "nba";
            Stn[59, 1] = "nnbi"; Stn[59, 5] = "nbi";
            Stn[60, 1] = "nnbu"; Stn[60, 5] = "nbu";
            Stn[61, 1] = "nnbe"; Stn[61, 5] = "nbe";
            Stn[62, 1] = "nnbo"; Stn[62, 5] = "nbo";
            Stn[63, 1] = "nnpa"; Stn[63, 5] = "npa";
            Stn[64, 1] = "nnpi"; Stn[64, 5] = "npi";
            Stn[65, 1] = "nnpu"; Stn[65, 5] = "npu";
            Stn[66, 1] = "nnpe"; Stn[66, 5] = "npe";
            Stn[67, 1] = "nnpo"; Stn[67, 5] = "npo";
            for (int i = STN_COL / 2; i < STN_COL; i++)
            {
                for (int k = 1; k < STN_ROW; k++)
                {
                    Stn[i, k] = Stn[i - (STN_COL / 2), k];
                }
            }

            Stnex[0, 1] = "nnwha"; Stnex[0, 4] = "nwha";
            Stnex[1, 1] = "nnwi"; Stnex[1, 2] = "nnwhi"; Stnex[1, 4] = "nwi"; Stnex[1, 5] = "nwhi";
            Stnex[2, 1] = "nnwe"; Stnex[2, 2] = "nnwhe"; Stnex[2, 4] = "nwe"; Stnex[2, 5] = "nwhe";
            Stnex[3, 1] = "nnwho"; Stnex[3, 4] = "nwho";
            Stnex[4, 1] = "nnkyi"; Stnex[4, 4] = "nkyi";
            Stnex[5, 1] = "nnkye"; Stnex[5, 4] = "nkye";
            Stnex[6, 1] = "nnkya"; Stnex[6, 4] = "nkya";
            Stnex[7, 1] = "nnkyu"; Stnex[7, 4] = "nkyu";
            Stnex[8, 1] = "nnkyo"; Stnex[8, 4] = "nkyo";
            Stnex[9, 1] = "nngyi"; Stnex[9, 4] = "ngyi";
            Stnex[10, 1] = "nngye"; Stnex[10, 4] = "ngye";
            Stnex[11, 1] = "nngya"; Stnex[11, 4] = "ngya";
            Stnex[12, 1] = "nngyu"; Stnex[12, 4] = "ngyu";
            Stnex[13, 1] = "nngyo"; Stnex[13, 4] = "ngyo";
            Stnex[14, 1] = "nnqa"; Stnex[14, 2] = "nnqwa"; Stnex[14, 3] = "nnkwa"; Stnex[14, 4] = "nqa"; Stnex[14, 5] = "nqwa"; Stnex[14, 6] = "nkwa";
            Stnex[15, 1] = "nnqi"; Stnex[15, 2] = "nnqwi"; Stnex[15, 3] = "nnqyi"; Stnex[15, 4] = "nqi"; Stnex[15, 5] = "nqwi"; Stnex[15, 6] = "nqyi";
            Stnex[16, 1] = "nnqwu"; Stnex[16, 4] = "nqwu";
            Stnex[17, 1] = "nnqe"; Stnex[17, 2] = "nnqwe"; Stnex[17, 3] = "nnqye"; Stnex[17, 4] = "nqe"; Stnex[17, 5] = "nqwe"; Stnex[17, 6] = "nqye";
            Stnex[18, 1] = "nnqo"; Stnex[18, 2] = "nnqwo"; Stnex[18, 4] = "nqo"; Stnex[18, 5] = "nqwo";
            Stnex[19, 1] = "nnqya"; Stnex[19, 4] = "nqya";
            Stnex[20, 1] = "nnqyu"; Stnex[20, 4] = "nqyu";
            Stnex[21, 1] = "nnqyo"; Stnex[21, 4] = "nqyo";
            Stnex[22, 1] = "nngwa"; Stnex[22, 4] = "ngwa";
            Stnex[23, 1] = "nngwi"; Stnex[23, 4] = "ngwi";
            Stnex[24, 1] = "nngwu"; Stnex[24, 4] = "ngwu";
            Stnex[25, 1] = "nngwe"; Stnex[25, 4] = "ngwe";
            Stnex[26, 1] = "nngwo"; Stnex[26, 4] = "ngwo";
            Stnex[27, 1] = "nnsyi"; Stnex[27, 4] = "nsyi";
            Stnex[28, 1] = "nnsye"; Stnex[28, 2] = "nnshe"; Stnex[28, 4] = "nsye"; Stnex[28, 5] = "nshe";
            Stnex[29, 1] = "nnsya"; Stnex[29, 2] = "nnsha"; Stnex[29, 4] = "nsya"; Stnex[29, 5] = "nsha";
            Stnex[30, 1] = "nnsyu"; Stnex[30, 2] = "nnshu"; Stnex[30, 4] = "nsyu"; Stnex[30, 5] = "nshu";
            Stnex[31, 1] = "nnsyo"; Stnex[31, 2] = "nnsho"; Stnex[31, 4] = "nsyo"; Stnex[31, 5] = "nsho";
            Stnex[32, 1] = "nnjyi"; Stnex[32, 2] = "nnzyi"; Stnex[32, 4] = "njyi"; Stnex[32, 5] = "nzyi";
            Stnex[33, 1] = "nnje"; Stnex[33, 2] = "nnjye"; Stnex[33, 3] = "nnzye"; Stnex[33, 4] = "nje"; Stnex[33, 5] = "njye"; Stnex[33, 6] = "nzye";
            Stnex[34, 1] = "nnja"; Stnex[34, 2] = "nnjya"; Stnex[34, 3] = "nnzya"; Stnex[34, 4] = "nja"; Stnex[34, 5] = "njya"; Stnex[34, 6] = "nzya";
            Stnex[35, 1] = "nnju"; Stnex[35, 2] = "nnjyu"; Stnex[35, 3] = "nnzyu"; Stnex[35, 4] = "nju"; Stnex[35, 5] = "njyu"; Stnex[35, 6] = "nzyu";
            Stnex[36, 1] = "nnjo"; Stnex[36, 2] = "nnjyo"; Stnex[36, 3] = "nnzyo"; Stnex[36, 4] = "njo"; Stnex[36, 5] = "njyo"; Stnex[36, 6] = "nzyo";
            Stnex[37, 1] = "nnswa"; Stnex[37, 4] = "nswa";
            Stnex[38, 1] = "nnswi"; Stnex[38, 4] = "nswi";
            Stnex[39, 1] = "nnswu"; Stnex[39, 4] = "nswu";
            Stnex[40, 1] = "nnswe"; Stnex[40, 4] = "nswe";
            Stnex[41, 1] = "nnswo"; Stnex[41, 4] = "nswo";
            Stnex[42, 1] = "nntyi"; Stnex[42, 2] = "nncyi"; Stnex[42, 4] = "ntyi"; Stnex[42, 5] = "ncyi";
            Stnex[43, 1] = "nntye"; Stnex[43, 2] = "nncye"; Stnex[43, 3] = "nnche"; Stnex[43, 4] = "ntye"; Stnex[43, 5] = "ncye"; Stnex[43, 6] = "nche";
            Stnex[44, 1] = "nntya"; Stnex[44, 2] = "nncya"; Stnex[44, 3] = "nncha"; Stnex[44, 4] = "ntya"; Stnex[44, 5] = "ncya"; Stnex[44, 6] = "ncha";
            Stnex[45, 1] = "nntyu"; Stnex[45, 2] = "nncyu"; Stnex[45, 3] = "nnchu"; Stnex[45, 4] = "ntyu"; Stnex[45, 5] = "ncyu"; Stnex[45, 6] = "nchu";
            Stnex[46, 1] = "nntyo"; Stnex[46, 2] = "nncyo"; Stnex[46, 3] = "nncho"; Stnex[46, 4] = "ntyo"; Stnex[46, 5] = "ncyo"; Stnex[46, 6] = "ncho";
            Stnex[47, 1] = "nndyi"; Stnex[47, 4] = "ndyi";
            Stnex[48, 1] = "nndye"; Stnex[48, 4] = "ndye";
            Stnex[49, 1] = "nndya"; Stnex[49, 4] = "ndya";
            Stnex[50, 1] = "nndyu"; Stnex[50, 4] = "ndyu";
            Stnex[51, 1] = "nndyo"; Stnex[51, 4] = "ndyo";
            Stnex[52, 1] = "nntsa"; Stnex[52, 4] = "ntsa";
            Stnex[53, 1] = "nntsi"; Stnex[53, 4] = "ntsi";
            Stnex[54, 1] = "nntse"; Stnex[54, 4] = "ntse";
            Stnex[55, 1] = "nntso"; Stnex[55, 4] = "ntso";
            Stnex[56, 1] = "nnthi"; Stnex[56, 4] = "nthi";
            Stnex[57, 1] = "nnthe"; Stnex[57, 4] = "nthe";
            Stnex[58, 1] = "nntha"; Stnex[58, 4] = "ntha";
            Stnex[59, 1] = "nnthu"; Stnex[59, 4] = "nthu";
            Stnex[60, 1] = "nntho"; Stnex[60, 4] = "ntho";
            Stnex[61, 1] = "nndhi"; Stnex[61, 4] = "ndhi";
            Stnex[62, 1] = "nndhe"; Stnex[62, 4] = "ndhe";
            Stnex[63, 1] = "nndha"; Stnex[63, 4] = "ndha";
            Stnex[64, 1] = "nndhu"; Stnex[64, 4] = "ndhu";
            Stnex[65, 1] = "nndho"; Stnex[65, 4] = "ndho";
            Stnex[66, 1] = "nntwa"; Stnex[66, 4] = "ntwa";
            Stnex[67, 1] = "nntwi"; Stnex[67, 4] = "ntwi";
            Stnex[68, 1] = "nntwu"; Stnex[68, 4] = "ntwu";
            Stnex[69, 1] = "nntwe"; Stnex[69, 4] = "ntwe";
            Stnex[70, 1] = "nntwo"; Stnex[70, 4] = "ntwo";
            Stnex[71, 1] = "nndwa"; Stnex[71, 4] = "ndwa";
            Stnex[72, 1] = "nndwi"; Stnex[72, 4] = "ndwi";
            Stnex[73, 1] = "nndwu"; Stnex[73, 4] = "ndwu";
            Stnex[74, 1] = "nndwe"; Stnex[74, 4] = "ndwe";
            Stnex[75, 1] = "nndwo"; Stnex[75, 4] = "ndwo";
            Stnex[76, 1] = "nnhyi"; Stnex[76, 4] = "nhyi";
            Stnex[77, 1] = "nnhye"; Stnex[77, 4] = "nhye";
            Stnex[78, 1] = "nnhya"; Stnex[78, 4] = "nhya";
            Stnex[79, 1] = "nnhyu"; Stnex[79, 4] = "nhyu";
            Stnex[80, 1] = "nnhyo"; Stnex[80, 4] = "nhyo";
            Stnex[81, 1] = "nnbyi"; Stnex[81, 4] = "nbyi";
            Stnex[82, 1] = "nnbye"; Stnex[82, 4] = "nbye";
            Stnex[83, 1] = "nnbya"; Stnex[83, 4] = "nbya";
            Stnex[84, 1] = "nnbyu"; Stnex[84, 4] = "nbyu";
            Stnex[85, 1] = "nnbyo"; Stnex[85, 4] = "nbyo";
            Stnex[86, 1] = "nnpyi"; Stnex[86, 4] = "npyi";
            Stnex[87, 1] = "nnpye"; Stnex[87, 4] = "npye";
            Stnex[88, 1] = "nnpya"; Stnex[88, 4] = "npya";
            Stnex[89, 1] = "nnpyu"; Stnex[89, 4] = "npyu";
            Stnex[90, 1] = "nnpyo"; Stnex[90, 4] = "npyo";
            Stnex[91, 1] = "nnfa"; Stnex[91, 2] = "nnfwa"; Stnex[91, 4] = "nfa"; Stnex[91, 5] = "nfwa";
            Stnex[92, 1] = "nnfi"; Stnex[92, 2] = "nnfyi"; Stnex[92, 3] = "nnfwi"; Stnex[92, 4] = "nfi"; Stnex[92, 5] = "nfyi"; Stnex[92, 6] = "nfwi";
            Stnex[93, 1] = "nnfwu"; Stnex[93, 4] = "nfwu";
            Stnex[94, 1] = "nnfe"; Stnex[94, 2] = "nnfye"; Stnex[94, 3] = "nnfwe"; Stnex[94, 4] = "nfe"; Stnex[94, 5] = "nfye"; Stnex[94, 6] = "nfwe";
            Stnex[95, 1] = "nnfo"; Stnex[95, 2] = "nnfwo"; Stnex[95, 4] = "nfo"; Stnex[95, 5] = "nfwo";
            Stnex[96, 1] = "nnfya"; Stnex[96, 4] = "nfya";
            Stnex[97, 1] = "nnfyu"; Stnex[97, 4] = "nfyu";
            Stnex[98, 1] = "nnfyo"; Stnex[98, 4] = "nfyo";
            Stnex[99, 1] = "nnmyi"; Stnex[99, 4] = "nmyi";
            Stnex[100, 1] = "nnmye"; Stnex[100, 4] = "nmye";
            Stnex[101, 1] = "nnmya"; Stnex[101, 4] = "nmya";
            Stnex[102, 1] = "nnmyu"; Stnex[102, 4] = "nmyu";
            Stnex[103, 1] = "nnmyo"; Stnex[103, 4] = "nmyo";
            Stnex[104, 1] = "nnryi"; Stnex[104, 4] = "nryi";
            Stnex[105, 1] = "nnrye"; Stnex[105, 4] = "nrye";
            Stnex[106, 1] = "nnrya"; Stnex[106, 4] = "nrya";
            Stnex[107, 1] = "nnryu"; Stnex[107, 4] = "nryu";
            Stnex[108, 1] = "nnryo"; Stnex[108, 4] = "nryo";
            Stnex[109, 1] = "nnva"; Stnex[109, 4] = "nva";
            Stnex[110, 1] = "nnvi"; Stnex[110, 2] = "nnvyi"; Stnex[110, 4] = "nvi"; Stnex[110, 5] = "nvyi";
            Stnex[111, 1] = "nnve"; Stnex[111, 2] = "nnvye"; Stnex[111, 4] = "nve"; Stnex[111, 5] = "nvye";
            Stnex[112, 1] = "nnvo"; Stnex[112, 4] = "nvo";
            Stnex[113, 1] = "nnvya"; Stnex[113, 4] = "nvya";
            Stnex[114, 1] = "nnvyu"; Stnex[114, 4] = "nvyu";
            Stnex[115, 1] = "nnvyo"; Stnex[115, 4] = "nvyo";
            for (int i = STNEX_COL / 2; i < 232; i++)
            {
                for (int k = 1; k < STNEX_ROW; k++)
                {
                    Stnex[i, k] = Stnex[i - (STNEX_COL / 2), k];
                }
            }

            Stnt[0, 1] = "nnkka"; Stnt[0, 2] = "nncca"; Stnt[0, 3] = "nnltuka"; Stnt[0, 4] = "nnltuca"; Stnt[0, 5] = "nkka"; Stnt[0, 6] = "ncca"; Stnt[0, 7] = "nltuka"; Stnt[0, 8] = "nltuca";
            Stnt[1, 1] = "nnkki"; Stnt[1, 2] = "nnltuki"; Stnt[1, 3] = "nnkki"; Stnt[1, 4] = "nnltuki"; Stnt[1, 5] = "nkki"; Stnt[1, 6] = "nltuki"; Stnt[1, 7] = "nkki"; Stnt[1, 8] = "nltuki";
            Stnt[2, 1] = "nnkku"; Stnt[2, 2] = "nnccu"; Stnt[2, 3] = "nnqqu"; Stnt[2, 4] = "nnltuku"; Stnt[2, 5] = "nnltucu"; Stnt[2, 6] = "nkku"; Stnt[2, 7] = "nccu"; Stnt[2, 8] = "nqqu"; Stnt[2, 9] = "nltuku"; Stnt[2, 10] = "nltucu";
            Stnt[3, 1] = "nnkke"; Stnt[3, 2] = "nnltuke"; Stnt[3, 3] = "nkke"; Stnt[3, 4] = "nltuke";
            Stnt[4, 1] = "nnkko"; Stnt[4, 2] = "nncco"; Stnt[4, 3] = "nnltuko"; Stnt[4, 4] = "nnltuco"; Stnt[4, 5] = "nkko"; Stnt[4, 6] = "ncco"; Stnt[4, 7] = "nltuko"; Stnt[4, 8] = "nltuco";
            Stnt[5, 1] = "nnssa"; Stnt[5, 2] = "nnltusa"; Stnt[5, 3] = "nssa"; Stnt[5, 4] = "nltusa";
            Stnt[6, 1] = "nnssi"; Stnt[6, 2] = "nnsshi"; Stnt[6, 3] = "nncci"; Stnt[6, 4] = "nnltusi"; Stnt[6, 5] = "nnltushi"; Stnt[6, 6] = "nssi"; Stnt[6, 7] = "nsshi"; Stnt[6, 8] = "ncci"; Stnt[6, 9] = "nltusi"; Stnt[6, 10] = "nltushi";
            Stnt[7, 1] = "nnssu"; Stnt[7, 2] = "nnltusu"; Stnt[7, 3] = "nssu"; Stnt[7, 4] = "nltusu";
            Stnt[8, 1] = "nnsse"; Stnt[8, 2] = "nncce"; Stnt[8, 3] = "nnltuse"; Stnt[8, 4] = "nnltuce"; Stnt[8, 5] = "nsse"; Stnt[8, 6] = "ncce"; Stnt[8, 7] = "nltuse"; Stnt[8, 8] = "nltuce";
            Stnt[9, 1] = "nnsso"; Stnt[9, 2] = "nnltuso"; Stnt[9, 3] = "nsso"; Stnt[9, 4] = "nltuso";
            Stnt[10, 1] = "nntta"; Stnt[10, 2] = "nnltuta"; Stnt[10, 3] = "ntta"; Stnt[10, 4] = "nltuta";
            Stnt[11, 1] = "nntti"; Stnt[11, 2] = "nncchi"; Stnt[11, 3] = "nnltuti"; Stnt[11, 4] = "nnltuchi"; Stnt[11, 5] = "ntti"; Stnt[11, 6] = "ncchi"; Stnt[11, 7] = "nltuti"; Stnt[11, 8] = "nltuchi";
            Stnt[12, 1] = "nnttu"; Stnt[12, 2] = "nnttsu"; Stnt[12, 3] = "nnltutu"; Stnt[12, 4] = "nnltutsu"; Stnt[12, 5] = "nttu"; Stnt[12, 6] = "nttsu"; Stnt[12, 7] = "nltutu"; Stnt[12, 8] = "nltutsu";
            Stnt[13, 1] = "nntte"; Stnt[13, 2] = "nnltute"; Stnt[13, 3] = "ntte"; Stnt[13, 4] = "nltute";
            Stnt[14, 1] = "nntto"; Stnt[14, 2] = "nnltuto"; Stnt[14, 3] = "ntto"; Stnt[14, 4] = "nltuto";
            Stnt[15, 1] = "nnhha"; Stnt[15, 2] = "nnltuha"; Stnt[15, 3] = "nhha"; Stnt[15, 4] = "nltuha";
            Stnt[16, 1] = "nnhhi"; Stnt[16, 2] = "nnltuhi"; Stnt[16, 3] = "nhhi"; Stnt[16, 4] = "nltuhi";
            Stnt[17, 1] = "nnhhu"; Stnt[17, 2] = "nnffu"; Stnt[17, 3] = "nnltuhu"; Stnt[17, 4] = "nnltufu"; Stnt[17, 5] = "nhhu"; Stnt[17, 6] = "nffu"; Stnt[17, 7] = "nltuhu"; Stnt[17, 8] = "nltufu";
            Stnt[18, 1] = "nnhhe"; Stnt[18, 2] = "nnltuhe"; Stnt[18, 3] = "nhhe"; Stnt[18, 4] = "nltuhe";
            Stnt[19, 1] = "nnhho"; Stnt[19, 2] = "nnltuho"; Stnt[19, 3] = "nhho"; Stnt[19, 4] = "nltuho";
            Stnt[20, 1] = "nnmma"; Stnt[20, 2] = "nnltuma"; Stnt[20, 3] = "nmma"; Stnt[20, 4] = "nltuma";
            Stnt[21, 1] = "nnmmi"; Stnt[21, 2] = "nnltumi"; Stnt[21, 3] = "nmmi"; Stnt[21, 4] = "nltumi";
            Stnt[22, 1] = "nnmmu"; Stnt[22, 2] = "nnltumu"; Stnt[22, 3] = "nmmu"; Stnt[22, 4] = "nltumu";
            Stnt[23, 1] = "nnmme"; Stnt[23, 2] = "nnltume"; Stnt[23, 3] = "nmme"; Stnt[23, 4] = "nltume";
            Stnt[24, 1] = "nnmmo"; Stnt[24, 2] = "nnltumo"; Stnt[24, 3] = "nmmo"; Stnt[24, 4] = "nltumo";
            Stnt[25, 1] = "nnyya"; Stnt[25, 2] = "nnltuya"; Stnt[25, 3] = "nyya"; Stnt[25, 4] = "nltuya";
            Stnt[26, 1] = "nnyyu"; Stnt[26, 2] = "nnltuyu"; Stnt[26, 3] = "nyyu"; Stnt[26, 4] = "nltuyu";
            Stnt[27, 1] = "nnyyo"; Stnt[27, 2] = "nnltuyo"; Stnt[27, 3] = "nyyo"; Stnt[27, 4] = "nltuyo";
            Stnt[28, 1] = "nnrra"; Stnt[28, 2] = "nnltura"; Stnt[28, 3] = "nrra"; Stnt[28, 4] = "nltura";
            Stnt[29, 1] = "nnrri"; Stnt[29, 2] = "nnlturi"; Stnt[29, 3] = "nrri"; Stnt[29, 4] = "nlturi";
            Stnt[30, 1] = "nnrru"; Stnt[30, 2] = "nnlturu"; Stnt[30, 3] = "nrru"; Stnt[30, 4] = "nlturu";
            Stnt[31, 1] = "nnrre"; Stnt[31, 2] = "nnlture"; Stnt[31, 3] = "nrre"; Stnt[31, 4] = "nlture";
            Stnt[32, 1] = "nnrro"; Stnt[32, 2] = "nnlturo"; Stnt[32, 3] = "nrro"; Stnt[32, 4] = "nlturo";
            Stnt[33, 1] = "nnwwa"; Stnt[33, 2] = "nnltuwa"; Stnt[33, 3] = "nwwa"; Stnt[33, 4] = "nltuwa";
            Stnt[34, 1] = "nnwwo"; Stnt[34, 2] = "nnltuwo"; Stnt[34, 3] = "nwwo"; Stnt[34, 4] = "nltuwo";
            Stnt[35, 1] = "nnlla"; Stnt[35, 2] = "nnxxa"; Stnt[35, 3] = "nnltula"; Stnt[35, 4] = "nlla"; Stnt[35, 5] = "nxxa"; Stnt[35, 6] = "nltula";
            Stnt[36, 1] = "nnlli"; Stnt[36, 2] = "nnxxi"; Stnt[36, 3] = "nnllyi"; Stnt[36, 4] = "nnxxyi"; Stnt[36, 5] = "nnltuli"; Stnt[36, 6] = "nlli"; Stnt[36, 7] = "nxxi"; Stnt[36, 8] = "nllyi"; Stnt[36, 9] = "nxxyi"; Stnt[36, 10] = "nltuli";
            Stnt[37, 1] = "nnllu"; Stnt[37, 2] = "nnxxu"; Stnt[37, 3] = "nnltulu"; Stnt[37, 4] = "nllu"; Stnt[37, 5] = "nxxu"; Stnt[37, 6] = "nltulu";
            Stnt[38, 1] = "nnlle"; Stnt[38, 2] = "nnxxe"; Stnt[38, 3] = "nnllye"; Stnt[38, 4] = "nnxxye"; Stnt[38, 5] = "nnltule"; Stnt[38, 6] = "nlle"; Stnt[38, 7] = "nxxe"; Stnt[38, 8] = "nllye"; Stnt[38, 9] = "nxxye"; Stnt[38, 10] = "nltule";
            Stnt[39, 1] = "nnllo"; Stnt[39, 2] = "nnxxo"; Stnt[39, 3] = "nnltulo"; Stnt[39, 4] = "nllo"; Stnt[39, 5] = "nxxo"; Stnt[39, 6] = "nltulo";
            Stnt[40, 1] = "nnlltu"; Stnt[40, 2] = "nnxxtu"; Stnt[40, 3] = "nnlltsu"; Stnt[40, 4] = "nnltultu"; Stnt[40, 5] = "nlltu"; Stnt[40, 6] = "nxxtu"; Stnt[40, 7] = "nlltsu"; Stnt[40, 8] = "nltultu";
            Stnt[41, 1] = "nnllya"; Stnt[41, 2] = "nnxxya"; Stnt[41, 3] = "nnltulya"; Stnt[41, 4] = "nllya"; Stnt[41, 5] = "nxxya"; Stnt[41, 6] = "nltulya";
            Stnt[42, 1] = "nnllyu"; Stnt[42, 2] = "nnxxyu"; Stnt[42, 3] = "nnltulyu"; Stnt[42, 4] = "nllyu"; Stnt[42, 5] = "nxxyu"; Stnt[42, 6] = "nltulyu";
            Stnt[43, 1] = "nnllyo"; Stnt[43, 2] = "nnxxyo"; Stnt[43, 3] = "nnltulyo"; Stnt[43, 4] = "nllyo"; Stnt[43, 5] = "nxxyo"; Stnt[43, 6] = "nltulyo";
            Stnt[44, 1] = "nnllwa"; Stnt[44, 2] = "nnxxwa"; Stnt[44, 3] = "nnltulwa"; Stnt[44, 4] = "nllwa"; Stnt[44, 5] = "nxxwa"; Stnt[44, 6] = "nltulwa";
            Stnt[45, 1] = "nnllka"; Stnt[45, 2] = "nnxxka"; Stnt[45, 3] = "nnltulka"; Stnt[45, 4] = "nllka"; Stnt[45, 5] = "nxxka"; Stnt[45, 6] = "nltulka";
            Stnt[46, 1] = "nnllke"; Stnt[46, 2] = "nnxxke"; Stnt[46, 3] = "nnltulke"; Stnt[46, 4] = "nllke"; Stnt[46, 5] = "nxxke"; Stnt[46, 6] = "nltulke";
            Stnt[47, 1] = "nngga"; Stnt[47, 2] = "nnltuga"; Stnt[47, 3] = "ngga"; Stnt[47, 4] = "nltuga";
            Stnt[48, 1] = "nnggi"; Stnt[48, 2] = "nnltugi"; Stnt[48, 3] = "nggi"; Stnt[48, 4] = "nltugi";
            Stnt[49, 1] = "nnggu"; Stnt[49, 2] = "nnltugu"; Stnt[49, 3] = "nggu"; Stnt[49, 4] = "nltugu";
            Stnt[50, 1] = "nngge"; Stnt[50, 2] = "nnltuge"; Stnt[50, 3] = "ngge"; Stnt[50, 4] = "nltuge";
            Stnt[51, 1] = "nnggo"; Stnt[51, 2] = "nnltugo"; Stnt[51, 3] = "nggo"; Stnt[51, 4] = "nltugo";
            Stnt[52, 1] = "nnzza"; Stnt[52, 2] = "nnltuza"; Stnt[52, 3] = "nzza"; Stnt[52, 4] = "nltuza";
            Stnt[53, 1] = "nnjji"; Stnt[53, 2] = "nnzzi"; Stnt[53, 3] = "nnltuji"; Stnt[53, 4] = "nnltuzi"; Stnt[53, 5] = "njji"; Stnt[53, 6] = "nzzi"; Stnt[53, 7] = "nltuji"; Stnt[53, 8] = "nltuzi";
            Stnt[54, 1] = "nnzzu"; Stnt[54, 2] = "nnltuzu"; Stnt[54, 3] = "nzzu"; Stnt[54, 4] = "nltuzu";
            Stnt[55, 1] = "nnzze"; Stnt[55, 2] = "nnltuze"; Stnt[55, 3] = "nzze"; Stnt[55, 4] = "nltuze";
            Stnt[56, 1] = "nnzzo"; Stnt[56, 2] = "nnltuzo"; Stnt[56, 3] = "nzzo"; Stnt[56, 4] = "nltuzo";
            Stnt[57, 1] = "nndda"; Stnt[57, 2] = "nnltuda"; Stnt[57, 3] = "ndda"; Stnt[57, 4] = "nltuda";
            Stnt[58, 1] = "nnddi"; Stnt[58, 2] = "nnltudi"; Stnt[58, 3] = "nddi"; Stnt[58, 4] = "nltudi";
            Stnt[59, 1] = "nnddu"; Stnt[59, 2] = "nnltudu"; Stnt[59, 3] = "nddu"; Stnt[59, 4] = "nltudu";
            Stnt[60, 1] = "nndde"; Stnt[60, 2] = "nnltude"; Stnt[60, 3] = "ndde"; Stnt[60, 4] = "nltude";
            Stnt[61, 1] = "nnddo"; Stnt[61, 2] = "nnltudo"; Stnt[61, 3] = "nddo"; Stnt[61, 4] = "nltudo";
            Stnt[62, 1] = "nnbba"; Stnt[62, 2] = "nnltuba"; Stnt[62, 3] = "nbba"; Stnt[62, 4] = "nltuba";
            Stnt[63, 1] = "nnbbi"; Stnt[63, 2] = "nnltubi"; Stnt[63, 3] = "nbbi"; Stnt[63, 4] = "nltubi";
            Stnt[64, 1] = "nnbbu"; Stnt[64, 2] = "nnltubu"; Stnt[64, 3] = "nbbu"; Stnt[64, 4] = "nltubu";
            Stnt[65, 1] = "nnbbe"; Stnt[65, 2] = "nnltube"; Stnt[65, 3] = "nbbe"; Stnt[65, 4] = "nltube";
            Stnt[66, 1] = "nnbbo"; Stnt[66, 2] = "nnltubo"; Stnt[66, 3] = "nbbo"; Stnt[66, 4] = "nltubo";
            Stnt[67, 1] = "nnppa"; Stnt[67, 2] = "nnltupa"; Stnt[67, 3] = "nppa"; Stnt[67, 4] = "nltupa";
            Stnt[68, 1] = "nnppi"; Stnt[68, 2] = "nnltupi"; Stnt[68, 3] = "nppi"; Stnt[68, 4] = "nltupi";
            Stnt[69, 1] = "nnppu"; Stnt[69, 2] = "nnltupu"; Stnt[69, 3] = "nppu"; Stnt[69, 4] = "nltupu";
            Stnt[70, 1] = "nnppe"; Stnt[70, 2] = "nnltupe"; Stnt[70, 3] = "nppe"; Stnt[70, 4] = "nltupe";
            Stnt[71, 1] = "nnppo"; Stnt[71, 2] = "nnltupo"; Stnt[71, 3] = "nppo"; Stnt[71, 4] = "nltupo";
            for (int i = STNT_COL / 2; i < STNT_COL; i++)
            {
                for (int k = 1; k < STNT_ROW; k++)
                {
                    Stnt[i, k] = Stnt[i - (STNT_COL / 2), k];
                }
            }

            Stntex[0, 1] = "nnyye"; Stntex[0, 2] = "nnltuye"; Stntex[0, 3] = "nyye"; Stntex[0, 4] = "nltuye";
            Stntex[1, 1] = "nnwwha"; Stntex[1, 2] = "nnltuwha"; Stntex[1, 3] = "nwwha"; Stntex[1, 4] = "nltuwha";
            Stntex[2, 1] = "nnwwi"; Stntex[2, 2] = "nnwwhi"; Stntex[2, 3] = "nnltuwi"; Stntex[2, 4] = "nnltuwhi"; Stntex[2, 5] = "nwwi"; Stntex[2, 6] = "nwwhi"; Stntex[2, 7] = "nltuwi"; Stntex[2, 8] = "nltuwhi";
            Stntex[3, 1] = "nnwwe"; Stntex[3, 2] = "nnwwhe"; Stntex[3, 3] = "nnltuwe"; Stntex[3, 4] = "nnltuwhe"; Stntex[3, 5] = "nwwe"; Stntex[3, 6] = "nwwhe"; Stntex[3, 7] = "nltuwe"; Stntex[3, 8] = "nltuwhe";
            Stntex[4, 1] = "nnwwho"; Stntex[4, 2] = "nnltuwho"; Stntex[4, 3] = "nwwho"; Stntex[4, 4] = "nltuwho";
            Stntex[5, 1] = "nnkkyi"; Stntex[5, 2] = "nnltukyi"; Stntex[5, 3] = "nkkyi"; Stntex[5, 4] = "nltukyi";
            Stntex[6, 1] = "nnkkye"; Stntex[6, 2] = "nnltukye"; Stntex[6, 3] = "nkkye"; Stntex[6, 4] = "nltukye";
            Stntex[7, 1] = "nnkkya"; Stntex[7, 2] = "nnltukya"; Stntex[7, 3] = "nkkya"; Stntex[7, 4] = "nltukya";
            Stntex[8, 1] = "nnkkyu"; Stntex[8, 2] = "nnltukyu"; Stntex[8, 3] = "nkkyu"; Stntex[8, 4] = "nltukyu";
            Stntex[9, 1] = "nnkkyo"; Stntex[9, 2] = "nnltukyo"; Stntex[9, 3] = "nkkyo"; Stntex[9, 4] = "nltukyo";
            Stntex[10, 1] = "nnggyi"; Stntex[10, 2] = "nnltugyi"; Stntex[10, 3] = "nggyi"; Stntex[10, 4] = "nltugyi";
            Stntex[11, 1] = "nnggye"; Stntex[11, 2] = "nnltugye"; Stntex[11, 3] = "nggye"; Stntex[11, 4] = "nltugye";
            Stntex[12, 1] = "nnggya"; Stntex[12, 2] = "nnltugya"; Stntex[12, 3] = "nggya"; Stntex[12, 4] = "nltugya";
            Stntex[13, 1] = "nnggyu"; Stntex[13, 2] = "nnltugyu"; Stntex[13, 3] = "nggyu"; Stntex[13, 4] = "nltugyu";
            Stntex[14, 1] = "nnggyo"; Stntex[14, 2] = "nnltugyo"; Stntex[14, 3] = "nggyo"; Stntex[14, 4] = "nltugyo";
            Stntex[15, 1] = "nnqqa"; Stntex[15, 2] = "nnqqwa"; Stntex[15, 3] = "nnkkwa"; Stntex[15, 4] = "nnltuqa"; Stntex[15, 5] = "nnltuqwa"; Stntex[15, 6] = "nqqa"; Stntex[15, 7] = "nqqwa"; Stntex[15, 8] = "nkkwa"; Stntex[15, 9] = "nltuqa"; Stntex[15, 10] = "nltuqwa";
            Stntex[16, 1] = "nnqqi"; Stntex[16, 2] = "nnqqwi"; Stntex[16, 3] = "nnqqyi"; Stntex[16, 4] = "nnltuqi"; Stntex[16, 5] = "nnltuqwi"; Stntex[16, 6] = "nqqi"; Stntex[16, 7] = "nqqwi"; Stntex[16, 8] = "nqqyi"; Stntex[16, 9] = "nltuqi"; Stntex[16, 10] = "nltuqwi";
            Stntex[17, 1] = "nnqqwu"; Stntex[17, 2] = "nnltuqwu"; Stntex[17, 3] = "nqqwu"; Stntex[17, 4] = "nltuqwu";
            Stntex[18, 1] = "nnqqe"; Stntex[18, 2] = "nnqqwe"; Stntex[18, 3] = "nnqqye"; Stntex[18, 4] = "nnltuqe"; Stntex[18, 5] = "nnltuqye"; Stntex[18, 6] = "nqqe"; Stntex[18, 7] = "nqqwe"; Stntex[18, 8] = "nqqye"; Stntex[18, 9] = "nltuqe"; Stntex[18, 10] = "nltuqye";
            Stntex[19, 1] = "nnqqo"; Stntex[19, 2] = "nnqqwo"; Stntex[19, 3] = "nnltuqo"; Stntex[19, 4] = "nnltuqwo"; Stntex[19, 5] = "nqqo"; Stntex[19, 6] = "nqqwo"; Stntex[19, 7] = "nltuqo"; Stntex[19, 8] = "nltuqwo";
            Stntex[20, 1] = "nnqqya"; Stntex[20, 2] = "nnltuqya"; Stntex[20, 3] = "nqqya"; Stntex[20, 4] = "nltuqya";
            Stntex[21, 1] = "nnqqyu"; Stntex[21, 2] = "nnltuqyu"; Stntex[21, 3] = "nqqyu"; Stntex[21, 4] = "nltuqyu";
            Stntex[22, 1] = "nnqqyo"; Stntex[22, 2] = "nnltuqyo"; Stntex[22, 3] = "nqqyo"; Stntex[22, 4] = "nltuqyo";
            Stntex[23, 1] = "nnggwa"; Stntex[23, 2] = "nnltugwa"; Stntex[23, 3] = "nggwa"; Stntex[23, 4] = "nltugwa";
            Stntex[24, 1] = "nnggwi"; Stntex[24, 2] = "nnltugwi"; Stntex[24, 3] = "nggwi"; Stntex[24, 4] = "nltugwi";
            Stntex[25, 1] = "nnggwu"; Stntex[25, 2] = "nnltugwu"; Stntex[25, 3] = "nggwu"; Stntex[25, 4] = "nltugwu";
            Stntex[26, 1] = "nnggwe"; Stntex[26, 2] = "nnltugwe"; Stntex[26, 3] = "nggwe"; Stntex[26, 4] = "nltugwe";
            Stntex[27, 1] = "nnggwo"; Stntex[27, 2] = "nnltugwo"; Stntex[27, 3] = "nggwo"; Stntex[27, 4] = "nltugwo";
            Stntex[28, 1] = "nnssyi"; Stntex[28, 2] = "nnltusyi"; Stntex[28, 3] = "nssyi"; Stntex[28, 4] = "nltusyi";
            Stntex[29, 1] = "nnssye"; Stntex[29, 2] = "nnsshe"; Stntex[29, 3] = "nnltusye"; Stntex[29, 4] = "nnltushe"; Stntex[29, 5] = "nssye"; Stntex[29, 6] = "nsshe"; Stntex[29, 7] = "nltusye"; Stntex[29, 8] = "nltushe";
            Stntex[30, 1] = "nnssya"; Stntex[30, 2] = "nnssha"; Stntex[30, 3] = "nnltusya"; Stntex[30, 4] = "nnltusha"; Stntex[30, 5] = "nssya"; Stntex[30, 6] = "nssha"; Stntex[30, 7] = "nltusya"; Stntex[30, 8] = "nltusha";
            Stntex[31, 1] = "nnssyu"; Stntex[31, 2] = "nnsshu"; Stntex[31, 3] = "nnltusyu"; Stntex[31, 4] = "nnltushu"; Stntex[31, 5] = "nssyu"; Stntex[31, 6] = "nsshu"; Stntex[31, 7] = "nltusyu"; Stntex[31, 8] = "nltushu";
            Stntex[32, 1] = "nnssyo"; Stntex[32, 2] = "nnssho"; Stntex[32, 3] = "nnltusyo"; Stntex[32, 4] = "nnltusho"; Stntex[32, 5] = "nssyo"; Stntex[32, 6] = "nssho"; Stntex[32, 7] = "nltusyo"; Stntex[32, 8] = "nltusho";
            Stntex[33, 1] = "nnjjyi"; Stntex[33, 2] = "nnzzyi"; Stntex[33, 3] = "nnltujyi"; Stntex[33, 4] = "nnltuzyi"; Stntex[33, 5] = "njjyi"; Stntex[33, 6] = "nzzyi"; Stntex[33, 7] = "nltujyi"; Stntex[33, 8] = "nltuzyi";
            Stntex[34, 1] = "nnjje"; Stntex[34, 2] = "nnjjye"; Stntex[34, 3] = "nnzzye"; Stntex[34, 4] = "nnltuje"; Stntex[34, 5] = "nnltujye"; Stntex[34, 6] = "nnltuzye"; Stntex[34, 7] = "njje"; Stntex[34, 8] = "njjye"; Stntex[34, 9] = "nzzye"; Stntex[34, 10] = "nltuje"; Stntex[34, 11] = "nltujye"; Stntex[34, 12] = "nltuzye";
            Stntex[35, 1] = "nnjja"; Stntex[35, 2] = "nnjjya"; Stntex[35, 3] = "nnzzya"; Stntex[35, 4] = "nnltuja"; Stntex[35, 5] = "nnltujya"; Stntex[35, 6] = "nnltuzya"; Stntex[35, 7] = "njja"; Stntex[35, 8] = "njjya"; Stntex[35, 9] = "nzzya"; Stntex[35, 10] = "nltuja"; Stntex[35, 11] = "nltujya"; Stntex[35, 12] = "nltuzya";
            Stntex[36, 1] = "nnjju"; Stntex[36, 2] = "nnjjyu"; Stntex[36, 3] = "nnzzyu"; Stntex[36, 4] = "nnltuju"; Stntex[36, 5] = "nnltujyu"; Stntex[36, 6] = "nnltuzyu"; Stntex[36, 7] = "njju"; Stntex[36, 8] = "njjyu"; Stntex[36, 9] = "nzzyu"; Stntex[36, 10] = "nltuju"; Stntex[36, 11] = "nltujyu"; Stntex[36, 12] = "nltuzyu";
            Stntex[37, 1] = "nnjjo"; Stntex[37, 2] = "nnjjyo"; Stntex[37, 3] = "nnzzyo"; Stntex[37, 4] = "nnltujo"; Stntex[37, 5] = "nnltujyo"; Stntex[37, 6] = "nnltuzyo"; Stntex[37, 7] = "njjo"; Stntex[37, 8] = "njjyo"; Stntex[37, 9] = "nzzyo"; Stntex[37, 10] = "nltujo"; Stntex[37, 11] = "nltujyo"; Stntex[37, 12] = "nltuzyo";
            Stntex[38, 1] = "nnsswa"; Stntex[38, 2] = "nnltuswa"; Stntex[38, 3] = "nsswa"; Stntex[38, 4] = "nltuswa";
            Stntex[39, 1] = "nnsswi"; Stntex[39, 2] = "nnltuswi"; Stntex[39, 3] = "nsswi"; Stntex[39, 4] = "nltuswi";
            Stntex[40, 1] = "nnsswu"; Stntex[40, 2] = "nnltuswu"; Stntex[40, 3] = "nsswu"; Stntex[40, 4] = "nltuswu";
            Stntex[41, 1] = "nnsswe"; Stntex[41, 2] = "nnltuswe"; Stntex[41, 3] = "nsswe"; Stntex[41, 4] = "nltuswe";
            Stntex[42, 1] = "nnsswo"; Stntex[42, 2] = "nnltuswo"; Stntex[42, 3] = "nsswo"; Stntex[42, 4] = "nltuswo";
            Stntex[43, 1] = "nnttyi"; Stntex[43, 2] = "nnccyi"; Stntex[43, 3] = "nnltutyi"; Stntex[43, 4] = "nnltucyi"; Stntex[43, 5] = "nttyi"; Stntex[43, 6] = "nccyi"; Stntex[43, 7] = "nltutyi"; Stntex[43, 8] = "nltucyi";
            Stntex[44, 1] = "nnttye"; Stntex[44, 2] = "nnccye"; Stntex[44, 3] = "nncche"; Stntex[44, 4] = "nnltutye"; Stntex[44, 5] = "nnltucye"; Stntex[44, 6] = "nnltuche"; Stntex[44, 7] = "nttye"; Stntex[44, 8] = "nccye"; Stntex[44, 9] = "ncche"; Stntex[44, 10] = "nltutye"; Stntex[44, 11] = "nltucye"; Stntex[44, 12] = "nltuche";
            Stntex[45, 1] = "nnttya"; Stntex[45, 2] = "nnccya"; Stntex[45, 3] = "nnccha"; Stntex[45, 4] = "nnltutya"; Stntex[45, 5] = "nnltucya"; Stntex[45, 6] = "nnltucha"; Stntex[45, 7] = "nttya"; Stntex[45, 8] = "nccya"; Stntex[45, 9] = "nccha"; Stntex[45, 10] = "nltutya"; Stntex[45, 11] = "nltucya"; Stntex[45, 12] = "nltucha";
            Stntex[46, 1] = "nnttyu"; Stntex[46, 2] = "nnccyu"; Stntex[46, 3] = "nncchu"; Stntex[46, 4] = "nnltutyu"; Stntex[46, 5] = "nnltucyu"; Stntex[46, 6] = "nnltuchu"; Stntex[46, 7] = "nttyu"; Stntex[46, 8] = "nccyu"; Stntex[46, 9] = "ncchu"; Stntex[46, 10] = "nltutyu"; Stntex[46, 11] = "nltucyu"; Stntex[46, 12] = "nltuchu";
            Stntex[47, 1] = "nnttyo"; Stntex[47, 2] = "nnccyo"; Stntex[47, 3] = "nnccho"; Stntex[47, 4] = "nnltutyo"; Stntex[47, 5] = "nnltucyo"; Stntex[47, 6] = "nnltucho"; Stntex[47, 7] = "nttyo"; Stntex[47, 8] = "nccyo"; Stntex[47, 9] = "nccho"; Stntex[47, 10] = "nltutyo"; Stntex[47, 11] = "nltucyo"; Stntex[47, 12] = "nltucho";
            Stntex[48, 1] = "nnddyi"; Stntex[48, 2] = "nnltudyi"; Stntex[48, 3] = "nddyi"; Stntex[48, 4] = "nltudyi";
            Stntex[49, 1] = "nnddye"; Stntex[49, 2] = "nnltudye"; Stntex[49, 3] = "nddye"; Stntex[49, 4] = "nltudye";
            Stntex[50, 1] = "nnddya"; Stntex[50, 2] = "nnltudya"; Stntex[50, 3] = "nddya"; Stntex[50, 4] = "nltudya";
            Stntex[51, 1] = "nnddyu"; Stntex[51, 2] = "nnltudyu"; Stntex[51, 3] = "nddyu"; Stntex[51, 4] = "nltudyu";
            Stntex[52, 1] = "nnddyo"; Stntex[52, 2] = "nnltudyo"; Stntex[52, 3] = "nddyo"; Stntex[52, 4] = "nltudyo";
            Stntex[53, 1] = "nnttsa"; Stntex[53, 2] = "nnltutsa"; Stntex[53, 3] = "nttsa"; Stntex[53, 4] = "nltutsa";
            Stntex[54, 1] = "nnttsi"; Stntex[54, 2] = "nnltutsi"; Stntex[54, 3] = "nttsi"; Stntex[54, 4] = "nltutsi";
            Stntex[55, 1] = "nnttse"; Stntex[55, 2] = "nnltutse"; Stntex[55, 3] = "nttse"; Stntex[55, 4] = "nltutse";
            Stntex[56, 1] = "nnttso"; Stntex[56, 2] = "nnltutso"; Stntex[56, 3] = "nttso"; Stntex[56, 4] = "nltutso";
            Stntex[57, 1] = "nntthi"; Stntex[57, 2] = "nnltuthi"; Stntex[57, 3] = "ntthi"; Stntex[57, 4] = "nltuthi";
            Stntex[58, 1] = "nntthe"; Stntex[58, 2] = "nnltuthe"; Stntex[58, 3] = "ntthe"; Stntex[58, 4] = "nltuthe";
            Stntex[59, 1] = "nnttha"; Stntex[59, 2] = "nnltutha"; Stntex[59, 3] = "nttha"; Stntex[59, 4] = "nltutha";
            Stntex[60, 1] = "nntthu"; Stntex[60, 2] = "nnltuthu"; Stntex[60, 3] = "ntthu"; Stntex[60, 4] = "nltuthu";
            Stntex[61, 1] = "nnttho"; Stntex[61, 2] = "nnltutho"; Stntex[61, 3] = "nttho"; Stntex[61, 4] = "nltutho";
            Stntex[62, 1] = "nnddhi"; Stntex[62, 2] = "nnltudhi"; Stntex[62, 3] = "nddhi"; Stntex[62, 4] = "nltudhi";
            Stntex[63, 1] = "nnddhe"; Stntex[63, 2] = "nnltudhe"; Stntex[63, 3] = "nddhe"; Stntex[63, 4] = "nltudhe";
            Stntex[64, 1] = "nnddha"; Stntex[64, 2] = "nnltudha"; Stntex[64, 3] = "nddha"; Stntex[64, 4] = "nltudha";
            Stntex[65, 1] = "nnddhu"; Stntex[65, 2] = "nnltudhu"; Stntex[65, 3] = "nddhu"; Stntex[65, 4] = "nltudhu";
            Stntex[66, 1] = "nnddho"; Stntex[66, 2] = "nnltudho"; Stntex[66, 3] = "nddho"; Stntex[66, 4] = "nltudho";
            Stntex[67, 1] = "nnttwa"; Stntex[67, 2] = "nnltutwa"; Stntex[67, 3] = "nttwa"; Stntex[67, 4] = "nltutwa";
            Stntex[68, 1] = "nnttwi"; Stntex[68, 2] = "nnltutwi"; Stntex[68, 3] = "nttwi"; Stntex[68, 4] = "nltutwi";
            Stntex[69, 1] = "nnttwu"; Stntex[69, 2] = "nnltutwu"; Stntex[69, 3] = "nttwu"; Stntex[69, 4] = "nltutwu";
            Stntex[70, 1] = "nnttwe"; Stntex[70, 2] = "nnltutwe"; Stntex[70, 3] = "nttwe"; Stntex[70, 4] = "nltutwe";
            Stntex[71, 1] = "nnttwo"; Stntex[71, 2] = "nnltutwo"; Stntex[71, 3] = "nttwo"; Stntex[71, 4] = "nltutwo";
            Stntex[72, 1] = "nnddwa"; Stntex[72, 2] = "nnltudwa"; Stntex[72, 3] = "nddwa"; Stntex[72, 4] = "nltudwa";
            Stntex[73, 1] = "nnddwi"; Stntex[73, 2] = "nnltudwi"; Stntex[73, 3] = "nddwi"; Stntex[73, 4] = "nltudwi";
            Stntex[74, 1] = "nnddwu"; Stntex[74, 2] = "nnltudwu"; Stntex[74, 3] = "nddwu"; Stntex[74, 4] = "nltudwu";
            Stntex[75, 1] = "nnddwe"; Stntex[75, 2] = "nnltudwe"; Stntex[75, 3] = "nddwe"; Stntex[75, 4] = "nltudwe";
            Stntex[76, 1] = "nnddwo"; Stntex[76, 2] = "nnltudwo"; Stntex[76, 3] = "nddwo"; Stntex[76, 4] = "nltudwo";
            Stntex[77, 1] = "nnhhyi"; Stntex[77, 2] = "nnltuhyi"; Stntex[77, 3] = "nhhyi"; Stntex[77, 4] = "nltuhyi";
            Stntex[78, 1] = "nnhhye"; Stntex[78, 2] = "nnltuhye"; Stntex[78, 3] = "nhhye"; Stntex[78, 4] = "nltuhye";
            Stntex[79, 1] = "nnhhya"; Stntex[79, 2] = "nnltuhya"; Stntex[79, 3] = "nhhya"; Stntex[79, 4] = "nltuhya";
            Stntex[80, 1] = "nnhhyu"; Stntex[80, 2] = "nnltuhyu"; Stntex[80, 3] = "nhhyu"; Stntex[80, 4] = "nltuhyu";
            Stntex[81, 1] = "nnhhyo"; Stntex[81, 2] = "nnltuhyo"; Stntex[81, 3] = "nhhyo"; Stntex[81, 4] = "nltuhyo";
            Stntex[82, 1] = "nnbbyi"; Stntex[82, 2] = "nnltubyi"; Stntex[82, 3] = "nbbyi"; Stntex[82, 4] = "nltubyi";
            Stntex[83, 1] = "nnbbye"; Stntex[83, 2] = "nnltubye"; Stntex[83, 3] = "nbbye"; Stntex[83, 4] = "nltubye";
            Stntex[84, 1] = "nnbbya"; Stntex[84, 2] = "nnltubya"; Stntex[84, 3] = "nbbya"; Stntex[84, 4] = "nltubya";
            Stntex[85, 1] = "nnbbyu"; Stntex[85, 2] = "nnltubyu"; Stntex[85, 3] = "nbbyu"; Stntex[85, 4] = "nltubyu";
            Stntex[86, 1] = "nnbbyo"; Stntex[86, 2] = "nnltubyo"; Stntex[86, 3] = "nbbyo"; Stntex[86, 4] = "nltubyo";
            Stntex[87, 1] = "nnppyi"; Stntex[87, 2] = "nnltupyi"; Stntex[87, 3] = "nppyi"; Stntex[87, 4] = "nltupyi";
            Stntex[88, 1] = "nnppye"; Stntex[88, 2] = "nnltupye"; Stntex[88, 3] = "nppye"; Stntex[88, 4] = "nltupye";
            Stntex[89, 1] = "nnppya"; Stntex[89, 2] = "nnltupya"; Stntex[89, 3] = "nppya"; Stntex[89, 4] = "nltupya";
            Stntex[90, 1] = "nnppyu"; Stntex[90, 2] = "nnltupyu"; Stntex[90, 3] = "nppyu"; Stntex[90, 4] = "nltupyu";
            Stntex[91, 1] = "nnppyo"; Stntex[91, 2] = "nnltupyo"; Stntex[91, 3] = "nppyo"; Stntex[91, 4] = "nltupyo";
            Stntex[92, 1] = "nnffa"; Stntex[92, 2] = "nnffwa"; Stntex[92, 3] = "nnltufa"; Stntex[92, 4] = "nnltufwa"; Stntex[92, 5] = "nffa"; Stntex[92, 6] = "nffwa"; Stntex[92, 7] = "nltufa"; Stntex[92, 8] = "nltufwa";
            Stntex[93, 1] = "nnffi"; Stntex[93, 2] = "nnffyi"; Stntex[93, 3] = "nnffwi"; Stntex[93, 4] = "nnltufi"; Stntex[93, 5] = "nnltufyi"; Stntex[93, 6] = "nnltufwi"; Stntex[93, 7] = "nffi"; Stntex[93, 8] = "nffyi"; Stntex[93, 9] = "nffwi"; Stntex[93, 10] = "nltufi"; Stntex[93, 11] = "nltufyi"; Stntex[93, 12] = "nltufwi";
            Stntex[94, 1] = "nnffwu"; Stntex[94, 2] = "nnltufwu"; Stntex[94, 3] = "nffwu"; Stntex[94, 4] = "nltufwu";
            Stntex[95, 1] = "nnffe"; Stntex[95, 2] = "nnffye"; Stntex[95, 3] = "nnffwe"; Stntex[95, 4] = "nnltufe"; Stntex[95, 5] = "nnltufye"; Stntex[95, 6] = "nnltufwe"; Stntex[95, 7] = "nffe"; Stntex[95, 8] = "nffye"; Stntex[95, 9] = "nffwe"; Stntex[95, 10] = "nltufe"; Stntex[95, 11] = "nltufye"; Stntex[95, 12] = "nltufwe";
            Stntex[96, 1] = "nnffo"; Stntex[96, 2] = "nnffwo"; Stntex[96, 3] = "nnltufo"; Stntex[96, 4] = "nnltufwo"; Stntex[96, 5] = "nffo"; Stntex[96, 6] = "nffwo"; Stntex[96, 7] = "nltufo"; Stntex[96, 8] = "nltufwo";
            Stntex[97, 1] = "nnffya"; Stntex[97, 2] = "nnltufya"; Stntex[97, 3] = "nffya"; Stntex[97, 4] = "nltufya";
            Stntex[98, 1] = "nnffyu"; Stntex[98, 2] = "nnltufyu"; Stntex[98, 3] = "nffyu"; Stntex[98, 4] = "nltufyu";
            Stntex[99, 1] = "nnffyo"; Stntex[99, 2] = "nnltufyo"; Stntex[99, 3] = "nffyo"; Stntex[99, 4] = "nltufyo";
            Stntex[100, 1] = "nnmmyi"; Stntex[100, 2] = "nnltumyi"; Stntex[100, 3] = "nmmyi"; Stntex[100, 4] = "nltumyi";
            Stntex[101, 1] = "nnmmye"; Stntex[101, 2] = "nnltumye"; Stntex[101, 3] = "nmmye"; Stntex[101, 4] = "nltumye";
            Stntex[102, 1] = "nnmmya"; Stntex[102, 2] = "nnltumya"; Stntex[102, 3] = "nmmya"; Stntex[102, 4] = "nltumya";
            Stntex[103, 1] = "nnmmyu"; Stntex[103, 2] = "nnltumyu"; Stntex[103, 3] = "nmmyu"; Stntex[103, 4] = "nltumyu";
            Stntex[104, 1] = "nnmmyo"; Stntex[104, 2] = "nnltumyo"; Stntex[104, 3] = "nmmyo"; Stntex[104, 4] = "nltumyo";
            Stntex[105, 1] = "nnrryi"; Stntex[105, 2] = "nnlturyi"; Stntex[105, 3] = "nrryi"; Stntex[105, 4] = "nlturyi";
            Stntex[106, 1] = "nnrrye"; Stntex[106, 2] = "nnlturye"; Stntex[106, 3] = "nrrye"; Stntex[106, 4] = "nlturye";
            Stntex[107, 1] = "nnrrya"; Stntex[107, 2] = "nnlturya"; Stntex[107, 3] = "nrrya"; Stntex[107, 4] = "nlturya";
            Stntex[108, 1] = "nnrryu"; Stntex[108, 2] = "nnlturyu"; Stntex[108, 3] = "nrryu"; Stntex[108, 4] = "nlturyu";
            Stntex[109, 1] = "nnrryo"; Stntex[109, 2] = "nnlturyo"; Stntex[109, 3] = "nrryo"; Stntex[109, 4] = "nlturyo";
            Stntex[110, 1] = "nnvva"; Stntex[110, 2] = "nnltuva"; Stntex[110, 3] = "nvva"; Stntex[110, 4] = "nltuva";
            Stntex[111, 1] = "nnvvi"; Stntex[111, 2] = "nnvvyi"; Stntex[111, 3] = "nnltuvi"; Stntex[111, 4] = "nnltuvyi"; Stntex[111, 5] = "nvvi"; Stntex[111, 6] = "nvvyi"; Stntex[111, 7] = "nltuvi"; Stntex[111, 8] = "nltuvyi";
            Stntex[112, 1] = "nnvve"; Stntex[112, 2] = "nnvvye"; Stntex[112, 3] = "nnltuve"; Stntex[112, 4] = "nnltuvye"; Stntex[112, 5] = "nvve"; Stntex[112, 6] = "nvvye"; Stntex[112, 7] = "nltuve"; Stntex[112, 8] = "nltuvye";
            Stntex[113, 1] = "nnvvo"; Stntex[113, 2] = "nnltuvo"; Stntex[113, 3] = "nvvo"; Stntex[113, 4] = "nltuvo";
            Stntex[114, 1] = "nnvvya"; Stntex[114, 2] = "nnltuvya"; Stntex[114, 3] = "nvvya"; Stntex[114, 4] = "nltuvya";
            Stntex[115, 1] = "nnvvyu"; Stntex[115, 2] = "nnltuvyu"; Stntex[115, 3] = "nvvyu"; Stntex[115, 4] = "nltuvyu";
            Stntex[116, 1] = "nnvvyo"; Stntex[116, 2] = "nnltuvyo"; Stntex[116, 3] = "nvvyo"; Stntex[116, 4] = "nltuvyo";
            for (int i = STNTEX_COL / 2; i < STNTEX_COL; i++)
            {
                for (int k = 1; k < STNTEX_ROW; k++)
                {
                    Stntex[i, k] = Stntex[i - (STNTEX_COL / 2), k];
                }
            }
        }
    }
    public void SetSYA(int sya, int n)
    {
        if (sya == 1)
        {
            //SYA SYU SYO
            Stex[29, 1] = "sye"; Stex[29, 2] = "she";// Stex[29, 3] = "sile"; Stex[29, 4] = "shile";
            Stex[30, 1] = "sya"; Stex[30, 2] = "sha";// Stex[30, 3] = "silya"; Stex[30, 4] = "shilya";
            Stex[31, 1] = "syu"; Stex[31, 2] = "shu";// Stex[31, 3] = "silyu"; Stex[31, 4] = "shilyu";
            Stex[32, 1] = "syo"; Stex[32, 2] = "sho";// Stex[32, 3] = "silyo"; Stex[32, 4] = "shilyo";

            Stex[151, 1] = "sye"; Stex[151, 2] = "she";// Stex[151, 3] = "sile"; Stex[151, 4] = "shile";
            Stex[152, 1] = "sya"; Stex[152, 2] = "sha";// Stex[152, 3] = "silya"; Stex[152, 4] = "shilya";
            Stex[153, 1] = "syu"; Stex[153, 2] = "shu";// Stex[153, 3] = "silyu"; Stex[153, 4] = "shilyu";
            Stex[154, 1] = "syo"; Stex[154, 2] = "sho";// Stex[154, 3] = "silyo"; Stex[154, 4] = "shilyo";

            Sttex[29, 1] = "ssye"; Sttex[29, 2] = "sshe"; Sttex[29, 3] = "ltusye"; Sttex[29, 4] = "ltushe";
            Sttex[30, 1] = "ssya"; Sttex[30, 2] = "ssha"; Sttex[30, 3] = "ltusya"; Sttex[30, 4] = "ltusha";
            Sttex[31, 1] = "ssyu"; Sttex[31, 2] = "sshu"; Sttex[31, 3] = "ltusyu"; Sttex[31, 4] = "ltushu";
            Sttex[32, 1] = "ssyo"; Sttex[32, 2] = "ssho"; Sttex[32, 3] = "ltusyo"; Sttex[32, 4] = "ltusho";

            Sttex[146, 1] = "ssye"; Sttex[146, 2] = "sshe"; Sttex[146, 3] = "ltusye"; Sttex[146, 4] = "ltushe";
            Sttex[147, 1] = "ssya"; Sttex[147, 2] = "ssha"; Sttex[147, 3] = "ltusya"; Sttex[147, 4] = "ltusha";
            Sttex[148, 1] = "ssyu"; Sttex[148, 2] = "sshu"; Sttex[148, 3] = "ltusyu"; Sttex[148, 4] = "ltushu";
            Sttex[149, 1] = "ssyo"; Sttex[149, 2] = "ssho"; Sttex[149, 3] = "ltusyo"; Sttex[149, 4] = "ltusho";

            Stnex[28, 1] = "nnsye"; Stnex[28, 2] = "nnshe"; Stnex[28, 4] = "nsye"; Stnex[28, 5] = "nshe";
            Stnex[29, 1] = "nnsya"; Stnex[29, 2] = "nnsha"; Stnex[29, 4] = "nsya"; Stnex[29, 5] = "nsha";
            Stnex[30, 1] = "nnsyu"; Stnex[30, 2] = "nnshu"; Stnex[30, 4] = "nsyu"; Stnex[30, 5] = "nshu";
            Stnex[31, 1] = "nnsyo"; Stnex[31, 2] = "nnsho"; Stnex[31, 4] = "nsyo"; Stnex[31, 5] = "nsho";

            Stnex[144, 1] = "nnsye"; Stnex[144, 2] = "nnshe"; Stnex[144, 4] = "nsye"; Stnex[144, 5] = "nshe";
            Stnex[145, 1] = "nnsya"; Stnex[145, 2] = "nnsha"; Stnex[145, 4] = "nsya"; Stnex[145, 5] = "nsha";
            Stnex[146, 1] = "nnsyu"; Stnex[146, 2] = "nnshu"; Stnex[146, 4] = "nsyu"; Stnex[146, 5] = "nshu";
            Stnex[147, 1] = "nnsyo"; Stnex[147, 2] = "nnsho"; Stnex[147, 4] = "nsyo"; Stnex[147, 5] = "nsho";

            Stntex[29, 1] = "nnssye"; Stntex[29, 2] = "nnsshe"; Stntex[29, 3] = "nnltusye"; Stntex[29, 4] = "nnltushe"; Stntex[29, 5] = "nssye"; Stntex[29, 6] = "nsshe"; Stntex[29, 7] = "nltusye"; Stntex[29, 8] = "nltushe";
            Stntex[30, 1] = "nnssya"; Stntex[30, 2] = "nnssha"; Stntex[30, 3] = "nnltusya"; Stntex[30, 4] = "nnltusha"; Stntex[30, 5] = "nssya"; Stntex[30, 6] = "nssha"; Stntex[30, 7] = "nltusya"; Stntex[30, 8] = "nltusha";
            Stntex[31, 1] = "nnssyu"; Stntex[31, 2] = "nnsshu"; Stntex[31, 3] = "nnltusyu"; Stntex[31, 4] = "nnltushu"; Stntex[31, 5] = "nssyu"; Stntex[31, 6] = "nsshu"; Stntex[31, 7] = "nltusyu"; Stntex[31, 8] = "nltushu";
            Stntex[32, 1] = "nnssyo"; Stntex[32, 2] = "nnssho"; Stntex[32, 3] = "nnltusyo"; Stntex[32, 4] = "nnltusho"; Stntex[32, 5] = "nssyo"; Stntex[32, 6] = "nssho"; Stntex[32, 7] = "nltusyo"; Stntex[32, 8] = "nltusho";

            Stntex[146, 1] = "nnssye"; Stntex[146, 2] = "nnsshe"; Stntex[146, 3] = "nnltusye"; Stntex[146, 4] = "nnltushe"; Stntex[146, 5] = "nssye"; Stntex[146, 6] = "nsshe"; Stntex[146, 7] = "nltusye"; Stntex[146, 8] = "nltushe";
            Stntex[147, 1] = "nnssya"; Stntex[147, 2] = "nnssha"; Stntex[147, 3] = "nnltusya"; Stntex[147, 4] = "nnltusha"; Stntex[147, 5] = "nssya"; Stntex[147, 6] = "nssha"; Stntex[147, 7] = "nltusya"; Stntex[147, 8] = "nltusha";
            Stntex[148, 1] = "nnssyu"; Stntex[148, 2] = "nnsshu"; Stntex[148, 3] = "nnltusyu"; Stntex[148, 4] = "nnltushu"; Stntex[148, 5] = "nssyu"; Stntex[148, 6] = "nsshu"; Stntex[148, 7] = "nltusyu"; Stntex[148, 8] = "nltushu";
            Stntex[149, 1] = "nnssyo"; Stntex[149, 2] = "nnssho"; Stntex[149, 3] = "nnltusyo"; Stntex[149, 4] = "nnltusho"; Stntex[149, 5] = "nssyo"; Stntex[149, 6] = "nssho"; Stntex[149, 7] = "nltusyo"; Stntex[149, 8] = "nltusho";
            if (n == 1)
            {
                Stnex[28, 1] = "nsye"; Stnex[28, 2] = "nshe"; Stnex[28, 4] = "nnsye"; Stnex[28, 5] = "nnshe";
                Stnex[29, 1] = "nsya"; Stnex[29, 2] = "nsha"; Stnex[29, 4] = "nnsya"; Stnex[29, 5] = "nnsha";
                Stnex[30, 1] = "nsyu"; Stnex[30, 2] = "nshu"; Stnex[30, 4] = "nnsyu"; Stnex[30, 5] = "nnshu";
                Stnex[31, 1] = "nsyo"; Stnex[31, 2] = "nsho"; Stnex[31, 4] = "nnsyo"; Stnex[31, 5] = "nnsho";

                Stnex[144, 1] = "nsye"; Stnex[144, 2] = "nshe"; Stnex[144, 4] = "nnsye"; Stnex[144, 5] = "nnshe";
                Stnex[145, 1] = "nsya"; Stnex[145, 2] = "nsha"; Stnex[145, 4] = "nnsya"; Stnex[145, 5] = "nnsha";
                Stnex[146, 1] = "nsyu"; Stnex[146, 2] = "nshu"; Stnex[146, 4] = "nnsyu"; Stnex[146, 5] = "nnshu";
                Stnex[147, 1] = "nsyo"; Stnex[147, 2] = "nsho"; Stnex[147, 4] = "nnsyo"; Stnex[147, 5] = "nnsho";

                Stntex[29, 1] = "nssye"; Stntex[29, 2] = "nsshe"; Stntex[29, 3] = "nltusye"; Stntex[29, 4] = "nltushe"; Stntex[29, 5] = "nnssye"; Stntex[29, 6] = "nnsshe"; Stntex[29, 7] = "nnltusye"; Stntex[29, 8] = "nnltushe";
                Stntex[30, 1] = "nssya"; Stntex[30, 2] = "nssha"; Stntex[30, 3] = "nltusya"; Stntex[30, 4] = "nltusha"; Stntex[30, 5] = "nnssya"; Stntex[30, 6] = "nnssha"; Stntex[30, 7] = "nnltusya"; Stntex[30, 8] = "nnltusha";
                Stntex[31, 1] = "nssyu"; Stntex[31, 2] = "nsshu"; Stntex[31, 3] = "nltusyu"; Stntex[31, 4] = "nltushu"; Stntex[31, 5] = "nnssyu"; Stntex[31, 6] = "nnsshu"; Stntex[31, 7] = "nnltusyu"; Stntex[31, 8] = "nnltushu";
                Stntex[32, 1] = "nssyo"; Stntex[32, 2] = "nssho"; Stntex[32, 3] = "nltusyo"; Stntex[32, 4] = "nltusho"; Stntex[32, 5] = "nnssyo"; Stntex[32, 6] = "nnssho"; Stntex[32, 7] = "nnltusyo"; Stntex[32, 8] = "nnltusho";

                Stntex[146, 1] = "nssye"; Stntex[146, 2] = "nsshe"; Stntex[146, 3] = "nltusye"; Stntex[146, 4] = "nltushe"; Stntex[146, 5] = "nnssye"; Stntex[146, 6] = "nnsshe"; Stntex[146, 7] = "nnltusye"; Stntex[146, 8] = "nnltushe";
                Stntex[147, 1] = "nssya"; Stntex[147, 2] = "nssha"; Stntex[147, 3] = "nltusya"; Stntex[147, 4] = "nltusha"; Stntex[147, 5] = "nnssya"; Stntex[147, 6] = "nnssha"; Stntex[147, 7] = "nnltusya"; Stntex[147, 8] = "nnltusha";
                Stntex[148, 1] = "nssyu"; Stntex[148, 2] = "nsshu"; Stntex[148, 3] = "nltusyu"; Stntex[148, 4] = "nltushu"; Stntex[148, 5] = "nnssyu"; Stntex[148, 6] = "nnsshu"; Stntex[148, 7] = "nnltusyu"; Stntex[148, 8] = "nnltushu";
                Stntex[149, 1] = "nssyo"; Stntex[149, 2] = "nssho"; Stntex[149, 3] = "nltusyo"; Stntex[149, 4] = "nltusho"; Stntex[149, 5] = "nnssyo"; Stntex[149, 6] = "nnssho"; Stntex[149, 7] = "nnltusyo"; Stntex[149, 8] = "nnltusho";
            }
        }
        else
        {
            //SHA SHU SHO
            Stex[29, 1] = "she"; Stex[29, 2] = "sye";// Stex[29, 3] = "sile"; Stex[29, 4] = "shile";
            Stex[30, 1] = "sha"; Stex[30, 2] = "sya";// Stex[30, 3] = "silya"; Stex[30, 4] = "shilya";
            Stex[31, 1] = "shu"; Stex[31, 2] = "syu";// Stex[31, 3] = "silyu"; Stex[31, 4] = "shilyu";
            Stex[32, 1] = "sho"; Stex[32, 2] = "syo";// Stex[32, 3] = "silyo"; Stex[32, 4] = "shilyo";

            Stex[151, 1] = "she"; Stex[151, 2] = "sye";// Stex[151, 3] = "sile"; Stex[151, 4] = "shile";
            Stex[152, 1] = "sha"; Stex[152, 2] = "sya";// Stex[152, 3] = "silya"; Stex[152, 4] = "shilya";
            Stex[153, 1] = "shu"; Stex[153, 2] = "syu";// Stex[153, 3] = "silyu"; Stex[153, 4] = "shilyu";
            Stex[154, 1] = "sho"; Stex[154, 2] = "syo";// Stex[154, 3] = "silyo"; Stex[154, 4] = "shilyo";

            Sttex[29, 1] = "sshe"; Sttex[29, 2] = "ssye"; Sttex[29, 3] = "ltushe"; Sttex[29, 4] = "ltusye";
            Sttex[30, 1] = "ssha"; Sttex[30, 2] = "ssya"; Sttex[30, 3] = "ltusha"; Sttex[30, 4] = "ltusya";
            Sttex[31, 1] = "sshu"; Sttex[31, 2] = "ssyu"; Sttex[31, 3] = "ltushu"; Sttex[31, 4] = "ltusyu";
            Sttex[32, 1] = "ssho"; Sttex[32, 2] = "ssyo"; Sttex[32, 3] = "ltusho"; Sttex[32, 4] = "ltusyo";

            Sttex[146, 1] = "sshe"; Sttex[146, 2] = "ssye"; Sttex[146, 3] = "ltushe"; Sttex[146, 4] = "ltusye";
            Sttex[147, 1] = "ssha"; Sttex[147, 2] = "ssya"; Sttex[147, 3] = "ltusha"; Sttex[147, 4] = "ltusya";
            Sttex[148, 1] = "sshu"; Sttex[148, 2] = "ssyu"; Sttex[148, 3] = "ltushu"; Sttex[148, 4] = "ltusyu";
            Sttex[149, 1] = "ssho"; Sttex[149, 2] = "ssyo"; Sttex[149, 3] = "ltusho"; Sttex[149, 4] = "ltusyo";

            Stnex[28, 1] = "nnshe"; Stnex[28, 2] = "nnsye"; Stnex[28, 4] = "nshe"; Stnex[28, 5] = "nsye";
            Stnex[29, 1] = "nnsha"; Stnex[29, 2] = "nnsya"; Stnex[29, 4] = "nsha"; Stnex[29, 5] = "nsya";
            Stnex[30, 1] = "nnshu"; Stnex[30, 2] = "nnsyu"; Stnex[30, 4] = "nshu"; Stnex[30, 5] = "nsyu";
            Stnex[31, 1] = "nnsho"; Stnex[31, 2] = "nnsyo"; Stnex[31, 4] = "nsho"; Stnex[31, 5] = "nsyo";

            Stnex[144, 1] = "nnshe"; Stnex[144, 2] = "nnsye"; Stnex[144, 3] = "nshe"; Stnex[144, 4] = "nsye";
            Stnex[145, 1] = "nnsha"; Stnex[145, 2] = "nnsya"; Stnex[145, 3] = "nsha"; Stnex[145, 4] = "nsya";
            Stnex[146, 1] = "nnshu"; Stnex[146, 2] = "nnsyu"; Stnex[146, 3] = "nshu"; Stnex[146, 4] = "nsyu";
            Stnex[147, 1] = "nnsho"; Stnex[147, 2] = "nnsyo"; Stnex[147, 3] = "nsho"; Stnex[147, 4] = "nsyo";

            Stntex[29, 1] = "nnsshe"; Stntex[29, 2] = "nnssye"; Stntex[29, 3] = "nnltushe"; Stntex[29, 4] = "nnltusye"; Stntex[29, 5] = "nsshe"; Stntex[29, 6] = "nssye"; Stntex[29, 7] = "nltushe"; Stntex[29, 8] = "nltusye";
            Stntex[30, 1] = "nnssha"; Stntex[30, 2] = "nnssya"; Stntex[30, 3] = "nnltusha"; Stntex[30, 4] = "nnltusya"; Stntex[30, 5] = "nssha"; Stntex[30, 6] = "nssya"; Stntex[30, 7] = "nltusha"; Stntex[30, 8] = "nltusya";
            Stntex[31, 1] = "nnsshu"; Stntex[31, 2] = "nnssyu"; Stntex[31, 3] = "nnltushu"; Stntex[31, 4] = "nnltusyu"; Stntex[31, 5] = "nsshu"; Stntex[31, 6] = "nssyu"; Stntex[31, 7] = "nltushu"; Stntex[31, 8] = "nltusyu";
            Stntex[32, 1] = "nnssho"; Stntex[32, 2] = "nnssyo"; Stntex[32, 3] = "nnltusho"; Stntex[32, 4] = "nnltusyo"; Stntex[32, 5] = "nssho"; Stntex[32, 6] = "nssyo"; Stntex[32, 7] = "nltusho"; Stntex[32, 8] = "nltusyo";

            Stntex[146, 1] = "nnsshe"; Stntex[146, 2] = "nnssye"; Stntex[146, 3] = "nnltushe"; Stntex[146, 4] = "nnltusye"; Stntex[146, 5] = "nsshe"; Stntex[146, 6] = "nssye"; Stntex[146, 7] = "nltushe"; Stntex[146, 8] = "nltusye";
            Stntex[147, 1] = "nnssha"; Stntex[147, 2] = "nnssya"; Stntex[147, 3] = "nnltusha"; Stntex[147, 4] = "nnltusya"; Stntex[147, 5] = "nssha"; Stntex[147, 6] = "nssya"; Stntex[147, 7] = "nltusha"; Stntex[147, 8] = "nltusya";
            Stntex[148, 1] = "nnsshu"; Stntex[148, 2] = "nnssyu"; Stntex[148, 3] = "nnltushu"; Stntex[148, 4] = "nnltusyu"; Stntex[148, 5] = "nsshu"; Stntex[148, 6] = "nssyu"; Stntex[148, 7] = "nltushu"; Stntex[148, 8] = "nltusyu";
            Stntex[149, 1] = "nnssho"; Stntex[149, 2] = "nnssyo"; Stntex[149, 3] = "nnltusho"; Stntex[149, 4] = "nnltusyo"; Stntex[149, 5] = "nssho"; Stntex[149, 6] = "nssyo"; Stntex[149, 7] = "nltusho"; Stntex[149, 8] = "nltusyo";
            if (n == 1)
            {
                Stnex[28, 1] = "nshe"; Stnex[28, 2] = "nsye"; Stnex[28, 3] = "nnshe"; Stnex[28, 4] = "nnsye";
                Stnex[29, 1] = "nsha"; Stnex[29, 2] = "nsya"; Stnex[29, 3] = "nnsha"; Stnex[29, 4] = "nnsya";
                Stnex[30, 1] = "nshu"; Stnex[30, 2] = "nsyu"; Stnex[30, 3] = "nnshu"; Stnex[30, 4] = "nnsyu";
                Stnex[31, 1] = "nsho"; Stnex[31, 2] = "nsyo"; Stnex[31, 3] = "nnsho"; Stnex[31, 4] = "nnsyo";

                Stnex[144, 1] = "nshe"; Stnex[144, 2] = "nsye"; Stnex[144, 3] = "nnshe"; Stnex[144, 4] = "nnsye";
                Stnex[145, 1] = "nsha"; Stnex[145, 2] = "nsya"; Stnex[145, 3] = "nnsha"; Stnex[145, 4] = "nnsya";
                Stnex[146, 1] = "nshu"; Stnex[146, 2] = "nsyu"; Stnex[146, 3] = "nnshu"; Stnex[146, 4] = "nnsyu";
                Stnex[147, 1] = "nsho"; Stnex[147, 2] = "nsyo"; Stnex[147, 3] = "nnsho"; Stnex[147, 4] = "nnsyo";

                Stntex[29, 1] = "nsshe"; Stntex[29, 2] = "nssye"; Stntex[29, 3] = "nltushe"; Stntex[29, 4] = "nltusye"; Stntex[29, 5] = "nnsshe"; Stntex[29, 6] = "nnssye"; Stntex[29, 7] = "nnltushe"; Stntex[29, 8] = "nnltusye";
                Stntex[30, 1] = "nssha"; Stntex[30, 2] = "nssya"; Stntex[30, 3] = "nltusha"; Stntex[30, 4] = "nltusya"; Stntex[30, 5] = "nnssha"; Stntex[30, 6] = "nnssya"; Stntex[30, 7] = "nnltusha"; Stntex[30, 8] = "nnltusya";
                Stntex[31, 1] = "nsshu"; Stntex[31, 2] = "nssyu"; Stntex[31, 3] = "nltushu"; Stntex[31, 4] = "nltusyu"; Stntex[31, 5] = "nnsshu"; Stntex[31, 6] = "nnssyu"; Stntex[31, 7] = "nnltushu"; Stntex[31, 8] = "nnltusyu";
                Stntex[32, 1] = "nssho"; Stntex[32, 2] = "nssyo"; Stntex[32, 3] = "nltusho"; Stntex[32, 4] = "nltusyo"; Stntex[32, 5] = "nnssho"; Stntex[32, 6] = "nnssyo"; Stntex[32, 7] = "nnltusho"; Stntex[32, 8] = "nnltusyo";

                Stntex[146, 1] = "nsshe"; Stntex[146, 2] = "nssye"; Stntex[146, 3] = "nltushe"; Stntex[146, 4] = "nltusye"; Stntex[146, 5] = "nnsshe"; Stntex[146, 6] = "nnssye"; Stntex[146, 7] = "nnltushe"; Stntex[146, 8] = "nnltusye";
                Stntex[147, 1] = "nssha"; Stntex[147, 2] = "nssya"; Stntex[147, 3] = "nltusha"; Stntex[147, 4] = "nltusya"; Stntex[147, 5] = "nnssha"; Stntex[147, 6] = "nnssya"; Stntex[147, 7] = "nnltusha"; Stntex[147, 8] = "nnltusya";
                Stntex[148, 1] = "nsshu"; Stntex[148, 2] = "nssyu"; Stntex[148, 3] = "nltushu"; Stntex[148, 4] = "nltusyu"; Stntex[148, 5] = "nnsshu"; Stntex[148, 6] = "nnssyu"; Stntex[148, 7] = "nnltushu"; Stntex[148, 8] = "nnltusyu";
                Stntex[149, 1] = "nssho"; Stntex[149, 2] = "nssyo"; Stntex[149, 3] = "nltusho"; Stntex[149, 4] = "nltusyo"; Stntex[149, 5] = "nnssho"; Stntex[149, 6] = "nnssyo"; Stntex[149, 7] = "nnltusho"; Stntex[149, 8] = "nnltusyo";
            }
        }
    }
    public void SetTYA(int tya, int nn)
    {
        if (tya == 1)
        {
            //TYA TYU TYO
            Stex[43, 1] = "tyi"; Stex[43, 2] = "cyi"; Stex[43, 3] = "tili"; Stex[43, 4] = "chili";
            Stex[44, 1] = "tye"; Stex[44, 2] = "cye"; Stex[44, 3] = "che"; Stex[44, 4] = "tile"; Stex[44, 5] = "chile";
            Stex[45, 1] = "tya"; Stex[45, 2] = "cya"; Stex[45, 3] = "cha"; Stex[45, 4] = "tilya"; Stex[45, 5] = "chilya";
            Stex[46, 1] = "tyu"; Stex[46, 2] = "cyu"; Stex[46, 3] = "chu"; Stex[46, 4] = "tilyu"; Stex[46, 5] = "chilyu";
            Stex[47, 1] = "tyo"; Stex[47, 2] = "cyo"; Stex[47, 3] = "cho"; Stex[47, 4] = "tilyo"; Stex[47, 5] = "chilyo";

            Stex[165, 1] = "tyi"; Stex[165, 2] = "cyi"; Stex[165, 3] = "tili"; Stex[165, 4] = "chili";
            Stex[166, 1] = "tye"; Stex[166, 2] = "cye"; Stex[166, 3] = "che"; Stex[166, 4] = "tile"; Stex[166, 5] = "chile";
            Stex[167, 1] = "tya"; Stex[167, 2] = "cya"; Stex[167, 3] = "cha"; Stex[167, 4] = "tilya"; Stex[167, 5] = "chilya";
            Stex[168, 1] = "tyu"; Stex[168, 2] = "cyu"; Stex[168, 3] = "chu"; Stex[168, 4] = "tilyu"; Stex[168, 5] = "chilyu";
            Stex[169, 1] = "tyo"; Stex[169, 2] = "cyo"; Stex[169, 3] = "cho"; Stex[169, 4] = "tilyo"; Stex[169, 5] = "chilyo";

            Sttex[43, 1] = "ttyi"; Sttex[43, 2] = "ccyi"; Sttex[43, 3] = "ltutyi"; Sttex[43, 4] = "ltucyi";
            Sttex[44, 1] = "ttye"; Sttex[44, 2] = "ccye"; Sttex[44, 3] = "cche"; Sttex[44, 4] = "ltutye"; Sttex[44, 5] = "ltucye"; Sttex[44, 6] = "ltuche";
            Sttex[45, 1] = "ttya"; Sttex[45, 2] = "ccya"; Sttex[45, 3] = "ccha"; Sttex[45, 4] = "ltutya"; Sttex[45, 5] = "ltucya"; Sttex[45, 6] = "ltucha";
            Sttex[46, 1] = "ttyu"; Sttex[46, 2] = "ccyu"; Sttex[46, 3] = "cchu"; Sttex[46, 4] = "ltutyu"; Sttex[46, 5] = "ltucyu"; Sttex[46, 6] = "ltuchu";
            Sttex[47, 1] = "ttyo"; Sttex[47, 2] = "ccyo"; Sttex[47, 3] = "ccho"; Sttex[47, 4] = "ltutyo"; Sttex[47, 5] = "ltucyo"; Sttex[47, 6] = "ltucho";

            Sttex[160, 1] = "ttyi"; Sttex[160, 2] = "ccyi"; Sttex[160, 3] = "ltutyi"; Sttex[160, 4] = "ltucyi";
            Sttex[161, 1] = "ttye"; Sttex[161, 2] = "ccye"; Sttex[161, 3] = "cche"; Sttex[161, 4] = "ltutye"; Sttex[161, 5] = "ltucye"; Sttex[161, 6] = "ltuche";
            Sttex[162, 1] = "ttya"; Sttex[162, 2] = "ccya"; Sttex[162, 3] = "ccha"; Sttex[162, 4] = "ltutya"; Sttex[162, 5] = "ltucya"; Sttex[162, 6] = "ltucha";
            Sttex[163, 1] = "ttyu"; Sttex[163, 2] = "ccyu"; Sttex[163, 3] = "cchu"; Sttex[163, 4] = "ltutyu"; Sttex[163, 5] = "ltucyu"; Sttex[163, 6] = "ltuchu";
            Sttex[164, 1] = "ttyo"; Sttex[164, 2] = "ccyo"; Sttex[164, 3] = "ccho"; Sttex[164, 4] = "ltutyo"; Sttex[164, 5] = "ltucyo"; Sttex[164, 6] = "ltucho";

            Stnex[42, 1] = "nntyi"; Stnex[42, 2] = "nncyi"; Stnex[42, 4] = "ntyi"; Stnex[42, 5] = "ncyi";
            Stnex[43, 1] = "nntye"; Stnex[43, 2] = "nncye"; Stnex[43, 3] = "nnche"; Stnex[43, 4] = "ntye"; Stnex[43, 5] = "ncye"; Stnex[43, 6] = "nche";
            Stnex[44, 1] = "nntya"; Stnex[44, 2] = "nncya"; Stnex[44, 3] = "nncha"; Stnex[44, 4] = "ntya"; Stnex[44, 5] = "ncya"; Stnex[44, 6] = "ncha";
            Stnex[45, 1] = "nntyu"; Stnex[45, 2] = "nncyu"; Stnex[45, 3] = "nnchu"; Stnex[45, 4] = "ntyu"; Stnex[45, 5] = "ncyu"; Stnex[45, 6] = "nchu";
            Stnex[46, 1] = "nntyo"; Stnex[46, 2] = "nncyo"; Stnex[46, 3] = "nncho"; Stnex[46, 4] = "ntyo"; Stnex[46, 5] = "ncyo"; Stnex[46, 6] = "ncho";

            Stnex[158, 1] = "nntyi"; Stnex[158, 2] = "nncyi"; Stnex[158, 4] = "ntyi"; Stnex[158, 5] = "ncyi";
            Stnex[159, 1] = "nntye"; Stnex[159, 2] = "nncye"; Stnex[159, 3] = "nnche"; Stnex[159, 4] = "ntye"; Stnex[159, 5] = "ncye"; Stnex[159, 6] = "nche";
            Stnex[160, 1] = "nntya"; Stnex[160, 2] = "nncya"; Stnex[160, 3] = "nncha"; Stnex[160, 4] = "ntya"; Stnex[160, 5] = "ncya"; Stnex[160, 6] = "ncha";
            Stnex[161, 1] = "nntyu"; Stnex[161, 2] = "nncyu"; Stnex[161, 3] = "nnchu"; Stnex[161, 4] = "ntyu"; Stnex[161, 5] = "ncyu"; Stnex[161, 6] = "nchu";
            Stnex[162, 1] = "nntyo"; Stnex[162, 2] = "nncyo"; Stnex[162, 3] = "nncho"; Stnex[162, 4] = "ntyo"; Stnex[162, 5] = "ncyo"; Stnex[162, 6] = "ncho";

            Stntex[43, 1] = "nnttyi"; Stntex[43, 2] = "nnccyi"; Stntex[43, 3] = "nnltutyi"; Stntex[43, 4] = "nnltucyi"; Stntex[43, 5] = "nttyi"; Stntex[43, 6] = "nccyi"; Stntex[43, 7] = "nltutyi"; Stntex[43, 8] = "nltucyi";
            Stntex[44, 1] = "nnttye"; Stntex[44, 2] = "nnccye"; Stntex[44, 3] = "nncche"; Stntex[44, 4] = "nnltutye"; Stntex[44, 5] = "nnltucye"; Stntex[44, 6] = "nnltuche"; Stntex[44, 7] = "nttye"; Stntex[44, 8] = "nccye"; Stntex[44, 9] = "ncche"; Stntex[44, 10] = "nltutye"; Stntex[44, 11] = "nltucye"; Stntex[44, 12] = "nltuche";
            Stntex[45, 1] = "nnttya"; Stntex[45, 2] = "nnccya"; Stntex[45, 3] = "nnccha"; Stntex[45, 4] = "nnltutya"; Stntex[45, 5] = "nnltucya"; Stntex[45, 6] = "nnltucha"; Stntex[45, 7] = "nttya"; Stntex[45, 8] = "nccya"; Stntex[45, 9] = "nccha"; Stntex[45, 10] = "nltutya"; Stntex[45, 11] = "nltucya"; Stntex[45, 12] = "nltucha";
            Stntex[46, 1] = "nnttyu"; Stntex[46, 2] = "nnccyu"; Stntex[46, 3] = "nncchu"; Stntex[46, 4] = "nnltutyu"; Stntex[46, 5] = "nnltucyu"; Stntex[46, 6] = "nnltuchu"; Stntex[46, 7] = "nttyu"; Stntex[46, 8] = "nccyu"; Stntex[46, 9] = "ncchu"; Stntex[46, 10] = "nltutyu"; Stntex[46, 11] = "nltucyu"; Stntex[46, 12] = "nltuchu";
            Stntex[47, 1] = "nnttyo"; Stntex[47, 2] = "nnccyo"; Stntex[47, 3] = "nnccho"; Stntex[47, 4] = "nnltutyo"; Stntex[47, 5] = "nnltucyo"; Stntex[47, 6] = "nnltucho"; Stntex[47, 7] = "nttyo"; Stntex[47, 8] = "nccyo"; Stntex[47, 9] = "nccho"; Stntex[47, 10] = "nltutyo"; Stntex[47, 11] = "nltucyo"; Stntex[47, 12] = "nltucho";

            Stntex[160, 1] = "nnttyi"; Stntex[160, 2] = "nnccyi"; Stntex[160, 3] = "nnltutyi"; Stntex[160, 4] = "nnltucyi"; Stntex[160, 5] = "nttyi"; Stntex[160, 6] = "nccyi"; Stntex[160, 7] = "nltutyi"; Stntex[160, 8] = "nltucyi";
            Stntex[161, 1] = "nnttye"; Stntex[161, 2] = "nnccye"; Stntex[161, 3] = "nncche"; Stntex[161, 4] = "nnltutye"; Stntex[161, 5] = "nnltucye"; Stntex[161, 6] = "nnltuche"; Stntex[161, 7] = "nttye"; Stntex[161, 8] = "nccye"; Stntex[161, 9] = "ncche"; Stntex[161, 10] = "nltutye"; Stntex[161, 11] = "nltucye"; Stntex[161, 12] = "nltuche";
            Stntex[162, 1] = "nnttya"; Stntex[162, 2] = "nnccya"; Stntex[162, 3] = "nnccha"; Stntex[162, 4] = "nnltutya"; Stntex[162, 5] = "nnltucya"; Stntex[162, 6] = "nnltucha"; Stntex[162, 7] = "nttya"; Stntex[162, 8] = "nccya"; Stntex[162, 9] = "nccha"; Stntex[162, 10] = "nltutya"; Stntex[162, 11] = "nltucya"; Stntex[162, 12] = "nltucha";
            Stntex[163, 1] = "nnttyu"; Stntex[163, 2] = "nnccyu"; Stntex[163, 3] = "nncchu"; Stntex[163, 4] = "nnltutyu"; Stntex[163, 5] = "nnltucyu"; Stntex[163, 6] = "nnltuchu"; Stntex[163, 7] = "nttyu"; Stntex[163, 8] = "nccyu"; Stntex[163, 9] = "ncchu"; Stntex[163, 10] = "nltutyu"; Stntex[163, 11] = "nltucyu"; Stntex[163, 12] = "nltuchu";
            Stntex[164, 1] = "nnttyo"; Stntex[164, 2] = "nnccyo"; Stntex[164, 3] = "nnccho"; Stntex[164, 4] = "nnltutyo"; Stntex[164, 5] = "nnltucyo"; Stntex[164, 6] = "nnltucho"; Stntex[164, 7] = "nttyo"; Stntex[164, 8] = "nccyo"; Stntex[164, 9] = "nccho"; Stntex[164, 10] = "nltutyo"; Stntex[164, 11] = "nltucyo"; Stntex[164, 12] = "nltucho";

            if (nn == 1)
            {
                Stnex[42, 1] = "ntyi"; Stnex[42, 2] = "ncyi"; Stnex[42, 4] = "nntyi"; Stnex[42, 5] = "nncyi";
                Stnex[43, 1] = "ntye"; Stnex[43, 2] = "ncye"; Stnex[43, 3] = "nche"; Stnex[43, 4] = "nntye"; Stnex[43, 5] = "nncye"; Stnex[43, 6] = "nnche";
                Stnex[44, 1] = "ntya"; Stnex[44, 2] = "ncya"; Stnex[44, 3] = "ncha"; Stnex[44, 4] = "nntya"; Stnex[44, 5] = "nncya"; Stnex[44, 6] = "nncha";
                Stnex[45, 1] = "ntyu"; Stnex[45, 2] = "ncyu"; Stnex[45, 3] = "nchu"; Stnex[45, 4] = "nntyu"; Stnex[45, 5] = "nncyu"; Stnex[45, 6] = "nnchu";
                Stnex[46, 1] = "ntyo"; Stnex[46, 2] = "ncyo"; Stnex[46, 3] = "ncho"; Stnex[46, 4] = "nntyo"; Stnex[46, 5] = "nncyo"; Stnex[46, 6] = "nncho";

                Stnex[158, 1] = "ntyi"; Stnex[158, 2] = "ncyi"; Stnex[158, 4] = "nntyi"; Stnex[158, 5] = "nncyi";
                Stnex[159, 1] = "ntye"; Stnex[159, 2] = "ncye"; Stnex[159, 3] = "nche"; Stnex[159, 4] = "nntye"; Stnex[159, 5] = "nncye"; Stnex[159, 6] = "nnche";
                Stnex[160, 1] = "ntya"; Stnex[160, 2] = "ncya"; Stnex[160, 3] = "ncha"; Stnex[160, 4] = "nntya"; Stnex[160, 5] = "nncya"; Stnex[160, 6] = "nncha";
                Stnex[161, 1] = "ntyu"; Stnex[161, 2] = "ncyu"; Stnex[161, 3] = "nchu"; Stnex[161, 4] = "nntyu"; Stnex[161, 5] = "nncyu"; Stnex[161, 6] = "nnchu";
                Stnex[162, 1] = "ntyo"; Stnex[162, 2] = "ncyo"; Stnex[162, 3] = "ncho"; Stnex[162, 4] = "nntyo"; Stnex[162, 5] = "nncyo"; Stnex[162, 6] = "nncho";

                Stntex[43, 1] = "nttyi"; Stntex[43, 2] = "nccyi"; Stntex[43, 3] = "nltutyi"; Stntex[43, 4] = "nltucyi"; Stntex[43, 5] = "nnttyi"; Stntex[43, 6] = "nnccyi"; Stntex[43, 7] = "nnltutyi"; Stntex[43, 8] = "nnltucyi";
                Stntex[44, 1] = "nttye"; Stntex[44, 2] = "nccye"; Stntex[44, 3] = "ncche"; Stntex[44, 4] = "nltutye"; Stntex[44, 5] = "nltucye"; Stntex[44, 6] = "nltuche"; Stntex[44, 7] = "nnttye"; Stntex[44, 8] = "nnccye"; Stntex[44, 9] = "nncche"; Stntex[44, 10] = "nnltutye"; Stntex[44, 11] = "nnltucye"; Stntex[44, 12] = "nnltuche";
                Stntex[45, 1] = "nttya"; Stntex[45, 2] = "nccya"; Stntex[45, 3] = "nccha"; Stntex[45, 4] = "nltutya"; Stntex[45, 5] = "nltucya"; Stntex[45, 6] = "nltucha"; Stntex[45, 7] = "nnttya"; Stntex[45, 8] = "nnccya"; Stntex[45, 9] = "nnccha"; Stntex[45, 10] = "nnltutya"; Stntex[45, 11] = "nnltucya"; Stntex[45, 12] = "nnltucha";
                Stntex[46, 1] = "nttyu"; Stntex[46, 2] = "nccyu"; Stntex[46, 3] = "ncchu"; Stntex[46, 4] = "nltutyu"; Stntex[46, 5] = "nltucyu"; Stntex[46, 6] = "nltuchu"; Stntex[46, 7] = "nnttyu"; Stntex[46, 8] = "nnccyu"; Stntex[46, 9] = "nncchu"; Stntex[46, 10] = "nnltutyu"; Stntex[46, 11] = "nnltucyu"; Stntex[46, 12] = "nnltuchu";
                Stntex[47, 1] = "nttyo"; Stntex[47, 2] = "nccyo"; Stntex[47, 3] = "nccho"; Stntex[47, 4] = "nltutyo"; Stntex[47, 5] = "nltucyo"; Stntex[47, 6] = "nltucho"; Stntex[47, 7] = "nnttyo"; Stntex[47, 8] = "nnccyo"; Stntex[47, 9] = "nnccho"; Stntex[47, 10] = "nnltutyo"; Stntex[47, 11] = "nnltucyo"; Stntex[47, 12] = "nnltucho";

                Stntex[160, 1] = "nttyi"; Stntex[160, 2] = "nccyi"; Stntex[160, 3] = "nltutyi"; Stntex[160, 4] = "nltucyi"; Stntex[160, 5] = "nnttyi"; Stntex[160, 6] = "nnccyi"; Stntex[160, 7] = "nnltutyi"; Stntex[160, 8] = "nnltucyi";
                Stntex[161, 1] = "nttye"; Stntex[161, 2] = "nccye"; Stntex[161, 3] = "ncche"; Stntex[161, 4] = "nltutye"; Stntex[161, 5] = "nltucye"; Stntex[161, 6] = "nltuche"; Stntex[161, 7] = "nnttye"; Stntex[161, 8] = "nnccye"; Stntex[161, 9] = "nncche"; Stntex[161, 10] = "nnltutye"; Stntex[161, 11] = "nnltucye"; Stntex[161, 12] = "nnltuche";
                Stntex[162, 1] = "nttya"; Stntex[162, 2] = "nccya"; Stntex[162, 3] = "nccha"; Stntex[162, 4] = "nltutya"; Stntex[162, 5] = "nltucya"; Stntex[162, 6] = "nltucha"; Stntex[162, 7] = "nnttya"; Stntex[162, 8] = "nnccya"; Stntex[162, 9] = "nnccha"; Stntex[162, 10] = "nnltutya"; Stntex[162, 11] = "nnltucya"; Stntex[162, 12] = "nnltucha";
                Stntex[163, 1] = "nttyu"; Stntex[163, 2] = "nccyu"; Stntex[163, 3] = "ncchu"; Stntex[163, 4] = "nltutyu"; Stntex[163, 5] = "nltucyu"; Stntex[163, 6] = "nltuchu"; Stntex[163, 7] = "nnttyu"; Stntex[163, 8] = "nnccyu"; Stntex[163, 9] = "nncchu"; Stntex[163, 10] = "nnltutyu"; Stntex[163, 11] = "nnltucyu"; Stntex[163, 12] = "nnltuchu";
                Stntex[164, 1] = "nttyo"; Stntex[164, 2] = "nccyo"; Stntex[164, 3] = "nccho"; Stntex[164, 4] = "nltutyo"; Stntex[164, 5] = "nltucyo"; Stntex[164, 6] = "nltucho"; Stntex[164, 7] = "nnttyo"; Stntex[164, 8] = "nnccyo"; Stntex[164, 9] = "nnccho"; Stntex[164, 10] = "nnltutyo"; Stntex[164, 11] = "nnltucyo"; Stntex[164, 12] = "nnltucho";
            }
        }
        else if (tya == 2)
        {
            //CHA CHU CHO
            Stex[43, 1] = "cyi"; Stex[43, 2] = "tyi"; Stex[43, 3] = "chili"; Stex[43, 4] = "tili";
            Stex[44, 1] = "che"; Stex[44, 2] = "tye"; Stex[44, 3] = "cye"; Stex[44, 4] = "chile"; Stex[44, 5] = "tile";
            Stex[45, 1] = "cha"; Stex[45, 2] = "tya"; Stex[45, 3] = "cya"; Stex[45, 4] = "chilya"; Stex[45, 5] = "tilya";
            Stex[46, 1] = "chu"; Stex[46, 2] = "tyu"; Stex[46, 3] = "cyu"; Stex[46, 4] = "chilyu"; Stex[46, 5] = "tilyu";
            Stex[47, 1] = "cho"; Stex[47, 2] = "tyo"; Stex[47, 3] = "cyo"; Stex[47, 4] = "chilyo"; Stex[47, 5] = "tilyo";

            Stex[165, 1] = "cyi"; Stex[165, 2] = "tyi"; Stex[165, 3] = "chili"; Stex[165, 4] = "tili";
            Stex[166, 1] = "che"; Stex[166, 2] = "tye"; Stex[166, 3] = "cye"; Stex[166, 4] = "chile"; Stex[166, 5] = "tile";
            Stex[167, 1] = "cha"; Stex[167, 2] = "tya"; Stex[167, 3] = "cya"; Stex[167, 4] = "chilya"; Stex[167, 5] = "tilya";
            Stex[168, 1] = "chu"; Stex[168, 2] = "tyu"; Stex[168, 3] = "cyu"; Stex[168, 4] = "chilyu"; Stex[168, 5] = "tilyu";
            Stex[169, 1] = "cho"; Stex[169, 2] = "tyo"; Stex[169, 3] = "cyo"; Stex[169, 4] = "chilyo"; Stex[169, 5] = "tilyo";

            Sttex[43, 1] = "ccyi"; Sttex[43, 2] = "ttyi"; Sttex[43, 3] = "ltucyi"; Sttex[43, 4] = "ltutyi";
            Sttex[44, 1] = "cche"; Sttex[44, 2] = "ttye"; Sttex[44, 3] = "ccye"; Sttex[44, 4] = "ltuche"; Sttex[44, 5] = "ltutye"; Sttex[44, 6] = "ltucye";
            Sttex[45, 1] = "ccha"; Sttex[45, 2] = "ttya"; Sttex[45, 3] = "ccya"; Sttex[45, 4] = "ltucha"; Sttex[45, 5] = "ltutya"; Sttex[45, 6] = "ltucya";
            Sttex[46, 1] = "cchu"; Sttex[46, 2] = "ttyu"; Sttex[46, 3] = "ccyu"; Sttex[46, 4] = "ltuchu"; Sttex[46, 5] = "ltutyu"; Sttex[46, 6] = "ltucyu";
            Sttex[47, 1] = "ccho"; Sttex[47, 2] = "ttyo"; Sttex[47, 3] = "ccyo"; Sttex[47, 4] = "ltucho"; Sttex[47, 5] = "ltutyo"; Sttex[47, 6] = "ltucyo";

            Sttex[160, 1] = "ccyi"; Sttex[160, 2] = "ttyi"; Sttex[160, 3] = "ltucyi"; Sttex[160, 4] = "ltutyi";
            Sttex[161, 1] = "cche"; Sttex[161, 2] = "ttye"; Sttex[161, 3] = "ccye"; Sttex[161, 4] = "ltuche"; Sttex[161, 5] = "ltutye"; Sttex[161, 6] = "ltucye";
            Sttex[162, 1] = "ccha"; Sttex[162, 2] = "ttya"; Sttex[162, 3] = "ccya"; Sttex[162, 4] = "ltucha"; Sttex[162, 5] = "ltutya"; Sttex[162, 6] = "ltucya";
            Sttex[163, 1] = "cchu"; Sttex[163, 2] = "ttyu"; Sttex[163, 3] = "ccyu"; Sttex[163, 4] = "ltuchu"; Sttex[163, 5] = "ltutyu"; Sttex[163, 6] = "ltucyu";
            Sttex[164, 1] = "ccho"; Sttex[164, 2] = "ttyo"; Sttex[164, 3] = "ccyo"; Sttex[164, 4] = "ltucho"; Sttex[164, 5] = "ltutyo"; Sttex[164, 6] = "ltucyo";

            Stnex[42, 1] = "nncyi"; Stnex[42, 2] = "nntyi"; Stnex[42, 3] = "ncyi"; Stnex[42, 4] = "ntyi";
            Stnex[43, 1] = "nnche"; Stnex[43, 2] = "nntye"; Stnex[43, 3] = "nncye"; Stnex[43, 4] = "nche"; Stnex[43, 5] = "ntye"; Stnex[43, 6] = "ncye";
            Stnex[44, 1] = "nncha"; Stnex[44, 2] = "nntya"; Stnex[44, 3] = "nncya"; Stnex[44, 4] = "ncha"; Stnex[44, 5] = "ntya"; Stnex[44, 6] = "ncya";
            Stnex[45, 1] = "nnchu"; Stnex[45, 2] = "nntyu"; Stnex[45, 3] = "nncyu"; Stnex[45, 4] = "nchu"; Stnex[45, 5] = "ntyu"; Stnex[45, 6] = "ncyu";
            Stnex[46, 1] = "nncho"; Stnex[46, 2] = "nntyo"; Stnex[46, 3] = "nncyo"; Stnex[46, 4] = "ncho"; Stnex[46, 5] = "ntyo"; Stnex[46, 6] = "ncyo";

            Stnex[158, 1] = "nncyi"; Stnex[158, 2] = "nntyi"; Stnex[158, 3] = "ncyi"; Stnex[158, 4] = "ntyi";
            Stnex[159, 1] = "nnche"; Stnex[159, 2] = "nntye"; Stnex[159, 3] = "nncye"; Stnex[159, 4] = "nche"; Stnex[159, 5] = "ntye"; Stnex[159, 6] = "ncye";
            Stnex[160, 1] = "nncha"; Stnex[160, 2] = "nntya"; Stnex[160, 3] = "nncya"; Stnex[160, 4] = "ncha"; Stnex[160, 5] = "ntya"; Stnex[160, 6] = "ncya";
            Stnex[161, 1] = "nnchu"; Stnex[161, 2] = "nntyu"; Stnex[161, 3] = "nncyu"; Stnex[161, 4] = "nchu"; Stnex[161, 5] = "ntyu"; Stnex[161, 6] = "ncyu";
            Stnex[162, 1] = "nncho"; Stnex[162, 2] = "nntyo"; Stnex[162, 3] = "nncyo"; Stnex[162, 4] = "ncho"; Stnex[162, 5] = "ntyo"; Stnex[162, 6] = "ncyo";

            Stntex[43, 1] = "nnccyi"; Stntex[43, 2] = "nnttyi"; Stntex[43, 3] = "nnltucyi"; Stntex[43, 4] = "nnltutyi"; Stntex[43, 5] = "nccyi"; Stntex[43, 6] = "nttyi"; Stntex[43, 7] = "nltucyi"; Stntex[43, 8] = "nltutyi";
            Stntex[44, 1] = "nncche"; Stntex[44, 2] = "nnttye"; Stntex[44, 3] = "nnccye"; Stntex[44, 4] = "nnltuche"; Stntex[44, 5] = "nnltutye"; Stntex[44, 6] = "nnltucye"; Stntex[44, 7] = "ncche"; Stntex[44, 8] = "nttye"; Stntex[44, 9] = "nccye"; Stntex[44, 10] = "nltuche"; Stntex[44, 11] = "nltutye"; Stntex[44, 12] = "nltucye";
            Stntex[45, 1] = "nnccha"; Stntex[45, 2] = "nnttya"; Stntex[45, 3] = "nnccya"; Stntex[45, 4] = "nnltucha"; Stntex[45, 5] = "nnltutya"; Stntex[45, 6] = "nnltucya"; Stntex[45, 7] = "nccha"; Stntex[45, 8] = "nttya"; Stntex[45, 9] = "nccya"; Stntex[45, 10] = "nltucha"; Stntex[45, 11] = "nltutya"; Stntex[45, 12] = "nltucya";
            Stntex[46, 1] = "nncchu"; Stntex[46, 2] = "nnttyu"; Stntex[46, 3] = "nnccyu"; Stntex[46, 4] = "nnltuchu"; Stntex[46, 5] = "nnltutyu"; Stntex[46, 6] = "nnltucyu"; Stntex[46, 7] = "ncchu"; Stntex[46, 8] = "nttyu"; Stntex[46, 9] = "nccyu"; Stntex[46, 10] = "nltuchu"; Stntex[46, 11] = "nltutyu"; Stntex[46, 12] = "nltucyu";
            Stntex[47, 1] = "nnccho"; Stntex[47, 2] = "nnttyo"; Stntex[47, 3] = "nnccyo"; Stntex[47, 4] = "nnltucho"; Stntex[47, 5] = "nnltutyo"; Stntex[47, 6] = "nnltucyo"; Stntex[47, 7] = "nccho"; Stntex[47, 8] = "nttyo"; Stntex[47, 9] = "nccyo"; Stntex[47, 10] = "nltucho"; Stntex[47, 11] = "nltutyo"; Stntex[47, 12] = "nltucyo";

            Stntex[160, 1] = "nnccyi"; Stntex[160, 2] = "nnttyi"; Stntex[160, 3] = "nnltucyi"; Stntex[160, 4] = "nnltutyi"; Stntex[160, 5] = "nccyi"; Stntex[160, 6] = "nttyi"; Stntex[160, 7] = "nltucyi"; Stntex[160, 8] = "nltutyi";
            Stntex[161, 1] = "nncche"; Stntex[161, 2] = "nnttye"; Stntex[161, 3] = "nnccye"; Stntex[161, 4] = "nnltuche"; Stntex[161, 5] = "nnltutye"; Stntex[161, 6] = "nnltucye"; Stntex[161, 7] = "ncche"; Stntex[161, 8] = "nttye"; Stntex[161, 9] = "nccye"; Stntex[161, 10] = "nltuche"; Stntex[161, 11] = "nltutye"; Stntex[161, 12] = "nltucye";
            Stntex[162, 1] = "nnccha"; Stntex[162, 2] = "nnttya"; Stntex[162, 3] = "nnccya"; Stntex[162, 4] = "nnltucha"; Stntex[162, 5] = "nnltutya"; Stntex[162, 6] = "nnltucya"; Stntex[162, 7] = "nccha"; Stntex[162, 8] = "nttya"; Stntex[162, 9] = "nccya"; Stntex[162, 10] = "nltucha"; Stntex[162, 11] = "nltutya"; Stntex[162, 12] = "nltucya";
            Stntex[163, 1] = "nncchu"; Stntex[163, 2] = "nnttyu"; Stntex[163, 3] = "nnccyu"; Stntex[163, 4] = "nnltuchu"; Stntex[163, 5] = "nnltutyu"; Stntex[163, 6] = "nnltucyu"; Stntex[163, 7] = "ncchu"; Stntex[163, 8] = "nttyu"; Stntex[163, 9] = "nccyu"; Stntex[163, 10] = "nltuchu"; Stntex[163, 11] = "nltutyu"; Stntex[163, 12] = "nltucyu";
            Stntex[164, 1] = "nnccho"; Stntex[164, 2] = "nnttyo"; Stntex[164, 3] = "nnccyo"; Stntex[164, 4] = "nnltucho"; Stntex[164, 5] = "nnltutyo"; Stntex[164, 6] = "nnltucyo"; Stntex[164, 7] = "nccho"; Stntex[164, 8] = "nttyo"; Stntex[164, 9] = "nccyo"; Stntex[164, 10] = "nltucho"; Stntex[164, 11] = "nltutyo"; Stntex[164, 12] = "nltucyo";

            if (nn == 1)
            {
                Stnex[42, 1] = "ncyi"; Stnex[42, 2] = "ntyi"; Stnex[42, 3] = "nntyi"; Stnex[42, 4] = "nncyi";
                Stnex[43, 1] = "nche"; Stnex[43, 2] = "ntye"; Stnex[43, 3] = "ncye"; Stnex[43, 4] = "nnche"; Stnex[43, 5] = "nntye"; Stnex[43, 6] = "nncye";
                Stnex[44, 1] = "ncha"; Stnex[44, 2] = "ntya"; Stnex[44, 3] = "ncya"; Stnex[44, 4] = "nncha"; Stnex[44, 5] = "nntya"; Stnex[44, 6] = "nncya";
                Stnex[45, 1] = "nchu"; Stnex[45, 2] = "ntyu"; Stnex[45, 3] = "ncyu"; Stnex[45, 4] = "nnchu"; Stnex[45, 5] = "nntyu"; Stnex[45, 6] = "nncyu";
                Stnex[46, 1] = "ncho"; Stnex[46, 2] = "ntyo"; Stnex[46, 3] = "ncyo"; Stnex[46, 4] = "nncho"; Stnex[46, 5] = "nntyo"; Stnex[46, 6] = "nncyo";

                Stnex[158, 1] = "ncyi"; Stnex[158, 2] = "ntyi"; Stnex[158, 3] = "nncyi"; Stnex[158, 4] = "nntyi";
                Stnex[159, 1] = "nche"; Stnex[159, 2] = "ntye"; Stnex[159, 3] = "ncye"; Stnex[159, 4] = "nnche"; Stnex[159, 5] = "nntye"; Stnex[159, 6] = "nncye";
                Stnex[160, 1] = "ncha"; Stnex[160, 2] = "ntya"; Stnex[160, 3] = "ncya"; Stnex[160, 4] = "nncha"; Stnex[160, 5] = "nntya"; Stnex[160, 6] = "nncya";
                Stnex[161, 1] = "nchu"; Stnex[161, 2] = "ntyu"; Stnex[161, 3] = "ncyu"; Stnex[161, 4] = "nnchu"; Stnex[161, 5] = "nntyu"; Stnex[161, 6] = "nncyu";
                Stnex[162, 1] = "ncho"; Stnex[162, 2] = "ntyo"; Stnex[162, 3] = "ncyo"; Stnex[162, 4] = "nncho"; Stnex[162, 5] = "nntyo"; Stnex[162, 6] = "nncyo";

                Stntex[43, 1] = "nccyi"; Stntex[43, 2] = "nttyi"; Stntex[43, 3] = "nltucyi"; Stntex[43, 4] = "nltutyi"; Stntex[43, 5] = "nnccyi"; Stntex[43, 6] = "nnttyi"; Stntex[43, 7] = "nnltucyi"; Stntex[43, 8] = "nnltutyi";
                Stntex[44, 1] = "ncche"; Stntex[44, 2] = "nttye"; Stntex[44, 3] = "nccye"; Stntex[44, 4] = "nltuche"; Stntex[44, 5] = "nltutye"; Stntex[44, 6] = "nltucye"; Stntex[44, 7] = "nncche"; Stntex[44, 8] = "nnttye"; Stntex[44, 9] = "nnccye"; Stntex[44, 10] = "nnltuche"; Stntex[44, 11] = "nnltutye"; Stntex[44, 12] = "nnltucye";
                Stntex[45, 1] = "nccha"; Stntex[45, 2] = "nttya"; Stntex[45, 3] = "nccya"; Stntex[45, 4] = "nltucha"; Stntex[45, 5] = "nltutya"; Stntex[45, 6] = "nltucya"; Stntex[45, 7] = "nnccha"; Stntex[45, 8] = "nnttya"; Stntex[45, 9] = "nnccya"; Stntex[45, 10] = "nnltucha"; Stntex[45, 11] = "nnltutya"; Stntex[45, 12] = "nnltucya";
                Stntex[46, 1] = "ncchu"; Stntex[46, 2] = "nttyu"; Stntex[46, 3] = "nccyu"; Stntex[46, 4] = "nltuchu"; Stntex[46, 5] = "nltutyu"; Stntex[46, 6] = "nltucyu"; Stntex[46, 7] = "nncchu"; Stntex[46, 8] = "nnttyu"; Stntex[46, 9] = "nnccyu"; Stntex[46, 10] = "nnltuchu"; Stntex[46, 11] = "nnltutyu"; Stntex[46, 12] = "nnltucyu";
                Stntex[47, 1] = "nccho"; Stntex[47, 2] = "nttyo"; Stntex[47, 3] = "nccyo"; Stntex[47, 4] = "nltucho"; Stntex[47, 5] = "nltutyo"; Stntex[47, 6] = "nltucyo"; Stntex[47, 7] = "nnccho"; Stntex[47, 8] = "nnttyo"; Stntex[47, 9] = "nnccyo"; Stntex[47, 10] = "nnltucho"; Stntex[47, 11] = "nnltutyo"; Stntex[47, 12] = "nnltucyo";

                Stntex[160, 1] = "nccyi"; Stntex[160, 2] = "nttyi"; Stntex[160, 3] = "nltucyi"; Stntex[160, 4] = "nltutyi"; Stntex[160, 5] = "nnccyi"; Stntex[160, 6] = "nnttyi"; Stntex[160, 7] = "nnltucyi"; Stntex[160, 8] = "nnltutyi";
                Stntex[161, 1] = "ncche"; Stntex[161, 2] = "nttye"; Stntex[161, 3] = "nccye"; Stntex[161, 4] = "nltuche"; Stntex[161, 5] = "nltutye"; Stntex[161, 6] = "nltucye"; Stntex[161, 7] = "nncche"; Stntex[161, 8] = "nnttye"; Stntex[161, 9] = "nnccye"; Stntex[161, 10] = "nnltuche"; Stntex[161, 11] = "nnltutye"; Stntex[161, 12] = "nnltucye";
                Stntex[162, 1] = "nccha"; Stntex[162, 2] = "nttya"; Stntex[162, 3] = "nccya"; Stntex[162, 4] = "nltucha"; Stntex[162, 5] = "nltutya"; Stntex[162, 6] = "nltucya"; Stntex[162, 7] = "nnccha"; Stntex[162, 8] = "nnttya"; Stntex[162, 9] = "nnccya"; Stntex[162, 10] = "nnltucha"; Stntex[162, 11] = "nnltutya"; Stntex[162, 12] = "nnltucya";
                Stntex[163, 1] = "ncchu"; Stntex[163, 2] = "nttyu"; Stntex[163, 3] = "nccyu"; Stntex[163, 4] = "nltuchu"; Stntex[163, 5] = "nltutyu"; Stntex[163, 6] = "nltucyu"; Stntex[163, 7] = "nncchu"; Stntex[163, 8] = "nnttyu"; Stntex[163, 9] = "nnccyu"; Stntex[163, 10] = "nnltuchu"; Stntex[163, 11] = "nnltutyu"; Stntex[163, 12] = "nnltucyu";
                Stntex[164, 1] = "nccho"; Stntex[164, 2] = "nttyo"; Stntex[164, 3] = "nccyo"; Stntex[164, 4] = "nltucho"; Stntex[164, 5] = "nltutyo"; Stntex[164, 6] = "nltucyo"; Stntex[164, 7] = "nnccho"; Stntex[164, 8] = "nnttyo"; Stntex[164, 9] = "nnccyo"; Stntex[164, 10] = "nnltucho"; Stntex[164, 11] = "nnltutyo"; Stntex[164, 12] = "nnltucyo";
            }
        }
        else if (tya == 3)
        {
            //CYA CYU CYO
            Stex[43, 1] = "cyi"; Stex[43, 2] = "tyi"; Stex[43, 3] = "chili"; Stex[43, 4] = "tili";
            Stex[44, 1] = "cye"; Stex[44, 2] = "che"; Stex[44, 3] = "tye"; Stex[44, 4] = "chile"; Stex[44, 5] = "tile";
            Stex[45, 1] = "cya"; Stex[45, 2] = "cha"; Stex[45, 3] = "tya"; Stex[45, 4] = "chilya"; Stex[45, 5] = "tilya";
            Stex[46, 1] = "cyu"; Stex[46, 2] = "chu"; Stex[46, 3] = "tyu"; Stex[46, 4] = "chilyu"; Stex[46, 5] = "tilyu";
            Stex[47, 1] = "cyo"; Stex[47, 2] = "cho"; Stex[47, 3] = "tyo"; Stex[47, 4] = "chilyo"; Stex[47, 5] = "tilyo";

            Stex[165, 1] = "cyi"; Stex[165, 2] = "tyi"; Stex[165, 3] = "chili"; Stex[165, 4] = "tili";
            Stex[166, 1] = "cye"; Stex[166, 2] = "che"; Stex[166, 3] = "tye"; Stex[166, 4] = "chile"; Stex[166, 5] = "tile";
            Stex[167, 1] = "cya"; Stex[167, 2] = "cha"; Stex[167, 3] = "tya"; Stex[167, 4] = "chilya"; Stex[167, 5] = "tilya";
            Stex[168, 1] = "cyu"; Stex[168, 2] = "chu"; Stex[168, 3] = "tyu"; Stex[168, 4] = "chilyu"; Stex[168, 5] = "tilyu";
            Stex[169, 1] = "cyo"; Stex[169, 2] = "cho"; Stex[169, 3] = "tyo"; Stex[169, 4] = "chilyo"; Stex[169, 5] = "tilyo";

            Sttex[43, 1] = "ccyi"; Sttex[43, 2] = "ttyi"; Sttex[43, 3] = "ltucyi"; Sttex[43, 4] = "ltutyi";
            Sttex[44, 1] = "ccye"; Sttex[44, 2] = "cche"; Sttex[44, 3] = "ttye"; Sttex[44, 4] = "ltucye"; Sttex[44, 5] = "ltuche"; Sttex[44, 6] = "ltutye";
            Sttex[45, 1] = "ccya"; Sttex[45, 2] = "ccha"; Sttex[45, 3] = "ttya"; Sttex[45, 4] = "ltucya"; Sttex[45, 5] = "ltucha"; Sttex[45, 6] = "ltutya";
            Sttex[46, 1] = "ccyu"; Sttex[46, 2] = "cchu"; Sttex[46, 3] = "ttyu"; Sttex[46, 4] = "ltucyu"; Sttex[46, 5] = "ltuchu"; Sttex[46, 6] = "ltutyu";
            Sttex[47, 1] = "ccyo"; Sttex[47, 2] = "ccho"; Sttex[47, 3] = "ttyo"; Sttex[47, 4] = "ltucyo"; Sttex[47, 5] = "ltucho"; Sttex[47, 6] = "ltutyo";

            Sttex[160, 1] = "ccyi"; Sttex[160, 2] = "ttyi"; Sttex[160, 3] = "ltucyi"; Sttex[160, 4] = "ltutyi";
            Sttex[161, 1] = "ccye"; Sttex[161, 2] = "cche"; Sttex[161, 3] = "ttye"; Sttex[161, 4] = "ltucye"; Sttex[161, 5] = "ltuche"; Sttex[161, 6] = "ltutye";
            Sttex[162, 1] = "ccya"; Sttex[162, 2] = "ccha"; Sttex[162, 3] = "ttya"; Sttex[162, 4] = "ltucya"; Sttex[162, 5] = "ltucha"; Sttex[162, 6] = "ltutya";
            Sttex[163, 1] = "ccyu"; Sttex[163, 2] = "cchu"; Sttex[163, 3] = "ttyu"; Sttex[163, 4] = "ltucyu"; Sttex[163, 5] = "ltuchu"; Sttex[163, 6] = "ltutyu";
            Sttex[164, 1] = "ccyo"; Sttex[164, 2] = "ccho"; Sttex[164, 3] = "ttyo"; Sttex[164, 4] = "ltucyo"; Sttex[164, 5] = "ltucho"; Sttex[164, 6] = "ltutyo";

            Stnex[42, 1] = "nncyi"; Stnex[42, 2] = "nntyi"; Stnex[42, 3] = "ncyi"; Stnex[42, 4] = "ntyi";
            Stnex[43, 1] = "nncye"; Stnex[43, 2] = "nnche"; Stnex[43, 3] = "nntye"; Stnex[43, 4] = "ncye"; Stnex[43, 5] = "nche"; Stnex[43, 6] = "ntye";
            Stnex[44, 1] = "nncya"; Stnex[44, 2] = "nncha"; Stnex[44, 3] = "nntya"; Stnex[44, 4] = "ncya"; Stnex[44, 5] = "ncha"; Stnex[44, 6] = "ntya";
            Stnex[45, 1] = "nncyu"; Stnex[45, 2] = "nnchu"; Stnex[45, 3] = "nntyu"; Stnex[45, 4] = "ncyu"; Stnex[45, 5] = "nchu"; Stnex[45, 6] = "ntyu";
            Stnex[46, 1] = "nncyo"; Stnex[46, 2] = "nncho"; Stnex[46, 3] = "nntyo"; Stnex[46, 4] = "ncyo"; Stnex[46, 5] = "ncho"; Stnex[46, 6] = "ntyo";

            Stnex[158, 1] = "nncyi"; Stnex[158, 2] = "nntyi"; Stnex[158, 3] = "ncyi"; Stnex[158, 4] = "ntyi";
            Stnex[159, 1] = "nncye"; Stnex[159, 2] = "nnche"; Stnex[159, 3] = "nntye"; Stnex[159, 4] = "ncye"; Stnex[159, 5] = "nche"; Stnex[159, 6] = "ntye";
            Stnex[160, 1] = "nncya"; Stnex[160, 2] = "nncha"; Stnex[160, 3] = "nntya"; Stnex[160, 4] = "ncya"; Stnex[160, 5] = "ncha"; Stnex[160, 6] = "ntya";
            Stnex[161, 1] = "nncyu"; Stnex[161, 2] = "nnchu"; Stnex[161, 3] = "nntyu"; Stnex[161, 4] = "ncyu"; Stnex[161, 5] = "nchu"; Stnex[161, 6] = "ntyu";
            Stnex[162, 1] = "nncyo"; Stnex[162, 2] = "nncho"; Stnex[162, 3] = "nntyo"; Stnex[162, 4] = "ncyo"; Stnex[162, 5] = "ncho"; Stnex[162, 6] = "ntyo";

            Stntex[43, 1] = "nnccyi"; Stntex[43, 2] = "nnttyi"; Stntex[43, 3] = "nnltucyi"; Stntex[43, 4] = "nnltutyi"; Stntex[43, 5] = "nccyi"; Stntex[43, 6] = "nttyi"; Stntex[43, 7] = "nltucyi"; Stntex[43, 8] = "nltutyi";
            Stntex[44, 1] = "nnccye"; Stntex[44, 2] = "nncche"; Stntex[44, 3] = "nnttye"; Stntex[44, 4] = "nnltucye"; Stntex[44, 5] = "nnltuche"; Stntex[44, 6] = "nnltutye"; Stntex[44, 7] = "nccye"; Stntex[44, 8] = "ncche"; Stntex[44, 9] = "nttye"; Stntex[44, 10] = "nltucye"; Stntex[44, 11] = "nltuche"; Stntex[44, 12] = "nltutye";
            Stntex[45, 1] = "nnccya"; Stntex[45, 2] = "nnccha"; Stntex[45, 3] = "nnttya"; Stntex[45, 4] = "nnltucya"; Stntex[45, 5] = "nnltucha"; Stntex[45, 6] = "nnltutya"; Stntex[45, 7] = "nccya"; Stntex[45, 8] = "nccha"; Stntex[45, 9] = "nttya"; Stntex[45, 10] = "nltucya"; Stntex[45, 11] = "nltucha"; Stntex[45, 12] = "nltutya";
            Stntex[46, 1] = "nnccyu"; Stntex[46, 2] = "nncchu"; Stntex[46, 3] = "nnttyu"; Stntex[46, 4] = "nnltucyu"; Stntex[46, 5] = "nnltuchu"; Stntex[46, 6] = "nnltutyu"; Stntex[46, 7] = "nccyu"; Stntex[46, 8] = "ncchu"; Stntex[46, 9] = "nttyu"; Stntex[46, 10] = "nltucyu"; Stntex[46, 11] = "nltuchu"; Stntex[46, 12] = "nltutyu";
            Stntex[47, 1] = "nnccyo"; Stntex[47, 2] = "nnccho"; Stntex[47, 3] = "nnttyo"; Stntex[47, 4] = "nnltucyo"; Stntex[47, 5] = "nnltucho"; Stntex[47, 6] = "nnltutyo"; Stntex[47, 7] = "nccyo"; Stntex[47, 8] = "nccho"; Stntex[47, 9] = "nttyo"; Stntex[47, 10] = "nltucyo"; Stntex[47, 11] = "nltucho"; Stntex[47, 12] = "nltutyo";

            Stntex[160, 1] = "nnccyi"; Stntex[160, 2] = "nnttyi"; Stntex[160, 3] = "nnltucyi"; Stntex[160, 4] = "nnltutyi"; Stntex[160, 5] = "nccyi"; Stntex[160, 6] = "nttyi"; Stntex[160, 7] = "nltucyi"; Stntex[160, 8] = "nltutyi";
            Stntex[161, 1] = "nnccye"; Stntex[161, 2] = "nncche"; Stntex[161, 3] = "nnttye"; Stntex[161, 4] = "nnltucye"; Stntex[161, 5] = "nnltuche"; Stntex[161, 6] = "nnltutye"; Stntex[161, 7] = "nccye"; Stntex[161, 8] = "ncche"; Stntex[161, 9] = "nttye"; Stntex[161, 10] = "nltucye"; Stntex[161, 11] = "nltuche"; Stntex[161, 12] = "nltutye";
            Stntex[162, 1] = "nnccya"; Stntex[162, 2] = "nnccha"; Stntex[162, 3] = "nnttya"; Stntex[162, 4] = "nnltucya"; Stntex[162, 5] = "nnltucha"; Stntex[162, 6] = "nnltutya"; Stntex[162, 7] = "nccya"; Stntex[162, 8] = "nccha"; Stntex[162, 9] = "nttya"; Stntex[162, 10] = "nltucya"; Stntex[162, 11] = "nltucha"; Stntex[162, 12] = "nltutya";
            Stntex[163, 1] = "nnccyu"; Stntex[163, 2] = "nncchu"; Stntex[163, 3] = "nnttyu"; Stntex[163, 4] = "nnltucyu"; Stntex[163, 5] = "nnltuchu"; Stntex[163, 6] = "nnltutyu"; Stntex[163, 7] = "nccyu"; Stntex[163, 8] = "ncchu"; Stntex[163, 9] = "nttyu"; Stntex[163, 10] = "nltucyu"; Stntex[163, 11] = "nltuchu"; Stntex[163, 12] = "nltutyu";
            Stntex[164, 1] = "nnccyo"; Stntex[164, 2] = "nnccho"; Stntex[164, 3] = "nnttyo"; Stntex[164, 4] = "nnltucyo"; Stntex[164, 5] = "nnltucho"; Stntex[164, 6] = "nnltutyo"; Stntex[164, 7] = "nccyo"; Stntex[164, 8] = "nccho"; Stntex[164, 9] = "nttyo"; Stntex[164, 10] = "nltucyo"; Stntex[164, 11] = "nltucho"; Stntex[164, 12] = "nltutyo";

            if (nn == 1)
            {
                Stnex[42, 1] = "ncyi"; Stnex[42, 2] = "ntyi"; Stnex[42, 3] = "nntyi"; Stnex[42, 4] = "nncyi";
                Stnex[43, 1] = "ncye"; Stnex[43, 2] = "nche"; Stnex[43, 3] = "ntye"; Stnex[43, 4] = "nncye"; Stnex[43, 5] = "nnche"; Stnex[43, 6] = "nntye";
                Stnex[44, 1] = "ncya"; Stnex[44, 2] = "ncha"; Stnex[44, 3] = "ntya"; Stnex[44, 4] = "nncya"; Stnex[44, 5] = "nncha"; Stnex[44, 6] = "nntya";
                Stnex[45, 1] = "ncyu"; Stnex[45, 2] = "nchu"; Stnex[45, 3] = "ntyu"; Stnex[45, 4] = "nncyu"; Stnex[45, 5] = "nnchu"; Stnex[45, 6] = "nntyu";
                Stnex[46, 1] = "ncyo"; Stnex[46, 2] = "ncho"; Stnex[46, 3] = "ntyo"; Stnex[46, 4] = "nncyo"; Stnex[46, 5] = "nncho"; Stnex[46, 6] = "nntyo";

                Stnex[158, 1] = "ncyi"; Stnex[158, 2] = "ntyi"; Stnex[158, 3] = "nncyi"; Stnex[158, 4] = "nntyi";
                Stnex[159, 1] = "ncye"; Stnex[159, 2] = "nche"; Stnex[159, 3] = "ntye"; Stnex[159, 4] = "nncye"; Stnex[159, 5] = "nnche"; Stnex[159, 6] = "nntye";
                Stnex[160, 1] = "ncya"; Stnex[160, 2] = "ncha"; Stnex[160, 3] = "ntya"; Stnex[160, 4] = "nncya"; Stnex[160, 5] = "nncha"; Stnex[160, 6] = "nntya";
                Stnex[161, 1] = "ncyu"; Stnex[161, 2] = "nchu"; Stnex[161, 3] = "ntyu"; Stnex[161, 4] = "nncyu"; Stnex[161, 5] = "nnchu"; Stnex[161, 6] = "nntyu";
                Stnex[162, 1] = "ncyo"; Stnex[162, 2] = "ncho"; Stnex[162, 3] = "ntyo"; Stnex[162, 4] = "nncyo"; Stnex[162, 5] = "nncho"; Stnex[162, 6] = "nntyo";

                Stntex[43, 1] = "nccyi"; Stntex[43, 2] = "nttyi"; Stntex[43, 3] = "nltucyi"; Stntex[43, 4] = "nltutyi"; Stntex[43, 5] = "nnccyi"; Stntex[43, 6] = "nnttyi"; Stntex[43, 7] = "nnltucyi"; Stntex[43, 8] = "nnltutyi";
                Stntex[44, 1] = "nccye"; Stntex[44, 2] = "ncche"; Stntex[44, 3] = "nttye"; Stntex[44, 4] = "nltucye"; Stntex[44, 5] = "nltuche"; Stntex[44, 6] = "nltutye"; Stntex[44, 7] = "nnccye"; Stntex[44, 8] = "nncche"; Stntex[44, 9] = "nnttye"; Stntex[44, 10] = "nnltucye"; Stntex[44, 11] = "nnltuche"; Stntex[44, 12] = "nnltutye";
                Stntex[45, 1] = "nccya"; Stntex[45, 2] = "nccha"; Stntex[45, 3] = "nttya"; Stntex[45, 4] = "nltucya"; Stntex[45, 5] = "nltucha"; Stntex[45, 6] = "nltutya"; Stntex[45, 7] = "nnccya"; Stntex[45, 8] = "nnccha"; Stntex[45, 9] = "nnttya"; Stntex[45, 10] = "nnltucya"; Stntex[45, 11] = "nnltucha"; Stntex[45, 12] = "nnltutya";
                Stntex[46, 1] = "nccyu"; Stntex[46, 2] = "ncchu"; Stntex[46, 3] = "nttyu"; Stntex[46, 4] = "nltucyu"; Stntex[46, 5] = "nltuchu"; Stntex[46, 6] = "nltutyu"; Stntex[46, 7] = "nnccyu"; Stntex[46, 8] = "nncchu"; Stntex[46, 9] = "nnttyu"; Stntex[46, 10] = "nnltucyu"; Stntex[46, 11] = "nnltuchu"; Stntex[46, 12] = "nnltutyu";
                Stntex[47, 1] = "nccyo"; Stntex[47, 2] = "nccho"; Stntex[47, 3] = "nttyo"; Stntex[47, 4] = "nltucyo"; Stntex[47, 5] = "nltucho"; Stntex[47, 6] = "nltutyo"; Stntex[47, 7] = "nnccyo"; Stntex[47, 8] = "nnccho"; Stntex[47, 9] = "nnttyo"; Stntex[47, 10] = "nnltucyo"; Stntex[47, 11] = "nnltucho"; Stntex[47, 12] = "nnltutyo";

                Stntex[160, 1] = "nccyi"; Stntex[160, 2] = "nttyi"; Stntex[160, 3] = "nltucyi"; Stntex[160, 4] = "nltutyi"; Stntex[160, 5] = "nnccyi"; Stntex[160, 6] = "nnttyi"; Stntex[160, 7] = "nnltucyi"; Stntex[160, 8] = "nnltutyi";
                Stntex[161, 1] = "nccye"; Stntex[161, 2] = "ncche"; Stntex[161, 3] = "nttye"; Stntex[161, 4] = "nltucye"; Stntex[161, 5] = "nltuche"; Stntex[161, 6] = "nltutye"; Stntex[161, 7] = "nnccye"; Stntex[161, 8] = "nncche"; Stntex[161, 9] = "nnttye"; Stntex[161, 10] = "nnltucye"; Stntex[161, 11] = "nnltuche"; Stntex[161, 12] = "nnltutye";
                Stntex[162, 1] = "nccya"; Stntex[162, 2] = "nccha"; Stntex[162, 3] = "nttya"; Stntex[162, 4] = "nltucya"; Stntex[162, 5] = "nltucha"; Stntex[162, 6] = "nltutya"; Stntex[162, 7] = "nnccya"; Stntex[162, 8] = "nnccha"; Stntex[162, 9] = "nnttya"; Stntex[162, 10] = "nnltucya"; Stntex[162, 11] = "nnltucha"; Stntex[162, 12] = "nnltutya";
                Stntex[163, 1] = "nccyu"; Stntex[163, 2] = "ncchu"; Stntex[163, 3] = "nttyu"; Stntex[163, 4] = "nltucyu"; Stntex[163, 5] = "nltuchu"; Stntex[163, 6] = "nltutyu"; Stntex[163, 7] = "nnccyu"; Stntex[163, 8] = "nncchu"; Stntex[163, 9] = "nnttyu"; Stntex[163, 10] = "nnltucyu"; Stntex[163, 11] = "nnltuchu"; Stntex[163, 12] = "nnltutyu";
                Stntex[164, 1] = "nccyo"; Stntex[164, 2] = "nccho"; Stntex[164, 3] = "nttyo"; Stntex[164, 4] = "nltucyo"; Stntex[164, 5] = "nltucho"; Stntex[164, 6] = "nltutyo"; Stntex[164, 7] = "nnccyo"; Stntex[164, 8] = "nnccho"; Stntex[164, 9] = "nnttyo"; Stntex[164, 10] = "nnltucyo"; Stntex[164, 11] = "nnltucho"; Stntex[164, 12] = "nnltutyo";
            }
        }
    }
    public void SetJA(int ja, int nn)
    {
        if (ja == 1)
        {
            //JA JU JO
            Stex[33, 1] = "jyi"; Stex[33, 2] = "zyi"; Stex[33, 3] = "jili"; Stex[33, 4] = "zili";
            Stex[34, 1] = "je"; Stex[34, 2] = "jye"; Stex[34, 3] = "zye"; Stex[34, 4] = "jile"; Stex[34, 5] = "zile";
            Stex[35, 1] = "ja"; Stex[35, 2] = "jya"; Stex[35, 3] = "zya"; Stex[35, 4] = "jilya"; Stex[35, 5] = "zilya";
            Stex[36, 1] = "ju"; Stex[36, 2] = "jyu"; Stex[36, 3] = "zyu"; Stex[36, 4] = "jilyu"; Stex[36, 5] = "zilyu";
            Stex[37, 1] = "jo"; Stex[37, 2] = "jyo"; Stex[37, 3] = "zyo"; Stex[37, 4] = "jilyo"; Stex[37, 5] = "zilyo";

            Stex[155, 1] = "jyi"; Stex[155, 2] = "zyi"; Stex[155, 3] = "jili"; Stex[155, 4] = "zili";
            Stex[156, 1] = "je"; Stex[156, 2] = "jye"; Stex[156, 3] = "zye"; Stex[156, 4] = "jile"; Stex[156, 5] = "zile";
            Stex[157, 1] = "ja"; Stex[157, 2] = "jya"; Stex[157, 3] = "zya"; Stex[157, 4] = "jilya"; Stex[157, 5] = "zilya";
            Stex[158, 1] = "ju"; Stex[158, 2] = "jyu"; Stex[158, 3] = "zyu"; Stex[158, 4] = "jilyu"; Stex[158, 5] = "zilyu";
            Stex[159, 1] = "jo"; Stex[159, 2] = "jyo"; Stex[159, 3] = "zyo"; Stex[159, 4] = "jilyo"; Stex[159, 5] = "zilyo";

            Sttex[33, 1] = "jjyi"; Sttex[33, 2] = "zzyi"; Sttex[33, 3] = "ltujyi"; Sttex[33, 4] = "ltuzyi";
            Sttex[34, 1] = "jje"; Sttex[34, 2] = "jjye"; Sttex[34, 3] = "zzye"; Sttex[34, 4] = "ltuje"; Sttex[34, 5] = "ltujye"; Sttex[34, 6] = "ltuzye";
            Sttex[35, 1] = "jja"; Sttex[35, 2] = "jjya"; Sttex[35, 3] = "zzya"; Sttex[35, 4] = "ltuja"; Sttex[35, 5] = "ltujya"; Sttex[35, 6] = "ltuzya";
            Sttex[36, 1] = "jju"; Sttex[36, 2] = "jjyu"; Sttex[36, 3] = "zzyu"; Sttex[36, 4] = "ltuju"; Sttex[36, 5] = "ltujyu"; Sttex[36, 6] = "ltuzyu";
            Sttex[37, 1] = "jjo"; Sttex[37, 2] = "jjyo"; Sttex[37, 3] = "zzyo"; Sttex[37, 4] = "ltujo"; Sttex[37, 5] = "ltujyo"; Sttex[37, 6] = "ltuzyo";
            Sttex[150, 1] = "jjyi"; Sttex[150, 2] = "zzyi"; Sttex[150, 3] = "ltujyi"; Sttex[150, 4] = "ltuzyi";
            Sttex[151, 1] = "jje"; Sttex[151, 2] = "jjye"; Sttex[151, 3] = "zzye"; Sttex[151, 4] = "ltuje"; Sttex[151, 5] = "ltujye"; Sttex[151, 6] = "ltuzye";
            Sttex[152, 1] = "jja"; Sttex[152, 2] = "jjya"; Sttex[152, 3] = "zzya"; Sttex[152, 4] = "ltuja"; Sttex[152, 5] = "ltujya"; Sttex[152, 6] = "ltuzya";
            Sttex[153, 1] = "jju"; Sttex[153, 2] = "jjyu"; Sttex[153, 3] = "zzyu"; Sttex[153, 4] = "ltuju"; Sttex[153, 5] = "ltujyu"; Sttex[153, 6] = "ltuzyu";
            Sttex[154, 1] = "jjo"; Sttex[154, 2] = "jjyo"; Sttex[154, 3] = "zzyo"; Sttex[154, 4] = "ltujo"; Sttex[154, 5] = "ltujyo"; Sttex[154, 6] = "ltuzyo";

            Stnex[32, 1] = "nnjyi"; Stnex[32, 2] = "nnzyi"; Stnex[32, 4] = "njyi"; Stnex[32, 5] = "nzyi";
            Stnex[33, 1] = "nnje"; Stnex[33, 2] = "nnjye"; Stnex[33, 3] = "nnzye"; Stnex[33, 4] = "nje"; Stnex[33, 5] = "njye"; Stnex[33, 6] = "nzye";
            Stnex[34, 1] = "nnja"; Stnex[34, 2] = "nnjya"; Stnex[34, 3] = "nnzya"; Stnex[34, 4] = "nja"; Stnex[34, 5] = "njya"; Stnex[34, 6] = "nzya";
            Stnex[35, 1] = "nnju"; Stnex[35, 2] = "nnjyu"; Stnex[35, 3] = "nnzyu"; Stnex[35, 4] = "nju"; Stnex[35, 5] = "njyu"; Stnex[35, 6] = "nzyu";
            Stnex[36, 1] = "nnjo"; Stnex[36, 2] = "nnjyo"; Stnex[36, 3] = "nnzyo"; Stnex[36, 4] = "njo"; Stnex[36, 5] = "njyo"; Stnex[36, 6] = "nzyo";
            Stnex[148, 1] = "nnjyi"; Stnex[148, 2] = "nnzyi"; Stnex[148, 4] = "njyi"; Stnex[148, 5] = "nzyi";
            Stnex[149, 1] = "nnje"; Stnex[149, 2] = "nnjye"; Stnex[149, 3] = "nnzye"; Stnex[149, 4] = "nje"; Stnex[149, 5] = "njye"; Stnex[149, 6] = "nzye";
            Stnex[150, 1] = "nnja"; Stnex[150, 2] = "nnjya"; Stnex[150, 3] = "nnzya"; Stnex[150, 4] = "nja"; Stnex[150, 5] = "njya"; Stnex[150, 6] = "nzya";
            Stnex[151, 1] = "nnju"; Stnex[151, 2] = "nnjyu"; Stnex[151, 3] = "nnzyu"; Stnex[151, 4] = "nju"; Stnex[151, 5] = "njyu"; Stnex[151, 6] = "nzyu";
            Stnex[152, 1] = "nnjo"; Stnex[152, 2] = "nnjyo"; Stnex[152, 3] = "nnzyo"; Stnex[152, 4] = "njo"; Stnex[152, 5] = "njyo"; Stnex[152, 6] = "nzyo";

            Stntex[33, 1] = "nnjjyi"; Stntex[33, 2] = "nnzzyi"; Stntex[33, 3] = "nnltujyi"; Stntex[33, 4] = "nnltuzyi"; Stntex[33, 5] = "njjyi"; Stntex[33, 6] = "nzzyi"; Stntex[33, 7] = "nltujyi"; Stntex[33, 8] = "nltuzyi";
            Stntex[34, 1] = "nnjje"; Stntex[34, 2] = "nnjjye"; Stntex[34, 3] = "nnzzye"; Stntex[34, 4] = "nnltuje"; Stntex[34, 5] = "nnltujye"; Stntex[34, 6] = "nnltuzye"; Stntex[34, 7] = "njje"; Stntex[34, 8] = "njjye"; Stntex[34, 9] = "nzzye"; Stntex[34, 10] = "nltuje"; Stntex[34, 11] = "nltujye"; Stntex[34, 12] = "nltuzye";
            Stntex[35, 1] = "nnjja"; Stntex[35, 2] = "nnjjya"; Stntex[35, 3] = "nnzzya"; Stntex[35, 4] = "nnltuja"; Stntex[35, 5] = "nnltujya"; Stntex[35, 6] = "nnltuzya"; Stntex[35, 7] = "njja"; Stntex[35, 8] = "njjya"; Stntex[35, 9] = "nzzya"; Stntex[35, 10] = "nltuja"; Stntex[35, 11] = "nltujya"; Stntex[35, 12] = "nltuzya";
            Stntex[36, 1] = "nnjju"; Stntex[36, 2] = "nnjjyu"; Stntex[36, 3] = "nnzzyu"; Stntex[36, 4] = "nnltuju"; Stntex[36, 5] = "nnltujyu"; Stntex[36, 6] = "nnltuzyu"; Stntex[36, 7] = "njju"; Stntex[36, 8] = "njjyu"; Stntex[36, 9] = "nzzyu"; Stntex[36, 10] = "nltuju"; Stntex[36, 11] = "nltujyu"; Stntex[36, 12] = "nltuzyu";
            Stntex[37, 1] = "nnjjo"; Stntex[37, 2] = "nnjjyo"; Stntex[37, 3] = "nnzzyo"; Stntex[37, 4] = "nnltujo"; Stntex[37, 5] = "nnltujyo"; Stntex[37, 6] = "nnltuzyo"; Stntex[37, 7] = "njjo"; Stntex[37, 8] = "njjyo"; Stntex[37, 9] = "nzzyo"; Stntex[37, 10] = "nltujo"; Stntex[37, 11] = "nltujyo"; Stntex[37, 12] = "nltuzyo";
            Stntex[150, 1] = "nnjjyi"; Stntex[150, 2] = "nnzzyi"; Stntex[150, 3] = "nnltujyi"; Stntex[150, 4] = "nnltuzyi"; Stntex[150, 5] = "njjyi"; Stntex[150, 6] = "nzzyi"; Stntex[150, 7] = "nltujyi"; Stntex[150, 8] = "nltuzyi";
            Stntex[151, 1] = "nnjje"; Stntex[151, 2] = "nnjjye"; Stntex[151, 3] = "nnzzye"; Stntex[151, 4] = "nnltuje"; Stntex[151, 5] = "nnltujye"; Stntex[151, 6] = "nnltuzye"; Stntex[151, 7] = "njje"; Stntex[151, 8] = "njjye"; Stntex[151, 9] = "nzzye"; Stntex[151, 10] = "nltuje"; Stntex[151, 11] = "nltujye"; Stntex[151, 12] = "nltuzye";
            Stntex[152, 1] = "nnjja"; Stntex[152, 2] = "nnjjya"; Stntex[152, 3] = "nnzzya"; Stntex[152, 4] = "nnltuja"; Stntex[152, 5] = "nnltujya"; Stntex[152, 6] = "nnltuzya"; Stntex[152, 7] = "njja"; Stntex[152, 8] = "njjya"; Stntex[152, 9] = "nzzya"; Stntex[152, 10] = "nltuja"; Stntex[152, 11] = "nltujya"; Stntex[152, 12] = "nltuzya";
            Stntex[153, 1] = "nnjju"; Stntex[153, 2] = "nnjjyu"; Stntex[153, 3] = "nnzzyu"; Stntex[153, 4] = "nnltuju"; Stntex[153, 5] = "nnltujyu"; Stntex[153, 6] = "nnltuzyu"; Stntex[153, 7] = "njju"; Stntex[153, 8] = "njjyu"; Stntex[153, 9] = "nzzyu"; Stntex[153, 10] = "nltuju"; Stntex[153, 11] = "nltujyu"; Stntex[153, 12] = "nltuzyu";
            Stntex[154, 1] = "nnjjo"; Stntex[154, 2] = "nnjjyo"; Stntex[154, 3] = "nnzzyo"; Stntex[154, 4] = "nnltujo"; Stntex[154, 5] = "nnltujyo"; Stntex[154, 6] = "nnltuzyo"; Stntex[154, 7] = "njjo"; Stntex[154, 8] = "njjyo"; Stntex[154, 9] = "nzzyo"; Stntex[154, 10] = "nltujo"; Stntex[154, 11] = "nltujyo"; Stntex[154, 12] = "nltuzyo";

            if (nn == 1)
            {
                Stnex[32, 1] = "njyi"; Stnex[32, 2] = "nzyi"; Stnex[32, 4] = "nnjyi"; Stnex[32, 5] = "nnzyi";
                Stnex[33, 1] = "nje"; Stnex[33, 2] = "njye"; Stnex[33, 3] = "nzye"; Stnex[33, 4] = "nnje"; Stnex[33, 5] = "nnjye"; Stnex[33, 6] = "nnzye";
                Stnex[34, 1] = "nja"; Stnex[34, 2] = "njya"; Stnex[34, 3] = "nzya"; Stnex[34, 4] = "nnja"; Stnex[34, 5] = "nnjya"; Stnex[34, 6] = "nnzya";
                Stnex[35, 1] = "nju"; Stnex[35, 2] = "njyu"; Stnex[35, 3] = "nzyu"; Stnex[35, 4] = "nnju"; Stnex[35, 5] = "nnjyu"; Stnex[35, 6] = "nnzyu";
                Stnex[36, 1] = "njo"; Stnex[36, 2] = "njyo"; Stnex[36, 3] = "nzyo"; Stnex[36, 4] = "nnjo"; Stnex[36, 5] = "nnjyo"; Stnex[36, 6] = "nnzyo";
                Stnex[148, 1] = "njyi"; Stnex[148, 2] = "nzyi"; Stnex[148, 4] = "nnjyi"; Stnex[148, 5] = "nnzyi";
                Stnex[149, 1] = "nje"; Stnex[149, 2] = "njye"; Stnex[149, 3] = "nzye"; Stnex[149, 4] = "nnje"; Stnex[149, 5] = "nnjye"; Stnex[149, 6] = "nnzye";
                Stnex[150, 1] = "nja"; Stnex[150, 2] = "njya"; Stnex[150, 3] = "nzya"; Stnex[150, 4] = "nnja"; Stnex[150, 5] = "nnjya"; Stnex[150, 6] = "nnzya";
                Stnex[151, 1] = "nju"; Stnex[151, 2] = "njyu"; Stnex[151, 3] = "nzyu"; Stnex[151, 4] = "nnju"; Stnex[151, 5] = "nnjyu"; Stnex[151, 6] = "nnzyu";
                Stnex[152, 1] = "njo"; Stnex[152, 2] = "njyo"; Stnex[152, 3] = "nzyo"; Stnex[152, 4] = "nnjo"; Stnex[152, 5] = "nnjyo"; Stnex[152, 6] = "nnzyo";

                Stntex[33, 1] = "njjyi"; Stntex[33, 2] = "nzzyi"; Stntex[33, 3] = "nltujyi"; Stntex[33, 4] = "nltuzyi"; Stntex[33, 5] = "nnjjyi"; Stntex[33, 6] = "nnzzyi"; Stntex[33, 7] = "nnltujyi"; Stntex[33, 8] = "nnltuzyi";
                Stntex[34, 1] = "njje"; Stntex[34, 2] = "njjye"; Stntex[34, 3] = "nzzye"; Stntex[34, 4] = "nltuje"; Stntex[34, 5] = "nltujye"; Stntex[34, 6] = "nltuzye"; Stntex[34, 7] = "nnjje"; Stntex[34, 8] = "nnjjye"; Stntex[34, 9] = "nnzzye"; Stntex[34, 10] = "nnltuje"; Stntex[34, 11] = "nnltujye"; Stntex[34, 12] = "nnltuzye";
                Stntex[35, 1] = "njja"; Stntex[35, 2] = "njjya"; Stntex[35, 3] = "nzzya"; Stntex[35, 4] = "nltuja"; Stntex[35, 5] = "nltujya"; Stntex[35, 6] = "nltuzya"; Stntex[35, 7] = "nnjja"; Stntex[35, 8] = "nnjjya"; Stntex[35, 9] = "nnzzya"; Stntex[35, 10] = "nnltuja"; Stntex[35, 11] = "nnltujya"; Stntex[35, 12] = "nnltuzya";
                Stntex[36, 1] = "njju"; Stntex[36, 2] = "njjyu"; Stntex[36, 3] = "nzzyu"; Stntex[36, 4] = "nltuju"; Stntex[36, 5] = "nltujyu"; Stntex[36, 6] = "nltuzyu"; Stntex[36, 7] = "nnjju"; Stntex[36, 8] = "nnjjyu"; Stntex[36, 9] = "nnzzyu"; Stntex[36, 10] = "nnltuju"; Stntex[36, 11] = "nnltujyu"; Stntex[36, 12] = "nnltuzyu";
                Stntex[37, 1] = "njjo"; Stntex[37, 2] = "njjyo"; Stntex[37, 3] = "nzzyo"; Stntex[37, 4] = "nltujo"; Stntex[37, 5] = "nltujyo"; Stntex[37, 6] = "nltuzyo"; Stntex[37, 7] = "nnjjo"; Stntex[37, 8] = "nnjjyo"; Stntex[37, 9] = "nnzzyo"; Stntex[37, 10] = "nnltujo"; Stntex[37, 11] = "nnltujyo"; Stntex[37, 12] = "nnltuzyo";
                Stntex[150, 1] = "njjyi"; Stntex[150, 2] = "nzzyi"; Stntex[150, 3] = "nltujyi"; Stntex[150, 4] = "nltuzyi"; Stntex[150, 5] = "nnjjyi"; Stntex[150, 6] = "nnzzyi"; Stntex[150, 7] = "nnltujyi"; Stntex[150, 8] = "nnltuzyi";
                Stntex[151, 1] = "njje"; Stntex[151, 2] = "njjye"; Stntex[151, 3] = "nzzye"; Stntex[151, 4] = "nltuje"; Stntex[151, 5] = "nltujye"; Stntex[151, 6] = "nltuzye"; Stntex[151, 7] = "nnjje"; Stntex[151, 8] = "nnjjye"; Stntex[151, 9] = "nnzzye"; Stntex[151, 10] = "nnltuje"; Stntex[151, 11] = "nnltujye"; Stntex[151, 12] = "nnltuzye";
                Stntex[152, 1] = "njja"; Stntex[152, 2] = "njjya"; Stntex[152, 3] = "nzzya"; Stntex[152, 4] = "nltuja"; Stntex[152, 5] = "nltujya"; Stntex[152, 6] = "nltuzya"; Stntex[152, 7] = "nnjja"; Stntex[152, 8] = "nnjjya"; Stntex[152, 9] = "nnzzya"; Stntex[152, 10] = "nnltuja"; Stntex[152, 11] = "nnltujya"; Stntex[152, 12] = "nnltuzya";
                Stntex[153, 1] = "njju"; Stntex[153, 2] = "njjyu"; Stntex[153, 3] = "nzzyu"; Stntex[153, 4] = "nltuju"; Stntex[153, 5] = "nltujyu"; Stntex[153, 6] = "nltuzyu"; Stntex[153, 7] = "nnjju"; Stntex[153, 8] = "nnjjyu"; Stntex[153, 9] = "nnzzyu"; Stntex[153, 10] = "nnltuju"; Stntex[153, 11] = "nnltujyu"; Stntex[153, 12] = "nnltuzyu";
                Stntex[154, 1] = "njjo"; Stntex[154, 2] = "njjyo"; Stntex[154, 3] = "nzzyo"; Stntex[154, 4] = "nltujo"; Stntex[154, 5] = "nltujyo"; Stntex[154, 6] = "nltuzyo"; Stntex[154, 7] = "nnjjo"; Stntex[154, 8] = "nnjjyo"; Stntex[154, 9] = "nnzzyo"; Stntex[154, 10] = "nnltujo"; Stntex[154, 11] = "nnltujyo"; Stntex[154, 12] = "nnltuzyo";
            }
        }
        else if (ja == 2)
        {
            //JYA JYU JYO
            Stex[33, 1] = "jyi"; Stex[33, 2] = "zyi"; Stex[33, 3] = "jili"; Stex[33, 4] = "zili";
            Stex[34, 1] = "jye"; Stex[34, 2] = "je"; Stex[34, 3] = "zye"; Stex[34, 4] = "jile"; Stex[34, 5] = "zile";
            Stex[35, 1] = "jya"; Stex[35, 2] = "ja"; Stex[35, 3] = "zya"; Stex[35, 4] = "jilya"; Stex[35, 5] = "zilya";
            Stex[36, 1] = "jyu"; Stex[36, 2] = "ju"; Stex[36, 3] = "zyu"; Stex[36, 4] = "jilyu"; Stex[36, 5] = "zilyu";
            Stex[37, 1] = "jyo"; Stex[37, 2] = "jo"; Stex[37, 3] = "zyo"; Stex[37, 4] = "jilyo"; Stex[37, 5] = "zilyo";

            Stex[155, 1] = "jyi"; Stex[155, 2] = "zyi"; Stex[155, 3] = "jili"; Stex[155, 4] = "zili";
            Stex[156, 1] = "jye"; Stex[156, 2] = "je"; Stex[156, 3] = "zye"; Stex[156, 4] = "jile"; Stex[156, 5] = "zile";
            Stex[157, 1] = "jya"; Stex[157, 2] = "ja"; Stex[157, 3] = "zya"; Stex[157, 4] = "jilya"; Stex[157, 5] = "zilya";
            Stex[158, 1] = "jyu"; Stex[158, 2] = "ju"; Stex[158, 3] = "zyu"; Stex[158, 4] = "jilyu"; Stex[158, 5] = "zilyu";
            Stex[159, 1] = "jyo"; Stex[159, 2] = "jo"; Stex[159, 3] = "zyo"; Stex[159, 4] = "jilyo"; Stex[159, 5] = "zilyo";

            Sttex[33, 1] = "jjyi"; Sttex[33, 2] = "zzyi"; Sttex[33, 3] = "ltujyi"; Sttex[33, 4] = "ltuzyi";
            Sttex[34, 1] = "jjye"; Sttex[34, 2] = "jje"; Sttex[34, 3] = "zzye"; Sttex[34, 4] = "ltujye"; Sttex[34, 5] = "ltuje"; Sttex[34, 6] = "ltuzye";
            Sttex[35, 1] = "jjya"; Sttex[35, 2] = "jja"; Sttex[35, 3] = "zzya"; Sttex[35, 4] = "ltujya"; Sttex[35, 5] = "ltuja"; Sttex[35, 6] = "ltuzya";
            Sttex[36, 1] = "jjyu"; Sttex[36, 2] = "jju"; Sttex[36, 3] = "zzyu"; Sttex[36, 4] = "ltujyu"; Sttex[36, 5] = "ltuju"; Sttex[36, 6] = "ltuzyu";
            Sttex[37, 1] = "jjyo"; Sttex[37, 2] = "jjo"; Sttex[37, 3] = "zzyo"; Sttex[37, 4] = "ltujyo"; Sttex[37, 5] = "ltujo"; Sttex[37, 6] = "ltuzyo";

            Sttex[150, 1] = "jjyi"; Sttex[150, 2] = "zzyi"; Sttex[150, 3] = "ltujyi"; Sttex[150, 4] = "ltuzyi";
            Sttex[151, 1] = "jjye"; Sttex[151, 2] = "jje"; Sttex[151, 3] = "zzye"; Sttex[151, 4] = "ltujye"; Sttex[151, 5] = "ltuje"; Sttex[151, 6] = "ltuzye";
            Sttex[152, 1] = "jjya"; Sttex[152, 2] = "jja"; Sttex[152, 3] = "zzya"; Sttex[152, 4] = "ltujya"; Sttex[152, 5] = "ltuja"; Sttex[152, 6] = "ltuzya";
            Sttex[153, 1] = "jjyu"; Sttex[153, 2] = "jju"; Sttex[153, 3] = "zzyu"; Sttex[153, 4] = "ltujyu"; Sttex[153, 5] = "ltuju"; Sttex[153, 6] = "ltuzyu";
            Sttex[154, 1] = "jjyo"; Sttex[154, 2] = "jjo"; Sttex[154, 3] = "zzyo"; Sttex[154, 4] = "ltujyo"; Sttex[154, 5] = "ltujo"; Sttex[154, 6] = "ltuzyo";

            Stnex[32, 1] = "nnjyi"; Stnex[32, 2] = "nnzyi"; Stnex[32, 4] = "njyi"; Stnex[32, 5] = "nzyi";
            Stnex[33, 1] = "nnjye"; Stnex[33, 2] = "nnje"; Stnex[33, 3] = "nnzye"; Stnex[33, 4] = "njye"; Stnex[33, 5] = "nje"; Stnex[33, 6] = "nzye";
            Stnex[34, 1] = "nnjya"; Stnex[34, 2] = "nnja"; Stnex[34, 3] = "nnzya"; Stnex[34, 4] = "njya"; Stnex[34, 5] = "nja"; Stnex[34, 6] = "nzya";
            Stnex[35, 1] = "nnjyu"; Stnex[35, 2] = "nnju"; Stnex[35, 3] = "nnzyu"; Stnex[35, 4] = "njyu"; Stnex[35, 5] = "nju"; Stnex[35, 6] = "nzyu";
            Stnex[36, 1] = "nnjyo"; Stnex[36, 2] = "nnjo"; Stnex[36, 3] = "nnzyo"; Stnex[36, 4] = "njyo"; Stnex[36, 5] = "njo"; Stnex[36, 6] = "nzyo";

            Stnex[148, 1] = "nnjyi"; Stnex[148, 2] = "nnzyi"; Stnex[148, 4] = "njyi"; Stnex[148, 5] = "nzyi";
            Stnex[149, 1] = "nnjye"; Stnex[149, 2] = "nnje"; Stnex[149, 3] = "nnzye"; Stnex[149, 4] = "njye"; Stnex[149, 5] = "nje"; Stnex[149, 6] = "nzye";
            Stnex[150, 1] = "nnjya"; Stnex[150, 2] = "nnja"; Stnex[150, 3] = "nnzya"; Stnex[150, 4] = "njya"; Stnex[150, 5] = "nja"; Stnex[150, 6] = "nzya";
            Stnex[151, 1] = "nnjyu"; Stnex[151, 2] = "nnju"; Stnex[151, 3] = "nnzyu"; Stnex[151, 4] = "njyu"; Stnex[151, 5] = "nju"; Stnex[151, 6] = "nzyu";
            Stnex[152, 1] = "nnjyo"; Stnex[152, 2] = "nnjo"; Stnex[152, 3] = "nnzyo"; Stnex[152, 4] = "njyo"; Stnex[152, 5] = "njo"; Stnex[152, 6] = "nzyo";

            Stntex[33, 1] = "nnjjyi"; Stntex[33, 2] = "nnzzyi"; Stntex[33, 3] = "nnltujyi"; Stntex[33, 4] = "nnltuzyi"; Stntex[33, 5] = "njjyi"; Stntex[33, 6] = "nzzyi"; Stntex[33, 7] = "nltujyi"; Stntex[33, 8] = "nltuzyi";
            Stntex[34, 1] = "nnjjye"; Stntex[34, 2] = "nnjje"; Stntex[34, 3] = "nnzzye"; Stntex[34, 4] = "nnltujye"; Stntex[34, 5] = "nnltuje"; Stntex[34, 6] = "nnltuzye"; Stntex[34, 7] = "njjye"; Stntex[34, 8] = "njje"; Stntex[34, 9] = "nzzye"; Stntex[34, 10] = "nltujye"; Stntex[34, 11] = "nltuje"; Stntex[34, 12] = "nltuzye";
            Stntex[35, 1] = "nnjjya"; Stntex[35, 2] = "nnjja"; Stntex[35, 3] = "nnzzya"; Stntex[35, 4] = "nnltujya"; Stntex[35, 5] = "nnltuja"; Stntex[35, 6] = "nnltuzya"; Stntex[35, 7] = "njjya"; Stntex[35, 8] = "njja"; Stntex[35, 9] = "nzzya"; Stntex[35, 10] = "nltujya"; Stntex[35, 11] = "nltuja"; Stntex[35, 12] = "nltuzya";
            Stntex[36, 1] = "nnjjyu"; Stntex[36, 2] = "nnjju"; Stntex[36, 3] = "nnzzyu"; Stntex[36, 4] = "nnltujyu"; Stntex[36, 5] = "nnltuju"; Stntex[36, 6] = "nnltuzyu"; Stntex[36, 7] = "njjyu"; Stntex[36, 8] = "njju"; Stntex[36, 9] = "nzzyu"; Stntex[36, 10] = "nltujyu"; Stntex[36, 11] = "nltuju"; Stntex[36, 12] = "nltuzyu";
            Stntex[37, 1] = "nnjjyo"; Stntex[37, 2] = "nnjjo"; Stntex[37, 3] = "nnzzyo"; Stntex[37, 4] = "nnltujyo"; Stntex[37, 5] = "nnltujo"; Stntex[37, 6] = "nnltuzyo"; Stntex[37, 7] = "njjyo"; Stntex[37, 8] = "njjo"; Stntex[37, 9] = "nzzyo"; Stntex[37, 10] = "nltujyo"; Stntex[37, 11] = "nltujo"; Stntex[37, 12] = "nltuzyo";

            Stntex[150, 1] = "nnjjyi"; Stntex[150, 2] = "nnzzyi"; Stntex[150, 3] = "nnltujyi"; Stntex[150, 4] = "nnltuzyi"; Stntex[150, 5] = "njjyi"; Stntex[150, 6] = "nzzyi"; Stntex[150, 7] = "nltujyi"; Stntex[150, 8] = "nltuzyi";
            Stntex[151, 1] = "nnjjye"; Stntex[151, 2] = "nnjje"; Stntex[151, 3] = "nnzzye"; Stntex[151, 4] = "nnltujye"; Stntex[151, 5] = "nnltuje"; Stntex[151, 6] = "nnltuzye"; Stntex[151, 7] = "njjye"; Stntex[151, 8] = "njje"; Stntex[151, 9] = "nzzye"; Stntex[151, 10] = "nltujye"; Stntex[151, 11] = "nltuje"; Stntex[151, 12] = "nltuzye";
            Stntex[152, 1] = "nnjjya"; Stntex[152, 2] = "nnjja"; Stntex[152, 3] = "nnzzya"; Stntex[152, 4] = "nnltujya"; Stntex[152, 5] = "nnltuja"; Stntex[152, 6] = "nnltuzya"; Stntex[152, 7] = "njjya"; Stntex[152, 8] = "njja"; Stntex[152, 9] = "nzzya"; Stntex[152, 10] = "nltujya"; Stntex[152, 11] = "nltuja"; Stntex[152, 12] = "nltuzya";
            Stntex[153, 1] = "nnjjyu"; Stntex[153, 2] = "nnjju"; Stntex[153, 3] = "nnzzyu"; Stntex[153, 4] = "nnltujyu"; Stntex[153, 5] = "nnltuju"; Stntex[153, 6] = "nnltuzyu"; Stntex[153, 7] = "njjyu"; Stntex[153, 8] = "njju"; Stntex[153, 9] = "nzzyu"; Stntex[153, 10] = "nltujyu"; Stntex[153, 11] = "nltuju"; Stntex[153, 12] = "nltuzyu";
            Stntex[154, 1] = "nnjjyo"; Stntex[154, 2] = "nnjjo"; Stntex[154, 3] = "nnzzyo"; Stntex[154, 4] = "nnltujyo"; Stntex[154, 5] = "nnltujo"; Stntex[154, 6] = "nnltuzyo"; Stntex[154, 7] = "njjyo"; Stntex[154, 8] = "njjo"; Stntex[154, 9] = "nzzyo"; Stntex[154, 10] = "nltujyo"; Stntex[154, 11] = "nltujo"; Stntex[154, 12] = "nltuzyo";

            if (nn == 1)
            {
                Stnex[32, 1] = "njyi"; Stnex[32, 2] = "nzyi"; Stnex[32, 4] = "nnjyi"; Stnex[32, 5] = "nnzyi";
                Stnex[33, 1] = "njye"; Stnex[33, 2] = "nje"; Stnex[33, 3] = "nzye"; Stnex[33, 4] = "nnjye"; Stnex[33, 5] = "nnje"; Stnex[33, 6] = "nnzye";
                Stnex[34, 1] = "njya"; Stnex[34, 2] = "nja"; Stnex[34, 3] = "nzya"; Stnex[34, 4] = "nnjya"; Stnex[34, 5] = "nnja"; Stnex[34, 6] = "nnzya";
                Stnex[35, 1] = "njyu"; Stnex[35, 2] = "nju"; Stnex[35, 3] = "nzyu"; Stnex[35, 4] = "nnjyu"; Stnex[35, 5] = "nnju"; Stnex[35, 6] = "nnzyu";
                Stnex[36, 1] = "njyo"; Stnex[36, 2] = "njo"; Stnex[36, 3] = "nzyo"; Stnex[36, 4] = "nnjyo"; Stnex[36, 5] = "nnjo"; Stnex[36, 6] = "nnzyo";

                Stnex[148, 1] = "njyi"; Stnex[148, 2] = "nzyi"; Stnex[148, 4] = "nnjyi"; Stnex[148, 5] = "nnzyi";
                Stnex[149, 1] = "njye"; Stnex[149, 2] = "nje"; Stnex[149, 3] = "nzye"; Stnex[149, 4] = "nnjye"; Stnex[149, 5] = "nnje"; Stnex[149, 6] = "nnzye";
                Stnex[150, 1] = "njya"; Stnex[150, 2] = "nja"; Stnex[150, 3] = "nzya"; Stnex[150, 4] = "nnjya"; Stnex[150, 5] = "nnja"; Stnex[150, 6] = "nnzya";
                Stnex[151, 1] = "njyu"; Stnex[151, 2] = "nju"; Stnex[151, 3] = "nzyu"; Stnex[151, 4] = "nnjyu"; Stnex[151, 5] = "nnju"; Stnex[151, 6] = "nnzyu";
                Stnex[152, 1] = "njyo"; Stnex[152, 2] = "njo"; Stnex[152, 3] = "nzyo"; Stnex[152, 4] = "nnjyo"; Stnex[152, 5] = "nnjo"; Stnex[152, 6] = "nnzyo";

                Stntex[33, 1] = "njjyi"; Stntex[33, 2] = "nzzyi"; Stntex[33, 3] = "nltujyi"; Stntex[33, 4] = "nltuzyi"; Stntex[33, 5] = "nnjjyi"; Stntex[33, 6] = "nnzzyi"; Stntex[33, 7] = "nnltujyi"; Stntex[33, 8] = "nnltuzyi";
                Stntex[34, 1] = "njjye"; Stntex[34, 2] = "njje"; Stntex[34, 3] = "nzzye"; Stntex[34, 4] = "nltujye"; Stntex[34, 5] = "nltuje"; Stntex[34, 6] = "nltuzye"; Stntex[34, 7] = "nnjjye"; Stntex[34, 8] = "nnjje"; Stntex[34, 9] = "nnzzye"; Stntex[34, 10] = "nnltujye"; Stntex[34, 11] = "nnltuje"; Stntex[34, 12] = "nnltuzye";
                Stntex[35, 1] = "njjya"; Stntex[35, 2] = "njja"; Stntex[35, 3] = "nzzya"; Stntex[35, 4] = "nltujya"; Stntex[35, 5] = "nltuja"; Stntex[35, 6] = "nltuzya"; Stntex[35, 7] = "nnjjya"; Stntex[35, 8] = "nnjja"; Stntex[35, 9] = "nnzzya"; Stntex[35, 10] = "nnltujya"; Stntex[35, 11] = "nnltuja"; Stntex[35, 12] = "nnltuzya";
                Stntex[36, 1] = "njjyu"; Stntex[36, 2] = "njju"; Stntex[36, 3] = "nzzyu"; Stntex[36, 4] = "nltujyu"; Stntex[36, 5] = "nltuju"; Stntex[36, 6] = "nltuzyu"; Stntex[36, 7] = "nnjjyu"; Stntex[36, 8] = "nnjju"; Stntex[36, 9] = "nnzzyu"; Stntex[36, 10] = "nnltujyu"; Stntex[36, 11] = "nnltuju"; Stntex[36, 12] = "nnltuzyu";
                Stntex[37, 1] = "njjyo"; Stntex[37, 2] = "njjo"; Stntex[37, 3] = "nzzyo"; Stntex[37, 4] = "nltujyo"; Stntex[37, 5] = "nltujo"; Stntex[37, 6] = "nltuzyo"; Stntex[37, 7] = "nnjjyo"; Stntex[37, 8] = "nnjjo"; Stntex[37, 9] = "nnzzyo"; Stntex[37, 10] = "nnltujyo"; Stntex[37, 11] = "nnltujo"; Stntex[37, 12] = "nnltuzyo";

                Stntex[150, 1] = "njjyi"; Stntex[150, 2] = "nzzyi"; Stntex[150, 3] = "nltujyi"; Stntex[150, 4] = "nltuzyi"; Stntex[150, 5] = "nnjjyi"; Stntex[150, 6] = "nnzzyi"; Stntex[150, 7] = "nnltujyi"; Stntex[150, 8] = "nnltuzyi";
                Stntex[151, 1] = "njjye"; Stntex[151, 2] = "njje"; Stntex[151, 3] = "nzzye"; Stntex[151, 4] = "nltujye"; Stntex[151, 5] = "nltuje"; Stntex[151, 6] = "nltuzye"; Stntex[151, 7] = "nnjjye"; Stntex[151, 8] = "nnjje"; Stntex[151, 9] = "nnzzye"; Stntex[151, 10] = "nnltujye"; Stntex[151, 11] = "nnltuje"; Stntex[151, 12] = "nnltuzye";
                Stntex[152, 1] = "njjya"; Stntex[152, 2] = "njja"; Stntex[152, 3] = "nzzya"; Stntex[152, 4] = "nltujya"; Stntex[152, 5] = "nltuja"; Stntex[152, 6] = "nltuzya"; Stntex[152, 7] = "nnjjya"; Stntex[152, 8] = "nnjja"; Stntex[152, 9] = "nnzzya"; Stntex[152, 10] = "nnltujya"; Stntex[152, 11] = "nnltuja"; Stntex[152, 12] = "nnltuzya";
                Stntex[153, 1] = "njjyu"; Stntex[153, 2] = "njju"; Stntex[153, 3] = "nzzyu"; Stntex[153, 4] = "nltujyu"; Stntex[153, 5] = "nltuju"; Stntex[153, 6] = "nltuzyu"; Stntex[153, 7] = "nnjjyu"; Stntex[153, 8] = "nnjju"; Stntex[153, 9] = "nnzzyu"; Stntex[153, 10] = "nnltujyu"; Stntex[153, 11] = "nnltuju"; Stntex[153, 12] = "nnltuzyu";
                Stntex[154, 1] = "njjyo"; Stntex[154, 2] = "njjo"; Stntex[154, 3] = "nzzyo"; Stntex[154, 4] = "nltujyo"; Stntex[154, 5] = "nltujo"; Stntex[154, 6] = "nltuzyo"; Stntex[154, 7] = "nnjjyo"; Stntex[154, 8] = "nnjjo"; Stntex[154, 9] = "nnzzyo"; Stntex[154, 10] = "nnltujyo"; Stntex[154, 11] = "nnltujo"; Stntex[154, 12] = "nnltuzyo";
            }
        }
        else if (ja == 3)
        {
            //ZYA ZYU ZYO
            Stex[33, 1] = "zyi"; Stex[33, 2] = "jyi"; Stex[33, 3] = "zili"; Stex[33, 4] = "jili";
            Stex[34, 1] = "zye"; Stex[34, 2] = "je"; Stex[34, 3] = "jye"; Stex[34, 4] = "zile"; Stex[34, 5] = "jile";
            Stex[35, 1] = "zya"; Stex[35, 2] = "ja"; Stex[35, 3] = "jya"; Stex[35, 4] = "zilya"; Stex[35, 5] = "jilya";
            Stex[36, 1] = "zyu"; Stex[36, 2] = "ju"; Stex[36, 3] = "jyu"; Stex[36, 4] = "zilyu"; Stex[36, 5] = "jilyu";
            Stex[37, 1] = "zyo"; Stex[37, 2] = "jo"; Stex[37, 3] = "jyo"; Stex[37, 4] = "zilyo"; Stex[37, 5] = "jilyo";

            Stex[155, 1] = "zyi"; Stex[155, 2] = "jyi"; Stex[155, 3] = "zili"; Stex[155, 4] = "jili";
            Stex[156, 1] = "zye"; Stex[156, 2] = "je"; Stex[156, 3] = "jye"; Stex[156, 4] = "zile"; Stex[156, 5] = "jile";
            Stex[157, 1] = "zya"; Stex[157, 2] = "ja"; Stex[157, 3] = "jya"; Stex[157, 4] = "zilya"; Stex[157, 5] = "jilya";
            Stex[158, 1] = "zyu"; Stex[158, 2] = "ju"; Stex[158, 3] = "jyu"; Stex[158, 4] = "zilyu"; Stex[158, 5] = "jilyu";
            Stex[159, 1] = "zyo"; Stex[159, 2] = "jo"; Stex[159, 3] = "jyo"; Stex[159, 4] = "zilyo"; Stex[159, 5] = "jilyo";

            Sttex[33, 1] = "zzyi"; Sttex[33, 2] = "jjyi"; Sttex[33, 3] = "ltuzyi"; Sttex[33, 4] = "ltujyi";
            Sttex[34, 1] = "zzye"; Sttex[34, 2] = "jje"; Sttex[34, 3] = "jjye"; Sttex[34, 4] = "ltuzye"; Sttex[34, 5] = "ltuje"; Sttex[34, 6] = "ltujye";
            Sttex[35, 1] = "zzya"; Sttex[35, 2] = "jja"; Sttex[35, 3] = "jjya"; Sttex[35, 4] = "ltuzya"; Sttex[35, 5] = "ltuja"; Sttex[35, 6] = "ltujya";
            Sttex[36, 1] = "zzyu"; Sttex[36, 2] = "jju"; Sttex[36, 3] = "jjyu"; Sttex[36, 4] = "ltuzyu"; Sttex[36, 5] = "ltuju"; Sttex[36, 6] = "ltujyu";
            Sttex[37, 1] = "zzyo"; Sttex[37, 2] = "jjo"; Sttex[37, 3] = "jjyo"; Sttex[37, 4] = "ltuzyo"; Sttex[37, 5] = "ltujo"; Sttex[37, 6] = "ltujyo";

            Sttex[150, 1] = "zzyi"; Sttex[150, 2] = "jjyi"; Sttex[150, 3] = "ltuzyi"; Sttex[150, 4] = "ltujyi";
            Sttex[151, 1] = "zzye"; Sttex[151, 2] = "jje"; Sttex[151, 3] = "jjye"; Sttex[151, 4] = "ltuzye"; Sttex[151, 5] = "ltuje"; Sttex[151, 6] = "ltujye";
            Sttex[152, 1] = "zzya"; Sttex[152, 2] = "jja"; Sttex[152, 3] = "jjya"; Sttex[152, 4] = "ltuzya"; Sttex[152, 5] = "ltuja"; Sttex[152, 6] = "ltujya";
            Sttex[153, 1] = "zzyu"; Sttex[153, 2] = "jju"; Sttex[153, 3] = "jjyu"; Sttex[153, 4] = "ltuzyu"; Sttex[153, 5] = "ltuju"; Sttex[153, 6] = "ltujyu";
            Sttex[154, 1] = "zzyo"; Sttex[154, 2] = "jjo"; Sttex[154, 3] = "jjyo"; Sttex[154, 4] = "ltuzyo"; Sttex[154, 5] = "ltujo"; Sttex[154, 6] = "ltujyo";

            Stnex[32, 1] = "nnzyi"; Stnex[32, 2] = "nnjyi"; Stnex[32, 3] = "nzyi"; Stnex[32, 4] = "njyi";
            Stnex[33, 1] = "nnzye"; Stnex[33, 2] = "nnje"; Stnex[33, 3] = "nnjye"; Stnex[33, 4] = "nzye"; Stnex[33, 5] = "nje"; Stnex[33, 6] = "njye";
            Stnex[34, 1] = "nnzya"; Stnex[34, 2] = "nnja"; Stnex[34, 3] = "nnjya"; Stnex[34, 4] = "nzya"; Stnex[34, 5] = "nja"; Stnex[34, 6] = "njya";
            Stnex[35, 1] = "nnzyu"; Stnex[35, 2] = "nnju"; Stnex[35, 3] = "nnjyu"; Stnex[35, 4] = "nzyu"; Stnex[35, 5] = "nju"; Stnex[35, 6] = "njyu";
            Stnex[36, 1] = "nnzyo"; Stnex[36, 2] = "nnjo"; Stnex[36, 3] = "nnjyo"; Stnex[36, 4] = "nzyo"; Stnex[36, 5] = "njo"; Stnex[36, 6] = "njyo";

            Stnex[148, 1] = "nnzyi"; Stnex[148, 2] = "nnjyi"; Stnex[148, 3] = "nzyi"; Stnex[148, 4] = "njyi";
            Stnex[149, 1] = "nnzye"; Stnex[149, 2] = "nnje"; Stnex[149, 3] = "nnjye"; Stnex[149, 4] = "nzye"; Stnex[149, 5] = "nje"; Stnex[149, 6] = "njye";
            Stnex[150, 1] = "nnzya"; Stnex[150, 2] = "nnja"; Stnex[150, 3] = "nnjya"; Stnex[150, 4] = "nzya"; Stnex[150, 5] = "nja"; Stnex[150, 6] = "njya";
            Stnex[151, 1] = "nnzyu"; Stnex[151, 2] = "nnju"; Stnex[151, 3] = "nnjyu"; Stnex[151, 4] = "nzyu"; Stnex[151, 5] = "nju"; Stnex[151, 6] = "njyu";
            Stnex[152, 1] = "nnzyo"; Stnex[152, 2] = "nnjo"; Stnex[152, 3] = "nnjyo"; Stnex[152, 4] = "nzyo"; Stnex[152, 5] = "njo"; Stnex[152, 6] = "njyo";

            Stntex[33, 1] = "nnzzyi"; Stntex[33, 2] = "nnjjyi"; Stntex[33, 3] = "nnltuzyi"; Stntex[33, 4] = "nnltujyi"; Stntex[33, 5] = "nzzyi"; Stntex[33, 6] = "njjyi"; Stntex[33, 7] = "nltuzyi"; Stntex[33, 8] = "nltujyi";
            Stntex[34, 1] = "nnzzye"; Stntex[34, 2] = "nnjje"; Stntex[34, 3] = "nnjjye"; Stntex[34, 4] = "nnltuzye"; Stntex[34, 5] = "nnltuje"; Stntex[34, 6] = "nnltujye"; Stntex[34, 7] = "nzzye"; Stntex[34, 8] = "njje"; Stntex[34, 9] = "njjye"; Stntex[34, 10] = "nltuzye"; Stntex[34, 11] = "nltuje"; Stntex[34, 12] = "nltujye";
            Stntex[35, 1] = "nnzzya"; Stntex[35, 2] = "nnjja"; Stntex[35, 3] = "nnjjya"; Stntex[35, 4] = "nnltuzya"; Stntex[35, 5] = "nnltuja"; Stntex[35, 6] = "nnltujya"; Stntex[35, 7] = "nzzya"; Stntex[35, 8] = "njja"; Stntex[35, 9] = "njjya"; Stntex[35, 10] = "nltuzya"; Stntex[35, 11] = "nltuja"; Stntex[35, 12] = "nltujya";
            Stntex[36, 1] = "nnzzyu"; Stntex[36, 2] = "nnjju"; Stntex[36, 3] = "nnjjyu"; Stntex[36, 4] = "nnltuzyu"; Stntex[36, 5] = "nnltuju"; Stntex[36, 6] = "nnltujyu"; Stntex[36, 7] = "nzzyu"; Stntex[36, 8] = "njju"; Stntex[36, 9] = "njjyu"; Stntex[36, 10] = "nltuzyu"; Stntex[36, 11] = "nltuju"; Stntex[36, 12] = "nltujyu";
            Stntex[37, 1] = "nnzzyo"; Stntex[37, 2] = "nnjjo"; Stntex[37, 3] = "nnjjyo"; Stntex[37, 4] = "nnltuzyo"; Stntex[37, 5] = "nnltujo"; Stntex[37, 6] = "nnltujyo"; Stntex[37, 7] = "nzzyo"; Stntex[37, 8] = "njjo"; Stntex[37, 9] = "njjyo"; Stntex[37, 10] = "nltuzyo"; Stntex[37, 11] = "nltujo"; Stntex[37, 12] = "nltujyo";

            Stntex[150, 1] = "nnzzyi"; Stntex[150, 2] = "nnjjyi"; Stntex[150, 3] = "nnltuzyi"; Stntex[150, 4] = "nnltujyi"; Stntex[150, 5] = "nzzyi"; Stntex[150, 6] = "njjyi"; Stntex[150, 7] = "nltuzyi"; Stntex[150, 8] = "nltujyi";
            Stntex[151, 1] = "nnzzye"; Stntex[151, 2] = "nnjje"; Stntex[151, 3] = "nnjjye"; Stntex[151, 4] = "nnltuzye"; Stntex[151, 5] = "nnltuje"; Stntex[151, 6] = "nnltujye"; Stntex[151, 7] = "nzzye"; Stntex[151, 8] = "njje"; Stntex[151, 9] = "njjye"; Stntex[151, 10] = "nltuzye"; Stntex[151, 11] = "nltuje"; Stntex[151, 12] = "nltujye";
            Stntex[152, 1] = "nnzzya"; Stntex[152, 2] = "nnjja"; Stntex[152, 3] = "nnjjya"; Stntex[152, 4] = "nnltuzya"; Stntex[152, 5] = "nnltuja"; Stntex[152, 6] = "nnltujya"; Stntex[152, 7] = "nzzya"; Stntex[152, 8] = "njja"; Stntex[152, 9] = "njjya"; Stntex[152, 10] = "nltuzya"; Stntex[152, 11] = "nltuja"; Stntex[152, 12] = "nltujya";
            Stntex[153, 1] = "nnzzyu"; Stntex[153, 2] = "nnjju"; Stntex[153, 3] = "nnjjyu"; Stntex[153, 4] = "nnltuzyu"; Stntex[153, 5] = "nnltuju"; Stntex[153, 6] = "nnltujyu"; Stntex[153, 7] = "nzzyu"; Stntex[153, 8] = "njju"; Stntex[153, 9] = "njjyu"; Stntex[153, 10] = "nltuzyu"; Stntex[153, 11] = "nltuju"; Stntex[153, 12] = "nltujyu";
            Stntex[154, 1] = "nnzzyo"; Stntex[154, 2] = "nnjjo"; Stntex[154, 3] = "nnjjyo"; Stntex[154, 4] = "nnltuzyo"; Stntex[154, 5] = "nnltujo"; Stntex[154, 6] = "nnltujyo"; Stntex[154, 7] = "nzzyo"; Stntex[154, 8] = "njjo"; Stntex[154, 9] = "njjyo"; Stntex[154, 10] = "nltuzyo"; Stntex[154, 11] = "nltujo"; Stntex[154, 12] = "nltujyo";

            if (nn == 1)
            {
                Stnex[32, 1] = "nzyi"; Stnex[32, 2] = "njyi"; Stnex[32, 3] = "nnzyi"; Stnex[32, 4] = "nnjyi";
                Stnex[33, 1] = "nzye"; Stnex[33, 2] = "nje"; Stnex[33, 3] = "njye"; Stnex[33, 4] = "nnzye"; Stnex[33, 5] = "nnje"; Stnex[33, 6] = "nnjye";
                Stnex[34, 1] = "nzya"; Stnex[34, 2] = "nja"; Stnex[34, 3] = "njya"; Stnex[34, 4] = "nnzya"; Stnex[34, 5] = "nnja"; Stnex[34, 6] = "nnjya";
                Stnex[35, 1] = "nzyu"; Stnex[35, 2] = "nju"; Stnex[35, 3] = "njyu"; Stnex[35, 4] = "nnzyu"; Stnex[35, 5] = "nnju"; Stnex[35, 6] = "nnjyu";
                Stnex[36, 1] = "nzyo"; Stnex[36, 2] = "njo"; Stnex[36, 3] = "njyo"; Stnex[36, 4] = "nnzyo"; Stnex[36, 5] = "nnjo"; Stnex[36, 6] = "nnjyo";

                Stnex[148, 1] = "nzyi"; Stnex[148, 2] = "njyi"; Stnex[148, 3] = "nnzyi"; Stnex[148, 4] = "nnjyi";
                Stnex[149, 1] = "nzye"; Stnex[149, 2] = "nje"; Stnex[149, 3] = "njye"; Stnex[149, 4] = "nnzye"; Stnex[149, 5] = "nnje"; Stnex[149, 6] = "nnjye";
                Stnex[150, 1] = "nzya"; Stnex[150, 2] = "nja"; Stnex[150, 3] = "njya"; Stnex[150, 4] = "nnzya"; Stnex[150, 5] = "nnja"; Stnex[150, 6] = "nnjya";
                Stnex[151, 1] = "nzyu"; Stnex[151, 2] = "nju"; Stnex[151, 3] = "njyu"; Stnex[151, 4] = "nnzyu"; Stnex[151, 5] = "nnju"; Stnex[151, 6] = "nnjyu";
                Stnex[152, 1] = "nzyo"; Stnex[152, 2] = "njo"; Stnex[152, 3] = "njyo"; Stnex[152, 4] = "nnzyo"; Stnex[152, 5] = "nnjo"; Stnex[152, 6] = "nnjyo";

                Stntex[33, 1] = "nzzyi"; Stntex[33, 2] = "njjyi"; Stntex[33, 3] = "nltuzyi"; Stntex[33, 4] = "nltujyi"; Stntex[33, 5] = "nnzzyi"; Stntex[33, 6] = "nnjjyi"; Stntex[33, 7] = "nnltuzyi"; Stntex[33, 8] = "nnltujyi";
                Stntex[34, 1] = "nzzye"; Stntex[34, 2] = "njje"; Stntex[34, 3] = "njjye"; Stntex[34, 4] = "nltuzye"; Stntex[34, 5] = "nltuje"; Stntex[34, 6] = "nltujye"; Stntex[34, 7] = "nnzzye"; Stntex[34, 8] = "nnjje"; Stntex[34, 9] = "nnjjye"; Stntex[34, 10] = "nnltuzye"; Stntex[34, 11] = "nnltuje"; Stntex[34, 12] = "nnltujye";
                Stntex[35, 1] = "nzzya"; Stntex[35, 2] = "njja"; Stntex[35, 3] = "njjya"; Stntex[35, 4] = "nltuzya"; Stntex[35, 5] = "nltuja"; Stntex[35, 6] = "nltujya"; Stntex[35, 7] = "nnzzya"; Stntex[35, 8] = "nnjja"; Stntex[35, 9] = "nnjjya"; Stntex[35, 10] = "nnltuzya"; Stntex[35, 11] = "nnltuja"; Stntex[35, 12] = "nnltujya";
                Stntex[36, 1] = "nzzyu"; Stntex[36, 2] = "njju"; Stntex[36, 3] = "njjyu"; Stntex[36, 4] = "nltuzyu"; Stntex[36, 5] = "nltuju"; Stntex[36, 6] = "nltujyu"; Stntex[36, 7] = "nnzzyu"; Stntex[36, 8] = "nnjju"; Stntex[36, 9] = "nnjjyu"; Stntex[36, 10] = "nnltuzyu"; Stntex[36, 11] = "nnltuju"; Stntex[36, 12] = "nnltujyu";
                Stntex[37, 1] = "nzzyo"; Stntex[37, 2] = "njjo"; Stntex[37, 3] = "njjyo"; Stntex[37, 4] = "nltuzyo"; Stntex[37, 5] = "nltujo"; Stntex[37, 6] = "nltujyo"; Stntex[37, 7] = "nnzzyo"; Stntex[37, 8] = "nnjjo"; Stntex[37, 9] = "nnjjyo"; Stntex[37, 10] = "nnltuzyo"; Stntex[37, 11] = "nnltujo"; Stntex[37, 12] = "nnltujyo";

                Stntex[150, 1] = "nzzyi"; Stntex[150, 2] = "njjyi"; Stntex[150, 3] = "nltuzyi"; Stntex[150, 4] = "nltujyi"; Stntex[150, 5] = "nnzzyi"; Stntex[150, 6] = "nnjjyi"; Stntex[150, 7] = "nnltuzyi"; Stntex[150, 8] = "nnltujyi";
                Stntex[151, 1] = "nzzye"; Stntex[151, 2] = "njje"; Stntex[151, 3] = "njjye"; Stntex[151, 4] = "nltuzye"; Stntex[151, 5] = "nltuje"; Stntex[151, 6] = "nltujye"; Stntex[151, 7] = "nnzzye"; Stntex[151, 8] = "nnjje"; Stntex[151, 9] = "nnjjye"; Stntex[151, 10] = "nnltuzye"; Stntex[151, 11] = "nnltuje"; Stntex[151, 12] = "nnltujye";
                Stntex[152, 1] = "nzzya"; Stntex[152, 2] = "njja"; Stntex[152, 3] = "njjya"; Stntex[152, 4] = "nltuzya"; Stntex[152, 5] = "nltuja"; Stntex[152, 6] = "nltujya"; Stntex[152, 7] = "nnzzya"; Stntex[152, 8] = "nnjja"; Stntex[152, 9] = "nnjjya"; Stntex[152, 10] = "nnltuzya"; Stntex[152, 11] = "nnltuja"; Stntex[152, 12] = "nnltujya";
                Stntex[153, 1] = "nzzyu"; Stntex[153, 2] = "njju"; Stntex[153, 3] = "njjyu"; Stntex[153, 4] = "nltuzyu"; Stntex[153, 5] = "nltuju"; Stntex[153, 6] = "nltujyu"; Stntex[153, 7] = "nnzzyu"; Stntex[153, 8] = "nnjju"; Stntex[153, 9] = "nnjjyu"; Stntex[153, 10] = "nnltuzyu"; Stntex[153, 11] = "nnltuju"; Stntex[153, 12] = "nnltujyu";
                Stntex[154, 1] = "nzzyo"; Stntex[154, 2] = "njjo"; Stntex[154, 3] = "njjyo"; Stntex[154, 4] = "nltuzyo"; Stntex[154, 5] = "nltujo"; Stntex[154, 6] = "nltujyo"; Stntex[154, 7] = "nnzzyo"; Stntex[154, 8] = "nnjjo"; Stntex[154, 9] = "nnjjyo"; Stntex[154, 10] = "nnltuzyo"; Stntex[154, 11] = "nnltujo"; Stntex[154, 12] = "nnltujyo";
            }
        }
    }
    public override List<string> MakeSearchStr(string input_textk, int str_posk)
    {
        // 入力回数をカウント
        //keyPressCount++;
        //Debug.Log("キーが押された回数: " + keyPressCount);

        int i, k;
        SearchStrs.Clear();
        string subtext;
        bool exflg = false;
        string strbuf = string.Empty;
        List<string> return_str = new List<string>();
        return_str.Clear();

        strbuf = input_textk.Substring(str_posk, 1);

        int restword = input_textk.Length - str_posk;
        if (strbuf == "ん" || strbuf == "ン") // 一文字目が「ん」
        {
            //「ん」「っ」付き４文字 １２候補  1-11:4文字 12-21:3文字 22-29:2文字 30-33:1文字
            if (!exflg && restword >= 4)
            {
                subtext = input_textk.Substring(str_posk, 4);
                for (i = 0; i < STNTEX_COL; i++)
                {
                    if (subtext == Stntex[i, 0])
                    {
                        for (k = 1; k < STNTEX_ROW; k++)
                        {
                            if (string.IsNullOrEmpty(Stntex[i, k])) continue;
                            SearchStrs.Add(new TagSearchStrings(Stntex[i, k], 4));
                            return_str.Add(Stntex[i, k]);
                        }
                        exflg = true;
                        break;
                    }
                }
            }

            //「ん」「っ」付き３文字 １０候補
            if (!exflg && restword >= 3)
            {
                subtext = input_textk.Substring(str_posk, 3);
                for (i = 0; i < STNT_COL; i++)
                {
                    if (subtext == Stnt[i, 0])
                    {
                        for (k = 1; k < STNT_ROW; k++)
                        {
                            if (string.IsNullOrEmpty(Stnt[i, k])) continue;
                            SearchStrs.Add(new TagSearchStrings(Stnt[i, k], 3));
                            return_str.Add(Stnt[i, k]);
                        }
                        exflg = true;
                        break;
                    }
                }
            }

            //「ん」付き３文字 ６候補
            if (!exflg && restword >= 3)
            {
                subtext = input_textk.Substring(str_posk, 3);
                for (i = 0; i < STNEX_COL; i++)
                {
                    if (subtext == Stnex[i, 0])
                    {
                        for (k = 1; k < STNEX_ROW; k++)
                        {
                            if (string.IsNullOrEmpty(Stntex[i, k])) continue;
                            SearchStrs.Add(new TagSearchStrings(Stnex[i, k], 3));
                            return_str.Add(Stnex[i, k]);
                        }
                        exflg = true;
                        break;
                    }
                }
            }

            //「ん」付き２文字 ８候補
            if (!exflg && restword >= 2)
            {
                subtext = input_textk.Substring(str_posk, 2);
                for (i = 0; i < STN_COL; i++)
                {
                    if (subtext == Stn[i, 0])
                    {
                        for (k = 1; k < STN_ROW; k++)
                        {
                            if (string.IsNullOrEmpty(Stn[i, k])) continue;
                            SearchStrs.Add(new TagSearchStrings(Stn[i, k], 2));
                            return_str.Add(Stn[i, k]);
                        }
                        exflg = true;
                        break;
                    }
                }
            }
            if (!exflg)
            {
                SearchStrs.Add(new TagSearchStrings("nn", 1));
                SearchStrs.Add(new TagSearchStrings("xn", 1));
                return_str.Add("nn");
                return_str.Add("xn");
            }
        }
        else if (strbuf == "っ" || strbuf == "ッ") // 一文字目が「っ」
        {
            //「っ」付き３文字 ６候補 0-11:4文字 12-21:3文字 22-29:2文字 30-33:1文字
            if (!exflg && restword >= 3)
            {
                subtext = input_textk.Substring(str_posk, 3);
                for (i = 0; i < STTEX_COL; i++)
                {
                    if (subtext == Sttex[i, 0])
                    {
                        for (k = 1; k < STTEX_ROW; k++)
                        {
                            if (string.IsNullOrEmpty(Sttex[i, k])) continue;
                            SearchStrs.Add(new TagSearchStrings(Sttex[i, k], 3));
                            return_str.Add(Sttex[i, k]);
                        }
                        exflg = true;
                        break;
                    }
                }
            }

            //「っ」付き２文字 ５候補
            if (!exflg && restword >= 2)
            {
                subtext = input_textk.Substring(str_posk, 2);
                for (i = 0; i < STT_COL; i++)
                {
                    if (subtext == Stt[i, 0])
                    {
                        for (k = 1; k < STT_ROW; k++)
                        {
                            if (string.IsNullOrEmpty(Stt[i, k])) continue;
                            SearchStrs.Add(new TagSearchStrings(Stt[i, k], 2));
                            return_str.Add(Stt[i, k]);
                        }
                        exflg = true;
                        break;
                    }
                }
            }
            if (!exflg)
            {
                SearchStrs.Add(new TagSearchStrings("ltu", 1));
                SearchStrs.Add(new TagSearchStrings("xtu", 1));
                SearchStrs.Add(new TagSearchStrings("ltsu", 1));
                return_str.Add("ltu");
                return_str.Add("xtu");
                return_str.Add("ltsu");
            }
        }
        else  // その他
        {
            //２文字 ５候補  0-11:4文字 12-21:3文字 22-29:2文字 30-33:1文字
            if (!exflg && restword >= 2)
            {
                subtext = input_textk.Substring(str_posk, 2);
                for (i = 0; i < STEX_COL; i++)
                {
                    if (subtext == Stex[i, 0])
                    {
                        for (k = 1; k < STEX_ROW; k++)
                        {
                            if (string.IsNullOrEmpty(Stex[i, k])) continue;
                            SearchStrs.Add(new TagSearchStrings(Stex[i, k], 2));
                            return_str.Add(Stex[i, k]);
                        }
                        exflg = true;
                        break;
                    }
                }
            }
            if (!exflg)
            {
                subtext = input_textk.Substring(str_posk, 1);
                for (i = 0; i < ST_COL; i++)
                {
                    if (subtext == St[i, 0])
                    {
                        for (k = 1; k < ST_ROW; k++)
                        {
                            if (string.IsNullOrEmpty(St[i, k])) continue;
                            SearchStrs.Add(new TagSearchStrings(St[i, k], 1));
                            return_str.Add(St[i, k]);
                        }
                        break;
                    }
                }
            }
        }

        // 検索配列のローマ表示テキストにおける先頭文字位置(入力時の文字が表示している例と違う場合の修正用)
        //for (i = 0; i < CRomaPreservStr.SEARCH_STR_LENGHT; i++)
        foreach (var search_str in SearchStrs)
        {
            if (!String.IsNullOrEmpty(search_str.SearchStr))
            {
                // 現在１番手候補の文字長さ保存
                EnglishLen = search_str.SearchStr.Length;
                break;
            }
        }
        //return return_str;
        return new List<string>(); // 適宜変更
    }
    public override string MakeInputText(string input_textk)
    {
        string eword = string.Empty;
        string subtext = input_textk;
        string strbuf;
        bool exflg;
        int err = 0;
        int textlength;

        while (!String.IsNullOrEmpty(subtext))
        {
            if (++err > 700)
            {
                Debug.Log("SubTextが読み取れません");
                return null;
            }

            textlength = subtext.Length;
            exflg = false;

            strbuf = subtext.Substring(0, 1);
            if (strbuf == "ん" || strbuf == "ン") // 一文字目が「ん」
            {
                //「ん」「っ」付き４文字
                if (!exflg && textlength >= 4)
                {
                    for (int k = 0; k < STNTEX_COL; k++)
                    {
                        if (subtext.Substring(0, 4) == Stntex[k, 0])
                        {
                            eword += Stntex[k, 1];
                            // subtextを4文字目から最後まで切り取る(最初の４文字を切り捨てる)
                            subtext = subtext.Substring(4, textlength - 4);
                            exflg = true;
                            break;
                        }
                    }
                }

                //「ん」「っ」付き３文字
                if (!exflg && textlength >= 3)
                {
                    for (int k = 0; k < STNT_COL; k++)
                    {
                        if (subtext.Substring(0, 3) == Stnt[k, 0])
                        {
                            eword += Stnt[k, 1];
                            // subtextを3文字目から最後まで切り取る(最初の3文字を切り捨てる)
                            subtext = subtext.Substring(3, textlength - 3);
                            exflg = true;
                            break;
                        }
                    }
                }

                //「ん」付き３文字
                if (!exflg && textlength >= 3)
                {
                    for (int k = 0; k < STNEX_COL; k++)
                    {
                        if (subtext.Substring(0, 3) == Stnex[k, 0])
                        {
                            eword += Stnex[k, 1];
                            // subtextを3文字目から最後まで切り取る(最初の3文字を切り捨てる)
                            subtext = subtext.Substring(3, textlength - 3);
                            exflg = true;
                            break;
                        }
                    }
                }

                //「ん」付き２文字
                if (!exflg && textlength >= 2)
                {
                    for (int k = 0; k < STN_COL; k++)
                    {
                        if (subtext.Substring(0, 2) == Stn[k, 0])
                        {
                            eword += Stn[k, 1];
                            // subtextを2文字目から最後まで切り取る(最初の2文字を切り捨てる)
                            subtext = subtext.Substring(2, textlength - 2);
                            exflg = true;
                            break;
                        }
                    }
                }
                if (!exflg)
                {
                    eword += "nn";
                    // subtextを1文字目から最後まで切り取る(最初の1文字を切り捨てる)
                    subtext = subtext.Substring(1, textlength - 1);
                }
            }
            else if (strbuf == "っ" || strbuf == "ッ") // 一文字目が「っ」
            {
                ////「っ」付き３文字
                if (!exflg && textlength >= 3)
                {
                    for (int k = 0; k < STTEX_COL; k++)
                    {
                        if (subtext.Substring(0, 3) == Sttex[k, 0])
                        {
                            eword += Sttex[k, 1];
                            // subtextを3文字目から最後まで切り取る(最初の3文字を切り捨てる)
                            subtext = subtext.Substring(3, textlength - 3);
                            exflg = true;
                            break;
                        }
                    }
                }

                //「っ」付き２文字
                if (!exflg && textlength >= 2)
                {
                    for (int k = 0; k < STT_COL; k++)
                    {
                        if (subtext.Substring(0, 2) == Stt[k, 0])
                        {
                            eword += Stt[k, 1];
                            // subtextを2文字目から最後まで切り取る(最初の2文字を切り捨てる)
                            subtext = subtext.Substring(2, textlength - 2);
                            exflg = true;
                            break;
                        }
                    }
                }

                if (!exflg)
                {
                    eword += "ltu";
                    //　subtextを1文字目から最後まで切り取る(最初の1文字を切り捨てる)
                    subtext = subtext.Substring(1, textlength - 1);
                }
            }
            else // その他
            {
                //２文字
                if (!exflg && textlength >= 2)
                {
                    for (int k = 0; k < STEX_COL; k++)
                    {
                        // subtextを先頭から2文字切り取る
                        if (subtext.Substring(0, 2) == Stex[k, 0])
                        {
                            eword += Stex[k, 1];
                            // subtextを2文字目から最後まで切り取る(最初の2文字を切り捨てる)
                            subtext = subtext.Substring(2, textlength - 2);
                            exflg = true;
                            break;
                        }
                    }
                }
                //１文字
                if (!exflg)
                {
                    for (int k = 0; k < ST_COL; k++)
                    {
                        if (subtext.Substring(0, 1) == St[k, 0])
                        {
                            if (St[k, 0] == " " || St[k, 0] == "?@")
                            {
                                eword += " ";
                            }
                            else
                            {
                                eword += St[k, 1];
                            }
                            // subtextを1文字目から最後まで切り取る(最初の1文字を切り捨てる)
                            subtext = subtext.Substring(1, textlength - 1);
                            exflg = true;
                            break;
                        }
                    }
                }
            }
        }

        NowInputText = eword;

        return eword;
    }
    public bool StrChange(ref string input_texte, ref int english_len,
        string strbuf, List<TagSearchStrings> search_strs, int english_pos)
    {
        int e_len = english_len;
        if (input_texte.Substring(english_pos, strbuf.Length) != strbuf)
        {
            foreach (var search_str in search_strs)
            {
                if (string.IsNullOrEmpty(search_str.SearchStr)) continue;
                // 文字を切り取りすぎないようなチェックする(追加点)
                if (search_str.SearchStr.Length < strbuf.Length) continue;
                if (search_str.SearchStr.Substring(0, strbuf.Length) == strbuf)
                {
                    int textlength = input_texte.Length;

                    input_texte =
                            // 問題文の始めからsearch_strの手前まで切り取ったもの
                            input_texte.Substring(0, english_pos) +
                            search_str.SearchStr +
                            // 問題文の始めからsearch_strの手前まで切り取ったもの
                            input_texte.Substring(
                                english_pos + e_len,
                                textlength - (english_pos + e_len)
                            );
                    english_len = search_str.SearchStr.Length;
                    return true;
                }
            }
        }
        return false;
    }
    protected override bool InputJudge(string[] input_textk, string key, bool shift)
    {
        // エンターキーやその他除外したいキーをチェック
        if (key == "Return" || key == "Escape" || key == "Space") // 除外したいキーを追加
        {
            // 除外するキーの場合はカウントしない
            return false;
        }
          // キーが押されたときにカウントを増やす
        keyPressCount++;
        string temp_str = GetKeyString(key, shift);
        Debug.Log("入力されたキー:" + temp_str);
        Debug.Log("入力回数:" + keyPressCount);

        if (temp_str == null) return false;
         // StrBufに追加する前に、temp_strを保存
        //string lastInputChar = temp_str;
        //StrBuf += lastInputChar;
        StrBuf += temp_str;
        //if (StrBuf.Length > 0) // StrBufが空でない場合
            //{
                //Debug.Log("入力されたキー: " + temp_str);
            //}
        bool agreeflg = false;
        foreach (var search_str in SearchStrs)
        {
            // 完全一致した場合(一文字終了)
            if (search_str.SearchStr == StrBuf)
            {
                if (1 < search_str.SearchStr.Length)
                {
                    // 表示文字と違う場合修正
                    StrChangedFlag = StrChange(ref NowInputText,
                        ref EnglishLen, StrBuf, SearchStrs, EnglishPos);
                }
                StrBuf = string.Empty;

                // ４文字適合
                if (search_str.SearchNum == 4)
                {
                    StrPosK += 4;
                }
                // ３文字適合
                else if (search_str.SearchNum == 3)
                {
                    StrPosK += 3;
                }
                // ２文字適合
                else if (search_str.SearchNum == 2)
                {
                    StrPosK += 2;
                }
                else // １文字適合
                {
                    ++StrPosK;
                }

                if (StrPosE == 0)
                {

                }
                StrPosK -= KanaPos; // かな表示の途中経過分を差し引く
                KanaPos = 0;
                EnglishPos = ++StrPosE;
                TypeMissFlag = false;

                // 一文終了時
                if (NowInputText.Length <= StrPosE)
                {
                    InputPos = 0;
                    StrPosK = 0; // 入力文(かな)ポジション
                    StrPosE = 0; // ローマ字文ポジション
                    EnglishPos = 0;
                    // 全文終了
                    if (input_textk.Length <= ++TextPos)
                    {
                        TextPos = 0;
                        return true; // タイプ終了
                    }
                    // 次のローマ字文章を作成してInputTextEに代入
                    NowInputText = MakeInputText(input_textk[TextPos]);
                    if (NowInputText == null)
                    {
                        Debug.Log("InputTextE is null");
                        return false;
                    }
                }

                MakeSearchStr(input_textk[TextPos], StrPosK);

                //DrawTextMeshPro(InputTextMesh, NowInputText, StrPosE, ScrollBaseE, ScrollDispMaxE, TypeMissFlag);
                InputPos = StrPosE;

                return false; // タイプ継続
            }
            else if (!string.IsNullOrEmpty(search_str.SearchStr)) // SearchStrが空かどうか調べる
            {
                if (StrBuf.Length <= search_str.SearchStr.Length) // 文字を切り取りすぎないための処理
                {
                    if (search_str.SearchStr.Substring(0, StrBuf.Length) == StrBuf)
                    {
                        agreeflg = true;
                        break;
                    }
                }
            }
        }

        // 一致してない場合のみここを通る(完全一致の場合は上でreturnされるのでここは通らない
        // 合致していなければバッファから消す
        if (!agreeflg) // タイプ文字が不正解の時の処理
        {
            TypeMissFlag = true;
            StrBuf = StrBuf.Substring(0, StrBuf.Length - 1); //間違えた一文字をバッファから消す
            incorrectInputCount++; // 誤った入力回数を増やす
        }
        else //タイプ文字が正解の時の処理
        {
            TypeMissFlag = false;
            string str_temp = input_textk[TextPos].Substring(StrPosK, 1);
            if (StrBuf == "ltu" || StrBuf == "xtu")
            {
                if (str_temp == "っ" || str_temp == "ッ") { StrPosK++; KanaPos = 1; }
            }
            else if (StrBuf == "nn")
            {
                if (str_temp == "ん" || str_temp == "ン") { StrPosK++; KanaPos = 1; }
            }
            StrChangedFlag = StrChange(
                ref NowInputText, ref EnglishLen, StrBuf, SearchStrs, EnglishPos
            );
            InputPos = ++StrPosE;
        }

        return false;
    }
    // キーボード押下回数を取得するメソッド
    public int GetKeyPressCount()
    {
        return keyPressCount;
    }
    public int GetIncorrectInputCount()
    {
        return incorrectInputCount; // 誤った入力回数を返す
    }

}
