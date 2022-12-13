using HexGrids.Nodes;
using UnityEngine;
using Zenject;

namespace Units.Manage
{
    public class UnitMemoryPool : MonoMemoryPool<UnitData, HexNode, Unit>
    {
        protected override void OnSpawned(Unit item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(Unit item)
        {
            item.gameObject.SetActive(false);
        }

        protected override void OnCreated(Unit item)
        {
            item.gameObject.SetActive(false);
        }

        protected override void OnDestroyed(Unit item)
        {
            Object.Destroy(item.gameObject);
        }
    }
}