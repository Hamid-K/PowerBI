using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.ProgramSynthesis.Compound.Split.Constraints;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Logging;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;
using Microsoft.ProgramSynthesis.Wrangling.Session;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Compound.Split
{
	// Token: 0x02000915 RID: 2325
	public class Session : NonInteractiveSession<Program, StringRegion, ITable<StringRegion>>
	{
		// Token: 0x06003227 RID: 12839 RVA: 0x00094639 File Offset: 0x00092839
		public Session(IJournalStorage journalStorage = null, CultureInfo culture = null, ILogger logger = null)
			: base(Learner.Instance, Loader.Instance, journalStorage, culture, "Compound.Split", logger, true)
		{
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x00094654 File Offset: 0x00092854
		public new static JsonSerializerSettings JsonSerializerSettings
		{
			get
			{
				return Session.LazyJsonSerializerSettings.Value;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x00094660 File Offset: 0x00092860
		protected override JsonSerializerSettings JsonSerializerSettingsInstance
		{
			get
			{
				return Session.JsonSerializerSettings;
			}
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x00094668 File Offset: 0x00092868
		public void AddInput(TextReader input, int linesToRead = 200)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < linesToRead)
			{
				int num = input.Read();
				if (num == -1)
				{
					break;
				}
				stringBuilder.Append((char)num);
				if (num == 13)
				{
					i++;
					if (input.Peek() == 10)
					{
						num = input.Read();
						stringBuilder.Append((char)num);
					}
				}
				else if (num == 10)
				{
					i++;
				}
			}
			base.Constraints.Add(new ReadInputLineCount(linesToRead));
			base.Inputs.Add(Session.CreateStringRegion(stringBuilder.ToString()));
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000946EC File Offset: 0x000928EC
		public void AddInput(string input, int linesToRead = 200)
		{
			using (StringReader stringReader = new StringReader(input))
			{
				this.AddInput(stringReader, linesToRead);
			}
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x00094724 File Offset: 0x00092924
		public static StringRegion CreateStringRegion(string s)
		{
			return new StringRegion(s, Learner.Tokens);
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x00094734 File Offset: 0x00092934
		protected override IEnumerable<KeyValuePair<string, string>> TrackedLearningProperties(LearnProgramRequest<Program, StringRegion, ITable<StringRegion>> request, Program learnedProgram)
		{
			IEnumerable<KeyValuePair<string, string>> enumerable = base.TrackedLearningProperties(request, learnedProgram);
			if (learnedProgram != null && learnedProgram.Properties != null)
			{
				enumerable = enumerable.Concat(new KeyValuePair<string, string>[]
				{
					KVP.Create<string, string>("CompoundSplitFilterEmptyLines", learnedProgram.Properties.FilterEmptyLines.ToString()),
					KVP.Create<string, string>("CompoundSplitHasCommentHeader", learnedProgram.Properties.HasCommentHeader.ToString()),
					KVP.Create<string, string>("CompoundSplitHasNewLineInQuotes", learnedProgram.Properties.HasNewLineInQuotes.ToString()),
					KVP.Create<string, string>("CompoundSplitIsDelimitedProgram", (learnedProgram.Properties.ColumnDelimiter != null) ? "True" : "False"),
					KVP.Create<string, string>("CompoundSplitIsFixedWidthProgram", (learnedProgram.Properties.FieldPositions != null) ? "True" : "False"),
					KVP.Create<string, string>("CompoundSplitSkipLinesCount", learnedProgram.Properties.SkipLinesCount.ToString()),
					KVP.Create<string, string>("CompoundSplitQuotingStyle", learnedProgram.Properties.QuotingConf.Style.ToString()),
					KVP.Create<string, string>("SplitTextColumnCount", learnedProgram.Properties.ColumnCount.ToString()),
					KVP.Create<string, string>("CompoundSplitHasDataRegex", (learnedProgram.Properties.DataRegex != null) ? "True" : "False"),
					KVP.Create<string, string>("CompoundSplitHasHeaderRegex", (learnedProgram.Properties.HeaderRegex != null) ? "True" : "False")
				});
				if (learnedProgram.Properties.ColumnDelimiter != null)
				{
					string columnDelimiter = learnedProgram.Properties.ColumnDelimiter;
					string text = (columnDelimiter.All(new Func<char, bool>(Session.<TrackedLearningProperties>g__NonDataCharacter|11_0)) ? columnDelimiter : "<omitted-may-contain-PII>");
					enumerable = enumerable.AppendItem(KVP.Create<string, string>("CompoundSplitColumnDelimiter", text));
				}
				if (learnedProgram.Properties.CommentStr != null)
				{
					string commentStr = learnedProgram.Properties.CommentStr;
					string text2 = (commentStr.All(new Func<char, bool>(Session.<TrackedLearningProperties>g__NonDataCharacter|11_0)) ? commentStr : "<omitted-may-contain-PII>");
					enumerable = enumerable.AppendItem(KVP.Create<string, string>("CompoundSplitCommentStr", text2));
				}
				if (learnedProgram.Properties.EscapeCharacter != null)
				{
					char value = learnedProgram.Properties.EscapeCharacter.Value;
					string text3 = (Session.<TrackedLearningProperties>g__NonDataCharacter|11_0(value) ? value.ToString() : "<omitted-may-contain-PII>");
					enumerable = enumerable.AppendItem(KVP.Create<string, string>("CompoundSplitEscapeCharacter", text3));
				}
				if (learnedProgram.Properties.HeaderIndex.HasValue)
				{
					enumerable = enumerable.AppendItem(KVP.Create<string, string>("CompoundSplitHeaderIndex", learnedProgram.Properties.HeaderIndex.Value.ToString()));
				}
				if (learnedProgram.Properties.QuoteCharacter != null)
				{
					char value2 = learnedProgram.Properties.QuoteCharacter.Value;
					string text4 = (Session.<TrackedLearningProperties>g__NonDataCharacter|11_0(value2) ? value2.ToString() : "<omitted-may-contain-PII>");
					enumerable = enumerable.AppendItem(KVP.Create<string, string>("CompoundSplitQuoteCharacter", text4));
				}
			}
			return enumerable;
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x00094A90 File Offset: 0x00092C90
		protected override IEnumerable<KeyValuePair<string, string>> TrackedLearningUserProperties(LearnProgramRequest<Program, StringRegion, ITable<StringRegion>> request, Program topProgram, bool includeConstraints = true)
		{
			StringRegion stringRegion;
			if (request == null)
			{
				stringRegion = null;
			}
			else
			{
				IImmutableList<StringRegion> inputs = request.Inputs;
				stringRegion = ((inputs != null) ? inputs.FirstOrDefault<StringRegion>() : null);
			}
			StringRegion stringRegion2 = stringRegion;
			string text = null;
			if (stringRegion2 != null)
			{
				int num = 0;
				int i;
				for (i = 0; i < stringRegion2.Value.Length; i++)
				{
					if (stringRegion2.Value[i] == '\n')
					{
						num++;
						if (num == 20)
						{
							break;
						}
					}
				}
				text = stringRegion2.Value.Substring(0, Math.Min(i, 20000));
			}
			return base.TrackedLearningUserProperties(request, topProgram, includeConstraints).AppendItem(KVP.Create<string, string>("InputSample", text));
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x00094B3F File Offset: 0x00092D3F
		[CompilerGenerated]
		internal static bool <TrackedLearningProperties>g__NonDataCharacter|11_0(char c)
		{
			return char.IsPunctuation(c) || char.IsSymbol(c) || char.IsWhiteSpace(c);
		}

		// Token: 0x04001905 RID: 6405
		private const int LoggingInputLines = 20;

		// Token: 0x04001906 RID: 6406
		private const int LoggingInputSize = 20000;

		// Token: 0x04001907 RID: 6407
		private static readonly Lazy<JsonSerializerSettings> LazyJsonSerializerSettings = new Lazy<JsonSerializerSettings>(() => new SessionJsonSerializerSettings().Initialize());
	}
}
