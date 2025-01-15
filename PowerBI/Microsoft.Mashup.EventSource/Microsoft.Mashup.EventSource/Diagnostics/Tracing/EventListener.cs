using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Diagnostics.Contracts.Internal;
using Microsoft.Diagnostics.Tracing.Internal;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000016 RID: 22
	public abstract class EventListener : IDisposable
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x000080F0 File Offset: 0x000062F0
		protected EventListener()
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_CreatingListener)
				{
					throw new InvalidOperationException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_ListenerCreatedInsideCallback", Array.Empty<object>()));
				}
				try
				{
					EventListener.s_CreatingListener = true;
					this.m_Next = EventListener.s_Listeners;
					EventListener.s_Listeners = this;
					WeakReference[] array = EventListener.s_EventSources.ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						EventSource eventSource = array[i].Target as EventSource;
						if (eventSource != null)
						{
							eventSource.AddListener(this);
						}
					}
				}
				finally
				{
					EventListener.s_CreatingListener = false;
				}
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000081AC File Offset: 0x000063AC
		public virtual void Dispose()
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				Contract.Assert(EventListener.s_Listeners != null);
				if (EventListener.s_Listeners != null)
				{
					if (this == EventListener.s_Listeners)
					{
						EventListener eventListener = EventListener.s_Listeners;
						EventListener.s_Listeners = this.m_Next;
						EventListener.RemoveReferencesToListenerInEventSources(eventListener);
					}
					else
					{
						EventListener eventListener2 = EventListener.s_Listeners;
						EventListener next;
						for (;;)
						{
							next = eventListener2.m_Next;
							if (next == null)
							{
								break;
							}
							if (next == this)
							{
								goto Block_6;
							}
							eventListener2 = next;
						}
						return;
						Block_6:
						eventListener2.m_Next = next.m_Next;
						EventListener.RemoveReferencesToListenerInEventSources(next);
					}
				}
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00008250 File Offset: 0x00006450
		public void EnableEvents(EventSource eventSource, EventLevel level)
		{
			this.EnableEvents(eventSource, level, EventKeywords.None);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000825C File Offset: 0x0000645C
		public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword)
		{
			this.EnableEvents(eventSource, level, matchAnyKeyword, null);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00008268 File Offset: 0x00006468
		public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword, IDictionary<string, string> arguments)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			Contract.EndContractBlock();
			eventSource.SendCommand(this, 0, 0, EventCommand.Update, true, level, matchAnyKeyword, arguments);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00008298 File Offset: 0x00006498
		public void DisableEvents(EventSource eventSource)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			Contract.EndContractBlock();
			eventSource.SendCommand(this, 0, 0, EventCommand.Update, false, EventLevel.LogAlways, EventKeywords.None, null);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000082C7 File Offset: 0x000064C7
		protected internal virtual void OnEventSourceCreated(EventSource eventSource)
		{
		}

		// Token: 0x060000D7 RID: 215
		protected internal abstract void OnEventWritten(EventWrittenEventArgs eventData);

		// Token: 0x060000D8 RID: 216 RVA: 0x000082C9 File Offset: 0x000064C9
		protected static int EventSourceIndex(EventSource eventSource)
		{
			return eventSource.m_id;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000082D4 File Offset: 0x000064D4
		internal static void AddEventSource(EventSource newEventSource)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_EventSources == null)
				{
					EventListener.s_EventSources = new List<WeakReference>(2);
				}
				if (!EventListener.s_EventSourceShutdownRegistered)
				{
					EventListener.s_EventSourceShutdownRegistered = true;
					AppDomain.CurrentDomain.ProcessExit += EventListener.DisposeOnShutdown;
					AppDomain.CurrentDomain.DomainUnload += EventListener.DisposeOnShutdown;
				}
				int num = -1;
				if (EventListener.s_EventSources.Count % 64 == 63)
				{
					int num2 = EventListener.s_EventSources.Count;
					while (0 < num2)
					{
						num2--;
						WeakReference weakReference = EventListener.s_EventSources[num2];
						if (!weakReference.IsAlive)
						{
							num = num2;
							weakReference.Target = newEventSource;
							break;
						}
					}
				}
				if (num < 0)
				{
					num = EventListener.s_EventSources.Count;
					EventListener.s_EventSources.Add(new WeakReference(newEventSource));
				}
				newEventSource.m_id = num;
				for (EventListener next = EventListener.s_Listeners; next != null; next = next.m_Next)
				{
					newEventSource.AddListener(next);
				}
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000083E8 File Offset: 0x000065E8
		private static void DisposeOnShutdown(object sender, EventArgs e)
		{
			foreach (WeakReference weakReference in EventListener.s_EventSources)
			{
				EventSource eventSource = weakReference.Target as EventSource;
				if (eventSource != null)
				{
					eventSource.Dispose();
				}
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00008448 File Offset: 0x00006648
		private static void RemoveReferencesToListenerInEventSources(EventListener listenerToRemove)
		{
			foreach (WeakReference weakReference in EventListener.s_EventSources)
			{
				EventSource eventSource = weakReference.Target as EventSource;
				if (eventSource != null)
				{
					if (eventSource.m_Dispatchers.m_Listener == listenerToRemove)
					{
						eventSource.m_Dispatchers = eventSource.m_Dispatchers.m_Next;
					}
					else
					{
						EventDispatcher eventDispatcher = eventSource.m_Dispatchers;
						EventDispatcher next;
						for (;;)
						{
							next = eventDispatcher.m_Next;
							if (next == null)
							{
								break;
							}
							if (next.m_Listener == listenerToRemove)
							{
								goto Block_6;
							}
							eventDispatcher = next;
						}
						Contract.Assert(false, "EventSource did not have a registered EventListener!");
						continue;
						Block_6:
						eventDispatcher.m_Next = next.m_Next;
					}
				}
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00008504 File Offset: 0x00006704
		[Conditional("DEBUG")]
		internal static void Validate()
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				Dictionary<EventListener, bool> dictionary = new Dictionary<EventListener, bool>();
				for (EventListener next = EventListener.s_Listeners; next != null; next = next.m_Next)
				{
					dictionary.Add(next, true);
				}
				int num = -1;
				foreach (WeakReference weakReference in EventListener.s_EventSources)
				{
					num++;
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null)
					{
						Contract.Assert(eventSource.m_id == num, "Unexpected event source ID.");
						for (EventDispatcher eventDispatcher = eventSource.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
						{
							Contract.Assert(dictionary.ContainsKey(eventDispatcher.m_Listener), "EventSource has a listener not on the global list.");
						}
						foreach (EventListener eventListener in dictionary.Keys)
						{
							EventDispatcher eventDispatcher = eventSource.m_Dispatchers;
							for (;;)
							{
								Contract.Assert(eventDispatcher != null, "Listener is not on all eventSources.");
								if (eventDispatcher.m_Listener == eventListener)
								{
									break;
								}
								eventDispatcher = eventDispatcher.m_Next;
							}
						}
					}
				}
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00008690 File Offset: 0x00006890
		internal static object EventListenersLock
		{
			get
			{
				if (EventListener.s_EventSources == null)
				{
					Interlocked.CompareExchange<List<WeakReference>>(ref EventListener.s_EventSources, new List<WeakReference>(2), null);
				}
				return EventListener.s_EventSources;
			}
		}

		// Token: 0x04000053 RID: 83
		internal volatile EventListener m_Next;

		// Token: 0x04000054 RID: 84
		internal static EventListener s_Listeners;

		// Token: 0x04000055 RID: 85
		internal static List<WeakReference> s_EventSources;

		// Token: 0x04000056 RID: 86
		private static bool s_CreatingListener;

		// Token: 0x04000057 RID: 87
		private static bool s_EventSourceShutdownRegistered;
	}
}
