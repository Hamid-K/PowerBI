using System;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008C0 RID: 2240
	public sealed class AggregateRowInfo
	{
		// Token: 0x06007AAD RID: 31405 RVA: 0x001F96EF File Offset: 0x001F78EF
		internal static AggregateRowInfo CreateAndSaveAggregateInfo(OnDemandProcessingContext odpContext)
		{
			AggregateRowInfo aggregateRowInfo = new AggregateRowInfo();
			aggregateRowInfo.SaveAggregateInfo(odpContext);
			return aggregateRowInfo;
		}

		// Token: 0x06007AAE RID: 31406 RVA: 0x001F9700 File Offset: 0x001F7900
		internal void SaveAggregateInfo(OnDemandProcessingContext odpContext)
		{
			FieldsImpl fieldsImpl = odpContext.ReportObjectModel.FieldsImpl;
			this.m_aggregationFieldCount = fieldsImpl.AggregationFieldCount;
			if (this.m_aggregationFieldChecked == null)
			{
				this.m_aggregationFieldChecked = new bool[fieldsImpl.Count];
			}
			for (int i = 0; i < fieldsImpl.Count; i++)
			{
				this.m_aggregationFieldChecked[i] = fieldsImpl[i].AggregationFieldChecked;
			}
			this.m_validAggregateRow = fieldsImpl.ValidAggregateRow;
		}

		// Token: 0x06007AAF RID: 31407 RVA: 0x001F9770 File Offset: 0x001F7970
		internal void RestoreAggregateInfo(OnDemandProcessingContext odpContext)
		{
			FieldsImpl fieldsImpl = odpContext.ReportObjectModel.FieldsImpl;
			fieldsImpl.AggregationFieldCount = this.m_aggregationFieldCount;
			Global.Tracer.Assert(this.m_aggregationFieldChecked != null, "(null != m_aggregationFieldChecked)");
			for (int i = 0; i < fieldsImpl.Count; i++)
			{
				fieldsImpl[i].AggregationFieldChecked = this.m_aggregationFieldChecked[i];
			}
			fieldsImpl.ValidAggregateRow = this.m_validAggregateRow;
		}

		// Token: 0x06007AB0 RID: 31408 RVA: 0x001F97E0 File Offset: 0x001F79E0
		internal void CombineAggregateInfo(OnDemandProcessingContext odpContext, AggregateRowInfo updated)
		{
			FieldsImpl fieldsImpl = odpContext.ReportObjectModel.FieldsImpl;
			if (updated == null)
			{
				fieldsImpl.ValidAggregateRow = false;
				return;
			}
			if (!updated.m_validAggregateRow)
			{
				fieldsImpl.ValidAggregateRow = false;
			}
			for (int i = 0; i < fieldsImpl.Count; i++)
			{
				if (updated.m_aggregationFieldChecked[i])
				{
					fieldsImpl.ConsumeAggregationField(i);
				}
			}
		}

		// Token: 0x04003D4D RID: 15693
		private bool[] m_aggregationFieldChecked;

		// Token: 0x04003D4E RID: 15694
		private int m_aggregationFieldCount;

		// Token: 0x04003D4F RID: 15695
		private bool m_validAggregateRow;
	}
}
