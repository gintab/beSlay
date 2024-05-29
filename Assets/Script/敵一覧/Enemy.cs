using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public int maxHP = 100; // �ő�HP
    private int currentHP; // ���݂�HP

    public Text hpText; // HP��\������e�L�X�g

    public int maxAttackCount = 1;
    private int attackCount;

    void Start()
    {
        attackCount = maxAttackCount;
        currentHP = maxHP; // �ő�HP�ŏ�����
        UpdateHPText(); // HP�e�L�X�g���X�V
    }

    // HP�����炷���\�b�h
    public void TakeDamage(int damage)
    {
        currentHP -= damage; // �_���[�W��K�p
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); // HP��0�����ɂȂ�Ȃ��悤�ɂ���
        UpdateHPText(); // HP�e�L�X�g���X�V

        // HP��0�ɂȂ�����G��j�󂷂�Ȃǂ̏�����ǉ�����
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    // HP�e�L�X�g���X�V���郁�\�b�h
    void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + currentHP.ToString(); // HP�e�L�X�g���X�V
        }
    }
    public virtual void Attack()
    {

    }
    public virtual void StepCount()
    {
        attackCount--;
        if (attackCount <= 0)
        {
            Attack();
            attackCount = maxAttackCount;
        }
    }
}
