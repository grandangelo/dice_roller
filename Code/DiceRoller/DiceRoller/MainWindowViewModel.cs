using DiceRoller.Support;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private int _totalSum;
        private string _result;
        private List<string> _extractions;
        private ObservableCollection<Die> _dice;
        private Random _r;
        #endregion

        #region Public Members
        public string Result { get => _result; set { if (value == _result) return; _result = value;  OnPropertyChanged(); } }
        public ObservableCollection<Die> Dice { get => _dice; set { if (value == _dice) return; _dice = value; OnPropertyChanged(); } }
        public ICommand DecreaseDiceCommand { get; set; }
        public ICommand DecreaseModifierCommand { get; set; }
        public ICommand IncreaseDiceCommand { get; set; }
        public ICommand IncreaseModifierCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand RollCommand { get; set; }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            _r = new Random();
            _extractions = new List<string>();
            Dice = new ObservableCollection<Die>()
            {
                new Die(4, "D4"),
                new Die(6, "D6"),
                new Die(8, "D8"),
                new Die(10, "D10"),
                new Die(12, "D12"),
                new Die(20, "D20"),
                new Die(100, "D100"),
            };
            DecreaseDiceCommand = new RelayCommand(DecreaseDiceNumber);
            DecreaseModifierCommand = new RelayCommand(DecreaseModifier);
            IncreaseDiceCommand = new RelayCommand(IncreaseDiceNumber);
            IncreaseModifierCommand = new RelayCommand(IncreaseModifier);
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

        private void DecreaseModifier(object parameters)
        {
            string selectedDice = (string)parameters;
            static int decreaseModifier(int currentValue) => currentValue - 1;
            ModifyModifier(selectedDice, decreaseModifier);
        }

        private void IncreaseDiceNumber(object parameters)
        {
            string selectedDice = (string)parameters;
            static int increaseDice(int currentValue) => currentValue + 1;
            ModifyDiceNumber(selectedDice, increaseDice);
        }

        private void IncreaseModifier(object parameters)
        {
            string selectedDice = (string)parameters;
            static int increaseModifier(int currentValue) => currentValue + 1;
            ModifyModifier(selectedDice, increaseModifier);
        }

        private void Reset(object parameters)
        {
            foreach (var die in Dice)
            {
                die.NumberOfRolls = 0;
                die.Modifier = 0;
            }
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
                case "4":
                    Dice.GetDie(4).NumberOfRolls = function(Dice.GetDie(4).NumberOfRolls);
                    break;
                case "6":
                    Dice.GetDie(6).NumberOfRolls = function(Dice.GetDie(6).NumberOfRolls);
                    break;
                case "8":
                    Dice.GetDie(8).NumberOfRolls = function(Dice.GetDie(8).NumberOfRolls);
                    break;
                case "10":
                    Dice.GetDie(10).NumberOfRolls = function(Dice.GetDie(10).NumberOfRolls);
                    break;
                case "12":
                    Dice.GetDie(12).NumberOfRolls = function(Dice.GetDie(12).NumberOfRolls);
                    break;
                case "20":
                    Dice.GetDie(20).NumberOfRolls = function(Dice.GetDie(20).NumberOfRolls);
                    break;
                case "100":
                    Dice.GetDie(100).NumberOfRolls = function(Dice.GetDie(100).NumberOfRolls);
                    break;
                default:
                    throw new Exception($"Invalid dice: {die}.");
            }
        }

        private void ModifyModifier(string die, Func<int, int> function)
        {
            switch (die.ToLower())
            {
                case "4":
                    Dice.GetDie(4).Modifier = function(Dice.GetDie(4).Modifier);
                    break;
                case "6":
                    Dice.GetDie(6).Modifier = function(Dice.GetDie(6).Modifier);
                    break;
                case "8":
                    Dice.GetDie(8).Modifier = function(Dice.GetDie(8).Modifier);
                    break;
                case "10":
                    Dice.GetDie(10).Modifier = function(Dice.GetDie(10).Modifier);
                    break;
                case "12":
                    Dice.GetDie(12).Modifier = function(Dice.GetDie(12).Modifier);
                    break;
                case "20":
                    Dice.GetDie(20).Modifier = function(Dice.GetDie(20).Modifier);
                    break;
                case "100":
                    Dice.GetDie(100).Modifier = function(Dice.GetDie(100).Modifier);
                    break;
                default:
                    throw new Exception($"Invalid dice: {die}.");
            }
        }

        private void RollDice()
        {
            _extractions.Clear();
            var items = new List<Tuple<int, int, bool, int>>()
            {
                new Tuple<int, int, bool, int>(Dice.GetDie(4).NumberOfRolls, 4, Dice.GetDie(4).AppyModifierAtEachRoll, Dice.GetDie(4).Modifier),
                new Tuple<int, int, bool, int>(Dice.GetDie(6).NumberOfRolls, 6, Dice.GetDie(6).AppyModifierAtEachRoll, Dice.GetDie(6).Modifier),
                new Tuple<int, int, bool, int>(Dice.GetDie(8).NumberOfRolls, 8, Dice.GetDie(8).AppyModifierAtEachRoll, Dice.GetDie(8).Modifier),
                new Tuple<int, int, bool, int>(Dice.GetDie(10).NumberOfRolls, 10, Dice.GetDie(10).AppyModifierAtEachRoll, Dice.GetDie(10).Modifier),
                new Tuple<int, int, bool, int>(Dice.GetDie(12).NumberOfRolls, 12, Dice.GetDie(12).AppyModifierAtEachRoll, Dice.GetDie(12).Modifier),
                new Tuple<int, int, bool, int>(Dice.GetDie(20).NumberOfRolls, 20, Dice.GetDie(20).AppyModifierAtEachRoll, Dice.GetDie(20).Modifier),
                new Tuple<int, int, bool, int>(Dice.GetDie(100).NumberOfRolls, 100, Dice.GetDie(100).AppyModifierAtEachRoll, Dice.GetDie(100).Modifier),
            };
            foreach (var item in items)
            {
                RollDie(item.Item1, item.Item2, item.Item3, item.Item4, ref _extractions);
            }
        }

        private void RollDie(int numberOfRolls, int maxValue, bool applyAll, int modifier, ref List<string> extractions)
        {
            List<int> dieRoll = new List<int>();
            for (int i = 0; i < numberOfRolls; i++)
            {
                var extraction = _r.Next(1, maxValue + 1);
                dieRoll.Add(extraction);
            }
            if (dieRoll.Count != 0)
            {
                int totalModifier = applyAll ? modifier * numberOfRolls : modifier;
                string entry = numberOfRolls == 1 ?
                    $"D{maxValue}: {string.Join(", ", dieRoll)}{(totalModifier == 0 ? string.Empty : $" + {totalModifier}.")}" :
                    $"D{maxValue}: {string.Join(", ", dieRoll)} + {totalModifier}. Sum {dieRoll.Sum() + totalModifier}";
                extractions.Add(entry);
                _totalSum += dieRoll.Sum() + totalModifier;
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

    public static class Extensions
    {
        public static Die GetDie(this ObservableCollection<Die> dice, int maxValue)
        {
            return dice.Single(dice => dice.MaxDieValue == maxValue);
        }
    }
}
