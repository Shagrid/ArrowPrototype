using System;
using UnityEngine;

namespace ExampleTemplate.Model.Score
{
    public sealed class BodyPoint : MonoBehaviour
    {
        [SerializeField] private float BaseScore = 100f;

        private void OnCollisionEnter(Collision other)
        {
            var barrier = other.transform.GetComponent<BarrierCoefficient>();
            if (barrier != null)
            {
                var points = BaseScore * barrier.GetCoefficient();
            }
        }
    }
}