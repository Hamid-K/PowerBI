using System;
using Microsoft.ReportingServices.DataExtensions;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000658 RID: 1624
	[Serializable]
	public sealed class DataSetPublishingResult
	{
		// Token: 0x06005A62 RID: 23138 RVA: 0x001727FD File Offset: 0x001709FD
		internal DataSetPublishingResult(DataSetDefinition dataSetDefinition, DataSourceInfo dataSourceInfo, UserLocationFlags userReferenceLocation, ProcessingMessageList warnings)
		{
			this.m_dataSetDefinition = dataSetDefinition;
			this.m_dataSourceInfo = dataSourceInfo;
			this.m_userReferenceLocation = userReferenceLocation;
			this.m_warnings = warnings;
		}

		// Token: 0x1700203C RID: 8252
		// (get) Token: 0x06005A63 RID: 23139 RVA: 0x00172822 File Offset: 0x00170A22
		public DataSetDefinition DataSetDefinition
		{
			get
			{
				return this.m_dataSetDefinition;
			}
		}

		// Token: 0x1700203D RID: 8253
		// (get) Token: 0x06005A64 RID: 23140 RVA: 0x0017282A File Offset: 0x00170A2A
		public DataSourceInfo DataSourceInfo
		{
			get
			{
				return this.m_dataSourceInfo;
			}
		}

		// Token: 0x1700203E RID: 8254
		// (get) Token: 0x06005A65 RID: 23141 RVA: 0x00172832 File Offset: 0x00170A32
		public bool HasUserProfileQueryDependencies
		{
			get
			{
				return (this.m_userReferenceLocation & UserLocationFlags.ReportQueries) != (UserLocationFlags)0;
			}
		}

		// Token: 0x1700203F RID: 8255
		// (get) Token: 0x06005A66 RID: 23142 RVA: 0x00172841 File Offset: 0x00170A41
		public ProcessingMessageList Warnings
		{
			get
			{
				return this.m_warnings;
			}
		}

		// Token: 0x17002040 RID: 8256
		// (get) Token: 0x06005A67 RID: 23143 RVA: 0x00172849 File Offset: 0x00170A49
		public int TimeOut
		{
			get
			{
				if (this.m_dataSetDefinition != null && this.m_dataSetDefinition.DataSetCore != null && this.m_dataSetDefinition.DataSetCore.Query != null)
				{
					return this.m_dataSetDefinition.DataSetCore.Query.TimeOut;
				}
				return 0;
			}
		}

		// Token: 0x04002E90 RID: 11920
		private DataSetDefinition m_dataSetDefinition;

		// Token: 0x04002E91 RID: 11921
		private DataSourceInfo m_dataSourceInfo;

		// Token: 0x04002E92 RID: 11922
		private UserLocationFlags m_userReferenceLocation;

		// Token: 0x04002E93 RID: 11923
		private ProcessingMessageList m_warnings;
	}
}
