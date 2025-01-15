using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002AA RID: 682
	internal sealed class RowInstance
	{
		// Token: 0x06001A2E RID: 6702 RVA: 0x00069E0B File Offset: 0x0006800B
		internal RowInstance(FieldInfo[] fieldInfos, Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow recordRow)
		{
			this.m_recordRow = recordRow;
			this.m_fieldInfos = fieldInfos;
		}

		// Token: 0x17000EEE RID: 3822
		public FieldInstance this[int fieldIndex]
		{
			get
			{
				if (fieldIndex < 0 || fieldIndex >= this.m_recordRow.RecordFields.Length)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[]
					{
						fieldIndex,
						0,
						this.m_recordRow.RecordFields.Length
					});
				}
				if (this.m_fields == null)
				{
					this.m_fields = new FieldInstance[this.m_recordRow.RecordFields.Length];
				}
				if (this.m_fields[fieldIndex] == null)
				{
					this.m_fields[fieldIndex] = new FieldInstance((this.m_fieldInfos != null) ? this.m_fieldInfos[fieldIndex] : null, this.m_recordRow.RecordFields[fieldIndex]);
				}
				return this.m_fields[fieldIndex];
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x00069EDC File Offset: 0x000680DC
		public bool IsAggregateRow
		{
			get
			{
				return this.m_recordRow.IsAggregateRow;
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x00069EE9 File Offset: 0x000680E9
		public int AggregationFieldCount
		{
			get
			{
				return this.m_recordRow.AggregationFieldCount;
			}
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00069EF8 File Offset: 0x000680F8
		internal void UpdateRecordRow(Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow recordRow)
		{
			this.m_recordRow = recordRow;
			if (this.m_fields != null)
			{
				for (int i = 0; i < this.m_fields.Length; i++)
				{
					FieldInstance fieldInstance = this.m_fields[i];
					if (fieldInstance != null)
					{
						fieldInstance.UpdateRecordField(this.m_recordRow.RecordFields[i]);
					}
				}
			}
		}

		// Token: 0x04000D0C RID: 3340
		private Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow m_recordRow;

		// Token: 0x04000D0D RID: 3341
		private FieldInstance[] m_fields;

		// Token: 0x04000D0E RID: 3342
		private readonly FieldInfo[] m_fieldInfos;
	}
}
