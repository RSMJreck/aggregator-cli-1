﻿using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace aggregator.Engine.Language {
    internal class PreprocessedRule : IPreprocessedRule
    {
        public PreprocessedRule()
        {
            Impersonate = false;
            Language = RuleLanguage.Unknown;
            References = new List<string>();
            Imports = new List<string>();
            FirstCodeLine = 0;
            RuleCode = new List<string>();
        }

        public bool Impersonate { get; set; }

        public RuleLanguage Language { get; internal set; }

        IReadOnlyList<string> IPreprocessedRule.References => new ReadOnlyCollection<string>(References);

        IReadOnlyList<string> IPreprocessedRule.Imports => new ReadOnlyCollection<string>(Imports);

        IReadOnlyList<string> IPreprocessedRule.RuleCode => new ReadOnlyCollection<string>(RuleCode);

        int IPreprocessedRule.FirstCodeLine => FirstCodeLine;


        public IList<string> References { get; }

        public IList<string> Imports { get; }

        public int FirstCodeLine { get; internal set; }

        public IList<string> RuleCode { get; }
    }
}