using System;

public class TimerDisplay
{
    // Show timer on screen
    public void DisplayTimer(TimerState state)
    {
        // Clear the screen
        Console.Clear();
        // Turn seconds into time we can show
        TimeSpan timeSpan = TimeSpan.FromSeconds(state.TimeRemaining);

        // Pick the right name for the session
        string sessionType = state.SessionType == SessionType.Work ? "WORK SESSION" : "BREAK TIME";
        Text.WriteTitle(sessionType);

        // Show how much time is left
        Console.WriteLine($"    {timeSpan.ToString(Constants.TimerFormat)}");
        // Show a blank line
        Console.WriteLine();

        // Work out what the timer is doing
        string status = state.IsRunning ? "RUNNING" : "PAUSED";
        // Show if timer is going or stopped
        Console.WriteLine($"    Status: {status}");
        // Show a blank line
        Console.WriteLine();

        // Show how many sessions we've done
        Console.WriteLine($"    Sessions: {state.SessionsCompleted}");
    }

    // Show the commands user can use
    public void DisplayInstructions()
    {
        // Show a blank line
        Console.WriteLine();
        Text.WriteTitle("COMMANDS");
        // Show all the commands in a box
        Text.WriteBox(
            "[S] Start/Resume",
            "[P] Pause",
            "[R] Reset",
            "[Q] Quit"
        );
    }

    // Show welcome message at the start
    public void DisplayWelcome()
    {
        // Clear the screen
        Console.Clear();
        Text.WriteTitle("POMODORO TIMER");
        // Show a blank line
        Console.WriteLine();
        // Show how long work is
        Console.WriteLine("  Work: 25 minutes");
        // Show how long break is
        Console.WriteLine("  Break: 5 minutes");
        // Show a blank line
        Console.WriteLine();
    }

    // Show message when session is done
    public void DisplayCompletion(SessionType type, int sessions)
    {
        // Clear the screen
        Console.Clear();

        // Work out what just finished
        if (type == SessionType.Work)
        {
            Text.WriteTitle("BREAK COMPLETE!");
        }
        else
        {
            Text.WriteTitle("WORK SESSION COMPLETE!");
        }
    }

    // Ask what to do next
    public void DisplayNextSessionPrompt()
    {
        // Show a blank line
        Console.WriteLine();
        // Ask to start next session
        Console.WriteLine("Press [S] to start next session...");
        // Ask to put timer back to start
        Console.WriteLine("Press [R] to reset timer...");
        // Ask to stop program
        Console.WriteLine("Press [Q] to quit...");
    }
}
