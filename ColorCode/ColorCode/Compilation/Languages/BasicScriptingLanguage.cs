// Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Collections.Generic;
using ColorCode.Common;

namespace ColorCode.Compilation.Languages
{
    public class BasicScriptingLanguage : ILanguage
    {
        public string Id
        {
            get { return LanguageId.BasicScriptingLanguage; }
        }

        public string Name
        {
            get { return "BasicScriptingLanguage"; }
        }

        public string CssClassName
        {
            get { return "bsl"; }
        }

        public string FirstLinePattern
        {
            get
            {
                return null;
            }
        }

        public IList<LanguageRule> Rules
        {
            get
            {
                return new List<LanguageRule>
                           {
                               new LanguageRule(
                                   @"/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/",
                                   new Dictionary<int, string>
                                       {
                                           { 0, ScopeName.Comment },
                                       }),
                               new LanguageRule(
                                   @"(///)(?:\s*?(<[/a-zA-Z0-9\s""=]+>))*([^\r\n]*)",
                                   new Dictionary<int, string>
                                       {
                                           { 1, ScopeName.XmlDocTag },
                                           { 2, ScopeName.XmlDocTag },
                                           { 3, ScopeName.XmlDocComment },
                                       }),
                               new LanguageRule(
                                   @"(#.*?)\r?$",
                                   new Dictionary<int, string>
                                       {
                                           { 1, ScopeName.Comment }
                                       }),
                               new LanguageRule(
                                   @"'[^\n]*?(?<!\\)'",
                                   new Dictionary<int, string>
                                       {
                                           { 0, ScopeName.String }
                                       }),
                               new LanguageRule(
                                   @"(?s)@""(?:""""|.)*?""(?!"")",
                                   new Dictionary<int, string>
                                       {
                                           { 0, ScopeName.StringCSharpVerbatim }
                                       }),
                               new LanguageRule(
                                   @"(?s)(""[^\n]*?(?<!\\)"")",
                                   new Dictionary<int, string>
                                       {
                                           { 0, ScopeName.String }
                                       }),
                               new LanguageRule(
                                   @"\[(assembly|module|type|return|param|method|field|property|event):[^\]""]*(""[^\n]*?(?<!\\)"")?[^\]]*\]",
                                   new Dictionary<int, string>
                                       {
                                           { 1, ScopeName.Keyword },
                                           { 2, ScopeName.String }
                                       }),
                               /*new LanguageRule(
                                   @"^\s*(\#define|\#elif|\#else|\#endif|\#endregion|\#error|\#if|\#line|\#pragma|\#region|\#undef|\#warning).*?$",
                                   new Dictionary<int, string>
                                       {
                                           { 1, ScopeName.PreprocessorKeyword }
                                       }),*/
                               new LanguageRule(
                                   @"\b(wait|declare|showQuestionDialog|echo)\b",
                                   new Dictionary<int, string>
                                       {
                                           { 1, ScopeName.Keyword }
                                       }),
                           };
            }
        }

        public bool HasAlias(string lang)
        {
            switch (lang.ToLower())
            {
                case "bsl":
                    return true;

                default:
                    return false;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
