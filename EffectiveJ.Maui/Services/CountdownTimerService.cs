
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EffectiveJ.Maui.Services
{

    public class CountdownTimerService
    {
        private Timer timer;
        private bool isRunning = false;
        private TimeSpan remainingTime;

        public event Action<TimeSpan> TimeChanged;
        public event Action CountdownFinished;

        public bool IsRunning => isRunning;

        public void Start(TimeSpan duration)
        {   
            if (!isRunning)
            {
                remainingTime = duration;
                isRunning = true;
                timer = new Timer(TimerCallback, null, 0, 1000);
            }
        }

        public void Stop()
        {
            if (isRunning)
            {
                timer.Dispose();
                isRunning = false;
                CountdownFinished?.Invoke();
            }
        }

        private void TimerCallback(object state)
        {
            remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
            TimeChanged?.Invoke(remainingTime);

            if (remainingTime.TotalSeconds <= 0)
            {
                Stop();
            }
        }
    }

}
