using System;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002CA RID: 714
	public abstract class TimerBase : PendingCallback
	{
		// Token: 0x06001323 RID: 4899 RVA: 0x00042474 File Offset: 0x00040674
		internal TimerBase([NotNull] string identity, [NotNull] IWorkTicketFactory workTicketFactory, [NotNull] TimerCallback timerCallback, object state, TimerCreationFlags timerCreationFlags)
			: base(identity, workTicketFactory)
		{
			using (DisposeController disposeController = new DisposeController(this))
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<TimerCallback>(timerCallback, "timerCallback");
				this.m_timerCallback = timerCallback;
				this.m_state = state;
				disposeController.PreventDispose();
			}
			this.m_lock = new object();
			this.m_disposed = false;
			this.m_timerCreationFlags = timerCreationFlags;
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x000424E8 File Offset: 0x000406E8
		protected void InvokeTimerCallback()
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Timer '{0}' ticked, so it invokes its callback", new object[] { base.Name });
			TopLevelHandler.Run(this, ((this.m_timerCreationFlags & TimerCreationFlags.NoCrash) > TimerCreationFlags.Crash) ? TopLevelHandlerOption.SwallowNonfatal : TopLevelHandlerOption.SwallowBenign, delegate
			{
				this.m_timerCallback(this.m_state);
				if (!UtilsContext.Current.Activity.Equals(Activity.Empty))
				{
					UtilsContext.Current.ClearStack();
				}
			});
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0004253C File Offset: 0x0004073C
		public override void Dispose()
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (!this.m_disposed)
				{
					this.m_disposed = true;
					base.Dispose();
				}
			}
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00042590 File Offset: 0x00040790
		internal static int ConvertTimeSpanToInt(TimeSpan timeSpan)
		{
			if (timeSpan == TimeSpan.MinValue || timeSpan == TimeSpan.MaxValue)
			{
				return -1;
			}
			return Convert.ToInt32(timeSpan.TotalMilliseconds);
		}

		// Token: 0x04000728 RID: 1832
		private TimerCallback m_timerCallback;

		// Token: 0x04000729 RID: 1833
		private object m_state;

		// Token: 0x0400072A RID: 1834
		private object m_lock;

		// Token: 0x0400072B RID: 1835
		private bool m_disposed;

		// Token: 0x0400072C RID: 1836
		private TimerCreationFlags m_timerCreationFlags;
	}
}
