using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Feng.Battle
{
    [RequireComponent(typeof(Image))]
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] Image image;
        [SerializeField] Image image2;
        [SerializeField] private Canvas canvas;

        private void Start()
        {
            this.canvas.worldCamera = Camera.main;
        }

        public void UpdateHealthBarUI(float percentage)
        {
            if (percentage < 0 || percentage > 1)
            {
                throw new PercentageOutOfRangeException();
            }

            float current = image.fillAmount;
            image.DOFillAmount(percentage, 0.05f);
            image2.DOFillAmount( percentage, 0.5f );
            image.color = new Color(1 - percentage, percentage, 0);


        }
    }
}