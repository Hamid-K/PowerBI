using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200019D RID: 413
	public class MapField : ReportObject, INamedObject
	{
		// Token: 0x06000D8E RID: 3470 RVA: 0x000226F5 File Offset: 0x000208F5
		public MapField()
		{
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x000226FD File Offset: 0x000208FD
		internal MapField(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00022706 File Offset: 0x00020906
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x00022714 File Offset: 0x00020914
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

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00022723 File Offset: 0x00020923
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x00022731 File Offset: 0x00020931
		public string Value
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

		// Token: 0x06000D94 RID: 3476 RVA: 0x00022740 File Offset: 0x00020940
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003C9 RID: 969
		internal class Definition : DefinitionStore<MapField, MapField.Definition.Properties>
		{
			// Token: 0x0600186D RID: 6253 RVA: 0x0003B6E9 File Offset: 0x000398E9
			private Definition()
			{
			}

			// Token: 0x020004E1 RID: 1249
			internal enum Properties
			{
				// Token: 0x04000FEF RID: 4079
				Name,
				// Token: 0x04000FF0 RID: 4080
				Value,
				// Token: 0x04000FF1 RID: 4081
				PropertyCount
			}
		}
	}
}
