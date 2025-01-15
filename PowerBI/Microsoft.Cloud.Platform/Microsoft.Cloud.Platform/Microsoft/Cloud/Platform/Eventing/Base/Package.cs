using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003CF RID: 975
	public class Package : IPackage
	{
		// Token: 0x06001E17 RID: 7703 RVA: 0x00071A1C File Offset: 0x0006FC1C
		public Package([NotNull] PackageMetadata packageMetadata, int eventCount, IEventingServer eventingServer)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<PackageMetadata>(packageMetadata, "packageMetadata");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(eventCount, "eventCount");
			this.m_pmd = packageMetadata;
			this.m_events = new EventMetadata[eventCount];
			this.m_enabled = new int[eventCount];
			this.m_enabledLock = new object();
			this.m_eventingServer = eventingServer;
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00071A76 File Offset: 0x0006FC76
		public void Set([NotNull] EventMetadata metadata)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<EventMetadata>(metadata, "metadata");
			this.m_events[metadata.Index] = metadata;
			if ((metadata.Attributes & EventAttributes.AlwaysEnabled) != EventAttributes.None)
			{
				this.m_enabled[metadata.Index] = 1;
			}
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x000034FD File Offset: 0x000016FD
		public bool IsEnabled(int index)
		{
			return true;
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x00071AA9 File Offset: 0x0006FCA9
		public EventFireResult Fire(WireEventBase evt)
		{
			this.m_eventingServer.SubmitEvent(evt);
			return EventFireResult.Fired;
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x00071AB8 File Offset: 0x0006FCB8
		public PackageMetadata Metadata
		{
			get
			{
				return this.m_pmd;
			}
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x00071AC0 File Offset: 0x0006FCC0
		public void Reconfigure(HashSet<EventIdentifier> events)
		{
			object enabledLock = this.m_enabledLock;
			lock (enabledLock)
			{
				for (int i = 0; i < this.m_enabled.Length; i++)
				{
					bool flag2 = false;
					foreach (EventIdentifier eventIdentifier in events)
					{
						if (this.m_events[i].Matches(eventIdentifier))
						{
							this.m_enabled[i] = 1;
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						if ((this.m_events[i].Attributes & EventAttributes.AlwaysEnabled) != EventAttributes.None)
						{
							this.m_enabled[i] = 1;
						}
						else
						{
							this.m_enabled[i] = 0;
						}
					}
				}
			}
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00071B90 File Offset: 0x0006FD90
		public bool IsEnabled(EventIdentifier eid)
		{
			int num = this.Find(eid);
			if (num == -1)
			{
				throw new EventNotInPackageException(eid, this);
			}
			return this.IsEnabled(num);
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00071BB8 File Offset: 0x0006FDB8
		public IEnumerable<EventMetadata> GetEvents()
		{
			return this.m_events.ToList<EventMetadata>();
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x00071BC5 File Offset: 0x0006FDC5
		public bool Contains(EventIdentifier eid)
		{
			return this.Find(eid) != -1;
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x00071BD4 File Offset: 0x0006FDD4
		public EventMetadata GetEventMetadata(EventIdentifier eid)
		{
			int num = this.Find(eid);
			if (num == -1)
			{
				throw new EventNotInPackageException(eid, this);
			}
			return this.m_events[num];
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x00071C00 File Offset: 0x0006FE00
		private int Find(EventIdentifier eid)
		{
			if (this.Metadata.Id.Equals(EventsKitIdentifiers.GetEventsKitId(eid.EventId)))
			{
				for (int i = 0; i < this.m_events.Length; i++)
				{
					if (this.m_events[i].Matches(eid))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x04000A59 RID: 2649
		private IEventingServer m_eventingServer;

		// Token: 0x04000A5A RID: 2650
		private EventMetadata[] m_events;

		// Token: 0x04000A5B RID: 2651
		private int[] m_enabled;

		// Token: 0x04000A5C RID: 2652
		private object m_enabledLock;

		// Token: 0x04000A5D RID: 2653
		private PackageMetadata m_pmd;

		// Token: 0x04000A5E RID: 2654
		private const int InvalidIndex = -1;
	}
}
