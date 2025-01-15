using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B0 RID: 432
	public sealed class ComputeExpression
	{
		// Token: 0x06001144 RID: 4420 RVA: 0x000306C8 File Offset: 0x0002E8C8
		public ComputeExpression(SingleValueNode expression, string alias, IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<string>(alias, "alias");
			this.expression = expression;
			this.alias = alias;
			this.typeReference = typeReference;
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x000306FD File Offset: 0x0002E8FD
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x00030705 File Offset: 0x0002E905
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x0003070D File Offset: 0x0002E90D
		public IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x040008D3 RID: 2259
		private readonly SingleValueNode expression;

		// Token: 0x040008D4 RID: 2260
		private readonly string alias;

		// Token: 0x040008D5 RID: 2261
		private readonly IEdmTypeReference typeReference;
	}
}
