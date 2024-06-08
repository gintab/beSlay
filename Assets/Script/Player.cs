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
                //�}�E�X�̂���ʒu���擾(�X�N���[�����W)
                Vector3 MousePoint = Input.mousePosition;
                //�X�N���[�����W�����[���h���W�ɕϊ�
                MousePoint = Camera.main.ScreenToWorldPoint(MousePoint);

                //�}�E�X�̂���ʒu����A��(0, 0, 1)�Ɍ�������Ray�𔭎ˁi���[���h���W�j
                RaycastHit2D[] hit2D = Physics2D.RaycastAll(MousePoint, Vector3.forward);

                //Ray��hit�����I�u�W�F�N�g�ɖړI�̃I�u�W�F�N�g�����邩�`�F�b�N
                foreach (RaycastHit2D hit in hit2D)
                {
                    //�Ȃɂ��ƏՓ˂������������̃I�u�W�F�N�g�̖��O�����O�ɏo��
                    if (hit.collider)
                    {
                        Debug.Log("�N���b�N�ΏہF" + hit.collider.gameObject.name);
                        //���g�̊m�F����;

                        // �J�[�h���N���b�N�����ꍇ
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
                            // �J�[�h��I����Ԃɂ���
                            if (selectedCard.isSelected == false && DeckManager.Instance.isSelectedHand == false)
                            {
                                selectedCard.Select();
                            }
                            return;
                        }
                        else if (hit.collider.gameObject.CompareTag("Enemy"))
                        {
                            // �J�[�h��I��
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
