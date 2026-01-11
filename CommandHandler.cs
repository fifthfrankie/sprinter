using System;

public class CommandHandler
{
    private TimerEngine _timer;
    private PomodoroSession _session;
    // Shows timer on screen
    private TimerDisplay _display;
    // Holds timer information
    private TimerState _state;

    public CommandHandler(TimerEngine timer, PomodoroSession session, TimerDisplay display, TimerState state)
    {
        _timer = timer;
        _session = session;
        _display = display;
        _state = state;
    }

    // Work out which key was pressed
    public void Handle(ConsoleKeyInfo key)
    {
        switch (key.Key)
        {
            case ConsoleKey.S:
                HandleStart();
                break;

            case ConsoleKey.P:
                HandlePause();
                break;

            case ConsoleKey.R:
                HandleReset();
                break;

            case ConsoleKey.Q:
                HandleQuit();
                break;
        }
    }

    // Start the timer
    private void HandleStart()
    {
        // Only start if timer is stopped
        if (!_state.IsRunning)
        {
            // Make timer go
            _timer.Start();
            // Remember timer is going now
            _state.IsRunning = true;
            // Show the timer on screen
            _display.DisplayTimer(_state);
            // Show the commands
            _display.DisplayInstructions();
        }
    }

    // Stop the timer
    private void HandlePause()
    {
        // Only stop if timer is going
        if (_state.IsRunning)
        {
            // Stop timer
            _timer.Stop();
            // Remember timer is stopped now
            _state.IsRunning = false;
            // Show the timer on screen
            _display.DisplayTimer(_state);
            // Show the commands
            _display.DisplayInstructions();
        }
    }

    // Put everything back to start
    private void HandleReset()
    {
        // Stop timer
        _timer.Stop();
        // Remember timer is stopped
        _state.IsRunning = false;
        // Set to work session
        _state.SessionType = SessionType.Work;
        // Put time back to start
        _state.TimeRemaining = Constants.WorkDuration;
        // Set sessions to zero
        _state.SessionsCompleted = 0;
        // Show the timer on screen
        _display.DisplayTimer(_state);
        // Show the commands
        _display.DisplayInstructions();
    }

    // Stop program
    private void HandleQuit()
    {
        // Stop timer
        _timer.Stop();
        // Clear up timer
        _timer.Dispose();
        // Stop program
        Environment.Exit(0);
    }
}
