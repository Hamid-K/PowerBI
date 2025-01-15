using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000188 RID: 392
	public static class AsyncInvoker
	{
		// Token: 0x06000A13 RID: 2579 RVA: 0x00022B6C File Offset: 0x00020D6C
		private static void Invoke(AsyncInvoker.AsyncInvokerContext context)
		{
			Task.Factory.StartNew(delegate(object taskContext)
			{
				AsyncInvoker.AsyncInvokerContext asyncInvokerContext = (AsyncInvoker.AsyncInvokerContext)taskContext;
				if (asyncInvokerContext.m_event != null)
				{
					Exception ex = TopLevelHandler.Run(null, TopLevelHandlerOption.SwallowNonfatal, asyncInvokerContext.m_method);
					if (ex != null)
					{
						asyncInvokerContext.m_exception = ex;
					}
					asyncInvokerContext.m_event.Set();
					return;
				}
				TopLevelHandler.Run(null, TopLevelHandlerOption.SwallowBenign, asyncInvokerContext.m_method);
			}, context, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00022BA4 File Offset: 0x00020DA4
		public static void InvokeMethodAsynchronously([NotNull] Action method, WaitOrNot wait, string methodName)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action>(method, "method");
			if (wait == WaitOrNot.Wait)
			{
				AsyncInvoker.InvokeMethodAsynchronously(method, new SyncWaiterMethod(AsyncInvoker.TrivialWait), methodName);
				return;
			}
			AsyncInvoker.InvokeMethodAsynchronously(method, null, methodName);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00022BD1 File Offset: 0x00020DD1
		public static void InvokeMethodAsynchronously<T>(Action<T> method, T t, WaitOrNot wait, string methodName)
		{
			AsyncInvoker.InvokeMethodAsynchronously(delegate
			{
				method(t);
			}, wait, methodName);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00022BF8 File Offset: 0x00020DF8
		public static void InvokeMethodAsynchronously<T1, T2>(Action<T1, T2> method, T1 t1, T2 t2, WaitOrNot wait, string methodName)
		{
			AsyncInvoker.InvokeMethodAsynchronously(delegate
			{
				method(t1, t2);
			}, wait, methodName);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00022C27 File Offset: 0x00020E27
		public static void InvokeMethodAsynchronously<T1, T2, T3>(Action<T1, T2, T3> method, T1 t1, T2 t2, T3 t3, WaitOrNot wait, string methodName)
		{
			AsyncInvoker.InvokeMethodAsynchronously(delegate
			{
				method(t1, t2, t3);
			}, wait, methodName);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00022C5E File Offset: 0x00020E5E
		public static void InvokeMethodAsynchronously<T1, T2, T3, T4>(Action<T1, T2, T3, T4> method, T1 t1, T2 t2, T3 t3, T4 t4, WaitOrNot wait, string methodName)
		{
			AsyncInvoker.InvokeMethodAsynchronously(delegate
			{
				method(t1, t2, t3, t4);
			}, wait, methodName);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00022CA0 File Offset: 0x00020EA0
		public static void InvokeMethodAsynchronously([NotNull] Action method, SyncWaiterMethod syncWaitingMethod, string methodName)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action>(method, "method");
			AsyncInvoker.AsyncInvokerContext asyncInvokerContext = new AsyncInvoker.AsyncInvokerContext();
			asyncInvokerContext.m_method = method;
			if (syncWaitingMethod != null)
			{
				asyncInvokerContext.m_event = new ManualResetEvent(false);
				AsyncInvoker.Invoke(asyncInvokerContext);
				syncWaitingMethod(asyncInvokerContext.m_event);
				if (asyncInvokerContext.m_exception != null)
				{
					TraceSourceBase<UtilsTrace>.Tracer.TraceInformation("AsyncInvoker had an exception when calling {0}", new object[] { methodName });
					ExceptionDispatchInfo.Capture(asyncInvokerContext.m_exception).Throw();
					return;
				}
			}
			else
			{
				AsyncInvoker.Invoke(asyncInvokerContext);
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00022D1E File Offset: 0x00020F1E
		public static void TrivialWait([NotNull] WaitHandle waitHandle)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<WaitHandle>(waitHandle, "waitHandle");
			waitHandle.WaitOne();
		}

		// Token: 0x02000655 RID: 1621
		private class AsyncInvokerContext
		{
			// Token: 0x040011D7 RID: 4567
			public Action m_method;

			// Token: 0x040011D8 RID: 4568
			public ManualResetEvent m_event;

			// Token: 0x040011D9 RID: 4569
			public Exception m_exception;
		}
	}
}
