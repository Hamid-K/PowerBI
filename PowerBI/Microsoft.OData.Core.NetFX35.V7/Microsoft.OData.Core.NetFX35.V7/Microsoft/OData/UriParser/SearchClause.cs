using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000156 RID: 342
	public sealed class SearchClause
	{
		// Token: 0x06000EE3 RID: 3811 RVA: 0x0002ADB3 File Offset: 0x00028FB3
		public SearchClause(SingleValueNode expression)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			this.expression = expression;
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x0002ADCE File Offset: 0x00028FCE
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x04000792 RID: 1938
		private readonly SingleValueNode expression;
	}
}
