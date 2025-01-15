using System;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000161 RID: 353
	internal sealed class BatchDataBinding : IStructuredToString
	{
		// Token: 0x06000CD9 RID: 3289 RVA: 0x0003510D File Offset: 0x0003330D
		internal BatchDataBinding(BatchDataSetPlan dataSetPlan, int outputTableIndex)
		{
			Contract.RetailAssert(outputTableIndex >= 0, "outputTableIndex");
			this.m_dataSetPlan = dataSetPlan;
			this.m_outputTableIndex = outputTableIndex;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00035134 File Offset: 0x00033334
		public BatchDataSetPlan DataSetPlan
		{
			get
			{
				return this.m_dataSetPlan;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0003513C File Offset: 0x0003333C
		public int OutputTableIndex
		{
			get
			{
				return this.m_outputTableIndex;
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00035144 File Offset: 0x00033344
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("DataBinding");
			builder.WriteAttribute<string>("DataSetPlanName", this.DataSetPlan.Name, false, false);
			builder.WriteAttribute<int>("OutputTableIndex", this.OutputTableIndex, true, false);
			builder.EndObject();
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00035184 File Offset: 0x00033384
		public override string ToString()
		{
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(ExpressionStringBuilderFactory.Create(null, false), 0, false);
			this.WriteTo(structuredStringBuilder);
			return structuredStringBuilder.ToString();
		}

		// Token: 0x0400065D RID: 1629
		private readonly BatchDataSetPlan m_dataSetPlan;

		// Token: 0x0400065E RID: 1630
		private readonly int m_outputTableIndex;
	}
}
