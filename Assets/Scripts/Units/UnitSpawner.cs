using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HexGrids.Nodes;
using Tests;
using Tests.Signals;
using Units.Manage;
using UnityEngine;
using Zenject;

namespace Units
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private HexNode hexNode;

        private TestSettings _testSettings;
        private UnitManager _unitManager;

        [Inject]
        public void Construct(UnitManager setUnitManager, TestSettings setTestSettings)
        {
            _testSettings = setTestSettings;
            _unitManager = setUnitManager;
        }

        public Unit Spawn()
        {
            var unitData = GetUnitData();
            var unit = _unitManager.Spawn(unitData, hexNode);
            return unit;
        }

        private UnitData GetUnitData()
        {
            var data = Resources.LoadAll("Data/Units", typeof(UnitData)).Cast<UnitData>().ToList();
            return data.FirstOrDefault(x =>
                x.lookDirection == _testSettings.LookDirection && x.unitSizeType == _testSettings.UnitSizeType);
        }
    }
}