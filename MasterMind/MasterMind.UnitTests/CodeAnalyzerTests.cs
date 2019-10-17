using System;
using System.Collections.Generic;
using System.Linq;
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
                CodePeg[] code = { };
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
       

        [Theory]
        [InlineData(new int[] { 1, 1, 2, 2 }, new int[] { 1, 2, 3, 1 }, new int[] { 1, 2 })]
        [InlineData(new int[] { 0, 0, 1, 1 }, new int[] { 1, 3, 2, 5 }, new int[] { 0, 1 })]
        [InlineData(new int[] {2, 2, 3, 3 }, new int[] { 1, 3, 2, 5 }, new int[] { 0, 2 })]
        [InlineData(new int[] { 1, 2, 3, 4 }, new int[] { 1, 3, 2, 5 }, new int[] { 1, 2 })]
        [InlineData(new int[] { 2, 1, 3, 5 }, new int[] { 1, 3, 2, 5 }, new int[] { 1, 3 })]
        [InlineData(new int[] { 1, 3, 2, 5 }, new int[] { 1, 3, 2, 5 }, new int[] { 4, 0 })]
        [InlineData(new int[] { 5, 2, 3, 1 }, new int[] { 1, 3, 2, 5 }, new int[] { 0, 4 })]
        public void CanEvaluateGuess(int[] guess, int[] code, int[] expected)
        {
            CodePeg[] Guess = guess.Select(g => (CodePeg)g).ToArray();
            CodePeg[] Code = code.Select(c => (CodePeg)c).ToArray();
            List<KeyPeg> key = new CodeAnalyzer().EvaluateGuess(Guess, Code).ToList();
            int[] result = { key.Count(k => k == KeyPeg.Black), key.Count(k => k == KeyPeg.White) };
            Assert.Equal(expected, result);
        }
    }
}
