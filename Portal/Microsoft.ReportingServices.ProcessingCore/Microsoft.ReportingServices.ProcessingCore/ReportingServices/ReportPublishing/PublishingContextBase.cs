using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x0200038A RID: 906
	[Serializable]
	public abstract class PublishingContextBase
	{
		// Token: 0x06002509 RID: 9481 RVA: 0x000B1684 File Offset: 0x000AF884
		protected PublishingContextBase(PublishingContextKind publishingContextKind, ICatalogItemContext catalogContext, IChunkFactory createChunkFactory, AppDomain compilationTempAppDomain, bool generateExpressionHostWithRefusedPermissions, ReportProcessingFlags processingFlags, ReportProcessing.CheckSharedDataSource checkDataSourceCallback, ReportProcessing.ResolveTemporaryDataSource resolveTemporaryDataSourceCallback, DataSourceInfoCollection originalDataSources, ReportProcessing.CheckSharedDataSet checkDataSetCallback, ReportProcessing.ResolveTemporaryDataSet resolveTemporaryDataSetCallback, DataSetInfoCollection originalDataSets, IConfiguration configuration, IDataProtection dataProtection, bool isInternalRepublish, bool isPackagedReportArchive, bool isRdlx, bool traceAtomicScopes)
		{
			this.m_publishingContextKind = publishingContextKind;
			this.m_catalogContext = catalogContext;
			this.m_createChunkFactory = createChunkFactory;
			this.m_compilationTempAppDomain = compilationTempAppDomain;
			this.m_generateExpressionHostWithRefusedPermissions = generateExpressionHostWithRefusedPermissions;
			this.m_processingFlags = processingFlags;
			this.m_checkDataSourceCallback = checkDataSourceCallback;
			this.m_checkDataSetCallback = checkDataSetCallback;
			this.m_resolveTemporaryDataSourceCallback = resolveTemporaryDataSourceCallback;
			this.m_resolveTemporaryDataSetCallback = resolveTemporaryDataSetCallback;
			this.m_originalDataSources = originalDataSources;
			this.m_originalDataSets = originalDataSets;
			this.m_configuration = configuration;
			this.m_dataProtection = dataProtection;
			this.m_isInternalRepublish = isInternalRepublish;
			this.m_traceAtomicScopes = traceAtomicScopes;
			this.m_isPackagedReportArchive = isPackagedReportArchive;
			this.m_isRdlx = isRdlx;
			this.m_publishingVersioning = new PublishingVersioning(this.m_configuration, this);
		}

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x0600250A RID: 9482 RVA: 0x000B1736 File Offset: 0x000AF936
		internal PublishingVersioning PublishingVersioning
		{
			get
			{
				return this.m_publishingVersioning;
			}
		}

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x000B173E File Offset: 0x000AF93E
		internal bool IsInternalRepublish
		{
			get
			{
				return this.m_isInternalRepublish;
			}
		}

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x000B1746 File Offset: 0x000AF946
		internal PublishingContextKind PublishingContextKind
		{
			get
			{
				return this.m_publishingContextKind;
			}
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x0600250D RID: 9485 RVA: 0x000B174E File Offset: 0x000AF94E
		internal ICatalogItemContext CatalogContext
		{
			get
			{
				return this.m_catalogContext;
			}
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x0600250E RID: 9486 RVA: 0x000B1756 File Offset: 0x000AF956
		internal IChunkFactory CreateChunkFactory
		{
			get
			{
				return this.m_createChunkFactory;
			}
		}

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x0600250F RID: 9487 RVA: 0x000B175E File Offset: 0x000AF95E
		internal AppDomain CompilationTempAppDomain
		{
			get
			{
				return this.m_compilationTempAppDomain;
			}
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x000B1766 File Offset: 0x000AF966
		internal bool GenerateExpressionHostWithRefusedPermissions
		{
			get
			{
				return this.m_generateExpressionHostWithRefusedPermissions;
			}
		}

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x06002511 RID: 9489 RVA: 0x000B176E File Offset: 0x000AF96E
		// (set) Token: 0x06002512 RID: 9490 RVA: 0x000B1776 File Offset: 0x000AF976
		internal ReportProcessingFlags ProcessingFlags
		{
			get
			{
				return this.m_processingFlags;
			}
			set
			{
				this.m_processingFlags = value;
			}
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06002513 RID: 9491 RVA: 0x000B177F File Offset: 0x000AF97F
		internal ReportProcessing.CheckSharedDataSource CheckDataSourceCallback
		{
			get
			{
				return this.m_checkDataSourceCallback;
			}
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06002514 RID: 9492 RVA: 0x000B1787 File Offset: 0x000AF987
		internal ReportProcessing.CheckSharedDataSet CheckDataSetCallback
		{
			get
			{
				return this.m_checkDataSetCallback;
			}
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06002515 RID: 9493 RVA: 0x000B178F File Offset: 0x000AF98F
		internal ReportProcessing.ResolveTemporaryDataSource ResolveTemporaryDataSourceCallback
		{
			get
			{
				return this.m_resolveTemporaryDataSourceCallback;
			}
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06002516 RID: 9494 RVA: 0x000B1797 File Offset: 0x000AF997
		internal ReportProcessing.ResolveTemporaryDataSet ResolveTemporaryDataSetCallback
		{
			get
			{
				return this.m_resolveTemporaryDataSetCallback;
			}
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06002517 RID: 9495 RVA: 0x000B179F File Offset: 0x000AF99F
		internal DataSourceInfoCollection OriginalDataSources
		{
			get
			{
				return this.m_originalDataSources;
			}
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06002518 RID: 9496 RVA: 0x000B17A7 File Offset: 0x000AF9A7
		internal DataSetInfoCollection OriginalDataSets
		{
			get
			{
				return this.m_originalDataSets;
			}
		}

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x000B17AF File Offset: 0x000AF9AF
		internal IConfiguration Configuration
		{
			get
			{
				return this.m_configuration;
			}
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x000B17B7 File Offset: 0x000AF9B7
		internal IDataProtection DataProtection
		{
			get
			{
				return this.m_dataProtection;
			}
		}

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x0600251B RID: 9499 RVA: 0x000B17BF File Offset: 0x000AF9BF
		internal bool TraceAtomicScopes
		{
			get
			{
				return this.m_traceAtomicScopes;
			}
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x0600251C RID: 9500 RVA: 0x000B17C7 File Offset: 0x000AF9C7
		internal bool IsRdlx
		{
			get
			{
				return this.m_isRdlx;
			}
		}

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x000B17CF File Offset: 0x000AF9CF
		internal bool IsPackagedReportArchive
		{
			get
			{
				return this.m_isPackagedReportArchive;
			}
		}

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x000B17D7 File Offset: 0x000AF9D7
		internal bool IsRdlSandboxingEnabled
		{
			get
			{
				return this.Configuration != null && this.Configuration.RdlSandboxing != null;
			}
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x000B17F1 File Offset: 0x000AF9F1
		internal bool IsRestrictedDataRegionSort(bool isDataRowSort)
		{
			return isDataRowSort && this.m_publishingVersioning.IsRdlFeatureRestricted(RdlFeatures.Sort_DataRegion);
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x000B1804 File Offset: 0x000AFA04
		internal bool IsRestrictedGroupSort(bool isDataRowSort, Microsoft.ReportingServices.ReportIntermediateFormat.Sorting sorting)
		{
			return !sorting.NaturalSort && !sorting.DeferredSort && this.m_publishingVersioning.IsRdlFeatureRestricted(RdlFeatures.Sort_Group_Applied);
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x000B1824 File Offset: 0x000AFA24
		internal bool IsRestrictedNaturalGroupSort(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			return expressionInfo.Type != Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Field && this.m_publishingVersioning.IsRdlFeatureRestricted(RdlFeatures.SortGroupExpression_OnlySimpleField);
		}

		// Token: 0x04001575 RID: 5493
		private readonly bool m_isRdlx;

		// Token: 0x04001576 RID: 5494
		private readonly PublishingContextKind m_publishingContextKind;

		// Token: 0x04001577 RID: 5495
		private readonly ICatalogItemContext m_catalogContext;

		// Token: 0x04001578 RID: 5496
		private readonly IChunkFactory m_createChunkFactory;

		// Token: 0x04001579 RID: 5497
		private readonly AppDomain m_compilationTempAppDomain;

		// Token: 0x0400157A RID: 5498
		private readonly bool m_generateExpressionHostWithRefusedPermissions;

		// Token: 0x0400157B RID: 5499
		private ReportProcessingFlags m_processingFlags;

		// Token: 0x0400157C RID: 5500
		private readonly ReportProcessing.CheckSharedDataSource m_checkDataSourceCallback;

		// Token: 0x0400157D RID: 5501
		private readonly ReportProcessing.CheckSharedDataSet m_checkDataSetCallback;

		// Token: 0x0400157E RID: 5502
		private readonly ReportProcessing.ResolveTemporaryDataSource m_resolveTemporaryDataSourceCallback;

		// Token: 0x0400157F RID: 5503
		private readonly ReportProcessing.ResolveTemporaryDataSet m_resolveTemporaryDataSetCallback;

		// Token: 0x04001580 RID: 5504
		private readonly DataSourceInfoCollection m_originalDataSources;

		// Token: 0x04001581 RID: 5505
		private readonly DataSetInfoCollection m_originalDataSets;

		// Token: 0x04001582 RID: 5506
		private readonly IConfiguration m_configuration;

		// Token: 0x04001583 RID: 5507
		private readonly IDataProtection m_dataProtection;

		// Token: 0x04001584 RID: 5508
		private readonly bool m_isInternalRepublish;

		// Token: 0x04001585 RID: 5509
		private readonly bool m_traceAtomicScopes;

		// Token: 0x04001586 RID: 5510
		private readonly bool m_isPackagedReportArchive;

		// Token: 0x04001587 RID: 5511
		private readonly PublishingVersioning m_publishingVersioning;
	}
}
