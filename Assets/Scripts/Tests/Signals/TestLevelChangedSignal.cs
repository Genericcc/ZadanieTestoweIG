namespace Tests.Signals
{
    public class TestLevelChangedSignal
    {
        private int _level;
        public int Level => _level;

        private int _levelsCount;
        public int LevelsCount => _levelsCount;
        
        public TestLevelChangedSignal(int level, int levelsCount)
        {
            _level = level;
            _levelsCount = levelsCount;
        }
    }
}