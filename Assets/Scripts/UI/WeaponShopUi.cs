using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopUi : MonoBehaviour
{
    public GameObject objectToActivate;
    public GameObject price;
    public GameObject weaponInformation;
    private GameObject weapon;
    public Transform itemSelect;
    private int weaponLevel;
    [SerializeField] private int[] upgradeCosts = new int[] { 0, 100, 200 }; // �� ���� ������ ���׷��̵� ��� �迭

    public void Start()
    {
        
        weaponLevel = GameManager.gameManager.weaponLevel + 1;
        price.GetComponent<Text>().text = upgradeCosts[weaponLevel - 1].ToString() + " ��";
    }
    private void OnTriggerEnter(Collider other) // �÷��̾� ���ٽ� ���� Ȱ��ȭ
    {
        if (other.CompareTag("Player"))
        {
            objectToActivate.SetActive(true);
            weapon = itemSelect.transform.Find("WP_Bundle").transform.Find(GameManager.gameManager.weaponName).transform.Find(GameManager.gameManager.weaponName + weaponLevel).gameObject;
            ChangeWeapon(GameManager.gameManager.weaponName);
            weaponInformation.GetComponent<Text>().text = "���� ���ݷ� = " + weapon.GetComponent<Hit>().WeaponData.AttackDamage + '\n' + "���� ���ݼӵ� = " + weapon.GetComponent<Hit>().WeaponData.AttackSpeed + '\n' + "�˹� = " + weapon.GetComponent<Hit>().WeaponData.KnockBack;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectToActivate.SetActive(false);
        }
    }

    public void UpgradeWeapon()
    {
        if (GameManager.gameManager.SpendCurrency(upgradeCosts[weaponLevel])) // ���� �Ŵ��� �ڵ� �� ������ true �� �� �Ҹ� ������ false
        {
            if (GameManager.gameManager.weaponLevel >= 3)
            {
                Debug.Log("Ǯ���Դϴ�");
            }
            else
            {
                GameManager.gameManager.weaponLevel += 1;
                weaponLevel = GameManager.gameManager.weaponLevel+1;
                weapon = itemSelect.transform.Find("WP_Bundle").transform.Find(GameManager.gameManager.weaponName).transform.Find(GameManager.gameManager.weaponName + weaponLevel).gameObject;
                price.GetComponent<Text>().text = upgradeCosts[weaponLevel - 1].ToString() + " ��";
                ChangeWeapon(GameManager.gameManager.weaponName);
                weaponInformation.GetComponent<Text>().text = "���� ���ݷ� = " + weapon.GetComponent<Hit>().WeaponData.AttackDamage + '\n' + "���� ���ݼӵ� = " + weapon.GetComponent<Hit>().WeaponData.AttackSpeed + '\n' + "�˹� = " + weapon.GetComponent<Hit>().WeaponData.KnockBack;
            }
            Debug.Log("���ż���");
        }
        else
        {
            // ��ǰ ���ſ� ������ ��� �޽����� �� ������ �˷��ִ� ǥ�� �����ֱ�
            Debug.Log("���Ž���");
        }
    }

    public void ChangeWeapon(string weaponName)
    {
        //1���� �� 1�� ���� Ȱ��
        if (weaponLevel == 1)
        {
            itemSelect.transform.Find("WP_Bundle").transform.Find(weaponName).transform.Find(weaponName + weaponLevel).gameObject.SetActive(true);
        }
        //1�� �ʰ��� �� ���ܰ蹫�� ��Ȱ�� �� ���� ����Ȱ��
        else if (1 < weaponLevel && weaponLevel <= 3)
        {
            itemSelect.transform.Find("WP_Bundle").transform.Find(weaponName).transform.Find(weaponName + weaponLevel).gameObject.SetActive(true);
            itemSelect.transform.Find("WP_Bundle").transform.Find(weaponName).transform.Find(weaponName + (weaponLevel - 1)).gameObject.SetActive(false);
        }
        else if (weaponLevel > 3)
        {
            Debug.Log("err");
        }
    }
}