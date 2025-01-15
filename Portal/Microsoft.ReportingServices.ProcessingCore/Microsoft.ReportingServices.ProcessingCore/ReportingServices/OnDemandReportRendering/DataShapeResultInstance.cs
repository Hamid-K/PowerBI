using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200009E RID: 158
	internal sealed class DataShapeResultInstance : BaseInstance, IReportScopeInstance
	{
		// Token: 0x06000975 RID: 2421 RVA: 0x0002739E File Offset: 0x0002559E
		internal DataShapeResultInstance(DataShapeResult dataShapeResult)
			: base(null)
		{
			this.m_dataShapeResult = dataShapeResult;
			this.m_isNewContext = true;
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x000273B5 File Offset: 0x000255B5
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x000273BD File Offset: 0x000255BD
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

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x000273C6 File Offset: 0x000255C6
		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x000273CE File Offset: 0x000255CE
		public string UniqueName
		{
			get
			{
				return InstancePathItem.GenerateInstancePathString(this.m_dataShapeResult.RifReportDefinition.InstancePath) + "xA";
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x000273EF File Offset: 0x000255EF
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x000273F1 File Offset: 0x000255F1
		public void ResetContext()
		{
			this.m_dataShapeResult.SetNewContext();
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x000273FE File Offset: 0x000255FE
		internal override void SetNewContext()
		{
			base.SetNewContext();
			this.m_isNewContext = true;
		}

		// Token: 0x04000275 RID: 629
		private readonly DataShapeResult m_dataShapeResult;

		// Token: 0x04000276 RID: 630
		private bool m_isNewContext;
	}
}
