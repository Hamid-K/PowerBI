using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B4E RID: 2894
	public abstract class FuzzyNestedJoinAlgorithm : NestedJoinAlgorithm
	{
		// Token: 0x0600502D RID: 20525 RVA: 0x0010C9D7 File Offset: 0x0010ABD7
		public static NestedJoinAlgorithm GetFuzzyNestedJoinAlgorithm(IEngineHost host, TableTypeAlgebra.JoinKind joinKind)
		{
			if (joinKind != TableTypeAlgebra.JoinKind.LeftOuter)
			{
				return new GroupJoinFuzzyNestedJoinAlgorithm(host);
			}
			return new IndexFuzzyNestedJoinAlgorithm(host);
		}
	}
}
