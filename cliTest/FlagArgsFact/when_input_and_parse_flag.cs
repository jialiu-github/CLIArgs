using System;
using cliArgs;
using Xunit;

namespace cliArgsTest.FlagArgsFact
{
    public class when_input_and_parse_flag
    {
        [Fact]
        public void should_be_false_when_input_empty_string()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("flag", 'f')
                .EndCommand()
                .Build();

            ArgsParsingResult result = parser.Parse(new [] { "" });
            Assert.False(result.GetFlagValue("-f"));
        }

        [Fact]
        public void should_be_true_when_defined_full_form_flag_begin_with_double_dash_line()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("flag", null)
                .EndCommand()
                .Build();

            ArgsParsingResult result = parser.Parse(new [] { "--flag" });
            Assert.True(result.GetFlagValue("--flag"));
        }

        [Fact]
        public void should_be_true_when_input_defined_full_form_flag()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("flag-n", null)
                .EndCommand()
                .Build();

            ArgsParsingResult result = parser.Parse(new [] { "--flag-n" });
            Assert.True(result.GetFlagValue("--flag-n"));
        }

        [Fact]
        public void should_be_true_when_defined_abbr_form_flag_begin_with_single_dash_line()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption(null, 'f')
                .EndCommand()
                .Build();

            ArgsParsingResult result = parser.Parse(new [] { "-f" });
            Assert.True(result.GetFlagValue("-f"));
        }

        [Fact]
        public void should_parse_faild_when_input_string_neither_begin_with_single_dash_line_nor_double_dash_line()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("flag", 'f')
                .EndCommand()
                .Build();
            var argsParsingResult = parser.Parse(new [] { "flag" });
            Assert.False(argsParsingResult.IsSuccess);
        }

        [Fact]
        public void should_throw_exception_when_get_value_after_parser_faild()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("flag", 'f')
                .EndCommand()
                .Build();
            var argsParsingResult = parser.Parse(new [] { "flag" });
            Assert.False(argsParsingResult.IsSuccess);
            Assert.Throws<InvalidOperationException>(() => argsParsingResult.GetFlagValue("--flag"));
        }

        [Fact]
        public void should_can_get_description_when_given_description()
        {
            string expectDescription = "some description";
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("flag", 'f', expectDescription)
                .EndCommand()
                .Build();
            string description = parser.Help("-f");
            Assert.Equal(expectDescription, description);
        }

        [Fact]
        public void should_can_get_empty_string_when_description_undefined()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("flag", 'f')
                .EndCommand()
                .Build();
            string description = parser.Help("-f");
            Assert.Equal(string.Empty, description);
        }

        [Fact]
        public void should_parse_faild_when_input_one_flag_twice()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("flag", 'f')
                .EndCommand()
                .Build();
            var argsParsingResult = parser.Parse(new [] { "--flag", "-f" });
            Assert.False(argsParsingResult.IsSuccess);
            Assert.Equal(ParseErrorCode.DuplicateFlagsInArgs, argsParsingResult.Error.Code);
        }

        [Fact]
        public void should_parse_faild_with_detail_information_when_input_undefined_flag()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("flag", 'f')
                .EndCommand()
                .Build();
            var argsParsingResult = parser.Parse(new [] { "--flat" });
            Assert.False(argsParsingResult.IsSuccess);
            Assert.Equal(ParseErrorCode.FreeValueNotSupported, argsParsingResult.Error.Code);
            Assert.Equal("--flat", argsParsingResult.Error.Trigger);
        }

        [Fact]
        public void should_parse_success_when_has_multiple_flags()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("flag", 'f')
                .AddFlagOption("edit", 'e')
                .EndCommand()
                .Build();
            var argsParsingResult = parser.Parse(new [] { "--edit" });
            Assert.True(argsParsingResult.IsSuccess);
            Assert.True(argsParsingResult.GetFlagValue("--edit"));
            Assert.False(argsParsingResult.GetFlagValue("--flag"));
        }

        [Fact]
        public void should_parse_success_when_input_multiple_flags()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("no-edit", 'n')
                .AddFlagOption("amend", 'a')
                .EndCommand()
                .Build();
            var argsParsingResult = parser.Parse(new [] { "--no-edit", "-a" });
            Assert.True(argsParsingResult.IsSuccess);
            Assert.True(argsParsingResult.GetFlagValue("--no-edit"));
            Assert.True(argsParsingResult.GetFlagValue("--amend"));
        }

        [Fact]
        public void should_throw_exception_when_input_any_null_arguments()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("no-edit", 'n')
                .EndCommand()
                .Build();
            Assert.Throws<ArgumentException>(() => parser.Parse(new [] { "--no-edit", null }));
            Assert.Throws<ArgumentNullException>(() => parser.Parse(null));
        }

        [Fact]
        public void should_throw_exception_when_get_value_of_undefined_flag()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("no-edit", 'n')
                .EndCommand()
                .Build();
            ArgsParsingResult argsParsingResult = parser.Parse(new [] { "--no-edit" });

            Assert.Throws<ArgumentException>(() => { argsParsingResult.GetFlagValue("--not-this-flag"); });
        }

        [Fact]
        public void should_parse_success_when_input_combined_flags()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("read", 'r')
                .AddFlagOption("force", 'f')
                .AddFlagOption("check", 'c')
                .EndCommand()
                .Build();

            ArgsParsingResult argsParsingResult = parser.Parse(new [] { "-rf" });
            Assert.True(argsParsingResult.IsSuccess);
            Assert.True(argsParsingResult.GetFlagValue("--read"));
            Assert.True(argsParsingResult.GetFlagValue("--force"));
            Assert.False(argsParsingResult.GetFlagValue("--check"));
        }

        [Fact]
        public void should_parse_success_when_parse_twice()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("read", 'r')
                .AddFlagOption("force", 'f')
                .EndCommand()
                .Build();

            ArgsParsingResult argsParsingResult = parser.Parse(new [] { "-r" });
            Assert.True(argsParsingResult.IsSuccess);
            Assert.True(argsParsingResult.GetFlagValue("--read"));
            Assert.False(argsParsingResult.GetFlagValue("-f"));

            parser.Parse(new [] {"-f"});
            Assert.True(argsParsingResult.IsSuccess);
            Assert.False(argsParsingResult.GetFlagValue("--read"));
            Assert.True(argsParsingResult.GetFlagValue("-f"));
        }

        [Fact]
        public void should_ingore_case_when_parese()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("read", 'r')
                .AddFlagOption("force", 'f')
                .AddFlagOption("check", 'c')
                .AddFlagOption("amend", 'a')
                .EndCommand()
                .Build();

            ArgsParsingResult argsParsingResult = parser.Parse(new [] { "-C", "--ForCe", "-Ra" });
            Assert.True(argsParsingResult.IsSuccess);
            Assert.True(argsParsingResult.GetFlagValue("--read"));
            Assert.True(argsParsingResult.GetFlagValue("--force"));
            Assert.True(argsParsingResult.GetFlagValue("--check"));
            Assert.True(argsParsingResult.GetFlagValue("--amend"));
        }

        [Fact]
        public void should_parser_faild_when_input_undefinded_flag_in_combined_flags()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("read", 'r')
                .AddFlagOption("check", 'c')
                .EndCommand()
                .Build();

            ArgsParsingResult argsParsingResult = parser.Parse(new [] { "-rf" });
            Assert.False(argsParsingResult.IsSuccess);
            Assert.Equal(ParseErrorCode.FreeValueNotSupported, argsParsingResult.Error.Code);
            Assert.Equal("-rf", argsParsingResult.Error.Trigger);
        }

        [Fact]
        public void should_parse_faild_when_input_duplicate_flag_in_combined_flags()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("read", 'r')
                .AddFlagOption("check", 'c')
                .AddFlagOption("force", 'f')
                .EndCommand()
                .Build();

            ArgsParsingResult argsParsingResult = parser.Parse(new [] { "-f", "--read","-rc" });
            Assert.False(argsParsingResult.IsSuccess);
            Assert.Equal(ParseErrorCode.DuplicateFlagsInArgs, argsParsingResult.Error.Code);
            Assert.Equal("--read", argsParsingResult.Error.Trigger);
        }

        [Fact]
        public void should_throw_exception_when_get_value_by_combined_input()
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("read", 'r')
                .AddFlagOption("check", 'c')
                .AddFlagOption("force", 'f')
                .EndCommand()
                .Build();
            ArgsParsingResult argsParsingResult = parser.Parse(new [] { "-f" });

            Assert.Throws<ArgumentException>(() => { argsParsingResult.GetFlagValue("-rc"); });
        }
    }
}
