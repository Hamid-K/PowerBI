using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000174 RID: 372
	internal class GetServerTimeZonesResponse : ServiceResponse
	{
		// Token: 0x060010DD RID: 4317 RVA: 0x00031809 File Offset: 0x00030809
		internal GetServerTimeZonesResponse()
		{
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0003181C File Offset: 0x0003081C
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			reader.ReadStartElement(XmlNamespace.Messages, "TimeZoneDefinitions");
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.IsStartElement(XmlNamespace.Types, "TimeZoneDefinition"))
					{
						TimeZoneDefinition timeZoneDefinition = new TimeZoneDefinition();
						timeZoneDefinition.LoadFromXml(reader);
						this.timeZones.Add(timeZoneDefinition.ToTimeZoneInfo());
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Messages, "TimeZoneDefinitions"));
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x00031884 File Offset: 0x00030884
		public Collection<TimeZoneInfo> TimeZones
		{
			get
			{
				return this.timeZones;
			}
		}

		// Token: 0x040009CE RID: 2510
		private Collection<TimeZoneInfo> timeZones = new Collection<TimeZoneInfo>();
	}
}
