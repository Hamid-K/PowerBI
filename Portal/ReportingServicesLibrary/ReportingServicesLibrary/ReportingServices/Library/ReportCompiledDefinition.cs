using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000128 RID: 296
	internal class ReportCompiledDefinition
	{
		// Token: 0x06000BE6 RID: 3046 RVA: 0x0002D2DD File Offset: 0x0002B4DD
		public static ReportCompiledDefinition Load(CatalogItemContext itemContext, RSService service, SecurityRequirements requirements)
		{
			return ReportCompiledDefinition.Load(itemContext, service, requirements, null);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002D2E8 File Offset: 0x0002B4E8
		public static ReportCompiledDefinition Load(CatalogItemContext itemContext, RSService service, SecurityRequirements requirements, ReportProcessing.NeedsUpgrade needsUpgradeCallback)
		{
			return ReportCompiledDefinition.Load(itemContext, service, requirements, needsUpgradeCallback, false);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002D2F4 File Offset: 0x0002B4F4
		public static ReportCompiledDefinition Load(CatalogItemContext itemContext, RSService service, SecurityRequirements requirements, ReportProcessing.NeedsUpgrade needsUpgradeCallback, bool useServiceConnectionForRepublishing)
		{
			return ReportCompiledDefinition.Load(itemContext, service, requirements, needsUpgradeCallback, useServiceConnectionForRepublishing, null);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002D304 File Offset: 0x0002B504
		public static ReportCompiledDefinition Load(CatalogItemContext itemContext, RSService service, SecurityRequirements requirements, ReportProcessing.NeedsUpgrade needsUpgradeCallback, bool useServiceConnectionForRepublishing, SubreportRetrieval.ParentReportContext parentContext)
		{
			DefinitionLoader definitionLoader = new DefinitionLoader(service, useServiceConnectionForRepublishing);
			if (needsUpgradeCallback != null)
			{
				definitionLoader.UpgradeCheckCallback = needsUpgradeCallback;
			}
			return definitionLoader.GetCompiledDefinition(itemContext, requirements, parentContext);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002D330 File Offset: 0x0002B530
		public ReportCompiledDefinition(CatalogItemContext itemContext, ReportSnapshot definitionSnapshot, Guid reportId, Guid linkId, ItemType itemType, Guid executionSnapshotId, string properties, string description, byte[] securityDescriptor, int executionOptions, bool isRdceReport)
		{
			this.m_itemContext = itemContext;
			this.m_definitionSnapshot = definitionSnapshot;
			this.m_reportId = reportId;
			this.m_linkId = linkId;
			this.m_itemType = itemType;
			this.m_executionSnapshotId = executionSnapshotId;
			this.m_properties = properties;
			this.m_description = description;
			this.m_secDesc = securityDescriptor;
			this.m_execOptions = executionOptions;
			this.m_isRdceReport = isRdceReport;
			RSService.EnsureItemTypeIsReportOrDataSet(this.m_itemType, this.m_itemContext.OriginalItemPath.Value);
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0002D3B3 File Offset: 0x0002B5B3
		public CatalogItemContext ItemContext
		{
			get
			{
				return this.m_itemContext;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0002D3BB File Offset: 0x0002B5BB
		public ReportSnapshot DefinitionSnapshot
		{
			get
			{
				return this.m_definitionSnapshot;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0002D3C3 File Offset: 0x0002B5C3
		public Guid ReportId
		{
			get
			{
				return this.m_reportId;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0002D3CB File Offset: 0x0002B5CB
		public Guid LinkId
		{
			get
			{
				return this.m_linkId;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0002D3D3 File Offset: 0x0002B5D3
		public Guid ExecutionSnapshotId
		{
			get
			{
				return this.m_executionSnapshotId;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0002D3DB File Offset: 0x0002B5DB
		public string Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0002D3E3 File Offset: 0x0002B5E3
		public string Description
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0002D3EB File Offset: 0x0002B5EB
		public byte[] SecurityDescriptor
		{
			get
			{
				return this.m_secDesc;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0002D3F3 File Offset: 0x0002B5F3
		public int ExecutionOptions
		{
			get
			{
				return this.m_execOptions;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0002D3FB File Offset: 0x0002B5FB
		public bool IsRdceReport
		{
			get
			{
				return this.m_isRdceReport;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0002D403 File Offset: 0x0002B603
		public ItemType Type
		{
			get
			{
				return this.m_itemType;
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002D40C File Offset: 0x0002B60C
		public BaseReportCatalogItem ConvertToCatalogItem(CatalogItemFactory catalogItemFactory)
		{
			BaseReportCatalogItem baseReportCatalogItem = catalogItemFactory.GetCatalogItem(this.ItemContext, this.ReportId, this.Type, this.SecurityDescriptor, this.LinkId) as BaseReportCatalogItem;
			RSTrace.CatalogTrace.Assert(baseReportCatalogItem != null, "reportItem");
			return baseReportCatalogItem;
		}

		// Token: 0x040004CC RID: 1228
		private readonly CatalogItemContext m_itemContext;

		// Token: 0x040004CD RID: 1229
		private readonly ReportSnapshot m_definitionSnapshot;

		// Token: 0x040004CE RID: 1230
		private readonly Guid m_reportId;

		// Token: 0x040004CF RID: 1231
		private readonly Guid m_linkId;

		// Token: 0x040004D0 RID: 1232
		private readonly ItemType m_itemType;

		// Token: 0x040004D1 RID: 1233
		private readonly Guid m_executionSnapshotId;

		// Token: 0x040004D2 RID: 1234
		private readonly string m_properties;

		// Token: 0x040004D3 RID: 1235
		private readonly string m_description;

		// Token: 0x040004D4 RID: 1236
		private readonly byte[] m_secDesc;

		// Token: 0x040004D5 RID: 1237
		private readonly int m_execOptions;

		// Token: 0x040004D6 RID: 1238
		private readonly bool m_isRdceReport;
	}
}
