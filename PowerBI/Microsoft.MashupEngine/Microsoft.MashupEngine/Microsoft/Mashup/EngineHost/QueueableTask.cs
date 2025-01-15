using System;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001985 RID: 6533
	internal class QueueableTask<T> : SimpleTask<T>, INotifyTaskCompletion
	{
		// Token: 0x0600A5B3 RID: 42419 RVA: 0x0022483B File Offset: 0x00222A3B
		public QueueableTask(Func<T> ctor, TaskStatus initialStatus)
			: base(initialStatus)
		{
			this.ctor = ctor;
			if (initialStatus == TaskStatus.Running)
			{
				EvaluatorThreadPool.Start(new ThreadStart(this.Run));
			}
		}

		// Token: 0x0600A5B4 RID: 42420 RVA: 0x00224860 File Offset: 0x00222A60
		protected void Run()
		{
			try
			{
				base.SetResult(this.ctor());
			}
			catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
			{
				base.SetException(ex);
			}
		}

		// Token: 0x0600A5B5 RID: 42421 RVA: 0x002248B4 File Offset: 0x00222AB4
		void INotifyTaskCompletion.Notify(ITask antecedent)
		{
			object syncRoot = this.syncRoot;
			lock (syncRoot)
			{
				if (this.status == TaskStatus.Created || this.status == TaskStatus.WaitingForActivation)
				{
					this.status = TaskStatus.Running;
					EvaluatorThreadPool.Start(new ThreadStart(this.Run));
				}
			}
		}

		// Token: 0x04005644 RID: 22084
		private readonly Func<T> ctor;
	}
}
