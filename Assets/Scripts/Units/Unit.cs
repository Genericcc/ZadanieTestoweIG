using System;
using System.Collections.Generic;
using HexGrids.Nodes;
using Units.Navigators;
using UnityEngine;
using Zenject;

namespace Units
{
    public class Unit : MonoBehaviour, IPoolable<UnitData, HexNode, IMemoryPool>, IDisposable
    {
        private IMemoryPool _memoryPool;

        private MeshRenderer _meshRenderer;

        private IUnitNavigator _unitNavigator;

        private UnitData _unitData;
        public UnitData UnitData => _unitData;

        public int Id => gameObject.GetInstanceID();

        public MeshRenderer MeshRenderer
        {
            get
            {
                if (_meshRenderer == null) _meshRenderer = GetComponent<MeshRenderer>();

                return _meshRenderer;
            }
        }
        
        public class Factory : PlaceholderFactory<UnitData, HexNode, Unit>
        {
        }

        public void Dispose()
        {
            _memoryPool.Despawn(this);
        }

        public void OnDespawned()
        {
            _unitNavigator.Reset();
            _memoryPool = null;
            Destroy(_unitData);
        }

        public void OnSpawned(UnitData setUnitData, HexNode setHexNode, IMemoryPool setMemoryPool)
        {
            _memoryPool = setMemoryPool;
            _unitData = Instantiate(setUnitData);
            _unitNavigator.SetHexNode(setHexNode);
            MeshRenderer.material.color = Color.yellow;
        }

        [Inject]
        public void Construct(IUnitNavigator setUnitNavigator)
        {
            _unitNavigator = setUnitNavigator;
        }

        public List<HexNode> GetWalkableNodes()
        {
            return _unitNavigator.GetWalkableNodes();
        }

        public void Die()
        {
            Dispose();
        }
    }
}