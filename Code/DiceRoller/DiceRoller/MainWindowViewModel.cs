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
        #endregion

        #region Public Members
        public int D4Number { get => _d4Number; set { if (value == _d4Number) return; _d4Number = value;  OnPropertyChanged(); } }
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
            switch (selectedDice.ToLower())
            {
                case "d4":
                    D4Number--;
                    break;
                default:
                    throw new Exception($"Invalid dice: {selectedDice}.");
            }
        }

        private void IncreaseDiceNumber(object parameters)
        {
            string selectedDice = (string)parameters;
            switch (selectedDice.ToLower())
            {
                case "d4":
                    D4Number++;
                    break;
                default:
                    throw new Exception($"Invalid dice: {selectedDice}.");
            }
        }
        #endregion

        #region Private Methods

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
