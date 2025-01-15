using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000259 RID: 601
	public class MultiAsyncOperationRunner
	{
		// Token: 0x06000FD3 RID: 4051 RVA: 0x000361F0 File Offset: 0x000343F0
		public MultiAsyncOperationRunner()
			: this(new List<MultiAsyncOperationRunner.AsyncOperation>())
		{
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00036200 File Offset: 0x00034400
		public MultiAsyncOperationRunner([NotNull] IEnumerable<MultiAsyncOperationRunner.AsyncOperation> asyncOperations)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<MultiAsyncOperationRunner.AsyncOperation>>(asyncOperations, "asyncOperations");
			ExtendedDiagnostics.EnsureArgument("asyncOperations", !asyncOperations.Contains(null), "asyncOperations argument can not contain null elements");
			this.m_lock = new object();
			this.m_asyncOperations = asyncOperations.ToList<MultiAsyncOperationRunner.AsyncOperation>();
			this.m_started = false;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00036258 File Offset: 0x00034458
		public virtual void AddAsyncOperation(string name, Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod)
		{
			MultiAsyncOperationRunner.AsyncOperation asyncOperation = new MultiAsyncOperationRunner.AsyncOperation(name, beginMethod, endMethod);
			this.AddAsyncOperationInternal(asyncOperation);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00036278 File Offset: 0x00034478
		public virtual void AddAsyncOperation(string name, Sequencer.TaskBeginFunction begin)
		{
			MultiAsyncOperationRunner.AsyncOperation asyncOperation = new MultiAsyncOperationRunner.AsyncOperation(name, (AsyncCallback c, object s) => begin().ToApm(c, s), delegate(IAsyncResult ar)
			{
				((Task)ar).ExtendedWait();
			});
			this.AddAsyncOperationInternal(asyncOperation);
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x000362CB File Offset: 0x000344CB
		public IAsyncResult BeginExecute(AsyncCallback callback, object state)
		{
			return this.BeginExecute(null, callback, state);
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x000362D8 File Offset: 0x000344D8
		public IAsyncResult BeginExecute(WorkTicket workTicket, AsyncCallback callback, object state)
		{
			IAsyncResult mcar;
			using (DisposeController disposeController = new DisposeController(workTicket))
			{
				object @lock = this.m_lock;
				lock (@lock)
				{
					ExtendedDiagnostics.EnsureOperation(!this.m_started, "BeginExecute cannot be called more than once.");
					this.m_started = true;
				}
				this.m_mcar = new MultiChainedAsyncResult<WorkTicket>(callback, state, workTicket);
				List<IAsyncResult> list = new List<IAsyncResult>();
				using (List<MultiAsyncOperationRunner.AsyncOperation>.Enumerator enumerator = this.m_asyncOperations.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MultiAsyncOperationRunner.<>c__DisplayClass10_0 CS$<>8__locals1 = new MultiAsyncOperationRunner.<>c__DisplayClass10_0();
						CS$<>8__locals1.<>4__this = this;
						CS$<>8__locals1.asyncOperation = enumerator.Current;
						TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Executing begin method of operation {0}", new object[] { CS$<>8__locals1.asyncOperation.Name });
						IAsyncResult ar = null;
						Exception ex = TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
						{
							ar = CS$<>8__locals1.asyncOperation.BeginMethod(new AsyncCallback(CS$<>8__locals1.<>4__this.m_mcar.BeginAsyncFunctionCallback), CS$<>8__locals1.asyncOperation);
						});
						if (ex != null)
						{
							this.AddException(CS$<>8__locals1.asyncOperation.Name, ex);
						}
						else
						{
							list.Add(ar);
						}
					}
				}
				this.m_mcar.BeginJoin(list);
				disposeController.PreventDispose();
				mcar = this.m_mcar;
			}
			return mcar;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0003647C File Offset: 0x0003467C
		public void EndExecute(IAsyncResult ar)
		{
			ExtendedDiagnostics.EnsureArgument("ar", ar == this.m_mcar, "AsyncResult must be the same instance returned from BeginExecute");
			MultiChainedAsyncResult<WorkTicket> multiChainedAsyncResult = (MultiChainedAsyncResult<WorkTicket>)ar;
			using (multiChainedAsyncResult.WorkTicket)
			{
				using (IEnumerator<IAsyncResult> enumerator = multiChainedAsyncResult.EndJoin().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MultiAsyncOperationRunner.<>c__DisplayClass11_0 CS$<>8__locals1 = new MultiAsyncOperationRunner.<>c__DisplayClass11_0();
						CS$<>8__locals1.asyncResult = enumerator.Current;
						MultiAsyncOperationRunner.AsyncOperation asyncOperation = (MultiAsyncOperationRunner.AsyncOperation)CS$<>8__locals1.asyncResult.AsyncState;
						TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Executing end method of operation {0}", new object[] { asyncOperation.Name });
						Exception ex3 = TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
						{
							asyncOperation.EndMethod(CS$<>8__locals1.asyncResult);
						});
						if (ex3 != null)
						{
							this.AddException(asyncOperation.Name, ex3);
						}
					}
				}
				if (this.m_exceptionDictionary != null)
				{
					Exception ex2 = null;
					if (this.m_exceptionDictionary.Any((KeyValuePair<string, Exception> ex) => ex.Value is MonitoredException))
					{
						ex2 = this.m_exceptionDictionary.First((KeyValuePair<string, Exception> ex) => ex.Value is MonitoredException).Value;
					}
					else if (this.m_exceptionDictionary.Any<KeyValuePair<string, Exception>>())
					{
						ex2 = this.m_exceptionDictionary.First<KeyValuePair<string, Exception>>().Value;
					}
					throw new MultiAsyncOperationRunnerException(this.m_exceptionDictionary, string.Empty, ex2);
				}
			}
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00036650 File Offset: 0x00034850
		protected void AddAsyncOperationInternal(MultiAsyncOperationRunner.AsyncOperation asyncOperation)
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Adding operation {0}", new object[] { asyncOperation.Name });
			object @lock = this.m_lock;
			lock (@lock)
			{
				ExtendedDiagnostics.EnsureOperation(!this.m_started, "Cannot add an async operation after BeginExecute was called");
				this.m_asyncOperations.Add(asyncOperation);
			}
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x000366C8 File Offset: 0x000348C8
		private void AddException(string operationName, Exception exception)
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "End method of operation {0} threw exception {1}", new object[] { operationName, exception });
			if (this.m_exceptionDictionary == null)
			{
				this.m_exceptionDictionary = new Dictionary<string, Exception>();
			}
			if (!this.m_exceptionDictionary.ContainsKey(operationName))
			{
				this.m_exceptionDictionary.Add(operationName, exception);
				return;
			}
			string text = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", new object[]
			{
				operationName,
				Guid.NewGuid()
			});
			this.m_exceptionDictionary.Add(text, exception);
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Uniqifying operation {0} to {1} and added exception {2}.", new object[] { operationName, text, exception });
		}

		// Token: 0x040005EE RID: 1518
		private List<MultiAsyncOperationRunner.AsyncOperation> m_asyncOperations;

		// Token: 0x040005EF RID: 1519
		private object m_lock;

		// Token: 0x040005F0 RID: 1520
		private MultiChainedAsyncResult<WorkTicket> m_mcar;

		// Token: 0x040005F1 RID: 1521
		private bool m_started;

		// Token: 0x040005F2 RID: 1522
		private Dictionary<string, Exception> m_exceptionDictionary;

		// Token: 0x020006CC RID: 1740
		public sealed class AsyncOperation
		{
			// Token: 0x06002E7B RID: 11899 RVA: 0x000A23BE File Offset: 0x000A05BE
			public AsyncOperation([NotNull] string name, [NotNull] Func<AsyncCallback, object, IAsyncResult> beginMethod, [NotNull] Action<IAsyncResult> endMethod)
			{
				ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
				ExtendedDiagnostics.EnsureArgumentNotNull<Func<AsyncCallback, object, IAsyncResult>>(beginMethod, "beginMethod");
				ExtendedDiagnostics.EnsureArgumentNotNull<Action<IAsyncResult>>(endMethod, "endMethod");
				this.Name = name;
				this.BeginMethod = beginMethod;
				this.EndMethod = endMethod;
			}

			// Token: 0x06002E7C RID: 11900 RVA: 0x000A23FC File Offset: 0x000A05FC
			public AsyncOperation(string name, ISequencer sequencer)
				: this(name, new Func<AsyncCallback, object, IAsyncResult>(sequencer.BeginExecute), new Action<IAsyncResult>(sequencer.EndExecute))
			{
			}

			// Token: 0x17000739 RID: 1849
			// (get) Token: 0x06002E7D RID: 11901 RVA: 0x000A241F File Offset: 0x000A061F
			// (set) Token: 0x06002E7E RID: 11902 RVA: 0x000A2427 File Offset: 0x000A0627
			public Func<AsyncCallback, object, IAsyncResult> BeginMethod { get; private set; }

			// Token: 0x1700073A RID: 1850
			// (get) Token: 0x06002E7F RID: 11903 RVA: 0x000A2430 File Offset: 0x000A0630
			// (set) Token: 0x06002E80 RID: 11904 RVA: 0x000A2438 File Offset: 0x000A0638
			public Action<IAsyncResult> EndMethod { get; private set; }

			// Token: 0x1700073B RID: 1851
			// (get) Token: 0x06002E81 RID: 11905 RVA: 0x000A2441 File Offset: 0x000A0641
			// (set) Token: 0x06002E82 RID: 11906 RVA: 0x000A2449 File Offset: 0x000A0649
			public string Name { get; private set; }
		}
	}
}
