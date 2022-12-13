using HexGrids.Nodes;
using Units.Signals;
using UnityEngine;
using Zenject;

namespace Units.Manage
{
    [CreateAssetMenu(fileName = "UnitManagerInstaller", menuName = "Units/UnitManagerInstaller", order = 0)]
    public class UnitManagerInstaller : ScriptableObjectInstaller
    {
        public Unit unitPrefab;

        public override void InstallBindings()
        {
            Container.Bind<UnitManager>().FromNewComponentOnNewGameObject()
                .AsSingle()
                .NonLazy();
            
            Container.BindFactory<UnitData, HexNode, Unit, Unit.Factory>().FromMonoPoolableMemoryPool(
                x => x.WithInitialSize(1).FromComponentInNewPrefab(unitPrefab));
            
            Container.DeclareSignal<UnitSpawnedSignal>();
        }
    }
}