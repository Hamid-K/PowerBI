using System;
using System.Linq;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.MonitoredUtils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x0200008C RID: 140
	internal class MonitoredErrorSinkServices : IMonitoredErrorSinkServices
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x0000E8C7 File Offset: 0x0000CAC7
		public MonitoredErrorSinkServices()
		{
			this.m_windowsEventLogAttributeCache = new AttributeCache<WindowsEventLogBaseAttribute>();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000E8DA File Offset: 0x0000CADA
		public void Initialize(ISinkServices sinkServices)
		{
			this.m_sinkServices = sinkServices;
			this.m_eventsKitExplorer = this.m_sinkServices.GetEventsKitExplorer();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000E8F4 File Offset: 0x0000CAF4
		public string GetFriendlyName(WireEventBase publishedEvent)
		{
			string text;
			try
			{
				IEventMetadata eventMetadata = this.m_eventsKitExplorer.GetEventMetadata(new EventsKitIdentifiers(publishedEvent.Id.EventId));
				text = eventMetadata.EventMethod.DeclaringType.FullName + "." + eventMetadata.EventMethod.Name;
			}
			catch (EventMetadataNotFoundException ex)
			{
				throw new MonitoredErrorSinkServicesException(ex);
			}
			return text;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000E95C File Offset: 0x0000CB5C
		public bool DoesEventIndicateActivityCompletedSuccessfully(WireEventBase publishedEvent)
		{
			long num = publishedEvent.FullIdToEventId();
			if (num.Equals(5071707678334261657L) || num.Equals(5464362666731113465L))
			{
				return true;
			}
			if (num.Equals(6994294237486268222L) || num.Equals(7527287753132401043L))
			{
				EventParameter eventParameter = publishedEvent.EventParameters.FirstOrDefault((EventParameter parameter) => parameter.Type == typeof(ActivityEndedWith));
				if (eventParameter != null && eventParameter.Value is ActivityEndedWith)
				{
					ActivityEndedWith activityEndedWith = (ActivityEndedWith)eventParameter.Value;
					return activityEndedWith - ActivityEndedWith.Success <= 1;
				}
			}
			return false;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000EA0C File Offset: 0x0000CC0C
		public bool DoesEventIndicateActivityCompletedWithFailure(WireEventBase publishedEvent)
		{
			long num = publishedEvent.FullIdToEventId();
			if (num.Equals(2187010839073174704L) || num.Equals(5867609055991075014L))
			{
				return true;
			}
			if (num.Equals(6994294237486268222L) || num.Equals(7527287753132401043L))
			{
				EventParameter eventParameter = publishedEvent.EventParameters.FirstOrDefault((EventParameter parameter) => parameter.Type == typeof(ActivityEndedWith));
				if (eventParameter != null && eventParameter.Value is ActivityEndedWith)
				{
					ActivityEndedWith activityEndedWith = (ActivityEndedWith)eventParameter.Value;
					return activityEndedWith - ActivityEndedWith.Error <= 1;
				}
			}
			return false;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000EABC File Offset: 0x0000CCBC
		public bool TryGetWindowsEventLogMetadata(WireEventBase publishedEvent, out WindowsEventLogBaseAttribute attribute)
		{
			bool flag;
			try
			{
				flag = this.m_windowsEventLogAttributeCache.TryGetAttribute(publishedEvent, this.m_eventsKitExplorer, out attribute);
			}
			catch (EventMetadataNotFoundException ex)
			{
				throw new MonitoredErrorSinkServicesException(ex);
			}
			return flag;
		}

		// Token: 0x04000157 RID: 343
		private ISinkServices m_sinkServices;

		// Token: 0x04000158 RID: 344
		private readonly AttributeCache<WindowsEventLogBaseAttribute> m_windowsEventLogAttributeCache;

		// Token: 0x04000159 RID: 345
		private IEventsKitExplorer m_eventsKitExplorer;
	}
}
