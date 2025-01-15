using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200014D RID: 333
	public sealed class AttendeeAvailability : ServiceResponse
	{
		// Token: 0x06001034 RID: 4148 RVA: 0x0002F69E File Offset: 0x0002E69E
		internal AttendeeAvailability()
		{
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0002F6BC File Offset: 0x0002E6BC
		internal void LoadFreeBusyViewFromXml(EwsServiceXmlReader reader, FreeBusyViewType viewType)
		{
			reader.ReadStartElement(XmlNamespace.Messages, "FreeBusyView");
			string text = reader.ReadElementValue(XmlNamespace.Types, "FreeBusyViewType");
			this.viewType = (FreeBusyViewType)Enum.Parse(typeof(FreeBusyViewType), text, false);
			do
			{
				reader.Read();
				string localName;
				if (reader.IsStartElement() && (localName = reader.LocalName) != null)
				{
					if (!(localName == "MergedFreeBusy"))
					{
						if (!(localName == "CalendarEventArray"))
						{
							if (localName == "WorkingHours")
							{
								this.workingHours = new WorkingHours();
								this.workingHours.LoadFromXml(reader, reader.LocalName);
							}
						}
						else
						{
							do
							{
								reader.Read();
								if (reader.IsStartElement(XmlNamespace.Types, "CalendarEvent"))
								{
									CalendarEvent calendarEvent = new CalendarEvent();
									calendarEvent.LoadFromXml(reader, "CalendarEvent");
									this.calendarEvents.Add(calendarEvent);
								}
							}
							while (!reader.IsEndElement(XmlNamespace.Types, "CalendarEventArray"));
						}
					}
					else
					{
						string text2 = reader.ReadElementValue();
						for (int i = 0; i < text2.Length; i++)
						{
							this.mergedFreeBusyStatus.Add((LegacyFreeBusyStatus)byte.Parse(text2.get_Chars(i).ToString()));
						}
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Messages, "FreeBusyView"));
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0002F7F9 File Offset: 0x0002E7F9
		public Collection<CalendarEvent> CalendarEvents
		{
			get
			{
				return this.calendarEvents;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0002F801 File Offset: 0x0002E801
		public FreeBusyViewType ViewType
		{
			get
			{
				return this.viewType;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0002F809 File Offset: 0x0002E809
		public Collection<LegacyFreeBusyStatus> MergedFreeBusyStatus
		{
			get
			{
				return this.mergedFreeBusyStatus;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x0002F811 File Offset: 0x0002E811
		public WorkingHours WorkingHours
		{
			get
			{
				return this.workingHours;
			}
		}

		// Token: 0x0400098F RID: 2447
		private Collection<CalendarEvent> calendarEvents = new Collection<CalendarEvent>();

		// Token: 0x04000990 RID: 2448
		private Collection<LegacyFreeBusyStatus> mergedFreeBusyStatus = new Collection<LegacyFreeBusyStatus>();

		// Token: 0x04000991 RID: 2449
		private FreeBusyViewType viewType;

		// Token: 0x04000992 RID: 2450
		private WorkingHours workingHours;
	}
}
