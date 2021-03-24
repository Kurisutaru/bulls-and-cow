using System;
using System.Collections.Generic;
using System.Linq;

namespace BullsAndCows
{
  class Program
  {
    static int bullCount = 0;
    static int cowCount = 0;
    static Random random = new Random();
    static bool win = false;

    static IList<string> secretKey = new List<string>();
    static IList<string> inputKey = new List<string>();
    static IList<InputHistory> inputHistory = new List<InputHistory>();

    static void Main(string[] args)
    {
      while(secretKey.Count < 4)
      {
        string tmp = Convert.ToString(random.Next(0, 9));
        if(!secretKey.Contains(tmp))
        {
          secretKey.Add(tmp);
        }
      }

      while(!win)
      {
        Console.Clear();
        PrintHistory();

        //Give Space
        Console.Write("Your Guess [4 Digit] ? ");
        string tmp = Console.ReadLine();

        if(!string.IsNullOrEmpty(tmp) && tmp.Length == 4 && !NonDuplicateCheck(tmp))
        {
          EngineCheck(tmp);

          if(bullCount == 4)
          {
            win = true;
            Console.Clear();
            PrintHistory();
            Console.WriteLine("You win !");
            Console.Write("Press any key to exit . . .");
            Console.Read();
          }
        }
      }
    }

    public static void EngineCheck(string input)
    {
      //Set Default
      bullCount = 0;
      cowCount = 0;
      inputKey.Clear();

      for (int i = 0; i < input.Length; i++)
      {
        inputKey.Add(input[i].ToString());
      }

      for (int i = 0; i < secretKey.Count; i++)
      {
        if(secretKey[i] == inputKey[i])
        {
          bullCount++;
        }
        else if (secretKey.Contains(inputKey[i]))
        {
          cowCount++;
        }
      }

      inputHistory.Add(new InputHistory() { input = input, result = $"Bull : {bullCount}, Cow : {cowCount}" });

    }

    public static void PrintHistory()
    {
      if (inputHistory.Count > 0)
      {
        Console.WriteLine($"Input History");
        Console.WriteLine($"=============");
        for (int i = 0; i < inputHistory.Count; i++)
        {
          Console.WriteLine($"{i+1} | {inputHistory[i].input} | {inputHistory[i].result}");
        }
        Console.WriteLine("=============");
      }
    }

    public static bool NonDuplicateCheck(string input)
    {
      var set = new HashSet<char>();
      return input.Any(x => !set.Add(x));
    }
  }

  class InputHistory
  {
    public string input { get; set; }
    public string result { get; set; }
  }
}
