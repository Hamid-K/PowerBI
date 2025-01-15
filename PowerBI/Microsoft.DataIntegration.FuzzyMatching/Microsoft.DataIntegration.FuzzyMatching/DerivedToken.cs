using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000AD RID: 173
	[Serializable]
	internal struct DerivedToken
	{
		// Token: 0x060006B6 RID: 1718 RVA: 0x0001DF6A File Offset: 0x0001C16A
		internal DerivedToken(int t, int w, int p, bool bu)
		{
			this.token = t;
			this.weight = w;
			this.rulePos = p;
			this.isUnit = bu;
		}

		// Token: 0x04000275 RID: 629
		internal int token;

		// Token: 0x04000276 RID: 630
		internal int weight;

		// Token: 0x04000277 RID: 631
		internal int rulePos;

		// Token: 0x04000278 RID: 632
		internal bool isUnit;
	}
}
