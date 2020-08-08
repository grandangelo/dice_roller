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
        #endregion

        #region Public Members
        public int D4Number { get => _d4Number; set { if (value == _d4Number) return; _d4Number = value;  OnPropertyChanged(); } }
        public int D6Number { get => _d6Number; set { if (value == _d6Number) return; _d6Number = value;  OnPropertyChanged(); } }
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
            static int decreaseDice(int currentValue) => currentValue - 1;
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
