using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A2 RID: 418
	public sealed class SearchClause
	{
		// Token: 0x06001408 RID: 5128 RVA: 0x0003ACFB File Offset: 0x00038EFB
		public SearchClause(SingleValueNode expression)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			this.expression = expression;
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0003AD16 File Offset: 0x00038F16
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x040008D6 RID: 2262
		private readonly SingleValueNode expression;
	}
}
