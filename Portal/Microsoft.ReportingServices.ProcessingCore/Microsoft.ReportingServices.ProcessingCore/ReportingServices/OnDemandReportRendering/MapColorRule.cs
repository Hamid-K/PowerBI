using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001C3 RID: 451
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapColorRule : MapAppearanceRule
	{
		// Token: 0x0600119C RID: 4508 RVA: 0x000492D4 File Offset: 0x000474D4
		internal MapColorRule(MapColorRule defObject, MapVectorLayer mapVectorLayer, Map map)
			: base(defObject, mapVectorLayer, map)
		{
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x000492DF File Offset: 0x000474DF
		public ReportBoolProperty ShowInColorScale
		{
			get
			{
				if (this.m_showInColorScale == null && this.MapColorRuleDef.ShowInColorScale != null)
				{
					this.m_showInColorScale = new ReportBoolProperty(this.MapColorRuleDef.ShowInColorScale);
				}
				return this.m_showInColorScale;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x00049312 File Offset: 0x00047512
		internal MapColorRule MapColorRuleDef
		{
			get
			{
				return (MapColorRule)base.MapAppearanceRuleDef;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x0004931F File Offset: 0x0004751F
		internal new MapColorRuleInstance Instance
		{
			get
			{
				return (MapColorRuleInstance)this.GetInstance();
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0004932C File Offset: 0x0004752C
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000857 RID: 2135
		private ReportBoolProperty m_showInColorScale;
	}
}
