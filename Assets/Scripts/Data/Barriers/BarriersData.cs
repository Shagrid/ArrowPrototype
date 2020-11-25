using System;
using System.Linq;
using ExampleTemplate;
using UnityEngine;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "BarriersData", menuName = "Data/Barriers/BarriersData")]
    public class BarriersData : ScriptableObject
    {
        [SerializeField] private BarrierInfo[] _barriers;

        public float GetCoefficient(BarrierType barrierType)
        {
            var result = _barriers.SingleOrDefault(x => x.BarrierType == barrierType);
            if (result == null)
                throw new ArgumentException($"Нет данных для препятствия: {barrierType}");
            return result.Coefficient;
        }
    }
}