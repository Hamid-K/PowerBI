using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000090 RID: 144
	internal sealed class LinkedReportProperyResolver
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x00018828 File Offset: 0x00016A28
		public LinkedReportProperyResolver(ExternalItemPath sourceReportInternalPath, RSService catalogService)
		{
			RSTrace.CatalogTrace.Assert(!ItemPathBase.IsNullOrEmpty(sourceReportInternalPath));
			RSTrace.CatalogTrace.Assert(catalogService != null, "catalogService != null");
			this.m_sourceReportInternalPath = sourceReportInternalPath;
			this.m_catalogService = catalogService;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00018876 File Offset: 0x00016A76
		public void Resolve()
		{
			if (!this.m_resolved)
			{
				this.GetReferencedReportProfileState();
				this.m_resolved = true;
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00018890 File Offset: 0x00016A90
		private void GetReferencedReportProfileState()
		{
			RSTrace.CatalogTrace.Assert(this.m_catalogService != null);
			CatalogItemContext catalogItemContext = new CatalogItemContext(this.m_catalogService);
			catalogItemContext.SetPath(this.m_sourceReportInternalPath, ItemPathOptions.None);
			RSReportContext rsreportContext = new RSReportContext(this.m_catalogService, catalogItemContext);
			rsreportContext.RetrieveProperties();
			this.m_dependsOnUser = rsreportContext.Properties.DependsOnUser;
			this.m_rdce = rsreportContext.Properties.Rdce;
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x000188FF File Offset: 0x00016AFF
		public UserProfileState DependsOnUser
		{
			get
			{
				RSTrace.CatalogTrace.Assert(this.m_resolved, "m_resolved");
				return this.m_dependsOnUser;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0001891C File Offset: 0x00016B1C
		public string Rdce
		{
			get
			{
				RSTrace.CatalogTrace.Assert(this.m_resolved, "m_resolved");
				return this.m_rdce;
			}
		}

		// Token: 0x04000325 RID: 805
		private readonly ExternalItemPath m_sourceReportInternalPath;

		// Token: 0x04000326 RID: 806
		private readonly RSService m_catalogService;

		// Token: 0x04000327 RID: 807
		private bool m_resolved;

		// Token: 0x04000328 RID: 808
		private UserProfileState m_dependsOnUser = UserProfileState.Both;

		// Token: 0x04000329 RID: 809
		private string m_rdce;
	}
}
