using System;
using System.Globalization;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000628 RID: 1576
	internal sealed class ProgressiveProcessingContext : StreamingOdpProcessingContextBase
	{
		// Token: 0x060056D4 RID: 22228 RVA: 0x0016E828 File Offset: 0x0016CA28
		internal ProgressiveProcessingContext(string requestUserName, IGetResource getResourceFunction, CultureInfo userLanguage, IProcessingDataExtensionConnection createDataExtensionInstanceFunction, ReportRuntimeSetup reportRuntimeSetup, CreateAndRegisterStream createStreamCallback, IJobContext jobContext, IDataProtection dataProtection, IConfiguration configuration, ServerDataSourceSettings serverDataSourceSettings, DateTime executionTimeStamp, DataSourceInfoCollection dataSources)
			: base(requestUserName, getResourceFunction, userLanguage, createDataExtensionInstanceFunction, reportRuntimeSetup, createStreamCallback, jobContext, dataProtection, configuration, serverDataSourceSettings, executionTimeStamp)
		{
			this.m_dataSources = dataSources;
		}

		// Token: 0x17001FA5 RID: 8101
		// (get) Token: 0x060056D5 RID: 22229 RVA: 0x0016E856 File Offset: 0x0016CA56
		public DataSourceInfoCollection DataSources
		{
			get
			{
				return this.m_dataSources;
			}
		}

		// Token: 0x04002DD7 RID: 11735
		private readonly DataSourceInfoCollection m_dataSources;
	}
}
