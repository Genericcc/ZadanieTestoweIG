namespace Tests.Signals
{
    public class TestLevelChangeSignal
    {
        private TestLevelType _type;
        public TestLevelType TestLevelType => _type;
        
        public TestLevelChangeSignal(TestLevelType testLevelType)
        {
            _type = testLevelType;
        }
    }
    
    public enum TestLevelType
    {
        Previous,
        Next
    }
}