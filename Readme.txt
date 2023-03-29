The code is used to log different messages throughout an application. We want to improve the source code by allowing: 

- Log messages to different destinations such as text files, console, databases, etc. A message can be logged to multiple destinations at a time.
- Mark messages as Message, Warning, or Error. A message can only have a type at a time.
- All settings should be stored on the application config file.

For this you should have into consideration the following:

- Apply SOLID principles into the solution.
- Apply the following design patterns: Singleton, Strategy.
- Use dependency injection.