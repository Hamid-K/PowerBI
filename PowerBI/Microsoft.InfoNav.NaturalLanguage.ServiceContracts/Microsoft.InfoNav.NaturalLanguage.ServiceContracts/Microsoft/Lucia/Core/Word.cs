using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000076 RID: 118
	[DataContract(Name = "Word", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	[XmlRoot(ElementName = "Word", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	[XmlType(TypeName = "Word", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	public sealed class Word : IModelLinguisticItem, IExtensibleDataObject, IModelLinguisticSourcedItem
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00004F52 File Offset: 0x00003152
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00004F5A File Offset: 0x0000315A
		[DataMember(Name = "Value", IsRequired = true, EmitDefaultValue = false, Order = 10)]
		[XmlText]
		public string Value { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00004F63 File Offset: 0x00003163
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00004F6B File Offset: 0x0000316B
		[XmlAttribute(AttributeName = "Source")]
		[DefaultValue(LinguisticItemSource.User)]
		public LinguisticItemSource Source { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00004F74 File Offset: 0x00003174
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00004F7C File Offset: 0x0000317C
		[XmlIgnore]
		[DataMember(Name = "Source", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		private int DataContractSource
		{
			get
			{
				return (int)this.Source;
			}
			set
			{
				this.Source = (LinguisticItemSource)value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00004F85 File Offset: 0x00003185
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00004F8D File Offset: 0x0000318D
		[DataMember(Name = "SourceType", IsRequired = false, EmitDefaultValue = false, Order = 23)]
		[XmlAttribute(AttributeName = "SourceType")]
		[DefaultValue(LinguisticItemSourceType.Default)]
		public LinguisticItemSourceType SourceType { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00004F96 File Offset: 0x00003196
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00004F9E File Offset: 0x0000319E
		[DataMember(Name = "SourceAgent", IsRequired = false, EmitDefaultValue = false, Order = 27)]
		[XmlAttribute(AttributeName = "SourceAgent")]
		public string SourceAgent { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00004FA8 File Offset: 0x000031A8
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00004FD6 File Offset: 0x000031D6
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00004FE4 File Offset: 0x000031E4
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00005026 File Offset: 0x00003226
		[DataMember(Name = "Weight", IsRequired = false, EmitDefaultValue = false, Order = 30)]
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
				this._weight = ((value == null) ? new double?(1.0) : value);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00005048 File Offset: 0x00003248
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00005050 File Offset: 0x00003250
		[DataMember(Name = "TemplateSchema", IsRequired = false, EmitDefaultValue = false)]
		[XmlAttribute(AttributeName = "TemplateSchema")]
		public string TemplateSchema { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00005059 File Offset: 0x00003259
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00005061 File Offset: 0x00003261
		[XmlAttribute(AttributeName = "Type")]
		[DefaultValue(WordType.None)]
		public WordType Type { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000506A File Offset: 0x0000326A
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00005072 File Offset: 0x00003272
		[XmlIgnore]
		[DataMember(Name = "Type", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		private int DataContractWordType
		{
			get
			{
				return (int)this.Type;
			}
			set
			{
				this.Type = (WordType)value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000507B File Offset: 0x0000327B
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00005083 File Offset: 0x00003283
		[XmlIgnore]
		public DateTime? LastModified
		{
			get
			{
				return this._lastModified;
			}
			set
			{
				this._lastModified = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000508C File Offset: 0x0000328C
		// (set) Token: 0x06000222 RID: 546 RVA: 0x000050AE File Offset: 0x000032AE
		[XmlAttribute(AttributeName = "LastModified")]
		[DataMember(Name = "LastModified", IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public string DataContractLastModified
		{
			get
			{
				if (this._lastModified == null)
				{
					return null;
				}
				return XmlConvert.ToString(this._lastModified.Value, XmlDateTimeSerializationMode.Utc);
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this._lastModified = null;
					return;
				}
				this._lastModified = new DateTime?(XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.Utc));
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000050D7 File Offset: 0x000032D7
		// (set) Token: 0x06000224 RID: 548 RVA: 0x000050DF File Offset: 0x000032DF
		ExtensionDataObject IExtensibleDataObject.ExtensionData { get; set; }

		// Token: 0x06000225 RID: 549 RVA: 0x000050E8 File Offset: 0x000032E8
		public static implicit operator Word(string value)
		{
			return new Word
			{
				Value = value
			};
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000050F8 File Offset: 0x000032F8
		public override string ToString()
		{
			return StringUtil.FormatInvariant("({0}: {1}, {2}, Weight={3})", new object[] { this.Value, this.Type, this.Source, this.Weight });
		}

		// Token: 0x0400027C RID: 636
		private const double DefaultWeight = 1.0;

		// Token: 0x0400027D RID: 637
		private double? _weight = new double?(1.0);

		// Token: 0x0400027E RID: 638
		private DateTime? _lastModified;
	}
}
