using Tests.Signals;
using Units;
using UnityEngine;
using Zenject;

namespace Tests
{
    [CreateAssetMenu(fileName = "TestInstaller", menuName = "Tests/TestInstaller", order = 0)]
    public class TestInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.Bind<TestManager>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<TestView>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();

            Container.Bind<TestSettings>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container.DeclareSignal<TestLevelChangeSignal>();
            Container.BindSignal<TestLevelChangeSignal>()
                .ToMethod<TestManager>(x => x.ChangeTest)
                .FromResolve();
            
            Container.DeclareSignal<TestUnitSizeChangedSignal>();
            Container.BindSignal<TestUnitSizeChangedSignal>()
                .ToMethod<TestSettings>(x => x.UnitSizeChanged)
                .FromResolve();

            Container.DeclareSignal<TestLookDirectionChangedSignal>();
            Container.BindSignal<TestLookDirectionChangedSignal>()
                .ToMethod<TestSettings>(x => x.LookDirectionChanged)
                .FromResolve();

            Container.DeclareSignal<TestLevelChangedSignal>();
            Container.BindSignal<TestLevelChangedSignal>()
                .ToMethod<TestView>(x => x.TestLevelChanged)
                .FromResolve();

            Container.DeclareSignal<TestSettingsChangedSignal>();
            Container.BindSignal<TestSettingsChangedSignal>()
                .ToMethod<TestManager>(x => x.SettingsChanged)
                .FromResolve();
        }
    }
}