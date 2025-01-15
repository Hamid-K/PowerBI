using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200062F RID: 1583
	[Serializable]
	public sealed class PublishingContext : PublishingContextBase
	{
		// Token: 0x060056EF RID: 22255 RVA: 0x0016EB98 File Offset: 0x0016CD98
		public PublishingContext(ICatalogItemContext catalogContext, byte[] datasetDefinition, IChunkFactory createChunkFactory, AppDomain compilationTempAppDomain, bool generateExpressionHostWithRefusedPermissions, ReportProcessing.CheckSharedDataSource checkDataSourceCallback, IConfiguration configuration)
			: base(PublishingContextKind.SharedDataSet, catalogContext, createChunkFactory, compilationTempAppDomain, generateExpressionHostWithRefusedPermissions, ReportProcessingFlags.NotSet, checkDataSourceCallback, null, null, null, null, null, configuration, null, false, false, false, false)
		{
			this.m_definition = datasetDefinition;
		}

		// Token: 0x060056F0 RID: 22256 RVA: 0x0016EBC8 File Offset: 0x0016CDC8
		public PublishingContext(ICatalogItemContext catalogContext, byte[] reportDefinition, IChunkFactory createChunkFactory, AppDomain compilationTempAppDomain, bool generateExpressionHostWithRefusedPermissions, ReportProcessingFlags processingFlags, IConfiguration configuration, IDataProtection dataProtection)
			: this(catalogContext, reportDefinition, createChunkFactory, compilationTempAppDomain, generateExpressionHostWithRefusedPermissions, processingFlags, null, null, null, null, null, null, configuration, dataProtection, false, false, false)
		{
		}

		// Token: 0x060056F1 RID: 22257 RVA: 0x0016EBF4 File Offset: 0x0016CDF4
		public PublishingContext(ICatalogItemContext catalogContext, byte[] reportDefinition, IChunkFactory createChunkFactory, AppDomain compilationTempAppDomain, bool generateExpressionHostWithRefusedPermissions, ReportProcessingFlags processingFlags, ReportProcessing.CheckSharedDataSource checkDataSourceCallback, ReportProcessing.ResolveTemporaryDataSource resolveTemporaryDataSourceCallback, DataSourceInfoCollection originalDataSources, ReportProcessing.CheckSharedDataSet checkDataSetCallback, ReportProcessing.ResolveTemporaryDataSet resolveTemporaryDataSetCallback, DataSetInfoCollection originalDataSets, IConfiguration configuration, IDataProtection dataProtection, bool isInternalRepublish, bool isPackagedReportArchive, bool isRdlx)
			: base(PublishingContextKind.Full, catalogContext, createChunkFactory, compilationTempAppDomain, generateExpressionHostWithRefusedPermissions, processingFlags, checkDataSourceCallback, resolveTemporaryDataSourceCallback, originalDataSources, checkDataSetCallback, resolveTemporaryDataSetCallback, originalDataSets, configuration, dataProtection, isInternalRepublish, isPackagedReportArchive, isRdlx, false)
		{
			this.m_definition = reportDefinition;
		}

		// Token: 0x17001FB1 RID: 8113
		// (get) Token: 0x060056F2 RID: 22258 RVA: 0x0016EC2E File Offset: 0x0016CE2E
		internal byte[] Definition
		{
			get
			{
				return this.m_definition;
			}
		}

		// Token: 0x04002DE4 RID: 11748
		private byte[] m_definition;
	}
}
