using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Turing;

public class TuringMachine
{
    public static List<Rule> LoadRulesFromFile(string path)
    {
        var result = new List<Rule>();
        foreach (var line in File.ReadAllLines(path))
        {
            var data = line.Split(' ');
            result.Add(new Rule()
            {
                InitState = data[0],
                InitInput = data[1],
                NextState = data[3],
                Action = data[2] switch
                {
                    "R" => RuleAction.MOVE_RIGHT,
                    "L" => RuleAction.MOVE_LEFT,

                    _ => RuleAction.REPLACE
                },
                Substitution = char.Parse(data[2]),
            });
        }

        return result;
    }
    public enum RuleAction
    {
        MOVE_LEFT,
        MOVE_RIGHT,
        REPLACE
    }

    public class Rule
    {
        public string InitState { get; set; }
        public string InitInput { get; set; }
        public string NextState { get; set; }
        public RuleAction Action { get; set; }
        public char? Substitution { get; set; }
    }

    private List<Rule> _rules;
    private string _state = string.Empty;
    private int _position = 0;

    public TuringMachine(List<Rule> rules)
    {
        _rules = rules;
    }

    public void Test(string state, string input)
    {
        _position = 0;
        _state = state;

        while (_position < input.Length && _position >= 0)
        {
            
            var rule = _rules.Find(r => r.InitState == _state && r.InitInput == input[_position].ToString());
            if (rule != null)
            {
                switch (rule.Action)
                {
                    case RuleAction.MOVE_LEFT:
                        _position--;
                        break;
                    case RuleAction.MOVE_RIGHT:
                        _position++;
                        break;
                    case RuleAction.REPLACE:
                        var stringBuilder = new StringBuilder(input);
                        if(rule.Substitution is not null)
                            stringBuilder[_position] = (char)rule.Substitution;
                        input = stringBuilder.ToString();
                        break;
                }
                Console.WriteLine($"{input.Substring(0, _position)}({_state}){input.Substring(_position)}");
                _state = rule.NextState;
                
            }
            else
            {
                break;
            }

        }

        Console.WriteLine("\n");
    }

}