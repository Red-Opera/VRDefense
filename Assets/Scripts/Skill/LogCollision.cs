using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogCollision : MonoBehaviour
{
    [SerializeField]
    private int damage = 50;
    [SerializeField]
    private float knockback = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        Monster monster = collision.gameObject.GetComponent<Monster>();

        if (monster != null)
        {
            // ���Ϳ� �������� ����
            monster.TakeDamage(damage, gameObject.transform.position, knockback);
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.collider);           // �� ���� �浹�ϵ��� ����
        }
    }
}
