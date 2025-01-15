using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200006A RID: 106
	[DataContract(Name = "Noun", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	[XmlRoot(ElementName = "Noun", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	[XmlType(TypeName = "Noun", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	public sealed class ModelLinguisticNoun : IModelLinguisticItem, IExtensibleDataObject
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00004657 File Offset: 0x00002857
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x0000465F File Offset: 0x0000285F
		[DataMember(Name = "Value", IsRequired = true, EmitDefaultValue = false, Order = 0)]
		[XmlText]
		public string Value { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00004668 File Offset: 0x00002868
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00004696 File Offset: 0x00002896
		[XmlAttribute(AttributeName = "Weight")]
		[DefaultValue(1.0)]
		public double Weight
		{
			get
			{
				double? weight = this._weight;
				if (weight == null)
				{
					return 1.0;
				}
				return weight.GetValueOrDefault();
			}
			set
			{
				this._weight = new double?(value);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000046A4 File Offset: 0x000028A4
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x000046E8 File Offset: 0x000028E8
		[DataMember(Name = "Weight", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		[XmlIgnore]
		private double? DataContractWeight
		{
			get
			{
				double? weight = this._weight;
				double num = 1.0;
				if (!((weight.GetValueOrDefault() == num) & (weight != null)))
				{
					return this._weight;
				}
				return null;
			}
			set
			{
				this._weight = new double?(value ?? 1.0);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000471D File Offset: 0x0000291D
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00004725 File Offset: 0x00002925
		ExtensionDataObject IExtensibleDataObject.ExtensionData { get; set; }

		// Token: 0x060001A8 RID: 424 RVA: 0x0000472E File Offset: 0x0000292E
		public static implicit operator ModelLinguisticNoun(string value)
		{
			return new ModelLinguisticNoun
			{
				Value = value
			};
		}

		// Token: 0x04000246 RID: 582
		private const double DefaultWeight = 1.0;

		// Token: 0x04000247 RID: 583
		private double? _weight = new double?(1.0);
	}
}
