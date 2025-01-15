using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002D0 RID: 720
	internal sealed class EffectiveRightsPropertyDefinition : PropertyDefinition
	{
		// Token: 0x06001986 RID: 6534 RVA: 0x00045240 File Offset: 0x00044240
		internal EffectiveRightsPropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00045250 File Offset: 0x00044250
		internal sealed override void LoadPropertyValueFromXml(EwsServiceXmlReader reader, PropertyBag propertyBag)
		{
			EffectiveRights effectiveRights = EffectiveRights.None;
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, base.XmlElementName);
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					string localName;
					if (reader.IsStartElement() && (localName = reader.LocalName) != null)
					{
						if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600186b-1 == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(7);
							dictionary.Add("CreateAssociated", 0);
							dictionary.Add("CreateContents", 1);
							dictionary.Add("CreateHierarchy", 2);
							dictionary.Add("Delete", 3);
							dictionary.Add("Modify", 4);
							dictionary.Add("Read", 5);
							dictionary.Add("ViewPrivateItems", 6);
							<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600186b-1 = dictionary;
						}
						int num;
						if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600186b-1.TryGetValue(localName, ref num))
						{
							switch (num)
							{
							case 0:
								if (reader.ReadElementValue<bool>())
								{
									effectiveRights |= EffectiveRights.CreateAssociated;
								}
								break;
							case 1:
								if (reader.ReadElementValue<bool>())
								{
									effectiveRights |= EffectiveRights.CreateContents;
								}
								break;
							case 2:
								if (reader.ReadElementValue<bool>())
								{
									effectiveRights |= EffectiveRights.CreateHierarchy;
								}
								break;
							case 3:
								if (reader.ReadElementValue<bool>())
								{
									effectiveRights |= EffectiveRights.Delete;
								}
								break;
							case 4:
								if (reader.ReadElementValue<bool>())
								{
									effectiveRights |= EffectiveRights.Modify;
								}
								break;
							case 5:
								if (reader.ReadElementValue<bool>())
								{
									effectiveRights |= EffectiveRights.Read;
								}
								break;
							case 6:
								if (reader.ReadElementValue<bool>())
								{
									effectiveRights |= EffectiveRights.ViewPrivateItems;
								}
								break;
							}
						}
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Types, base.XmlElementName));
			}
			propertyBag[this] = effectiveRights;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x000453BC File Offset: 0x000443BC
		internal override void LoadPropertyValueFromJson(object value, ExchangeService service, PropertyBag propertyBag)
		{
			EffectiveRights effectiveRights = EffectiveRights.None;
			JsonObject jsonObject = value as JsonObject;
			if (jsonObject != null)
			{
				foreach (string text in jsonObject.Keys)
				{
					string text2;
					if ((text2 = text) != null)
					{
						if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600186c-1 == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(7);
							dictionary.Add("CreateAssociated", 0);
							dictionary.Add("CreateContents", 1);
							dictionary.Add("CreateHierarchy", 2);
							dictionary.Add("Delete", 3);
							dictionary.Add("Modify", 4);
							dictionary.Add("Read", 5);
							dictionary.Add("ViewPrivateItems", 6);
							<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600186c-1 = dictionary;
						}
						int num;
						if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600186c-1.TryGetValue(text2, ref num))
						{
							switch (num)
							{
							case 0:
								if (jsonObject.ReadAsBool(text))
								{
									effectiveRights |= EffectiveRights.CreateAssociated;
								}
								break;
							case 1:
								if (jsonObject.ReadAsBool(text))
								{
									effectiveRights |= EffectiveRights.CreateContents;
								}
								break;
							case 2:
								if (jsonObject.ReadAsBool(text))
								{
									effectiveRights |= EffectiveRights.CreateHierarchy;
								}
								break;
							case 3:
								if (jsonObject.ReadAsBool(text))
								{
									effectiveRights |= EffectiveRights.Delete;
								}
								break;
							case 4:
								if (jsonObject.ReadAsBool(text))
								{
									effectiveRights |= EffectiveRights.Modify;
								}
								break;
							case 5:
								if (jsonObject.ReadAsBool(text))
								{
									effectiveRights |= EffectiveRights.Read;
								}
								break;
							case 6:
								if (jsonObject.ReadAsBool(text))
								{
									effectiveRights |= EffectiveRights.ViewPrivateItems;
								}
								break;
							}
						}
					}
				}
			}
			propertyBag[this] = value;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00045548 File Offset: 0x00044548
		internal override void WritePropertyValueToXml(EwsServiceXmlWriter writer, PropertyBag propertyBag, bool isUpdateOperation)
		{
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0004554A File Offset: 0x0004454A
		internal override void WriteJsonValue(JsonObject jsonObject, PropertyBag propertyBag, ExchangeService service, bool isUpdateOperation)
		{
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600198B RID: 6539 RVA: 0x0004554C File Offset: 0x0004454C
		public override Type Type
		{
			get
			{
				return typeof(EffectiveRights);
			}
		}
	}
}
