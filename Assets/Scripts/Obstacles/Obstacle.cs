using System;
using System.Collections.Generic;
using System.Linq;
using HexGrids;
using HexGrids.Nodes;
using UnityEngine;
using Zenject;

namespace Obstacles
{
    [RequireComponent(typeof(BoxCollider))]
    public class Obstacle : MonoBehaviour
    {
        private HexGrid _hexGrid;
        private BoxCollider _boxCollider;
        private List<HexNode> _hexNodes;
        
        public BoxCollider BoxCollider
        {
            get
            {
                if (_boxCollider == null)
                {
                    _boxCollider = GetComponent<BoxCollider>();
                }

                return _boxCollider;
            }
        }

        [Inject]
        public void Construct(HexGrid setHexGrid)
        {
            _hexGrid = setHexGrid;
        }
        
        private void OnEnable()
        {
            _hexNodes = _hexGrid.GetNodesInRange(BoxCollider);
            foreach (var hexNode in _hexNodes)
            {
                hexNode.Obstacle = this;
            }
        }

        private void OnDisable()
        {
            if (_hexNodes != null)
            {
                foreach (var hexNode in _hexNodes.Where(x => x != null))
                {
                    hexNode.Obstacle = null;
                }
            }
        }
    }
}