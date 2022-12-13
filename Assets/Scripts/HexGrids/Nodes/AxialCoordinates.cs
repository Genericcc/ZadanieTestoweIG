using System;
using UnityEngine;

namespace HexGrids.Nodes
{
    [Serializable]
    public struct AxialCoordinates : IEquatable<AxialCoordinates>
    {
        [SerializeField] private int q;
        [SerializeField] private int r;

        public AxialCoordinates(int q, int r)
        {
            this.q = q;
            this.r = r;
        }

        public int Q => q;
        public int R => r;
        

        public static implicit operator AxialCoordinates(Vector2Int v) => new(v.x, v.y);

        public static implicit operator Vector2Int(AxialCoordinates c) => new(c.q, c.r);

        public static implicit operator AxialCoordinates(CubeCoordinates c) => new(c.Q, c.S);

        public static implicit operator AxialCoordinates(HexCoordinates c) => c.AxialCoordinates;

        public static AxialCoordinates operator +(AxialCoordinates c1, AxialCoordinates c2) => new(c1.Q + c2.Q, c1.R + c2.R);

        public static AxialCoordinates operator -(AxialCoordinates c1, AxialCoordinates c2) => new(c1.Q - c2.Q, c1.R - c2.R);

        public static bool operator ==(AxialCoordinates c1, AxialCoordinates c2) => c1.Equals(c2);

        public static bool operator !=(AxialCoordinates c1, AxialCoordinates c2) => !c1.Equals(c2);
        
        public bool Equals(AxialCoordinates other) => q == other.q && r == other.r;

        public override bool Equals(object obj) => obj is AxialCoordinates other && Equals(other);
        
        public override int GetHashCode() => HashCode.Combine(q, r);
    }
}