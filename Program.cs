using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Create a place to store timer info
        var state = new TimerState();
        var display = new TimerDisplay();
        // Create session manager
        var session = new PomodoroSession(state);
        // Set up the timer
        var timer = new TimerEngine(
            Constants.UpdateInterval,
            () => OnTimerTick(state, session, display)
        );
        // All together
        var commandHandler = new CommandHandler(timer, session, display, state);

        // Show welcome message
        display.DisplayWelcome();
        // Show timer on screen
        display.DisplayTimer(state);
        // Show the commands
        display.DisplayInstructions();

        // Keep running forever
        while (true)
        {
            // Check if someone pressed a key
            if (Console.KeyAvailable)
            {
                // See which key was pressed
                var key = Console.ReadKey(true);
                // Do what the user asked for
                commandHandler.Handle(key);
            }
            Thread.Sleep(50);
        }
    }

    // What happens when timer ticks
    private static void OnTimerTick(TimerState state, PomodoroSession session, TimerDisplay display)
    {
        // Stop if timer isn't going
        if (!state.GetIsRunning())
            return;

        // Take time off the clock
        var sessionComplete = session.Tick();

        // Check if session is done
        if (sessionComplete)
        {
            // Get ready for next session
            var nextType = session.CompleteSession();
            // Show the done message
            display.DisplayCompletion(state.GetSessionType(), state.GetSessionsCompleted());
            // Ask what to do next
            display.DisplayNextSessionPrompt();
        }
        else
        {
            // Show timer on screen
            display.DisplayTimer(state);
        }
    }
}
