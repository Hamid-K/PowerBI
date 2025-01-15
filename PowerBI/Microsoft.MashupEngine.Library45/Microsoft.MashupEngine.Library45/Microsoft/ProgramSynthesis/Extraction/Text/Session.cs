using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;
using Microsoft.ProgramSynthesis.Wrangling.Session;

namespace Microsoft.ProgramSynthesis.Extraction.Text
{
	// Token: 0x02000EFD RID: 3837
	public class Session : NonInteractiveSession<Program, StringRegion, ITable<StringRegion>>
	{
		// Token: 0x0600685A RID: 26714 RVA: 0x00153F5B File Offset: 0x0015215B
		public Session(IJournalStorage journalStorage = null, CultureInfo culture = null)
			: base(Learner.Instance, Loader.Instance, journalStorage, culture, null, null, true)
		{
			this._normalizedInputs = new ConcurrentDictionary<string, StringRegion>();
		}

		// Token: 0x0600685B RID: 26715 RVA: 0x00153F80 File Offset: 0x00152180
		public void AddExample(string input, ITable<ExampleCell> table)
		{
			ConcurrentDictionary<string, StringRegion> normalizedInputs = this._normalizedInputs;
			string text = Example.NormalizeNewLines(input);
			Func<string, StringRegion> func;
			if ((func = Session.<>O.<0>__CreateStringRegion) == null)
			{
				func = (Session.<>O.<0>__CreateStringRegion = new Func<string, StringRegion>(Semantics.CreateStringRegion));
			}
			StringRegion orAdd = normalizedInputs.GetOrAdd(text, func);
			base.Constraints.Add(new Example(orAdd, table));
		}

		// Token: 0x04002E3B RID: 11835
		private ConcurrentDictionary<string, StringRegion> _normalizedInputs;

		// Token: 0x02000EFE RID: 3838
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04002E3C RID: 11836
			public static Func<string, StringRegion> <0>__CreateStringRegion;
		}
	}
}
