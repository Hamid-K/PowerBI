using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001DF RID: 479
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapPointTemplate : MapSpatialElementTemplate
	{
		// Token: 0x0600124A RID: 4682 RVA: 0x0004AFF6 File Offset: 0x000491F6
		internal MapPointTemplate(MapPointTemplate defObject, MapVectorLayer mapVectorLayer, Map map)
			: base(defObject, mapVectorLayer, map)
		{
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x0004B001 File Offset: 0x00049201
		public ReportSizeProperty Size
		{
			get
			{
				if (this.m_size == null && this.MapPointTemplateDef.Size != null)
				{
					this.m_size = new ReportSizeProperty(this.MapPointTemplateDef.Size);
				}
				return this.m_size;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0004B034 File Offset: 0x00049234
		public ReportEnumProperty<MapPointLabelPlacement> LabelPlacement
		{
			get
			{
				if (this.m_labelPlacement == null && this.MapPointTemplateDef.LabelPlacement != null)
				{
					this.m_labelPlacement = new ReportEnumProperty<MapPointLabelPlacement>(this.MapPointTemplateDef.LabelPlacement.IsExpression, this.MapPointTemplateDef.LabelPlacement.OriginalText, EnumTranslator.TranslateMapPointLabelPlacement(this.MapPointTemplateDef.LabelPlacement.StringValue, null));
				}
				return this.m_labelPlacement;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x0004B09D File Offset: 0x0004929D
		internal MapPointTemplate MapPointTemplateDef
		{
			get
			{
				return (MapPointTemplate)base.MapSpatialElementTemplateDef;
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x0004B0AA File Offset: 0x000492AA
		internal new MapPointTemplateInstance Instance
		{
			get
			{
				return (MapPointTemplateInstance)this.GetInstance();
			}
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0004B0B7 File Offset: 0x000492B7
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x040008B5 RID: 2229
		private ReportSizeProperty m_size;

		// Token: 0x040008B6 RID: 2230
		private ReportEnumProperty<MapPointLabelPlacement> m_labelPlacement;
	}
}
