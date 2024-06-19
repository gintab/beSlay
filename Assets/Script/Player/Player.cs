using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Player : SingletonMonoBehaviour<Player>
{
    public int MaxHP = 80;
    private int m_currentHP;
    public int CurrentHP { 
        set { 
            this.m_currentHP = value;
            OnHPChanged?.Invoke();
        } 
        get { return m_currentHP; } }
    private Action OnHPChanged;
    [SerializeField]
    UnityEngine.UI.Text hpText; // HPを表示するテキスト
    [SerializeField]
    UnityEngine.UI.Slider m_slider;
    [SerializeField]
    UnityEngine.UI.Text sliderHPtext;

    public Transform PlayerPosOnCanvas;
    public LayerMask cardLayer; // カードのレイヤー

    private Card selectedCard;

    void Start()
    {
        m_slider.maxValue = MaxHP;
        m_slider.minValue = 0;
        OnHPChanged += UpdateUIText;
        selectedCard = null;
        this.CurrentHP = MaxHP;
    }

    void Update()
    {
        if(TurnManager.Instance.currentTurn == TurnManager.Turn.Ally)
        {
            // マウスの左クリックを検出
            if (Input.GetMouseButtonDown(0))
            {
                // マウスの位置をスクリーン座標からワールド座標に変換
                Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // レイキャストを作成
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                pointerEventData.position = Input.mousePosition;

                // レイキャスト結果を保持するリスト
                var raycastResults = new List<RaycastResult>();

                // UIイベントシステムにレイキャストを送信
                EventSystem.current.RaycastAll(pointerEventData, raycastResults);

                // レイキャスト結果があるかどうかをチェック
                if (raycastResults.Count > 0)
                {
                    foreach (var result in raycastResults)
                    {
                        // UI要素がクリックされた場合の処理
                        Debug.Log("UI Element Clicked: " + result.gameObject.name);
                        //中身の確認処理;
                        // カードをクリックした場合
                        if (result.gameObject.CompareTag("Card"))
                        {
                            if (selectedCard != null)
                            {
                                selectedCard.UnSelect();
                                if (selectedCard.gameObject == result.gameObject)
                                {
                                    selectedCard = null;
                                    return;
                                }
                            }
                            selectedCard = result.gameObject.GetComponent<Card>();
                            // カードを選択状態にする
                            if (selectedCard.isSelected == false && DeckManager.Instance.isSelectedHand == false)
                            {
                                selectedCard.Select();
                            }
                            return;
                        }
                        else if (result.gameObject.CompareTag("Enemy"))
                        {
                            // カードを選択
                            if (selectedCard != null && selectedCard.isSelected && DeckManager.Instance.CurrentCost > 0)
                            {
                                List<Enemy> enemy = new List<Enemy>();
                                enemy.Add(result.gameObject.GetComponent<Enemy>());
                                selectedCard.Use(enemy);
                                selectedCard = null;
                            }
                            return;
                        }
                        else
                        {
                            if (selectedCard != null) selectedCard.UnSelect();
                            selectedCard = null;
                        }
                    }
                }
                else
                {
                    // UI要素以外がクリックされた場合の処理
                    Debug.Log("No UI Element Clicked");                                                  
                }

                
            }
        }
    }
    public void ChangeHP(int value)
    {
        if (value < 0) StartCoroutine(OnTakeDamage(value));
        CurrentHP += value;        
    }
    private void UpdateUIText()
    {
        hpText.text = CurrentHP.ToString() + "/" + MaxHP;
        sliderHPtext.text = CurrentHP.ToString() + "/" + MaxHP;
        m_slider.value = CurrentHP;
    }
    public IEnumerator OnTakeDamage(int damage)
    {
        UImanager.Instance.PopDamageText(PlayerPosOnCanvas, damage);
        yield return null;
    }
}
