using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : IState
{
    private enum Dir { LEFT, RIGHT };

    private Enemy enemy;
    private float leftX;
    private float rightX;
    private Dir dir = Dir.LEFT;

    public Wander(Enemy enemy, float leftX, float rightX)
    {
        this.enemy = enemy;
        this.leftX = leftX;
        this.rightX = rightX;
    }

    public void OnEnter() { }
    public void OnExit() { }
    public void Update(float deltaTime)
    {
        if (this.dir == Dir.LEFT && this.enemy.transform.position.x < this.leftX)
            this.dir = Dir.RIGHT;
        else if (this.dir == Dir.RIGHT && this.enemy.transform.position.x > this.rightX)
            this.dir = Dir.LEFT;
        if (this.dir == Dir.LEFT)
            this.enemy.Move(Vector2.left);
        else
            this.enemy.Move(Vector2.right);
    }
}
