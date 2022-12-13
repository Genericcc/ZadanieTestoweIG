using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HexGrids;
using HexGrids.Nodes;
using PathFinders;
using PathFinders.HexGrids;
using UnityEngine;
using Utils;
using Zenject;

namespace Units.Navigators
{
    public class UnitNavigator : IUnitNavigator
    {
        private IHexPathFinder _hexPathFinder;
        private Unit _unit;
        private HexNode _hexNode;

        private UnitData UnitData => _unit.UnitData;
        
        [Inject]
        public void Construct(Unit setUnit, IHexPathFinder setHexPathFinder)
        {
            _unit = setUnit;
            _hexPathFinder = setHexPathFinder;
        }
        
        public void Reset()
        {
            if (_hexNode != null)
            {
                UnLockNodes();
            }
            
            _hexNode = null;
        }

        public void SetHexNode(HexNode setHexNode)
        {
            if (_hexNode != null)
            {
                UnLockNodes();
            }
            
            _hexNode = setHexNode;
            LockNodes();
            SetPosition();
        }

        private void SetPosition()
        {
            var hexNodes = GetOccupiedNodes();
            var position = Vector3.zero;

            foreach (var hexNode in hexNodes)
            {
                position += hexNode.transform.position;
            }

            position /= hexNodes.Count();

            _unit.transform.position = position;
        }
        
        public IEnumerable<HexNode> GetOccupiedNodes()
        {
            return HexagonMath.GetNodesUnder(UnitData, _hexNode);
        }

        private void LockNodes()
        {
            foreach (var occupiedNode in GetOccupiedNodes())
            {
                occupiedNode.Unit = _unit;
            }
        }

        private void UnLockNodes()
        {
            foreach (var occupiedNode in GetOccupiedNodes())
            {
                occupiedNode.Unit = null;
            }
        }
        
        public List<HexNode> GetWalkableNodes()
        {
            return _hexPathFinder.GetWalkableNodes(_hexNode, _unit);
        }
    }
}