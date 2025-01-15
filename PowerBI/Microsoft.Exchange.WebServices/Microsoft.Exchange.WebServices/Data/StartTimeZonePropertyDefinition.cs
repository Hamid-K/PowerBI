using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002DD RID: 733
	internal class StartTimeZonePropertyDefinition : TimeZonePropertyDefinition
	{
		// Token: 0x060019EA RID: 6634 RVA: 0x000467A4 File Offset: 0x000457A4
		internal StartTimeZonePropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x000467B1 File Offset: 0x000457B1
		internal override void RegisterAssociatedInternalProperties(List<PropertyDefinition> properties)
		{
			base.RegisterAssociatedInternalProperties(properties);
			properties.Add(AppointmentSchema.MeetingTimeZone);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000467C8 File Offset: 0x000457C8
		internal override void WritePropertyValueToXml(EwsServiceXmlWriter writer, PropertyBag propertyBag, bool isUpdateOperation)
		{
			object obj = propertyBag[this];
			if (obj != null)
			{
				if (writer.Service.RequestedServerVersion == ExchangeVersion.Exchange2007_SP1)
				{
					ExchangeService exchangeService = writer.Service as ExchangeService;
					if (exchangeService != null && !exchangeService.Exchange2007CompatibilityMode)
					{
						MeetingTimeZone meetingTimeZone = new MeetingTimeZone((TimeZoneInfo)obj);
						meetingTimeZone.WriteToXml(writer, "MeetingTimeZone");
						return;
					}
				}
				else
				{
					base.WritePropertyValueToXml(writer, propertyBag, isUpdateOperation);
				}
			}
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00046826 File Offset: 0x00045826
		internal override void WriteToXml(EwsServiceXmlWriter writer)
		{
			if (writer.Service.RequestedServerVersion == ExchangeVersion.Exchange2007_SP1)
			{
				AppointmentSchema.MeetingTimeZone.WriteToXml(writer);
				return;
			}
			base.WriteToXml(writer);
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x00046848 File Offset: 0x00045848
		internal override bool HasFlag(PropertyDefinitionFlags flag, ExchangeVersion? version)
		{
			if (version != null && version.Value == ExchangeVersion.Exchange2007_SP1)
			{
				return AppointmentSchema.MeetingTimeZone.HasFlag(flag, version);
			}
			return base.HasFlag(flag, version);
		}
	}
}
