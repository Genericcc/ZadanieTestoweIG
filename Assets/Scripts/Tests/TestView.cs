using System;
using Tests.Signals;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Utils.Extensions;
using Zenject;

namespace Tests
{
    public class TestView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI currentTestText;

        [SerializeField]
        private TMP_Dropdown unitSizeDropdown;

        [SerializeField] 
        private TMP_Dropdown lookDirectionDropdown;
        
        private SignalBus _signalBus;

        private void Awake()
        {
            unitSizeDropdown.PopulateDropDownWithEnum<UnitSizeType>();
            lookDirectionDropdown.PopulateDropDownWithEnum<LookDirection>();
        }

        [Inject]
        public void Construct(SignalBus setSignalBus)
        {
            _signalBus = setSignalBus;
        }
        
        public void Next()
        {
            _signalBus.Fire(new TestLevelChangeSignal(TestLevelType.Next));
        }

        public void Previous()
        {
            _signalBus.Fire(new TestLevelChangeSignal(TestLevelType.Previous));
        }

        public void UnitSizeChanged(int size)
        {
            _signalBus.Fire(new TestUnitSizeChangedSignal((UnitSizeType)size));
        }
        
        public void UnitLookDirectionChanged(int direction)
        {
            _signalBus.Fire(new TestLookDirectionChangedSignal((LookDirection)direction));
        }
        
        public void TestLevelChanged(TestLevelChangedSignal testLevelChangedSignal)
        {
            currentTestText.text = $"{testLevelChangedSignal.Level}/{testLevelChangedSignal.LevelsCount}";
        }
    }
}
