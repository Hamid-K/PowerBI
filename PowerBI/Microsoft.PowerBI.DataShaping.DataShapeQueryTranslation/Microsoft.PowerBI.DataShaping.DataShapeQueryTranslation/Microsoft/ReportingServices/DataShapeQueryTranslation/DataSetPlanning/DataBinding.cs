using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000DC RID: 220
	internal sealed class DataBinding : IStructuredToString
	{
		// Token: 0x06000910 RID: 2320 RVA: 0x0002324B File Offset: 0x0002144B
		internal DataBinding(int dataSetPlanIndex, Relationship relationship = null, bool shouldRestoreContext = false)
		{
			this.m_dataSetPlanIndex = dataSetPlanIndex;
			this.m_relationship = relationship;
			this.m_shouldRestoreContext = shouldRestoreContext;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00023268 File Offset: 0x00021468
		public int DataSetPlanIndex
		{
			get
			{
				return this.m_dataSetPlanIndex;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00023270 File Offset: 0x00021470
		public Relationship Relationship
		{
			get
			{
				return this.m_relationship;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00023278 File Offset: 0x00021478
		public bool ShouldRestoreContext
		{
			get
			{
				return this.m_shouldRestoreContext;
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00023280 File Offset: 0x00021480
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("DataBinding");
			builder.WriteAttribute<int>("PlanIndex", this.m_dataSetPlanIndex, true, false);
			builder.WriteAttribute<bool>("ShouldRestoreContext", this.m_shouldRestoreContext, false, false);
			builder.WriteProperty<Relationship>("Relationship", this.m_relationship, false);
			builder.EndObject();
		}

		// Token: 0x0400044C RID: 1100
		private readonly int m_dataSetPlanIndex;

		// Token: 0x0400044D RID: 1101
		private readonly Relationship m_relationship;

		// Token: 0x0400044E RID: 1102
		private readonly bool m_shouldRestoreContext;
	}
}
