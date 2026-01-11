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

    // Get how much time is left
    public int GetTimeRemaining()
    {
        return _timeRemaining;
    }

    // Set how much time is left
    public void SetTimeRemaining(int time)
    {
        _timeRemaining = time;
    }

    // Get which type of session we're on
    public SessionType GetSessionType()
    {
        return _sessionType;
    }

    // Set which type of session we're on
    public void SetSessionType(SessionType type)
    {
        _sessionType = type;
    }

    // See if timer is going
    public bool GetIsRunning()
    {
        return _isRunning;
    }

    // Set if timer is going
    public void SetIsRunning(bool running)
    {
        _isRunning = running;
    }

    // Get how many sessions we've done
    public int GetSessionsCompleted()
    {
        return _sessionsCompleted;
    }

    // Set how many sessions we've done
    public void SetSessionsCompleted(int sessions)
    {
        _sessionsCompleted = sessions;
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
