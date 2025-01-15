using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001DB RID: 475
	public sealed class MapLineTemplate : MapSpatialElementTemplate
	{
		// Token: 0x06001220 RID: 4640 RVA: 0x0004A992 File Offset: 0x00048B92
		internal MapLineTemplate(MapLineTemplate defObject, MapLineLayer mapLineLayer, Map map)
			: base(defObject, mapLineLayer, map)
		{
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x0004A99D File Offset: 0x00048B9D
		public ReportSizeProperty Width
		{
			get
			{
				if (this.m_width == null && this.MapLineTemplateDef.Width != null)
				{
					this.m_width = new ReportSizeProperty(this.MapLineTemplateDef.Width);
				}
				return this.m_width;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x0004A9D0 File Offset: 0x00048BD0
		public ReportEnumProperty<MapLineLabelPlacement> LabelPlacement
		{
			get
			{
				if (this.m_labelPlacement == null && this.MapLineTemplateDef.LabelPlacement != null)
				{
					this.m_labelPlacement = new ReportEnumProperty<MapLineLabelPlacement>(this.MapLineTemplateDef.LabelPlacement.IsExpression, this.MapLineTemplateDef.LabelPlacement.OriginalText, EnumTranslator.TranslateMapLineLabelPlacement(this.MapLineTemplateDef.LabelPlacement.StringValue, null));
				}
				return this.m_labelPlacement;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x0004AA39 File Offset: 0x00048C39
		internal MapLineTemplate MapLineTemplateDef
		{
			get
			{
				return (MapLineTemplate)base.MapSpatialElementTemplateDef;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x0004AA46 File Offset: 0x00048C46
		public new MapLineTemplateInstance Instance
		{
			get
			{
				return (MapLineTemplateInstance)this.GetInstance();
			}
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0004AA53 File Offset: 0x00048C53
		internal override MapSpatialElementTemplateInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapLineTemplateInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0004AA83 File Offset: 0x00048C83
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x040008A1 RID: 2209
		private ReportSizeProperty m_width;

		// Token: 0x040008A2 RID: 2210
		private ReportEnumProperty<MapLineLabelPlacement> m_labelPlacement;
	}
}
