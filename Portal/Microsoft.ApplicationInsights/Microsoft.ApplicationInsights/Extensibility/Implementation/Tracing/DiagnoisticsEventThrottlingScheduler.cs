using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x02000097 RID: 151
	internal class DiagnoisticsEventThrottlingScheduler : IDiagnoisticsEventThrottlingScheduler, IDisposable
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x00014578 File Offset: 0x00012778
		~DiagnoisticsEventThrottlingScheduler()
		{
			this.Dispose(false);
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x000145A8 File Offset: 0x000127A8
		public ICollection<object> Tokens
		{
			get
			{
				return new ReadOnlyCollection<object>(this.timers.Cast<object>().ToList<object>());
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000145C0 File Offset: 0x000127C0
		public object ScheduleToRunEveryTimeIntervalInMilliseconds(int interval, Action actionToExecute)
		{
			if (interval <= 0)
			{
				throw new ArgumentOutOfRangeException("interval");
			}
			if (actionToExecute == null)
			{
				throw new ArgumentNullException("actionToExecute");
			}
			TaskTimerInternal taskTimerInternal = DiagnoisticsEventThrottlingScheduler.InternalCreateAndStartTimer(interval, actionToExecute);
			this.timers.Add(taskTimerInternal);
			CoreEventSource.Log.DiagnoisticsEventThrottlingSchedulerTimerWasCreated(interval.ToString(CultureInfo.InvariantCulture), "Incorrect");
			return taskTimerInternal;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001461C File Offset: 0x0001281C
		public void RemoveScheduledRoutine(object token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			TaskTimerInternal taskTimerInternal = token as TaskTimerInternal;
			if (taskTimerInternal == null)
			{
				throw new ArgumentException("token is not of type TaskTimerInternal", "token");
			}
			if (this.timers.Remove(taskTimerInternal))
			{
				DiagnoisticsEventThrottlingScheduler.DisposeTimer(taskTimerInternal);
				CoreEventSource.Log.DiagnoisticsEventThrottlingSchedulerTimerWasRemoved("Incorrect");
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00014674 File Offset: 0x00012874
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00014684 File Offset: 0x00012884
		private static void DisposeTimer(IDisposable timer)
		{
			try
			{
				timer.Dispose();
			}
			catch (Exception ex)
			{
				CoreEventSource.Log.DiagnoisticsEventThrottlingSchedulerDisposeTimerFailure(ex.ToInvariantString(), "Incorrect");
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000146C4 File Offset: 0x000128C4
		private static TaskTimerInternal InternalCreateAndStartTimer(int intervalInMilliseconds, Action action)
		{
			TaskTimerInternal timer = new TaskTimerInternal
			{
				Delay = TimeSpan.FromMilliseconds((double)intervalInMilliseconds)
			};
			Func<Task> task = null;
			task = delegate
			{
				timer.Start(task);
				action();
				return Task.FromResult<object>(null);
			};
			timer.Start(task);
			return timer;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00014726 File Offset: 0x00012926
		private void Dispose(bool managed)
		{
			if (managed && !this.disposed)
			{
				this.DisposeAllTimers();
			}
			this.disposed = true;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00014744 File Offset: 0x00012944
		private void DisposeAllTimers()
		{
			foreach (TaskTimerInternal taskTimerInternal in this.timers)
			{
				DiagnoisticsEventThrottlingScheduler.DisposeTimer(taskTimerInternal);
			}
			this.timers.Clear();
		}

		// Token: 0x040001DE RID: 478
		private readonly IList<TaskTimerInternal> timers = new List<TaskTimerInternal>();

		// Token: 0x040001DF RID: 479
		private volatile bool disposed;
	}
}
