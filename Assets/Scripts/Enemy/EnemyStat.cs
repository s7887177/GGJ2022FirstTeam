using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "enemy-stat", menuName = "Enemy/Stat")]
public class EnemyStat : ScriptableObject
{
    public int hp;
    public float speed;
    public float vision;
    public int atk;
}
