using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Build;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Split
{
	// Token: 0x02000F6A RID: 3946
	internal class GroupRegexLearner : GroupLearner
	{
		// Token: 0x06006D9D RID: 28061 RVA: 0x00165A6B File Offset: 0x00163C6B
		protected override IEnumerable<Record<records, TableExample>> Learn(StringRegion input, IReadOnlyList<List<StringRegionCell>> table, IReadOnlyList<StringRegion> records, IReadOnlyList<StringRegion> lines, skip skipNode, GrammarBuilders builder)
		{
			Func<Regex, IReadOnlyList<StringRegion>, IReadOnlyList<StringRegion>> func;
			if ((func = GroupRegexLearner.<>O.<0>__Group) == null)
			{
				func = (GroupRegexLearner.<>O.<0>__Group = new Func<Regex, IReadOnlyList<StringRegion>, IReadOnlyList<StringRegion>>(Semantics.Group));
			}
			foreach (Record<Regex, TableExample> record in GroupLearner.FindRegexAndGroupExample(table, records, lines, func))
			{
				Regex regex;
				TableExample tableExample;
				record.Deconstruct(out regex, out tableExample);
				Regex regex2 = regex;
				TableExample tableExample2 = tableExample;
				yield return new Record<records, TableExample>(builder.Node.Rule.Group(builder.Node.Rule.re(regex2), skipNode), tableExample2);
			}
			IEnumerator<Record<Regex, TableExample>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x02000F6B RID: 3947
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04002F88 RID: 12168
			public static Func<Regex, IReadOnlyList<StringRegion>, IReadOnlyList<StringRegion>> <0>__Group;
		}
	}
}
