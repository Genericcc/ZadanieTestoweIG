using System;
using UnityEngine;

namespace HexGrids.Nodes
{
    [Serializable]
    public struct HexCoordinates : IEquatable<HexCoordinates>
    {
        [SerializeField] private AxialCoordinates axialCoords;
        [SerializeField] private CubeCoordinates cubeCoordinates;
        
        public AxialCoordinates AxialCoordinates => axialCoords;
        public CubeCoordinates CubeCoordinates => cubeCoordinates;
        
        public HexCoordinates(AxialCoordinates setAxialCoords)
        {
            axialCoords = setAxialCoords;
            cubeCoordinates = axialCoords;
        }

        public HexCoordinates(CubeCoordinates setCubeCoordinates)
        {
            cubeCoordinates = setCubeCoordinates;
            axialCoords = cubeCoordinates;
        }
        
        public static implicit operator HexCoordinates(AxialCoordinates c) => new(c);
        
        public static implicit operator HexCoordinates(CubeCoordinates c) => new(c);
        
        public static HexCoordinates operator +(HexCoordinates c1, HexCoordinates c2) => new(c1.axialCoords + c2.axialCoords);

        public static HexCoordinates operator -(HexCoordinates c1, HexCoordinates c2) => new(c1.axialCoords - c2.axialCoords);

        public static bool operator ==(HexCoordinates c1, HexCoordinates c2) => c1.Equals(c2);

        public static bool operator !=(HexCoordinates c1, HexCoordinates c2) => !c1.Equals(c2);

        public bool Equals(HexCoordinates other) => axialCoords.Equals(other.axialCoords);

        public override bool Equals(object obj) => obj is HexCoordinates other && Equals(other);

        public override int GetHashCode() => axialCoords.GetHashCode();
    }
}