using System;
using System.Collections;
using System.Collections.Generic;
using HexGrids;
using Tests.Signals;
using UnityEngine;
using Zenject;

namespace Tests
{
    public class TestManager : MonoBehaviour
    {
        [SerializeField]
        private List<TestSet> testSets;

        private TestSet _currentTestSet;
        private int _currentTestIndex = 0;

        private HexGrid _hexGrid;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(HexGrid setHexGrid, SignalBus setSignalBus)
        {
            _hexGrid = setHexGrid;
            _signalBus = setSignalBus;
        }
        
        private void Start()
        {
            StartCoroutine(_StartTest());
        }

        private IEnumerator _StartTest()
        {
            while (!_hexGrid.IsReady)
            {
                yield return new WaitForFixedUpdate();
            }
            
            RunTest();
        }

        public void ChangeTest(TestLevelChangeSignal testLevelChangeSignal)
        {
            switch (testLevelChangeSignal.TestLevelType)
            {
                case TestLevelType.Previous:
                    Previous();
                    break;
                case TestLevelType.Next:
                    Next();
                    break;
                default:
                    Debug.LogError($"{nameof(TestManager)} -> {nameof(ChangeTest)} {testLevelChangeSignal} not implemented.");
                    throw new ArgumentOutOfRangeException(nameof(testLevelChangeSignal), testLevelChangeSignal, null);
            }
        }

        public void Next()
        {
            _currentTestIndex++;
            if (_currentTestIndex >= testSets.Count)
            {
                _currentTestIndex = 0;
            }
            
            RunTest();
        }

        public void Previous()
        {
            _currentTestIndex--;
            if (_currentTestIndex < 0)
            {
                _currentTestIndex = testSets.Count-1;
            }
            
            RunTest();
        }

        private void RunTest()
        {
            if (_currentTestSet != null)
            {
                _currentTestSet.EndTest();
            }

            _currentTestSet = testSets[_currentTestIndex];
            _currentTestSet.StartTest();
            
            _signalBus.Fire(new TestLevelChangedSignal(_currentTestIndex + 1, testSets.Count));
        }

        public void SettingsChanged(TestSettingsChangedSignal testSettingsChangedSignal)
        {
            if (_currentTestSet == null)
            {
                return;
            }
            
            _currentTestSet.Reset();
        }
    }
}