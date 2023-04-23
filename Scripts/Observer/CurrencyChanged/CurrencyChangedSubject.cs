using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyChangedSubject : ICurrencyChangedSubject, IDisposable
{
	private HashSet<ICurrencyChangedObserver> _setObservers = null;

	public ICurrencyChangedSubject Init()
	{
		if (_setObservers == null)
			_setObservers = new HashSet<ICurrencyChangedObserver>();
		_setObservers.Clear();

		return this;
	}

	#region implement ICurrencyChangedSubject
	ICurrencyChangedSubject ICurrencyChangedSubject.Attach(ICurrencyChangedObserver observer)
	{
		bool attach = _setObservers.Add(observer);
		if (!attach)
			Debug.Log($"{nameof(observer)} has been attached.");
		return this;
	}

	ICurrencyChangedSubject ICurrencyChangedSubject.Detach(ICurrencyChangedObserver observer)
	{
		_setObservers.Remove(observer);
		return this;
	}

	void ICurrencyChangedSubject.Notify(CurrencyCategory currency, decimal changed)
	{
		foreach (var observer in _setObservers)
		{
			observer.Update(currency, changed);
		}
	}
	#endregion

	#region implement IDisposable
	void IDisposable.Dispose()
	{
		_setObservers?.Clear();
	}
    #endregion
}
