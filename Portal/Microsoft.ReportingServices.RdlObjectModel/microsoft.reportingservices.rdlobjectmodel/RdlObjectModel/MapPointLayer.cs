using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200018A RID: 394
	public class MapPointLayer : MapVectorLayer
	{
		// Token: 0x06000C9B RID: 3227 RVA: 0x00021560 File Offset: 0x0001F760
		public MapPointLayer()
		{
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00021568 File Offset: 0x0001F768
		internal MapPointLayer(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x00021571 File Offset: 0x0001F771
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x00021585 File Offset: 0x0001F785
		public MapPointTemplate MapPointTemplate
		{
			get
			{
				return (MapPointTemplate)base.PropertyStore.GetObject(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x00021595 File Offset: 0x0001F795
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x000215A9 File Offset: 0x0001F7A9
		public MapPointRules MapPointRules
		{
			get
			{
				return (MapPointRules)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x000215B9 File Offset: 0x0001F7B9
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x000215CD File Offset: 0x0001F7CD
		[XmlElement(typeof(RdlCollection<MapPoint>))]
		public IList<MapPoint> MapPoints
		{
			get
			{
				return (IList<MapPoint>)base.PropertyStore.GetObject(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000215DD File Offset: 0x0001F7DD
		public override void Initialize()
		{
			base.Initialize();
			this.MapPoints = new RdlCollection<MapPoint>();
		}

		// Token: 0x020003B8 RID: 952
		internal new class Definition : DefinitionStore<MapPointLayer, MapPointLayer.Definition.Properties>
		{
			// Token: 0x0600185C RID: 6236 RVA: 0x0003B661 File Offset: 0x00039861
			private Definition()
			{
			}

			// Token: 0x020004D0 RID: 1232
			internal enum Properties
			{
				// Token: 0x04000F05 RID: 3845
				Name,
				// Token: 0x04000F06 RID: 3846
				VisibilityMode,
				// Token: 0x04000F07 RID: 3847
				MinimumZoom,
				// Token: 0x04000F08 RID: 3848
				MaximumZoom,
				// Token: 0x04000F09 RID: 3849
				Transparency,
				// Token: 0x04000F0A RID: 3850
				MapDataRegionName,
				// Token: 0x04000F0B RID: 3851
				MapBindingFieldPairs,
				// Token: 0x04000F0C RID: 3852
				MapFieldDefinitions,
				// Token: 0x04000F0D RID: 3853
				MapSpatialData,
				// Token: 0x04000F0E RID: 3854
				DataElementName,
				// Token: 0x04000F0F RID: 3855
				DataElementOutput,
				// Token: 0x04000F10 RID: 3856
				MapPointTemplate,
				// Token: 0x04000F11 RID: 3857
				MapPointRules,
				// Token: 0x04000F12 RID: 3858
				MapPoints,
				// Token: 0x04000F13 RID: 3859
				PropertyCount
			}
		}
	}
}
