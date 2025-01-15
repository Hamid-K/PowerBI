using System;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Events;

namespace AngleSharp.Extensions
{
	// Token: 0x020000EB RID: 235
	internal static class EventTargetExtensions
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x000348FD File Offset: 0x00032AFD
		public static bool FireSimpleEvent(this IEventTarget target, string eventName, bool bubble = false, bool cancelable = false)
		{
			Event @event = new Event();
			@event.IsTrusted = true;
			@event.Init(eventName, bubble, cancelable);
			return @event.Dispatch(target);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0003491A File Offset: 0x00032B1A
		public static bool Fire(this IEventTarget target, Event eventData)
		{
			eventData.IsTrusted = true;
			return eventData.Dispatch(target);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0003492C File Offset: 0x00032B2C
		public static Task FireAsync<T>(this IBrowsingContext target, string eventName, T data)
		{
			InteractivityEvent<T> interactivityEvent = new InteractivityEvent<T>(eventName, data);
			target.Fire(interactivityEvent);
			return interactivityEvent.Result ?? TaskEx.FromResult<bool>(false);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0003495C File Offset: 0x00032B5C
		public static bool Fire<T>(this IEventTarget target, Action<T> initializer, IEventTarget targetOverride = null) where T : Event, new()
		{
			T t = new T();
			t.IsTrusted = true;
			T t2 = t;
			initializer(t2);
			return t2.Dispatch(targetOverride ?? target);
		}
	}
}
