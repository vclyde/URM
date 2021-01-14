using System;
using System.Text.RegularExpressions;

namespace Vclyde.Urm
{
    public class Program
    {
        static void Main(string[] args)
        {
            UnlimitedRegisterMachine urm = new UnlimitedRegisterMachine();
            Console.WriteLine("Welcome to Unlimited Register Machine!");
            Console.WriteLine("Enter Instructions here: ");
            String input;
            int ctr = 0;
            do
            {
                Console.Write(++ctr + ". ");
                input = Console.ReadLine();
                urm.AddInstruction(input);
            } while (!String.IsNullOrWhiteSpace(input));

            Console.WriteLine("\nInput: ");
            ctr = 0;
            do
            {
                input = Console.ReadLine();
                if (!(String.IsNullOrWhiteSpace(input) || String.IsNullOrEmpty(input)))
                {
                    urm.Register[ctr++] = Convert.ToInt32(input);
                }
            } while (!String.IsNullOrWhiteSpace(input));

            urm.Execute();
            // Console.WriteLine(urm.Instruction.Count + "/" + urm.Instruction.Capacity);
        }
    }
}