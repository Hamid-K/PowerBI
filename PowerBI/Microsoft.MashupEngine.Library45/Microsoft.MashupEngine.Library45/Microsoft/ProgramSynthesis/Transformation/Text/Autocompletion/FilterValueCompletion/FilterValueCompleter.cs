using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion.Logging;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion.FilterValueCompletion
{
	// Token: 0x02001E23 RID: 7715
	public class FilterValueCompleter
	{
		// Token: 0x17002ACD RID: 10957
		// (get) Token: 0x060101D5 RID: 66005 RVA: 0x003750B8 File Offset: 0x003732B8
		public ColumnProfile Profile { get; }

		// Token: 0x17002ACE RID: 10958
		// (get) Token: 0x060101D6 RID: 66006 RVA: 0x003750C0 File Offset: 0x003732C0
		public string ColumnName
		{
			get
			{
				return this.Profile.ColumnName;
			}
		}

		// Token: 0x060101D7 RID: 66007 RVA: 0x003750D0 File Offset: 0x003732D0
		public static async Task<FilterValueCompleter> CreateAsync(ColumnProfile profile, CancellationToken cancel = default(CancellationToken), ILogger logger = null)
		{
			return await new FilterValueCompleter(profile, logger).Initialize(cancel);
		}

		// Token: 0x060101D8 RID: 66008 RVA: 0x00375124 File Offset: 0x00373324
		public static async Task<FilterValueCompleter> CreateAsync(string columnName, IEnumerable<string> columnSample, CancellationToken cancel = default(CancellationToken))
		{
			ColumnProfile columnProfile = await ColumnProfile.CreateAsync(columnName, columnSample, cancel);
			cancel.ThrowIfCancellationRequested();
			return await FilterValueCompleter.CreateAsync(columnProfile, cancel, null);
		}

		// Token: 0x060101D9 RID: 66009 RVA: 0x00375178 File Offset: 0x00373378
		private FilterValueCompleter(ColumnProfile profile, ILogger logger = null)
		{
			this.Profile = profile;
			this.Logger = logger;
		}

		// Token: 0x060101DA RID: 66010 RVA: 0x003751C8 File Offset: 0x003733C8
		private void _MakeTreeEntries(CompressedStringTrie tree, IEnumerable<ValueAndCount<string>> values, CancellationToken cancel)
		{
			(from vAndC in values
				group vAndC by vAndC.Value).ToList<IGrouping<string, ValueAndCount<string>>>().ForEach(delegate(IGrouping<string, ValueAndCount<string>> g, int index)
			{
				if (index % 100 == 0)
				{
					cancel.ThrowIfCancellationRequested();
				}
				tree.Add(g.Key, new ValueAndCount<string>(g.Key, g.Sum((ValueAndCount<string> vAndC) => vAndC.Count)));
			});
		}

		// Token: 0x060101DB RID: 66011 RVA: 0x00375224 File Offset: 0x00373424
		private Task<FilterValueCompleter> Initialize(CancellationToken cancel)
		{
			Task prefixTask = Task.Run(delegate
			{
				this._MakeTreeEntries(this._prefixSuggester, this.Profile.Prefixes, cancel);
			});
			Task suffixTask = Task.Run(delegate
			{
				this._MakeTreeEntries(this._suffixSuggester, this.Profile.Suffixes, cancel);
			});
			Task stringTask = Task.Run(delegate
			{
				this._MakeTreeEntries(this._stringSuggester, this.Profile.Strings, cancel);
			});
			Task substringTask = Task.Run(delegate
			{
				this._MakeTreeEntries(this._substringSuggester, this.Profile.Substrings, cancel);
			});
			return Task.Run<FilterValueCompleter>(delegate
			{
				Task.WaitAll(new Task[] { prefixTask, suffixTask, substringTask, stringTask });
				return this;
			});
		}

		// Token: 0x060101DC RID: 66012 RVA: 0x003752B0 File Offset: 0x003734B0
		private IReadOnlyList<CompletionResult> MakeResultList(IEnumerable<ValueAndCount<string>> valueAndCounts)
		{
			return valueAndCounts.Select((ValueAndCount<string> vAndC) => new CompletionResult(vAndC.Value, (double)vAndC.Count / (double)this.Profile.SampleSize)).ToList<CompletionResult>();
		}

		// Token: 0x060101DD RID: 66013 RVA: 0x003752CC File Offset: 0x003734CC
		public IReadOnlyList<CompletionResult> Suggest(FilterOperator op, string prefix, string suffix = null)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			IReadOnlyList<CompletionResult> readOnlyList = this.SuggestInternal(op, prefix, suffix);
			stopwatch.Stop();
			double num = stopwatch.ElapsedMillisecondsAsDouble();
			new FilterValueCompleterSuggestEvent(op, prefix, readOnlyList, num).Log(this.Logger);
			return readOnlyList;
		}

		// Token: 0x060101DE RID: 66014 RVA: 0x0037530C File Offset: 0x0037350C
		private IReadOnlyList<CompletionResult> SuggestInternal(FilterOperator op, string prefix, string suffix = null)
		{
			switch (op)
			{
			case FilterOperator.EqualTo:
			case FilterOperator.NotEqualTo:
				return this.MakeResultList(this._stringSuggester.Suggest(prefix).ToList<ValueAndCount<string>>());
			case FilterOperator.IsNull:
			case FilterOperator.IsNotNull:
			case FilterOperator.IsError:
			case FilterOperator.IsNotError:
			case FilterOperator.IsEmpty:
			case FilterOperator.IsNotEmpty:
				return FilterValueCompleter.EmptyResult;
			case FilterOperator.StartsWith:
			case FilterOperator.NotStartsWith:
				return this.MakeResultList(this._prefixSuggester.Suggest(prefix));
			case FilterOperator.EndsWith:
			case FilterOperator.NotEndsWith:
				return this.MakeResultList(this._suffixSuggester.Suggest(prefix));
			case FilterOperator.Contains:
			case FilterOperator.NotContains:
				return this.MakeResultList(this._substringSuggester.Suggest(prefix));
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x04006151 RID: 24913
		public readonly ILogger Logger;

		// Token: 0x04006152 RID: 24914
		private readonly CompressedStringTrie _prefixSuggester = new CompressedStringTrie();

		// Token: 0x04006153 RID: 24915
		private readonly CompressedStringTrie _suffixSuggester = new CompressedStringTrie();

		// Token: 0x04006154 RID: 24916
		private readonly CompressedStringTrie _substringSuggester = new CompressedStringTrie();

		// Token: 0x04006155 RID: 24917
		private readonly CompressedStringTrie _stringSuggester = new CompressedStringTrie();

		// Token: 0x04006156 RID: 24918
		private static readonly List<CompletionResult> EmptyResult = new List<CompletionResult>(0);
	}
}
