using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLog : MonoBehaviour
{
    [SerializeField]
    private float cooldown = 10;             // ��Ÿ��
    [SerializeField]
    private float force = 5f;              // ������ ��

    [SerializeField]
    private GameObject logPrefab;
    public bool isCooldown = false;         // ��Ÿ�� Ȱ��ȭ / ��Ȱ��ȭ üũ
    private GameObject log;

    private Transform playerTransform;      // �÷��̾� ��ġ

    private void Awake()
    {
        playerTransform = gameObject.transform.root;            // �÷��̾� ��ġ ����
    }

    public IEnumerator Log()
    {
        isCooldown = true;
        Vector3 playerPosition = playerTransform.transform.position;        // �÷��̾� ��ġ
        Vector3 playerForward = playerTransform.transform.forward;          // �÷��̾� ��
        Quaternion rotation = Quaternion.Euler(0f, playerTransform.rotation.eulerAngles.y, 90f);
        log = Instantiate(logPrefab, playerPosition + playerForward, rotation);
        Rigidbody rigidBody = log.GetComponent<Rigidbody>();

        if (rigidBody != null)
        {
            rigidBody.AddForce(playerForward * force, ForceMode.Impulse);           // �÷��̾� ���ʿ��� ���ư�
        }

        StartCoroutine(DestroyPrefab());

        yield return new WaitForSeconds(cooldown);

        isCooldown = false;
    }

    private IEnumerator DestroyPrefab()
    {
        yield return new WaitForSeconds(3f);

        Destroy(log);                        // ����
    }
}
