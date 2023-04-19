using UnityEngine;

namespace SubjectObserver
{
    public class ModelTest : ITimePerSecondObserver
    {
        #region implement ITimePerSecondObserver
        void ITimePerSecondObserver.Update(long nowTicks)
        {
            Debug.Log($"[{GetType().Name}]Update now: {nowTicks}");
        }
        #endregion
    }
}
