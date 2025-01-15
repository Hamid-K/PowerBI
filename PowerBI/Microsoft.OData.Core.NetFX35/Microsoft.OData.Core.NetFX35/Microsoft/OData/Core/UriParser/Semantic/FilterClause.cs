using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000240 RID: 576
	public sealed class FilterClause
	{
		// Token: 0x0600149D RID: 5277 RVA: 0x00049B43 File Offset: 0x00047D43
		public FilterClause(SingleValueNode expression, RangeVariable rangeVariable)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<RangeVariable>(rangeVariable, "parameter");
			this.expression = expression;
			this.rangeVariable = rangeVariable;
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x00049B6F File Offset: 0x00047D6F
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x00049B77 File Offset: 0x00047D77
		public RangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x00049B7F File Offset: 0x00047D7F
		public IEdmTypeReference ItemType
		{
			get
			{
				return this.RangeVariable.TypeReference;
			}
		}

		// Token: 0x040008A7 RID: 2215
		private readonly SingleValueNode expression;

		// Token: 0x040008A8 RID: 2216
		private readonly RangeVariable rangeVariable;
	}
}
