using System;
using cliArgs;
using Xunit;

namespace cliArgsTest.CommandFact
{
    public class when_create_command
    {
        [Fact]
        public void should_can_create_default_command()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("flag", 'f', string.Empty)
                .EndCommand()
                .Build();
            string[] args = { "--flag" };
            ArgsParsingResult result = parser.Parse(args);
            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("--flag"));
        }

        [Fact]
        public void should_throw_exception_when_add_default_commend_more_then_once()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new ArgsParserBuilder()
                    .BeginDefaultCommand()
                    .AddFlagOption("flag", 'f', string.Empty)
                    .EndCommand()
                    .BeginDefaultCommand()
                    .AddFlagOption("some", 's', string.Empty)
                    .EndCommand());
        }
        //todo should_create_success_when_same_flag_add_to_different_command
    }
}