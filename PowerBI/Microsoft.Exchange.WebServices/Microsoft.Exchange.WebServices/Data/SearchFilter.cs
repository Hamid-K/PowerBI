using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002E4 RID: 740
	public abstract class SearchFilter : ComplexProperty
	{
		// Token: 0x06001A17 RID: 6679 RVA: 0x000470DA File Offset: 0x000460DA
		internal SearchFilter()
		{
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x000470E4 File Offset: 0x000460E4
		internal static SearchFilter LoadFromXml(EwsServiceXmlReader reader)
		{
			reader.EnsureCurrentNodeIsStartElement();
			string localName = reader.LocalName;
			SearchFilter searchFilterInstance = SearchFilter.GetSearchFilterInstance(localName);
			if (searchFilterInstance != null)
			{
				searchFilterInstance.LoadFromXml(reader, reader.LocalName);
			}
			return searchFilterInstance;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00047118 File Offset: 0x00046118
		internal static SearchFilter LoadSearchFilterFromJson(JsonObject jsonObject, ExchangeService service)
		{
			SearchFilter searchFilterInstance = SearchFilter.GetSearchFilterInstance(jsonObject.ReadTypeString());
			if (searchFilterInstance != null)
			{
				searchFilterInstance.LoadFromJson(jsonObject, service);
			}
			return searchFilterInstance;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00047140 File Offset: 0x00046140
		private static SearchFilter GetSearchFilterInstance(string localName)
		{
			if (localName != null)
			{
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60018fc-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(12);
					dictionary.Add("Exists", 0);
					dictionary.Add("Contains", 1);
					dictionary.Add("Excludes", 2);
					dictionary.Add("Not", 3);
					dictionary.Add("And", 4);
					dictionary.Add("Or", 5);
					dictionary.Add("IsEqualTo", 6);
					dictionary.Add("IsNotEqualTo", 7);
					dictionary.Add("IsGreaterThan", 8);
					dictionary.Add("IsGreaterThanOrEqualTo", 9);
					dictionary.Add("IsLessThan", 10);
					dictionary.Add("IsLessThanOrEqualTo", 11);
					<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60018fc-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60018fc-1.TryGetValue(localName, ref num))
				{
					switch (num)
					{
					case 0:
						return new SearchFilter.Exists();
					case 1:
						return new SearchFilter.ContainsSubstring();
					case 2:
						return new SearchFilter.ExcludesBitmask();
					case 3:
						return new SearchFilter.Not();
					case 4:
						return new SearchFilter.SearchFilterCollection(LogicalOperator.And);
					case 5:
						return new SearchFilter.SearchFilterCollection(LogicalOperator.Or);
					case 6:
						return new SearchFilter.IsEqualTo();
					case 7:
						return new SearchFilter.IsNotEqualTo();
					case 8:
						return new SearchFilter.IsGreaterThan();
					case 9:
						return new SearchFilter.IsGreaterThanOrEqualTo();
					case 10:
						return new SearchFilter.IsLessThan();
					case 11:
						return new SearchFilter.IsLessThanOrEqualTo();
					}
				}
			}
			return null;
		}

		// Token: 0x06001A1B RID: 6683
		internal abstract string GetXmlElementName();

		// Token: 0x06001A1C RID: 6684 RVA: 0x000472B4 File Offset: 0x000462B4
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.AddTypeParameter(this.GetXmlElementName());
			return jsonObject;
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x000472D4 File Offset: 0x000462D4
		internal virtual void WriteToXml(EwsServiceXmlWriter writer)
		{
			base.WriteToXml(writer, this.GetXmlElementName());
		}

		// Token: 0x020002E5 RID: 741
		[EditorBrowsable(1)]
		public abstract class PropertyBasedFilter : SearchFilter
		{
			// Token: 0x06001A1E RID: 6686 RVA: 0x000472E3 File Offset: 0x000462E3
			internal PropertyBasedFilter()
			{
			}

			// Token: 0x06001A1F RID: 6687 RVA: 0x000472EB File Offset: 0x000462EB
			internal PropertyBasedFilter(PropertyDefinitionBase propertyDefinition)
			{
				this.propertyDefinition = propertyDefinition;
			}

			// Token: 0x06001A20 RID: 6688 RVA: 0x000472FA File Offset: 0x000462FA
			internal override void InternalValidate()
			{
				if (this.propertyDefinition == null)
				{
					throw new ServiceValidationException(Strings.PropertyDefinitionPropertyMustBeSet);
				}
			}

			// Token: 0x06001A21 RID: 6689 RVA: 0x00047314 File Offset: 0x00046314
			internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
			{
				return PropertyDefinitionBase.TryLoadFromXml(reader, ref this.propertyDefinition);
			}

			// Token: 0x06001A22 RID: 6690 RVA: 0x00047322 File Offset: 0x00046322
			internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
			{
				this.PropertyDefinition = PropertyDefinitionBase.TryLoadFromJson(jsonProperty.ReadAsJsonObject("Item"));
			}

			// Token: 0x06001A23 RID: 6691 RVA: 0x0004733A File Offset: 0x0004633A
			internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
			{
				this.PropertyDefinition.WriteToXml(writer);
			}

			// Token: 0x06001A24 RID: 6692 RVA: 0x00047348 File Offset: 0x00046348
			internal override object InternalToJson(ExchangeService service)
			{
				JsonObject jsonObject = base.InternalToJson(service) as JsonObject;
				jsonObject.Add("Item", ((IJsonSerializable)this.PropertyDefinition).ToJson(service));
				return jsonObject;
			}

			// Token: 0x1700064B RID: 1611
			// (get) Token: 0x06001A25 RID: 6693 RVA: 0x0004737A File Offset: 0x0004637A
			// (set) Token: 0x06001A26 RID: 6694 RVA: 0x00047382 File Offset: 0x00046382
			public PropertyDefinitionBase PropertyDefinition
			{
				get
				{
					return this.propertyDefinition;
				}
				set
				{
					this.SetFieldValue<PropertyDefinitionBase>(ref this.propertyDefinition, value);
				}
			}

			// Token: 0x04001413 RID: 5139
			private PropertyDefinitionBase propertyDefinition;
		}

		// Token: 0x020002E6 RID: 742
		public sealed class ContainsSubstring : SearchFilter.PropertyBasedFilter
		{
			// Token: 0x06001A27 RID: 6695 RVA: 0x00047391 File Offset: 0x00046391
			public ContainsSubstring()
			{
			}

			// Token: 0x06001A28 RID: 6696 RVA: 0x000473A7 File Offset: 0x000463A7
			public ContainsSubstring(PropertyDefinitionBase propertyDefinition, string value)
				: base(propertyDefinition)
			{
				this.value = value;
			}

			// Token: 0x06001A29 RID: 6697 RVA: 0x000473C5 File Offset: 0x000463C5
			public ContainsSubstring(PropertyDefinitionBase propertyDefinition, string value, ContainmentMode containmentMode, ComparisonMode comparisonMode)
				: this(propertyDefinition, value)
			{
				this.containmentMode = containmentMode;
				this.comparisonMode = comparisonMode;
			}

			// Token: 0x06001A2A RID: 6698 RVA: 0x000473DE File Offset: 0x000463DE
			internal override void InternalValidate()
			{
				base.InternalValidate();
				if (string.IsNullOrEmpty(this.value))
				{
					throw new ServiceValidationException(Strings.ValuePropertyMustBeSet);
				}
			}

			// Token: 0x06001A2B RID: 6699 RVA: 0x00047403 File Offset: 0x00046403
			internal override string GetXmlElementName()
			{
				return "Contains";
			}

			// Token: 0x06001A2C RID: 6700 RVA: 0x0004740C File Offset: 0x0004640C
			internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
			{
				bool flag = base.TryReadElementFromXml(reader);
				if (!flag && reader.LocalName == "Constant")
				{
					this.value = reader.ReadAttributeValue("Value");
					flag = true;
				}
				return flag;
			}

			// Token: 0x06001A2D RID: 6701 RVA: 0x0004744C File Offset: 0x0004644C
			internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
			{
				base.ReadAttributesFromXml(reader);
				this.containmentMode = reader.ReadAttributeValue<ContainmentMode>("ContainmentMode");
				try
				{
					this.comparisonMode = reader.ReadAttributeValue<ComparisonMode>("ContainmentComparison");
				}
				catch (ArgumentException)
				{
					this.comparisonMode = ComparisonMode.IgnoreCaseAndNonSpacingCharacters;
				}
			}

			// Token: 0x06001A2E RID: 6702 RVA: 0x000474A0 File Offset: 0x000464A0
			internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
			{
				base.LoadFromJson(jsonProperty, service);
				this.value = jsonProperty.ReadAsJsonObject("Constant").ReadAsString("Value");
				this.containmentMode = jsonProperty.ReadEnumValue<ContainmentMode>("ContainmentMode");
				this.comparisonMode = jsonProperty.ReadEnumValue<ComparisonMode>("ContainmentComparison");
			}

			// Token: 0x06001A2F RID: 6703 RVA: 0x000474F2 File Offset: 0x000464F2
			internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
			{
				base.WriteAttributesToXml(writer);
				writer.WriteAttributeValue("ContainmentMode", this.ContainmentMode);
				writer.WriteAttributeValue("ContainmentComparison", this.ComparisonMode);
			}

			// Token: 0x06001A30 RID: 6704 RVA: 0x00047527 File Offset: 0x00046527
			internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
			{
				base.WriteElementsToXml(writer);
				writer.WriteStartElement(XmlNamespace.Types, "Constant");
				writer.WriteAttributeValue("Value", this.Value);
				writer.WriteEndElement();
			}

			// Token: 0x06001A31 RID: 6705 RVA: 0x00047554 File Offset: 0x00046554
			internal override object InternalToJson(ExchangeService service)
			{
				JsonObject jsonObject = base.InternalToJson(service) as JsonObject;
				jsonObject.Add("ContainmentMode", this.ContainmentMode);
				jsonObject.Add("ContainmentComparison", this.ComparisonMode);
				JsonObject jsonObject2 = new JsonObject();
				jsonObject2.Add("Value", this.Value);
				jsonObject.Add("Constant", jsonObject2);
				return jsonObject;
			}

			// Token: 0x1700064C RID: 1612
			// (get) Token: 0x06001A32 RID: 6706 RVA: 0x000475BE File Offset: 0x000465BE
			// (set) Token: 0x06001A33 RID: 6707 RVA: 0x000475C6 File Offset: 0x000465C6
			public ContainmentMode ContainmentMode
			{
				get
				{
					return this.containmentMode;
				}
				set
				{
					this.SetFieldValue<ContainmentMode>(ref this.containmentMode, value);
				}
			}

			// Token: 0x1700064D RID: 1613
			// (get) Token: 0x06001A34 RID: 6708 RVA: 0x000475D5 File Offset: 0x000465D5
			// (set) Token: 0x06001A35 RID: 6709 RVA: 0x000475DD File Offset: 0x000465DD
			public ComparisonMode ComparisonMode
			{
				get
				{
					return this.comparisonMode;
				}
				set
				{
					this.SetFieldValue<ComparisonMode>(ref this.comparisonMode, value);
				}
			}

			// Token: 0x1700064E RID: 1614
			// (get) Token: 0x06001A36 RID: 6710 RVA: 0x000475EC File Offset: 0x000465EC
			// (set) Token: 0x06001A37 RID: 6711 RVA: 0x000475F4 File Offset: 0x000465F4
			public string Value
			{
				get
				{
					return this.value;
				}
				set
				{
					this.SetFieldValue<string>(ref this.value, value);
				}
			}

			// Token: 0x04001414 RID: 5140
			private ContainmentMode containmentMode = ContainmentMode.Substring;

			// Token: 0x04001415 RID: 5141
			private ComparisonMode comparisonMode = ComparisonMode.IgnoreCase;

			// Token: 0x04001416 RID: 5142
			private string value;
		}

		// Token: 0x020002E7 RID: 743
		public sealed class ExcludesBitmask : SearchFilter.PropertyBasedFilter
		{
			// Token: 0x06001A38 RID: 6712 RVA: 0x00047603 File Offset: 0x00046603
			public ExcludesBitmask()
			{
			}

			// Token: 0x06001A39 RID: 6713 RVA: 0x0004760B File Offset: 0x0004660B
			public ExcludesBitmask(PropertyDefinitionBase propertyDefinition, int bitmask)
				: base(propertyDefinition)
			{
				this.bitmask = bitmask;
			}

			// Token: 0x06001A3A RID: 6714 RVA: 0x0004761B File Offset: 0x0004661B
			internal override string GetXmlElementName()
			{
				return "Excludes";
			}

			// Token: 0x06001A3B RID: 6715 RVA: 0x00047624 File Offset: 0x00046624
			internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
			{
				bool flag = base.TryReadElementFromXml(reader);
				if (!flag && reader.LocalName == "Bitmask")
				{
					this.bitmask = Convert.ToInt32(reader.ReadAttributeValue("Value"), 16);
				}
				return flag;
			}

			// Token: 0x06001A3C RID: 6716 RVA: 0x00047667 File Offset: 0x00046667
			internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
			{
				base.LoadFromJson(jsonProperty, service);
				this.bitmask = Convert.ToInt32(jsonProperty.ReadAsJsonObject("Bitmask").ReadAsString("Value"), 16);
			}

			// Token: 0x06001A3D RID: 6717 RVA: 0x00047693 File Offset: 0x00046693
			internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
			{
				base.WriteElementsToXml(writer);
				writer.WriteStartElement(XmlNamespace.Types, "Bitmask");
				writer.WriteAttributeValue("Value", this.Bitmask);
				writer.WriteEndElement();
			}

			// Token: 0x06001A3E RID: 6718 RVA: 0x000476C4 File Offset: 0x000466C4
			internal override object InternalToJson(ExchangeService service)
			{
				JsonObject jsonObject = base.InternalToJson(service) as JsonObject;
				JsonObject jsonObject2 = new JsonObject();
				jsonObject2.Add("Value", "0x" + this.Bitmask.ToString("X", CultureInfo.InvariantCulture));
				return jsonObject;
			}

			// Token: 0x1700064F RID: 1615
			// (get) Token: 0x06001A3F RID: 6719 RVA: 0x00047712 File Offset: 0x00046712
			// (set) Token: 0x06001A40 RID: 6720 RVA: 0x0004771A File Offset: 0x0004671A
			public int Bitmask
			{
				get
				{
					return this.bitmask;
				}
				set
				{
					this.SetFieldValue<int>(ref this.bitmask, value);
				}
			}

			// Token: 0x04001417 RID: 5143
			private int bitmask;
		}

		// Token: 0x020002E8 RID: 744
		public sealed class Exists : SearchFilter.PropertyBasedFilter
		{
			// Token: 0x06001A41 RID: 6721 RVA: 0x00047729 File Offset: 0x00046729
			public Exists()
			{
			}

			// Token: 0x06001A42 RID: 6722 RVA: 0x00047731 File Offset: 0x00046731
			public Exists(PropertyDefinitionBase propertyDefinition)
				: base(propertyDefinition)
			{
			}

			// Token: 0x06001A43 RID: 6723 RVA: 0x0004773A File Offset: 0x0004673A
			internal override string GetXmlElementName()
			{
				return "Exists";
			}
		}

		// Token: 0x020002E9 RID: 745
		[EditorBrowsable(1)]
		public abstract class RelationalFilter : SearchFilter.PropertyBasedFilter
		{
			// Token: 0x06001A44 RID: 6724 RVA: 0x00047741 File Offset: 0x00046741
			internal RelationalFilter()
			{
			}

			// Token: 0x06001A45 RID: 6725 RVA: 0x00047749 File Offset: 0x00046749
			internal RelationalFilter(PropertyDefinitionBase propertyDefinition, PropertyDefinitionBase otherPropertyDefinition)
				: base(propertyDefinition)
			{
				this.otherPropertyDefinition = otherPropertyDefinition;
			}

			// Token: 0x06001A46 RID: 6726 RVA: 0x00047759 File Offset: 0x00046759
			internal RelationalFilter(PropertyDefinitionBase propertyDefinition, object value)
				: base(propertyDefinition)
			{
				this.value = value;
			}

			// Token: 0x06001A47 RID: 6727 RVA: 0x0004776C File Offset: 0x0004676C
			internal override void InternalValidate()
			{
				base.InternalValidate();
				if (this.otherPropertyDefinition == null && this.value == null)
				{
					throw new ServiceValidationException(Strings.EqualityComparisonFilterIsInvalid);
				}
				if (this.value != null && !(this.value is IConvertible) && !(this.value is ISearchStringProvider))
				{
					throw new ServiceValidationException(string.Format(Strings.SearchFilterComparisonValueTypeIsNotSupported, this.value.GetType().Name));
				}
			}

			// Token: 0x06001A48 RID: 6728 RVA: 0x000477E8 File Offset: 0x000467E8
			internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
			{
				bool flag = base.TryReadElementFromXml(reader);
				if (!flag && reader.LocalName == "FieldURIOrConstant")
				{
					reader.Read();
					reader.EnsureCurrentNodeIsStartElement();
					if (reader.IsStartElement(XmlNamespace.Types, "Constant"))
					{
						this.value = reader.ReadAttributeValue("Value");
						flag = true;
					}
					else
					{
						flag = PropertyDefinitionBase.TryLoadFromXml(reader, ref this.otherPropertyDefinition);
					}
				}
				return flag;
			}

			// Token: 0x06001A49 RID: 6729 RVA: 0x00047850 File Offset: 0x00046850
			internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
			{
				base.LoadFromJson(jsonProperty, service);
				JsonObject jsonObject = jsonProperty.ReadAsJsonObject("FieldURIOrConstant").ReadAsJsonObject("Item");
				if (jsonObject.ReadTypeString() == "Constant")
				{
					this.value = jsonObject["Value"];
					return;
				}
				this.otherPropertyDefinition = PropertyDefinitionBase.TryLoadFromJson(jsonProperty);
			}

			// Token: 0x06001A4A RID: 6730 RVA: 0x000478AC File Offset: 0x000468AC
			internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
			{
				base.WriteElementsToXml(writer);
				writer.WriteStartElement(XmlNamespace.Types, "FieldURIOrConstant");
				if (this.Value != null)
				{
					writer.WriteStartElement(XmlNamespace.Types, "Constant");
					writer.WriteAttributeValue("Value", true, this.Value);
					writer.WriteEndElement();
				}
				else
				{
					this.OtherPropertyDefinition.WriteToXml(writer);
				}
				writer.WriteEndElement();
			}

			// Token: 0x06001A4B RID: 6731 RVA: 0x0004790C File Offset: 0x0004690C
			internal override object InternalToJson(ExchangeService service)
			{
				JsonObject jsonObject = base.InternalToJson(service) as JsonObject;
				JsonObject jsonObject2 = new JsonObject();
				if (this.Value != null)
				{
					JsonObject jsonObject3 = new JsonObject();
					jsonObject3.Add("Value", this.Value);
					jsonObject3.AddTypeParameter("Constant");
					jsonObject2.Add("Item", jsonObject3);
				}
				else
				{
					jsonObject2.Add("Item", ((IJsonSerializable)this.OtherPropertyDefinition).ToJson(service));
				}
				jsonObject.Add("FieldURIOrConstant", jsonObject2);
				return jsonObject;
			}

			// Token: 0x17000650 RID: 1616
			// (get) Token: 0x06001A4C RID: 6732 RVA: 0x00047988 File Offset: 0x00046988
			// (set) Token: 0x06001A4D RID: 6733 RVA: 0x00047990 File Offset: 0x00046990
			public PropertyDefinitionBase OtherPropertyDefinition
			{
				get
				{
					return this.otherPropertyDefinition;
				}
				set
				{
					this.SetFieldValue<PropertyDefinitionBase>(ref this.otherPropertyDefinition, value);
					this.value = null;
				}
			}

			// Token: 0x17000651 RID: 1617
			// (get) Token: 0x06001A4E RID: 6734 RVA: 0x000479A6 File Offset: 0x000469A6
			// (set) Token: 0x06001A4F RID: 6735 RVA: 0x000479AE File Offset: 0x000469AE
			public object Value
			{
				get
				{
					return this.value;
				}
				set
				{
					this.SetFieldValue<object>(ref this.value, value);
					this.otherPropertyDefinition = null;
				}
			}

			// Token: 0x04001418 RID: 5144
			private PropertyDefinitionBase otherPropertyDefinition;

			// Token: 0x04001419 RID: 5145
			private object value;
		}

		// Token: 0x020002EA RID: 746
		public sealed class IsEqualTo : SearchFilter.RelationalFilter
		{
			// Token: 0x06001A50 RID: 6736 RVA: 0x000479C4 File Offset: 0x000469C4
			public IsEqualTo()
			{
			}

			// Token: 0x06001A51 RID: 6737 RVA: 0x000479CC File Offset: 0x000469CC
			public IsEqualTo(PropertyDefinitionBase propertyDefinition, PropertyDefinitionBase otherPropertyDefinition)
				: base(propertyDefinition, otherPropertyDefinition)
			{
			}

			// Token: 0x06001A52 RID: 6738 RVA: 0x000479D6 File Offset: 0x000469D6
			public IsEqualTo(PropertyDefinitionBase propertyDefinition, object value)
				: base(propertyDefinition, value)
			{
			}

			// Token: 0x06001A53 RID: 6739 RVA: 0x000479E0 File Offset: 0x000469E0
			internal override string GetXmlElementName()
			{
				return "IsEqualTo";
			}
		}

		// Token: 0x020002EB RID: 747
		public sealed class IsNotEqualTo : SearchFilter.RelationalFilter
		{
			// Token: 0x06001A54 RID: 6740 RVA: 0x000479E7 File Offset: 0x000469E7
			public IsNotEqualTo()
			{
			}

			// Token: 0x06001A55 RID: 6741 RVA: 0x000479EF File Offset: 0x000469EF
			public IsNotEqualTo(PropertyDefinitionBase propertyDefinition, PropertyDefinitionBase otherPropertyDefinition)
				: base(propertyDefinition, otherPropertyDefinition)
			{
			}

			// Token: 0x06001A56 RID: 6742 RVA: 0x000479F9 File Offset: 0x000469F9
			public IsNotEqualTo(PropertyDefinitionBase propertyDefinition, object value)
				: base(propertyDefinition, value)
			{
			}

			// Token: 0x06001A57 RID: 6743 RVA: 0x00047A03 File Offset: 0x00046A03
			internal override string GetXmlElementName()
			{
				return "IsNotEqualTo";
			}
		}

		// Token: 0x020002EC RID: 748
		public sealed class IsGreaterThan : SearchFilter.RelationalFilter
		{
			// Token: 0x06001A58 RID: 6744 RVA: 0x00047A0A File Offset: 0x00046A0A
			public IsGreaterThan()
			{
			}

			// Token: 0x06001A59 RID: 6745 RVA: 0x00047A12 File Offset: 0x00046A12
			public IsGreaterThan(PropertyDefinitionBase propertyDefinition, PropertyDefinitionBase otherPropertyDefinition)
				: base(propertyDefinition, otherPropertyDefinition)
			{
			}

			// Token: 0x06001A5A RID: 6746 RVA: 0x00047A1C File Offset: 0x00046A1C
			public IsGreaterThan(PropertyDefinitionBase propertyDefinition, object value)
				: base(propertyDefinition, value)
			{
			}

			// Token: 0x06001A5B RID: 6747 RVA: 0x00047A26 File Offset: 0x00046A26
			internal override string GetXmlElementName()
			{
				return "IsGreaterThan";
			}
		}

		// Token: 0x020002ED RID: 749
		public sealed class IsGreaterThanOrEqualTo : SearchFilter.RelationalFilter
		{
			// Token: 0x06001A5C RID: 6748 RVA: 0x00047A2D File Offset: 0x00046A2D
			public IsGreaterThanOrEqualTo()
			{
			}

			// Token: 0x06001A5D RID: 6749 RVA: 0x00047A35 File Offset: 0x00046A35
			public IsGreaterThanOrEqualTo(PropertyDefinitionBase propertyDefinition, PropertyDefinitionBase otherPropertyDefinition)
				: base(propertyDefinition, otherPropertyDefinition)
			{
			}

			// Token: 0x06001A5E RID: 6750 RVA: 0x00047A3F File Offset: 0x00046A3F
			public IsGreaterThanOrEqualTo(PropertyDefinitionBase propertyDefinition, object value)
				: base(propertyDefinition, value)
			{
			}

			// Token: 0x06001A5F RID: 6751 RVA: 0x00047A49 File Offset: 0x00046A49
			internal override string GetXmlElementName()
			{
				return "IsGreaterThanOrEqualTo";
			}
		}

		// Token: 0x020002EE RID: 750
		public sealed class IsLessThan : SearchFilter.RelationalFilter
		{
			// Token: 0x06001A60 RID: 6752 RVA: 0x00047A50 File Offset: 0x00046A50
			public IsLessThan()
			{
			}

			// Token: 0x06001A61 RID: 6753 RVA: 0x00047A58 File Offset: 0x00046A58
			public IsLessThan(PropertyDefinitionBase propertyDefinition, PropertyDefinitionBase otherPropertyDefinition)
				: base(propertyDefinition, otherPropertyDefinition)
			{
			}

			// Token: 0x06001A62 RID: 6754 RVA: 0x00047A62 File Offset: 0x00046A62
			public IsLessThan(PropertyDefinitionBase propertyDefinition, object value)
				: base(propertyDefinition, value)
			{
			}

			// Token: 0x06001A63 RID: 6755 RVA: 0x00047A6C File Offset: 0x00046A6C
			internal override string GetXmlElementName()
			{
				return "IsLessThan";
			}
		}

		// Token: 0x020002EF RID: 751
		public sealed class IsLessThanOrEqualTo : SearchFilter.RelationalFilter
		{
			// Token: 0x06001A64 RID: 6756 RVA: 0x00047A73 File Offset: 0x00046A73
			public IsLessThanOrEqualTo()
			{
			}

			// Token: 0x06001A65 RID: 6757 RVA: 0x00047A7B File Offset: 0x00046A7B
			public IsLessThanOrEqualTo(PropertyDefinitionBase propertyDefinition, PropertyDefinitionBase otherPropertyDefinition)
				: base(propertyDefinition, otherPropertyDefinition)
			{
			}

			// Token: 0x06001A66 RID: 6758 RVA: 0x00047A85 File Offset: 0x00046A85
			public IsLessThanOrEqualTo(PropertyDefinitionBase propertyDefinition, object value)
				: base(propertyDefinition, value)
			{
			}

			// Token: 0x06001A67 RID: 6759 RVA: 0x00047A8F File Offset: 0x00046A8F
			internal override string GetXmlElementName()
			{
				return "IsLessThanOrEqualTo";
			}
		}

		// Token: 0x020002F0 RID: 752
		public sealed class Not : SearchFilter
		{
			// Token: 0x06001A68 RID: 6760 RVA: 0x00047A96 File Offset: 0x00046A96
			public Not()
			{
			}

			// Token: 0x06001A69 RID: 6761 RVA: 0x00047A9E File Offset: 0x00046A9E
			public Not(SearchFilter searchFilter)
			{
				this.searchFilter = searchFilter;
			}

			// Token: 0x06001A6A RID: 6762 RVA: 0x00047AAD File Offset: 0x00046AAD
			private void SearchFilterChanged(ComplexProperty complexProperty)
			{
				this.Changed();
			}

			// Token: 0x06001A6B RID: 6763 RVA: 0x00047AB5 File Offset: 0x00046AB5
			internal override void InternalValidate()
			{
				if (this.searchFilter == null)
				{
					throw new ServiceValidationException(Strings.SearchFilterMustBeSet);
				}
			}

			// Token: 0x06001A6C RID: 6764 RVA: 0x00047ACF File Offset: 0x00046ACF
			internal override string GetXmlElementName()
			{
				return "Not";
			}

			// Token: 0x06001A6D RID: 6765 RVA: 0x00047AD6 File Offset: 0x00046AD6
			internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
			{
				this.searchFilter = SearchFilter.LoadFromXml(reader);
				return true;
			}

			// Token: 0x06001A6E RID: 6766 RVA: 0x00047AE5 File Offset: 0x00046AE5
			internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
			{
				this.searchFilter = SearchFilter.LoadSearchFilterFromJson(jsonProperty.ReadAsJsonObject("Item"), service);
			}

			// Token: 0x06001A6F RID: 6767 RVA: 0x00047AFE File Offset: 0x00046AFE
			internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
			{
				this.SearchFilter.WriteToXml(writer);
			}

			// Token: 0x06001A70 RID: 6768 RVA: 0x00047B0C File Offset: 0x00046B0C
			internal override object InternalToJson(ExchangeService service)
			{
				JsonObject jsonObject = base.InternalToJson(service) as JsonObject;
				jsonObject.Add("Item", this.SearchFilter.InternalToJson(service));
				return jsonObject;
			}

			// Token: 0x17000652 RID: 1618
			// (get) Token: 0x06001A71 RID: 6769 RVA: 0x00047B3E File Offset: 0x00046B3E
			// (set) Token: 0x06001A72 RID: 6770 RVA: 0x00047B48 File Offset: 0x00046B48
			public SearchFilter SearchFilter
			{
				get
				{
					return this.searchFilter;
				}
				set
				{
					if (this.searchFilter != null)
					{
						this.searchFilter.OnChange -= this.SearchFilterChanged;
					}
					this.SetFieldValue<SearchFilter>(ref this.searchFilter, value);
					if (this.searchFilter != null)
					{
						this.searchFilter.OnChange += this.SearchFilterChanged;
					}
				}
			}

			// Token: 0x0400141A RID: 5146
			private SearchFilter searchFilter;
		}

		// Token: 0x020002F1 RID: 753
		public sealed class SearchFilterCollection : SearchFilter, IEnumerable<SearchFilter>, IEnumerable
		{
			// Token: 0x06001A73 RID: 6771 RVA: 0x00047BA0 File Offset: 0x00046BA0
			public SearchFilterCollection()
			{
			}

			// Token: 0x06001A74 RID: 6772 RVA: 0x00047BB3 File Offset: 0x00046BB3
			public SearchFilterCollection(LogicalOperator logicalOperator)
			{
				this.logicalOperator = logicalOperator;
			}

			// Token: 0x06001A75 RID: 6773 RVA: 0x00047BCD File Offset: 0x00046BCD
			public SearchFilterCollection(LogicalOperator logicalOperator, params SearchFilter[] searchFilters)
				: this(logicalOperator)
			{
				this.AddRange(searchFilters);
			}

			// Token: 0x06001A76 RID: 6774 RVA: 0x00047BDD File Offset: 0x00046BDD
			public SearchFilterCollection(LogicalOperator logicalOperator, IEnumerable<SearchFilter> searchFilters)
				: this(logicalOperator)
			{
				this.AddRange(searchFilters);
			}

			// Token: 0x06001A77 RID: 6775 RVA: 0x00047BF0 File Offset: 0x00046BF0
			internal override void InternalValidate()
			{
				for (int i = 0; i < this.Count; i++)
				{
					try
					{
						this[i].InternalValidate();
					}
					catch (ServiceValidationException ex)
					{
						throw new ServiceValidationException(string.Format(Strings.SearchFilterAtIndexIsInvalid, i), ex);
					}
				}
			}

			// Token: 0x06001A78 RID: 6776 RVA: 0x00047C4C File Offset: 0x00046C4C
			private void SearchFilterChanged(ComplexProperty complexProperty)
			{
				this.Changed();
			}

			// Token: 0x06001A79 RID: 6777 RVA: 0x00047C54 File Offset: 0x00046C54
			internal override string GetXmlElementName()
			{
				return this.LogicalOperator.ToString();
			}

			// Token: 0x06001A7A RID: 6778 RVA: 0x00047C66 File Offset: 0x00046C66
			internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
			{
				this.Add(SearchFilter.LoadFromXml(reader));
				return true;
			}

			// Token: 0x06001A7B RID: 6779 RVA: 0x00047C78 File Offset: 0x00046C78
			internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
			{
				object[] array = jsonProperty.ReadAsArray("Item");
				foreach (object obj in array)
				{
					this.Add(SearchFilter.LoadSearchFilterFromJson(obj as JsonObject, service));
				}
			}

			// Token: 0x06001A7C RID: 6780 RVA: 0x00047CB8 File Offset: 0x00046CB8
			internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
			{
				foreach (SearchFilter searchFilter in this)
				{
					searchFilter.WriteToXml(writer);
				}
			}

			// Token: 0x06001A7D RID: 6781 RVA: 0x00047D00 File Offset: 0x00046D00
			internal override void WriteToXml(EwsServiceXmlWriter writer)
			{
				if (this.Count == 1)
				{
					this[0].WriteToXml(writer);
					return;
				}
				base.WriteToXml(writer);
			}

			// Token: 0x06001A7E RID: 6782 RVA: 0x00047D20 File Offset: 0x00046D20
			internal override object InternalToJson(ExchangeService service)
			{
				JsonObject jsonObject = base.InternalToJson(service) as JsonObject;
				List<object> list = new List<object>();
				foreach (SearchFilter searchFilter in this)
				{
					list.Add(searchFilter.InternalToJson(service));
				}
				jsonObject.Add("Items", list.ToArray());
				return jsonObject;
			}

			// Token: 0x06001A7F RID: 6783 RVA: 0x00047D94 File Offset: 0x00046D94
			public void Add(SearchFilter searchFilter)
			{
				if (searchFilter == null)
				{
					throw new ArgumentNullException("searchFilter");
				}
				searchFilter.OnChange += this.SearchFilterChanged;
				this.searchFilters.Add(searchFilter);
				this.Changed();
			}

			// Token: 0x06001A80 RID: 6784 RVA: 0x00047DC8 File Offset: 0x00046DC8
			public void AddRange(IEnumerable<SearchFilter> searchFilters)
			{
				if (searchFilters == null)
				{
					throw new ArgumentNullException("searchFilters");
				}
				foreach (SearchFilter searchFilter in searchFilters)
				{
					searchFilter.OnChange += this.SearchFilterChanged;
				}
				this.searchFilters.AddRange(searchFilters);
				this.Changed();
			}

			// Token: 0x06001A81 RID: 6785 RVA: 0x00047E3C File Offset: 0x00046E3C
			public void Clear()
			{
				if (this.Count > 0)
				{
					foreach (SearchFilter searchFilter in this)
					{
						searchFilter.OnChange -= this.SearchFilterChanged;
					}
					this.searchFilters.Clear();
					this.Changed();
				}
			}

			// Token: 0x06001A82 RID: 6786 RVA: 0x00047EAC File Offset: 0x00046EAC
			public bool Contains(SearchFilter searchFilter)
			{
				return this.searchFilters.Contains(searchFilter);
			}

			// Token: 0x06001A83 RID: 6787 RVA: 0x00047EBA File Offset: 0x00046EBA
			public void Remove(SearchFilter searchFilter)
			{
				if (searchFilter == null)
				{
					throw new ArgumentNullException("searchFilter");
				}
				if (this.Contains(searchFilter))
				{
					searchFilter.OnChange -= this.SearchFilterChanged;
					this.searchFilters.Remove(searchFilter);
					this.Changed();
				}
			}

			// Token: 0x06001A84 RID: 6788 RVA: 0x00047EF8 File Offset: 0x00046EF8
			public void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
				}
				this[index].OnChange -= this.SearchFilterChanged;
				this.searchFilters.RemoveAt(index);
				this.Changed();
			}

			// Token: 0x17000653 RID: 1619
			// (get) Token: 0x06001A85 RID: 6789 RVA: 0x00047F51 File Offset: 0x00046F51
			public int Count
			{
				get
				{
					return this.searchFilters.Count;
				}
			}

			// Token: 0x17000654 RID: 1620
			public SearchFilter this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
					}
					return this.searchFilters[index];
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
					}
					this.searchFilters[index] = value;
				}
			}

			// Token: 0x17000655 RID: 1621
			// (get) Token: 0x06001A88 RID: 6792 RVA: 0x00047FBF File Offset: 0x00046FBF
			// (set) Token: 0x06001A89 RID: 6793 RVA: 0x00047FC7 File Offset: 0x00046FC7
			public LogicalOperator LogicalOperator
			{
				get
				{
					return this.logicalOperator;
				}
				set
				{
					this.logicalOperator = value;
				}
			}

			// Token: 0x06001A8A RID: 6794 RVA: 0x00047FD0 File Offset: 0x00046FD0
			public IEnumerator<SearchFilter> GetEnumerator()
			{
				return this.searchFilters.GetEnumerator();
			}

			// Token: 0x06001A8B RID: 6795 RVA: 0x00047FE2 File Offset: 0x00046FE2
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.searchFilters.GetEnumerator();
			}

			// Token: 0x0400141B RID: 5147
			private List<SearchFilter> searchFilters = new List<SearchFilter>();

			// Token: 0x0400141C RID: 5148
			private LogicalOperator logicalOperator;
		}
	}
}
