using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchUI : MonoBehaviour
{
    [SerializeField]
    private GameObject watchUi;
    [SerializeField]
    private Transform cameraTransform;


    private bool uiActive = false;

    // Update is called once per frame
    void Update()
    {
        WatchUiActive();
    }

    private void WatchUiActive()
    {
        // �޼� ȸ�� ����
        Quaternion leftHandRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);

        // �޼� ��ġ ����

        Vector3 leftHandForward = leftHandRotation * Vector3.forward;

        //Debug.Log(leftHandRotation.eulerAngles.z);
        Debug.Log(leftHandForward);
        // �ո��� ������ �� UI Ȱ��ȭ
        if (leftHandRotation.eulerAngles.z > 240f && leftHandRotation.eulerAngles.z < 275f && leftHandForward.x > 0.65f && leftHandForward.x < 1f && !uiActive)
        {
            watchUi.SetActive(true);
            uiActive = true;
        }

        // �ո��� ���� ��ġ�� �ǵ����� �� UI ��Ȱ��ȭ
        if ((leftHandRotation.eulerAngles.z <= 240f || leftHandRotation.eulerAngles.z >= 275f) || (leftHandForward.x <= 0.65f || leftHandForward.x >= 1f) && uiActive)
        {
            watchUi.SetActive(false);
            uiActive = false;
        }
    }
}
