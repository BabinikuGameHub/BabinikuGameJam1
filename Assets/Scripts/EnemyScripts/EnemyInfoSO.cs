using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfoSO : ScriptableObject
{
    EnemyType EnemyType;
    int MonsterLevel;

    float MovementSpeed;
    float MinimumDistance = 20f;
    float RateOfFire;

    Sprite _aliveEnemySprite;
    Sprite _deadEnemySprite;

}
