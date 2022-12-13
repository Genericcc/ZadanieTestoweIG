using Utils;

namespace Tests.Signals
{
    public class TestLookDirectionChangedSignal
    {
        private LookDirection _lookDirection;
        public LookDirection LookDirection => _lookDirection;

        public TestLookDirectionChangedSignal(LookDirection lookDirection)
        {
            _lookDirection = lookDirection;
        }
    }
}