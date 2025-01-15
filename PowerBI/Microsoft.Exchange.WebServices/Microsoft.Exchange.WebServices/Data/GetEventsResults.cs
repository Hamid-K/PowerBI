using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002B5 RID: 693
	public sealed class GetEventsResults
	{
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x000437E9 File Offset: 0x000427E9
		internal static Dictionary<string, EventType> XmlElementNameToEventTypeMap
		{
			get
			{
				return GetEventsResults.xmlElementNameToEventTypeMap.Member;
			}
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x000437F5 File Offset: 0x000427F5
		internal GetEventsResults()
		{
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x00043808 File Offset: 0x00042808
		internal void LoadFromXml(EwsServiceXmlReader reader)
		{
			reader.ReadStartElement(XmlNamespace.Messages, "Notification");
			this.subscriptionId = reader.ReadElementValue(XmlNamespace.Types, "SubscriptionId");
			this.previousWatermark = reader.ReadElementValue(XmlNamespace.Types, "PreviousWatermark");
			this.moreEventsAvailable = reader.ReadElementValue<bool>(XmlNamespace.Types, "MoreEvents");
			do
			{
				reader.Read();
				if (reader.IsStartElement())
				{
					string localName = reader.LocalName;
					EventType eventType;
					if (GetEventsResults.xmlElementNameToEventTypeMap.Member.TryGetValue(localName, ref eventType))
					{
						this.newWatermark = reader.ReadElementValue(XmlNamespace.Types, "Watermark");
						if (eventType == EventType.Status)
						{
							reader.ReadEndElementIfNecessary(XmlNamespace.Types, localName);
						}
						else
						{
							this.LoadNotificationEventFromXml(reader, localName, eventType);
						}
					}
					else
					{
						reader.SkipCurrentElement();
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Messages, "Notification"));
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000438C0 File Offset: 0x000428C0
		internal void LoadFromJson(JsonObject eventsResponse, ExchangeService service)
		{
			foreach (string text in eventsResponse.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "SubscriptionId"))
					{
						if (!(text2 == "PreviousWatermark"))
						{
							if (!(text2 == "MoreEvents"))
							{
								if (text2 == "Events")
								{
									this.LoadEventsFromJson(eventsResponse.ReadAsArray(text), service);
								}
							}
							else
							{
								this.moreEventsAvailable = eventsResponse.ReadAsBool(text);
							}
						}
						else
						{
							this.previousWatermark = eventsResponse.ReadAsString(text);
						}
					}
					else
					{
						this.subscriptionId = eventsResponse.ReadAsString(text);
					}
				}
			}
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x00043988 File Offset: 0x00042988
		private void LoadEventsFromJson(object[] jsonEventsArray, ExchangeService service)
		{
			foreach (JsonObject jsonObject in jsonEventsArray)
			{
				this.newWatermark = jsonObject.ReadAsString("Watermark");
				EventType eventType = jsonObject.ReadEnumValue<EventType>("NotificationType");
				if (eventType != EventType.Status)
				{
					NotificationEvent notificationEvent;
					if (jsonObject.ContainsKey("FolderId"))
					{
						notificationEvent = new FolderEvent(eventType, service.ConvertUniversalDateTimeStringToLocalDateTime(jsonObject.ReadAsString("TimeStamp")).Value);
					}
					else
					{
						notificationEvent = new ItemEvent(eventType, service.ConvertUniversalDateTimeStringToLocalDateTime(jsonObject.ReadAsString("TimeStamp")).Value);
					}
					notificationEvent.LoadFromJson(jsonObject, service);
					this.events.Add(notificationEvent);
				}
			}
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x00043A40 File Offset: 0x00042A40
		private void LoadNotificationEventFromXml(EwsServiceXmlReader reader, string eventElementName, EventType eventType)
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
			this.events.Add(notificationEvent);
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x00043A98 File Offset: 0x00042A98
		internal string SubscriptionId
		{
			get
			{
				return this.subscriptionId;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x00043AA0 File Offset: 0x00042AA0
		internal string PreviousWatermark
		{
			get
			{
				return this.previousWatermark;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x00043AA8 File Offset: 0x00042AA8
		internal string NewWatermark
		{
			get
			{
				return this.newWatermark;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x00043AB0 File Offset: 0x00042AB0
		internal bool MoreEventsAvailable
		{
			get
			{
				return this.moreEventsAvailable;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x00043AB8 File Offset: 0x00042AB8
		public IEnumerable<FolderEvent> FolderEvents
		{
			get
			{
				return Enumerable.OfType<FolderEvent>(this.events);
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x00043AC5 File Offset: 0x00042AC5
		public IEnumerable<ItemEvent> ItemEvents
		{
			get
			{
				return Enumerable.OfType<ItemEvent>(this.events);
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x00043AD2 File Offset: 0x00042AD2
		public Collection<NotificationEvent> AllEvents
		{
			get
			{
				return this.events;
			}
		}

		// Token: 0x040013D0 RID: 5072
		private static LazyMember<Dictionary<string, EventType>> xmlElementNameToEventTypeMap = new LazyMember<Dictionary<string, EventType>>(delegate
		{
			Dictionary<string, EventType> dictionary = new Dictionary<string, EventType>();
			dictionary.Add("CopiedEvent", EventType.Copied);
			dictionary.Add("CreatedEvent", EventType.Created);
			dictionary.Add("DeletedEvent", EventType.Deleted);
			dictionary.Add("ModifiedEvent", EventType.Modified);
			dictionary.Add("MovedEvent", EventType.Moved);
			dictionary.Add("NewMailEvent", EventType.NewMail);
			dictionary.Add("StatusEvent", EventType.Status);
			dictionary.Add("FreeBusyChangedEvent", EventType.FreeBusyChanged);
			return dictionary;
		});

		// Token: 0x040013D1 RID: 5073
		private string newWatermark;

		// Token: 0x040013D2 RID: 5074
		private string subscriptionId;

		// Token: 0x040013D3 RID: 5075
		private string previousWatermark;

		// Token: 0x040013D4 RID: 5076
		private bool moreEventsAvailable;

		// Token: 0x040013D5 RID: 5077
		private Collection<NotificationEvent> events = new Collection<NotificationEvent>();
	}
}
