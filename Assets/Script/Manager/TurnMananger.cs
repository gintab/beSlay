using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : SingletonMonoBehaviour<TurnManager>
{
    public enum Turn
    {
        Ally,
        Enemy
    }

    public Turn currentTurn;
    public List<GameObject> DisabledUIOnEnemyTurn;
    private Action OnEnemyTurn;
    private Action OnAllyTurn;

    // �^�[���̐؂�ւ�
    public void SwitchTurn()
    {
        if (currentTurn == Turn.Ally)
        {
            currentTurn = Turn.Enemy;
            for (int ii = 0; ii < DisabledUIOnEnemyTurn.Count; ii++)
            {
                DisabledUIOnEnemyTurn[ii].SetActive(false);
            }
            OnEnemyTurn?.Invoke();
            SwitchTurn();
        }
        else
        {
            currentTurn = Turn.Ally;
            for (int ii = 0; ii < DisabledUIOnEnemyTurn.Count; ii++)
            {
                DisabledUIOnEnemyTurn[ii].SetActive(true);
            }
            
            OnAllyTurn?.Invoke();
        }

        Debug.Log("Turn switched to: " + currentTurn);
    }

    // �Q�[���J�n���̏����ݒ�
    public void StartBattle()
    {
        currentTurn = Turn.Ally;
        OnEnemyTurn += DeckManager.Instance.DisposeHandAll;
        OnEnemyTurn += EnemyManager.Instance.AttackEnemies;
        //�؂�ւ���
        DeckManager Instance = DeckManager.Instance;
        OnAllyTurn += Instance.ResetCost;//�R�X�g������
        Instance.DrawCard(Instance.drawNum);
        Debug.Log("Game started. Turn: " + currentTurn);
    }
    // �e�X�g�p�̃^�[���؂�ւ��{�^���Ȃǂ�ǉ�����ꍇ�AUpdate()���ɂ��̏�����ǉ����܂�
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         SwitchTurn();
    //     }
    // }
}
