using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000199 RID: 409
	public sealed class OrderByClause
	{
		// Token: 0x060013CA RID: 5066 RVA: 0x0003A93E File Offset: 0x00038B3E
		public OrderByClause(OrderByClause thenBy, SingleValueNode expression, OrderByDirection direction, RangeVariable rangeVariable)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<RangeVariable>(rangeVariable, "parameter");
			this.thenBy = thenBy;
			this.expression = expression;
			this.direction = direction;
			this.rangeVariable = rangeVariable;
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x0003A97C File Offset: 0x00038B7C
		public OrderByClause ThenBy
		{
			get
			{
				return this.thenBy;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x0003A984 File Offset: 0x00038B84
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0003A98C File Offset: 0x00038B8C
		public OrderByDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x0003A994 File Offset: 0x00038B94
		public RangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x0003A99C File Offset: 0x00038B9C
		public IEdmTypeReference ItemType
		{
			get
			{
				return this.RangeVariable.TypeReference;
			}
		}

		// Token: 0x040008C0 RID: 2240
		private readonly SingleValueNode expression;

		// Token: 0x040008C1 RID: 2241
		private readonly OrderByDirection direction;

		// Token: 0x040008C2 RID: 2242
		private readonly RangeVariable rangeVariable;

		// Token: 0x040008C3 RID: 2243
		private readonly OrderByClause thenBy;
	}
}
