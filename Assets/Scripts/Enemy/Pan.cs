using UnityEngine;
using System;

public class Pan : Enemy
{
    private struct Transition
    {
        public string from;
        public string to;
        public Func<bool> condition;

        public Transition(string from, string to, Func<bool> condition)
        {
            this.from = from;
            this.to = to;
            this.condition = condition;
        }
    }

    public float wanderLeft = 0;
    public float wanderRight = 5;
    public float followLeft;
    public float followRight;

    private IState wander;
    private IState follow;

    void Start()
    {
        this.wander = new Wander(this, this.wanderLeft, this.wanderRight);
    }

    void Update()
    {
        var currX = transform.position.x;
        if (this.followLeft <= currX && currX <= this.followRight && this.seePlayer())
        {
            this.follow.Update(Time.deltaTime);
        }
        else
        {
            this.wander.Update(Time.deltaTime);
        }
    }

    public override void Move(Vector2 delta)
    {
        var flip = delta.x < 0;
        delta = delta.normalized;
        transform.eulerAngles = new Vector3(0, flip ? 180 : 0, 0);
        var rb2d = GetComponent<Rigidbody2D>();
        var newVel = rb2d.velocity;
        newVel.x = delta.x;
        rb2d.velocity = newVel;
    }

    public override void Attack() { }

    private bool seePlayer()
    {
        return false;
    }
}
