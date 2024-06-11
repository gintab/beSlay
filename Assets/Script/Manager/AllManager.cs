using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllManager : SingletonMonoBehaviour<AllManager>
{
    
    public bool gameIsOver;
    TurnManager m_turnManager;
    DeckManager m_deckManager;

    // ƒQ[ƒ€‚ÌŠJnˆ—
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
}
