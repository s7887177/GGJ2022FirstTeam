using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [field: SerializeField]
    public EnemyStat stat { get; private set; }

    public abstract void Move(Vector2 delta);
    public abstract void Attack();
}
