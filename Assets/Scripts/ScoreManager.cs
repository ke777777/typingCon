using TMPro;
using UnityEngine;
public class ScoreManager : MonoBehaviour
{
    private CRomaTypeEngine cromaTypeEngine;
    private int score; // �X�R�A��ێ�����ϐ�
    public TextMeshProUGUI scoreText; // TMP�e�L�X�g��Inspector����ݒ肷�邽�߂̕ϐ�

    void Start()
    {
        // CRomaTypeEngine�̃C���X�^���X���쐬
        cromaTypeEngine = new CRomaTypeEngine();
        score = 0; // �����X�R�A

    }
    /*void Update()
    {
        // �L�[�{�[�h�������ꂽ�ꍇ�ɃJ�E���g
        if (Input.anyKeyDown)
        {
            score++;
            Debug.Log("�L�[�������ꂽ��: " + score);
            cromaTypeEngine.IncrementKeyPressCount();
             CalculateScore(); // �L�[�������ꂽ�Ƃ��ɃX�R�A���v�Z
        }
    }
*/

    public void CalculateScore()
    {
        // �L�[�{�[�h�����񐔂��擾
        int keyPressCount = cromaTypeEngine.GetKeyPressCount();
        Debug.Log("Current Key Press Count: " + keyPressCount); // �L�[�����񐔂����O�ɏo��

        // �X�R�A�ɃL�[�{�[�h�����񐔂����Z
        score += keyPressCount; // �X�R�A�����Z


        scoreText.text = "Current Score: " + score; // �X�R�A��TMP�e�L�X�g�ɐݒ�

        Debug.Log("Current Score: " + score); // �X�R�A���R���\�[���ɕ\��
    }
}

/*
// �g�p��
public class Game
{
    public void Start(CRomaTypeEngine engine, TextMeshProUGUI scoreText)
    {
        // ScoreManager�̃C���X�^���X���쐬
        ScoreManager scoreManager = new ScoreManager(engine, scoreText);

        // �X�R�A���v�Z
        scoreManager.CalculateScore();
    }
}

// �v���O�����̃G���g���|�C���g
public class Program
{
    public static void Main(string[] args)
    {
        // CRomaTypeEngine�̃C���X�^���X���쐬
        CRomaTypeEngine engine = new CRomaTypeEngine();

        // Game�N���X�̃C���X�^���X���쐬���A�X�^�[�g���\�b�h���Ă�
        Game game = new Game();
        // TMP�I�u�W�F�N�g�̎擾��Unity�G�f�B�^�ōs�����߁A�����ł͏ȗ�
    }
}
*/