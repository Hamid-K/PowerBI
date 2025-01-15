using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000085 RID: 133
	public sealed class PhysicalAddressEntry : DictionaryEntryProperty<PhysicalAddressKey>
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x00014411 File Offset: 0x00013411
		public PhysicalAddressEntry()
		{
			this.propertyBag = new SimplePropertyBag<string>();
			this.propertyBag.OnChange += this.PropertyBagChanged;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0001443B File Offset: 0x0001343B
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x00014452 File Offset: 0x00013452
		public string Street
		{
			get
			{
				return (string)this.propertyBag["Street"];
			}
			set
			{
				this.propertyBag["Street"] = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00014465 File Offset: 0x00013465
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x0001447C File Offset: 0x0001347C
		public string City
		{
			get
			{
				return (string)this.propertyBag["City"];
			}
			set
			{
				this.propertyBag["City"] = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0001448F File Offset: 0x0001348F
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x000144A6 File Offset: 0x000134A6
		public string State
		{
			get
			{
				return (string)this.propertyBag["State"];
			}
			set
			{
				this.propertyBag["State"] = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x000144B9 File Offset: 0x000134B9
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x000144D0 File Offset: 0x000134D0
		public string CountryOrRegion
		{
			get
			{
				return (string)this.propertyBag["CountryOrRegion"];
			}
			set
			{
				this.propertyBag["CountryOrRegion"] = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x000144E3 File Offset: 0x000134E3
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x000144FA File Offset: 0x000134FA
		public string PostalCode
		{
			get
			{
				return (string)this.propertyBag["PostalCode"];
			}
			set
			{
				this.propertyBag["PostalCode"] = value;
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001450D File Offset: 0x0001350D
		internal override void ClearChangeLog()
		{
			this.propertyBag.ClearChangeLog();
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001451A File Offset: 0x0001351A
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			if (PhysicalAddressEntry.PhysicalAddressSchema.XmlElementNames.Contains(reader.LocalName))
			{
				this.propertyBag[reader.LocalName] = reader.ReadElementValue();
				return true;
			}
			return false;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00014548 File Offset: 0x00013548
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			foreach (string text in PhysicalAddressEntry.PhysicalAddressSchema.XmlElementNames)
			{
				writer.WriteElementValue(XmlNamespace.Types, text, this.propertyBag[text]);
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000145A8 File Offset: 0x000135A8
		internal override bool WriteSetUpdateToXml(EwsServiceXmlWriter writer, ServiceObject ewsObject, string ownerDictionaryXmlElementName)
		{
			List<string> list = new List<string>();
			foreach (string text in this.propertyBag.AddedItems)
			{
				list.Add(text);
			}
			foreach (string text2 in this.propertyBag.ModifiedItems)
			{
				list.Add(text2);
			}
			foreach (string text3 in list)
			{
				writer.WriteStartElement(XmlNamespace.Types, ewsObject.GetSetFieldXmlElementName());
				writer.WriteStartElement(XmlNamespace.Types, "IndexedFieldURI");
				writer.WriteAttributeValue("FieldURI", PhysicalAddressEntry.GetFieldUri(text3));
				writer.WriteAttributeValue("FieldIndex", base.Key.ToString());
				writer.WriteEndElement();
				writer.WriteStartElement(XmlNamespace.Types, ewsObject.GetXmlElementName());
				writer.WriteStartElement(XmlNamespace.Types, ownerDictionaryXmlElementName);
				writer.WriteStartElement(XmlNamespace.Types, "Entry");
				this.WriteAttributesToXml(writer);
				writer.WriteElementValue(XmlNamespace.Types, text3, this.propertyBag[text3]);
				writer.WriteEndElement();
				writer.WriteEndElement();
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
			foreach (string text4 in this.propertyBag.RemovedItems)
			{
				this.InternalWriteDeleteFieldToXml(writer, ewsObject, text4);
			}
			return true;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00014774 File Offset: 0x00013774
		internal override bool WriteSetUpdateToJson(ExchangeService service, ServiceObject ewsObject, PropertyDefinition propertyDefinition, List<JsonObject> updates)
		{
			List<string> list = new List<string>();
			list.AddRange(this.propertyBag.AddedItems);
			list.AddRange(this.propertyBag.ModifiedItems);
			foreach (string text in list)
			{
				JsonObject jsonObject = new JsonObject();
				jsonObject.AddTypeParameter(ewsObject.GetSetFieldXmlElementName());
				JsonObject jsonObject2 = new JsonObject();
				jsonObject2.AddTypeParameter("DictionaryPropertyUri");
				jsonObject2.Add("FieldURI", PhysicalAddressEntry.GetFieldUri(text));
				jsonObject2.Add("FieldIndex", base.Key.ToString());
				jsonObject.Add("Path", jsonObject2);
				JsonObject jsonObject3 = new JsonObject();
				jsonObject3.Add("Key", base.Key);
				jsonObject3.Add(text, this.propertyBag[text]);
				JsonObject jsonObject4 = new JsonObject();
				jsonObject4.AddTypeParameter(ewsObject.GetXmlElementName());
				jsonObject4.Add(propertyDefinition.XmlElementName, new JsonObject[] { jsonObject3 });
				jsonObject.Add(PropertyBag.GetPropertyUpdateItemName(ewsObject), jsonObject4);
				updates.Add(jsonObject);
			}
			foreach (string text2 in this.propertyBag.RemovedItems)
			{
				this.InternalWriteDeleteUpdateToJson(ewsObject, text2, updates);
			}
			return true;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001490C File Offset: 0x0001390C
		internal override bool WriteDeleteUpdateToXml(EwsServiceXmlWriter writer, ServiceObject ewsObject)
		{
			foreach (string text in PhysicalAddressEntry.PhysicalAddressSchema.XmlElementNames)
			{
				this.InternalWriteDeleteFieldToXml(writer, ewsObject, text);
			}
			return true;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00014964 File Offset: 0x00013964
		internal override bool WriteDeleteUpdateToJson(ExchangeService service, ServiceObject ewsObject, List<JsonObject> updates)
		{
			foreach (string text in PhysicalAddressEntry.PhysicalAddressSchema.XmlElementNames)
			{
				this.InternalWriteDeleteUpdateToJson(ewsObject, text, updates);
			}
			return true;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000149BC File Offset: 0x000139BC
		internal void InternalWriteDeleteUpdateToJson(ServiceObject ewsObject, string propertyName, List<JsonObject> updates)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.AddTypeParameter(ewsObject.GetDeleteFieldXmlElementName());
			JsonObject jsonObject2 = new JsonObject();
			jsonObject2.AddTypeParameter("DictionaryPropertyUri");
			jsonObject2.Add("FieldURI", PhysicalAddressEntry.GetFieldUri(propertyName));
			jsonObject2.Add("FieldIndex", base.Key.ToString());
			jsonObject.Add("Path", jsonObject2);
			updates.Add(jsonObject);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00014A2C File Offset: 0x00013A2C
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Key", base.Key);
			jsonObject.Add("Street", this.Street);
			jsonObject.Add("City", this.City);
			jsonObject.Add("State", this.State);
			jsonObject.Add("CountryOrRegion", this.CountryOrRegion);
			jsonObject.Add("PostalCode", this.PostalCode);
			return jsonObject;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00014AAC File Offset: 0x00013AAC
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			base.Key = jsonProperty.ReadEnumValue<PhysicalAddressKey>("Key");
			this.Street = jsonProperty.ReadAsString("Street");
			this.City = jsonProperty.ReadAsString("City");
			this.State = jsonProperty.ReadAsString("State");
			this.Street = jsonProperty.ReadAsString("Street");
			this.CountryOrRegion = jsonProperty.ReadAsString("CountryOrRegion");
			this.PostalCode = jsonProperty.ReadAsString("PostalCode");
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00014B30 File Offset: 0x00013B30
		private static string GetFieldUri(string xmlElementName)
		{
			return "contacts:PhysicalAddress:" + xmlElementName;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00014B3D File Offset: 0x00013B3D
		private void PropertyBagChanged()
		{
			this.Changed();
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00014B48 File Offset: 0x00013B48
		private void InternalWriteDeleteFieldToXml(EwsServiceXmlWriter writer, ServiceObject ewsObject, string fieldXmlElementName)
		{
			writer.WriteStartElement(XmlNamespace.Types, ewsObject.GetDeleteFieldXmlElementName());
			writer.WriteStartElement(XmlNamespace.Types, "IndexedFieldURI");
			writer.WriteAttributeValue("FieldURI", PhysicalAddressEntry.GetFieldUri(fieldXmlElementName));
			writer.WriteAttributeValue("FieldIndex", base.Key.ToString());
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x040001FC RID: 508
		private SimplePropertyBag<string> propertyBag;

		// Token: 0x02000086 RID: 134
		private static class PhysicalAddressSchema
		{
			// Token: 0x17000151 RID: 337
			// (get) Token: 0x0600060B RID: 1547 RVA: 0x00014BA6 File Offset: 0x00013BA6
			public static List<string> XmlElementNames
			{
				get
				{
					return PhysicalAddressEntry.PhysicalAddressSchema.xmlElementNames.Member;
				}
			}

			// Token: 0x040001FD RID: 509
			public const string Street = "Street";

			// Token: 0x040001FE RID: 510
			public const string City = "City";

			// Token: 0x040001FF RID: 511
			public const string State = "State";

			// Token: 0x04000200 RID: 512
			public const string CountryOrRegion = "CountryOrRegion";

			// Token: 0x04000201 RID: 513
			public const string PostalCode = "PostalCode";

			// Token: 0x04000202 RID: 514
			private static LazyMember<List<string>> xmlElementNames = new LazyMember<List<string>>(delegate
			{
				List<string> list = new List<string>();
				list.Add("Street");
				list.Add("City");
				list.Add("State");
				list.Add("CountryOrRegion");
				list.Add("PostalCode");
				return list;
			});
		}
	}
}
