using HexGrids.Nodes;
using UnityEngine;
using Zenject;

namespace Units.Manage
{
    public class UnitManager : MonoBehaviour
    {
        private Unit.Factory _unitFactory;

        [Inject]
        public void Construct(Unit.Factory unitMemoryPool)
        {
            _unitFactory = unitMemoryPool;
        }

        public Unit Spawn(UnitData unitData, HexNode hexNode)
        {
            var unit = _unitFactory.Create(unitData, hexNode);
            
            return unit;
        }
    }
}