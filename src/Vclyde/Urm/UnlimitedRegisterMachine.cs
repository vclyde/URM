using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Vclyde.Urm
{
    public sealed class UnlimitedRegisterMachine
    {
        public List<int> Register { get; } 
        public List<string> Instruction { get; } 
        private int instructionCtr = 1; // Starts at 1

        public UnlimitedRegisterMachine()
        {
            Register = new List<int>(new int[1000]);
            Instruction = new List<string>(20);
            Instruction.Insert(0, "");
        }

        public static bool IsSyntaxCorrect(string instruction)
        {
            return Regex.IsMatch(instruction.Trim(), @"(S|J|T|Z)\((\s*)(\d+)(,\s*\d+)*(\s*)\)");
        }

        public void AddInstruction(string instruction)
        {
            if (IsSyntaxCorrect(instruction))
                Instruction.Add(instruction);
            else if (String.IsNullOrWhiteSpace(instruction) || String.IsNullOrEmpty(instruction))
                return;
            else
                throw new ArgumentException("Invalid instruction syntax -> " + instruction);
        }

        public void Execute()
        {
            instructionCtr = 1;
            while (instructionCtr < Instruction.Count)
            {
                string instruction = Instruction[instructionCtr].Trim();
                char operation = instruction[0];

                string[] num = instruction.Split(new char[] { ',', ' ', 'Z', 'S', 'T', 'J', '(', ')' },
                            StringSplitOptions.RemoveEmptyEntries);

                switch (operation)
                {
                    case 'Z':
                        Zero(Convert.ToInt32(num[0]));
                        break;
                    case 'S':
                        Successor(Convert.ToInt32(num[0]));
                        break;
                    case 'T':
                        Transfer(Convert.ToInt32(num[0]), Convert.ToInt32(num[1]));
                        break;
                    case 'J':
                        Jump(Convert.ToInt32(num[0]), Convert.ToInt32(num[1]), Convert.ToInt32(num[2]));
                        break;
                    default:
                        throw new InvalidOperationException("Invalid " + operation);
                }
            }

            Console.WriteLine("Output: {0}\n", Register[0]);
        }

        private void Zero(int n)
        {
            Register[n] = 0;
            instructionCtr++;
        }

        private void Successor(int n)
        {
            Register[n] = Register[n] + 1;
            instructionCtr++;
        }

        private void Transfer(int m, int n)
        {
            Register[n] = Register[m];
            instructionCtr++;
        }

        private void Jump(int m, int n, int p)
        {
            if (Register[m] == Register[n])
                instructionCtr = p;
            else
                instructionCtr++;
        }
    }
}