using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // ī�޶� �ٶ󺸰� �ְ� �ϴ� ��� ��𼭵� �� �� �ְ�
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
