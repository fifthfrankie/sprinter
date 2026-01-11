using System;

// Work or break session
public enum SessionType
{
    Work,
    Break
}

public class TimerState
{
    // How much time is left
    private volatile int _timeRemaining;
    // Which type of session we're on
    private SessionType _sessionType;
    // See if timer is going
    private bool _isRunning;
    // How many sessions we've done
    private int _sessionsCompleted;

    public TimerState()
    {
        // Set up starting values
        Reset();
    }

    // How much time is left
    public int TimeRemaining
    {
        get => _timeRemaining;
        set => _timeRemaining = value;
    }

    // Which type of session we're on
    public SessionType SessionType
    {
        get => _sessionType;
        set => _sessionType = value;
    }

    // See if timer is going
    public bool IsRunning
    {
        get => _isRunning;
        set => _isRunning = value;
    }

    // How many sessions we've done
    public int SessionsCompleted
    {
        get => _sessionsCompleted;
        set => _sessionsCompleted = value;
    }

    // Put everything back to start
    public void Reset()
    {
        _timeRemaining = Constants.WorkDuration;
        _sessionType = SessionType.Work;
        _isRunning = false;
        _sessionsCompleted = 0;
    }

    // Swap between work and break
    public void ToggleSessionType()
    {
        _sessionType = _sessionType == SessionType.Work ? SessionType.Break : SessionType.Work;
    }

    // Add one to the sessions we've done
    public void IncrementSessionsCompleted()
    {
        _sessionsCompleted++;
    }
}
