using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000183 RID: 387
	public sealed class FilterClause
	{
		// Token: 0x06001323 RID: 4899 RVA: 0x00039228 File Offset: 0x00037428
		public FilterClause(SingleValueNode expression, RangeVariable rangeVariable)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<RangeVariable>(rangeVariable, "parameter");
			this.expression = expression;
			this.rangeVariable = rangeVariable;
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x00039256 File Offset: 0x00037456
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x0003925E File Offset: 0x0003745E
		public RangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x00039266 File Offset: 0x00037466
		public IEdmTypeReference ItemType
		{
			get
			{
				return this.RangeVariable.TypeReference;
			}
		}

		// Token: 0x04000891 RID: 2193
		private readonly SingleValueNode expression;

		// Token: 0x04000892 RID: 2194
		private readonly RangeVariable rangeVariable;
	}
}
