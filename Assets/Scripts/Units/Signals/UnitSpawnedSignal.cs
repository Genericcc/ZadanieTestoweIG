namespace Units.Signals
{
    public class UnitSpawnedSignal
    {
        private Unit _unit;
        public Unit Unit => _unit;

        public UnitSpawnedSignal(Unit setUnit)
        {
            _unit = setUnit;
        }
    }
}