Root Application - Tracking Drive History

Language: C# (.NET framework 4.5)

General Approach to problem:

I started with a very high level SUDO code design as follows:

            // Validate input file
            // Open Stream and read and process file line by line
	    //     Space delimited string to commands
	    //	   Commands are stored in enums (Driver and trip)
            //     Validate commands
	    //	   Process command
            // Generate Report

Then I started designing the Interfaces and the relationships between them.

When designing the MotorVehicleSystem Class, I chose to use a Dictionary with the "Name" of the driver as the KEY.
My reasoning for a dictionary was for the fast lookups and the ability ensure no duplicate keys.

For the report, I did project the dictionary values onto a list for faster iteration of the contents. I also leveraged the .NET framework's LINQ library to easily filter out the drivers who averaged less than 5 mph and over 100mph and ordering the results.

Start and end times were kept in TimeSpans which is a powerful way to track time intervals.

For testing, I followed TDD, by creating the unit tests functions once I had my methods/properties stubbed out.

Miscellanous Requirement Assumptions:

- If driver is not registered in the system you cannot add trip.
- Cannot register multiple drivers with the same name.\

C# compiler resource:

https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/command-line-building-with-csc-exe