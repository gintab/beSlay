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
    UnityEngine.UI.Text hpText; // HP��\������e�L�X�g
    [SerializeField]
    UnityEngine.UI.Slider m_slider;
    [SerializeField]
    UnityEngine.UI.Text sliderHPtext;

    public Transform PlayerPosOnCanvas;
    public LayerMask cardLayer; // �J�[�h�̃��C���[

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
            // �}�E�X�̍��N���b�N�����o
            if (Input.GetMouseButtonDown(0))
            {
                // �}�E�X�̈ʒu���X�N���[�����W���烏�[���h���W�ɕϊ�
                Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // ���C�L���X�g���쐬
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                pointerEventData.position = Input.mousePosition;

                // ���C�L���X�g���ʂ�ێ����郊�X�g
                var raycastResults = new List<RaycastResult>();

                // UI�C�x���g�V�X�e���Ƀ��C�L���X�g�𑗐M
                EventSystem.current.RaycastAll(pointerEventData, raycastResults);

                // ���C�L���X�g���ʂ����邩�ǂ������`�F�b�N
                if (raycastResults.Count > 0)
                {
                    foreach (var result in raycastResults)
                    {
                        // UI�v�f���N���b�N���ꂽ�ꍇ�̏���
                        Debug.Log("UI Element Clicked: " + result.gameObject.name);
                        //���g�̊m�F����;
                        // �J�[�h���N���b�N�����ꍇ
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
                            // �J�[�h��I����Ԃɂ���
                            if (selectedCard.isSelected == false && DeckManager.Instance.isSelectedHand == false)
                            {
                                selectedCard.Select();
                            }
                            return;
                        }
                        else if (result.gameObject.CompareTag("Enemy"))
                        {
                            // �J�[�h��I��
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
                    // UI�v�f�ȊO���N���b�N���ꂽ�ꍇ�̏���
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
