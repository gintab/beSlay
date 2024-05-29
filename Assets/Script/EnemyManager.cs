using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
{
    [SerializeField]
    List<Enemy> stageEnemies;
    private List<Enemy> currentEnemies;

    private void Start()
    {
        currentEnemies = stageEnemies;
    }
    public void StepAttackCount()
    {
        foreach (var enemy in currentEnemies)
        {
            enemy.StepCount();
        }
    }
}
