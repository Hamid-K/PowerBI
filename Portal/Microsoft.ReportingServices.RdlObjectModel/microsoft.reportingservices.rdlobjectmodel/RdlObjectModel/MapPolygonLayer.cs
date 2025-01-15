using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000189 RID: 393
	public class MapPolygonLayer : MapVectorLayer
	{
		// Token: 0x06000C8E RID: 3214 RVA: 0x00021488 File Offset: 0x0001F688
		public MapPolygonLayer()
		{
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00021490 File Offset: 0x0001F690
		internal MapPolygonLayer(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x00021499 File Offset: 0x0001F699
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x000214AD File Offset: 0x0001F6AD
		public MapPolygonTemplate MapPolygonTemplate
		{
			get
			{
				return (MapPolygonTemplate)base.PropertyStore.GetObject(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x000214BD File Offset: 0x0001F6BD
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x000214D1 File Offset: 0x0001F6D1
		public MapPolygonRules MapPolygonRules
		{
			get
			{
				return (MapPolygonRules)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x000214E1 File Offset: 0x0001F6E1
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x000214F5 File Offset: 0x0001F6F5
		public MapPointTemplate MapCenterPointTemplate
		{
			get
			{
				return (MapPointTemplate)base.PropertyStore.GetObject(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x00021505 File Offset: 0x0001F705
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x00021519 File Offset: 0x0001F719
		public MapPointRules MapCenterPointRules
		{
			get
			{
				return (MapPointRules)base.PropertyStore.GetObject(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x00021529 File Offset: 0x0001F729
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x0002153D File Offset: 0x0001F73D
		[XmlElement(typeof(RdlCollection<MapPolygon>))]
		public IList<MapPolygon> MapPolygons
		{
			get
			{
				return (IList<MapPolygon>)base.PropertyStore.GetObject(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0002154D File Offset: 0x0001F74D
		public override void Initialize()
		{
			base.Initialize();
			this.MapPolygons = new RdlCollection<MapPolygon>();
		}

		// Token: 0x020003B7 RID: 951
		internal new class Definition : DefinitionStore<MapPolygonLayer, MapPolygonLayer.Definition.Properties>
		{
			// Token: 0x0600185B RID: 6235 RVA: 0x0003B659 File Offset: 0x00039859
			private Definition()
			{
			}

			// Token: 0x020004CF RID: 1231
			internal enum Properties
			{
				// Token: 0x04000EF3 RID: 3827
				Name,
				// Token: 0x04000EF4 RID: 3828
				VisibilityMode,
				// Token: 0x04000EF5 RID: 3829
				MinimumZoom,
				// Token: 0x04000EF6 RID: 3830
				MaximumZoom,
				// Token: 0x04000EF7 RID: 3831
				Transparency,
				// Token: 0x04000EF8 RID: 3832
				MapDataRegionName,
				// Token: 0x04000EF9 RID: 3833
				MapBindingFieldPairs,
				// Token: 0x04000EFA RID: 3834
				MapFieldDefinitions,
				// Token: 0x04000EFB RID: 3835
				MapSpatialData,
				// Token: 0x04000EFC RID: 3836
				DataElementName,
				// Token: 0x04000EFD RID: 3837
				DataElementOutput,
				// Token: 0x04000EFE RID: 3838
				MapPolygonTemplate,
				// Token: 0x04000EFF RID: 3839
				MapPolygonRules,
				// Token: 0x04000F00 RID: 3840
				MapCenterPointTemplate,
				// Token: 0x04000F01 RID: 3841
				MapCenterPointRules,
				// Token: 0x04000F02 RID: 3842
				MapPolygons,
				// Token: 0x04000F03 RID: 3843
				PropertyCount
			}
		}
	}
}
