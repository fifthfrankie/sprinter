using System;
using System.Timers;

public class TimerEngine : IDisposable
{
    // The timer that actually counts time
    private System.Timers.Timer _timer;
    // What to do when timer ticks
    private Action _onTick;

    public TimerEngine(int interval, Action onTick)
    {
        _onTick = onTick;
        _timer = new System.Timers.Timer(interval);
        _timer.Elapsed += OnElapsed;
        _timer.AutoReset = true;
    }

    // See if timer is going
    public bool IsRunning
    {
        get { return _timer.Enabled; }
    }

    // Make timer go
    public void Start()
    {
        _timer.Start();
    }

    // Stop timer
    public void Stop()
    {
        _timer.Stop();
    }

    // Run the tick action
    private void OnElapsed(object? sender, ElapsedEventArgs e)
    {
        _onTick?.Invoke();
    }

    // Clear up timer
    public void Dispose()
    {
        _timer?.Dispose();
    }
}
