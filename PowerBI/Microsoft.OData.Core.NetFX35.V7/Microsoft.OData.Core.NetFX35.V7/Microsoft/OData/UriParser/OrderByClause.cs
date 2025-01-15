using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200014D RID: 333
	public sealed class OrderByClause
	{
		// Token: 0x06000EB9 RID: 3769 RVA: 0x0002AB22 File Offset: 0x00028D22
		public OrderByClause(OrderByClause thenBy, SingleValueNode expression, OrderByDirection direction, RangeVariable rangeVariable)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<RangeVariable>(rangeVariable, "parameter");
			this.thenBy = thenBy;
			this.expression = expression;
			this.direction = direction;
			this.rangeVariable = rangeVariable;
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0002AB60 File Offset: 0x00028D60
		public OrderByClause ThenBy
		{
			get
			{
				return this.thenBy;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x0002AB68 File Offset: 0x00028D68
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x0002AB70 File Offset: 0x00028D70
		public OrderByDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x0002AB78 File Offset: 0x00028D78
		public RangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x0002AB80 File Offset: 0x00028D80
		public IEdmTypeReference ItemType
		{
			get
			{
				return this.RangeVariable.TypeReference;
			}
		}

		// Token: 0x04000785 RID: 1925
		private readonly SingleValueNode expression;

		// Token: 0x04000786 RID: 1926
		private readonly OrderByDirection direction;

		// Token: 0x04000787 RID: 1927
		private readonly RangeVariable rangeVariable;

		// Token: 0x04000788 RID: 1928
		private readonly OrderByClause thenBy;
	}
}
