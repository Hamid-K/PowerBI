using System;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Aggregation
{
	// Token: 0x020002A9 RID: 681
	public sealed class AggregateExpression
	{
		// Token: 0x0600178A RID: 6026 RVA: 0x00050A48 File Offset: 0x0004EC48
		public AggregateExpression(SingleValueNode expression, AggregationMethod method, string alias, IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<string>(alias, "alias");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			this.expression = expression;
			this.method = method;
			this.alias = alias;
			this.typeReference = typeReference;
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x00050A9A File Offset: 0x0004EC9A
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x00050AA2 File Offset: 0x0004ECA2
		public AggregationMethod Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x00050AAA File Offset: 0x0004ECAA
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x00050AB2 File Offset: 0x0004ECB2
		public IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x04000A20 RID: 2592
		private readonly AggregationMethod method;

		// Token: 0x04000A21 RID: 2593
		private readonly SingleValueNode expression;

		// Token: 0x04000A22 RID: 2594
		private readonly string alias;

		// Token: 0x04000A23 RID: 2595
		private readonly IEdmTypeReference typeReference;
	}
}
