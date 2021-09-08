using System;

/*
  Variables

    Declaration
      Reserving some space in memory (labeling folder)

      Example:
        int number;
    
    Assignment
      Giving/updating value to a variable


      Example:
        number = 32;
        

    Initialization
      Combination of declaration and assignment in 1 step

      Example:
        string name = "cj";         
 */

int GetUserInput(string message)
{
  int value;

  do
  {
    Console.WriteLine(message);
  }
  while (!int.TryParse(Console.ReadLine(), out value));

  return value;
}

int age = GetUserInput("What is your age?");
Console.WriteLine($"Your age is {age}");

/*
  int - whole number
  float - scientific notation 0.000000032
  double - decimal (good for prices)
  long   

  decimal 
*/


