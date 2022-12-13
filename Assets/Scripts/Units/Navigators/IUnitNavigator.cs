using System.Collections.Generic;
using HexGrids.Nodes;

namespace Units.Navigators
{
    public interface IUnitNavigator
    {
        void Reset();
        void SetHexNode(HexNode setHexNode);
        List<HexNode> GetWalkableNodes();
    }
}