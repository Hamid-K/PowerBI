using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000A5 RID: 165
	public sealed class CalendarEvent : ComplexProperty
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x000191E3 File Offset: 0x000181E3
		internal CalendarEvent()
		{
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x000191EB File Offset: 0x000181EB
		public DateTime StartTime
		{
			get
			{
				return this.startTime;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x000191F3 File Offset: 0x000181F3
		public DateTime EndTime
		{
			get
			{
				return this.endTime;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x000191FB File Offset: 0x000181FB
		public LegacyFreeBusyStatus FreeBusyStatus
		{
			get
			{
				return this.freeBusyStatus;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x00019203 File Offset: 0x00018203
		public CalendarEventDetails Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001920C File Offset: 0x0001820C
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "StartTime")
				{
					this.startTime = reader.ReadElementValueAsUnbiasedDateTimeScopedToServiceTimeZone();
					return true;
				}
				if (localName == "EndTime")
				{
					this.endTime = reader.ReadElementValueAsUnbiasedDateTimeScopedToServiceTimeZone();
					return true;
				}
				if (localName == "BusyType")
				{
					this.freeBusyStatus = reader.ReadElementValue<LegacyFreeBusyStatus>();
					return true;
				}
				if (localName == "CalendarEventDetails")
				{
					this.details = new CalendarEventDetails();
					this.details.LoadFromXml(reader, reader.LocalName);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x000192A4 File Offset: 0x000182A4
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "StartTime"))
					{
						if (!(text2 == "EndTime"))
						{
							if (!(text2 == "BusyType"))
							{
								if (text2 == "CalendarEventDetails")
								{
									this.details = new CalendarEventDetails();
									this.details.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
								}
							}
							else
							{
								this.freeBusyStatus = jsonProperty.ReadEnumValue<LegacyFreeBusyStatus>(text);
							}
						}
						else
						{
							this.endTime = EwsUtilities.ParseAsUnbiasedDatetimescopedToServicetimeZone(jsonProperty.ReadAsString(text), service);
						}
					}
					else
					{
						this.startTime = EwsUtilities.ParseAsUnbiasedDatetimescopedToServicetimeZone(jsonProperty.ReadAsString(text), service);
					}
				}
			}
		}

		// Token: 0x0400026A RID: 618
		private DateTime startTime;

		// Token: 0x0400026B RID: 619
		private DateTime endTime;

		// Token: 0x0400026C RID: 620
		private LegacyFreeBusyStatus freeBusyStatus;

		// Token: 0x0400026D RID: 621
		private CalendarEventDetails details;
	}
}
