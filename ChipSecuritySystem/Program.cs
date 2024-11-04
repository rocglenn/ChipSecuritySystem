
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChipSecuritySystem;

class Program
{
    const Color StartingColor = Color.Blue;
    const Color EndingColor = Color.Green;
    
    public static void Main(string[] args)
    {
        List<ColorChip> chips = new List<ColorChip>
        {
            new (Color.Blue, Color.Purple),
            new(Color.Blue, Color.Yellow),
            new(Color.Yellow, Color.Red),
            new(Color.Red, Color.Orange),
            new(Color.Orange, Color.Purple),
            new(Color.Purple, Color.Blue),
            new(Color.Purple, Color.Green),
        };

        var result = BuildChipChain(chips);
        Console.WriteLine(ChipResultToString(result));
    }
    
    private static List<ColorChip>? BuildChipChain(List<ColorChip> chips)
    {
        //edge case: no chip starts with blue or ends with green
         bool hasStartingColor = chips.Any(c => c.StartColor == StartingColor);
         bool hasEndingColor = chips.Any(c => c.EndColor == EndingColor);
         if (!hasStartingColor || !hasEndingColor)
             return null;
        
         var result = new List<ColorChip>();
         var currentColor = StartingColor;
         var counter = chips.Count;
        
         //first pass, build a chain without chips ending in "green"
         while (counter > 0)
         {
             var chip = chips.FirstOrDefault(c => c.StartColor == currentColor);
             if (chip == null) break;
             
             //skip green ending chips
             if (chip.EndColor == EndingColor)
             {
                 counter--;
                 continue;
             }
             
             result.Add(chip);
             currentColor = chip.EndColor;
             chips.Remove(chip);
         }
        
         //final pass, look specifically for the green ending chips
         counter = chips.Count;
         while (counter > 0)
         {
             var chip = chips.FirstOrDefault(c => c.StartColor == currentColor);
             if (chip == null) break;
             
             result.Add(chip);
             currentColor = chip.EndColor;
             chips.Remove(chip);
             counter--;
         }
         return result;
    }

    private static string ChipResultToString(List<ColorChip>? chips)
    {
        if (chips == null || !chips.Any() || !(chips[0].StartColor == StartingColor && chips[^1].EndColor == EndingColor)) 
            return Constants.ErrorMessage;
        
        var result = new StringBuilder();
        result.Append($"{StartingColor}");
        foreach (var chip in chips)
        {
            result.Append($" [{chip.ToString()}] ");
        }
        result.Append($"{EndingColor}");
        return result.ToString();
    }
}