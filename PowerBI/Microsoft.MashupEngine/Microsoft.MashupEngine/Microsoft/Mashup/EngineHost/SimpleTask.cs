using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001984 RID: 6532
	internal class SimpleTask<T> : ITask<T>, ITask, IDisposable
	{
		// Token: 0x0600A5A4 RID: 42404 RVA: 0x00224530 File Offset: 0x00222730
		static SimpleTask()
		{
			SimpleTask<T> simpleTask = new SimpleTask<T>();
			simpleTask.SetResult(default(T));
			SimpleTask<T>.completeTask = simpleTask;
		}

		// Token: 0x0600A5A5 RID: 42405 RVA: 0x00224556 File Offset: 0x00222756
		public SimpleTask()
			: this(TaskStatus.Created)
		{
		}

		// Token: 0x0600A5A6 RID: 42406 RVA: 0x0022455F File Offset: 0x0022275F
		protected SimpleTask(TaskStatus initialStatus = TaskStatus.Created)
		{
			this.syncRoot = new object();
			this.continuations = new TaskContinuations();
			this.status = initialStatus;
		}

		// Token: 0x17002A4A RID: 10826
		// (get) Token: 0x0600A5A7 RID: 42407 RVA: 0x00224584 File Offset: 0x00222784
		public Exception Exception
		{
			get
			{
				object obj = this.syncRoot;
				Exception ex;
				lock (obj)
				{
					ex = this.exception;
				}
				return ex;
			}
		}

		// Token: 0x17002A4B RID: 10827
		// (get) Token: 0x0600A5A8 RID: 42408 RVA: 0x002245C8 File Offset: 0x002227C8
		public bool IsCompleted
		{
			get
			{
				object obj = this.syncRoot;
				bool complete;
				lock (obj)
				{
					complete = this.Complete;
				}
				return complete;
			}
		}

		// Token: 0x17002A4C RID: 10828
		// (get) Token: 0x0600A5A9 RID: 42409 RVA: 0x0022460C File Offset: 0x0022280C
		public bool IsFaulted
		{
			get
			{
				object obj = this.syncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = this.status == TaskStatus.Faulted;
				}
				return flag2;
			}
		}

		// Token: 0x17002A4D RID: 10829
		// (get) Token: 0x0600A5AA RID: 42410 RVA: 0x00224654 File Offset: 0x00222854
		public static ITask<T> CompleteTask
		{
			get
			{
				return SimpleTask<T>.completeTask;
			}
		}

		// Token: 0x0600A5AB RID: 42411 RVA: 0x0022465B File Offset: 0x0022285B
		public virtual void Dispose()
		{
			if (this.wait != null)
			{
				this.wait.Close();
				this.wait = null;
			}
		}

		// Token: 0x0600A5AC RID: 42412 RVA: 0x00224678 File Offset: 0x00222878
		public virtual void Wait()
		{
			ManualResetEvent manualResetEvent = null;
			object obj = this.syncRoot;
			lock (obj)
			{
				if (!this.Complete)
				{
					if (this.wait == null)
					{
						this.wait = new ManualResetEvent(false);
					}
					manualResetEvent = this.wait;
				}
			}
			if (manualResetEvent != null)
			{
				manualResetEvent.WaitOne();
			}
		}

		// Token: 0x17002A4E RID: 10830
		// (get) Token: 0x0600A5AD RID: 42413 RVA: 0x002246E4 File Offset: 0x002228E4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public T Result
		{
			get
			{
				this.Wait();
				object obj = this.syncRoot;
				T t;
				lock (obj)
				{
					if (this.exception != null)
					{
						throw this.exception;
					}
					t = this.result;
				}
				return t;
			}
		}

		// Token: 0x17002A4F RID: 10831
		// (get) Token: 0x0600A5AE RID: 42414 RVA: 0x0022473C File Offset: 0x0022293C
		protected bool Complete
		{
			get
			{
				return this.status == TaskStatus.RanToCompletion || this.status == TaskStatus.Faulted;
			}
		}

		// Token: 0x0600A5AF RID: 42415 RVA: 0x00224754 File Offset: 0x00222954
		public void SetException(Exception exception)
		{
			object obj = this.syncRoot;
			ManualResetEvent manualResetEvent;
			lock (obj)
			{
				this.exception = exception;
				manualResetEvent = this.wait;
				this.wait = null;
				this.status = TaskStatus.Faulted;
			}
			this.Finish(manualResetEvent);
		}

		// Token: 0x0600A5B0 RID: 42416 RVA: 0x002247B4 File Offset: 0x002229B4
		public void SetResult(T result)
		{
			object obj = this.syncRoot;
			ManualResetEvent manualResetEvent;
			lock (obj)
			{
				this.result = result;
				manualResetEvent = this.wait;
				this.wait = null;
				this.status = TaskStatus.RanToCompletion;
			}
			this.Finish(manualResetEvent);
		}

		// Token: 0x0600A5B1 RID: 42417 RVA: 0x00224814 File Offset: 0x00222A14
		private void Finish(ManualResetEvent localWait)
		{
			if (localWait != null)
			{
				localWait.Set();
			}
			this.continuations.NotifyContinuations(this);
		}

		// Token: 0x0600A5B2 RID: 42418 RVA: 0x0022482C File Offset: 0x00222A2C
		public ITask<TResult> ContinueWith<TResult>(Func<ITask<T>, TResult> continuationFunction)
		{
			return this.continuations.ContinueWith<T, TResult>(continuationFunction, this);
		}

		// Token: 0x0400563D RID: 22077
		private static readonly SimpleTask<T> completeTask;

		// Token: 0x0400563E RID: 22078
		protected readonly object syncRoot;

		// Token: 0x0400563F RID: 22079
		private readonly TaskContinuations continuations;

		// Token: 0x04005640 RID: 22080
		protected TaskStatus status;

		// Token: 0x04005641 RID: 22081
		private ManualResetEvent wait;

		// Token: 0x04005642 RID: 22082
		private T result;

		// Token: 0x04005643 RID: 22083
		private Exception exception;
	}
}
