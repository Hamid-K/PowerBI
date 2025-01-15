using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001982 RID: 6530
	internal class TaskContinuations
	{
		// Token: 0x0600A59F RID: 42399 RVA: 0x00224408 File Offset: 0x00222608
		public ITask<TResult> ContinueWith<T, TResult>(Func<ITask<T>, TResult> continuationFunction, ITask<T> parent)
		{
			ITask<TResult> task;
			lock (this)
			{
				TaskStatus taskStatus = ((parent.IsCompleted || parent.IsFaulted) ? TaskStatus.Running : TaskStatus.WaitingForActivation);
				SimpleTask<TResult> simpleTask = new QueueableTask<TResult>(() => continuationFunction(parent), taskStatus);
				this.continuations = this.continuations.Add(simpleTask);
				task = simpleTask;
			}
			return task;
		}

		// Token: 0x0600A5A0 RID: 42400 RVA: 0x002244A0 File Offset: 0x002226A0
		public void NotifyContinuations(ITask parent)
		{
			ITask[] array;
			lock (this)
			{
				array = this.continuations;
			}
			ITask[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				INotifyTaskCompletion notifyTaskCompletion = array2[i] as INotifyTaskCompletion;
				if (notifyTaskCompletion != null)
				{
					notifyTaskCompletion.Notify(parent);
				}
			}
		}

		// Token: 0x0400563A RID: 22074
		private ITask[] continuations = EmptyArray<ITask>.Instance;
	}
}
