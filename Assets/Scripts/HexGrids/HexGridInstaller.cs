using UnityEngine;
using Zenject;

namespace HexGrids
{
    [CreateAssetMenu(fileName = "HexGridInstaller", menuName = "HexGrid/HexGridInstaller", order = 0)]
    public class HexGridInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<HexGrid>().FromComponentInHierarchy().AsSingle();
        }
    }
}