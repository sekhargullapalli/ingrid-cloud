using System.Collections.Generic;

namespace MasterMind
{
    public interface ICodeAnalyzer
    {
        ICollection<KeyPeg> EvaluateGuess(IList<CodePeg> guess, IList<CodePeg> code);
    }
}
