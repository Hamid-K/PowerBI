using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000258 RID: 600
	public class MultiAsyncOperationJoiner : IIdentifiable
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00035E27 File Offset: 0x00034027
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x00035E2F File Offset: 0x0003402F
		public string Name { get; private set; }

		// Token: 0x06000FCD RID: 4045 RVA: 0x00035E38 File Offset: 0x00034038
		public MultiAsyncOperationJoiner([NotNull] string name)
		{
			Ensure.ArgNotNullOrEmpty(name, "name");
			this.m_lock = new object();
			this.m_asyncResults = new List<IAsyncResult>();
			this.m_mcar = new MultiChainedAsyncResult<WorkTicket>(new AsyncCallback(this.OnAllAsyncOperationsCompleted), null, null);
			this.Name = name;
			this.m_stopwatch = Stopwatch.StartNew();
			TraceSourceBase<MultiAsyncOperationTrace>.Tracer.TraceVerbose("MultiAsyncOperationJoiner({0}): Starting", new object[] { this.Name });
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00035EB8 File Offset: 0x000340B8
		public void InvokeAsyncOperation([NotNull] string name, [NotNull] Func<AsyncCallback, object, IAsyncResult> beginMethod, [NotNull] Action<IAsyncResult> endMethod)
		{
			Ensure.ArgNotNullOrEmpty(name, "name");
			Ensure.ArgNotNull<Func<AsyncCallback, object, IAsyncResult>>(beginMethod, "beginMethod");
			Ensure.ArgNotNull<Action<IAsyncResult>>(endMethod, "endMethod");
			TraceSourceBase<MultiAsyncOperationTrace>.Tracer.TraceVerbose("MultiAsyncOperationJoiner({0}): Invoking operation '{1}'", new object[] { this.Name, name });
			AsyncCallback <>9__1;
			Exception ex = TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
			{
				Func<AsyncCallback, object, IAsyncResult> beginMethod2 = beginMethod;
				AsyncCallback asyncCallback;
				if ((asyncCallback = <>9__1) == null)
				{
					asyncCallback = (<>9__1 = delegate(IAsyncResult iar)
					{
						Exception ex2 = TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
						{
							endMethod(iar);
						});
						if (ex2 != null)
						{
							this.AddException(name, ex2);
						}
						TraceSourceBase<MultiAsyncOperationTrace>.Tracer.TraceVerbose("MultiAsyncOperationJoiner({0}): Received completion of operation '{1}'", new object[] { this.Name, name });
						this.m_mcar.BeginAsyncFunctionCallback(iar);
					});
				}
				IAsyncResult asyncResult = beginMethod2(asyncCallback, name);
				object @lock = this.m_lock;
				lock (@lock)
				{
					this.m_asyncResults.Add(asyncResult);
				}
			});
			if (ex != null)
			{
				this.AddException(name, ex);
			}
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00035F64 File Offset: 0x00034164
		public IAsyncResult BeginJoin(AsyncCallback callback, object state)
		{
			TraceSourceBase<MultiAsyncOperationTrace>.Tracer.TraceVerbose("MultiAsyncOperationJoiner({0}): Starting to wait for all operations to complete", new object[] { this.Name });
			List<IAsyncResult> list = null;
			object @lock = this.m_lock;
			lock (@lock)
			{
				Ensure.IsNull<VoidAsyncResult>(this.m_var, "m_var");
				this.m_var = new VoidAsyncResult(callback, state);
				list = this.m_asyncResults;
				this.m_asyncResults = null;
			}
			this.m_mcar.BeginJoin(list);
			return this.m_var;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00035FFC File Offset: 0x000341FC
		public void EndJoin(IAsyncResult asyncResult)
		{
			this.m_mcar.EndJoin();
			this.m_var.End();
			TraceSourceBase<MultiAsyncOperationTrace>.Tracer.TraceVerbose("MultiAsyncOperationJoiner({0}): All operations have completed (in {1} msec since construction)", new object[]
			{
				this.Name,
				this.m_stopwatch.ElapsedMilliseconds
			});
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

		// Token: 0x06000FD1 RID: 4049 RVA: 0x000360F2 File Offset: 0x000342F2
		private void OnAllAsyncOperationsCompleted(IAsyncResult ar)
		{
			this.m_var.SignalCompletion(ar.CompletedSynchronously);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00036108 File Offset: 0x00034308
		private void AddException(string name, Exception ex)
		{
			TraceSourceBase<MultiAsyncOperationTrace>.Tracer.TraceWarning("MultiAsyncOperationJoiner({0}): Exception thrown during operation '{1}': '{2}'", new object[] { this.Name, name, ex });
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (this.m_exceptionDictionary == null)
				{
					this.m_exceptionDictionary = new Dictionary<string, Exception>();
				}
				if (this.m_exceptionDictionary.ContainsKey(name))
				{
					string text = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", new object[]
					{
						name,
						Guid.NewGuid()
					});
					this.m_exceptionDictionary.Add(text, ex);
					TraceSourceBase<MultiAsyncOperationTrace>.Tracer.TraceWarning("MultiAsyncOperationJoiner({0}): Failed to collect exception as the name isn't unique for operation '{1}'", new object[] { this.Name, name });
				}
				else
				{
					this.m_exceptionDictionary.Add(name, ex);
				}
			}
		}

		// Token: 0x040005E7 RID: 1511
		private object m_lock;

		// Token: 0x040005E8 RID: 1512
		private List<IAsyncResult> m_asyncResults;

		// Token: 0x040005E9 RID: 1513
		private MultiChainedAsyncResult<WorkTicket> m_mcar;

		// Token: 0x040005EA RID: 1514
		private VoidAsyncResult m_var;

		// Token: 0x040005EB RID: 1515
		private Dictionary<string, Exception> m_exceptionDictionary;

		// Token: 0x040005ED RID: 1517
		private Stopwatch m_stopwatch;
	}
}
