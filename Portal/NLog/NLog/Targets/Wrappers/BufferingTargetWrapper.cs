using System;
using System.ComponentModel;
using System.Threading;
using NLog.Common;
using NLog.Internal;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000064 RID: 100
	[Target("BufferingWrapper", IsWrapper = true)]
	public class BufferingTargetWrapper : WrapperTargetBase
	{
		// Token: 0x06000897 RID: 2199 RVA: 0x0001643D File Offset: 0x0001463D
		public BufferingTargetWrapper()
			: this(null)
		{
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00016446 File Offset: 0x00014646
		public BufferingTargetWrapper(string name, Target wrappedTarget)
			: this(wrappedTarget)
		{
			base.Name = name;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00016456 File Offset: 0x00014656
		public BufferingTargetWrapper(Target wrappedTarget)
			: this(wrappedTarget, 100)
		{
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00016461 File Offset: 0x00014661
		public BufferingTargetWrapper(Target wrappedTarget, int bufferSize)
			: this(wrappedTarget, bufferSize, -1)
		{
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001646C File Offset: 0x0001466C
		public BufferingTargetWrapper(Target wrappedTarget, int bufferSize, int flushTimeout)
			: this(wrappedTarget, bufferSize, flushTimeout, BufferingTargetWrapperOverflowAction.Flush)
		{
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00016478 File Offset: 0x00014678
		public BufferingTargetWrapper(Target wrappedTarget, int bufferSize, int flushTimeout, BufferingTargetWrapperOverflowAction overflowAction)
		{
			base.WrappedTarget = wrappedTarget;
			this.BufferSize = bufferSize;
			this.FlushTimeout = flushTimeout;
			this.SlidingTimeout = true;
			this.OverflowAction = overflowAction;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x000164AF File Offset: 0x000146AF
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x000164B7 File Offset: 0x000146B7
		[DefaultValue(100)]
		public int BufferSize { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x000164C0 File Offset: 0x000146C0
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x000164C8 File Offset: 0x000146C8
		[DefaultValue(-1)]
		public int FlushTimeout { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x000164D1 File Offset: 0x000146D1
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x000164D9 File Offset: 0x000146D9
		[DefaultValue(true)]
		public bool SlidingTimeout { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x000164E2 File Offset: 0x000146E2
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x000164EA File Offset: 0x000146EA
		[DefaultValue("Flush")]
		public BufferingTargetWrapperOverflowAction OverflowAction { get; set; }

		// Token: 0x060008A5 RID: 2213 RVA: 0x000164F3 File Offset: 0x000146F3
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			this.WriteEventsInBuffer("Flush Async");
			base.FlushAsync(asyncContinuation);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00016508 File Offset: 0x00014708
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			this._buffer = new LogEventInfoBuffer(this.BufferSize, false, 0);
			InternalLogger.Trace<string>("BufferingWrapper(Name={0}): Create Timer", base.Name);
			this._flushTimer = new Timer(new TimerCallback(this.FlushCallback), null, -1, -1);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00016558 File Offset: 0x00014758
		protected override void CloseTarget()
		{
			Timer flushTimer = this._flushTimer;
			if (flushTimer != null)
			{
				this._flushTimer = null;
				if (flushTimer.WaitForDispose(TimeSpan.FromSeconds(1.0)))
				{
					this.WriteEventsInBuffer("Closing Target");
				}
			}
			base.CloseTarget();
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x000165A0 File Offset: 0x000147A0
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			base.PrecalculateVolatileLayouts(logEvent.LogEvent);
			int num = this._buffer.Append(logEvent);
			if (num >= this.BufferSize)
			{
				if (this.OverflowAction == BufferingTargetWrapperOverflowAction.Flush)
				{
					this.WriteEventsInBuffer("Exceeding BufferSize");
					return;
				}
			}
			else if (this.FlushTimeout > 0 && (this.SlidingTimeout || num == 1))
			{
				this._flushTimer.Change(this.FlushTimeout, -1);
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001660C File Offset: 0x0001480C
		private void FlushCallback(object state)
		{
			bool flag = false;
			try
			{
				int num = Math.Min(this.FlushTimeout / 2, 100);
				flag = Monitor.TryEnter(this._lockObject, num);
				if (flag)
				{
					if (this._flushTimer != null)
					{
						this.WriteEventsInBuffer(null);
					}
				}
				else if (this._buffer.Count > 0)
				{
					Timer flushTimer = this._flushTimer;
					if (flushTimer != null)
					{
						flushTimer.Change(this.FlushTimeout, -1);
					}
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "BufferingWrapper(Name={0}): Error in flush procedure.", new object[] { base.Name });
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this._lockObject);
				}
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x000166CC File Offset: 0x000148CC
		private void WriteEventsInBuffer(string reason)
		{
			if (base.WrappedTarget == null)
			{
				InternalLogger.Error<string>("BufferingWrapper(Name={0}): WrappedTarget is NULL", base.Name);
				return;
			}
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				AsyncLogEventInfo[] eventsAndClear = this._buffer.GetEventsAndClear();
				if (eventsAndClear.Length != 0)
				{
					if (reason != null)
					{
						InternalLogger.Trace<string, int, string>("BufferingWrapper(Name={0}): Writing {1} events ({2})", base.Name, eventsAndClear.Length, reason);
					}
					base.WrappedTarget.WriteAsyncLogEvents(eventsAndClear);
				}
			}
		}

		// Token: 0x040001E2 RID: 482
		private LogEventInfoBuffer _buffer;

		// Token: 0x040001E3 RID: 483
		private Timer _flushTimer;

		// Token: 0x040001E4 RID: 484
		private readonly object _lockObject = new object();
	}
}
