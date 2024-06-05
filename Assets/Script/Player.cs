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
    UnityEngine.UI.Text hpText; // HP��\������e�L�X�g
    [SerializeField]
    UnityEngine.UI.Slider m_slider;

    public LayerMask cardLayer; // �J�[�h�̃��C���[

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
            // �}�E�X�̍��N���b�N�����o
            if (Input.GetMouseButtonDown(0))
            {
                //RaycastAll�̈����iPointerEventData�j�쐬
                PointerEventData pointData = new PointerEventData(EventSystem.current);

                //RaycastAll�̌��ʊi�[�pList
                List<RaycastResult> RayResult = new List<RaycastResult>();

                //PointerEventData�Ƀ}�E�X�̈ʒu���Z�b�g
                pointData.position = Input.mousePosition;
                //RayCast�i�X�N���[�����W�j
                EventSystem.current.RaycastAll(pointData, RayResult);

                foreach (RaycastResult result in RayResult)
                {
                    //���g�̊m�F����

                    // �J�[�h���N���b�N�����ꍇ
                    if (result.gameObject.CompareTag("Card"))
                    {
                        if (selectedCard != null)
                        {
                            selectedCard.UnSelect();
                            if (selectedCard.gameObject == result.gameObject)
                            {
                                selectedCard = null;
                                break;�@//�����J�[�h���N���b�N�����ꍇ�͉����̂�  
                            }
                        }
                        selectedCard = result.gameObject.GetComponent<Card>();
                        // �J�[�h��I����Ԃɂ���
                        if (selectedCard.isSelected == false && DeckManager.Instance.isSelectedHand == false)
                        {
                            selectedCard.Select();
                        }
                        break;
                    }
                    else if (result.gameObject.CompareTag("Enemy"))
                    {
                        // �J�[�h��I��
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
