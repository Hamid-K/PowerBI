using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002D1 RID: 721
	public sealed class ExtendedPropertyDefinition : PropertyDefinitionBase
	{
		// Token: 0x0600198C RID: 6540 RVA: 0x00045558 File Offset: 0x00044558
		internal ExtendedPropertyDefinition()
		{
			this.mapiType = MapiPropertyType.String;
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x00045568 File Offset: 0x00044568
		internal ExtendedPropertyDefinition(MapiPropertyType mapiType)
			: this()
		{
			this.mapiType = mapiType;
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00045577 File Offset: 0x00044577
		public ExtendedPropertyDefinition(int tag, MapiPropertyType mapiType)
			: this(mapiType)
		{
			if (tag < 0 || tag > 65535)
			{
				throw new ArgumentOutOfRangeException("tag", Strings.TagValueIsOutOfRange);
			}
			this.tag = new int?(tag);
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x000455AD File Offset: 0x000445AD
		public ExtendedPropertyDefinition(DefaultExtendedPropertySet propertySet, string name, MapiPropertyType mapiType)
			: this(mapiType)
		{
			EwsUtilities.ValidateParam(name, "name");
			this.propertySet = new DefaultExtendedPropertySet?(propertySet);
			this.name = name;
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x000455D4 File Offset: 0x000445D4
		public ExtendedPropertyDefinition(DefaultExtendedPropertySet propertySet, int id, MapiPropertyType mapiType)
			: this(mapiType)
		{
			this.propertySet = new DefaultExtendedPropertySet?(propertySet);
			this.id = new int?(id);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x000455F5 File Offset: 0x000445F5
		public ExtendedPropertyDefinition(Guid propertySetId, string name, MapiPropertyType mapiType)
			: this(mapiType)
		{
			EwsUtilities.ValidateParam(name, "name");
			this.propertySetId = new Guid?(propertySetId);
			this.name = name;
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0004561C File Offset: 0x0004461C
		public ExtendedPropertyDefinition(Guid propertySetId, int id, MapiPropertyType mapiType)
			: this(mapiType)
		{
			this.propertySetId = new Guid?(propertySetId);
			this.id = new int?(id);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x00045640 File Offset: 0x00044640
		internal static bool IsEqualTo(ExtendedPropertyDefinition extPropDef1, ExtendedPropertyDefinition extPropDef2)
		{
			return object.ReferenceEquals(extPropDef1, extPropDef2) || (extPropDef1 != null && extPropDef2 != null && extPropDef1.Id == extPropDef2.Id && extPropDef1.MapiType == extPropDef2.MapiType && extPropDef1.Tag == extPropDef2.Tag && extPropDef1.Name == extPropDef2.Name && extPropDef1.PropertySet == extPropDef2.PropertySet && extPropDef1.propertySetId == extPropDef2.propertySetId);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0004576E File Offset: 0x0004476E
		internal override string GetXmlElementName()
		{
			return "ExtendedFieldURI";
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x00045775 File Offset: 0x00044775
		protected override string GetJsonType()
		{
			return "ExtendedPropertyUri";
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x0004577C File Offset: 0x0004477C
		public override ExchangeVersion Version
		{
			get
			{
				return ExchangeVersion.Exchange2007_SP1;
			}
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x00045780 File Offset: 0x00044780
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			if (this.propertySet != null)
			{
				writer.WriteAttributeValue("DistinguishedPropertySetId", this.propertySet.Value);
			}
			if (this.propertySetId != null)
			{
				writer.WriteAttributeValue("PropertySetId", this.propertySetId.Value.ToString());
			}
			if (this.tag != null)
			{
				writer.WriteAttributeValue("PropertyTag", this.tag.Value);
			}
			if (!string.IsNullOrEmpty(this.name))
			{
				writer.WriteAttributeValue("PropertyName", this.name);
			}
			if (this.id != null)
			{
				writer.WriteAttributeValue("PropertyId", this.id.Value);
			}
			writer.WriteAttributeValue("PropertyType", this.mapiType);
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0004586C File Offset: 0x0004486C
		internal override void AddJsonProperties(JsonObject jsonPropertyDefinition, ExchangeService service)
		{
			if (this.propertySet != null)
			{
				jsonPropertyDefinition.Add("DistinguishedPropertySetId", this.propertySet.Value);
			}
			if (this.propertySetId != null)
			{
				jsonPropertyDefinition.Add("PropertySetId", this.propertySetId.Value.ToString());
			}
			if (this.tag != null)
			{
				jsonPropertyDefinition.Add("PropertyTag", this.tag.Value);
			}
			if (!string.IsNullOrEmpty(this.name))
			{
				jsonPropertyDefinition.Add("PropertyName", this.name);
			}
			if (this.id != null)
			{
				jsonPropertyDefinition.Add("PropertyId", this.id.Value);
			}
			jsonPropertyDefinition.Add("PropertyType", this.mapiType);
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0004594C File Offset: 0x0004494C
		internal void LoadFromXml(EwsServiceXmlReader reader)
		{
			string text = reader.ReadAttributeValue("DistinguishedPropertySetId");
			if (!string.IsNullOrEmpty(text))
			{
				this.propertySet = new DefaultExtendedPropertySet?((DefaultExtendedPropertySet)Enum.Parse(typeof(DefaultExtendedPropertySet), text, false));
			}
			text = reader.ReadAttributeValue("PropertySetId");
			if (!string.IsNullOrEmpty(text))
			{
				this.propertySetId = new Guid?(new Guid(text));
			}
			text = reader.ReadAttributeValue("PropertyTag");
			if (!string.IsNullOrEmpty(text))
			{
				this.tag = new int?((int)Convert.ToUInt16(text, 16));
			}
			this.name = reader.ReadAttributeValue("PropertyName");
			text = reader.ReadAttributeValue("PropertyId");
			if (!string.IsNullOrEmpty(text))
			{
				this.id = new int?(int.Parse(text));
			}
			this.mapiType = reader.ReadAttributeValue<MapiPropertyType>("PropertyType");
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00045A24 File Offset: 0x00044A24
		internal void LoadFromJson(JsonObject jsonObject)
		{
			foreach (string text in jsonObject.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600187e-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(6);
						dictionary.Add("DistinguishedPropertySetId", 0);
						dictionary.Add("PropertySetId", 1);
						dictionary.Add("PropertyTag", 2);
						dictionary.Add("PropertyName", 3);
						dictionary.Add("PropertyId", 4);
						dictionary.Add("PropertyType", 5);
						<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600187e-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600187e-1.TryGetValue(text2, ref num))
					{
						switch (num)
						{
						case 0:
							this.propertySet = new DefaultExtendedPropertySet?(jsonObject.ReadEnumValue<DefaultExtendedPropertySet>(text));
							break;
						case 1:
							this.propertySetId = new Guid?(new Guid(jsonObject.ReadAsString(text)));
							break;
						case 2:
							this.tag = new int?((int)Convert.ToUInt16(jsonObject.ReadAsString(text), 16));
							break;
						case 3:
							this.name = jsonObject.ReadAsString(text);
							break;
						case 4:
							this.id = new int?(jsonObject.ReadAsInt(text));
							break;
						case 5:
							this.mapiType = jsonObject.ReadEnumValue<MapiPropertyType>(text);
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00045B94 File Offset: 0x00044B94
		public static bool operator ==(ExtendedPropertyDefinition extPropDef1, ExtendedPropertyDefinition extPropDef2)
		{
			return ExtendedPropertyDefinition.IsEqualTo(extPropDef1, extPropDef2);
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00045B9D File Offset: 0x00044B9D
		public static bool operator !=(ExtendedPropertyDefinition extPropDef1, ExtendedPropertyDefinition extPropDef2)
		{
			return !ExtendedPropertyDefinition.IsEqualTo(extPropDef1, extPropDef2);
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00045BAC File Offset: 0x00044BAC
		public override bool Equals(object obj)
		{
			ExtendedPropertyDefinition extendedPropertyDefinition = obj as ExtendedPropertyDefinition;
			return ExtendedPropertyDefinition.IsEqualTo(extendedPropertyDefinition, this);
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00045BC7 File Offset: 0x00044BC7
		public override int GetHashCode()
		{
			return this.GetPrintableName().GetHashCode();
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00045BD4 File Offset: 0x00044BD4
		internal override string GetPrintableName()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.Append(this.FormatField<string>("Name", this.Name));
			stringBuilder.Append(this.FormatField<MapiPropertyType?>("MapiType", new MapiPropertyType?(this.MapiType)));
			stringBuilder.Append(this.FormatField<int?>("Id", this.Id));
			stringBuilder.Append(this.FormatField<DefaultExtendedPropertySet?>("PropertySet", this.PropertySet));
			stringBuilder.Append(this.FormatField<Guid?>("PropertySetId", this.PropertySetId));
			stringBuilder.Append(this.FormatField<int?>("Tag", this.Tag));
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00045C9A File Offset: 0x00044C9A
		internal string FormatField<T>(string name, T fieldValue)
		{
			if (fieldValue == null)
			{
				return string.Empty;
			}
			return string.Format("{0}: {1} ", name, fieldValue.ToString());
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x00045CC2 File Offset: 0x00044CC2
		public DefaultExtendedPropertySet? PropertySet
		{
			get
			{
				return this.propertySet;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x00045CCA File Offset: 0x00044CCA
		public Guid? PropertySetId
		{
			get
			{
				return this.propertySetId;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x00045CD2 File Offset: 0x00044CD2
		public int? Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x00045CDA File Offset: 0x00044CDA
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x00045CE2 File Offset: 0x00044CE2
		public int? Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x00045CEA File Offset: 0x00044CEA
		public MapiPropertyType MapiType
		{
			get
			{
				return this.mapiType;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x00045CF2 File Offset: 0x00044CF2
		public override Type Type
		{
			get
			{
				return MapiTypeConverter.MapiTypeConverterMap[this.MapiType].Type;
			}
		}

		// Token: 0x040013F8 RID: 5112
		private const string FieldFormat = "{0}: {1} ";

		// Token: 0x040013F9 RID: 5113
		private const string PropertySetFieldName = "PropertySet";

		// Token: 0x040013FA RID: 5114
		private const string PropertySetIdFieldName = "PropertySetId";

		// Token: 0x040013FB RID: 5115
		private const string TagFieldName = "Tag";

		// Token: 0x040013FC RID: 5116
		private const string NameFieldName = "Name";

		// Token: 0x040013FD RID: 5117
		private const string IdFieldName = "Id";

		// Token: 0x040013FE RID: 5118
		private const string MapiTypeFieldName = "MapiType";

		// Token: 0x040013FF RID: 5119
		private DefaultExtendedPropertySet? propertySet;

		// Token: 0x04001400 RID: 5120
		private Guid? propertySetId;

		// Token: 0x04001401 RID: 5121
		private int? tag;

		// Token: 0x04001402 RID: 5122
		private string name;

		// Token: 0x04001403 RID: 5123
		private int? id;

		// Token: 0x04001404 RID: 5124
		private MapiPropertyType mapiType;
	}
}
