using System;
using System.IO;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000629 RID: 1577
	internal sealed class ProgressivePublishingContext : PublishingContextBase
	{
		// Token: 0x060056D6 RID: 22230 RVA: 0x0016E860 File Offset: 0x0016CA60
		public ProgressivePublishingContext(ICatalogItemContext catalogContext, Stream reportDefinition, ReportProcessing.CheckSharedDataSource checkDataSourceCallback, IConfiguration configuration, IDataProtection dataProtection, DataSourceInfoCollection originalDataSources, ReportProcessing.ResolveTemporaryDataSource resolveTemporaryDataSource, bool isPackagedReportArchive)
			: base(PublishingContextKind.Progressive, catalogContext, null, null, false, ReportProcessingFlags.OnDemandEngine, checkDataSourceCallback, resolveTemporaryDataSource, originalDataSources, null, null, null, configuration, dataProtection, false, isPackagedReportArchive, true, true)
		{
			this.m_definition = reportDefinition;
		}

		// Token: 0x17001FA6 RID: 8102
		// (get) Token: 0x060056D7 RID: 22231 RVA: 0x0016E891 File Offset: 0x0016CA91
		public Stream Definition
		{
			get
			{
				return this.m_definition;
			}
		}

		// Token: 0x04002DD8 RID: 11736
		private readonly Stream m_definition;
	}
}
