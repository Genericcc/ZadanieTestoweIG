using System.Collections.Generic;
using System.Linq;
using HexGrids.Nodes;
using UnityEngine;

namespace HexGrids
{
    public class HexGrid : MonoBehaviour
    {
        private static readonly HexCoordinates[] Directions = new HexCoordinates[6]
        {
            new AxialCoordinates(+0, +1),
            new AxialCoordinates(+1, +0),
            new AxialCoordinates(+1, -1),
            new AxialCoordinates(+0, -1),
            new AxialCoordinates(-1, +0),
            new AxialCoordinates(-1, +1)
        };
        
        private Dictionary<HexCoordinates, HexNode> _hexes;
        
        public HexNode this[HexCoordinates coords]
        {
            get
            {
                _hexes.TryGetValue(coords, out var hex);
                return hex;
            }
        }

        private bool _isReady = false;
        public bool IsReady => _isReady;
        
        private void Start()
        {
            _isReady = false;
            _hexes = new Dictionary<HexCoordinates, HexNode>();
            
            foreach (Transform t in transform)
            {
                var hexNode = t.gameObject.GetComponent<HexNode>();
                if (hexNode == null) continue;
                
                _hexes.Add(hexNode.HexCoordinates, hexNode);
            }

            _isReady = true;
        }
        
        public IEnumerable<HexNode> GetNeighbours(HexNode hexNode)
        {
            return Directions.Select(x => this[hexNode.HexCoordinates + x])
                .Where(x => x != null);
        }

        public HexNode GetLeftNeighbour(HexNode hexNode)
        {
            return this[hexNode.HexCoordinates + new HexCoordinates(new AxialCoordinates(-1, 0))];
        }

        public HexNode GetRightNeighbour(HexNode hexNode)
        {
            return this[hexNode.HexCoordinates + new HexCoordinates(new AxialCoordinates(1, 0))];
        }
        
        public List<HexNode> GetNodesInRange(Collider objectCollider)
        {
            if (objectCollider == null) return new List<HexNode>();
            
            return _hexes.Values.Where(x => x != null && objectCollider.bounds.Contains(x.transform.position)).ToList();
        }
        
        public List<HexNode> GetNodesInRange(IEnumerable<Collider> objectColliders)
        {
            if (objectColliders == null || !objectColliders.Any()) return new List<HexNode>();
            
            return _hexes.Values.Where(x => x != null && objectColliders.Any(z => z.bounds.Contains(x.transform.position))).ToList();
        }

    }
}