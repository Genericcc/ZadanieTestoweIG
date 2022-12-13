using System.Collections.Generic;
using HexGrids.Nodes;
using Units;

namespace PathFinders.HexGrids
{
    public interface IHexPathFinder
    {
        List<HexNode> GetWalkableNodes(HexNode start, Unit unit);
    }
}