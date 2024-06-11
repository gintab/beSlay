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

    // ターンの切り替え
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

    // ゲーム開始時の初期設定
    public void StartBattle()
    {
        currentTurn = Turn.Ally;
        OnEnemyTurn += DeckManager.Instance.DisposeHandAll;
        OnEnemyTurn += EnemyManager.Instance.AttackEnemies;
        //切り替え時
        DeckManager Instance = DeckManager.Instance;
        OnAllyTurn += Instance.ResetCost;//コスト初期化
        Instance.DrawCard(Instance.drawNum);
        Debug.Log("Game started. Turn: " + currentTurn);
    }
    // テスト用のターン切り替えボタンなどを追加する場合、Update()内にその処理を追加します
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         SwitchTurn();
    //     }
    // }
}
