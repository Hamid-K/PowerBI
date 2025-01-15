using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Logging;
using Microsoft.ProgramSynthesis.Wrangling.Session;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x02001310 RID: 4880
	public class SplitSession : NonInteractiveSession<SplitProgram, StringRegion, SplitCell[]>
	{
		// Token: 0x060092D7 RID: 37591 RVA: 0x001EE35C File Offset: 0x001EC55C
		public SplitSession(IJournalStorage journalStorage = null, CultureInfo culture = null, ILogger logger = null)
			: base(SplitProgramLearner.Instance, SplitProgramLoader.Instance, journalStorage, culture, "Split.Text", logger, true)
		{
		}

		// Token: 0x17001932 RID: 6450
		// (get) Token: 0x060092D8 RID: 37592 RVA: 0x001EE377 File Offset: 0x001EC577
		public new static JsonSerializerSettings JsonSerializerSettings
		{
			get
			{
				return SplitSession.LazyJsonSerializerSettings.Value;
			}
		}

		// Token: 0x17001933 RID: 6451
		// (get) Token: 0x060092D9 RID: 37593 RVA: 0x001EE383 File Offset: 0x001EC583
		protected override JsonSerializerSettings JsonSerializerSettingsInstance
		{
			get
			{
				return SplitSession.JsonSerializerSettings;
			}
		}

		// Token: 0x060092DA RID: 37594 RVA: 0x001EE117 File Offset: 0x001EC317
		protected SplitProgram BuildProgram(regionSplit programNode, object extraInfo)
		{
			return new SplitProgram(programNode);
		}

		// Token: 0x060092DB RID: 37595 RVA: 0x001EE38A File Offset: 0x001EC58A
		public static StringRegion CreateStringRegion(string s)
		{
			return new StringRegion(s, Semantics.Tokens);
		}

		// Token: 0x060092DC RID: 37596 RVA: 0x001EE398 File Offset: 0x001EC598
		public IEnumerable<SplitCell[]> LearnOutputs()
		{
			SplitProgram program = base.Learn(null, default(CancellationToken), null);
			if (program == null)
			{
				return null;
			}
			return base.Inputs.Select((StringRegion i) => program.Run(i)).ToArray<SplitCell[]>();
		}

		// Token: 0x060092DD RID: 37597 RVA: 0x001EE3F8 File Offset: 0x001EC5F8
		public override async Task<IReadOnlyList<SignificantInput<StringRegion>>> GetSignificantInputsAsync(double? confidenceThreshold = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			return await Task.Run<List<SignificantInput<StringRegion>>>(() => this.GetDistinctFormats(cancel), cancel);
		}

		// Token: 0x060092DE RID: 37598 RVA: 0x001EE444 File Offset: 0x001EC644
		private Record<string, string> GetInputOutputPattern(StringRegion input, SplitProgram learntProgram)
		{
			string specialCharFormat = SplitSession.GetSpecialCharFormat(input.Value);
			SplitCell[] array = ((learntProgram != null) ? learntProgram.Run(SplitSession.CreateStringRegion(input.Value)) : null);
			string text;
			if (array != null)
			{
				text = new string(array.Select(delegate(SplitCell c)
				{
					StringRegion cellValue = c.CellValue;
					if (!string.IsNullOrWhiteSpace((cellValue != null) ? cellValue.Value : null))
					{
						return '1';
					}
					return '0';
				}).ToArray<char>());
			}
			else
			{
				text = null;
			}
			string text2 = text;
			return Record.Create<string, string>(specialCharFormat, text2);
		}

		// Token: 0x060092DF RID: 37599 RVA: 0x001EE4B0 File Offset: 0x001EC6B0
		private List<SignificantInput<StringRegion>> GetDistinctFormats(CancellationToken cancel)
		{
			SplitProgram learntProgram = base.Learn(null, cancel, null);
			MultiValueDictionary<Record<string, string>, StringRegion> multiValueDictionary = new MultiValueDictionary<Record<string, string>, StringRegion>();
			HashSet<Record<string, string>> hashSet = (from c in base.Constraints.OfType<NthExampleConstraint>()
				select this.GetInputOutputPattern(SplitSession.CreateStringRegion(c.InputString), learntProgram)).ConvertToHashSet<Record<string, string>>();
			foreach (StringRegion stringRegion in base.Inputs)
			{
				cancel.ThrowIfCancellationRequested();
				Record<string, string> inputOutputPattern = this.GetInputOutputPattern(stringRegion, learntProgram);
				if (!hashSet.Contains(inputOutputPattern))
				{
					multiValueDictionary.Add(inputOutputPattern, stringRegion);
				}
			}
			return multiValueDictionary.Values.Select(delegate(IReadOnlyCollection<StringRegion> r)
			{
				IReadOnlyList<StringRegion> readOnlyList = r.ToList<StringRegion>();
				return new SignificantInput<StringRegion>(1.0, readOnlyList.First<StringRegion>(), readOnlyList.Some<IReadOnlyList<StringRegion>>());
			}).ToList<SignificantInput<StringRegion>>();
		}

		// Token: 0x060092E0 RID: 37600 RVA: 0x001EE5A4 File Offset: 0x001EC7A4
		private static string GetSpecialCharFormat(string s)
		{
			return new string(s.Where((char c) => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)).ToArray<char>());
		}

		// Token: 0x060092E1 RID: 37601 RVA: 0x001EE5D8 File Offset: 0x001EC7D8
		protected override IEnumerable<KeyValuePair<string, double>> TrackedLearningMetrics(LearnProgramRequest<SplitProgram, StringRegion, SplitCell[]> request, SplitProgram topProgram)
		{
			IEnumerable<KeyValuePair<string, double>> enumerable = base.TrackedLearningMetrics(request, topProgram);
			KeyValuePair<string, double>[] array = new KeyValuePair<string, double>[1];
			int num = 0;
			string text = "NumExamples";
			int? num2 = ((request != null) ? new int?(request.Constraints.OfType<NthExampleConstraint>().Count<NthExampleConstraint>()) : null);
			array[num] = KVP.Create<string, double>(text, ((num2 != null) ? new double?((double)num2.GetValueOrDefault()) : null).GetValueOrDefault());
			return enumerable.Concat(array);
		}

		// Token: 0x060092E2 RID: 37602 RVA: 0x001EE658 File Offset: 0x001EC858
		protected override IEnumerable<KeyValuePair<string, string>> TrackedLearningProperties(LearnProgramRequest<SplitProgram, StringRegion, SplitCell[]> request, SplitProgram learnedProgram)
		{
			IEnumerable<KeyValuePair<string, string>> enumerable = base.TrackedLearningProperties(request, learnedProgram);
			if (learnedProgram != null && learnedProgram.Properties != null)
			{
				enumerable = enumerable.Concat(new KeyValuePair<string, string>[]
				{
					KVP.Create<string, string>("SplitTextColumnCount", learnedProgram.Properties.ColumnCount.ToString()),
					KVP.Create<string, string>("SplitTextSplitCount", learnedProgram.Properties.SplitCount.ToString())
				});
			}
			return enumerable;
		}

		// Token: 0x060092E3 RID: 37603 RVA: 0x001EE6D8 File Offset: 0x001EC8D8
		protected override IEnumerable<KeyValuePair<string, string>> TrackedLearningUserProperties(LearnProgramRequest<SplitProgram, StringRegion, SplitCell[]> request, SplitProgram topProgram, bool includeConstraints = true)
		{
			int num = 20;
			string text = ((((request != null) ? request.Inputs : null) == null) ? null : JsonConvert.SerializeObject(request.Inputs.TakeEvery(Math.Max(1, request.Inputs.Count / num)), this.JsonSerializerSettingsInstance));
			return base.TrackedLearningUserProperties(request, topProgram, includeConstraints).AppendItem(KVP.Create<string, string>("InputSample", text));
		}

		// Token: 0x04003C4A RID: 15434
		private static readonly Lazy<JsonSerializerSettings> LazyJsonSerializerSettings = new Lazy<JsonSerializerSettings>(() => new SplitSessionJsonSerializerSettings().Initialize());
	}
}
