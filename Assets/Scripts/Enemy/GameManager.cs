using System.Linq;
using UnityEngine;
namespace Feng
{
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
        private bool isLose;

        private void Update()
        {
            if (isWin || isLose) return;
            if (enemyCount <= 0)
            {
                isWin = true;
                OnWin();
            }
            var player = GameObject.FindGameObjectsWithTag("Player");
            if(player.Count() <= 0)
            {
                isLose = true;
                OnLose();
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
        public void OnLose()
        {
            var flowCharts = GameObject.FindObjectsOfType<Fungus.Flowchart>();
            foreach (var flowchart in flowCharts)
            {
                flowchart.ExecuteBlock("Lose");
            }
            
        }

    }
}
