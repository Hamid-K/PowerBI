using System;
using Microsoft.Diagnostics.Contracts.Internal;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000034 RID: 52
	internal sealed class EventSourceActivity : IDisposable
	{
		// Token: 0x0600019A RID: 410 RVA: 0x0000B794 File Offset: 0x00009994
		public EventSourceActivity(EventSource eventSource)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			Contract.EndContractBlock();
			this.eventSource = eventSource;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000B7B6 File Offset: 0x000099B6
		public static implicit operator EventSourceActivity(EventSource eventSource)
		{
			return new EventSourceActivity(eventSource);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000B7BE File Offset: 0x000099BE
		public EventSource EventSource
		{
			get
			{
				return this.eventSource;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000B7C6 File Offset: 0x000099C6
		public Guid Id
		{
			get
			{
				return this.activityId;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000B7CE File Offset: 0x000099CE
		public EventSourceActivity Start<T>(string eventName, EventSourceOptions options, T data)
		{
			return this.Start<T>(eventName, ref options, ref data);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000B7DC File Offset: 0x000099DC
		public EventSourceActivity Start(string eventName)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			EmptyStruct emptyStruct = default(EmptyStruct);
			return this.Start<EmptyStruct>(eventName, ref eventSourceOptions, ref emptyStruct);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000B804 File Offset: 0x00009A04
		public EventSourceActivity Start(string eventName, EventSourceOptions options)
		{
			EmptyStruct emptyStruct = default(EmptyStruct);
			return this.Start<EmptyStruct>(eventName, ref options, ref emptyStruct);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000B824 File Offset: 0x00009A24
		public EventSourceActivity Start<T>(string eventName, T data)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			return this.Start<T>(eventName, ref eventSourceOptions, ref data);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000B844 File Offset: 0x00009A44
		public void Stop<T>(T data)
		{
			this.Stop<T>(null, ref data);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000B850 File Offset: 0x00009A50
		public void Stop<T>(string eventName)
		{
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.Stop<EmptyStruct>(eventName, ref emptyStruct);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000B86E File Offset: 0x00009A6E
		public void Stop<T>(string eventName, T data)
		{
			this.Stop<T>(eventName, ref data);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000B879 File Offset: 0x00009A79
		public void Write<T>(string eventName, EventSourceOptions options, T data)
		{
			this.Write<T>(this.eventSource, eventName, ref options, ref data);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000B88C File Offset: 0x00009A8C
		public void Write<T>(string eventName, T data)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			this.Write<T>(this.eventSource, eventName, ref eventSourceOptions, ref data);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		public void Write(string eventName, EventSourceOptions options)
		{
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.Write<EmptyStruct>(this.eventSource, eventName, ref options, ref emptyStruct);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000B8DC File Offset: 0x00009ADC
		public void Write(string eventName)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.Write<EmptyStruct>(this.eventSource, eventName, ref eventSourceOptions, ref emptyStruct);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000B90A File Offset: 0x00009B0A
		public void Write<T>(EventSource source, string eventName, EventSourceOptions options, T data)
		{
			this.Write<T>(source, eventName, ref options, ref data);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000B918 File Offset: 0x00009B18
		public void Dispose()
		{
			if (this.state == EventSourceActivity.State.Started)
			{
				EmptyStruct emptyStruct = default(EmptyStruct);
				this.Stop<EmptyStruct>(null, ref emptyStruct);
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000B940 File Offset: 0x00009B40
		private EventSourceActivity Start<T>(string eventName, ref EventSourceOptions options, ref T data)
		{
			if (this.state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			if (!this.eventSource.IsEnabled())
			{
				return this;
			}
			EventSourceActivity eventSourceActivity = new EventSourceActivity(this.eventSource);
			if (!this.eventSource.IsEnabled(options.Level, options.Keywords))
			{
				Guid id = this.Id;
				eventSourceActivity.activityId = Guid.NewGuid();
				eventSourceActivity.startStopOptions = options;
				eventSourceActivity.eventName = eventName;
				eventSourceActivity.startStopOptions.Opcode = EventOpcode.Start;
				this.eventSource.Write<T>(eventName, ref eventSourceActivity.startStopOptions, ref eventSourceActivity.activityId, ref id, ref data);
			}
			else
			{
				eventSourceActivity.activityId = this.Id;
			}
			return eventSourceActivity;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000B9EA File Offset: 0x00009BEA
		private void Write<T>(EventSource eventSource, string eventName, ref EventSourceOptions options, ref T data)
		{
			if (this.state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			if (eventName == null)
			{
				throw new ArgumentNullException();
			}
			eventSource.Write<T>(eventName, ref options, ref this.activityId, ref EventSourceActivity.s_empty, ref data);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000BA18 File Offset: 0x00009C18
		private void Stop<T>(string eventName, ref T data)
		{
			if (this.state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			if (!this.StartEventWasFired)
			{
				return;
			}
			this.state = EventSourceActivity.State.Stopped;
			if (eventName == null)
			{
				eventName = this.eventName;
				if (eventName.EndsWith("Start"))
				{
					eventName = eventName.Substring(0, eventName.Length - 5);
				}
				eventName += "Stop";
			}
			this.startStopOptions.Opcode = EventOpcode.Stop;
			this.eventSource.Write<T>(eventName, ref this.startStopOptions, ref this.activityId, ref EventSourceActivity.s_empty, ref data);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000BAA3 File Offset: 0x00009CA3
		private bool StartEventWasFired
		{
			get
			{
				return this.eventName != null;
			}
		}

		// Token: 0x040000D4 RID: 212
		private readonly EventSource eventSource;

		// Token: 0x040000D5 RID: 213
		private EventSourceOptions startStopOptions;

		// Token: 0x040000D6 RID: 214
		internal Guid activityId;

		// Token: 0x040000D7 RID: 215
		private EventSourceActivity.State state;

		// Token: 0x040000D8 RID: 216
		private string eventName;

		// Token: 0x040000D9 RID: 217
		internal static Guid s_empty;

		// Token: 0x0200008D RID: 141
		private enum State
		{
			// Token: 0x040001BA RID: 442
			Started,
			// Token: 0x040001BB RID: 443
			Stopped
		}
	}
}
