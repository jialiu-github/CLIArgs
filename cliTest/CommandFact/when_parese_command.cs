using System;
using System.Linq;
using cliArgs;
using cliArgs.command;
using Xunit;

namespace cliArgsTest.CommandFact
{
    public class when_parese_command
    {
        [Fact]
        public void should_get_correct_command_when_parese_success()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .EndCommand()
                .Build();
            ArgsParsingResult result = parser.Parse(Array.Empty<string>());
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Command);
            Assert.Null(result.Command.Symbol);
        }

        [Fact]
        public void should_get_null_command_when_parese_failed()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .EndCommand()
                .Build();
            ArgsParsingResult result = parser.Parse(new [] { "--flat" });
            Assert.False(result.IsSuccess);
            Assert.Null(result.Command);
        }

        [Fact]
        public void should_get_empty_string_when_get_default_command_description()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("flat", 'f')
                .EndCommand()
                .Build();
            ArgsParsingResult result = parser.Parse(new [] { "--flat" });
            Assert.True(result.IsSuccess);
            Assert.Equal(string.Empty, result.Command.Description);
        }

        [Fact]
        public void should_get_empty_list_when_command_has_no_registered_options()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .EndCommand()
                .Build();
            ArgsParsingResult result = parser.Parse(Array.Empty<string>());
            Assert.True(result.IsSuccess);

            var count = result.Command.GetRegisteredOptionsMetadata().Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public void should_get_all_options_when_get_registered_options_metadata_from_command()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("flag", 'f', "flag description")
                .EndCommand()
                .Build();
            ArgsParsingResult result = parser.Parse(new [] {"--flag"});
            Assert.True(result.IsSuccess);

            IOptionDefinitionMetadata[] optionDefinitionMetadatas =
                result.Command.GetRegisteredOptionsMetadata().ToArray();
            IOptionDefinitionMetadata flagMetadata =
                optionDefinitionMetadatas.Single(
                    d => d.SymbolMetadata.FullForm.Equals("flag",
                        StringComparison.OrdinalIgnoreCase));
            Assert.Equal("flag", flagMetadata.SymbolMetadata.FullForm);
            Assert.Equal('f', flagMetadata.SymbolMetadata.Abbreviation);
            Assert.Equal("flag description", flagMetadata.Description);
        }
    }
}
