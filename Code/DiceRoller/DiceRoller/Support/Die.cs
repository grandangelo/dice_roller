using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DiceRoller.Support
{
    public class Die : INotifyPropertyChanged
    {
        #region Private Members
        private bool _applyModifierAtEachRoll;
        private int _modifier;
        private int _numberOfRolls;
        #endregion

        #region Public Members
        public bool AppyModifierAtEachRoll { get => _applyModifierAtEachRoll; set { if (value == _applyModifierAtEachRoll) return; _applyModifierAtEachRoll = value; OnPropertyChanged(); } }
        public int MaxDieValue { get; private set; }
        public int Modifier { get => _modifier; set { if (value == _modifier) return; _modifier = value; OnPropertyChanged(); } }
        public int NumberOfRolls { get => _numberOfRolls; set { if (value == _numberOfRolls) return; _numberOfRolls = value; OnPropertyChanged(); } }
        public string MaxDieValueString { get => MaxDieValue.ToString(); }
        public string Name { get; private set; }
        #endregion

        #region Constructor
        public Die(int maxDieValue, string name)
        {
            MaxDieValue = maxDieValue;
            Name = name;
        }
        #endregion

        #region Public Methods
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
