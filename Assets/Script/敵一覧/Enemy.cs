using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHP = 100; // �ő�HP
    private int currentHP; // ���݂�HP
    private int blockValue; //�u���b�N�l

    public Text hpText; // HP��\������e�L�X�g
    public Slider m_hpSlider; //HP�Q�[�W
    public TextMeshProUGUI AttackValueText;

    public int defaultDamage;
    protected int currentDamage;

    void Start()
    {
        currentDamage = defaultDamage;
        currentHP = maxHP; // �ő�HP�ŏ�����
        UpdateHPText(); // HP�e�L�X�g���X�V
        UpdateAttackText(); // �U���̓e�L�X�g���X�V
        UpdateHPSlider();//HPSlider�X�V
        m_hpSlider.maxValue = maxHP;
        m_hpSlider.value = currentHP;
    }

    /// <summary>
    /// �U�����󂯂�
    /// </summary>
    /// <param name="damage">�󂯂�_���[�W</param>
    /// <param name="penet">�ђʒl</param>
    public void TakeDamage(int damage,int penet = 0)
    {
        if (blockValue > 0)
        {
            blockValue -= damage;
            damage =  damage - blockValue;
        }
        if (damage <= 0) return;
        currentHP -= damage; // �_���[�W��K�p
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); // HP��0�����ɂȂ�Ȃ��悤�ɂ���
        UpdateHPText(); // HP�e�L�X�g���X�V
        UpdateHPSlider();//HPSlider�X�V
        UImanager.Instance.PopDamageText(this.transform,damage);
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
            hpText.text = currentHP.ToString() + "/" + maxHP.ToString(); // HP�e�L�X�g���X�V
        }
    }
    // �U���̓e�L�X�g���X�V���郁�\�b�h
    void UpdateAttackText()
    {
        if (AttackValueText != null)
        {
            AttackValueText.text = currentDamage.ToString(); // �U���̓e�L�X�g���X�V
        }
    }
    // HP�X���C�_�[���X�V���郁�\�b�h
    void UpdateHPSlider()
    {
        if (m_hpSlider != null)
        {
            m_hpSlider.value = currentHP; // HP�X���C�_�[�X�V
        }
    }
    public virtual void Attack()
    {

    }
}
