using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion.Logging;
using Microsoft.ProgramSynthesis.Transformation.Text.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion
{
	// Token: 0x02001DFE RID: 7678
	public class AutoCompleter : IAutoCompleter
	{
		// Token: 0x17002AA3 RID: 10915
		// (get) Token: 0x0601011D RID: 65821 RVA: 0x00373195 File Offset: 0x00371395
		public static TimeSpan DefaultSuggestionTimeLimit { get; } = TimeSpan.FromSeconds(3.0);

		// Token: 0x0601011E RID: 65822 RVA: 0x0037319C File Offset: 0x0037139C
		private AutoCompleter(IRow inputRow, bool includeNonExtensionCompletions, Func<IAutoCompleteSearchTree> searchTreeFactory, IRanker ranker = null, ILogger logger = null, Session session = null)
		{
			this.Ranker = ranker;
			this.Logger = logger;
			this.Session = session;
			this.InputRow = inputRow;
			List<string> list = new List<string>();
			foreach (string text in this.InputRow.ColumnNames)
			{
				object obj;
				if (this.InputRow.TryGetValue(text, out obj))
				{
					list.Add((obj != null) ? obj.ToString() : null);
				}
			}
			this._rowData = list;
			AutoCompleter.Stopwatch.Restart();
			List<EntityToken> list2 = EntityExtractor.Extract(this._rowData.Where((string s) => !string.IsNullOrEmpty(s)), true, Array.Empty<EntityBasedTokenizer>()).ToList<EntityToken>();
			AutoCompleter.Stopwatch.Stop();
			double num = AutoCompleter.Stopwatch.ElapsedMillisecondsAsDouble();
			AutoCompleter.Stopwatch.Restart();
			this.SearchTree = searchTreeFactory();
			foreach (EntityToken entityToken in list2)
			{
				entityToken.MakeSearchTreeEntries(this.SearchTree, includeNonExtensionCompletions);
			}
			AutoCompleter.Stopwatch.Stop();
			double num2 = AutoCompleter.Stopwatch.ElapsedMillisecondsAsDouble();
			this._lastCreateEvent = new AutoCompleterCreateEvent(this._rowData, list2, num, num2, num + num2);
			this._lastCreateEvent.Log(this.Logger);
			this.SuggestionTimeLimit = AutoCompleter.DefaultSuggestionTimeLimit;
		}

		// Token: 0x17002AA4 RID: 10916
		// (get) Token: 0x0601011F RID: 65823 RVA: 0x00373340 File Offset: 0x00371540
		public ILogger Logger { get; }

		// Token: 0x17002AA5 RID: 10917
		// (get) Token: 0x06010120 RID: 65824 RVA: 0x00373348 File Offset: 0x00371548
		public IAutoCompleteSearchTree SearchTree { get; }

		// Token: 0x17002AA6 RID: 10918
		// (get) Token: 0x06010121 RID: 65825 RVA: 0x00373350 File Offset: 0x00371550
		public static Type DefaultSearchTreeProvider { get; } = typeof(CompressedTrieSearchTree);

		// Token: 0x17002AA7 RID: 10919
		// (get) Token: 0x06010122 RID: 65826 RVA: 0x00373357 File Offset: 0x00371557
		public IRanker Ranker { get; }

		// Token: 0x17002AA8 RID: 10920
		// (get) Token: 0x06010123 RID: 65827 RVA: 0x0037335F File Offset: 0x0037155F
		public IRow InputRow { get; }

		// Token: 0x17002AA9 RID: 10921
		// (get) Token: 0x06010124 RID: 65828 RVA: 0x00373367 File Offset: 0x00371567
		public Session Session { get; }

		// Token: 0x17002AAA RID: 10922
		// (get) Token: 0x06010125 RID: 65829 RVA: 0x0037336F File Offset: 0x0037156F
		// (set) Token: 0x06010126 RID: 65830 RVA: 0x00373377 File Offset: 0x00371577
		public TimeSpan SuggestionTimeLimit { get; set; }

		// Token: 0x06010127 RID: 65831 RVA: 0x00373380 File Offset: 0x00371580
		public static Task<AutoCompleter> CreateAsync(IRow inputRow, ILogger logger = null, bool includeNonExtensionCompletions = false, IRanker ranker = null, Func<IAutoCompleteSearchTree> searchTreeFactory = null, Session session = null)
		{
			return Task.Run<AutoCompleter>(delegate
			{
				IRow inputRow2 = inputRow;
				bool includeNonExtensionCompletions2 = includeNonExtensionCompletions;
				Func<IAutoCompleteSearchTree> func;
				if ((func = searchTreeFactory) == null && (func = AutoCompleter.<>c.<>9__32_1) == null)
				{
					func = (AutoCompleter.<>c.<>9__32_1 = () => new CompressedTrieSearchTree(true));
				}
				return new AutoCompleter(inputRow2, includeNonExtensionCompletions2, func, ranker ?? new EntropyBasedRanker(), logger, session);
			});
		}

		// Token: 0x06010128 RID: 65832 RVA: 0x003733CE File Offset: 0x003715CE
		public Task<IReadOnlyList<ISuggestion>> SuggestAsync(string prefix, int minimumSuggestionCount = 3, int maximumSuggestionCount = 2147483647)
		{
			return Task.Run<IReadOnlyList<ISuggestion>>(delegate
			{
				AutoCompleter.Stopwatch.Restart();
				Guid guid = Guid.NewGuid();
				List<ISuggestion> list = null;
				Session session = this.Session;
				if (session != null && session.Constraints.OfType<Example>().Any<Example>())
				{
					list = this.SuggestBasedOnExamplesAndPrefix(prefix, guid, this.Session.Constraints.OfType<Example>());
				}
				list = ((list == null || list.IsEmpty<ISuggestion>()) ? this.SuggestBasedOnInputs(prefix, minimumSuggestionCount, guid) : list);
				AutoCompleter.Stopwatch.Stop();
				double num = AutoCompleter.Stopwatch.ElapsedMillisecondsAsDouble();
				this._lastSuggestEvent = this._lastCreateEvent.ChainSuggestEvent(prefix, guid.ToString(), list, num);
				this._lastSuggestEvent.Log(this.Logger);
				return list;
			});
		}

		// Token: 0x06010129 RID: 65833 RVA: 0x003733FC File Offset: 0x003715FC
		private List<ISuggestion> SuggestBasedOnExamplesAndPrefix(string prefix, Guid suggestEventId, IEnumerable<Example> examples)
		{
			List<Example> list = examples.ToList<Example>();
			PrefixOfOutputConstraint prefixOfOutputConstraint = new PrefixOfOutputConstraint(this.InputRow, new StringPrefixSet(prefix), false);
			List<ISuggestion> list2;
			using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
			{
				cancellationTokenSource.CancelAfter(this.SuggestionTimeLimit);
				Program program = Learner.Instance.Learn(list.Cast<Constraint<IRow, object>>().AppendItem(prefixOfOutputConstraint), null, cancellationTokenSource.Token);
				if (program == null)
				{
					list2 = new List<ISuggestion>();
				}
				else
				{
					object obj = program.Run(this.InputRow);
					if (obj == null)
					{
						list2 = new List<ISuggestion>();
					}
					else
					{
						list2 = new List<ISuggestion>
						{
							new Suggestion(this, prefix, 0U, obj.ToString(), null, suggestEventId.ToString())
						};
					}
				}
			}
			return list2;
		}

		// Token: 0x0601012A RID: 65834 RVA: 0x003734C8 File Offset: 0x003716C8
		private List<ISuggestion> SuggestBasedOnInputs(string prefix, int minimumSuggestionCount, Guid suggestEventId)
		{
			HashSet<string> hashSet = new HashSet<string>();
			List<CompletionResultWithIndex> list = new List<CompletionResultWithIndex>();
			for (int i = 0; i < prefix.Length; i++)
			{
				int num = prefix.Length - i;
				int index = i;
				string text = prefix.Substring(i, num);
				List<CompletionResultWithIndex> list2 = this.SearchTree.PrefixLookup(text).SelectMany((KeyValuePair<string, List<CompletionInfo>> r) => r.Value.Select((CompletionInfo c) => new CompletionResultWithIndex(r.Key, c, index, prefix))).ToList<CompletionResultWithIndex>();
				list.AddRange(list2);
				hashSet.AddRange(list2.Select((CompletionResultWithIndex c) => c.Value.Value));
				if (hashSet.Count >= minimumSuggestionCount)
				{
					break;
				}
			}
			return (from c in this.Ranker.Rank(list)
				select new Suggestion(this, prefix, (uint)c.Index, c.Value.Value, c.Value.Metadata, suggestEventId.ToString()) into s
				where s.CompleteValue != prefix
				select s).Distinct<Suggestion>().Cast<ISuggestion>().ToList<ISuggestion>();
		}

		// Token: 0x0601012B RID: 65835 RVA: 0x003735FA File Offset: 0x003717FA
		public void ConfirmSuggestion(string suggestionEventId, int suggestionIndex)
		{
			this._lastConfirmEvent = new AutoCompleterConfirmEvent(this._lastCreateEvent, suggestionEventId, suggestionIndex);
			this._lastConfirmEvent.Log(this.Logger);
		}

		// Token: 0x040060B5 RID: 24757
		private static readonly Stopwatch Stopwatch = new Stopwatch();

		// Token: 0x040060B6 RID: 24758
		private readonly AutoCompleterCreateEvent _lastCreateEvent;

		// Token: 0x040060B7 RID: 24759
		private AutoCompleterSuggestEvent _lastSuggestEvent;

		// Token: 0x040060B8 RID: 24760
		private AutoCompleterConfirmEvent _lastConfirmEvent;

		// Token: 0x040060B9 RID: 24761
		private IReadOnlyList<string> _rowData;

		// Token: 0x040060BC RID: 24764
		private const int DefaultMinimumSuggestionCount = 3;
	}
}
