using System;
using System.Collections.Generic;
using Xunit;

namespace MasterMind.UnitTests
{
    public class CodeAnalyzerTests
    {
        [Fact]
        public void EvaluateGuessEmptyCode_ThrowsException()
        {
            Action act = () =>
            {
                CodePeg[] code = {  };
                CodePeg[] guess = { CodePeg.Blue, CodePeg.Blue, CodePeg.Green, CodePeg.Green };
                var key = new CodeAnalyzer().EvaluateGuess(guess, code);
            };
            var exception = Record.Exception(act);
            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact]
        public void EvaluateGuess_UnequalInputSize_ThrowsException()
        {
            Action act = () => 
            {
                CodePeg[] code = { CodePeg.Blue, CodePeg.Green };
                CodePeg[] guess = { CodePeg.Blue, CodePeg.Blue, CodePeg.Green, CodePeg.Green };
                var key = new CodeAnalyzer().EvaluateGuess(guess, code);
            };
            var exception = Record.Exception(act);
            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
        }
    }
}
