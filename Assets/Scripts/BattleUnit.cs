using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Feng.Battle
{

    public class BattleUnit : MonoBehaviour
    {
        [SerializeField] public Collider2D attackRange;
        [SerializeField] public Collider2D damageRange;
        [SerializeField] HealthBarUI healthBarUI;
        [SerializeField] int maxHp;
        [SerializeField] int currentHp;
        [SerializeField] string targetTag;
        public int Atk;


        float hpPresentage => (float)currentHp / (float)maxHp;

        void Start()
        {
            healthBarUI.UpdateHealthBarUI(hpPresentage);
        }
        void Update()
        {
            healthBarUI.UpdateHealthBarUI(hpPresentage);
        }
        public void PerformAttack()
        {
            print(targetTag);
            AttackSystem.instance.PerformAttack(this.Atk, this.attackRange.bounds, targetTag);
        }

        public void OnDamage(int atk)
        {
            var nextHp = this.currentHp - atk;
            nextHp = nextHp <= 0 ? 0 : nextHp;
            this.currentHp = nextHp;

            // Update UI
            healthBarUI.UpdateHealthBarUI(hpPresentage);
            if (nextHp == 0)
            {
                OnDie();
            }
        }

        public void OnDie()
        {
           

            transform.DOScale(Vector3.zero,1).OnComplete(() => GameObject.Destroy(this.gameObject));

        }
    }

    public class PercentageOutOfRangeException : System.ArgumentOutOfRangeException
    {

    }
}