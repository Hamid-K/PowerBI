using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007BF RID: 1983
	internal sealed class DataSourceImpl : Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.DataSource
	{
		// Token: 0x0600705F RID: 28767 RVA: 0x001D4554 File Offset: 0x001D2754
		internal DataSourceImpl(Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSourceDef)
		{
			this.m_dataSource = dataSourceDef;
		}

		// Token: 0x1700264B RID: 9803
		// (get) Token: 0x06007060 RID: 28768 RVA: 0x001D4563 File Offset: 0x001D2763
		public override string DataSourceReference
		{
			get
			{
				if (this.m_dataSource.SharedDataSourceReferencePath == null)
				{
					return this.m_dataSource.DataSourceReference;
				}
				return this.m_dataSource.SharedDataSourceReferencePath;
			}
		}

		// Token: 0x1700264C RID: 9804
		// (get) Token: 0x06007061 RID: 28769 RVA: 0x001D4589 File Offset: 0x001D2789
		public override string Type
		{
			get
			{
				return this.m_dataSource.Type;
			}
		}

		// Token: 0x04003A0B RID: 14859
		private Microsoft.ReportingServices.ReportIntermediateFormat.DataSource m_dataSource;
	}
}
