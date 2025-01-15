using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001E9 RID: 489
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapPointTemplateInstance : MapSpatialElementTemplateInstance
	{
		// Token: 0x0600128D RID: 4749 RVA: 0x0004BBEE File Offset: 0x00049DEE
		internal MapPointTemplateInstance(MapPointTemplate defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x0004BC00 File Offset: 0x00049E00
		public ReportSize Size
		{
			get
			{
				if (this.m_size == null)
				{
					this.m_size = new ReportSize(((MapPointTemplate)this.m_defObject.MapSpatialElementTemplateDef).EvaluateSize(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_size;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x0004BC58 File Offset: 0x00049E58
		public MapPointLabelPlacement LabelPlacement
		{
			get
			{
				if (this.m_labelPlacement == null)
				{
					this.m_labelPlacement = new MapPointLabelPlacement?(((MapPointTemplate)this.m_defObject.MapSpatialElementTemplateDef).EvaluateLabelPlacement(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_labelPlacement.Value;
			}
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0004BCB8 File Offset: 0x00049EB8
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_size = null;
			this.m_labelPlacement = null;
		}

		// Token: 0x040008DA RID: 2266
		private MapPointTemplate m_defObject;

		// Token: 0x040008DB RID: 2267
		private ReportSize m_size;

		// Token: 0x040008DC RID: 2268
		private MapPointLabelPlacement? m_labelPlacement;
	}
}
