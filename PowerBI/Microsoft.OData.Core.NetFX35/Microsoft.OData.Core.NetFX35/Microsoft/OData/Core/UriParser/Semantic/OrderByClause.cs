using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000254 RID: 596
	public sealed class OrderByClause
	{
		// Token: 0x06001527 RID: 5415 RVA: 0x0004ADF2 File Offset: 0x00048FF2
		public OrderByClause(OrderByClause thenBy, SingleValueNode expression, OrderByDirection direction, RangeVariable rangeVariable)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<RangeVariable>(rangeVariable, "parameter");
			this.thenBy = thenBy;
			this.expression = expression;
			this.direction = direction;
			this.rangeVariable = rangeVariable;
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x0004AE2E File Offset: 0x0004902E
		public OrderByClause ThenBy
		{
			get
			{
				return this.thenBy;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x0004AE36 File Offset: 0x00049036
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0004AE3E File Offset: 0x0004903E
		public OrderByDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x0004AE46 File Offset: 0x00049046
		public RangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x0004AE4E File Offset: 0x0004904E
		public IEdmTypeReference ItemType
		{
			get
			{
				return this.RangeVariable.TypeReference;
			}
		}

		// Token: 0x040008D1 RID: 2257
		private readonly SingleValueNode expression;

		// Token: 0x040008D2 RID: 2258
		private readonly OrderByDirection direction;

		// Token: 0x040008D3 RID: 2259
		private readonly RangeVariable rangeVariable;

		// Token: 0x040008D4 RID: 2260
		private readonly OrderByClause thenBy;
	}
}
