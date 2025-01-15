using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000091 RID: 145
	internal sealed class DataShapeIntersectionInstance : BaseInstance, IReportScopeInstance
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x00026805 File Offset: 0x00024A05
		internal DataShapeIntersectionInstance(DataShapeIntersection dataShapeIntersection)
			: base(dataShapeIntersection)
		{
			this.m_dataShapeIntersection = dataShapeIntersection;
			this.m_isNewContext = true;
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0002681C File Offset: 0x00024A1C
		string IReportScopeInstance.UniqueName
		{
			get
			{
				return this.m_dataShapeIntersection.RifDataShapeIntersection.UniqueName;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0002682E File Offset: 0x00024A2E
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x00026836 File Offset: 0x00024A36
		bool IReportScopeInstance.IsNewContext
		{
			get
			{
				return this.m_isNewContext;
			}
			set
			{
				this.m_isNewContext = value;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0002683F File Offset: 0x00024A3F
		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00026847 File Offset: 0x00024A47
		internal override void SetNewContext()
		{
			if (this.m_isNewContext)
			{
				return;
			}
			this.m_isNewContext = true;
			base.SetNewContext();
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0002685F File Offset: 0x00024A5F
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00026864 File Offset: 0x00024A64
		internal void IncrementDataIntersectionLimitCounter()
		{
			if (this.m_dataShapeIntersection.Limit == null)
			{
				return;
			}
			IRIFReportDataScope irifreportDataScope = (IRIFReportDataScope)this.ReportScopeInstance.ReportScope.RIFReportScope;
			if (irifreportDataScope.CurrentStreamingScopeInstance == null)
			{
				return;
			}
			if (!irifreportDataScope.CurrentStreamingScopeInstance.Value().IsNoRows)
			{
				this.m_dataShapeIntersection.Limit.Increment();
			}
		}

		// Token: 0x04000253 RID: 595
		private readonly DataShapeIntersection m_dataShapeIntersection;

		// Token: 0x04000254 RID: 596
		private bool m_isNewContext;
	}
}
