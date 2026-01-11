using System;

public class PomodoroSession
{
    private TimerState _state;
    public PomodoroSession(TimerState state)
    {
        _state = state;
    }

    // Take one second off timer
    public bool Tick()
    {
        _state.TimeRemaining -= 1;
        // See if time ran out
        return _state.TimeRemaining <= 0;
    }

    // Get ready for next session
    public SessionType CompleteSession()
    {
        _state.ToggleSessionType();

        // Work out how long the next session is
        if (_state.SessionType == SessionType.Work)
        {
            _state.TimeRemaining = Constants.WorkDuration;
        }
        else
        {
            _state.TimeRemaining = Constants.BreakDuration;
        }

        return _state.SessionType;
    }
}
