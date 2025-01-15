using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000102 RID: 258
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class GaugeInstance : GaugePanelItemInstance
	{
		// Token: 0x06000B79 RID: 2937 RVA: 0x00032D62 File Offset: 0x00030F62
		internal GaugeInstance(Gauge defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00032D74 File Offset: 0x00030F74
		public bool ClipContent
		{
			get
			{
				if (this.m_clipContent == null)
				{
					this.m_clipContent = new bool?(((Gauge)this.m_defObject.GaugePanelItemDef).EvaluateClipContent(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_clipContent.Value;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00032DD4 File Offset: 0x00030FD4
		public double AspectRatio
		{
			get
			{
				if (this.m_aspectRatio == null)
				{
					this.m_aspectRatio = new double?(((Gauge)this.m_defObject.GaugePanelItemDef).EvaluateAspectRatio(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_aspectRatio.Value;
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00032E35 File Offset: 0x00031035
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_clipContent = null;
			this.m_aspectRatio = null;
		}

		// Token: 0x040004E1 RID: 1249
		private Gauge m_defObject;

		// Token: 0x040004E2 RID: 1250
		private bool? m_clipContent;

		// Token: 0x040004E3 RID: 1251
		private double? m_aspectRatio;
	}
}
