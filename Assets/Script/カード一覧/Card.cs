using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// カードタイプを表す列挙型
public enum CardType
{
    Buff,
    Debuff,
    Attack,
    Defense
}

// カードクラス
public class Card : MonoBehaviour
{
    // プロパティ
    [SerializeField]
    public int Cost;// コスト
    [SerializeField]
    public CardType Type; // カードタイプ
    [SerializeField]
    public int Rarity;// レア度
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
    // カード使用時のイベント
    public event Action<Card> OnCardUsed;
    // カード選択時のイベント
    public event Action<Card> OnCardSelected;
    // カード非選択時時のイベント
    public event Action<Card> OnCardUnSelected;

    public float defalutScaleX;
    public float defalutScaleY;

    // コンストラクタ
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

    // カードを使用するメソッド
    public virtual void Use(List<Enemy> enemy)
    {
        if (isSelected)
        {
            // カードを使用するたびにコストを消費する
            Debug.Log($"カードを使用しました。コスト: {Cost}");

            // カード使用時のイベントを発行
            if (OnCardUsed != null)
            {
                OnCardUsed(this);
            }
        }
    }

    // カードを使用するメソッド(非選択)
    public virtual void UseUnselect(List<Enemy> enemy)
    {
        if (isSelected)
        {
            // カードを使用するたびにコストを消費する
            Debug.Log($"カードを使用しました。コスト: {Cost}");

            // カード使用時のイベントを発行
            if (OnCardUsed != null)
            {
                OnCardUsed(this);
            }
        }
    }


    // カードの情報を表示するメソッド
    public void ShowInfo()
    {
        Debug.Log($"カード情報 - , 名前: {CardName}, タイプ: {Type}, コスト: {Cost}, レア度: {Rarity}");
    }
    public virtual void Select()
    {
        this.gameObject.transform.localScale = new Vector2(defalutScaleX * 1.5f,defalutScaleY * 1.5f);
        isSelected = true;
        // カード選択時のイベントを発行
        if (OnCardSelected != null)
        {
            OnCardSelected(this);
        }
    }
    public void UnSelect()
    {
        this.gameObject.transform.localScale = new Vector2(defalutScaleX, defalutScaleY);
        isSelected = false;
        // カード非選択時のイベントを発行
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