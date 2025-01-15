using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Read.FlatFile.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Logging;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;
using Microsoft.ProgramSynthesis.Wrangling.Session;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Read.FlatFile
{
	// Token: 0x02001256 RID: 4694
	public class Session : NonInteractiveSession<Program, string, ITable<string>>, ITextReaderInput
	{
		// Token: 0x06008D2D RID: 36141 RVA: 0x001DA944 File Offset: 0x001D8B44
		public Session(IJournalStorage journalStorage = null, CultureInfo culture = null, ILogger logger = null)
			: base(Learner.Instance, Loader.Instance, journalStorage, culture, "Read.FlatFile", logger, true)
		{
		}

		// Token: 0x17001835 RID: 6197
		// (get) Token: 0x06008D2E RID: 36142 RVA: 0x001DA95F File Offset: 0x001D8B5F
		public new static JsonSerializerSettings JsonSerializerSettings
		{
			get
			{
				return Session.LazyJsonSerializerSettings.Value;
			}
		}

		// Token: 0x17001836 RID: 6198
		// (get) Token: 0x06008D2F RID: 36143 RVA: 0x001DA96B File Offset: 0x001D8B6B
		protected override JsonSerializerSettings JsonSerializerSettingsInstance
		{
			get
			{
				return Session.JsonSerializerSettings;
			}
		}

		// Token: 0x06008D30 RID: 36144 RVA: 0x001DA974 File Offset: 0x001D8B74
		public void AddInput(string input, int linesToLearn = 200)
		{
			IReadOnlyList<string> readOnlyList = Semantics.SplitLines(input, false).Take(linesToLearn).ToList<string>();
			base.Inputs.Add(string.Join(string.Empty, readOnlyList));
		}

		// Token: 0x06008D31 RID: 36145 RVA: 0x001DA9AC File Offset: 0x001D8BAC
		public void AddInput(TextReader input, int linesToLearn = 200)
		{
			IReadOnlyList<string> readOnlyList = Semantics.SplitLines(input, false).Take(linesToLearn).ToList<string>();
			base.Inputs.Add(string.Join(string.Empty, readOnlyList));
		}

		// Token: 0x06008D32 RID: 36146 RVA: 0x001DA9E4 File Offset: 0x001D8BE4
		protected override IEnumerable<KeyValuePair<string, string>> TrackedLearningProperties(LearnProgramRequest<Program, string, ITable<string>> request, Program program)
		{
			IEnumerable<KeyValuePair<string, string>> enumerable = base.TrackedLearningProperties(request, program);
			if (program == null)
			{
				return enumerable;
			}
			IEnumerable<KeyValuePair<string, string>> enumerable2 = enumerable;
			KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[2];
			array[0] = KVP.Create<string, string>("ReadFlatFileProgramType", program.Switch<string>((CsvProgram _) => "CSV", (FwProgram _) => "FW", (ExtractionTextProgram _) => "EText"));
			array[1] = KVP.Create<string, string>("ReadFlatFileColumnCount", program.ColumnNames.Count.ToString());
			enumerable = enumerable2.Concat(array);
			SimpleProgram simpleProgram = program as SimpleProgram;
			if (simpleProgram != null)
			{
				enumerable = enumerable.Concat(new KeyValuePair<string, string>[]
				{
					KVP.Create<string, string>("ReadFlatFileSkipCount", simpleProgram.Skip.ToString()),
					KVP.Create<string, string>("ReadFlatFileSkipFooterCount", simpleProgram.SkipFooter.ToString()),
					KVP.Create<string, string>("ReadFlatFileFilterEmptyLines", simpleProgram.FilterEmptyLines.ToString()),
					KVP.Create<string, string>("ReadFlatFileHasMultiLineRows", simpleProgram.HasMultiLineRows ? "True" : "False"),
					KVP.Create<string, string>("ReadFlatFileHasEmptyLines", simpleProgram.HasEmptyLines ? "True" : "False")
				});
				if (simpleProgram.CommentStr.HasValue)
				{
					enumerable = enumerable.AppendItem(KVP.Create<string, string>("ReadFlatFileCommentStr", Session.<TrackedLearningProperties>g__TelemetryString|11_4(simpleProgram.CommentStr.Value)));
				}
				if (simpleProgram.NewLineStrings != null)
				{
					IEnumerable<KeyValuePair<string, string>> enumerable3 = enumerable;
					string text = "ReadFlatFileNewLineStrings";
					string text2 = ",";
					IEnumerable<string> newLineStrings = simpleProgram.NewLineStrings;
					Func<string, string> func;
					if ((func = Session.<>O.<0>__EscapeNewLineString) == null)
					{
						func = (Session.<>O.<0>__EscapeNewLineString = new Func<string, string>(Session.<TrackedLearningProperties>g__EscapeNewLineString|11_6));
					}
					enumerable = enumerable3.AppendItem(KVP.Create<string, string>(text, string.Join(text2, newLineStrings.Select(func))));
				}
			}
			CsvProgram csvProgram = program as CsvProgram;
			if (csvProgram != null)
			{
				if (csvProgram.Delimiter != null)
				{
					enumerable = enumerable.AppendItem(KVP.Create<string, string>("ReadFlatFileDelimiter", Session.<TrackedLearningProperties>g__TelemetryString|11_4(csvProgram.Delimiter)));
				}
				if (csvProgram.QuoteChar.HasValue)
				{
					enumerable = enumerable.AppendItem(KVP.Create<string, string>("ReadFlatFileQuoteChar", Session.<TrackedLearningProperties>g__TelemetryChar|11_5(csvProgram.QuoteChar.Value)));
				}
				if (csvProgram.EscapeChar.HasValue)
				{
					enumerable = enumerable.AppendItem(KVP.Create<string, string>("ReadFlatFileEscapeChar", Session.<TrackedLearningProperties>g__TelemetryChar|11_5(csvProgram.EscapeChar.Value)));
				}
				string text3 = (csvProgram.DoubleQuote ? "True" : "False");
				enumerable = enumerable.AppendItem(KVP.Create<string, string>("ReadFlatFileDoubleQuote", text3));
			}
			return enumerable;
		}

		// Token: 0x06008D33 RID: 36147 RVA: 0x001DACAC File Offset: 0x001D8EAC
		protected override IEnumerable<KeyValuePair<string, string>> TrackedLearningUserProperties(LearnProgramRequest<Program, string, ITable<string>> request, Program topProgram, bool includeConstraints = true)
		{
			string text;
			if (request == null)
			{
				text = null;
			}
			else
			{
				IImmutableList<string> inputs = request.Inputs;
				text = ((inputs != null) ? inputs.FirstOrDefault<string>() : null);
			}
			string text2 = text;
			string text3 = null;
			if (text2 != null)
			{
				int num = 0;
				int i;
				for (i = 0; i < Math.Min(20000, text2.Length); i++)
				{
					char c = text2[i];
					if (c == '\r' || c == '\n')
					{
						if (c == '\r' && i + 1 < text2.Length && text2[i + 1] == '\n')
						{
							i++;
						}
						num++;
						if (num >= 20)
						{
							break;
						}
					}
				}
				text3 = text2.Substring(0, i);
			}
			return base.TrackedLearningUserProperties(request, topProgram, includeConstraints).AppendItem(KVP.Create<string, string>("InputSample", text3));
		}

		// Token: 0x06008D35 RID: 36149 RVA: 0x00094B3F File Offset: 0x00092D3F
		[CompilerGenerated]
		internal static bool <TrackedLearningProperties>g__NonDataChar|11_3(char c)
		{
			return char.IsPunctuation(c) || char.IsSymbol(c) || char.IsWhiteSpace(c);
		}

		// Token: 0x06008D36 RID: 36150 RVA: 0x001DAD72 File Offset: 0x001D8F72
		[CompilerGenerated]
		internal static string <TrackedLearningProperties>g__TelemetryString|11_4(string s)
		{
			Func<char, bool> func;
			if ((func = Session.<>O.<1>__NonDataChar) == null)
			{
				func = (Session.<>O.<1>__NonDataChar = new Func<char, bool>(Session.<TrackedLearningProperties>g__NonDataChar|11_3));
			}
			if (!s.All(func))
			{
				return "<omitted-may-contain-PII>";
			}
			return s;
		}

		// Token: 0x06008D37 RID: 36151 RVA: 0x001DAD9E File Offset: 0x001D8F9E
		[CompilerGenerated]
		internal static string <TrackedLearningProperties>g__TelemetryChar|11_5(char c)
		{
			if (!Session.<TrackedLearningProperties>g__NonDataChar|11_3(c))
			{
				return "<omitted-may-contain-PII>";
			}
			return c.ToString();
		}

		// Token: 0x06008D38 RID: 36152 RVA: 0x001DADB8 File Offset: 0x001D8FB8
		[CompilerGenerated]
		internal static string <TrackedLearningProperties>g__EscapeNewLineString|11_6(string s)
		{
			string text;
			if (!(s == "\r\n"))
			{
				if (!(s == "\r"))
				{
					if (!(s == "\n"))
					{
						text = string.Empty;
					}
					else
					{
						text = "\\n";
					}
				}
				else
				{
					text = "\\r";
				}
			}
			else
			{
				text = "\\r\\n";
			}
			return text;
		}

		// Token: 0x040039D1 RID: 14801
		private const int LoggingInputLines = 20;

		// Token: 0x040039D2 RID: 14802
		private const int LoggingInputSize = 20000;

		// Token: 0x040039D3 RID: 14803
		private static readonly Lazy<JsonSerializerSettings> LazyJsonSerializerSettings = new Lazy<JsonSerializerSettings>(() => new SessionJsonSerializerSettings().Initialize());

		// Token: 0x040039D4 RID: 14804
		public const int DefaultLinesToLearn = 200;

		// Token: 0x02001257 RID: 4695
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040039D5 RID: 14805
			public static Func<string, string> <0>__EscapeNewLineString;

			// Token: 0x040039D6 RID: 14806
			public static Func<char, bool> <1>__NonDataChar;
		}
	}
}
