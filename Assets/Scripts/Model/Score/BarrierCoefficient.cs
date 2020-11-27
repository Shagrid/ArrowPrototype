using UnityEngine;

namespace ExampleTemplate.Model.Score
{
    public class BarrierCoefficient : MonoBehaviour
    {
        [SerializeField] private BarrierType _barrierType;

        public float GetCoefficient()
        {
            return Data.Instance.BarriersData.GetCoefficient(_barrierType);
        }
    }
}