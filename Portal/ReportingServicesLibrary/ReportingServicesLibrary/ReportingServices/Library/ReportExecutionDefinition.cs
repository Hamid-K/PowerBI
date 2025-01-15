using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000129 RID: 297
	internal sealed class ReportExecutionDefinition : ReportCompiledDefinition
	{
		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002D457 File Offset: 0x0002B657
		public static ReportExecutionDefinition Load(CatalogItemContext itemContext, ParameterInfoCollection queryParameters, ClientRequest userRequest, RSService service, SecurityRequirements requirements)
		{
			return new DefinitionLoader(service, false).GetExecutionDefinition(itemContext, userRequest, queryParameters, requirements);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0002D46A File Offset: 0x0002B66A
		public static ReportExecutionDefinition LoadHistorySnapshot(CatalogItemContext itemContext, ClientRequest userRequest, RSService service, SecurityRequirements requirements)
		{
			return new DefinitionLoader(service, false).GetHistorySnapshot(itemContext, userRequest, requirements);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002D47C File Offset: 0x0002B67C
		public ReportExecutionDefinition(CatalogItemContext itemContext, ReportSnapshot definitionSnapshot, Guid reportId, Guid linkId, ReportSnapshot snapshotData, ItemType itemType, ExternalItemPath reportDefinitionPath, string properties, string description, byte[] securityDescriptor, int executionOptions, bool hasData, bool foundInCache, bool cachingRequested, bool isRdceReport, DateTime executionDateTime, DateTime expirationDateTime)
			: base(itemContext, definitionSnapshot, reportId, linkId, itemType, (snapshotData != null) ? snapshotData.SnapshotDataID : Guid.Empty, properties, description, securityDescriptor, executionOptions, isRdceReport)
		{
			this.m_hasData = hasData;
			this.m_foundInCache = foundInCache;
			this.m_reportDefinitionPath = reportDefinitionPath;
			this.m_cachingRequested = cachingRequested;
			this.m_executionDateTime = executionDateTime;
			this.m_expirationDateTime = expirationDateTime;
			this.m_snapshotData = snapshotData;
			this.m_processedProperties = new ItemProperties(base.Properties);
			this.m_userDependencies = this.m_processedProperties.DependsOnUser;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002D50C File Offset: 0x0002B70C
		public void ResolveLinkedProperties(RSService service)
		{
			if (base.Type == ItemType.LinkedReport)
			{
				LinkedReportProperyResolver linkedReportProperyResolver = new LinkedReportProperyResolver(this.ReportDefinitionPath, service);
				linkedReportProperyResolver.Resolve();
				this.m_userDependencies = linkedReportProperyResolver.DependsOnUser;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0002D541 File Offset: 0x0002B741
		public bool HasData
		{
			get
			{
				return this.m_hasData;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0002D549 File Offset: 0x0002B749
		public bool FoundInCache
		{
			get
			{
				return this.m_foundInCache;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0002D551 File Offset: 0x0002B751
		public bool CachingRequested
		{
			get
			{
				return this.m_cachingRequested;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0002D559 File Offset: 0x0002B759
		public ReportSnapshot SnapshotData
		{
			get
			{
				return this.m_snapshotData;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0002D561 File Offset: 0x0002B761
		public ItemProperties ReportProperties
		{
			get
			{
				return this.m_processedProperties;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0002D569 File Offset: 0x0002B769
		public ExternalItemPath ReportDefinitionPath
		{
			get
			{
				return this.m_reportDefinitionPath;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0002D571 File Offset: 0x0002B771
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x0002D579 File Offset: 0x0002B779
		public DataSourceInfoCollection DataSources
		{
			get
			{
				return this.m_dataSources;
			}
			set
			{
				this.m_dataSources = value;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0002D582 File Offset: 0x0002B782
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x0002D58A File Offset: 0x0002B78A
		public DataSetInfoCollection DataSets
		{
			get
			{
				return this.m_dataSets;
			}
			set
			{
				this.m_dataSets = value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0002D593 File Offset: 0x0002B793
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x0002D59B File Offset: 0x0002B79B
		public ReportPaginationData PaginationData
		{
			get
			{
				return this.m_paginationData;
			}
			set
			{
				this.m_paginationData = value;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0002D5A4 File Offset: 0x0002B7A4
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x0002D5AC File Offset: 0x0002B7AC
		public UserProfileState UserDependencies
		{
			get
			{
				return this.m_userDependencies;
			}
			set
			{
				this.m_userDependencies = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0002D5B5 File Offset: 0x0002B7B5
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x0002D5BD File Offset: 0x0002B7BD
		public DateTime ExecutionDateTime
		{
			get
			{
				return this.m_executionDateTime;
			}
			set
			{
				this.m_executionDateTime = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0002D5C6 File Offset: 0x0002B7C6
		// (set) Token: 0x06000C0C RID: 3084 RVA: 0x0002D5CE File Offset: 0x0002B7CE
		public DateTime ExpirationDateTime
		{
			get
			{
				return this.m_expirationDateTime;
			}
			set
			{
				this.m_expirationDateTime = value;
			}
		}

		// Token: 0x040004D7 RID: 1239
		private readonly bool m_hasData;

		// Token: 0x040004D8 RID: 1240
		private readonly bool m_foundInCache;

		// Token: 0x040004D9 RID: 1241
		private readonly bool m_cachingRequested;

		// Token: 0x040004DA RID: 1242
		private readonly ReportSnapshot m_snapshotData;

		// Token: 0x040004DB RID: 1243
		private readonly ItemProperties m_processedProperties;

		// Token: 0x040004DC RID: 1244
		private readonly ExternalItemPath m_reportDefinitionPath;

		// Token: 0x040004DD RID: 1245
		private DataSourceInfoCollection m_dataSources;

		// Token: 0x040004DE RID: 1246
		private DataSetInfoCollection m_dataSets;

		// Token: 0x040004DF RID: 1247
		private ReportPaginationData m_paginationData;

		// Token: 0x040004E0 RID: 1248
		private UserProfileState m_userDependencies;

		// Token: 0x040004E1 RID: 1249
		private DateTime m_executionDateTime;

		// Token: 0x040004E2 RID: 1250
		private DateTime m_expirationDateTime;
	}
}
