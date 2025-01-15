using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001B3 RID: 435
	[DataContract(Name = "State", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class DiagOperationState : IDiagOperationState
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x0002FA55 File Offset: 0x0002DC55
		// (set) Token: 0x06000E16 RID: 3606 RVA: 0x0002FA5D File Offset: 0x0002DC5D
		public string UniqueIdentifier { get; set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0002FA66 File Offset: 0x0002DC66
		// (set) Token: 0x06000E18 RID: 3608 RVA: 0x0002FA6E File Offset: 0x0002DC6E
		public int Type { get; set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0002FA77 File Offset: 0x0002DC77
		// (set) Token: 0x06000E1A RID: 3610 RVA: 0x0002FA7F File Offset: 0x0002DC7F
		public bool IsRuntimeAware { get; set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x0002FA88 File Offset: 0x0002DC88
		// (set) Token: 0x06000E1C RID: 3612 RVA: 0x0002FA90 File Offset: 0x0002DC90
		public string BaseRawData { get; set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x0002FA99 File Offset: 0x0002DC99
		// (set) Token: 0x06000E1E RID: 3614 RVA: 0x0002FAA1 File Offset: 0x0002DCA1
		public long StartTime { get; set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0002FAAA File Offset: 0x0002DCAA
		// (set) Token: 0x06000E20 RID: 3616 RVA: 0x0002FAB2 File Offset: 0x0002DCB2
		public long LastEventTime { get; set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x0002FABB File Offset: 0x0002DCBB
		// (set) Token: 0x06000E22 RID: 3618 RVA: 0x0002FAC3 File Offset: 0x0002DCC3
		public int ErrorCode { get; set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x0002FACC File Offset: 0x0002DCCC
		// (set) Token: 0x06000E24 RID: 3620 RVA: 0x0002FAD4 File Offset: 0x0002DCD4
		public List<DiagEvent> Events
		{
			get
			{
				return this._events;
			}
			set
			{
				this._events = value;
			}
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0002FAE0 File Offset: 0x0002DCE0
		public DiagOperationState(int initNumberOfEvents)
		{
			this._events = new List<DiagEvent>(initNumberOfEvents);
			this.BaseRawData = "";
			this.StartTime = DateTime.UtcNow.Ticks;
			this.LastEventTime = this.StartTime;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0002FB29 File Offset: 0x0002DD29
		public DiagOperationState()
			: this(20)
		{
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0002FB34 File Offset: 0x0002DD34
		public void Merge(DiagOperationState states)
		{
			lock (this._events)
			{
				if (this.StartTime > states.StartTime)
				{
					this.StartTime = states.StartTime;
				}
				if (this.LastEventTime < states.LastEventTime)
				{
					this.LastEventTime = states.LastEventTime;
				}
				this.BaseRawData += states.BaseRawData;
				this.AddEvent(states.Events);
			}
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0002FBC8 File Offset: 0x0002DDC8
		public void AddEvent(List<DiagEvent> events)
		{
			foreach (DiagEvent diagEvent in events)
			{
				this.AddEvent(diagEvent);
			}
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0002FC18 File Offset: 0x0002DE18
		public void AddEvent(DiagEvent ev)
		{
			this._events.Add(ev);
			this._anyEventFailure = this._anyEventFailure || !ev.Result;
			if (ev.Ticks > this.LastEventTime)
			{
				this.LastEventTime = ev.Ticks;
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0002FC65 File Offset: 0x0002DE65
		public bool IsAnyEventFailed()
		{
			return this._anyEventFailure;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0002FC70 File Offset: 0x0002DE70
		public List<DiagEvent> GetFailedEvents()
		{
			List<DiagEvent> list = new List<DiagEvent>();
			foreach (DiagEvent diagEvent in this._events)
			{
				if (!diagEvent.Result)
				{
					list.Add(diagEvent);
				}
			}
			return list;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0002FCD4 File Offset: 0x0002DED4
		public bool Contains(DiagEventName state)
		{
			foreach (DiagEvent diagEvent in this._events)
			{
				if (diagEvent.CurrentEventName == state)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0002FD30 File Offset: 0x0002DF30
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.AppendFormat("OP:{0};", this.Type);
			stringBuilder.AppendFormat("UID:{0};", this.UniqueIdentifier);
			stringBuilder.AppendFormat("RData:{0};", this.BaseRawData);
			if (this._events != null)
			{
				int count = this._events.Count;
				for (int i = 0; i < count; i++)
				{
					if (this._events[i] != null)
					{
						stringBuilder.AppendFormat("$#${0};", this._events[i].ToString());
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040009D5 RID: 2517
		private List<DiagEvent> _events;

		// Token: 0x040009D6 RID: 2518
		private bool _anyEventFailure;
	}
}
