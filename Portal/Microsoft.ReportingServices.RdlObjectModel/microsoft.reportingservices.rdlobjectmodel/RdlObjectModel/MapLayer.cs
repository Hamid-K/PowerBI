using System;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000186 RID: 390
	[XmlElementClass("MapTileLayer", typeof(MapTileLayer))]
	[XmlElementClass("MapPolygonLayer", typeof(MapPolygonLayer))]
	[XmlElementClass("MapPointLayer", typeof(MapPointLayer))]
	[XmlElementClass("MapLineLayer", typeof(MapLineLayer))]
	public abstract class MapLayer : ReportObject, INamedObject
	{
		// Token: 0x06000C68 RID: 3176 RVA: 0x000211B4 File Offset: 0x0001F3B4
		public MapLayer()
		{
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x000211BC File Offset: 0x0001F3BC
		internal MapLayer(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x000211C5 File Offset: 0x0001F3C5
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x000211D3 File Offset: 0x0001F3D3
		[XmlAttribute(typeof(string))]
		public string Name
		{
			get
			{
				return base.PropertyStore.GetObject<string>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x000211E2 File Offset: 0x0001F3E2
		// (set) Token: 0x06000C6D RID: 3181 RVA: 0x000211F0 File Offset: 0x0001F3F0
		[ReportExpressionDefaultValue(typeof(MapVisibilityModes), MapVisibilityModes.Visible)]
		public ReportExpression<MapVisibilityModes> VisibilityMode
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapVisibilityModes>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00021204 File Offset: 0x0001F404
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x00021212 File Offset: 0x0001F412
		[ReportExpressionDefaultValue(typeof(double), "50")]
		public ReportExpression<double> MinimumZoom
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00021226 File Offset: 0x0001F426
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x00021234 File Offset: 0x0001F434
		[ReportExpressionDefaultValue(typeof(double), "200")]
		public ReportExpression<double> MaximumZoom
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x00021248 File Offset: 0x0001F448
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x00021256 File Offset: 0x0001F456
		[ReportExpressionDefaultValue(typeof(double), "0")]
		public ReportExpression<double> Transparency
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0002126C File Offset: 0x0001F46C
		public override void Initialize()
		{
			base.Initialize();
			this.VisibilityMode = MapVisibilityModes.Visible;
			this.MinimumZoom = 50.0;
			this.MaximumZoom = 200.0;
			this.Transparency = 0.0;
		}

		// Token: 0x020003B4 RID: 948
		internal class Definition : DefinitionStore<MapLayer, MapLayer.Definition.Properties>
		{
			// Token: 0x06001858 RID: 6232 RVA: 0x0003B641 File Offset: 0x00039841
			private Definition()
			{
			}

			// Token: 0x020004CC RID: 1228
			internal enum Properties
			{
				// Token: 0x04000ED7 RID: 3799
				Name,
				// Token: 0x04000ED8 RID: 3800
				VisibilityMode,
				// Token: 0x04000ED9 RID: 3801
				MinimumZoom,
				// Token: 0x04000EDA RID: 3802
				MaximumZoom,
				// Token: 0x04000EDB RID: 3803
				Transparency
			}
		}
	}
}
