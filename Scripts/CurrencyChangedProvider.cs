using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SubjectObserver
{
    public class CurrencyChangedProvider : MonoBehaviour, PlayerModelTest.IUICallback
    {
        // ui sender
        public Button BtnCoinChanged;
        public Button BtnDiamondChanged;
        public Button BtnDiamondFreeChanged;
        public Button BtnArenaChanged;
        public InputField InputCoinChanged;
        public InputField InputDiamondChanged;
        public InputField InputDiamondFreeChanged;
        public InputField InputArenaChanged;
        private Dictionary<CurrencyCategory, InputField> _dictInputCurrency;
        // ui for reciever (player model)
        public Text TxtCoin;
        public Text TxtDiamond;
        public Text TxtDiamondFree;
        public Text TxtArena;
        private Dictionary<CurrencyCategory, Text> _dictTxtCurrency;

        private ICurrencyChangedSubject _currencyChangedSubject = null;
        private PlayerModelTest _playerModel = null;

        void Awake()
        {
            InitUI();
            _currencyChangedSubject = new CurrencyChangedSubject().Init();
            _playerModel = new PlayerModelTest();
            _playerModel.Init(this, true);
            _currencyChangedSubject.Attach(_playerModel);
        }

        void OnDestroy()
        {
            var disposableObj = _currencyChangedSubject as IDisposable;
            if (disposableObj != null)
                disposableObj.Dispose();
        }

        public void ChangeSubmit(CurrencyCategory currency)
        {
            if (_dictInputCurrency.TryGetValue(currency, out InputField inputChanged))
            {
                bool hasValue = Decimal.TryParse(inputChanged.text, out Decimal changedValue);
                if (!hasValue)
                {
                    Debug.LogError($"Decimal parses {inputChanged.text} failed.");
                    return;
                }
                _currencyChangedSubject.Notify(currency, changedValue);
            }
        }

        private void InitUI()
        {
            BtnCoinChanged?.onClick.RemoveAllListeners();
            BtnCoinChanged?.onClick.AddListener(() => ChangeSubmit(CurrencyCategory.CoinPoint));
            BtnDiamondChanged?.onClick.RemoveAllListeners();
            BtnDiamondChanged?.onClick.AddListener(() => ChangeSubmit(CurrencyCategory.DiamondPoint));
            BtnDiamondFreeChanged?.onClick.RemoveAllListeners();
            BtnDiamondFreeChanged?.onClick.AddListener(() => ChangeSubmit(CurrencyCategory.DiamondPoint_Free));
            BtnArenaChanged?.onClick.RemoveAllListeners();
            BtnArenaChanged?.onClick.AddListener(() => ChangeSubmit(CurrencyCategory.ArenaPoint));

            _dictInputCurrency = new Dictionary<CurrencyCategory, InputField>();
            if (InputCoinChanged != null) _dictInputCurrency[CurrencyCategory.CoinPoint] = InputCoinChanged;
            if (InputDiamondChanged != null) _dictInputCurrency[CurrencyCategory.DiamondPoint] = InputDiamondChanged;
            if (InputDiamondFreeChanged != null) _dictInputCurrency[CurrencyCategory.DiamondPoint_Free] = InputDiamondFreeChanged;
            if (InputArenaChanged != null) _dictInputCurrency[CurrencyCategory.ArenaPoint] = InputArenaChanged;

            _dictTxtCurrency = new Dictionary<CurrencyCategory, Text>();
            if (TxtCoin != null) _dictTxtCurrency[CurrencyCategory.CoinPoint] = TxtCoin;
            if (TxtDiamond != null) _dictTxtCurrency[CurrencyCategory.DiamondPoint] = TxtDiamond;
            if (TxtDiamondFree != null) _dictTxtCurrency[CurrencyCategory.DiamondPoint_Free] = TxtDiamondFree;
            if (TxtArena != null) _dictTxtCurrency[CurrencyCategory.ArenaPoint] = TxtArena;
        }

        #region implement PlayerModelTest.IUICallback
        void PlayerModelTest.IUICallback.UpdateCurrencyValue(CurrencyCategory currency, decimal newValue)
        {
            if (_dictTxtCurrency.TryGetValue(currency, out Text uiText))
            {
                uiText.text = newValue.ToString();
            }
        }
        #endregion
    }
}
