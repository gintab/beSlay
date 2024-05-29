using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Net.Security;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.Rendering;
using UnityEditor;

public class DeckManager :SingletonMonoBehaviour<DeckManager>
{
    public List<GameObject> StartDeck;
    public bool isSelectedHand { get; private set; }
    public GameObject deckPos;
    public GameObject handPos;
    public GameObject cemePos;

    public TextMeshProUGUI deckTxt;
    public TextMeshProUGUI costTxt;
    public TextMeshProUGUI cemeTxt;

    public event System.Action<int> OnCostChanged;
    public event System.Action<int> OnDeckChanged;
    public event System.Action<int> OnCemeChanged;

    public int drawNum = 1;
    public const int startHandNum = 5;
    private int m_currentCost;
    public int CurrentCost
    {
        get => m_currentCost;
        set
        {
            m_currentCost = value;
            OnCostChanged?.Invoke(value);
        }
    }
    private int m_currentDeckNum;
    public int CurrentDeckNum
    {
        get => m_currentDeckNum;
        set
        {
            m_currentDeckNum = value;
            OnDeckChanged?.Invoke(value);
        }
    }
    private int m_currentCemeNum;
    public int CurrentCemeNum
    {
        get => m_currentCemeNum;
        set
        {
            m_currentCemeNum = value;
            OnCemeChanged?.Invoke(value);
        }
    }
    public int MaxCost;

    #region 初期化
    public DeckManager()
    {
        CurrentCost = MaxCost;
        CurrentCemeNum = 0;
    }
    public void StartBattle()
    {
        OnCostChanged += CostChanged;
        OnDeckChanged += DeckChanged;
        OnCemeChanged += CemeChanged;

        ResetAll();
        for (int i = 0; i < StartDeck.Count; i++)
        {
            AddCard(Instantiate(StartDeck[i]) as GameObject);
        }
        DrawCard(startHandNum);
    }
    
    public void ResetAll()
    {
        ResetDeck();
        ResetCeme();
        ResetHand();
        ResetCost();
        UIRefresh();
    }
    public void ResetCost()
    {
        CurrentCost = MaxCost;
    }
    public void ResetDeck()
    {
        foreach (Transform n in deckPos.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
        CurrentDeckNum = StartDeck.Count;
    }
    public void ResetCeme()
    {
        foreach (Transform n in cemePos.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
    }
    public void ResetHand()
    {
        foreach (Transform n in handPos.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
    }
    #endregion
    #region カード操作
    public void AddCard(GameObject card)
    {
        if (card.GetComponent<Card>() == null)
        {
            Debug.LogError(card.name + "カードを追加できませんでした");
            return;
        }
        card.transform.SetParent(deckPos.transform,false);
        UIRefresh();
    }
    public void DrawCard(int num)
    {
        if(num > deckPos.transform.childCount)
        {
            Debug.LogWarning("引く枚数がデッキの枚数を超えています。");
            return;
        }
        for (int ii = 0; ii < num; ii++)
        {
            //手札に加える
            Card getCard = deckPos.transform.GetChild(ii).GetComponent<Card>();
            if (getCard != null)
            {
                getCard.OnCardUsed += UseCard;
                getCard.OnCardSelected += SelectCard;
                getCard.OnCardUnSelected += UnSelectCard;
                getCard.gameObject.transform.SetParent(handPos.transform,false);
            }
        }
        UIRefresh();
    }

    private void UseCard(Card usedCard)
    {
        if (isSelectedHand)
        {
            if (CurrentCost  < usedCard.Cost)
            {
                //コスト足りないダメ
                return;
            }
            CurrentCost -= usedCard.Cost;
            //墓地に加える
            usedCard.transform.SetParent(cemePos.transform,false);
            UIRefresh();
            //使ったカードは非選択に
            usedCard.UnSelect();
            //イベントを破棄
            usedCard.OnCardSelected -= SelectCard;
            usedCard.OnCardUnSelected -= UnSelectCard;
            usedCard.OnCardUsed -= UseCard;
        }
    }

    [ContextMenu("山札をシャッフル")]
    private void ShuffleDeck()
    {
        foreach (var obj in deckPos.GetComponentsInChildren<Transform>())
        {
            obj.SetSiblingIndex(UnityEngine.Random.Range(0,deckPos.transform.childCount));
        }
    }
    public void DisposeHand(int value)
    {
        int index = 0;
        int childcount = handPos.transform.childCount;
        if (childcount == 0) { return; }
        for (int ii = 0; ii < value; ii++)
        {
            if (handPos.transform.childCount == 0) break; 
            index = UnityEngine.Random.Range(0,childcount);
            handPos.transform.GetChild(index).SetParent(cemePos.transform, false);
        }
        UIRefresh();
    }
    public void DisposeHandAll()
    {
        int childcount = handPos.transform.childCount;
        for (int ii = 0; ii < childcount; ii++)
        {
            handPos.transform.GetChild(0).SetParent(cemePos.transform, false);
        }
        UIRefresh();
    }
    #endregion
    public void SelectCard(Card usedCard)
    {
        if (isSelectedHand) return;
        ArrowController.Instance.PopArrow();
        isSelectedHand = true;
    }
    public void UnSelectCard(Card usedCard)
    {
        if (!isSelectedHand) return;
        ArrowController.Instance.UnPopArrow();
        isSelectedHand = false;
    }
    public void DeckChanged(int value)
    {
        deckTxt.text = value.ToString();
    }
    public void CostChanged(int value)
    {
        costTxt.text = value.ToString() + "/" + MaxCost.ToString();
    }
    public void CemeChanged(int value)
    {
        cemeTxt.text = value.ToString();
    }
    public void UIRefresh()
    {
        CurrentDeckNum = deckPos.transform.childCount;
        CurrentCemeNum = cemePos.transform.childCount;
    }
}
