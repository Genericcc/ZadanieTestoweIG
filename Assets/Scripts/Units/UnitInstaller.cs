using Units.Navigators;
using UnityEngine;
using Zenject;

namespace Units
{
    [CreateAssetMenu(fileName = "UnitInstaller", menuName = "Units/UnitInstaller", order = 0)]
    public class UnitInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Unit>().FromComponentOnRoot().AsSingle();
            Container.Bind<IUnitNavigator>().To<UnitNavigator>().AsSingle();
        }
    }
}