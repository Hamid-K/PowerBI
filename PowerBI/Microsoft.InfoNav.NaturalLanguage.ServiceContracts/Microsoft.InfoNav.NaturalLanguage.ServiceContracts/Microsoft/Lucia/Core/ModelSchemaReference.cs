using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000070 RID: 112
	[DataContract(Name = "SchemaReference", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	[XmlRoot(ElementName = "SchemaReference", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	[XmlType(TypeName = "SchemaReference", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	public sealed class ModelSchemaReference : IModelLinguisticItem, IExtensibleDataObject
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00004C34 File Offset: 0x00002E34
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00004C3C File Offset: 0x00002E3C
		[XmlAttribute(AttributeName = "Namespace")]
		public string Namespace { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00004C45 File Offset: 0x00002E45
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00004C4D File Offset: 0x00002E4D
		ExtensionDataObject IExtensibleDataObject.ExtensionData { get; set; }
	}
}
