using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public int enemyCount
    {
        get
        {
            return GameObject.FindObjectsOfType<Enemy>().Count();
        }
    }
    private bool isWin;

    private void Update()
    {
        if (isWin) return;
        if (enemyCount <= 0)
        {
            isWin = true;
            OnWin();
        }
    }
    public void OnWin()
    {
        var flowCharts = GameObject.FindObjectsOfType<Fungus.Flowchart>();
        foreach (var flowchart in flowCharts)
        {
            flowchart.ExecuteBlock("Win");
        }   
    }

}
public class Enemy : MonoBehaviour
{
    [field: SerializeField]
    public EnemyStat stat { get; private set; }
    public float wanderLeft = 0;
    public float wanderRight = 5;
    Fungus.Flowchart chart;
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
