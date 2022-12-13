using System;
using System.Collections.Generic;
using System.Linq;
using Collections;
using HexGrids.Nodes;
using Units;

namespace PathFinders.HexGrids
{
    public class HexPathFinder : IHexPathFinder
    {
        public List<HexNode> GetWalkableNodes(HexNode start, Unit unit)
        {
            var maxCost = unit.UnitData.movementPoints;

            var cameFrom = new Dictionary<HexNode, HexNode>();
            var frontier = new PriorityQueue<SearchNode>();
            var costSoFar = new Dictionary<HexNode, float>();

            cameFrom.Add(start, default);
            frontier.Enqueue(new SearchNode(start, 0));
            costSoFar.Add(start, 0);

            while (frontier.Count != 0)
            {
                var current = frontier.Dequeue();
                var currentCost = costSoFar[current.hexNode];
                var neighbours = current.hexNode.GetNeighbours().Where(x => CanWalkOn(x, unit));

                foreach (var neighbour in neighbours)
                {
                    var newCost = currentCost + GetHexCost(neighbour);
                    if (newCost > maxCost) continue;
                    if (costSoFar.TryGetValue(neighbour, out var csf) && newCost >= csf) continue;

                    if (IsUnitTwoHex(unit) && !CanTwoHexUnitPass(unit, neighbour, current)) continue;

                    costSoFar[neighbour] = newCost;
                    cameFrom[neighbour] = current.hexNode;
                    frontier.Enqueue(new SearchNode(neighbour, newCost));
                }
            }
            if (!IsUnitTwoHex(unit)) return cameFrom.Keys.Where(x => x.Unit != unit).ToList(); 

            List<HexNode> walkableNodes = CreateWalkableNodesForTwoHexUnit(cameFrom.Keys.ToList(), unit, costSoFar);
            return walkableNodes.Where(x => x.Unit != unit).ToList();
        }

        private float GetHexCost(HexNode hexNode)
        {
            return hexNode.GetPenalty();
        }

        private bool IsUnitTwoHex(Unit unit)
        {
            return unit.UnitData.unitSizeType == UnitSizeType.TwoHex;
        }

        private bool CanTwoHexUnitPass(Unit unit, HexNode currentHexNode, SearchNode cameFromHexNode)
        {
            HexNode leftNeighbour = currentHexNode.GetLeftNeighbour();
            HexNode rightNeighbour = currentHexNode.GetRightNeighbour();

            HexNode neighbour = unit.UnitData.lookDirection == Utils.LookDirection.Right ? leftNeighbour : rightNeighbour;

            return CanWalkOn(neighbour, unit) && IsInRange(neighbour, unit, cameFromHexNode.cost);
        }

        private bool CanWalkOn(HexNode hexNode, Unit unit)
        {
            return hexNode != null && (hexNode.IsWalkable || hexNode.Unit == unit);
        }

        private bool IsInRange(HexNode hexNode, Unit unit, float cost)
        {
            return hexNode != null && cost <= unit.UnitData.movementPoints;
        }

        private List<HexNode> CreateWalkableNodesForTwoHexUnit(List<HexNode> walkableHeadNodes, Unit unit, Dictionary<HexNode, float> costSoFar)
        {            
            List<HexNode> result = new List<HexNode>();

            foreach (HexNode hexNode in walkableHeadNodes)
            {
                result.Add(hexNode);

                HexNode leftNeighbour =  hexNode.GetLeftNeighbour();
                HexNode rightNeighbour = hexNode.GetRightNeighbour();
                HexNode tailNeighbour = unit.UnitData.lookDirection == Utils.LookDirection.Right ? leftNeighbour : rightNeighbour;

                if ((CanWalkOn(tailNeighbour, unit)) && IsInRange(tailNeighbour, unit, costSoFar[hexNode]))
                {
                    result.Add(tailNeighbour);
                }
            }
            return result;
        }

        private readonly struct SearchNode : IComparable<SearchNode>
        {
            public readonly HexNode hexNode;
            public readonly float cost;
         
            public SearchNode(HexNode setHexNode, float setCost)
            {
                hexNode = setHexNode;
                cost = setCost;
            }
            
            public int CompareTo(SearchNode other) => cost.CompareTo(other.cost);
        }
    }
}