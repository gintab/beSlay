using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllManager : SingletonMonoBehaviour<AllManager>
{
    
    public bool gameIsOver;
    TurnManager m_turnManager;
    DeckManager m_deckManager;

    // �Q�[���̊J�n����
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
    /// ���b�҂��Ă�����s����
    /// </summary>
    /// <param name="time">����</param>
    /// <param name="action">���s���e</param>
    /// <returns></returns>
    public IEnumerator WaitTimeCoroutine(float time,Action action)
    {
        yield return new WaitForSeconds(time);
        //time�b�҂��Ă��珈�������s����
        action();
    }
    internal static T GetRandom<T>(List<T> Params)
    {
        return Params[UnityEngine.Random.Range(0, Params.Count)];
    }
}
