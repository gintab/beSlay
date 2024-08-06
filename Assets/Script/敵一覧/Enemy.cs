using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public enum ActionType
{
    NONE,
    ATTACK,
    DEFENSE
}
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Sprite attack_sprite;
    [SerializeField]
    private Sprite defense_sprite;
    [SerializeField]
    private Sprite normal_sprite;
    [SerializeField]
    private GameObject block_IconObj;
    

    private Image m_image;

    public int maxHP = 100; // �ő�HP
    private ReactiveProperty<int> currentHP = new ReactiveProperty<int>(); // ���݂�HP
    public int CurrentHP
    {
        get{return currentHP.Value;}
        set{currentHP.Value = value;}
    }
    private ReactiveProperty<int> blockValue = new ReactiveProperty<int>(); //�u���b�N�l
    public int BlockValue
    {
        get { return blockValue.Value; }
        set { blockValue.Value = value; }
    }

    public Text hpText; // HP��\������e�L�X�g
    public Slider m_hpSlider; //HP�Q�[�W
    public TextMeshProUGUI AttackValueText;
    public TextMeshProUGUI block_valueText;

    private ActionType m_actionType = ActionType.NONE;

    public int defaultDamage;
    private ReactiveProperty<int> currentDamage = new ReactiveProperty<int>();
    public int CurrentDamage
    {
        get { return currentDamage.Value; }
        set { currentDamage.Value = value; }
    }

    protected virtual void Start()
    {
        m_image = gameObject.GetComponent<Image>();
        if (m_image == null)
        {
            m_image = gameObject.AddComponent<Image>();
        }
        SetActionType(ActionType.NONE);
        BlockValue = 3;
        InitStatus();
    }
    /// <summary>
    /// �X�e�[�^�X������
    /// </summary>
    public void InitStatus()
    {
        CurrentDamage = defaultDamage;
        CurrentHP = maxHP; // �ő�HP�ŏ�����
        UpdateHPText(); // HP�e�L�X�g���X�V
        UpdateAttackText(); // �U���̓e�L�X�g���X�V
        UpdateHPSlider();//HPSlider�X�V
        UpdateBlockText();//�u���b�N�l�X�V
        m_hpSlider.maxValue = maxHP;
        m_hpSlider.value = CurrentHP;
    }

    /// <summary>
    /// �U�����󂯂�
    /// </summary>
    /// <param name="damage">�󂯂�_���[�W</param>
    /// <param name="penet">�ђʒl</param>
    public void TakeDamage(int damage,int penet = 0)
    {
        if (BlockValue > 0)
        {
            SetActionType(ActionType.DEFENSE);
            StartCoroutine(DelaySetActionType(2f, ActionType.NONE));
            BlockValue -= damage;
            damage =  damage - BlockValue;
            UpdateBlockText();
        }
        if (damage <= 0) return;
        CurrentHP -= damage; // �_���[�W��K�p
        CurrentHP = Mathf.Clamp(CurrentHP, 0, maxHP); // HP��0�����ɂȂ�Ȃ��悤�ɂ���
        UpdateHPText(); // HP�e�L�X�g���X�V
        UpdateHPSlider();//HPSlider�X�V
        UImanager.Instance.PopDamageText(this.transform,damage);
        
        // HP��0�ɂȂ�����G��j�󂷂�Ȃǂ̏�����ǉ�����
        if (CurrentHP <= 0)
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
    protected void UpdateAttackText()
    {
        if (AttackValueText != null)
        {
            AttackValueText.text = CurrentDamage.ToString(); // �U���̓e�L�X�g���X�V
        }
    }
    // �u���b�N�e�L�X�g���X�V���郁�\�b�h
    protected void UpdateBlockText()
    {
        if (block_valueText != null)
        {
            block_valueText.text = BlockValue.ToString(); // �u���b�N�e�L�X�g���X�V
            if (BlockValue <= 0)
            {
                block_IconObj.SetActive(false);
            }
            else
            {
                block_IconObj.SetActive(true);
            }

                
        }        
    }
    // HP�X���C�_�[���X�V���郁�\�b�h
    protected void UpdateHPSlider()
    {
        if (m_hpSlider != null)
        {
            m_hpSlider.value = CurrentHP; // HP�X���C�_�[�X�V
        }
    }
    public virtual void Attack()
    {
        SetActionType(ActionType.ATTACK);
    }
    public void SetActionType(ActionType actionType)
    {
        m_actionType = actionType;
        ChangeImage();
    }
    public void ChangeImage()
    {
        switch (m_actionType)
        {
            case ActionType.ATTACK:
                m_image.sprite = attack_sprite;
                break;
            case ActionType.DEFENSE:
                m_image.sprite = defense_sprite;
                break;
            case ActionType.NONE:
                m_image.sprite = normal_sprite;
                break;

        }
    }
    private IEnumerator DelaySetActionType(float delay,ActionType actionType)
    {
        yield return new WaitForSeconds(delay);
        if (this == null)
        {
            yield return null;
        }else
        {
            SetActionType(actionType);
        }                    
    }
}
