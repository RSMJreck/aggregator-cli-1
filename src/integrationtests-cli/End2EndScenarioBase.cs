﻿using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit.Abstractions;

namespace integrationtests.cli
{
    public abstract class End2EndScenarioBase
    {
        static protected TestLogonData TestLogonData = new TestLogonData(
            // CI scenario
            Environment.GetEnvironmentVariable("DOWNLOADSECUREFILE_SECUREFILEPATH")
            // Visual Studio
            ?? "logon-data.json");

        private readonly ITestOutputHelper _output;

        protected End2EndScenarioBase(ITestOutputHelper output)
        {
            _output = output;
        }

        protected (int rc, string output) RunAggregatorCommand(string commandLine)
        {
            // see https://stackoverflow.com/a/14655145/100864
            var args = Regex.Matches(commandLine, @"[\""](?<a>.+?)[\""]|(?<a>[^ ]+)")
                .Cast<Match>()
                .Select(m => m.Groups["a"].Value)
                .ToArray();

            var saveOut = Console.Out;
            var saveErr = Console.Error;
            var buffered = new StringWriter();
            Console.SetOut(buffered);
            Console.SetError(buffered);

            var rc = aggregator.cli.Program.Main(args);

            Console.SetOut(saveOut);
            Console.SetError(saveErr);

            var output = buffered.ToString();
            _output.WriteLine(output);

            return (rc, output);
        }
    }
}