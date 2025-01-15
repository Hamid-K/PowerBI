using System;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text
{
	// Token: 0x02001BA2 RID: 7074
	public class Branch : Tuple<b?, st>
	{
		// Token: 0x0600E78E RID: 59278 RVA: 0x00311DFA File Offset: 0x0030FFFA
		public Branch(b? predicate, st body)
			: base(predicate, body)
		{
		}

		// Token: 0x170026A2 RID: 9890
		// (get) Token: 0x0600E78F RID: 59279 RVA: 0x00311E04 File Offset: 0x00310004
		public b? Predicate
		{
			get
			{
				return base.Item1;
			}
		}

		// Token: 0x170026A3 RID: 9891
		// (get) Token: 0x0600E790 RID: 59280 RVA: 0x00311E0C File Offset: 0x0031000C
		public st Body
		{
			get
			{
				return base.Item2;
			}
		}
	}
}
