using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002B6 RID: 694
	internal sealed class GetStreamingEventsResults
	{
		// Token: 0x060018D1 RID: 6353 RVA: 0x00043B79 File Offset: 0x00042B79
		internal GetStreamingEventsResults()
		{
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00043B8C File Offset: 0x00042B8C
		internal void LoadFromXml(EwsServiceXmlReader reader)
		{
			reader.ReadStartElement(XmlNamespace.Messages, "Notification");
			do
			{
				GetStreamingEventsResults.NotificationGroup notificationGroup = default(GetStreamingEventsResults.NotificationGroup);
				notificationGroup.SubscriptionId = reader.ReadElementValue(XmlNamespace.Types, "SubscriptionId");
				notificationGroup.Events = new Collection<NotificationEvent>();
				lock (this)
				{
					this.events.Add(notificationGroup);
				}
				do
				{
					reader.Read();
					if (reader.IsStartElement())
					{
						string localName = reader.LocalName;
						EventType eventType;
						if (GetEventsResults.XmlElementNameToEventTypeMap.TryGetValue(localName, ref eventType))
						{
							if (eventType == EventType.Status)
							{
								reader.ReadEndElementIfNecessary(XmlNamespace.Types, localName);
							}
							else
							{
								this.LoadNotificationEventFromXml(reader, localName, eventType, notificationGroup);
							}
						}
						else
						{
							reader.SkipCurrentElement();
						}
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Messages, "Notification"));
				reader.Read();
			}
			while (!reader.IsEndElement(XmlNamespace.Messages, "Notifications"));
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00043C64 File Offset: 0x00042C64
		private void LoadNotificationEventFromXml(EwsServiceXmlReader reader, string eventElementName, EventType eventType, GetStreamingEventsResults.NotificationGroup notifications)
		{
			DateTime dateTime = reader.ReadElementValue<DateTime>(XmlNamespace.Types, "TimeStamp");
			reader.Read();
			NotificationEvent notificationEvent;
			if (reader.LocalName == "FolderId")
			{
				notificationEvent = new FolderEvent(eventType, dateTime);
			}
			else
			{
				notificationEvent = new ItemEvent(eventType, dateTime);
			}
			notificationEvent.LoadFromXml(reader, eventElementName);
			notifications.Events.Add(notificationEvent);
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x00043CBD File Offset: 0x00042CBD
		internal Collection<GetStreamingEventsResults.NotificationGroup> Notifications
		{
			get
			{
				return this.events;
			}
		}

		// Token: 0x040013D7 RID: 5079
		private Collection<GetStreamingEventsResults.NotificationGroup> events = new Collection<GetStreamingEventsResults.NotificationGroup>();

		// Token: 0x020002B7 RID: 695
		internal struct NotificationGroup
		{
			// Token: 0x040013D8 RID: 5080
			internal string SubscriptionId;

			// Token: 0x040013D9 RID: 5081
			internal Collection<NotificationEvent> Events;
		}
	}
}
