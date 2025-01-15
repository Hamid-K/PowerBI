using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200012A RID: 298
	internal sealed class ItemParameterDefinition : ReportCompiledDefinition
	{
		// Token: 0x06000C0D RID: 3085 RVA: 0x0002D5D7 File Offset: 0x0002B7D7
		public static ItemParameterDefinition Load(CatalogItemContext itemContext, string historyId, bool forRendering, RSService service, SecurityRequirements requirements)
		{
			return new DefinitionLoader(service, false).GetParameterDefinition(itemContext, historyId, forRendering, requirements);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002D5EC File Offset: 0x0002B7EC
		public ItemParameterDefinition(CatalogItemContext itemContext, ReportSnapshot definitionSnapshot, Guid reportId, Guid linkId, ItemType itemType, ReportSnapshot snapshotData, string properties, string description, byte[] securityDescriptor, int executionOptions, DateTime snapshotExecutionDate, string storedParametersXml)
			: base(itemContext, definitionSnapshot, reportId, linkId, itemType, (snapshotData != null) ? snapshotData.SnapshotDataID : Guid.Empty, properties, description, securityDescriptor, executionOptions, false)
		{
			this.m_snapshotData = snapshotData;
			this.m_snapshotExecutionDate = snapshotExecutionDate;
			this.m_storedParametersXml = storedParametersXml;
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0002D639 File Offset: 0x0002B839
		public string StoredParametersXml
		{
			get
			{
				return this.m_storedParametersXml;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0002D641 File Offset: 0x0002B841
		public DateTime SnapshotExecutionDate
		{
			get
			{
				return this.m_snapshotExecutionDate;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0002D649 File Offset: 0x0002B849
		public ReportSnapshot SnapshotData
		{
			get
			{
				return this.m_snapshotData;
			}
		}

		// Token: 0x040004E3 RID: 1251
		private readonly ReportSnapshot m_snapshotData;

		// Token: 0x040004E4 RID: 1252
		private DateTime m_snapshotExecutionDate;

		// Token: 0x040004E5 RID: 1253
		private readonly string m_storedParametersXml;
	}
}
