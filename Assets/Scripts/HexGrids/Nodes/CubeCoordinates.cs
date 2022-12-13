using System;
using UnityEngine;

namespace HexGrids.Nodes
{
    [Serializable]
    public struct CubeCoordinates : IEquatable<CubeCoordinates>
    {
        [SerializeField] private int q;

        [SerializeField] private int r;

        [SerializeField] private int s;

        public CubeCoordinates(int q, int r, int s)
        {
            this.q = q;
            this.r = r;
            this.s = s;
        }

        public int Q => q;
        public int R => r;
        public int S => s;

        public static implicit operator CubeCoordinates(Vector3Int v) => new(v.x, v.y, v.z);

        public static implicit operator Vector3Int(CubeCoordinates c) => new(c.q, c.r, c.s);

        public static implicit operator CubeCoordinates(AxialCoordinates c) => new(c.Q, -c.Q - c.R, c.R);

        public static implicit operator CubeCoordinates(HexCoordinates c) => c.CubeCoordinates;
        
        public static CubeCoordinates operator +(CubeCoordinates c1, CubeCoordinates c2) =>
            new(c1.Q + c2.Q, c1.R + c2.R, c1.S + c2.S);

        public static CubeCoordinates operator -(CubeCoordinates c1, CubeCoordinates c2) =>
            new(c1.Q - c2.Q, c1.R - c2.R, c1.S - c2.S);

        public static bool operator ==(CubeCoordinates c1, CubeCoordinates c2) => c1.Equals(c2);

        public static bool operator !=(CubeCoordinates c1, CubeCoordinates c2) => !c1.Equals(c2);
        
        public bool Equals(CubeCoordinates other) => q == other.q && r == other.r && s == other.s;
        
        public override bool Equals(object obj) => obj is CubeCoordinates other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(q, r, s);
    }
}