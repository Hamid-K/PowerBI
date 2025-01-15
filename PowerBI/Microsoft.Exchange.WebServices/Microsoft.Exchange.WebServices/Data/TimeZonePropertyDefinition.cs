using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002DC RID: 732
	internal class TimeZonePropertyDefinition : PropertyDefinition
	{
		// Token: 0x060019E4 RID: 6628 RVA: 0x0004669C File Offset: 0x0004569C
		internal TimeZonePropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x000466AC File Offset: 0x000456AC
		internal override void LoadPropertyValueFromXml(EwsServiceXmlReader reader, PropertyBag propertyBag)
		{
			TimeZoneDefinition timeZoneDefinition = new TimeZoneDefinition();
			timeZoneDefinition.LoadFromXml(reader, base.XmlElementName);
			propertyBag[this] = timeZoneDefinition.ToTimeZoneInfo();
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000466DC File Offset: 0x000456DC
		internal override void LoadPropertyValueFromJson(object value, ExchangeService service, PropertyBag propertyBag)
		{
			TimeZoneDefinition timeZoneDefinition = new TimeZoneDefinition();
			JsonObject jsonObject = value as JsonObject;
			if (jsonObject != null)
			{
				timeZoneDefinition.LoadFromJson(jsonObject, service);
			}
			propertyBag[this] = timeZoneDefinition.ToTimeZoneInfo();
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00046710 File Offset: 0x00045710
		internal override void WritePropertyValueToXml(EwsServiceXmlWriter writer, PropertyBag propertyBag, bool isUpdateOperation)
		{
			TimeZoneInfo timeZoneInfo = (TimeZoneInfo)propertyBag[this];
			if (timeZoneInfo != null && (!writer.IsTimeZoneHeaderEmitted || timeZoneInfo != writer.Service.TimeZone))
			{
				TimeZoneDefinition timeZoneDefinition = new TimeZoneDefinition(timeZoneInfo);
				timeZoneDefinition.WriteToXml(writer, base.XmlElementName);
			}
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00046758 File Offset: 0x00045758
		internal override void WriteJsonValue(JsonObject jsonObject, PropertyBag propertyBag, ExchangeService service, bool isUpdateOperation)
		{
			TimeZoneInfo timeZoneInfo = propertyBag[this] as TimeZoneInfo;
			if (timeZoneInfo != null && timeZoneInfo != service.TimeZone)
			{
				TimeZoneDefinition timeZoneDefinition = new TimeZoneDefinition(timeZoneInfo);
				jsonObject.Add(base.XmlElementName, timeZoneDefinition.InternalToJson(service));
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x00046798 File Offset: 0x00045798
		public override Type Type
		{
			get
			{
				return typeof(TimeZoneInfo);
			}
		}
	}
}
