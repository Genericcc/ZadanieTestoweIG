using System.Collections.Generic;
using Obstacles;
using Units;
using UnityEngine;

namespace Tests
{
    public class TestSet : MonoBehaviour
    {
        private Unit unit;
        
        [SerializeField]
        private UnitSpawner unitSpawner;
        
        [SerializeField]
        private List<Obstacle> obstacles;

        public void StartTest()
        {
            gameObject.SetActive(true);
            unit = unitSpawner.Spawn();
            foreach (var obstacle in obstacles)
            {
                obstacle.gameObject.SetActive(true);
            }
            
            ShowTilesInRange();
        }

        public void EndTest()
        {
            Clear();
            foreach (var obstacle in obstacles)
            {
                obstacle.gameObject.SetActive(false);
            }
            unit.Die();
            unit = null;
            gameObject.SetActive(false);
        }

        public void Reset()
        {
            EndTest();
            StartTest();
        }

        private void ShowTilesInRange()
        {
            var hexNodes = unit.GetWalkableNodes();
            foreach (var hexNode in hexNodes)
            {
                hexNode.ShowWalkable();
            }
        }

        private void Clear()
        {
            var hexNodes = unit.GetWalkableNodes();
            foreach (var hexNode in hexNodes)
            {
                hexNode.Clear();
            }
        }
    }
}