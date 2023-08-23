using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkill : MonoBehaviour
{
    private SkillPaperTowel paperTowel;
    [SerializeField]
    private SkillCooldown skillCooldown;

    private void Awake()
    {
        paperTowel = GetComponent<SkillPaperTowel>();
    }
    private void FixedUpdate()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.X))       //��ư�� ������ ��
        {
            if (paperTowel.isCooldown == false)
            {
                paperTowel.StartCoroutine(paperTowel.PaperTowel());
                skillCooldown.HideSkillSetting(1);
            }
        }
    }
}
