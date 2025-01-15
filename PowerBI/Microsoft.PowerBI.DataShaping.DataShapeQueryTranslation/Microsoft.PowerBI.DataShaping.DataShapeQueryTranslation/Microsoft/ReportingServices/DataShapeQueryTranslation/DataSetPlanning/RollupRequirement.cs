using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000E5 RID: 229
	internal sealed class RollupRequirement : IStructuredToString
	{
		// Token: 0x06000956 RID: 2390 RVA: 0x00023BC8 File Offset: 0x00021DC8
		internal RollupRequirement(bool rollup, SortDirection sortDirection)
		{
			this.m_rollup = rollup;
			this.m_sortDirection = sortDirection;
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00023BDE File Offset: 0x00021DDE
		public bool Rollup
		{
			get
			{
				return this.m_rollup;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00023BE6 File Offset: 0x00021DE6
		public SortDirection SortDirection
		{
			get
			{
				return this.m_sortDirection;
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00023BEE File Offset: 0x00021DEE
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("RollupRequirement");
			builder.WriteAttribute<bool>("Rollup", this.m_rollup, false, false);
			builder.WriteAttribute<SortDirection>("SortDirection", this.m_sortDirection, false, false);
			builder.EndObject();
		}

		// Token: 0x0400046C RID: 1132
		private readonly bool m_rollup;

		// Token: 0x0400046D RID: 1133
		private readonly SortDirection m_sortDirection;
	}
}
