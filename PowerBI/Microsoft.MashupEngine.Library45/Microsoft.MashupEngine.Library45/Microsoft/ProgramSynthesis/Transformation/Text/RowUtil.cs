using System;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Text
{
	// Token: 0x02001BA9 RID: 7081
	public static class RowUtil
	{
		// Token: 0x0600E7F0 RID: 59376 RVA: 0x00312B19 File Offset: 0x00310D19
		public static State AsStateForLearning(this IRow row)
		{
			return State.CreateForLearning(Language.Grammar.InputSymbol, row);
		}

		// Token: 0x0600E7F1 RID: 59377 RVA: 0x00312B2B File Offset: 0x00310D2B
		public static State AsStateForExecution(this IRow row)
		{
			return State.CreateForExecution(Language.Grammar.InputSymbol, row);
		}

		// Token: 0x0600E7F2 RID: 59378 RVA: 0x00312B3D File Offset: 0x00310D3D
		internal static State AsStateForExecution(this IIndexableRow row)
		{
			return State.CreateForExecution(Language.Grammar.InputSymbol, new IndexableRowWrapper(row));
		}
	}
}
