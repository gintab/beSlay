using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : SingletonMonoBehaviour<BattleManager>
{
    public static event Action OnBattleStart;
    public static event Action OnBattleStop;
    public static event Action OnBattleComplete;
    public static event Action OnTurnChanged;
    public static event Action OnEnemyDead;

    public void BattleStart()
    {
        OnBattleStart?.Invoke();

    }
    public void TurnChange()
    {
        OnTurnChanged?.Invoke();
    }
}
