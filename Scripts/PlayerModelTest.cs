using System;
using System.Collections.Generic;
using UnityEngine;

namespace SubjectObserver
{
    public class PlayerModelTest : ICurrencyChangedObserver
    {
        public interface IUICallback
        {
            void UpdateCurrencyValue(CurrencyCategory currency, decimal newValue);
        }

        private IUICallback _uiCallback;
        private Dictionary<CurrencyCategory, decimal> _dictCurrencyValue;

        public void Init(IUICallback ui, bool uiRefresh)
        {
            _uiCallback = ui;

            _dictCurrencyValue = new Dictionary<CurrencyCategory, decimal>();
            foreach (CurrencyCategory currency in Enum.GetValues(typeof(CurrencyCategory)))
            {
                if (currency == CurrencyCategory.None) continue;
                _dictCurrencyValue[currency] = default;

                if (uiRefresh)
                    _uiCallback?.UpdateCurrencyValue(currency, _dictCurrencyValue[currency]);
            }

        }

        private bool TryChangedCurrency(CurrencyCategory currency, decimal changed)
        {
            decimal newValue = _dictCurrencyValue[currency] + changed;
            if (newValue < 0)
            {
                Debug.LogError($"Player changed {currency} failed. {_dictCurrencyValue[currency]} + {changed} = {newValue}");
                return false;
            }
            _dictCurrencyValue[currency] = newValue;
            return true;
        }

        #region implement ICurrencyChangedObserver
        void ICurrencyChangedObserver.Update(CurrencyCategory currency, decimal changed)
        {
            bool hasChanged = TryChangedCurrency(currency, changed);
            if (hasChanged)
            {
                _uiCallback?.UpdateCurrencyValue(currency, _dictCurrencyValue[currency]);
            }
        }
        #endregion
    }
}
