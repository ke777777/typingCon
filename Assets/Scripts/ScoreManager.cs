using TMPro;
using UnityEngine;
public class ScoreManager : MonoBehaviour
{
    private CRomaTypeEngine cromaTypeEngine;
    private int score; // スコアを保持する変数
    public TextMeshProUGUI scoreText; // TMPテキストをInspectorから設定するための変数

    void Start()
    {
        // CRomaTypeEngineのインスタンスを作成
        cromaTypeEngine = new CRomaTypeEngine();
        score = 0; // 初期スコア

    }
    /*void Update()
    {
        // キーボードが押された場合にカウント
        if (Input.anyKeyDown)
        {
            score++;
            Debug.Log("キーが押された回数: " + score);
            cromaTypeEngine.IncrementKeyPressCount();
             CalculateScore(); // キーが押されたときにスコアを計算
        }
    }
*/

    public void CalculateScore()
    {
        // キーボード押下回数を取得
        int keyPressCount = cromaTypeEngine.GetKeyPressCount();
        Debug.Log("Current Key Press Count: " + keyPressCount); // キー押下回数をログに出力

        // スコアにキーボード押下回数を加算
        score += keyPressCount; // スコアを加算


        scoreText.text = "Current Score: " + score; // スコアをTMPテキストに設定

        Debug.Log("Current Score: " + score); // スコアをコンソールに表示
    }
}

/*
// 使用例
public class Game
{
    public void Start(CRomaTypeEngine engine, TextMeshProUGUI scoreText)
    {
        // ScoreManagerのインスタンスを作成
        ScoreManager scoreManager = new ScoreManager(engine, scoreText);

        // スコアを計算
        scoreManager.CalculateScore();
    }
}

// プログラムのエントリポイント
public class Program
{
    public static void Main(string[] args)
    {
        // CRomaTypeEngineのインスタンスを作成
        CRomaTypeEngine engine = new CRomaTypeEngine();

        // Gameクラスのインスタンスを作成し、スタートメソッドを呼ぶ
        Game game = new Game();
        // TMPオブジェクトの取得はUnityエディタで行うため、ここでは省略
    }
}
*/