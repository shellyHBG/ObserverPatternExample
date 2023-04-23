using System;
using UnityEngine;
using UnityEngine.UI;

namespace SubjectObserver
{
    public class UnityTimeProvider : MonoBehaviour
    {
        private ModelTest m1, m2;

        private ITimePerSecondSubject _timeSubject = null;
        private long _lLastTicks;

        void Awake()
        {
            _timeSubject = new TimePerSecondSubject().Init();
            m1 = new ModelTest();
            m2 = new ModelTest();
        }

        // Update is called once per frame
        void Update()
        {
            long nowTicks = DateTime.Now.Ticks;
            if (nowTicks - _lLastTicks > TimeSpan.TicksPerSecond)
            {
                _lLastTicks = nowTicks;
                _timeSubject.Notify(nowTicks);
            }
        }

        void OnDestroy()
        {
            var disposableObj = _timeSubject as IDisposable;
            if (disposableObj != null)
                disposableObj.Dispose();
        }

        public void AttachModel1()
        {
            _timeSubject.Attach(m1);
        }

        public void DettachModel1()
        {
            _timeSubject.Detach(m1);
        }

        public void AttachModel2()
        {
            _timeSubject.Attach(m2);
        }

        public void DettachModel2()
        {
            _timeSubject.Detach(m2);
        }
    }
}
