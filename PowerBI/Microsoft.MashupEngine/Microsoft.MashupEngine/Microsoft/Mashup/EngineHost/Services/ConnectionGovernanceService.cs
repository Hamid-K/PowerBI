using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x0200199D RID: 6557
	public static class ConnectionGovernanceService
	{
		// Token: 0x0600A64C RID: 42572 RVA: 0x00226734 File Offset: 0x00224934
		public static IConnectionGovernanceService New(ConnectionGovernanceManager manager)
		{
			return new ConnectionGovernanceService.MatchingConnectionGovernanceService(new ConnectionGovernanceService.ExactConnectionGovernanceService(manager));
		}

		// Token: 0x0200199E RID: 6558
		private sealed class ExactConnectionGovernanceService : IConnectionGovernanceService
		{
			// Token: 0x0600A64D RID: 42573 RVA: 0x00226741 File Offset: 0x00224941
			public ExactConnectionGovernanceService(ConnectionGovernanceManager manager)
			{
				this.manager = manager;
			}

			// Token: 0x0600A64E RID: 42574 RVA: 0x00226750 File Offset: 0x00224950
			public ITask<IDisposable> BeginGetGovernedHandle(IResource resource, GlobalThreadId threadId)
			{
				return this.manager.BeginGetGovernedHandle(resource, threadId);
			}

			// Token: 0x04005689 RID: 22153
			private readonly ConnectionGovernanceManager manager;
		}

		// Token: 0x0200199F RID: 6559
		private sealed class MatchingConnectionGovernanceService : IConnectionGovernanceService
		{
			// Token: 0x0600A64F RID: 42575 RVA: 0x0022675F File Offset: 0x0022495F
			public MatchingConnectionGovernanceService(IConnectionGovernanceService exactService)
			{
				this.exactService = exactService;
			}

			// Token: 0x0600A650 RID: 42576 RVA: 0x0022676E File Offset: 0x0022496E
			public ITask<IDisposable> BeginGetGovernedHandle(IResource resource, GlobalThreadId threadId)
			{
				return new ConnectionGovernanceService.MatchingConnectionGovernanceService.MatchingGovernedHandleTask(this.exactService, resource, threadId);
			}

			// Token: 0x0400568A RID: 22154
			private readonly IConnectionGovernanceService exactService;

			// Token: 0x020019A0 RID: 6560
			private class MatchingGovernedHandleTask : ITask<IDisposable>, ITask, IDisposable
			{
				// Token: 0x0600A651 RID: 42577 RVA: 0x00226780 File Offset: 0x00224980
				public MatchingGovernedHandleTask(IConnectionGovernanceService exactService, IResource resource, GlobalThreadId threadId)
				{
					this.taskLock = new object();
					this.exactService = exactService;
					this.neededResources = new Stack<IResource>();
					List<string> list = ResourcePath.AllStartsFrom(resource.Kind, resource.Path).ToList<string>();
					list.Sort((string left, string right) => ResourcePath.Length(right) - ResourcePath.Length(left));
					foreach (string text in list)
					{
						this.neededResources.Push(new Resource(resource.Kind, text, null));
					}
					this.resourceCount = this.neededResources.Count;
					this.exactHandles = new Stack<IDisposable>(this.resourceCount);
					this.tasksToDispose = new List<ITask>(2 * this.resourceCount);
					this.threadId = threadId;
					this.waiting = SimpleTask<IDisposable>.CompleteTask;
					this.continuations = new TaskContinuations();
					this.Next();
				}

				// Token: 0x17002A6D RID: 10861
				// (get) Token: 0x0600A652 RID: 42578 RVA: 0x00226894 File Offset: 0x00224A94
				public bool IsCompleted
				{
					get
					{
						object obj = this.taskLock;
						bool flag2;
						lock (obj)
						{
							flag2 = this.neededResources.Count == 0 && this.waiting == null;
						}
						return flag2;
					}
				}

				// Token: 0x17002A6E RID: 10862
				// (get) Token: 0x0600A653 RID: 42579 RVA: 0x002268EC File Offset: 0x00224AEC
				public bool IsFaulted
				{
					get
					{
						return this.Exception != null;
					}
				}

				// Token: 0x17002A6F RID: 10863
				// (get) Token: 0x0600A654 RID: 42580 RVA: 0x002268F7 File Offset: 0x00224AF7
				[DebuggerBrowsable(DebuggerBrowsableState.Never)]
				public IDisposable Result
				{
					get
					{
						this.Wait();
						if (this.exception != null)
						{
							throw this.exception;
						}
						return new ConnectionGovernanceService.MatchingConnectionGovernanceService.MatchingGovernedHandle(this);
					}
				}

				// Token: 0x17002A70 RID: 10864
				// (get) Token: 0x0600A655 RID: 42581 RVA: 0x00226914 File Offset: 0x00224B14
				public Exception Exception
				{
					get
					{
						object obj = this.taskLock;
						Exception ex;
						lock (obj)
						{
							ex = this.exception;
						}
						return ex;
					}
				}

				// Token: 0x0600A656 RID: 42582 RVA: 0x00226958 File Offset: 0x00224B58
				public void Wait()
				{
					while (!this.IsCompleted)
					{
						object obj = this.taskLock;
						ITask task;
						lock (obj)
						{
							task = this.waiting;
							if (task == null)
							{
								break;
							}
						}
						task.Wait();
					}
				}

				// Token: 0x0600A657 RID: 42583 RVA: 0x002269B0 File Offset: 0x00224BB0
				public ITask<TResult> ContinueWith<TResult>(Func<ITask<IDisposable>, TResult> continuation)
				{
					return this.continuations.ContinueWith<IDisposable, TResult>(continuation, this);
				}

				// Token: 0x0600A658 RID: 42584 RVA: 0x002269C0 File Offset: 0x00224BC0
				public void Dispose()
				{
					ITask[] array = null;
					object obj = this.taskLock;
					lock (obj)
					{
						array = this.tasksToDispose.ToArray();
						this.tasksToDispose.Clear();
					}
					ITask[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i].Dispose();
					}
				}

				// Token: 0x0600A659 RID: 42585 RVA: 0x00226A30 File Offset: 0x00224C30
				public void DisposeHandles()
				{
					object obj = this.taskLock;
					List<IDisposable> list;
					lock (obj)
					{
						list = new List<IDisposable>(this.exactHandles.Count);
						while (this.exactHandles.Count > 0)
						{
							list.Add(this.exactHandles.Pop());
						}
					}
					foreach (IDisposable disposable in list)
					{
						disposable.Dispose();
					}
				}

				// Token: 0x0600A65A RID: 42586 RVA: 0x00226AD8 File Offset: 0x00224CD8
				private void Next()
				{
					object obj = this.taskLock;
					IResource resource;
					lock (obj)
					{
						resource = ((this.neededResources.Count == 0) ? null : this.neededResources.Pop());
					}
					if (resource != null)
					{
						ITask<IDisposable> task = this.exactService.BeginGetGovernedHandle(resource, this.threadId);
						ITask task2 = task.ContinueWith<object>(new Func<ITask<IDisposable>, object>(this.OnAvailable));
						bool isCompleted = task.IsCompleted;
						obj = this.taskLock;
						lock (obj)
						{
							this.tasksToDispose.Add(task);
							this.tasksToDispose.Add(task2);
							if (!isCompleted)
							{
								this.waiting = task;
							}
						}
					}
					if (Interlocked.Decrement(ref this.resourceCount) == -1)
					{
						this.Complete();
					}
				}

				// Token: 0x0600A65B RID: 42587 RVA: 0x00226BC4 File Offset: 0x00224DC4
				private object OnAvailable(ITask<IDisposable> task)
				{
					if (task.IsFaulted)
					{
						this.CompleteWithError(task.Exception);
					}
					else
					{
						object obj = this.taskLock;
						lock (obj)
						{
							this.exactHandles.Push(task.Result);
						}
						this.Next();
					}
					return null;
				}

				// Token: 0x0600A65C RID: 42588 RVA: 0x00226C2C File Offset: 0x00224E2C
				private void Complete()
				{
					object obj = this.taskLock;
					lock (obj)
					{
						this.waiting = null;
						this.neededResources.Clear();
					}
					this.continuations.NotifyContinuations(this);
				}

				// Token: 0x0600A65D RID: 42589 RVA: 0x00226C84 File Offset: 0x00224E84
				private void CompleteWithError(Exception e)
				{
					object obj = this.taskLock;
					lock (obj)
					{
						this.exception = e;
					}
					this.DisposeHandles();
					this.Complete();
				}

				// Token: 0x0400568B RID: 22155
				private readonly object taskLock;

				// Token: 0x0400568C RID: 22156
				private readonly IConnectionGovernanceService exactService;

				// Token: 0x0400568D RID: 22157
				private readonly Stack<IResource> neededResources;

				// Token: 0x0400568E RID: 22158
				private readonly Stack<IDisposable> exactHandles;

				// Token: 0x0400568F RID: 22159
				private readonly List<ITask> tasksToDispose;

				// Token: 0x04005690 RID: 22160
				private readonly GlobalThreadId threadId;

				// Token: 0x04005691 RID: 22161
				private readonly TaskContinuations continuations;

				// Token: 0x04005692 RID: 22162
				private int resourceCount;

				// Token: 0x04005693 RID: 22163
				private ITask waiting;

				// Token: 0x04005694 RID: 22164
				private Exception exception;
			}

			// Token: 0x020019A2 RID: 6562
			private class MatchingGovernedHandle : IDisposable
			{
				// Token: 0x0600A661 RID: 42593 RVA: 0x00226CEF File Offset: 0x00224EEF
				public MatchingGovernedHandle(ConnectionGovernanceService.MatchingConnectionGovernanceService.MatchingGovernedHandleTask task)
				{
					this.task = task;
				}

				// Token: 0x0600A662 RID: 42594 RVA: 0x00226CFE File Offset: 0x00224EFE
				public void Dispose()
				{
					this.task.DisposeHandles();
				}

				// Token: 0x04005697 RID: 22167
				private readonly ConnectionGovernanceService.MatchingConnectionGovernanceService.MatchingGovernedHandleTask task;
			}
		}
	}
}
