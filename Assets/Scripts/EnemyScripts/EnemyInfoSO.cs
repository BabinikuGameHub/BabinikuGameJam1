using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "My Game/Enemy Data")]
public class EnemyInfoSO : ScriptableObject
{
    public EnemyType EnemyType;
    public int MonsterLevel;

    public float MovementSpeed;
    public float MinimumDistance = 20f;
    public float RateOfFire;

    public Sprite _aliveEnemySprite;
    public Sprite _deadEnemySprite;

}
