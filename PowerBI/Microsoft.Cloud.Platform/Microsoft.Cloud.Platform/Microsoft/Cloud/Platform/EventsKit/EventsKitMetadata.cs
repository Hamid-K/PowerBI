using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000360 RID: 864
	public sealed class EventsKitMetadata : IEventsKitMetadata
	{
		// Token: 0x06001984 RID: 6532 RVA: 0x0005EC28 File Offset: 0x0005CE28
		public EventsKitMetadata(Type eventsKit)
		{
			this.m_eventsKitAttr = Auxiliary.GetCustomAttribute<EventsKitAttribute>(eventsKit);
			this.m_publishedEventAttr = Auxiliary.GetCustomAttribute<PublishedEventAttribute>(eventsKit);
			this.m_auditAttr = Auxiliary.GetCustomAttribute<AuditAttribute>(eventsKit);
			this.m_traceAttr = Auxiliary.GetCustomAttribute<TraceAttribute>(eventsKit);
			this.m_visibleAttr = Auxiliary.GetCustomAttribute<VisibleAttribute>(eventsKit);
			PerformanceCountersCategoryAttribute customAttribute = Auxiliary.GetCustomAttribute<PerformanceCountersCategoryAttribute>(eventsKit);
			this.EventsKitType = eventsKit;
			this.Id = new EventsKitIdentifiers(this.m_eventsKitAttr.Id);
			this.m_events = new Dictionary<long, EventsKitEventMetadata>();
			this.m_performanceCounterCategoy = ((customAttribute == null) ? new PerformanceCounterCategoryMetadata(this) : new PerformanceCounterCategoryMetadata(customAttribute, this));
			MethodInfo[] methods = eventsKit.GetMethods();
			for (int i = 0; i < methods.Length; i++)
			{
				EventsKitEventMetadata eventsKitEventMetadata = new EventsKitEventMetadata(methods[i], this);
				long eventId = eventsKitEventMetadata.Id.EventId;
				if (this.m_events.ContainsKey(eventId))
				{
					throw new InvalidOperationException(string.Concat(new object[] { "EventsKit ", eventsKit.Name, " contains duplicate event id: ", eventId }));
				}
				this.m_events.Add(eventId, eventsKitEventMetadata);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001985 RID: 6533 RVA: 0x0005ED3B File Offset: 0x0005CF3B
		public string Name
		{
			get
			{
				return this.EventsKitType.Name;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x0005ED48 File Offset: 0x0005CF48
		public int Priority
		{
			get
			{
				return this.m_eventsKitAttr.Priority;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001987 RID: 6535 RVA: 0x0005ED55 File Offset: 0x0005CF55
		public EventLevel Level
		{
			get
			{
				return this.m_eventsKitAttr.Level;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x0005ED62 File Offset: 0x0005CF62
		public string Namespace
		{
			get
			{
				return this.EventsKitType.Namespace;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001989 RID: 6537 RVA: 0x0005ED6F File Offset: 0x0005CF6F
		public string ClassName
		{
			get
			{
				return EventsKitMetadata.GetImplementationClassName(this.Name, false);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x0005ED7D File Offset: 0x0005CF7D
		public string MockClassName
		{
			get
			{
				return EventsKitMetadata.GetImplementationClassName(this.Name, true);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600198B RID: 6539 RVA: 0x0005ED8B File Offset: 0x0005CF8B
		public bool IsPublishedEvent
		{
			get
			{
				return this.m_publishedEventAttr != null && this.m_publishedEventAttr.Enable;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x0005EDA2 File Offset: 0x0005CFA2
		public bool IsPublishedToEventingServer
		{
			get
			{
				return this.m_publishedEventAttr != null && this.m_publishedEventAttr.PublishTo.HasFlag(PublishEventTo.PublishToEventingServer);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x0005EDC9 File Offset: 0x0005CFC9
		public bool IsPublishedToEtw
		{
			get
			{
				return this.m_publishedEventAttr != null && this.m_publishedEventAttr.PublishTo.HasFlag(PublishEventTo.PublishToEtw);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x0005EDF0 File Offset: 0x0005CFF0
		public bool IsAlwaysEnabled
		{
			get
			{
				return this.m_publishedEventAttr != null && this.m_publishedEventAttr.AlwaysEnabled;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x0005EE07 File Offset: 0x0005D007
		public bool IsAuditEvent
		{
			get
			{
				return this.m_auditAttr != null;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x0005EE12 File Offset: 0x0005D012
		public string PackageName
		{
			get
			{
				return EventsKitMetadata.GetPackageName(this.Name);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x0005EE1F File Offset: 0x0005D01F
		public bool IsTrace
		{
			get
			{
				return this.m_traceAttr != null && this.m_traceAttr.Enable;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x0005EE38 File Offset: 0x0005D038
		public string TraceProvider
		{
			get
			{
				string text = string.Empty;
				if (this.m_traceAttr != null && this.m_traceAttr.TraceProvider != null)
				{
					text = this.m_traceAttr.TraceProvider.FullName;
				}
				return text;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x0005EE78 File Offset: 0x0005D078
		public bool IsVisible
		{
			get
			{
				return this.m_visibleAttr != null && this.m_visibleAttr.Enable;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x0005EE8F File Offset: 0x0005D08F
		public PerformanceCounterCategoryMetadata PerformanceCountersCategory
		{
			get
			{
				return this.m_performanceCounterCategoy;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001995 RID: 6549 RVA: 0x0005EE97 File Offset: 0x0005D097
		public bool HasPerformanceCounters
		{
			get
			{
				return this.m_events.Values.Any((EventsKitEventMetadata e) => e.HasPerformanceCounters);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x0005EEC8 File Offset: 0x0005D0C8
		// (set) Token: 0x06001997 RID: 6551 RVA: 0x0005EED0 File Offset: 0x0005D0D0
		public bool IsWindowsEventLog
		{
			get
			{
				return this.m_isWindowsEventLog;
			}
			internal set
			{
				this.m_isWindowsEventLog = this.m_isWindowsEventLog || value;
			}
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0005EEE0 File Offset: 0x0005D0E0
		public static string GetImplementationClassName(string eventsKitName, bool isMock)
		{
			if (string.IsNullOrEmpty(eventsKitName))
			{
				throw new ArgumentNullException(eventsKitName);
			}
			string text;
			if (eventsKitName[0].Equals('I'))
			{
				text = eventsKitName.Remove(0, 1);
			}
			else
			{
				text = eventsKitName + "Impl";
			}
			if (!isMock)
			{
				return text;
			}
			return text + "Mock";
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0005EF36 File Offset: 0x0005D136
		public static string GetPackageName(string eventsKitName)
		{
			return eventsKitName + "Package";
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x0005EF43 File Offset: 0x0005D143
		// (set) Token: 0x0600199B RID: 6555 RVA: 0x0005EF4B File Offset: 0x0005D14B
		public EventsKitIdentifiers Id { get; private set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x0005EF54 File Offset: 0x0005D154
		public IEnumerable<IEventMetadata> Events
		{
			get
			{
				return from e in this.m_events.Values
					orderby e.Order
					select (e);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x0005EFB4 File Offset: 0x0005D1B4
		public IEnumerable<IEventMetadata> PublishedEvents
		{
			get
			{
				return this.Events.Where((IEventMetadata e) => e.IsPublishedEvent);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x0005EFE0 File Offset: 0x0005D1E0
		// (set) Token: 0x0600199F RID: 6559 RVA: 0x0005EFE8 File Offset: 0x0005D1E8
		public Type EventsKitType { get; private set; }

		// Token: 0x060019A0 RID: 6560 RVA: 0x0005EFF4 File Offset: 0x0005D1F4
		public IEventMetadata GetEvent(long eventAttributeId)
		{
			EventsKitEventMetadata eventsKitEventMetadata;
			if (!this.m_events.TryGetValue(eventAttributeId, out eventsKitEventMetadata))
			{
				EventsKitIdentifiers eventsKitIdentifiers = new EventsKitIdentifiers(this.Id.EventsKitId, eventAttributeId);
				throw new EventMetadataNotFoundException(eventsKitIdentifiers.FullId, eventsKitIdentifiers.EventsKitId, eventsKitIdentifiers.EventId);
			}
			return eventsKitEventMetadata;
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0005F03C File Offset: 0x0005D23C
		public string GetSchemaAsString(Type useOnlyEventsWithAttributeType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("[{0}({1})]".FormatWithInvariantCulture(new object[]
			{
				typeof(EventsKitAttribute).Name,
				this.Id.EventsKitId
			}));
			if (this.PublishedEvents.Any<IEventMetadata>())
			{
				stringBuilder.AppendLine("[{0}]".FormatWithInvariantCulture(new object[] { typeof(PublishedEventAttribute).Name }));
			}
			if (this.IsTrace)
			{
				stringBuilder.AppendLine("[{0}({1})]".FormatWithInvariantCulture(new object[]
				{
					typeof(TraceAttribute).Name,
					this.TraceProvider
				}));
			}
			stringBuilder.AppendLine("[{0}({1})]".FormatWithInvariantCulture(new object[]
			{
				typeof(PerformanceCountersCategoryAttribute).Name,
				this.PerformanceCountersCategory.CategoryName
			}));
			stringBuilder.AppendLine("interface {0}".FormatWithInvariantCulture(new object[] { this.EventsKitType.FullName }));
			stringBuilder.AppendLine("{");
			stringBuilder.Append(string.Join(Environment.NewLine, from e in this.Events
				orderby e.Id.EventId
				where useOnlyEventsWithAttributeType == null || Auxiliary.GetCustomAttribute(e.EventMethod, useOnlyEventsWithAttributeType) != null
				select e.GetSchemaAsString()));
			stringBuilder.AppendLine("}");
			return stringBuilder.ToString();
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0005ED3B File Offset: 0x0005CF3B
		public override string ToString()
		{
			return this.EventsKitType.Name;
		}

		// Token: 0x040008CE RID: 2254
		private const string c_IMPLEMENTATION_SUFFIX = "Impl";

		// Token: 0x040008CF RID: 2255
		private const string c_MOCK_SUFFIX = "Mock";

		// Token: 0x040008D0 RID: 2256
		private const string c_PACKAGE_SUFFIX = "Package";

		// Token: 0x040008D1 RID: 2257
		internal const string c_Base = "Base";

		// Token: 0x040008D2 RID: 2258
		internal const int c_BaseIdOffset = 1;

		// Token: 0x040008D3 RID: 2259
		private readonly EventsKitAttribute m_eventsKitAttr;

		// Token: 0x040008D4 RID: 2260
		private readonly PublishedEventAttribute m_publishedEventAttr;

		// Token: 0x040008D5 RID: 2261
		private readonly AuditAttribute m_auditAttr;

		// Token: 0x040008D6 RID: 2262
		private readonly TraceAttribute m_traceAttr;

		// Token: 0x040008D7 RID: 2263
		private readonly VisibleAttribute m_visibleAttr;

		// Token: 0x040008D8 RID: 2264
		private readonly Dictionary<long, EventsKitEventMetadata> m_events;

		// Token: 0x040008D9 RID: 2265
		private bool m_isWindowsEventLog;

		// Token: 0x040008DA RID: 2266
		private PerformanceCounterCategoryMetadata m_performanceCounterCategoy;
	}
}
