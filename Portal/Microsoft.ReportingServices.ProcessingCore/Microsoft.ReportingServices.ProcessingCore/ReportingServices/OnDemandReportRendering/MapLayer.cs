using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001A7 RID: 423
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapLayer : MapObjectCollectionItem
	{
		// Token: 0x060010EA RID: 4330 RVA: 0x00047741 File Offset: 0x00045941
		internal MapLayer(MapLayer defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x00047757 File Offset: 0x00045957
		public string Name
		{
			get
			{
				return this.m_defObject.Name;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x00047764 File Offset: 0x00045964
		public ReportEnumProperty<MapVisibilityMode> VisibilityMode
		{
			get
			{
				if (this.m_visibilityMode == null && this.m_defObject.VisibilityMode != null)
				{
					this.m_visibilityMode = new ReportEnumProperty<MapVisibilityMode>(this.m_defObject.VisibilityMode.IsExpression, this.m_defObject.VisibilityMode.OriginalText, EnumTranslator.TranslateMapVisibilityMode(this.m_defObject.VisibilityMode.StringValue, null));
				}
				return this.m_visibilityMode;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x000477CD File Offset: 0x000459CD
		public ReportDoubleProperty MinimumZoom
		{
			get
			{
				if (this.m_minimumZoom == null && this.m_defObject.MinimumZoom != null)
				{
					this.m_minimumZoom = new ReportDoubleProperty(this.m_defObject.MinimumZoom);
				}
				return this.m_minimumZoom;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x00047800 File Offset: 0x00045A00
		public ReportDoubleProperty MaximumZoom
		{
			get
			{
				if (this.m_maximumZoom == null && this.m_defObject.MaximumZoom != null)
				{
					this.m_maximumZoom = new ReportDoubleProperty(this.m_defObject.MaximumZoom);
				}
				return this.m_maximumZoom;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x00047833 File Offset: 0x00045A33
		public ReportDoubleProperty Transparency
		{
			get
			{
				if (this.m_transparency == null && this.m_defObject.Transparency != null)
				{
					this.m_transparency = new ReportDoubleProperty(this.m_defObject.Transparency);
				}
				return this.m_transparency;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x00047866 File Offset: 0x00045A66
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0004786E File Offset: 0x00045A6E
		internal MapLayer MapLayerDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x00047876 File Offset: 0x00045A76
		internal MapLayerInstance Instance
		{
			get
			{
				return this.GetInstance();
			}
		}

		// Token: 0x060010F3 RID: 4339
		internal abstract MapLayerInstance GetInstance();

		// Token: 0x060010F4 RID: 4340 RVA: 0x0004787E File Offset: 0x00045A7E
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000803 RID: 2051
		protected Map m_map;

		// Token: 0x04000804 RID: 2052
		private MapLayer m_defObject;

		// Token: 0x04000805 RID: 2053
		private ReportEnumProperty<MapVisibilityMode> m_visibilityMode;

		// Token: 0x04000806 RID: 2054
		private ReportDoubleProperty m_minimumZoom;

		// Token: 0x04000807 RID: 2055
		private ReportDoubleProperty m_maximumZoom;

		// Token: 0x04000808 RID: 2056
		private ReportDoubleProperty m_transparency;
	}
}
