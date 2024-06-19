using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// �J�[�h�^�C�v��\���񋓌^
public enum CardType
{
    Buff,
    Debuff,
    Attack,
    Defense
}

// �J�[�h�N���X
public class Card : MonoBehaviour
{
    // �v���p�e�B
    [SerializeField]
    public int Cost;// �R�X�g
    [SerializeField]
    public CardType Type; // �J�[�h�^�C�v
    [SerializeField]
    public int Rarity;// ���A�x
    [SerializeField]
    public string CardName = "";
    [SerializeField]
    public string effectText = "";

    [SerializeField]
    UnityEngine.UI.Text cost_txt;
    [SerializeField]
    UnityEngine.UI.Text effect_txt;
    [SerializeField]
    UnityEngine.UI.Text cardName_txt;

    public bool isSelected = false;
    // �J�[�h�g�p���̃C�x���g
    public event Action<Card> OnCardUsed;
    // �J�[�h�I�����̃C�x���g
    public event Action<Card> OnCardSelected;
    // �J�[�h��I�������̃C�x���g
    public event Action<Card> OnCardUnSelected;

    public float defalutScaleX;
    public float defalutScaleY;

    // �R���X�g���N�^
    public Card(int cost, CardType type, int rarity,string cardName)
    {
        Cost = cost;
        Type = type;
        Rarity = rarity;
        CardName = cardName;
        isSelected = false;
    }
    private void Start()
    {
        defalutScaleX = this.gameObject.transform.localScale.x;
        defalutScaleY = this.gameObject.transform.localScale.y;
    }

    // �J�[�h���g�p���郁�\�b�h
    public virtual void Use(List<Enemy> enemy)
    {
        if (isSelected)
        {
            // �J�[�h���g�p���邽�тɃR�X�g�������
            Debug.Log($"�J�[�h���g�p���܂����B�R�X�g: {Cost}");

            // �J�[�h�g�p���̃C�x���g�𔭍s
            if (OnCardUsed != null)
            {
                OnCardUsed(this);
            }
        }
    }

    // �J�[�h���g�p���郁�\�b�h(��I��)
    public virtual void UseUnselect(List<Enemy> enemy)
    {
        if (isSelected)
        {
            // �J�[�h���g�p���邽�тɃR�X�g�������
            Debug.Log($"�J�[�h���g�p���܂����B�R�X�g: {Cost}");

            // �J�[�h�g�p���̃C�x���g�𔭍s
            if (OnCardUsed != null)
            {
                OnCardUsed(this);
            }
        }
    }


    // �J�[�h�̏���\�����郁�\�b�h
    public void ShowInfo()
    {
        Debug.Log($"�J�[�h��� - , ���O: {CardName}, �^�C�v: {Type}, �R�X�g: {Cost}, ���A�x: {Rarity}");
    }
    public virtual void Select()
    {
        this.gameObject.transform.localScale = new Vector2(defalutScaleX * 1.5f,defalutScaleY * 1.5f);
        isSelected = true;
        // �J�[�h�I�����̃C�x���g�𔭍s
        if (OnCardSelected != null)
        {
            OnCardSelected(this);
        }
    }
    public void UnSelect()
    {
        this.gameObject.transform.localScale = new Vector2(defalutScaleX, defalutScaleY);
        isSelected = false;
        // �J�[�h��I�����̃C�x���g�𔭍s
        if (OnCardUnSelected != null)
        {
            OnCardUnSelected(this);
        }
    }
    public void OnGUI()
    {
        if (cost_txt) cost_txt.text = Cost.ToString();
        if (effect_txt) effect_txt.text = effectText;
        if (cardName_txt) cardName_txt.text = CardName;
    }

    
}