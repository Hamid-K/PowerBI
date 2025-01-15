using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AngleSharp
{
	// Token: 0x0200001F RID: 31
	internal sealed class TaskEventLoop : IEventLoop
	{
		// Token: 0x060000FD RID: 253 RVA: 0x000068B0 File Offset: 0x00004AB0
		public TaskEventLoop()
		{
			this._queues = new Dictionary<TaskPriority, Queue<TaskEventLoop.TaskEventLoopEntry>>();
			this._current = null;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000068CC File Offset: 0x00004ACC
		public ICancellable Enqueue(Action<CancellationToken> task, TaskPriority priority)
		{
			TaskEventLoop.TaskEventLoopEntry taskEventLoopEntry = new TaskEventLoop.TaskEventLoopEntry(task);
			lock (this)
			{
				Queue<TaskEventLoop.TaskEventLoopEntry> queue = null;
				if (!this._queues.TryGetValue(priority, out queue))
				{
					queue = new Queue<TaskEventLoop.TaskEventLoopEntry>();
					this._queues.Add(priority, queue);
				}
				if (this._current == null)
				{
					this.SetCurrent(taskEventLoopEntry);
				}
				else
				{
					queue.Enqueue(taskEventLoopEntry);
				}
			}
			return taskEventLoopEntry;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006948 File Offset: 0x00004B48
		public void Spin()
		{
			lock (this)
			{
				TaskEventLoop.TaskEventLoopEntry current = this._current;
				if (current == null || !current.IsRunning)
				{
					TaskEventLoop.TaskEventLoopEntry taskEventLoopEntry;
					if ((taskEventLoopEntry = this.Dequeue(TaskPriority.Critical)) == null && (taskEventLoopEntry = this.Dequeue(TaskPriority.Microtask)) == null)
					{
						taskEventLoopEntry = this.Dequeue(TaskPriority.Normal) ?? this.Dequeue(TaskPriority.None);
					}
					this.SetCurrent(taskEventLoopEntry);
				}
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000069C4 File Offset: 0x00004BC4
		public void CancelAll()
		{
			lock (this)
			{
				foreach (KeyValuePair<TaskPriority, Queue<TaskEventLoop.TaskEventLoopEntry>> keyValuePair in this._queues)
				{
					Queue<TaskEventLoop.TaskEventLoopEntry> value = keyValuePair.Value;
					while (value.Count > 0)
					{
						value.Dequeue().Cancel();
					}
				}
				this._queues.Clear();
				TaskEventLoop.TaskEventLoopEntry current = this._current;
				if (current != null)
				{
					current.Cancel();
				}
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006A74 File Offset: 0x00004C74
		private void SetCurrent(TaskEventLoop.TaskEventLoopEntry entry)
		{
			this._current = entry;
			if (entry != null)
			{
				entry.Run(new Action(this.Continue));
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006A94 File Offset: 0x00004C94
		private void Continue()
		{
			lock (this)
			{
				this._current = null;
			}
			this.Spin();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006AD8 File Offset: 0x00004CD8
		private TaskEventLoop.TaskEventLoopEntry Dequeue(TaskPriority priority)
		{
			if (this._queues.ContainsKey(priority) && this._queues[priority].Count != 0)
			{
				return this._queues[priority].Dequeue();
			}
			return null;
		}

		// Token: 0x040001AD RID: 429
		private readonly Dictionary<TaskPriority, Queue<TaskEventLoop.TaskEventLoopEntry>> _queues;

		// Token: 0x040001AE RID: 430
		private TaskEventLoop.TaskEventLoopEntry _current;

		// Token: 0x02000421 RID: 1057
		private sealed class TaskEventLoopEntry : ICancellable
		{
			// Token: 0x06002134 RID: 8500 RVA: 0x000594EE File Offset: 0x000576EE
			public TaskEventLoopEntry(Action<CancellationToken> action)
			{
				this._cts = new CancellationTokenSource();
				this._action = action;
			}

			// Token: 0x17000A60 RID: 2656
			// (get) Token: 0x06002135 RID: 8501 RVA: 0x00059508 File Offset: 0x00057708
			public bool IsCompleted
			{
				get
				{
					return this._task != null && this._task.IsCompleted;
				}
			}

			// Token: 0x17000A61 RID: 2657
			// (get) Token: 0x06002136 RID: 8502 RVA: 0x00059520 File Offset: 0x00057720
			public bool IsRunning
			{
				get
				{
					return (this._task != null && this._task.Status == TaskStatus.Running) || this._task.Status == TaskStatus.WaitingForActivation || this._task.Status == TaskStatus.WaitingToRun || this._task.Status == TaskStatus.WaitingForChildrenToComplete;
				}
			}

			// Token: 0x06002137 RID: 8503 RVA: 0x00059570 File Offset: 0x00057770
			public void Run(Action callback)
			{
				if (this._task == null)
				{
					this._task = TaskEx.Run(delegate
					{
						this._action(this._cts.Token);
						callback();
					}, this._cts.Token);
				}
			}

			// Token: 0x06002138 RID: 8504 RVA: 0x000595BB File Offset: 0x000577BB
			public void Cancel()
			{
				this._cts.Cancel();
			}

			// Token: 0x04000D66 RID: 3430
			private readonly CancellationTokenSource _cts;

			// Token: 0x04000D67 RID: 3431
			private readonly Action<CancellationToken> _action;

			// Token: 0x04000D68 RID: 3432
			private Task _task;
		}
	}
}
