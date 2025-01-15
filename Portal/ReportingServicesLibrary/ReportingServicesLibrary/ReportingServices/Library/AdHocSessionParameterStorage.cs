using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000094 RID: 148
	internal sealed class AdHocSessionParameterStorage : IStoredParameterSource
	{
		// Token: 0x06000617 RID: 1559 RVA: 0x0001981C File Offset: 0x00017A1C
		public AdHocSessionParameterStorage(SessionReportItem sessionItem)
		{
			RSTrace.CatalogTrace.Assert(sessionItem != null, "sessionItem");
			RSTrace.CatalogTrace.Assert(sessionItem.Report != null, "sessionItem.Report");
			this.m_sessionItem = sessionItem;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00019856 File Offset: 0x00017A56
		public ParameterInfoCollection RetrieveParameters(ReportProcessing reportProcessing)
		{
			return this.m_sessionItem.Report.EffectiveParams;
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00019868 File Offset: 0x00017A68
		public ReportSnapshot CompiledParameterSource
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_sessionItem.Report.CompiledDefinition;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00005BEF File Offset: 0x00003DEF
		public bool IsSnapshotExecution
		{
			[DebuggerStepThrough]
			get
			{
				return false;
			}
		}

		// Token: 0x0400033B RID: 827
		private readonly SessionReportItem m_sessionItem;
	}
}
