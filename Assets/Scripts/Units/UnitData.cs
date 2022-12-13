using UnityEngine;
using Utils;

namespace Units
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "Units/UnitData", order = 0)]
    public class UnitData : ScriptableObject
    {
        public UnitSizeType unitSizeType = UnitSizeType.OneHex;
        public float movementPoints = 10;
        public LookDirection lookDirection;
    }
}