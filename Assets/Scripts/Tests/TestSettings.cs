using Tests.Signals;
using Units;
using Utils;
using Zenject;

namespace Tests
{
    public class TestSettings
    {
        private UnitSizeType _unitSizeType;
        private LookDirection _lookDirection;
        
        public UnitSizeType UnitSizeType => _unitSizeType;
        public LookDirection LookDirection => _lookDirection;
        
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus setSignalBus)
        {
            _signalBus = setSignalBus;
        }
        
        public void UnitSizeChanged(TestUnitSizeChangedSignal testUnitSizeChangedSignal)
        {
            _unitSizeType = testUnitSizeChangedSignal.UnitSizeType;
            _signalBus.Fire<TestSettingsChangedSignal>();
        }

        public void LookDirectionChanged(TestLookDirectionChangedSignal testLookDirectionChangedSignal)
        {
            _lookDirection = testLookDirectionChangedSignal.LookDirection;
            _signalBus.Fire<TestSettingsChangedSignal>();
        }
    }
}