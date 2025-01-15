using System;
using System.Globalization;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.OnDemandReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005FB RID: 1531
	internal sealed class DataShapeProcessingContext : StreamingOdpProcessingContextBase
	{
		// Token: 0x0600545A RID: 21594 RVA: 0x0016243C File Offset: 0x0016063C
		public DataShapeProcessingContext(string requestUserName, IGetResource getResourceFunction, CultureInfo userLanguage, IProcessingDataExtensionConnection createDataExtensionInstanceFunction, ReportRuntimeSetup reportRuntimeSetup, CreateAndRegisterStream createStreamCallback, CreateJsonStreamWriter createJsonStreamCallback, IJobContext jobContext, Microsoft.ReportingServices.Diagnostics.Internal.DataShape dataShapeAdditionalInfo, IDataProtection dataProtection, IConfiguration configuration, ServerDataSourceSettings serverDataSourceSettings, DateTime executionTimeStamp, IDataShapeAbortHelper abortHelper, bool? useParallelQueryExecution = null)
			: base(requestUserName, getResourceFunction, userLanguage, createDataExtensionInstanceFunction, reportRuntimeSetup, createStreamCallback, jobContext, dataProtection, configuration, serverDataSourceSettings, executionTimeStamp)
		{
			this.CreateJsonStreamWriterCallback = createJsonStreamCallback;
			this.DataShapeAdditionalInfo = dataShapeAdditionalInfo;
			this.m_abortHelper = abortHelper;
			this.UseParallelQueryExecution = useParallelQueryExecution.GetValueOrDefault(true);
		}

		// Token: 0x0600545B RID: 21595 RVA: 0x00162488 File Offset: 0x00160688
		internal override IAbortHelper GetAbortHelper()
		{
			return this.m_abortHelper;
		}

		// Token: 0x17001F07 RID: 7943
		// (get) Token: 0x0600545C RID: 21596 RVA: 0x00162490 File Offset: 0x00160690
		// (set) Token: 0x0600545D RID: 21597 RVA: 0x00162498 File Offset: 0x00160698
		internal CreateJsonStreamWriter CreateJsonStreamWriterCallback { get; private set; }

		// Token: 0x17001F08 RID: 7944
		// (get) Token: 0x0600545E RID: 21598 RVA: 0x001624A1 File Offset: 0x001606A1
		// (set) Token: 0x0600545F RID: 21599 RVA: 0x001624A9 File Offset: 0x001606A9
		internal Microsoft.ReportingServices.Diagnostics.Internal.DataShape DataShapeAdditionalInfo { get; private set; }

		// Token: 0x17001F09 RID: 7945
		// (get) Token: 0x06005460 RID: 21600 RVA: 0x001624B2 File Offset: 0x001606B2
		// (set) Token: 0x06005461 RID: 21601 RVA: 0x001624BA File Offset: 0x001606BA
		internal bool UseParallelQueryExecution { get; private set; }

		// Token: 0x04002CEB RID: 11499
		private readonly IDataShapeAbortHelper m_abortHelper;
	}
}
