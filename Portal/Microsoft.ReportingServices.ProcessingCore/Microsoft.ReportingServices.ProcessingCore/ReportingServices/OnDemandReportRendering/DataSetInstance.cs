using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002A9 RID: 681
	internal sealed class DataSetInstance
	{
		// Token: 0x06001A26 RID: 6694 RVA: 0x00069C63 File Offset: 0x00067E63
		internal DataSetInstance(Microsoft.ReportingServices.OnDemandReportRendering.DataSet dataSetDef)
		{
			this.m_dataSetDef = dataSetDef;
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x00069C72 File Offset: 0x00067E72
		internal IDataComparer Comparer
		{
			get
			{
				if (this.m_dataSetInstance == null)
				{
					return null;
				}
				if (this.m_comparer == null)
				{
					this.m_comparer = this.m_dataSetInstance.CreateProcessingComparer(this.m_dataSetDef.RenderingContext.OdpContext);
				}
				return this.m_comparer;
			}
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00069CB0 File Offset: 0x00067EB0
		public void ResetContext()
		{
			if (this.m_dataReader == null)
			{
				this.CreateDataReader();
			}
			else if (!this.m_dataReader.MoveToFirstRow())
			{
				Global.Tracer.Trace(TraceLevel.Verbose, "OnDemandReportRendering.DataSetInstance triggered a second query execution or second chunk open for dataset: {0} in report {1}", new object[]
				{
					this.m_dataSetDef.Name,
					this.m_dataSetDef.RenderingContext.OdpContext.ReportContext.ItemPathAsString
				});
				this.Close();
				this.CreateDataReader();
			}
			this.m_row = null;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x00069D2E File Offset: 0x00067F2E
		public bool MoveNext()
		{
			if (this.m_dataReader == null)
			{
				return false;
			}
			if (this.m_dataReader.GetNextRow())
			{
				if (this.m_row != null)
				{
					this.m_row.UpdateRecordRow(this.m_dataReader.RecordRow);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00069D68 File Offset: 0x00067F68
		public void Close()
		{
			if (this.m_dataReader != null)
			{
				this.m_dataReader.Close();
				this.m_dataReader = null;
			}
			this.m_row = null;
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x00069D8B File Offset: 0x00067F8B
		public RowInstance Row
		{
			get
			{
				if (this.m_dataReader == null)
				{
					return null;
				}
				if (this.m_row == null)
				{
					this.m_row = new RowInstance(this.m_dataSetInstance.FieldInfos, this.m_dataReader.RecordRow);
				}
				return this.m_row;
			}
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00069DC8 File Offset: 0x00067FC8
		private void CreateDataReader()
		{
			OnDemandProcessingContext odpContext = this.m_dataSetDef.RenderingContext.OdpContext;
			this.m_dataReader = odpContext.CreateSequentialDataReader(this.m_dataSetDef.DataSetDef, out this.m_dataSetInstance);
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00069E03 File Offset: 0x00068003
		internal void SetNewContext()
		{
			this.Close();
		}

		// Token: 0x04000D07 RID: 3335
		private readonly Microsoft.ReportingServices.OnDemandReportRendering.DataSet m_dataSetDef;

		// Token: 0x04000D08 RID: 3336
		private DataSetInstance m_dataSetInstance;

		// Token: 0x04000D09 RID: 3337
		private RowInstance m_row;

		// Token: 0x04000D0A RID: 3338
		private IRecordRowReader m_dataReader;

		// Token: 0x04000D0B RID: 3339
		private IDataComparer m_comparer;
	}
}
