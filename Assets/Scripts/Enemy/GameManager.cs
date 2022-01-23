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
}
