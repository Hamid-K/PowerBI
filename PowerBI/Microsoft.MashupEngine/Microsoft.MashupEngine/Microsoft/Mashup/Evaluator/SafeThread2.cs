using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D4A RID: 7498
	public static class SafeThread2
	{
		// Token: 0x0600BA95 RID: 47765 RVA: 0x0025C640 File Offset: 0x0025A840
		public static void AddExceptionHandler(IExceptionHandler2 handler)
		{
			List<IExceptionHandler2> list = SafeThread2.exceptionHandlers;
			lock (list)
			{
				SafeThread2.exceptionHandlers.Add(handler);
			}
		}

		// Token: 0x0600BA96 RID: 47766 RVA: 0x0025C684 File Offset: 0x0025A884
		public static void RemoveExceptionHandler(IExceptionHandler2 handler)
		{
			List<IExceptionHandler2> list = SafeThread2.exceptionHandlers;
			lock (list)
			{
				SafeThread2.exceptionHandlers.Remove(handler);
			}
		}

		// Token: 0x0600BA97 RID: 47767 RVA: 0x0025C6CC File Offset: 0x0025A8CC
		public static ParameterizedThreadStart CreateThreadStart(ParameterizedThreadStart threadStart)
		{
			Action<object> wrapped = SafeThread2.CreateAction(delegate(object o)
			{
				threadStart(o);
			});
			return delegate(object o)
			{
				wrapped(o);
			};
		}

		// Token: 0x0600BA98 RID: 47768 RVA: 0x0025C6FC File Offset: 0x0025A8FC
		public static ThreadStart CreateThreadStart(ThreadStart threadStart)
		{
			Action wrapped = SafeThread2.CreateAction(delegate
			{
				threadStart();
			});
			return delegate
			{
				wrapped();
			};
		}

		// Token: 0x0600BA99 RID: 47769 RVA: 0x0025C72C File Offset: 0x0025A92C
		public static WaitCallback CreateWaitCallback(WaitCallback waitCallback)
		{
			Action<object> wrapped = SafeThread2.CreateAction(delegate(object o)
			{
				waitCallback(o);
			});
			return delegate(object o)
			{
				wrapped(o);
			};
		}

		// Token: 0x0600BA9A RID: 47770 RVA: 0x0025C75C File Offset: 0x0025A95C
		public static TimerCallback CreateTimerCallback(TimerCallback timerCallback)
		{
			Action<object> wrapped = SafeThread2.CreateAction(delegate(object o)
			{
				timerCallback(o);
			});
			return delegate(object o)
			{
				wrapped(o);
			};
		}

		// Token: 0x0600BA9B RID: 47771 RVA: 0x0025C78C File Offset: 0x0025A98C
		public static void HandleException(Exception e)
		{
			List<IExceptionHandler2> list = SafeThread2.exceptionHandlers;
			IExceptionHandler2[] array;
			lock (list)
			{
				array = SafeThread2.exceptionHandlers.ToArray();
			}
			int num = array.Length - 1;
			while (num >= 0 && !array[num].TryHandleException(e))
			{
				num--;
			}
		}

		// Token: 0x0600BA9C RID: 47772 RVA: 0x0025C7EC File Offset: 0x0025A9EC
		private static Action CreateAction(Action action)
		{
			Action<object> wrapped = SafeThread2.CreateAction(delegate(object o)
			{
				action();
			});
			return delegate
			{
				wrapped(null);
			};
		}

		// Token: 0x0600BA9D RID: 47773 RVA: 0x0025C81C File Offset: 0x0025AA1C
		private static Action<object> CreateAction(Action<object> action)
		{
			return delegate(object o)
			{
				try
				{
					action(o);
				}
				catch (Exception ex)
				{
					SafeThread2.HandleException(ex);
				}
			};
		}

		// Token: 0x04005EF5 RID: 24309
		private static readonly List<IExceptionHandler2> exceptionHandlers = new List<IExceptionHandler2>
		{
			new SafeThread2.DefaultExceptionHandler()
		};

		// Token: 0x02001D4B RID: 7499
		private sealed class DefaultExceptionHandler : IExceptionHandler2
		{
			// Token: 0x0600BA9F RID: 47775 RVA: 0x0025C84C File Offset: 0x0025AA4C
			public bool TryHandleException(Exception exception)
			{
				try
				{
					using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("SafeThread2/DefaultExceptionHandler/HandleException", null, TraceEventType.Information, null))
					{
						SafeExceptions.TraceIsSafeException(hostTrace, exception);
					}
					EvaluatorTracing.Service.Flush();
					Trace.Flush();
					goto IL_0035;
				}
				finally
				{
					throw exception;
				}
				goto IL_0035;
				for (;;)
				{
					IL_0035:
					goto IL_0035;
				}
			}
		}
	}
}
