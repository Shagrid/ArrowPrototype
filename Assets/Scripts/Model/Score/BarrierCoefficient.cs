using UnityEngine;

namespace ExampleTemplate
{
    public class BarrierCoefficient : MonoBehaviour
    {
        [SerializeField] private BarrierType _barrierType;

        public float GetCoefficient()
        {
            return Data.Instance.BarriersData.GetCoefficient(_barrierType);
        }
        public float GetDamageCoefficient()
        {
            return Data.Instance.BarriersData.GetDamageCoefficient(_barrierType);
        }
    }
}