using System; 
using System.Timers;

class Program
{
    // === CONSTANTS === //
    const int WORK_DURATION = 25 * 60; // Default work length (25*60s = 25 minutes)
    const int BREAK_DURATION = 5 * 60; // Default break duration (every 25 mins, break for 5 mins)
    const int UPDATE_INTERVAL = 1000; // 1000ms = 1s, timer tick rate
    const string TIMER_FORMAT = "mm\\:ss"; // Display format: shows as "25:00" 

    // === VARIABLES === //
    static int timeRemaining; // Countdown in secs
    static bool isWorkSession = true; // true = Work, false = Break
    static bool isRunning = false; // true = Timer counting down
    static System.Timers.Timer? timer; // Timer object: the actual timer that fires events
    static int sessionsCompleted = 0; // How many work sessions fininshed

    // === TIMER SETUP === //
    
    static void SetupTimer()
    {
        timer = new System.Timers.Timer(UPDATE_INTERVAL);  // Create timer object
        timer.Elapsed += OnTimerTick; // Hook to event handler; what runs when timer ticks
        // Don't start automatically - wait for user to press start
        timer.AutoReset = true; // Keeps firing after each tick
        
        // Init state variables
        timeRemaining = WORK_DURATION;
        isWorkSession = true;
        isRunning = false;
        sessionsCompleted = 0;
    }

    // === EVENT HANDLER === //
        
    static void OnTimerTick(object? sender, ElapsedEventArgs e)
    {
        // only update if timer is actually running
        if (!isRunning)
            return; 

        timeRemaining--; // decrease timer by 1s
        DisplayTimer() ; // update the display
    
        if (timeRemaining <= 0) // check if session is complete
        {
            HandleSessionComplete();
        }
    }

    // === DISPLAY METHODS === //
    
    static void DisplayTimer()
    {
        Console.Clear();
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeRemaining); // convert seconds to TimeSpan for easy formatting

        // Display session type
        string sessionType = isWorkSession ? "WORK SESSION" : "BREAK TIME";
        Text.WriteTitle(sessionType);

        // Display countdown (mm:ss format)
        Console.WriteLine($"    {timeSpan.ToString(TIMER_FORMAT)}");
        Console.WriteLine();

        // Display status
        string status = isRunning ? "RUNNING" : "PAUSED";
        Console.WriteLine($"    Status: {status}");
        Console.WriteLine();

        // Display sessions completed
        Console.WriteLine($"    Sessions: {sessionsCompleted}");
    }

    static void DisplayInstructions()
    {
        Console.WriteLine();
        Text.WriteTitle("COMMANDS");
        Text.WriteBox(
            "[S] Start/Resume",
            "[P] Pause",
            "[R] Reset",
            "[Q] Quit"
        );
    }

    // === SESSION COMPLETION === //

    static void HandleSessionComplete()
    {
        timer!.Stop(); // Stop the timer
        isRunning = false;
        //[ONLY WORKS ON WINDOWS] Console.Beep(1000, 500); // Sound notification: frequency (1000hz), duration (500ms)
       
        Console.Clear(); // Display completion
        
        if (isWorkSession)
        {
            Text.WriteTitle("WORK SESSION COMPLETE!");
            sessionsCompleted++;
        }

        else
        {
            Text.WriteTitle("BREAK COMPLETE!");
        }
        
        Console.WriteLine();
        Console.WriteLine("Press [S] to start next session...");
        Console.WriteLine("Press [R] to reset timer...");
        Console.WriteLine("Press [Q] to quit...");

        isWorkSession = !isWorkSession; // Switch session type for next round
        timeRemaining = isWorkSession ? WORK_DURATION : BREAK_DURATION; // Reset time based on new session type
    }

    // === COMMAND HANDLER === //

    static void HandleCommand(ConsoleKeyInfo key)
    {
        switch (key.Key)
            {
                // Start or Resume
                case ConsoleKey.S:
                    if (!isRunning)
                    {
                        timer!.Start();
                        isRunning = true;
                        DisplayTimer();
                        DisplayInstructions();
                    }
                    break;
                
                // Pause
                case ConsoleKey.P: 
                    if (isRunning)
                    {
                        timer!.Stop();
                        isRunning = false;
                        DisplayTimer();
                        DisplayInstructions();
                    }
                    break;
                
                // Resume
                case ConsoleKey.R:
                    timer!.Stop();
                    isRunning = false;
                    isWorkSession = true;
                    timeRemaining = WORK_DURATION;
                    sessionsCompleted = 0;
                    DisplayTimer();
                    DisplayInstructions();
                    break;
                
                // Quit
                case ConsoleKey.Q:
                    timer!.Stop();
                    timer!.Dispose();
                    Environment.Exit(0);
                    break;
            }
    }


    // === MAIN METHOD === //

    static void Main(string[] args)
    {
        // Setup timer and init state
        SetupTimer();

        // Show welcome screen
        Console.Clear();
        Text.WriteTitle("POMODORO TIMER");
        Console.WriteLine();
        Console.WriteLine("  Work: 25 minutes");
        Console.WriteLine("  Break: 5 minutes");
        Console.WriteLine();

        // Display init state
        DisplayTimer();
        DisplayInstructions();

        // Main program loop
        while (true)
        {
            // Check if key is available
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true); // Read key without displaying it
                HandleCommand(key); // Handle the command
            }

            System.Threading.Thread.Sleep(50); // Small delay to prevent excessive CPU usage
        }
    }
}
