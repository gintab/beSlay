using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllManager : SingletonMonoBehaviour<AllManager>
{
    
    public bool gameIsOver;
    TurnManager m_turnManager;
    DeckManager m_deckManager;

    // ゲームの開始処理
    void Start()
    {
        gameIsOver = false;
        m_turnManager = TurnManager.Instance;
        m_deckManager = DeckManager.Instance;
        BattleStart();
    }

    public void BattleStart()
    {
        m_deckManager.StartBattle();
        m_turnManager.StartBattle();
    }
    /// <summary>
    /// 数秒待ってから実行する
    /// </summary>
    /// <param name="time">時間</param>
    /// <param name="action">実行内容</param>
    /// <returns></returns>
    public IEnumerator WaitTimeCoroutine(float time,Action action)
    {
        yield return new WaitForSeconds(time);
        //time秒待ってから処理を実行する
        action();
    }
    internal static T GetRandom<T>(List<T> Params)
    {
        return Params[UnityEngine.Random.Range(0, Params.Count)];
    }
}
