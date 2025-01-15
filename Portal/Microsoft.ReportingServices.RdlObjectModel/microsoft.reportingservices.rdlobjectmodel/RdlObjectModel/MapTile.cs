using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001AA RID: 426
	public class MapTile : ReportObject, INamedObject
	{
		// Token: 0x06000DFC RID: 3580 RVA: 0x00022D8F File Offset: 0x00020F8F
		public MapTile()
		{
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00022D97 File Offset: 0x00020F97
		internal MapTile(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x00022DA0 File Offset: 0x00020FA0
		// (set) Token: 0x06000DFF RID: 3583 RVA: 0x00022DAE File Offset: 0x00020FAE
		[XmlElement("Name")]
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

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x00022DBD File Offset: 0x00020FBD
		// (set) Token: 0x06000E01 RID: 3585 RVA: 0x00022DCB File Offset: 0x00020FCB
		public string TileData
		{
			get
			{
				return base.PropertyStore.GetObject<string>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x00022DDA File Offset: 0x00020FDA
		// (set) Token: 0x06000E03 RID: 3587 RVA: 0x00022DE8 File Offset: 0x00020FE8
		public string MIMEType
		{
			get
			{
				return base.PropertyStore.GetObject<string>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00022DF7 File Offset: 0x00020FF7
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003D6 RID: 982
		internal class Definition : DefinitionStore<MapTile, MapTile.Definition.Properties>
		{
			// Token: 0x0600187A RID: 6266 RVA: 0x0003B751 File Offset: 0x00039951
			private Definition()
			{
			}

			// Token: 0x020004EE RID: 1262
			internal enum Properties
			{
				// Token: 0x0400102C RID: 4140
				Name,
				// Token: 0x0400102D RID: 4141
				TileData,
				// Token: 0x0400102E RID: 4142
				MIMEType,
				// Token: 0x0400102F RID: 4143
				PropertyCount
			}
		}
	}
}
