using UnityEngine;

public class HandVisibilityController : MonoBehaviour
{
    public OVRHand leftHand;  // �����OVRHand�R���|�[�l���g
    public OVRHand rightHand; // �E���OVRHand�R���|�[�l���g
    public GameObject keyboard; // �L�[�{�[�h�̃I�u�W�F�N�g

    void Update()
    {
        // ��̃��b�V���������Ȃ��悤�ɂ���
        if (leftHand && leftHand.IsTracked)
        {
            leftHand.gameObject.SetActive(true);
        }
        if (rightHand && rightHand.IsTracked)
        {
            rightHand.gameObject.SetActive(true);
        }
    }
}


