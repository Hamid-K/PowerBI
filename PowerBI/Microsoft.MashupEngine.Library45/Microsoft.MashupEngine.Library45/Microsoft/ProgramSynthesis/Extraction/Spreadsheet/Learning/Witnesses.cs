using System;
using System.Linq;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Learning
{
	// Token: 0x02000EA4 RID: 3748
	public class Witnesses : DomainLearningLogic
	{
		// Token: 0x06006661 RID: 26209 RVA: 0x0014E21B File Offset: 0x0014C41B
		public Witnesses(Grammar grammar, Witnesses.Options options)
			: base(grammar)
		{
			this._options = options;
			this._builders = GrammarBuilders.Instance(grammar);
		}

		// Token: 0x04002D2D RID: 11565
		private readonly GrammarBuilders _builders;

		// Token: 0x04002D2E RID: 11566
		private readonly Witnesses.Options _options;

		// Token: 0x04002D2F RID: 11567
		public static int[] KValues = Enumerable.Range(0, 30).ToArray<int>();

		// Token: 0x04002D30 RID: 11568
		public static StyleFilter[] StyleFilterValues = new StyleFilter[]
		{
			new StyleFilter(true, false, false, false, false, null, null, null),
			new StyleFilter(true, false, false, false, true, null, null, null),
			new StyleFilter(false, true, false, false, false, null, null, null),
			new StyleFilter(false, false, true, false, false, null, null, null),
			new StyleFilter(false, false, false, true, false, null, null, null)
		};

		// Token: 0x04002D31 RID: 11569
		public static SplitMode[] SplitModeValues = new SplitMode[]
		{
			SplitMode.RestOnly | SplitMode.RequirePrecedingBlank,
			SplitMode.RestOnly | SplitMode.RequirePrecedingAtMostOne,
			SplitMode.RestOnly,
			SplitMode.FirstCellOnly,
			SplitMode.Any,
			SplitMode.Only
		};

		// Token: 0x04002D32 RID: 11570
		public static TitleAboveMode[] TitleAboveModeValues = new TitleAboveMode[]
		{
			TitleAboveMode.MustBeMerged | TitleAboveMode.AtMostOneCell | TitleAboveMode.IncludeTopTableRow,
			TitleAboveMode.MustBeMerged | TitleAboveMode.AtMostOneCell,
			TitleAboveMode.AtMostOneCell | TitleAboveMode.IncludeTopTableRow,
			TitleAboveMode.AtMostOneCell,
			TitleAboveMode.AtMostTwoCells
		}.SelectMany((TitleAboveMode mode) => new TitleAboveMode[]
		{
			mode,
			mode | TitleAboveMode.RequireAligned,
			mode | TitleAboveMode.AllowGaps | TitleAboveMode.RequireAligned
		}).ToArray<TitleAboveMode>();

		// Token: 0x02000EA5 RID: 3749
		public class Options : DSLOptions
		{
		}
	}
}
