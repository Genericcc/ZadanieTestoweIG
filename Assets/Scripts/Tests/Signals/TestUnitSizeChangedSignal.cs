using Units;

namespace Tests.Signals
{
    public class TestUnitSizeChangedSignal
    {
        private UnitSizeType _unitSizeType;
        public UnitSizeType UnitSizeType => _unitSizeType;

        public TestUnitSizeChangedSignal(UnitSizeType unitSizeType)
        {
            _unitSizeType = unitSizeType;
        }
    }
}