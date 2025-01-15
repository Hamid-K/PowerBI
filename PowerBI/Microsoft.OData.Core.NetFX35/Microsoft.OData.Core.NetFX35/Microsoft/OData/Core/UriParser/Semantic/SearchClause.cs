using System;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200025B RID: 603
	public sealed class SearchClause
	{
		// Token: 0x06001549 RID: 5449 RVA: 0x0004B07A File Offset: 0x0004927A
		public SearchClause(SingleValueNode expression)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			this.expression = expression;
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0004B094 File Offset: 0x00049294
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x040008DE RID: 2270
		private readonly SingleValueNode expression;
	}
}
