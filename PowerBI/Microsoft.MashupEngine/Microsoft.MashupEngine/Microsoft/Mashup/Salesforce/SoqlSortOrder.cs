using System;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001F6 RID: 502
	internal class SoqlSortOrder
	{
		// Token: 0x06000A16 RID: 2582 RVA: 0x00016672 File Offset: 0x00014872
		public SoqlSortOrder(SoqlExpression[] expressions, bool[] ascendings)
		{
			this.expressions = expressions;
			this.ascendings = ascendings;
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x00016688 File Offset: 0x00014888
		public SoqlExpression[] Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x00016690 File Offset: 0x00014890
		public bool[] Ascendings
		{
			get
			{
				return this.ascendings;
			}
		}

		// Token: 0x04000607 RID: 1543
		public static readonly SoqlSortOrder None = new SoqlSortOrder(new SoqlExpression[0], new bool[0]);

		// Token: 0x04000608 RID: 1544
		private readonly SoqlExpression[] expressions;

		// Token: 0x04000609 RID: 1545
		private readonly bool[] ascendings;
	}
}
