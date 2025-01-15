using System;
using System.Diagnostics;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005AD RID: 1453
	public sealed class ReportDataSource
	{
		// Token: 0x06005242 RID: 21058 RVA: 0x0015AC80 File Offset: 0x00158E80
		public ReportDataSource(string dataSourceType, Guid modelID, ReportProcessing.CreateDataExtensionInstance createDataExtensionInstance)
		{
			if (dataSourceType == null)
			{
				if (Global.Tracer.TraceError)
				{
					Global.Tracer.Trace(TraceLevel.Error, "The data source type is null. Cannot instantiate data processing component.");
				}
				throw new ReportProcessingException(ErrorCode.rsDataSourceTypeNull);
			}
			this.m_dataSourceType = dataSourceType;
			this.m_modelID = modelID;
			this.m_createDataExtensionInstance = createDataExtensionInstance;
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x0015ACD4 File Offset: 0x00158ED4
		public IDbConnection CreateConnection()
		{
			IDbConnection dbConnection = this.m_createDataExtensionInstance(this.m_dataSourceType, this.m_modelID);
			if (dbConnection == null)
			{
				if (Global.Tracer.TraceError)
				{
					Global.Tracer.Trace(TraceLevel.Error, "The connection object of the data source type {0} does not implement any of the required interfaces.", new object[] { this.m_dataSourceType });
				}
				throw new DataExtensionNotFoundException(this.m_dataSourceType);
			}
			if (Global.Tracer.TraceVerbose)
			{
				Global.Tracer.Trace(TraceLevel.Verbose, "A connection object for the {0} data source has been created.", new object[] { this.m_dataSourceType });
			}
			return dbConnection;
		}

		// Token: 0x0400298F RID: 10639
		private readonly string m_dataSourceType;

		// Token: 0x04002990 RID: 10640
		private readonly Guid m_modelID;

		// Token: 0x04002991 RID: 10641
		private readonly ReportProcessing.CreateDataExtensionInstance m_createDataExtensionInstance;
	}
}
