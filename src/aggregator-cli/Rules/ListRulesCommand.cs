﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aggregator.cli
{
    [Verb("list.rules", HelpText = "List the rule in existing Aggregator instance in Azure.")]
    class ListRulesCommand : CommandBase
    {
        [Option('i', "instance", Required = true, HelpText = "Aggregator instance name.")]
        public string Instance { get; set; }

        internal override async Task<int> RunAsync()
        {
            var azure = AzureLogon.Load()?.Logon();
            if (azure == null)
            {
                WriteError($"Must logon.azure first.");
                return 2;
            }
            var rules = new AggregatorRules(azure);
            bool any = false;
            foreach (var item in await rules.List(Instance))
            {
                WriteOutput(
                    item,
                    (data) => $"Rule {item.name} {(item.config.disabled ? "(disabled)" : string.Empty)}");
                any = true;
            }
            if (!any)
            {
                WriteInfo($"No rules found in aggregator instance {Instance}.");
            }
            return 0;
        }
    }
}