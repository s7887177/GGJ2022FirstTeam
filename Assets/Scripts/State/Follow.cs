using UnityEngine;

public class Follow : IState
{
    private Transform target;
    private Enemy enemy;
    // TODO: Move this to enemy
    private float cooldown = 1.5f;

    public Follow(Enemy enemy, Transform target)
    {
        this.target = target;
        this.enemy = enemy;
    }

    public void OnEnter() { }
    public void OnExit() { }

    public void Update(float delta)
    {
        this.cooldown -= delta;
        if (this.cooldown < 0)
        {
            this.enemy.Attack();
            this.cooldown += 1.5f;
        }
        this.enemy.Move(this.target.position - this.enemy.transform.position);
    }
}
