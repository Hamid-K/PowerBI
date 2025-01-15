using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000187 RID: 391
	public class MapTileLayer : MapLayer
	{
		// Token: 0x06000C75 RID: 3189 RVA: 0x000212C7 File Offset: 0x0001F4C7
		public MapTileLayer()
		{
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x000212CF File Offset: 0x0001F4CF
		internal MapTileLayer(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x000212D8 File Offset: 0x0001F4D8
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x000212E6 File Offset: 0x0001F4E6
		[ReportExpressionDefaultValue(typeof(MapTileStyles), MapTileStyles.Road)]
		public ReportExpression<MapTileStyles> TileStyle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapTileStyles>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x000212FA File Offset: 0x0001F4FA
		// (set) Token: 0x06000C7A RID: 3194 RVA: 0x0002130D File Offset: 0x0001F50D
		[XmlElement(typeof(RdlCollection<MapTile>))]
		public IList<MapTile> MapTiles
		{
			get
			{
				return (IList<MapTile>)base.PropertyStore.GetObject(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x0002131C File Offset: 0x0001F51C
		// (set) Token: 0x06000C7C RID: 3196 RVA: 0x0002132A File Offset: 0x0001F52A
		public ReportExpression<bool> UseSecureConnection
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002133E File Offset: 0x0001F53E
		public override void Initialize()
		{
			base.Initialize();
			this.TileStyle = MapTileStyles.Road;
			this.MapTiles = new RdlCollection<MapTile>();
		}

		// Token: 0x020003B5 RID: 949
		internal new class Definition : DefinitionStore<MapTileLayer, MapTileLayer.Definition.Properties>
		{
			// Token: 0x06001859 RID: 6233 RVA: 0x0003B649 File Offset: 0x00039849
			private Definition()
			{
			}

			// Token: 0x020004CD RID: 1229
			internal enum Properties
			{
				// Token: 0x04000EDD RID: 3805
				Name,
				// Token: 0x04000EDE RID: 3806
				VisibilityMode,
				// Token: 0x04000EDF RID: 3807
				MinimumZoom,
				// Token: 0x04000EE0 RID: 3808
				MaximumZoom,
				// Token: 0x04000EE1 RID: 3809
				Transparency,
				// Token: 0x04000EE2 RID: 3810
				TileStyle,
				// Token: 0x04000EE3 RID: 3811
				MapTiles,
				// Token: 0x04000EE4 RID: 3812
				UserSecureConnection,
				// Token: 0x04000EE5 RID: 3813
				PropertyCount
			}
		}
	}
}
