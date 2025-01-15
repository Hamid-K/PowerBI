using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000303 RID: 771
	public sealed class ReportInstance : BaseInstance, IReportScopeInstance
	{
		// Token: 0x06001C3F RID: 7231 RVA: 0x00070869 File Offset: 0x0006EA69
		internal ReportInstance(Report reportDef, ReportInstance reportInstance)
			: base(null)
		{
			this.m_reportDef = reportDef;
			this.m_reportInstance = reportInstance;
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x00070887 File Offset: 0x0006EA87
		internal ReportInstance(Report reportDef)
			: base(null)
		{
			this.m_reportDef = reportDef;
			this.m_reportInstance = null;
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x000708A5 File Offset: 0x0006EAA5
		// (set) Token: 0x06001C42 RID: 7234 RVA: 0x000708AD File Offset: 0x0006EAAD
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

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x000708B6 File Offset: 0x0006EAB6
		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x000708BE File Offset: 0x0006EABE
		public string UniqueName
		{
			get
			{
				if (this.m_reportDef.IsOldSnapshot)
				{
					return this.m_reportDef.RenderReport.UniqueName;
				}
				return InstancePathItem.GenerateInstancePathString(this.m_reportDef.ReportDef.InstancePath) + "xA";
			}
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x000708FD File Offset: 0x0006EAFD
		public string Language
		{
			get
			{
				if (this.m_language == null)
				{
					if (this.m_reportDef.IsOldSnapshot)
					{
						return this.m_reportDef.RenderReport.ReportLanguage;
					}
					this.m_language = this.m_reportInstance.Language;
				}
				return this.m_language;
			}
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x0007093C File Offset: 0x0006EB3C
		public int AutoRefresh
		{
			get
			{
				if (this.m_reportDef.IsOldSnapshot)
				{
					return this.m_reportDef.RenderReport.AutoRefresh;
				}
				return this.m_reportDef.ReportDef.EvaluateAutoRefresh(this, this.m_reportDef.RenderingContext.OdpContext);
			}
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x00070988 File Offset: 0x0006EB88
		public string InitialPageName
		{
			get
			{
				if (this.m_reportDef.IsOldSnapshot)
				{
					return null;
				}
				ExpressionInfo initialPageName = this.m_reportDef.ReportDef.InitialPageName;
				if (initialPageName != null)
				{
					if (!initialPageName.IsExpression)
					{
						this.m_initialPageName = initialPageName.StringValue;
					}
					else
					{
						this.m_initialPageName = this.m_reportDef.ReportDef.EvaluateInitialPageName(this, this.m_reportDef.RenderingContext.OdpContext);
					}
				}
				return this.m_initialPageName;
			}
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x000709FB File Offset: 0x0006EBFB
		internal Report ReportDef
		{
			get
			{
				return this.m_reportDef;
			}
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x00070A03 File Offset: 0x0006EC03
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00070A05 File Offset: 0x0006EC05
		public void ResetContext()
		{
			this.m_reportDef.SetNewContext();
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00070A12 File Offset: 0x0006EC12
		internal override void SetNewContext()
		{
			base.SetNewContext();
			this.m_isNewContext = true;
		}

		// Token: 0x04000EE2 RID: 3810
		private Report m_reportDef;

		// Token: 0x04000EE3 RID: 3811
		private ReportInstance m_reportInstance;

		// Token: 0x04000EE4 RID: 3812
		private string m_language;

		// Token: 0x04000EE5 RID: 3813
		private bool m_isNewContext = true;

		// Token: 0x04000EE6 RID: 3814
		private string m_initialPageName;
	}
}
