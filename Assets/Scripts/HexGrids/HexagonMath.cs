using System;
using System.Collections.Generic;
using HexGrids.Nodes;
using Units;
using UnityEngine;
using Utils;

namespace HexGrids
{
    public static class HexagonMath
    {
        public static IEnumerable<HexNode> GetNodesUnder(UnitData unitData, HexNode hexNode)
        {
            yield return hexNode;

            if (unitData.unitSizeType == UnitSizeType.OneHex) yield break;
            
            switch (unitData.lookDirection)
            {
                case LookDirection.Left:
                    yield return hexNode.GetRightNeighbour();
                    break;
                case LookDirection.Right:
                    yield return hexNode.GetLeftNeighbour();
                    break;
                default:
                    Debug.LogError($"UnitNavigator -> GetOccupiedNodes not supported LookDirection {unitData.lookDirection}");
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static Vector3 ToPosition(float scale, HexOrientation hexOrientation, HexCoordinates hexCoordinates)
        {
            var x = scale;
            var y = scale;

            var sqrt3 = Mathf.Sqrt(3);
            switch (hexOrientation)
            {
                case HexOrientation.PointyTop:
                    x *= sqrt3 * hexCoordinates.AxialCoordinates.Q + sqrt3 / 2.0f * hexCoordinates.AxialCoordinates.R;
                    y *= 3.0f / 2.0f * hexCoordinates.AxialCoordinates.R;
                    break;
                case HexOrientation.FlatTop:
                    x *= 3.0f / 2.0f * hexCoordinates.AxialCoordinates.Q;
                    y *= sqrt3 / 2.0f * hexCoordinates.AxialCoordinates.Q + sqrt3 * hexCoordinates.AxialCoordinates.R;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(hexOrientation), hexOrientation, null);
            }

            return new Vector3(x, 0, y);

        }
    }
}