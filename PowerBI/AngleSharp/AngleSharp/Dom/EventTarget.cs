using System;
using System.Collections.Generic;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom
{
	// Token: 0x02000155 RID: 341
	public abstract class EventTarget : IEventTarget
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x00043AB8 File Offset: 0x00041CB8
		private List<EventTarget.RegisteredEventListener> Listeners
		{
			get
			{
				List<EventTarget.RegisteredEventListener> list;
				if ((list = this._listeners) == null)
				{
					list = (this._listeners = new List<EventTarget.RegisteredEventListener>());
				}
				return list;
			}
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00043AE0 File Offset: 0x00041CE0
		public void AddEventListener(string type, DomEventHandler callback = null, bool capture = false)
		{
			if (callback != null)
			{
				this.Listeners.Add(new EventTarget.RegisteredEventListener
				{
					Type = type,
					Callback = callback,
					IsCaptured = capture
				});
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00043B1C File Offset: 0x00041D1C
		public void RemoveEventListener(string type, DomEventHandler callback = null, bool capture = false)
		{
			if (callback != null)
			{
				List<EventTarget.RegisteredEventListener> listeners = this._listeners;
				if (listeners == null)
				{
					return;
				}
				listeners.Remove(new EventTarget.RegisteredEventListener
				{
					Type = type,
					Callback = callback,
					IsCaptured = capture
				});
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00043B5E File Offset: 0x00041D5E
		public void RemoveEventListeners()
		{
			if (this._listeners != null)
			{
				this._listeners.Clear();
			}
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00043B74 File Offset: 0x00041D74
		public void InvokeEventListener(Event ev)
		{
			if (this._listeners != null)
			{
				string type = ev.Type;
				EventTarget.RegisteredEventListener[] array = this._listeners.ToArray();
				IEventTarget currentTarget = ev.CurrentTarget;
				EventPhase phase = ev.Phase;
				foreach (EventTarget.RegisteredEventListener registeredEventListener in array)
				{
					if (this._listeners.Contains(registeredEventListener) && registeredEventListener.Type.Is(type))
					{
						if ((ev.Flags & EventFlags.StopImmediatePropagation) == EventFlags.StopImmediatePropagation)
						{
							break;
						}
						if ((!registeredEventListener.IsCaptured || phase != EventPhase.Bubbling) && (registeredEventListener.IsCaptured || phase != EventPhase.Capturing))
						{
							registeredEventListener.Callback(currentTarget, ev);
						}
					}
				}
			}
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00043C1C File Offset: 0x00041E1C
		public bool HasEventListener(string type)
		{
			if (this._listeners != null)
			{
				using (List<EventTarget.RegisteredEventListener>.Enumerator enumerator = this._listeners.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Type.Is(type))
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00043C84 File Offset: 0x00041E84
		public bool Dispatch(Event ev)
		{
			if (ev == null || (ev.Flags & EventFlags.Dispatch) == EventFlags.Dispatch || (ev.Flags & EventFlags.Initialized) != EventFlags.Initialized)
			{
				throw new DomException(DomError.InvalidState);
			}
			ev.IsTrusted = false;
			return ev.Dispatch(this);
		}

		// Token: 0x04000948 RID: 2376
		private List<EventTarget.RegisteredEventListener> _listeners;

		// Token: 0x020004D4 RID: 1236
		private struct RegisteredEventListener
		{
			// Token: 0x04001194 RID: 4500
			public string Type;

			// Token: 0x04001195 RID: 4501
			public DomEventHandler Callback;

			// Token: 0x04001196 RID: 4502
			public bool IsCaptured;
		}
	}
}
