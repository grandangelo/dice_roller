﻿using DiceRoller.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace DiceRoller
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Private Members
        private int _d4Number;
        private int _d6Number;
        private int _d8Number;
        private int _d10Number;
        private int _d12Number;
        private int _d20Number;
        private int _d100Number;
        private int _totalSum;
        private string _result;
        private List<string> _extractions;
        private Random _r;
        #endregion

        #region Public Members
        public int D4Number { get => _d4Number; set { if (value == _d4Number) return; _d4Number = value;  OnPropertyChanged(); } }
        public int D6Number { get => _d6Number; set { if (value == _d6Number) return; _d6Number = value;  OnPropertyChanged(); } }
        public int D8Number { get => _d8Number; set { if (value == _d8Number) return; _d8Number = value;  OnPropertyChanged(); } }
        public int D10Number { get => _d10Number; set { if (value == _d10Number) return; _d10Number = value;  OnPropertyChanged(); } }
        public int D12Number { get => _d12Number; set { if (value == _d12Number) return; _d12Number = value;  OnPropertyChanged(); } }
        public int D20Number { get => _d20Number; set { if (value == _d20Number) return; _d20Number = value;  OnPropertyChanged(); } }
        public int D100Number { get => _d100Number; set { if (value == _d100Number) return; _d100Number = value;  OnPropertyChanged(); } }
        public string Result { get => _result; set { if (value == _result) return; _result = value;  OnPropertyChanged(); } }
        public ICommand DecreaseDiceCommand { get; set; }
        public ICommand IncreaseDiceCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand RollCommand { get; set; }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            _r = new Random();
            _extractions = new List<string>();
            DecreaseDiceCommand = new RelayCommand(DecreaseDiceNumber);
            IncreaseDiceCommand = new RelayCommand(IncreaseDiceNumber);
            ResetCommand = new RelayCommand(Reset);
            RollCommand = new RelayCommand(Roll);
            Result = "No result.";
        }
        #endregion

        #region Public Methods
        #endregion

        #region Command Support
        private void DecreaseDiceNumber(object parameters)
        {
            string selectedDice = (string)parameters;

            static int decreaseDice(int currentValue)
            {
                if (currentValue > 0)
                {
                    return currentValue - 1;
                }
                else
                {
                    return 0;
                }
            }
            ModifyDiceNumber(selectedDice, decreaseDice);
        }

        private void IncreaseDiceNumber(object parameters)
        {
            string selectedDice = (string)parameters;
            static int increaseDice(int currentValue) => currentValue + 1;
            ModifyDiceNumber(selectedDice, increaseDice);
        }

        private void Reset(object parameters)
        {
            D4Number = 0;
            D6Number = 0;
            D8Number = 0;
            D10Number = 0;
            D12Number = 0;
            D20Number = 0;
            D100Number = 0;
        }

        private void Roll(object parameters)
        {
            _totalSum = 0;
            RollDice();
            if (_extractions.Count != 0)
            {
                Result = string.Join(Environment.NewLine, _extractions);
                Result += $"{Environment.NewLine}Total sum: {_totalSum}.";
            }
            else
            {
                Result = "No result.";
            }
        }
        #endregion

        #region Private Methods
        private void ModifyDiceNumber(string die, Func<int, int> function)
        {
            switch (die.ToLower())
            {
                case "d4":
                    D4Number = function(D4Number);
                    break;
                case "d6":
                    D6Number = function(D6Number);
                    break;
                case "d8":
                    D8Number = function(D8Number);
                    break;
                case "d10":
                    D10Number = function(D10Number);
                    break;
                case "d12":
                    D12Number = function(D12Number);
                    break;
                case "d20":
                    D20Number = function(D20Number);
                    break;
                case "d100":
                    D100Number = function(D100Number);
                    break;
                default:
                    throw new Exception($"Invalid dice: {die}.");
            }
        }

        private void RollDice()
        {
            _extractions.Clear();
            var items = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(D4Number, 4),
                new Tuple<int, int>(D6Number, 6),
                new Tuple<int, int>(D8Number, 8),
                new Tuple<int, int>(D10Number, 10),
                new Tuple<int, int>(D12Number, 12),
                new Tuple<int, int>(D20Number, 20),
                new Tuple<int, int>(D100Number, 100),
            };
            foreach (var item in items)
            {
                RollDie(item.Item1, item.Item2, ref _extractions);
            }
        }

        private void RollDie(int numberOfRolls, int maxValue, ref List<string> extractions)
        {
            List<int> dieRoll = new List<int>();
            for (int i = 0; i < numberOfRolls; i++)
            {
                dieRoll.Add(_r.Next(1, maxValue + 1));
            }
            if (dieRoll.Count != 0)
            {
                extractions.Add($"D{maxValue}: {string.Join(", ", dieRoll)}. Sum {dieRoll.Sum()}");
                _totalSum += dieRoll.Sum();
            }
        }
        #endregion

        #region INotifyPropertyChanged Support
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
