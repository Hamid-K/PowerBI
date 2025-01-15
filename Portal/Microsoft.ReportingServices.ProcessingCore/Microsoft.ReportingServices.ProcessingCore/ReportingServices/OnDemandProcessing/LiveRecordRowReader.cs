using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007F3 RID: 2035
	internal sealed class LiveRecordRowReader : IRecordRowReader, IDisposable
	{
		// Token: 0x060071A7 RID: 29095 RVA: 0x001D84CE File Offset: 0x001D66CE
		public LiveRecordRowReader(DataSet dataSet, OnDemandProcessingContext odpContext)
		{
			this.m_dataSource = new RuntimeLiveReaderDataSource(odpContext.ReportDefinition, dataSet, odpContext);
			this.m_dataSource.Initialize();
		}

		// Token: 0x060071A8 RID: 29096 RVA: 0x001D84F4 File Offset: 0x001D66F4
		public bool GetNextRow()
		{
			this.m_currentRow = this.m_dataSource.ReadNextRow();
			return this.m_currentRow != null;
		}

		// Token: 0x1700269E RID: 9886
		// (get) Token: 0x060071A9 RID: 29097 RVA: 0x001D8510 File Offset: 0x001D6710
		public RecordRow RecordRow
		{
			get
			{
				return this.m_currentRow;
			}
		}

		// Token: 0x060071AA RID: 29098 RVA: 0x001D8518 File Offset: 0x001D6718
		public bool MoveToFirstRow()
		{
			return false;
		}

		// Token: 0x060071AB RID: 29099 RVA: 0x001D851B File Offset: 0x001D671B
		public void Close()
		{
			if (this.m_dataSource != null)
			{
				this.m_dataSource.RecordTimeDataRetrieval();
				this.m_dataSource.Teardown();
				this.m_dataSource = null;
			}
		}

		// Token: 0x060071AC RID: 29100 RVA: 0x001D8542 File Offset: 0x001D6742
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x1700269F RID: 9887
		// (get) Token: 0x060071AD RID: 29101 RVA: 0x001D854A File Offset: 0x001D674A
		public DataSetInstance DataSetInstance
		{
			get
			{
				return this.m_dataSource.DataSetInstance;
			}
		}

		// Token: 0x04003A80 RID: 14976
		private RuntimeLiveReaderDataSource m_dataSource;

		// Token: 0x04003A81 RID: 14977
		private RecordRow m_currentRow;
	}
}
