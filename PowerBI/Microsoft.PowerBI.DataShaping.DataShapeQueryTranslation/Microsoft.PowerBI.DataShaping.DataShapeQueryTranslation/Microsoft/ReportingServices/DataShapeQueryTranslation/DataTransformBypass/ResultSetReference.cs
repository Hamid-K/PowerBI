using System;
using Microsoft.DataShaping.Common;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataTransformBypass
{
	// Token: 0x020000C8 RID: 200
	internal sealed class ResultSetReference : IEquatable<ResultSetReference>
	{
		// Token: 0x06000863 RID: 2147 RVA: 0x0001FE85 File Offset: 0x0001E085
		internal ResultSetReference(IDataSetPlan dataSetPlan, PlanNamedTable table)
		{
			this.m_dataSetPlan = dataSetPlan;
			this.m_table = table;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0001FE9B File Offset: 0x0001E09B
		internal IDataSetPlan DataSetPlan
		{
			get
			{
				return this.m_dataSetPlan;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0001FEA3 File Offset: 0x0001E0A3
		internal PlanNamedTable Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001FEAB File Offset: 0x0001E0AB
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResultSetReference);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001FEB9 File Offset: 0x0001E0B9
		public bool Equals(ResultSetReference other)
		{
			return other != null && this.DataSetPlan == other.DataSetPlan && this.Table == other.Table;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001FEDC File Offset: 0x0001E0DC
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.m_dataSetPlan.GetHashCode(), Hashing.GetHashCode<PlanNamedTable>(this.m_table, null));
		}

		// Token: 0x0400041D RID: 1053
		private readonly IDataSetPlan m_dataSetPlan;

		// Token: 0x0400041E RID: 1054
		private readonly PlanNamedTable m_table;
	}
}
