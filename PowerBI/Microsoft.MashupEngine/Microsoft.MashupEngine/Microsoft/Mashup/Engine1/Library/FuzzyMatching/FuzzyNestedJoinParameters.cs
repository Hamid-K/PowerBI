using System;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B54 RID: 2900
	public class FuzzyNestedJoinParameters : NestedJoinParameters
	{
		// Token: 0x0600503D RID: 20541 RVA: 0x0010CD2A File Offset: 0x0010AF2A
		public FuzzyNestedJoinParameters(Query leftQuery, int[] leftKeys, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumnName, Keys joinColumns, FuzzyJoinOptions fuzzyJoinOptions)
			: base(leftQuery, leftKeys, rightTable, rightKey, joinKind, newColumnName, joinColumns)
		{
			this.fuzzyJoinOptions = fuzzyJoinOptions;
		}

		// Token: 0x170018F7 RID: 6391
		// (get) Token: 0x0600503E RID: 20542 RVA: 0x0010CD45 File Offset: 0x0010AF45
		public FuzzyJoinOptions JoinOptions
		{
			get
			{
				return this.fuzzyJoinOptions;
			}
		}

		// Token: 0x04002B0E RID: 11022
		private readonly FuzzyJoinOptions fuzzyJoinOptions;
	}
}
