using System;
using System.Collections;
using System.Collections.Generic;
using Obstacles;
using Units;
using UnityEngine;
using Zenject;

namespace HexGrids.Nodes
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class HexNode : MonoBehaviour, IEquatable<HexNode>
    {
        [SerializeField] private HexCoordinates hexCoordinates;
        public HexCoordinates HexCoordinates => hexCoordinates;

        [SerializeField] private float penalty = 1;

        [SerializeField] private bool isWalkable;
        public bool IsWalkable => isWalkable && _obstacle == null;

        private SpriteRenderer _spriteRenderer;

        public SpriteRenderer SpriteRenderer
        {
            get
            {
                if (_spriteRenderer == null)
                {
                    _spriteRenderer = GetComponent<SpriteRenderer>();
                }

                return _spriteRenderer;
            }
        }

        [SerializeField] private Color defaultColor = Color.white;
        [SerializeField] private Color walkable = Color.green;
        [SerializeField] private Color notWalkable = Color.red;
        
        private Unit _unit;

        public Unit Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                Clear();
            }
        }
        
        private Obstacle _obstacle;

        public Obstacle Obstacle
        {
            get => _obstacle;
            set
            {
                _obstacle = value;
                Clear();
            }
        }

        private HexGrid _hexGrid;

        [Inject]
        public void Construct(HexGrid setHexGrid)
        {
            _hexGrid = setHexGrid;
        }

        private void Start()
        {
            Clear();
        }

        public float GetPenalty()
        {
            return penalty;
        }

        public IEnumerable<HexNode> GetNeighbours()
        {
            return _hexGrid.GetNeighbours(this);
        }

        public HexNode GetLeftNeighbour()
        {
            return _hexGrid.GetLeftNeighbour(this);
        }

        public HexNode GetRightNeighbour()
        {
            return _hexGrid.GetRightNeighbour(this);
        }

        private void SetColor(Color color)
        {
            SpriteRenderer.color = color;
        }

        public bool Equals(HexNode other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return hexCoordinates.Equals(other.hexCoordinates);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            if (obj is HexNode hexNode)
            {
                return Equals(hexNode);
            }

            return false;
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return hexCoordinates.GetHashCode();
        }

        public void ShowWalkable()
        {
            SetColor(walkable);
        }

        public void Clear()
        {
            SetColor(IsWalkable && _unit == null ? defaultColor : notWalkable);   
        }
    }
}