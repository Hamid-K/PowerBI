using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000EA RID: 234
	internal sealed class DataSetPlanGeneratorResult
	{
		// Token: 0x06000979 RID: 2425 RVA: 0x00023E81 File Offset: 0x00022081
		internal DataSetPlanGeneratorResult(List<DataSetPlanInfo> dataSetPlanInfos)
		{
			this.m_dataSetPlanInfos = dataSetPlanInfos;
			this.m_outputPlanMapping = DataSetPlanGeneratorResult.CreateOutputPlanMapping(dataSetPlanInfos);
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00023E9C File Offset: 0x0002209C
		internal List<DataSetPlanInfo> DataSetPlanInfos
		{
			get
			{
				return this.m_dataSetPlanInfos;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00023EA4 File Offset: 0x000220A4
		internal OutputPlanMapping OutputPlanMapping
		{
			get
			{
				return this.m_outputPlanMapping;
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00023EAC File Offset: 0x000220AC
		private static OutputPlanMapping CreateOutputPlanMapping(List<DataSetPlanInfo> plans)
		{
			OutputPlanMapping outputPlanMapping = new OutputPlanMapping();
			for (int i = 0; i < plans.Count; i++)
			{
				DataSetPlanInfo dataSetPlanInfo = plans[i];
				dataSetPlanInfo.PlanIndex = i;
				foreach (IContextItem contextItem in dataSetPlanInfo.OutputItems)
				{
					outputPlanMapping.Add(contextItem, i);
				}
			}
			return outputPlanMapping;
		}

		// Token: 0x04000475 RID: 1141
		private readonly List<DataSetPlanInfo> m_dataSetPlanInfos;

		// Token: 0x04000476 RID: 1142
		private readonly OutputPlanMapping m_outputPlanMapping;
	}
}
