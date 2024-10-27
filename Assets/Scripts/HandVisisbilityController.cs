using UnityEngine;

public class HandVisibilityController : MonoBehaviour
{
    public OVRHand leftHand;  // 左手のOVRHandコンポーネント
    public OVRHand rightHand; // 右手のOVRHandコンポーネント
    public GameObject keyboard; // キーボードのオブジェクト

    void Update()
    {
        // 手のメッシュが消えないようにする
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


