using UnityEngine;

public class CrosshairDisplay : MonoBehaviour
{
    public Material crosshairMaterial;  // �g�p������Animespeedlines�}�e���A��
    private GameObject crosshairInstance;
    private Transform centerEyeAnchor;

    void Start()
    {
        // OVR Camera Rig��CenterEyeAnchor���擾
        OVRCameraRig cameraRig = FindObjectOfType<OVRCameraRig>();
        if (cameraRig != null)
        {
            centerEyeAnchor = cameraRig.centerEyeAnchor;
        }
        else
        {
            Debug.LogError("OVR Camera Rig��������܂���B�V�[���ɔz�u����Ă��܂����H");
            return;
        }

        // �N���X�w�A�p��Quad�𐶐�
        crosshairInstance = GameObject.CreatePrimitive(PrimitiveType.Quad);
        crosshairInstance.transform.SetParent(centerEyeAnchor); // �J�����ɒǏ]
        crosshairInstance.transform.localPosition = new Vector3(0, 0, 3); // �J�����̑O�ɔz�u
        crosshairInstance.transform.localRotation = Quaternion.identity; // ��]�Ȃ�
        crosshairInstance.transform.localScale = new Vector3(5, 5, 1); // �T�C�Y��傫������

        // Quad��Renderer��Animespeedlines�}�e���A����K�p
        Renderer renderer = crosshairInstance.GetComponent<Renderer>();
        if (renderer != null && crosshairMaterial != null)
        {
            renderer.material = crosshairMaterial; // �w��̃}�e���A����K�p
        }
        else
        {
            Debug.LogError("Renderer��Material��������܂���B");
        }
    }

    void Update()
    {
        // �J�����̑O�ɃN���X�w�A����ɕ\��
        if (centerEyeAnchor != null && crosshairInstance != null)
        {
            crosshairInstance.transform.localPosition = new Vector3(0, 0, 2); // �J�����̑O2���[�g���ɕ\��
        }
    }
}
