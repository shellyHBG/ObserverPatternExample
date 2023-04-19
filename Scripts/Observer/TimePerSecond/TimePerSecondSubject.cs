using System;
using System.Collections.Generic;
using UnityEngine;

public class TimePerSecondSubject : ITimePerSecondSubject, IDisposable
{
	private HashSet<ITimePerSecondObserver> _setObservers = null;

	public ITimePerSecondSubject Init()
	{
		if (_setObservers == null)
			_setObservers = new HashSet<ITimePerSecondObserver>();
		_setObservers.Clear();

		return this;
	}

	#region implement ITimePerSecondSubject
	ITimePerSecondSubject ITimePerSecondSubject.Attach(ITimePerSecondObserver observer)
	{
		bool attach = _setObservers.Add(observer);
		if (!attach)
			Debug.Log($"{nameof(observer)} has been attached.");
		return this;
	}

	ITimePerSecondSubject ITimePerSecondSubject.Detach(ITimePerSecondObserver observer)
	{
		_setObservers.Remove(observer);
		return this;
	}

	void ITimePerSecondSubject.Notify(long nowTicks)
	{
		DateTime now = new DateTime(nowTicks);
		Debug.Log($"now: {now.ToString("yyyy/MM/dd HH:mm:ss")}");
		foreach (var observer in _setObservers)
		{
			observer.Update(nowTicks);
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
