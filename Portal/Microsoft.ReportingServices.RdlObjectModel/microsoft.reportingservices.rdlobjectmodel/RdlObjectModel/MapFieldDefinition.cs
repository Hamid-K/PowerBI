using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001AB RID: 427
	public class MapFieldDefinition : ReportObject, INamedObject
	{
		// Token: 0x06000E05 RID: 3589 RVA: 0x00022DFF File Offset: 0x00020FFF
		public MapFieldDefinition()
		{
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00022E07 File Offset: 0x00021007
		internal MapFieldDefinition(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x00022E10 File Offset: 0x00021010
		// (set) Token: 0x06000E08 RID: 3592 RVA: 0x00022E1E File Offset: 0x0002101E
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

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00022E2D File Offset: 0x0002102D
		// (set) Token: 0x06000E0A RID: 3594 RVA: 0x00022E3B File Offset: 0x0002103B
		public MapDataTypes DataType
		{
			get
			{
				return (MapDataTypes)base.PropertyStore.GetInteger(1);
			}
			set
			{
				base.PropertyStore.SetInteger(1, (int)value);
			}
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00022E4A File Offset: 0x0002104A
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003D7 RID: 983
		internal class Definition : DefinitionStore<MapFieldDefinition, MapFieldDefinition.Definition.Properties>
		{
			// Token: 0x0600187B RID: 6267 RVA: 0x0003B759 File Offset: 0x00039959
			private Definition()
			{
			}

			// Token: 0x020004EF RID: 1263
			internal enum Properties
			{
				// Token: 0x04001031 RID: 4145
				Name,
				// Token: 0x04001032 RID: 4146
				DataType,
				// Token: 0x04001033 RID: 4147
				PropertyCount
			}
		}
	}
}
