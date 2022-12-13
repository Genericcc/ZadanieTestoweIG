using System;
using System.Collections.Generic;
using System.Linq;
using HexGrids.Nodes;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace HexGrids
{
    [CustomEditor(typeof(HexGrid))]
    public class HexGridEditor : Editor
    {
        private HexNode _prefab;
        private Vector2Int _size;
        private float _scale = 1;
        private HexOrientation _hexOrientation;
        
        void OnEnable()
        {
            var prefabPaths = AssetDatabase.FindAssets("HexNodePrefab");
            if (prefabPaths.Any())
            {
                _prefab = AssetDatabase.LoadAssetAtPath<HexNode>(AssetDatabase.GUIDToAssetPath(prefabPaths[0]));
            }

            _size = new Vector2Int(10, 8);
            _scale = 0.75f;
        }

        public override void OnInspectorGUI()
        {
            _prefab = (HexNode)EditorGUILayout.ObjectField("Prefab", _prefab, typeof(HexNode), false);
            _size = EditorGUILayout.Vector2IntField("Size", _size);
            _scale = EditorGUILayout.FloatField("Scale", _scale);
            _hexOrientation = (HexOrientation)EditorGUILayout.EnumPopup("HexOrientation", _hexOrientation);
            
            if (GUILayout.Button("Create"))
            {
                CreateGrid();
            }
            
            if (GUILayout.Button("Clear"))
            {
                Clear();
            }
        }

        private void CreateGrid()
        {
            Clear();
            switch (_hexOrientation)
            {
                case HexOrientation.PointyTop:
                    for (var y = 0; y < _size.y; y++)
                    {
                        var rOffset = Mathf.FloorToInt((float) y / 2); // or r>>1
                        for (var x = -rOffset; x < _size.x - rOffset; x++)
                            CreateObject(new HexCoordinates(new AxialCoordinates(x, y)));
                    }
                    
                    break;
                case HexOrientation.FlatTop:
                    for (var x = 0; x < _size.x; x++)
                    {
                        var yOffset = Mathf.FloorToInt((float) x / 2);
                        for (var y = -yOffset; y < _size.y - yOffset; y++)
                            CreateObject(new HexCoordinates(new AxialCoordinates(x, y)));
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Clear()
        {
            var targetTransform = (target as HexGrid)?.transform;
            if(targetTransform == null) return;

            var children = targetTransform.GetComponentsInChildren<HexNode>();
            foreach (var child in children)
            {
                DestroyImmediate(child.gameObject);
            }
        }

        private void CreateObject(HexCoordinates hexCoordinates)
        {
            var targetTransform = (target as HexGrid)?.transform;
            
            var hexNode = Instantiate(_prefab, targetTransform);
            var objTransform = hexNode.transform;
            var prefabTransform = _prefab.transform;
            
            objTransform.localRotation = prefabTransform.localRotation;
            objTransform.localScale = prefabTransform.localScale;
            hexNode.transform.position = HexagonMath.ToPosition(_scale, _hexOrientation, hexCoordinates);
            
            SetCoords(hexNode, hexCoordinates);
            
            hexNode.name = $"HexNode_{hexNode.HexCoordinates.AxialCoordinates.Q}_{hexNode.HexCoordinates.AxialCoordinates.R}";
        }

        private void SetCoords(HexNode hexNode, HexCoordinates hexCoordinates)
        {
            var field = hexNode.GetType().GetField("hexCoordinates", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            field.SetValue(hexNode, hexCoordinates);
        }

    }
}