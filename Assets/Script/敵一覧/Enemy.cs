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

    public int maxHP = 100; // 最大HP
    private ReactiveProperty<int> currentHP = new ReactiveProperty<int>(); // 現在のHP
    public int CurrentHP
    {
        get{return currentHP.Value;}
        set{currentHP.Value = value;}
    }
    private ReactiveProperty<int> blockValue = new ReactiveProperty<int>(); //ブロック値
    public int BlockValue
    {
        get { return blockValue.Value; }
        set { blockValue.Value = value; }
    }

    public Text hpText; // HPを表示するテキスト
    public Slider m_hpSlider; //HPゲージ
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
    /// ステータス初期化
    /// </summary>
    public void InitStatus()
    {
        CurrentDamage = defaultDamage;
        CurrentHP = maxHP; // 最大HPで初期化
        UpdateHPText(); // HPテキストを更新
        UpdateAttackText(); // 攻撃力テキストを更新
        UpdateHPSlider();//HPSlider更新
        UpdateBlockText();//ブロック値更新
        m_hpSlider.maxValue = maxHP;
        m_hpSlider.value = CurrentHP;
    }

    /// <summary>
    /// 攻撃を受けた
    /// </summary>
    /// <param name="damage">受けるダメージ</param>
    /// <param name="penet">貫通値</param>
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
        CurrentHP -= damage; // ダメージを適用
        CurrentHP = Mathf.Clamp(CurrentHP, 0, maxHP); // HPが0未満にならないようにする
        UpdateHPText(); // HPテキストを更新
        UpdateHPSlider();//HPSlider更新
        UImanager.Instance.PopDamageText(this.transform,damage);
        
        // HPが0になったら敵を破壊するなどの処理を追加する
        if (CurrentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    // HPテキストを更新するメソッド
    void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = currentHP.ToString() + "/" + maxHP.ToString(); // HPテキストを更新
        }
    }
    // 攻撃力テキストを更新するメソッド
    protected void UpdateAttackText()
    {
        if (AttackValueText != null)
        {
            AttackValueText.text = CurrentDamage.ToString(); // 攻撃力テキストを更新
        }
    }
    // ブロックテキストを更新するメソッド
    protected void UpdateBlockText()
    {
        if (block_valueText != null)
        {
            block_valueText.text = BlockValue.ToString(); // ブロックテキストを更新
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
    // HPスライダーを更新するメソッド
    protected void UpdateHPSlider()
    {
        if (m_hpSlider != null)
        {
            m_hpSlider.value = CurrentHP; // HPスライダー更新
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
