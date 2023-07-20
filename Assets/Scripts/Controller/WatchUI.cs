using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchUI : MonoBehaviour
{
    [SerializeField]
    private GameObject watchUi;
    //[SerializeField]
    //private GameObject leftController;
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

        //Vector3 leftHandForward = leftHandRotation * Vector3.forward;
        Vector3 cameraForward = cameraTransform.forward;

        watchUi.transform.position = cameraTransform.position + cameraForward * 1f;
        watchUi.transform.rotation = Quaternion.LookRotation(cameraForward);

        //watchUi.transform.position = leftController.transform.position + new Vector3(0, 0.5f, 0.3f);
        //watchUi.transform.rotation = Quaternion.Euler(watchUi.transform.rotation.x + leftController.transform.rotation.x, watchUi.transform.rotation.y + leftController.transform.rotation.y, watchUi.transform.rotation.z + leftController.transform.rotation.z);


        //Debug.Log(leftHandRotation.eulerAngles.z);
        //Debug.Log(leftHandForward);
        // �ո��� ������ �� UI Ȱ��ȭ
        //&& leftHandForward.x > 0.65f && leftHandForward.x < 1f 
        if (leftHandRotation.eulerAngles.z > 200f && leftHandRotation.eulerAngles.z < 300f && !uiActive)
        {
            watchUi.SetActive(true);
            uiActive = true;
        }

        // �ո��� ���� ��ġ�� �ǵ����� �� UI ��Ȱ��ȭ
        // || (leftHandForward.x <= 0.65f || leftHandForward.x >= 1f) 
        if ((leftHandRotation.eulerAngles.z <= 200f || leftHandRotation.eulerAngles.z >= 300f) && uiActive)
        {
            watchUi.SetActive(false);
            uiActive = false;
        }
    }
}
