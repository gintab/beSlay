using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : SingletonMonoBehaviour<Player>
{
    public int MaxHP = 80;
    private int currentHP;
    [SerializeField]
    UnityEngine.UI.Text hpText; // HPを表示するテキスト
    [SerializeField]
    UnityEngine.UI.Slider m_slider;

    public LayerMask cardLayer; // カードのレイヤー

    private Card selectedCard;

    void Start()
    {
        selectedCard = null;
        currentHP = MaxHP;
        m_slider.maxValue = MaxHP;
        m_slider.minValue = 0;
    }

    void Update()
    {
        if(TurnManager.Instance.currentTurn == TurnManager.Turn.Ally)
        {
            // マウスの左クリックを検出
            if (Input.GetMouseButtonDown(0))
            {
                //RaycastAllの引数（PointerEventData）作成
                PointerEventData pointData = new PointerEventData(EventSystem.current);

                //RaycastAllの結果格納用List
                List<RaycastResult> RayResult = new List<RaycastResult>();

                //PointerEventDataにマウスの位置をセット
                pointData.position = Input.mousePosition;
                //RayCast（スクリーン座標）
                EventSystem.current.RaycastAll(pointData, RayResult);

                foreach (RaycastResult result in RayResult)
                {
                    //中身の確認処理

                    // カードをクリックした場合
                    if (result.gameObject.CompareTag("Card"))
                    {
                        if (selectedCard != null)
                        {
                            selectedCard.UnSelect();
                            if (selectedCard.gameObject == result.gameObject)
                            {
                                selectedCard = null;
                                break;　//同じカードをクリックした場合は解除のみ  
                            }
                        }
                        selectedCard = result.gameObject.GetComponent<Card>();
                        // カードを選択状態にする
                        if (selectedCard.isSelected == false && DeckManager.Instance.isSelectedHand == false)
                        {
                            selectedCard.Select();
                        }
                        break;
                    }
                    else if (result.gameObject.CompareTag("Enemy"))
                    {
                        // カードを選択
                        if (selectedCard != null && selectedCard.isSelected && DeckManager.Instance.CurrentCost > 0)
                        {
                            selectedCard.Use(result.gameObject.GetComponent<Enemy>());                            
                            selectedCard = null;
                        }
                        break;
                    }
                    else
                    {
                        if (selectedCard != null) selectedCard.UnSelect();
                        selectedCard = null;
                    }
                }
            }
        }
    }
    public void ChangeHP(int value)
    {
        currentHP += value;
        hpText.text = currentHP.ToString() + "/" + MaxHP;
        m_slider.value = currentHP;
    }
}
