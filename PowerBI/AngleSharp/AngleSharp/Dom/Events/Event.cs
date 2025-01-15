using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Attributes;
using AngleSharp.Html;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001E3 RID: 483
	[DomName("Event")]
	public class Event : EventArgs
	{
		// Token: 0x06000FF2 RID: 4082 RVA: 0x000470F8 File Offset: 0x000452F8
		public Event()
		{
			this._flags = EventFlags.None;
			this._phase = EventPhase.None;
			this._time = DateTime.Now;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00047119 File Offset: 0x00045319
		[DomConstructor]
		[DomInitDict(1, true)]
		public Event(string type, bool bubbles = false, bool cancelable = false)
			: this()
		{
			this.Init(type, bubbles, cancelable);
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0004712A File Offset: 0x0004532A
		internal EventFlags Flags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x00047132 File Offset: 0x00045332
		[DomName("type")]
		public string Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x0004713A File Offset: 0x0004533A
		[DomName("target")]
		public IEventTarget OriginalTarget
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x00047142 File Offset: 0x00045342
		[DomName("currentTarget")]
		public IEventTarget CurrentTarget
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0004714A File Offset: 0x0004534A
		[DomName("eventPhase")]
		public EventPhase Phase
		{
			get
			{
				return this._phase;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x00047152 File Offset: 0x00045352
		[DomName("bubbles")]
		public bool IsBubbling
		{
			get
			{
				return this._bubbles;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x0004715A File Offset: 0x0004535A
		[DomName("cancelable")]
		public bool IsCancelable
		{
			get
			{
				return this._cancelable;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00047162 File Offset: 0x00045362
		[DomName("defaultPrevented")]
		public bool IsDefaultPrevented
		{
			get
			{
				return (this._flags & EventFlags.Canceled) == EventFlags.Canceled;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x0004716F File Offset: 0x0004536F
		// (set) Token: 0x06000FFD RID: 4093 RVA: 0x00047177 File Offset: 0x00045377
		[DomName("isTrusted")]
		public bool IsTrusted { get; internal set; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x00047180 File Offset: 0x00045380
		[DomName("timeStamp")]
		public DateTime Time
		{
			get
			{
				return this._time;
			}
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00047188 File Offset: 0x00045388
		[DomName("stopPropagation")]
		public void Stop()
		{
			this._flags |= EventFlags.StopPropagation;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00047198 File Offset: 0x00045398
		[DomName("stopImmediatePropagation")]
		public void StopImmediately()
		{
			this._flags |= EventFlags.StopImmediatePropagation;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x000471A8 File Offset: 0x000453A8
		[DomName("preventDefault")]
		public void Cancel()
		{
			if (this._cancelable)
			{
				this._flags |= EventFlags.Canceled;
			}
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x000471C0 File Offset: 0x000453C0
		[DomName("initEvent")]
		public void Init(string type, bool bubbles, bool cancelable)
		{
			this._flags |= EventFlags.Initialized;
			if ((this._flags & EventFlags.Dispatch) != EventFlags.Dispatch)
			{
				this._flags &= ~(EventFlags.StopPropagation | EventFlags.StopImmediatePropagation | EventFlags.Canceled);
				this.IsTrusted = false;
				this._target = null;
				this._type = type;
				this._bubbles = bubbles;
				this._cancelable = cancelable;
			}
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00047220 File Offset: 0x00045420
		internal bool Dispatch(IEventTarget target)
		{
			this._flags |= EventFlags.Dispatch;
			this._target = target;
			List<IEventTarget> list = new List<IEventTarget>();
			Node node = target as Node;
			if (node != null)
			{
				while ((node = node.Parent) != null)
				{
					list.Add(node);
				}
			}
			this._phase = EventPhase.Capturing;
			this.DispatchAt(list.Reverse<IEventTarget>());
			this._phase = EventPhase.AtTarget;
			if ((this._flags & EventFlags.StopPropagation) != EventFlags.StopPropagation)
			{
				this.CallListeners(target);
			}
			if (this._bubbles)
			{
				this._phase = EventPhase.Bubbling;
				this.DispatchAt(list);
			}
			this._flags &= ~EventFlags.Dispatch;
			this._phase = EventPhase.None;
			this._current = null;
			return (this._flags & EventFlags.Canceled) == EventFlags.Canceled;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x000472D3 File Offset: 0x000454D3
		private void CallListeners(IEventTarget target)
		{
			this._current = target;
			target.InvokeEventListener(this);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x000472E4 File Offset: 0x000454E4
		private void DispatchAt(IEnumerable<IEventTarget> targets)
		{
			foreach (IEventTarget eventTarget in targets)
			{
				this.CallListeners(eventTarget);
				if ((this._flags & EventFlags.StopPropagation) == EventFlags.StopPropagation)
				{
					break;
				}
			}
		}

		// Token: 0x04000A3F RID: 2623
		private EventFlags _flags;

		// Token: 0x04000A40 RID: 2624
		private EventPhase _phase;

		// Token: 0x04000A41 RID: 2625
		private IEventTarget _current;

		// Token: 0x04000A42 RID: 2626
		private IEventTarget _target;

		// Token: 0x04000A43 RID: 2627
		private bool _bubbles;

		// Token: 0x04000A44 RID: 2628
		private bool _cancelable;

		// Token: 0x04000A45 RID: 2629
		private string _type;

		// Token: 0x04000A46 RID: 2630
		private DateTime _time;
	}
}
