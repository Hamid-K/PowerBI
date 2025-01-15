using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002D6 RID: 726
	internal class MeetingTimeZonePropertyDefinition : PropertyDefinition
	{
		// Token: 0x060019C4 RID: 6596 RVA: 0x00045F06 File Offset: 0x00044F06
		internal MeetingTimeZonePropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00045F14 File Offset: 0x00044F14
		internal sealed override void LoadPropertyValueFromXml(EwsServiceXmlReader reader, PropertyBag propertyBag)
		{
			MeetingTimeZone meetingTimeZone = new MeetingTimeZone();
			meetingTimeZone.LoadFromXml(reader, base.XmlElementName);
			propertyBag[AppointmentSchema.StartTimeZone] = meetingTimeZone.ToTimeZoneInfo();
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00045F48 File Offset: 0x00044F48
		internal override void LoadPropertyValueFromJson(object value, ExchangeService service, PropertyBag propertyBag)
		{
			JsonObject jsonObject = value as JsonObject;
			if (jsonObject != null)
			{
				MeetingTimeZone meetingTimeZone = new MeetingTimeZone();
				meetingTimeZone.LoadFromJson(jsonObject, service);
				propertyBag[AppointmentSchema.StartTimeZone] = meetingTimeZone.ToTimeZoneInfo();
			}
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00045F80 File Offset: 0x00044F80
		internal override void WritePropertyValueToXml(EwsServiceXmlWriter writer, PropertyBag propertyBag, bool isUpdateOperation)
		{
			MeetingTimeZone meetingTimeZone = (MeetingTimeZone)propertyBag[this];
			if (meetingTimeZone != null)
			{
				meetingTimeZone.WriteToXml(writer, base.XmlElementName);
			}
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00045FAC File Offset: 0x00044FAC
		internal override void WriteJsonValue(JsonObject jsonObject, PropertyBag propertyBag, ExchangeService service, bool isUpdateOperation)
		{
			MeetingTimeZone meetingTimeZone = propertyBag[this] as MeetingTimeZone;
			if (meetingTimeZone != null)
			{
				jsonObject.Add(base.XmlElementName, meetingTimeZone.InternalToJson(service));
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x00045FDC File Offset: 0x00044FDC
		public override Type Type
		{
			get
			{
				return typeof(MeetingTimeZone);
			}
		}
	}
}
