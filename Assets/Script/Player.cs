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
                //マウスのある位置を取得(スクリーン座標)
                Vector3 MousePoint = Input.mousePosition;
                //スクリーン座標をワールド座標に変換
                MousePoint = Camera.main.ScreenToWorldPoint(MousePoint);

                //マウスのある位置から、奥(0, 0, 1)に向かってRayを発射（ワールド座標）
                RaycastHit2D[] hit2D = Physics2D.RaycastAll(MousePoint, Vector3.forward);

                //Rayがhitしたオブジェクトに目的のオブジェクトがあるかチェック
                foreach (RaycastHit2D hit in hit2D)
                {
                    //なにかと衝突した時だけそのオブジェクトの名前をログに出す
                    if (hit.collider)
                    {
                        Debug.Log("クリック対象：" + hit.collider.gameObject.name);
                        //中身の確認処理;

                        // カードをクリックした場合
                        if (hit.collider.gameObject.CompareTag("Card"))
                        {
                            if (selectedCard != null)
                            {
                                selectedCard.UnSelect();
                                if (selectedCard.gameObject == hit.collider.gameObject)
                                {
                                    selectedCard = null;
                                    return;
                                }
                            }
                            selectedCard = hit.collider.gameObject.GetComponent<Card>();
                            // カードを選択状態にする
                            if (selectedCard.isSelected == false && DeckManager.Instance.isSelectedHand == false)
                            {
                                selectedCard.Select();
                            }
                            return;
                        }
                        else if (hit.collider.gameObject.CompareTag("Enemy"))
                        {
                            // カードを選択
                            if (selectedCard != null && selectedCard.isSelected && DeckManager.Instance.CurrentCost > 0)
                            {
                                selectedCard.Use(hit.collider.gameObject.GetComponent<Enemy>());
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
