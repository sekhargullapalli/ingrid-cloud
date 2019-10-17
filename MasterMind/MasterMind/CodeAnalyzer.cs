using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterMind
{
    public class CodeAnalyzer : ICodeAnalyzer
    {
        public ICollection<KeyPeg> EvaluateGuess(IList<CodePeg> guess, IList<CodePeg> code)
        {
            //Step1: Estimating the perfect matches using the Zip extension method on two lists and then counting true values
            int perfectmatches = guess.Zip(code, (g, c) => g == c).Count(matched => matched);

            //Step2: Estimate right color guesses
            //First I get the common elements in both collections
            var commonElements = guess.Intersect(code);
            int rightColorGuesses = 0;
            foreach (var element in commonElements)
            {
                //Possible scenarios
                //1. One or both lists will not contain the element
                //2. Both lists will contain same amount of elements
                //3. One of the lists will contain fewer elements
                //In all cases, one need to take the minimum matches
                int gCount = guess.Count(g => g == element);
                int cCount = code.Count(c => c == element);
                rightColorGuesses += Math.Min(gCount, cCount);
            }
            //Reducing perfect matches as they are right color guesses as well!
            rightColorGuesses -= perfectmatches;

            List<KeyPeg> key = new List<KeyPeg>();
            key.AddRange(Enumerable.Repeat(KeyPeg.Black, perfectmatches));
            key.AddRange(Enumerable.Repeat(KeyPeg.White, rightColorGuesses));
            return key;
        }
    }


}
