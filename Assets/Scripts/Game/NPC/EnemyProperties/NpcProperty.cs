namespace NPC.EnemyProperties
{
    public class NpcProperty
    {
        private readonly float _maxValue;
        private float _value;

        public float MaxValue => _maxValue;

        public NpcProperty(float maxValue)
        {
            _maxValue = maxValue;
            _value = maxValue;
        }

        public NpcProperty()
        {
            _maxValue = float.MaxValue;
        }
        
        
        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                if (_value > _maxValue) _value = _maxValue;
                
                OnValueChange?.Invoke();
                if (_value <= 0) OnZeroValue?.Invoke();
            }
        }
        
        public delegate void ChangeValue();
        public ChangeValue OnValueChange;
        
        public delegate void ZeroValue();

        public ZeroValue OnZeroValue;
    }
}