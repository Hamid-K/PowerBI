using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001E5 RID: 485
	public sealed class MapLineTemplateInstance : MapSpatialElementTemplateInstance
	{
		// Token: 0x06001277 RID: 4727 RVA: 0x0004B5C8 File Offset: 0x000497C8
		internal MapLineTemplateInstance(MapLineTemplate defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x0004B5D8 File Offset: 0x000497D8
		public ReportSize Width
		{
			get
			{
				if (this.m_width == null)
				{
					this.m_width = new ReportSize(((MapLineTemplate)this.m_defObject.MapSpatialElementTemplateDef).EvaluateWidth(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_width;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x0004B630 File Offset: 0x00049830
		public MapLineLabelPlacement LabelPlacement
		{
			get
			{
				if (this.m_labelPlacement == null)
				{
					this.m_labelPlacement = new MapLineLabelPlacement?(((MapLineTemplate)this.m_defObject.MapSpatialElementTemplateDef).EvaluateLabelPlacement(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_labelPlacement.Value;
			}
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0004B690 File Offset: 0x00049890
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_width = null;
			this.m_labelPlacement = null;
		}

		// Token: 0x040008C5 RID: 2245
		private MapLineTemplate m_defObject;

		// Token: 0x040008C6 RID: 2246
		private ReportSize m_width;

		// Token: 0x040008C7 RID: 2247
		private MapLineLabelPlacement? m_labelPlacement;
	}
}
