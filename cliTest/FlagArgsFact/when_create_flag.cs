using System;
using cliArgs;
using Xunit;

namespace cliArgsTest.FlagArgsFact
{
    public class when_create_flag
    {
        [Fact]
        public void should_throw_exception_when_a_flag_has_neither_full_form_nor_abbreviation_form()
        {
            Assert.Throws<ArgumentException>(
                () => new ArgsParserBuilder()
                    .BeginDefaultCommand()
                    .AddFlagOption(null, null)
                    .EndCommand()
                    .Build());
        }

        [Fact]
        public void should_throw_exception_when_abbreviation_form_flag_is_not_a_english_letter()
        {
            Assert.Throws<ArgumentException>(
                () => new ArgsParserBuilder()
                    .BeginDefaultCommand()
                    .AddFlagOption(null, '&')
                    .EndCommand()
                    .Build());
        }

        [Fact]
        public void should_throw_exception_when_full_form_flag_is_empty_string()
        {
            Assert.Throws<ArgumentException>(
                () => new ArgsParserBuilder()
                    .BeginDefaultCommand()
                    .AddFlagOption("", 'f')
                    .EndCommand()
                    .Build());
        }

        [Fact]
        public void should_throw_exception_when_full_form_flag_has_invalid_character()
        {
            Assert.Throws<ArgumentException>(
                () => new ArgsParserBuilder()
                    .BeginDefaultCommand()
                    .AddFlagOption("-flag", 'f')
                    .EndCommand()
                    .Build());
        }

        [Fact]
        public void should_throw_exception_when_defind_ambiguity_abbr_form_flags()
        {
            Assert.Throws<ArgumentException>(
                () => new ArgsParserBuilder()
                    .BeginDefaultCommand()
                    .AddFlagOption("flag", 'f')
                    .AddFlagOption("some", 'f')
                    .EndCommand()
                    .Build());
        }

        [Fact]
        public void should_throw_exception_when_defind_ambiguity_full_form_flags()
        {
            Assert.Throws<ArgumentException>(
                () => new ArgsParserBuilder()
                    .BeginDefaultCommand()
                    .AddFlagOption("flag", 'f')
                    .AddFlagOption("flag", 'l')
                    .EndCommand()
                    .Build());
        }
    }
}
