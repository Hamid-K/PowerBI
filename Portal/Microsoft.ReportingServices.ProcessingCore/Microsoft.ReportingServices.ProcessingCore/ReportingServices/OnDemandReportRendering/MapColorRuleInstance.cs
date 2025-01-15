using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001D1 RID: 465
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapColorRuleInstance : MapAppearanceRuleInstance
	{
		// Token: 0x06001201 RID: 4609 RVA: 0x0004A507 File Offset: 0x00048707
		internal MapColorRuleInstance(MapColorRule defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x0004A518 File Offset: 0x00048718
		public bool ShowInColorScale
		{
			get
			{
				if (this.m_showInColorScale == null)
				{
					this.m_showInColorScale = new bool?(((MapColorRule)this.m_defObject.MapAppearanceRuleDef).EvaluateShowInColorScale(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_showInColorScale.Value;
			}
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0004A578 File Offset: 0x00048778
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_showInColorScale = null;
		}

		// Token: 0x0400088E RID: 2190
		private MapColorRule m_defObject;

		// Token: 0x0400088F RID: 2191
		private bool? m_showInColorScale;
	}
}
