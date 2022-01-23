using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Apple : Enemy
{
    public float wanderLeft = 0;
    public float wanderRight = 5;

    private IState state;

    void Start()
    {
        this.state = new Wander(this, this.wanderLeft, this.wanderRight);
        this.state.OnEnter();
    }

    void Update()
    {
        this.state.Update(Time.deltaTime);
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
}
