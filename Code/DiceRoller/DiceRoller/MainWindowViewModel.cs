using DiceRoller.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        #endregion

        #region Public Members
        public int D4Number { get => _d4Number; set { if (value == _d4Number) return; _d4Number = value;  OnPropertyChanged(); } }
        public int D6Number { get => _d6Number; set { if (value == _d6Number) return; _d6Number = value;  OnPropertyChanged(); } }
        public int D8Number { get => _d8Number; set { if (value == _d8Number) return; _d8Number = value;  OnPropertyChanged(); } }
        public int D10Number { get => _d10Number; set { if (value == _d10Number) return; _d10Number = value;  OnPropertyChanged(); } }
        public int D12Number { get => _d12Number; set { if (value == _d12Number) return; _d12Number = value;  OnPropertyChanged(); } }
        public int D20Number { get => _d20Number; set { if (value == _d20Number) return; _d20Number = value;  OnPropertyChanged(); } }
        public int D100Number { get => _d100Number; set { if (value == _d100Number) return; _d100Number = value;  OnPropertyChanged(); } }
        public ICommand DecreaseDiceCommand { get; set; }
        public ICommand IncreaseDiceCommand { get; set; }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            DecreaseDiceCommand = new RelayCommand(DecreaseDiceNumber);
            IncreaseDiceCommand = new RelayCommand(IncreaseDiceNumber);
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
