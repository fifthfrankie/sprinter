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
        _state.SetTimeRemaining(_state.GetTimeRemaining() - 1);
        // See if time ran out
        return _state.GetTimeRemaining() <= 0;
    }

    // Get ready for next session
    public SessionType CompleteSession()
    {
        _state.ToggleSessionType();

        // Work out how long the next session is
        if (_state.GetSessionType() == SessionType.Work)
        {
            _state.SetTimeRemaining(Constants.WorkDuration);
        }
        else
        {
            _state.SetTimeRemaining(Constants.BreakDuration);
        }

        return _state.GetSessionType();
    }
}
