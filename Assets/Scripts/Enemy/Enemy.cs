using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField]
    public EnemyStat stat { get; private set; }
    public float wanderLeft = 0;
    public float wanderRight = 5;

    private IState state;

    // Start is called before the first frame update
    void Start()
    {
        this.state = new Wander(this, this.wanderLeft, this.wanderRight);
    }

    // Update is called once per frame
    void Update()
    {
        this.state.Update(Time.deltaTime);
    }

    public void Move(Vector2 delta)
    {
        var flip = delta.x < 0;
        delta = delta.normalized;
        transform.eulerAngles = new Vector3(0, flip ? 180 : 0, 0);
        GetComponent<Rigidbody2D>().velocity = delta;
    }
}
