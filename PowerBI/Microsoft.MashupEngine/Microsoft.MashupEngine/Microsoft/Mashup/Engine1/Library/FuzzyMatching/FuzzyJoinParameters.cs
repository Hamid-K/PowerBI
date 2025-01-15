using System;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B3C RID: 2876
	public class FuzzyJoinParameters : JoinAlgorithm.JoinParameters
	{
		// Token: 0x06004FD1 RID: 20433 RVA: 0x0010B420 File Offset: 0x00109620
		public FuzzyJoinParameters(RowCount take, Query leftQuery, int[] leftKeys, Query rightQuery, int[] rightKeys, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, FuzzyJoinOptions fuzzyJoinOptions)
			: base(take, leftQuery, leftKeys, rightQuery, rightKeys, joinKind, joinKeys, joinColumns)
		{
			this.fuzzyJoinOptions = fuzzyJoinOptions;
		}

		// Token: 0x170018D4 RID: 6356
		// (get) Token: 0x06004FD2 RID: 20434 RVA: 0x0010B448 File Offset: 0x00109648
		public FuzzyJoinOptions JoinOptions
		{
			get
			{
				return this.fuzzyJoinOptions;
			}
		}

		// Token: 0x04002AC9 RID: 10953
		private readonly FuzzyJoinOptions fuzzyJoinOptions;
	}
}
