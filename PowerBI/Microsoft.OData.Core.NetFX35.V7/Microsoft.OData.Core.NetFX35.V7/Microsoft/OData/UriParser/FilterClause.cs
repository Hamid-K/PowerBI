using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000139 RID: 313
	public sealed class FilterClause
	{
		// Token: 0x06000E1D RID: 3613 RVA: 0x0002955F File Offset: 0x0002775F
		public FilterClause(SingleValueNode expression, RangeVariable rangeVariable)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<RangeVariable>(rangeVariable, "parameter");
			this.expression = expression;
			this.rangeVariable = rangeVariable;
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0002958D File Offset: 0x0002778D
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00029595 File Offset: 0x00027795
		public RangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x0002959D File Offset: 0x0002779D
		public IEdmTypeReference ItemType
		{
			get
			{
				return this.RangeVariable.TypeReference;
			}
		}

		// Token: 0x0400075C RID: 1884
		private readonly SingleValueNode expression;

		// Token: 0x0400075D RID: 1885
		private readonly RangeVariable rangeVariable;
	}
}
