using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000343 RID: 835
	public static class ExtendedEventsKit
	{
		// Token: 0x060018BC RID: 6332 RVA: 0x0005C4D5 File Offset: 0x0005A6D5
		public static IEventMetadata GetEvent(EventsKitEvent eventsKitEvent)
		{
			return ExtendedEventsKit.GetEventMetadata(eventsKitEvent.Method);
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0005C4D5 File Offset: 0x0005A6D5
		public static IEventMetadata GetEvent<T1>(EventsKitEvent<T1> eventsKitEvent)
		{
			return ExtendedEventsKit.GetEventMetadata(eventsKitEvent.Method);
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0005C4D5 File Offset: 0x0005A6D5
		public static IEventMetadata GetEvent<T1, T2>(EventsKitEvent<T1, T2> eventsKitEvent)
		{
			return ExtendedEventsKit.GetEventMetadata(eventsKitEvent.Method);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0005C4D5 File Offset: 0x0005A6D5
		public static IEventMetadata GetEvent<T1, T2, T3>(EventsKitEvent<T1, T2, T3> eventsKitEvent)
		{
			return ExtendedEventsKit.GetEventMetadata(eventsKitEvent.Method);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0005C4D5 File Offset: 0x0005A6D5
		public static IEventMetadata GetEvent<T1, T2, T3, T4>(EventsKitEvent<T1, T2, T3, T4> eventsKitEvent)
		{
			return ExtendedEventsKit.GetEventMetadata(eventsKitEvent.Method);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0005C4D5 File Offset: 0x0005A6D5
		public static IEventMetadata GetEvent<T1, T2, T3, T4, T5>(EventsKitEvent<T1, T2, T3, T4, T5> eventsKitEvent)
		{
			return ExtendedEventsKit.GetEventMetadata(eventsKitEvent.Method);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0005C4D5 File Offset: 0x0005A6D5
		public static IEventMetadata GetEvent<T1, T2, T3, T4, T5, T6>(EventsKitEvent<T1, T2, T3, T4, T5, T6> eventsKitEvent)
		{
			return ExtendedEventsKit.GetEventMetadata(eventsKitEvent.Method);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0005C4D5 File Offset: 0x0005A6D5
		public static IEventMetadata GetEvent<T1, T2, T3, T4, T5, T6, T7>(EventsKitEvent<T1, T2, T3, T4, T5, T6, T7> eventsKitEvent)
		{
			return ExtendedEventsKit.GetEventMetadata(eventsKitEvent.Method);
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0005C4D5 File Offset: 0x0005A6D5
		public static IEventMetadata GetEvent<T1, T2, T3, T4, T5, T6, T7, T8>(EventsKitEvent<T1, T2, T3, T4, T5, T6, T7, T8> eventsKitEvent)
		{
			return ExtendedEventsKit.GetEventMetadata(eventsKitEvent.Method);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0005C4D5 File Offset: 0x0005A6D5
		public static IEventMetadata GetEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9>(EventsKitEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9> eventsKitEvent)
		{
			return ExtendedEventsKit.GetEventMetadata(eventsKitEvent.Method);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0005C4D5 File Offset: 0x0005A6D5
		public static IEventMetadata GetEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(EventsKitEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> eventsKitEvent)
		{
			return ExtendedEventsKit.GetEventMetadata(eventsKitEvent.Method);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0005C4E4 File Offset: 0x0005A6E4
		public static IEnumerable<IEventMetadata> GetEvents(Type eventsKit)
		{
			IEventsKitMetadata eventsKitMetadata = ExtendedEventsKit.s_eventsKitExplorer.EventKits.Where((IEventsKitMetadata t) => t.EventsKitType.Equals(eventsKit)).FirstOrDefault<IEventsKitMetadata>();
			if (eventsKitMetadata != null)
			{
				return eventsKitMetadata.Events;
			}
			return Enumerable.Empty<IEventMetadata>();
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0005C530 File Offset: 0x0005A730
		private static IEventMetadata GetEventMetadata(MethodInfo eventMethod)
		{
			return (from e in ExtendedEventsKit.s_eventsKitExplorer.Events
				where e.EventMethod.DeclaringType.IsAssignableFrom(eventMethod.DeclaringType)
				where eventMethod.Name.Equals(e.EventMethod.Name)
				select e).ToList<IEventMetadata>().FirstOrDefault<IEventMetadata>();
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0005C580 File Offset: 0x0005A780
		private static IEventsKitExplorer CreateEventsKitExplorer()
		{
			return new EventsKitExplorerFactory().Create();
		}

		// Token: 0x0400088B RID: 2187
		private static IEventsKitExplorer s_eventsKitExplorer = ExtendedEventsKit.CreateEventsKitExplorer();
	}
}
