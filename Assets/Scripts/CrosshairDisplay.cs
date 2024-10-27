using UnityEngine;

public class CrosshairDisplay : MonoBehaviour
{
    public Material crosshairMaterial;  // 使用したいAnimespeedlinesマテリアル
    private GameObject crosshairInstance;
    private Transform centerEyeAnchor;

    void Start()
    {
        // OVR Camera RigのCenterEyeAnchorを取得
        OVRCameraRig cameraRig = FindObjectOfType<OVRCameraRig>();
        if (cameraRig != null)
        {
            centerEyeAnchor = cameraRig.centerEyeAnchor;
        }
        else
        {
            Debug.LogError("OVR Camera Rigが見つかりません。シーンに配置されていますか？");
            return;
        }

        // クロスヘア用のQuadを生成
        crosshairInstance = GameObject.CreatePrimitive(PrimitiveType.Quad);
        crosshairInstance.transform.SetParent(centerEyeAnchor); // カメラに追従
        crosshairInstance.transform.localPosition = new Vector3(0, 0, 3); // カメラの前に配置
        crosshairInstance.transform.localRotation = Quaternion.identity; // 回転なし
        crosshairInstance.transform.localScale = new Vector3(5, 5, 1); // サイズを大きくする

        // QuadのRendererにAnimespeedlinesマテリアルを適用
        Renderer renderer = crosshairInstance.GetComponent<Renderer>();
        if (renderer != null && crosshairMaterial != null)
        {
            renderer.material = crosshairMaterial; // 指定のマテリアルを適用
        }
        else
        {
            Debug.LogError("RendererかMaterialが見つかりません。");
        }
    }

    void Update()
    {
        // カメラの前にクロスヘアを常に表示
        if (centerEyeAnchor != null && crosshairInstance != null)
        {
            crosshairInstance.transform.localPosition = new Vector3(0, 0, 2); // カメラの前2メートルに表示
        }
    }
}
