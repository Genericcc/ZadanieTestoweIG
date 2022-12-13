using UnityEngine;
using Zenject;

namespace PathFinders.HexGrids
{
    [CreateAssetMenu(fileName = "HexPathFinderInstaller", menuName = "PathFinders/HexPathFinderInstaller", order = 0)]
    public class HexPathFinderInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IHexPathFinder>().To<HexPathFinder>().AsSingle();
        }
    }
}