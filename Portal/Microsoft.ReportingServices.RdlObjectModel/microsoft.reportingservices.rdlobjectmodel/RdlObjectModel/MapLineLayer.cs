using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200018B RID: 395
	public class MapLineLayer : MapVectorLayer
	{
		// Token: 0x06000CA4 RID: 3236 RVA: 0x000215F0 File Offset: 0x0001F7F0
		public MapLineLayer()
		{
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x000215F8 File Offset: 0x0001F7F8
		internal MapLineLayer(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x00021601 File Offset: 0x0001F801
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x00021615 File Offset: 0x0001F815
		public MapLineTemplate MapLineTemplate
		{
			get
			{
				return (MapLineTemplate)base.PropertyStore.GetObject(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00021625 File Offset: 0x0001F825
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x00021639 File Offset: 0x0001F839
		public MapLineRules MapLineRules
		{
			get
			{
				return (MapLineRules)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x00021649 File Offset: 0x0001F849
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x0002165D File Offset: 0x0001F85D
		[XmlElement(typeof(RdlCollection<MapLine>))]
		public IList<MapLine> MapLines
		{
			get
			{
				return (IList<MapLine>)base.PropertyStore.GetObject(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002166D File Offset: 0x0001F86D
		public override void Initialize()
		{
			base.Initialize();
			this.MapLines = new RdlCollection<MapLine>();
		}

		// Token: 0x020003B9 RID: 953
		internal new class Definition : DefinitionStore<MapLineLayer, MapLineLayer.Definition.Properties>
		{
			// Token: 0x0600185D RID: 6237 RVA: 0x0003B669 File Offset: 0x00039869
			private Definition()
			{
			}

			// Token: 0x020004D1 RID: 1233
			internal enum Properties
			{
				// Token: 0x04000F15 RID: 3861
				Name,
				// Token: 0x04000F16 RID: 3862
				VisibilityMode,
				// Token: 0x04000F17 RID: 3863
				MinimumZoom,
				// Token: 0x04000F18 RID: 3864
				MaximumZoom,
				// Token: 0x04000F19 RID: 3865
				Transparency,
				// Token: 0x04000F1A RID: 3866
				MapDataRegionName,
				// Token: 0x04000F1B RID: 3867
				MapBindingFieldPairs,
				// Token: 0x04000F1C RID: 3868
				MapFieldDefinitions,
				// Token: 0x04000F1D RID: 3869
				MapSpatialData,
				// Token: 0x04000F1E RID: 3870
				DataElementName,
				// Token: 0x04000F1F RID: 3871
				DataElementOutput,
				// Token: 0x04000F20 RID: 3872
				MapLineTemplate,
				// Token: 0x04000F21 RID: 3873
				MapLineRules,
				// Token: 0x04000F22 RID: 3874
				MapLines,
				// Token: 0x04000F23 RID: 3875
				PropertyCount
			}
		}
	}
}
