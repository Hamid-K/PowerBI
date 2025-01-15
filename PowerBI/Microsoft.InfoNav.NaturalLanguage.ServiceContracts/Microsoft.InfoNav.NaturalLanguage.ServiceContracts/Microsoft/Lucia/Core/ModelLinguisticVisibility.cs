using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200006F RID: 111
	[DataContract(Name = "Visibility", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	[XmlRoot(ElementName = "Visibility", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	[XmlType(TypeName = "Visibility", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	public struct ModelLinguisticVisibility : IModelLinguisticItem, IExtensibleDataObject
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00004BCD File Offset: 0x00002DCD
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00004BD5 File Offset: 0x00002DD5
		[DataMember(Name = "Value", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		[XmlText(Type = typeof(EntityVisibility))]
		public EntityVisibility Value { readonly get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00004BDE File Offset: 0x00002DDE
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00004BE6 File Offset: 0x00002DE6
		[DataMember(Name = "State", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		[XmlAttribute(AttributeName = "State")]
		[DefaultValue(PropertyState.Default)]
		public PropertyState State { readonly get; set; }

		// Token: 0x060001EB RID: 491 RVA: 0x00004BF0 File Offset: 0x00002DF0
		public static implicit operator ModelLinguisticVisibility(EntityVisibility value)
		{
			return new ModelLinguisticVisibility
			{
				Value = value
			};
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00004C0E File Offset: 0x00002E0E
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00004C16 File Offset: 0x00002E16
		ExtensionDataObject IExtensibleDataObject.ExtensionData { get; set; }

		// Token: 0x060001EE RID: 494 RVA: 0x00004C1F File Offset: 0x00002E1F
		public bool ShouldSerialize()
		{
			return this.State != PropertyState.Default || this.Value > EntityVisibility.Visible;
		}
	}
}
