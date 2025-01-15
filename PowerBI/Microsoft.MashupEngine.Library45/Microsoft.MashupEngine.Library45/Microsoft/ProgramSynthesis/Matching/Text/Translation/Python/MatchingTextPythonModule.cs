using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Matching.Text.Translation.Python
{
	// Token: 0x0200123B RID: 4667
	public class MatchingTextPythonModule : PythonModule
	{
		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x06008C8D RID: 35981 RVA: 0x001D7A04 File Offset: 0x001D5C04
		public IReadOnlyList<string> Regexes
		{
			get
			{
				IReadOnlyList<string> readOnlyList;
				if ((readOnlyList = this._regexes) == null)
				{
					readOnlyList = (this._regexes = this.ComputeRegexes());
				}
				return readOnlyList;
			}
		}

		// Token: 0x06008C8E RID: 35982 RVA: 0x001D7A2C File Offset: 0x001D5C2C
		private IReadOnlyList<string> ComputeRegexes()
		{
			List<string> list = new List<string>();
			foreach (MatchingLabel matchingLabel in this._labels)
			{
				switch (matchingLabel.Match)
				{
				case MatchingLabel.MatchType.NullMatch:
					list.Add(null);
					break;
				case MatchingLabel.MatchType.TokenSequenceMatch:
				{
					RegexProfile regexProfile = this._regexProfiles[this._tokenSequences[matchingLabel]];
					list.Add(regexProfile.Regex);
					break;
				}
				}
			}
			return list;
		}

		// Token: 0x06008C8F RID: 35983 RVA: 0x001D7AC4 File Offset: 0x001D5CC4
		public MatchingTextPythonModule(Program program, string moduleName, string headerModuleName, string aliasName, MatchingTextPythonModule.TranslationOptions translationOptions)
			: base(moduleName, headerModuleName, aliasName, Array.Empty<string>())
		{
			this._program = program;
			this._translationOptions = translationOptions;
			IEnumerable<ProgramNode> disjuncts = this._program.Disjuncts;
			this._labels = this._program.Labels.ToList<MatchingLabel>();
			this._examples = disjuncts.ZipWith(this._labels).ToDictionary((Record<ProgramNode, MatchingLabel> pair) => pair.Item2, delegate(Record<ProgramNode, MatchingLabel> pair)
			{
				IReadOnlyDictionary<ProgramNode, IReadOnlyList<string>> examples = this._program.Examples;
				return ((examples != null) ? examples[pair.Item1] : null) ?? new List<string>();
			});
			this._outliers = program.OutlierSamples;
			this._sizes = disjuncts.ZipWith(this._labels).ToDictionary((Record<ProgramNode, MatchingLabel> pair) => pair.Item2, delegate(Record<ProgramNode, MatchingLabel> pair)
			{
				IReadOnlyDictionary<ProgramNode, uint> sizes = this._program.Sizes;
				if (sizes == null)
				{
					return 0U;
				}
				return sizes[pair.Item1];
			});
			this._tokenSequences = this._labels.Where((MatchingLabel label) => label.Match == MatchingLabel.MatchType.TokenSequenceMatch).ToDictionary((MatchingLabel label) => label, (MatchingLabel label) => label.GetTokens());
			this._regexProfiles = this._tokenSequences.Values.ToList<IReadOnlyList<IToken>>().RegexDescriptions();
		}

		// Token: 0x06008C90 RID: 35984 RVA: 0x001D7C30 File Offset: 0x001D5E30
		private string GenerateMatchingLabelRepresentation(MatchingLabel label, bool produceIsMatch)
		{
			bool flag;
			string text;
			switch (label.Match)
			{
			case MatchingLabel.MatchType.NullMatch:
				flag = true;
				text = "<Null>";
				break;
			case MatchingLabel.MatchType.NoMatch:
				flag = false;
				text = null;
				break;
			case MatchingLabel.MatchType.TokenSequenceMatch:
			{
				flag = true;
				IReadOnlyList<IToken> tokens = label.GetTokens();
				IEnumerable<string> enumerable = tokens.Select((IToken o) => o.ToString());
				text = (tokens.Any<IToken>() ? string.Join(" & ", enumerable) : "<Empty>");
				break;
			}
			default:
				throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Unknown {0}: {1}", new object[] { "MatchType", label.Match })));
			}
			string text2 = (flag ? "True" : "False");
			string text3 = text.ToPythonLiteral();
			if (!produceIsMatch)
			{
				return text3;
			}
			return string.Concat(new string[] { "(", text2, ", ", text3, ")" });
		}

		// Token: 0x06008C91 RID: 35985 RVA: 0x001D7D34 File Offset: 0x001D5F34
		private CodeBuilder GenerateRegexDefinitions(IReadOnlyDictionary<IReadOnlyList<IToken>, RegexProfile> regexProfiles, bool produceRegexObjects, out IDictionary<string, string> regexNames)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			if (produceRegexObjects)
			{
				codeBuilder.AppendLine("import regex");
				codeBuilder.AppendLine();
			}
			regexNames = new Dictionary<string, string>();
			int num = regexProfiles.Count - 1;
			int length = num.ToString().Length;
			foreach (Record<int, MatchingLabel> record in this._labels.Enumerate<MatchingLabel>())
			{
				MatchingLabel matchingLabel;
				record.Deconstruct(out num, out matchingLabel);
				int num2 = num;
				MatchingLabel matchingLabel2 = matchingLabel;
				if (matchingLabel2.Match == MatchingLabel.MatchType.TokenSequenceMatch)
				{
					IReadOnlyList<IToken> readOnlyList = this._tokenSequences[matchingLabel2];
					IReadOnlyList<string> readOnlyList2 = this._examples[matchingLabel2];
					RegexProfile regexProfile = this._regexProfiles[readOnlyList];
					uint num3 = this._sizes[matchingLabel2];
					uint numberOfInputs = this._program.NumberOfInputs;
					double num4 = num3 / numberOfInputs * 100.0;
					string text = "pattern_" + num2.ToString().PadLeft(length, '0');
					string text2 = regexProfile.Regex.ToPythonLiteral();
					string text3 = (produceRegexObjects ? ("regex.compile(" + text2 + ")") : text2);
					regexNames[regexProfile.Regex] = text;
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0} = {1}", new object[] { text, text3 })));
					codeBuilder.AppendLine(string.Format("# Number of matches: {0} of {1} ({2:0.##}%)", num3, numberOfInputs, num4));
					foreach (string text4 in readOnlyList2)
					{
						codeBuilder.AppendLine("# Example: " + text4.ToPythonLiteral());
					}
					codeBuilder.AppendLine();
					codeBuilder.AppendLine();
				}
			}
			if (this._outliers != null && this._outliers.Count > 0)
			{
				codeBuilder.AppendLine("# Some outliers were detected.");
				foreach (string text5 in this._outliers.Take(5))
				{
					codeBuilder.AppendLine("# Example: " + text5.ToPythonLiteral());
				}
				codeBuilder.AppendLine();
				codeBuilder.AppendLine();
			}
			return codeBuilder;
		}

		// Token: 0x06008C92 RID: 35986 RVA: 0x001D7FD8 File Offset: 0x001D61D8
		private string GenerateMatchingCondition(MatchingLabel label, string inputVariable, IDictionary<string, string> regexNames)
		{
			switch (label.Match)
			{
			case MatchingLabel.MatchType.NullMatch:
				return FormattableString.Invariant(FormattableStringFactory.Create("{0} is None", new object[] { inputVariable }));
			case MatchingLabel.MatchType.NoMatch:
				return FormattableString.Invariant(FormattableStringFactory.Create("False", Array.Empty<object>()));
			case MatchingLabel.MatchType.TokenSequenceMatch:
			{
				RegexProfile regexProfile = this._regexProfiles[this._tokenSequences[label]];
				return FormattableString.Invariant(FormattableStringFactory.Create("{0} is not None and {1}.match({2})", new object[]
				{
					inputVariable,
					regexNames[regexProfile.Regex],
					inputVariable
				}));
			}
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06008C93 RID: 35987 RVA: 0x001D807C File Offset: 0x001D627C
		private CodeBuilder GeneratePySparkClassifyFunction(IDictionary<string, string> regexNames, string dfName, string columnName, string functionName)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			List<string> list = new List<string>();
			foreach (MatchingLabel matchingLabel in this._labels)
			{
				string text = this.GenerateMatchingLabelRepresentation(matchingLabel, false);
				switch (matchingLabel.Match)
				{
				case MatchingLabel.MatchType.NullMatch:
					list.Add(string.Concat(new string[] { ".when(", dfName, "[", columnName, "].isNull(), ", text, ")" }));
					break;
				case MatchingLabel.MatchType.NoMatch:
					list.Add(".when(functions.lit(False), " + text + ")");
					break;
				case MatchingLabel.MatchType.TokenSequenceMatch:
				{
					RegexProfile regexProfile = this._regexProfiles[this._tokenSequences[matchingLabel]];
					string text2 = regexNames[regexProfile.Regex];
					list.Add(string.Concat(new string[] { ".when(", dfName, "[", columnName, "].rlike(", text2, "), ", text, ")" }));
					break;
				}
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
			using (codeBuilder.NewScope(functionName + " = (", 1U))
			{
				codeBuilder.Append("functions");
				foreach (string text3 in list)
				{
					codeBuilder.AppendLine(text3);
				}
			}
			codeBuilder.AppendLine(")");
			return codeBuilder;
		}

		// Token: 0x06008C94 RID: 35988 RVA: 0x001D8280 File Offset: 0x001D6480
		private CodeBuilder GenerateClassifyFunction(IDictionary<string, string> regexNames, string functionName)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("def {0}({1}):", new object[] { functionName, "input_str" })), 1U))
			{
				IEnumerable<string> enumerable = "if".Yield<string>().Concat("elif".Yield<string>().Repeat<string>());
				foreach (Record<MatchingLabel, string> record in this._labels.ZipWith(enumerable))
				{
					MatchingLabel matchingLabel;
					string text;
					record.Deconstruct(out matchingLabel, out text);
					MatchingLabel matchingLabel2 = matchingLabel;
					string text2 = text;
					string text3 = this.GenerateMatchingCondition(matchingLabel2, "input_str", regexNames);
					using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("{0} {1}:", new object[] { text2, text3 })), 1U))
					{
						string text4 = this.GenerateMatchingLabelRepresentation(matchingLabel2, true);
						IReadOnlyList<string> readOnlyList = this._examples[matchingLabel2];
						if (readOnlyList != null)
						{
							codeBuilder.AppendLine("# " + string.Join(", ", readOnlyList.Select((string ex) => ex.ToPythonLiteral())));
						}
						codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("return {0}", new object[] { text4 })));
					}
				}
				using (codeBuilder.NewScope("else:", 1U))
				{
					if (this._outliers != null && this._outliers.Count > 0)
					{
						codeBuilder.AppendLine("# " + string.Join(", ", from ex in this._outliers.Take(5)
							select ex.ToPythonLiteral()));
					}
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("return (False, \"<Unknown>\")", Array.Empty<object>())));
				}
			}
			return codeBuilder;
		}

		// Token: 0x06008C95 RID: 35989 RVA: 0x001D84EC File Offset: 0x001D66EC
		private CodeBuilder GenerateAssertion(string inputVariable, string regexNameList)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			string text = (this._labels.Any((MatchingLabel l) => l.Match == MatchingLabel.MatchType.NullMatch) ? (inputVariable + " is None or any(") : (inputVariable + " is not None and any("));
			using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("assert {0}", new object[] { text })), 1U))
			{
				codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("r.match({0}) is not None for r in {1}", new object[] { inputVariable, regexNameList })));
			}
			codeBuilder.AppendLine("), (\"The data point %s does not conform to the learned patterns.\" % " + inputVariable + ")");
			return codeBuilder;
		}

		// Token: 0x06008C96 RID: 35990 RVA: 0x001D85BC File Offset: 0x001D67BC
		public override string GenerateUnisolatedCode(OptimizeFor optimization)
		{
			return this.GenerateClusteringCode(optimization, "identify_pattern");
		}

		// Token: 0x06008C97 RID: 35991 RVA: 0x001D85CC File Offset: 0x001D67CC
		public string GenerateClusteringCode(OptimizeFor optimization, string functionName)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			bool flag = this._translationOptions.PythonTarget != PythonTarget.PySpark;
			IDictionary<string, string> dictionary;
			codeBuilder.Append(this.GenerateRegexDefinitions(this._regexProfiles, flag, out dictionary));
			if (this._translationOptions.PythonTarget != PythonTarget.PySpark)
			{
				codeBuilder.Append(this.GenerateClassifyFunction(dictionary, functionName));
			}
			else
			{
				codeBuilder.Append(base.GeneratePysparkImports(new string[] { "from pyspark.sql import functions" }));
			}
			switch (this._translationOptions.PythonTarget)
			{
			case PythonTarget.Auto:
				return codeBuilder.GetCode();
			case PythonTarget.Library:
				codeBuilder.AppendLine();
				codeBuilder.AppendLine();
				using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("def classify({0}, {1}):", new object[] { "inputs", "column" })), 1U))
				{
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("ret = dict()", Array.Empty<object>())));
					using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("for point in inputs:", Array.Empty<object>())), 1U))
					{
						codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("key = {0}(point[{1}])", new object[] { functionName, "column" })));
						using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("if key in ret:", Array.Empty<object>())), 1U))
						{
							codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("ret[key].append(point)", Array.Empty<object>())));
						}
						using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("else:", Array.Empty<object>())), 1U))
						{
							codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("ret[key] = [point]", Array.Empty<object>())));
						}
					}
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("return ret", Array.Empty<object>())));
				}
				return codeBuilder.GetCode();
			case PythonTarget.Pandas:
				codeBuilder.AppendLine();
				codeBuilder.AppendLine();
				using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("def classify({0}, {1}):", new object[] { "df", "column" })), 1U))
				{
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("return {0}.groupby({1}[{2}].transform({3}))", new object[] { "df", "df", "column", functionName })));
				}
				return codeBuilder.GetCode();
			case PythonTarget.PySpark:
				codeBuilder.AppendLine();
				codeBuilder.AppendLine();
				using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("def classify({0}, {1}):", new object[] { "df", "column" })), 1U))
				{
					codeBuilder.Append(this.GeneratePySparkClassifyFunction(dictionary, "df", "column", functionName));
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("return {0}.groupBy({1}.alias(\"{2}\"))", new object[] { "df", functionName, functionName })));
				}
				return codeBuilder.GetCode();
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06008C98 RID: 35992 RVA: 0x001D897C File Offset: 0x001D6B7C
		public string GenerateCheckingCode(OptimizeFor optimization, string functionName)
		{
			if (this._translationOptions.PythonTarget == PythonTarget.PySpark)
			{
				return this.GeneratePySparkCheckingCode(optimization);
			}
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.AppendLine("import regex");
			codeBuilder.AppendLine();
			using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("{0} = [", new object[] { "regexes" })), 1U))
			{
				foreach (string text in this.Regexes.Collect<string>())
				{
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("regex.compile({0}),", new object[] { text.ToPythonLiteral() })));
				}
			}
			codeBuilder.AppendLine("]");
			codeBuilder.AppendLine();
			codeBuilder.AppendLine();
			using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("def {0}({1}):", new object[] { functionName, "input_str" })), 1U))
			{
				codeBuilder.Append(this.GenerateAssertion("input_str", "regexes"));
			}
			switch (this._translationOptions.PythonTarget)
			{
			case PythonTarget.Auto:
				return codeBuilder.GetCode();
			case PythonTarget.Library:
				codeBuilder.AppendLine();
				codeBuilder.AppendLine();
				using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("def check({0}, {1}):", new object[] { "inputs", "column" })), 1U))
				{
					using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("for point in inputs:", Array.Empty<object>())), 1U))
					{
						codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0}(point[{1}])", new object[] { functionName, "column" })));
					}
				}
				return codeBuilder.GetCode();
			case PythonTarget.Pandas:
				codeBuilder.AppendLine();
				codeBuilder.AppendLine();
				using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("def check({0}, {1}):", new object[] { "df", "column" })), 1U))
				{
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0}[{1}].apply({2})", new object[] { "df", "column", functionName })));
				}
				return codeBuilder.GetCode();
			case PythonTarget.PySpark:
				throw new Exception(FormattableString.Invariant(FormattableStringFactory.Create("Unreachable code -- PySpark case has been handled earlier.", Array.Empty<object>())));
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06008C99 RID: 35993 RVA: 0x001D8C4C File Offset: 0x001D6E4C
		private string GeneratePySparkCheckingCode(OptimizeFor optimizeFor)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			IDictionary<string, string> dictionary;
			codeBuilder.Append(this.GenerateRegexDefinitions(this._regexProfiles, false, out dictionary));
			using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("def check({0}, {1}):", new object[] { "df", "column" })), 1U))
			{
				using (codeBuilder.NewScope("filtered_df = (", 1U))
				{
					codeBuilder.Append("df");
					foreach (MatchingLabel matchingLabel in this._labels)
					{
						switch (matchingLabel.Match)
						{
						case MatchingLabel.MatchType.NullMatch:
							codeBuilder.AppendLine(".filter(df[column].isNotNull())");
							break;
						case MatchingLabel.MatchType.NoMatch:
							break;
						case MatchingLabel.MatchType.TokenSequenceMatch:
						{
							RegexProfile regexProfile = this._regexProfiles[this._tokenSequences[matchingLabel]];
							string text = dictionary[regexProfile.Regex];
							codeBuilder.AppendLine(".filter(df[column].rlike(" + text + ") == False)");
							break;
						}
						default:
							throw new ArgumentOutOfRangeException();
						}
					}
				}
				codeBuilder.AppendLine(")");
				codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("rows = filtered_df.head(1)", Array.Empty<object>())));
				using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("assert len(rows) == 0, (", Array.Empty<object>())), 1U))
				{
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("\"{0}\" % rows[0][{1}]", new object[] { "The data point %s does not conform to the learned patterns.", "column" })));
				}
				codeBuilder.AppendLine(")");
			}
			return codeBuilder.GetCode();
		}

		// Token: 0x06008C9A RID: 35994 RVA: 0x001D8E64 File Offset: 0x001D7064
		public static PythonHeaderModule GenerateHeaderModule(string headerModuleName)
		{
			Assembly assembly = typeof(MatchingTextPythonModule).GetTypeInfo().Assembly;
			string name = assembly.GetName().Name;
			string text = FormattableString.Invariant(FormattableStringFactory.Create("{0}.Translation.Python.matching_text.py", new object[] { name }));
			return new PythonHeaderModule(headerModuleName, new string[] { AssemblyResourceUtils.LoadResourceFromAssembly(assembly, text) });
		}

		// Token: 0x04003982 RID: 14722
		private readonly Program _program;

		// Token: 0x04003983 RID: 14723
		private readonly IReadOnlyDictionary<MatchingLabel, IReadOnlyList<IToken>> _tokenSequences;

		// Token: 0x04003984 RID: 14724
		private readonly IReadOnlyDictionary<IReadOnlyList<IToken>, RegexProfile> _regexProfiles;

		// Token: 0x04003985 RID: 14725
		private readonly IReadOnlyList<MatchingLabel> _labels;

		// Token: 0x04003986 RID: 14726
		private readonly IReadOnlyDictionary<MatchingLabel, IReadOnlyList<string>> _examples;

		// Token: 0x04003987 RID: 14727
		private readonly IReadOnlyList<string> _outliers;

		// Token: 0x04003988 RID: 14728
		private readonly IReadOnlyDictionary<MatchingLabel, uint> _sizes;

		// Token: 0x04003989 RID: 14729
		private IReadOnlyList<string> _regexes;

		// Token: 0x0400398A RID: 14730
		private const string AssertionMessage = "The data point %s does not conform to the learned patterns.";

		// Token: 0x0400398B RID: 14731
		public const string DefaultClusteringFunctionName = "identify_pattern";

		// Token: 0x0400398C RID: 14732
		private readonly MatchingTextPythonModule.TranslationOptions _translationOptions;

		// Token: 0x0200123C RID: 4668
		public class TranslationOptions
		{
			// Token: 0x06008C9D RID: 35997 RVA: 0x001D8F08 File Offset: 0x001D7108
			public TranslationOptions(PythonTarget target)
			{
				this.PythonTarget = target;
			}

			// Token: 0x0400398D RID: 14733
			internal readonly PythonTarget PythonTarget;
		}
	}
}
