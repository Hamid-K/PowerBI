using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.MonitoredUtils;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200014F RID: 335
	public static class AsyncUtils
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x0001E730 File Offset: 0x0001C930
		public static async Task<T> ExecuteInMonitoredScope<T>(ActivityType activityType, IActivityFactory activityFactory, IMonitoredActivityCompletionModelFactory monitoredActivityCompletionModelFactory, WorkTicket workTicket, Func<Task<T>> asyncMethod, Predicate<IMonitoredError> shouldActivityEndWithSuccess = null, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ActivityType>(activityType, "activityType");
			ExtendedDiagnostics.EnsureArgumentNotNull<IActivityFactory>(activityFactory, "activityFactory");
			return await AsyncUtils.ExecuteInMonitoredScope<T>(activityType, activityFactory.CreateSyncActivity(activityType), monitoredActivityCompletionModelFactory, workTicket, asyncMethod, shouldActivityEndWithSuccess, activityContext, auditContext);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001E7B4 File Offset: 0x0001C9B4
		public static async Task<T> ExecuteInMonitoredScope<T>(ActivityType activityType, IActivityFactory activityFactory, IMonitoredActivityCompletionModelFactory monitoredActivityCompletionModelFactory, WorkTicket workTicket, Func<Task<T>> asyncMethod, Func<Exception, IMonitoredError> monitoredErrorProvider, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ActivityType>(activityType, "activityType");
			ExtendedDiagnostics.EnsureArgumentNotNull<IActivityFactory>(activityFactory, "activityFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<IMonitoredActivityCompletionModelFactory>(monitoredActivityCompletionModelFactory, "monitoredActivityCompletionModelFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<WorkTicket>(workTicket, "workTicket");
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<Task<T>>>(asyncMethod, "asyncMethod");
			T t2;
			using (activityFactory.CreateSyncActivity(activityType))
			{
				try
				{
					MonitoredActivityCompletionModel completionModel = monitoredActivityCompletionModelFactory.CreateMonitoredActivityCompletionModel(activityType);
					try
					{
						completionModel.FireActivityStartedEvent(true, activityContext, null);
						T t = await asyncMethod();
						completionModel.FireActivityCompletedSuccessfullyEvent(true, activityContext, auditContext);
						t2 = t;
					}
					catch (Exception ex)
					{
						AsyncUtils.HandleActivityException(ex, completionModel, monitoredErrorProvider, activityContext, auditContext);
						throw;
					}
				}
				finally
				{
					if (workTicket != null)
					{
						((IDisposable)workTicket).Dispose();
					}
				}
			}
			return t2;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0001E838 File Offset: 0x0001CA38
		public static async Task<T> ExecuteInMonitoredScope<T>(ActivityType activityType, SyncActivity activity, IMonitoredActivityCompletionModelFactory monitoredActivityCompletionModelFactory, WorkTicket workTicket, Func<Task<T>> asyncMethod, Predicate<IMonitoredError> shouldActivityEndWithSuccess = null, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ActivityType>(activityType, "activityType");
			ExtendedDiagnostics.EnsureArgumentNotNull<SyncActivity>(activity, "activity");
			ExtendedDiagnostics.EnsureArgumentNotNull<IMonitoredActivityCompletionModelFactory>(monitoredActivityCompletionModelFactory, "monitoredActivityCompletionModelFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<WorkTicket>(workTicket, "workTicket");
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<Task<T>>>(asyncMethod, "asyncMethod");
			T t2;
			try
			{
				try
				{
					MonitoredActivityCompletionModel completionModel = monitoredActivityCompletionModelFactory.CreateMonitoredActivityCompletionModel(activityType);
					try
					{
						completionModel.FireActivityStartedEvent(true, activityContext, null);
						T t = await asyncMethod();
						completionModel.FireActivityCompletedSuccessfullyEvent(true, activityContext, auditContext);
						t2 = t;
					}
					catch (Exception ex)
					{
						AsyncUtils.HandleActivityException(ex, completionModel, shouldActivityEndWithSuccess, activityContext, auditContext);
						throw;
					}
				}
				finally
				{
					if (workTicket != null)
					{
						((IDisposable)workTicket).Dispose();
					}
				}
			}
			finally
			{
				if (activity != null)
				{
					((IDisposable)activity).Dispose();
				}
			}
			return t2;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001E8BC File Offset: 0x0001CABC
		public static async Task ExecuteInMonitoredScope(ActivityType activityType, IActivityFactory activityFactory, IMonitoredActivityCompletionModelFactory monitoredActivityCompletionModelFactory, WorkTicket workTicket, Func<Task> asyncMethod, Predicate<IMonitoredError> shouldActivityEndWithSuccess = null, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ActivityType>(activityType, "activityType");
			ExtendedDiagnostics.EnsureArgumentNotNull<IActivityFactory>(activityFactory, "activityFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<IMonitoredActivityCompletionModelFactory>(monitoredActivityCompletionModelFactory, "monitoredActivityCompletionModelFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<WorkTicket>(workTicket, "workTicket");
			using (activityFactory.CreateSyncActivity(activityType))
			{
				using (workTicket)
				{
					MonitoredActivityCompletionModel completionModel = monitoredActivityCompletionModelFactory.CreateMonitoredActivityCompletionModel(activityType);
					try
					{
						completionModel.FireActivityStartedEvent(true, activityContext, null);
						await asyncMethod();
						completionModel.FireActivityCompletedSuccessfullyEvent(true, activityContext, auditContext);
					}
					catch (Exception ex)
					{
						AsyncUtils.HandleActivityException(ex, completionModel, shouldActivityEndWithSuccess, activityContext, auditContext);
						throw;
					}
					completionModel = null;
				}
				WorkTicket workTicket2 = null;
			}
			SyncActivity syncActivity = null;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001E940 File Offset: 0x0001CB40
		public static async Task ExecuteInMonitoredScope(ActivityType activityType, IActivityFactory activityFactory, IMonitoredActivityCompletionModelFactory monitoredActivityCompletionModelFactory, Func<Task> asyncMethod, Predicate<IMonitoredError> shouldActivityEndWithSuccess = null, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ActivityType>(activityType, "activityType");
			ExtendedDiagnostics.EnsureArgumentNotNull<IActivityFactory>(activityFactory, "activityFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<IMonitoredActivityCompletionModelFactory>(monitoredActivityCompletionModelFactory, "monitoredActivityCompletionModelFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<Task>>(asyncMethod, "asyncMethod");
			using (activityFactory.CreateSyncActivity(activityType))
			{
				MonitoredActivityCompletionModel completionModel = monitoredActivityCompletionModelFactory.CreateMonitoredActivityCompletionModel(activityType);
				try
				{
					completionModel.FireActivityStartedEvent(true, activityContext, null);
					await asyncMethod();
					completionModel.FireActivityCompletedSuccessfullyEvent(true, activityContext, auditContext);
				}
				catch (Exception ex)
				{
					AsyncUtils.HandleActivityException(ex, completionModel, shouldActivityEndWithSuccess, activityContext, auditContext);
					throw;
				}
				completionModel = null;
			}
			SyncActivity syncActivity = null;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001E9B8 File Offset: 0x0001CBB8
		public static async Task ExecuteInMonitoredScope(ActivityType activityType, IActivityFactory activityFactory, IMonitoredActivityCompletionModelFactory monitoredActivityCompletionModelFactory, WorkTicket workTicket, Func<Task> asyncMethod, Func<Exception, IMonitoredError> monitoredErrorProvider, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ActivityType>(activityType, "activityType");
			ExtendedDiagnostics.EnsureArgumentNotNull<IActivityFactory>(activityFactory, "activityFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<IMonitoredActivityCompletionModelFactory>(monitoredActivityCompletionModelFactory, "monitoredActivityCompletionModelFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<WorkTicket>(workTicket, "workTicket");
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<Task>>(asyncMethod, "asyncMethod");
			using (activityFactory.CreateSyncActivity(activityType))
			{
				using (workTicket)
				{
					MonitoredActivityCompletionModel completionModel = monitoredActivityCompletionModelFactory.CreateMonitoredActivityCompletionModel(activityType);
					try
					{
						completionModel.FireActivityStartedEvent(true, activityContext, null);
						await asyncMethod();
						completionModel.FireActivityCompletedSuccessfullyEvent(true, activityContext, auditContext);
					}
					catch (Exception ex)
					{
						AsyncUtils.HandleActivityException(ex, completionModel, monitoredErrorProvider, activityContext, auditContext);
						throw;
					}
					completionModel = null;
				}
				WorkTicket workTicket2 = null;
			}
			SyncActivity syncActivity = null;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001EA3C File Offset: 0x0001CC3C
		public static async Task ExecuteInMonitoredScope(ActivityType activityType, SyncActivity activity, IMonitoredActivityCompletionModelFactory monitoredActivityCompletionModelFactory, WorkTicket workTicket, Func<Task> asyncMethod, Predicate<IMonitoredError> shouldActivityEndWithSuccess = null, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ActivityType>(activityType, "activityType");
			ExtendedDiagnostics.EnsureArgumentNotNull<SyncActivity>(activity, "activity");
			ExtendedDiagnostics.EnsureArgumentNotNull<IMonitoredActivityCompletionModelFactory>(monitoredActivityCompletionModelFactory, "monitoredActivityCompletionModelFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<WorkTicket>(workTicket, "workTicket");
			using (activity)
			{
				using (workTicket)
				{
					MonitoredActivityCompletionModel completionModel = monitoredActivityCompletionModelFactory.CreateMonitoredActivityCompletionModel(activityType);
					try
					{
						completionModel.FireActivityStartedEvent(true, activityContext, null);
						await asyncMethod();
						completionModel.FireActivityCompletedSuccessfullyEvent(true, activityContext, auditContext);
					}
					catch (Exception ex)
					{
						AsyncUtils.HandleActivityException(ex, completionModel, shouldActivityEndWithSuccess, activityContext, auditContext);
						throw;
					}
					completionModel = null;
				}
				WorkTicket workTicket2 = null;
			}
			SyncActivity syncActivity = null;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001EAC0 File Offset: 0x0001CCC0
		public static T ExecuteInMonitoredScope<T>(ActivityType activityType, IActivityFactory activityFactory, IMonitoredActivityCompletionModelFactory monitoredActivityCompletionModelFactory, WorkTicket workTicket, Func<T> syncMethod, Predicate<IMonitoredError> shouldActivityEndWithSuccess = null, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ActivityType>(activityType, "activityType");
			ExtendedDiagnostics.EnsureArgumentNotNull<IActivityFactory>(activityFactory, "activityFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<IMonitoredActivityCompletionModelFactory>(monitoredActivityCompletionModelFactory, "monitoredActivityCompletionModelFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<WorkTicket>(workTicket, "workTicket");
			T t2;
			using (activityFactory.CreateSyncActivity(activityType))
			{
				try
				{
					MonitoredActivityCompletionModel monitoredActivityCompletionModel = monitoredActivityCompletionModelFactory.CreateMonitoredActivityCompletionModel(activityType);
					try
					{
						if (typeof(Task).IsAssignableFrom(typeof(T)))
						{
							TraceSourceBase<MonitoredUtilsTrace>.Tracer.TraceError("Incorrectly calling a synchronous version of ExecuteInMonitoredScope. {0}", new object[] { UtilsContext.Current.GetActivityStackRepresentation() });
						}
						monitoredActivityCompletionModel.FireActivityStartedEvent(true, activityContext, null);
						T t = syncMethod();
						monitoredActivityCompletionModel.FireActivityCompletedSuccessfullyEvent(true, activityContext, auditContext);
						t2 = t;
					}
					catch (Exception ex)
					{
						AsyncUtils.HandleActivityException(ex, monitoredActivityCompletionModel, shouldActivityEndWithSuccess, activityContext, auditContext);
						throw;
					}
				}
				finally
				{
					if (workTicket != null)
					{
						((IDisposable)workTicket).Dispose();
					}
				}
			}
			return t2;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001EBB4 File Offset: 0x0001CDB4
		public static T ExecuteInMonitoredScope<T>(ActivityType activityType, IActivityFactory activityFactory, IMonitoredActivityCompletionModelFactory monitoredActivityCompletionModelFactory, WorkTicket workTicket, Func<T> syncMethod, Func<Exception, IMonitoredError> monitoredErrorProvider, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ActivityType>(activityType, "activityType");
			ExtendedDiagnostics.EnsureArgumentNotNull<IActivityFactory>(activityFactory, "activityFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<IMonitoredActivityCompletionModelFactory>(monitoredActivityCompletionModelFactory, "monitoredActivityCompletionModelFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<WorkTicket>(workTicket, "workTicket");
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<T>>(syncMethod, "syncMethod");
			T t2;
			using (activityFactory.CreateSyncActivity(activityType))
			{
				try
				{
					MonitoredActivityCompletionModel monitoredActivityCompletionModel = monitoredActivityCompletionModelFactory.CreateMonitoredActivityCompletionModel(activityType);
					try
					{
						if (typeof(Task).IsAssignableFrom(typeof(T)))
						{
							TraceSourceBase<MonitoredUtilsTrace>.Tracer.TraceError("Incorrectly calling a synchronous version of ExecuteInMonitoredScope. {0}", new object[] { UtilsContext.Current.GetActivityStackRepresentation() });
						}
						monitoredActivityCompletionModel.FireActivityStartedEvent(true, activityContext, null);
						T t = syncMethod();
						monitoredActivityCompletionModel.FireActivityCompletedSuccessfullyEvent(true, activityContext, auditContext);
						t2 = t;
					}
					catch (Exception ex)
					{
						AsyncUtils.HandleActivityException(ex, monitoredActivityCompletionModel, monitoredErrorProvider, activityContext, auditContext);
						throw;
					}
				}
				finally
				{
					if (workTicket != null)
					{
						((IDisposable)workTicket).Dispose();
					}
				}
			}
			return t2;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001ECB4 File Offset: 0x0001CEB4
		public static void ExecuteInMonitoredScope(ActivityType activityType, IActivityFactory activityFactory, IMonitoredActivityCompletionModelFactory monitoredActivityCompletionModelFactory, WorkTicket workTicket, Action syncMethod, Predicate<IMonitoredError> shouldActivityEndWithSuccess = null, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ActivityType>(activityType, "activityType");
			ExtendedDiagnostics.EnsureArgumentNotNull<IActivityFactory>(activityFactory, "activityFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<IMonitoredActivityCompletionModelFactory>(monitoredActivityCompletionModelFactory, "monitoredActivityCompletionModelFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<WorkTicket>(workTicket, "workTicket");
			using (activityFactory.CreateSyncActivity(activityType))
			{
				try
				{
					MonitoredActivityCompletionModel monitoredActivityCompletionModel = monitoredActivityCompletionModelFactory.CreateMonitoredActivityCompletionModel(activityType);
					try
					{
						monitoredActivityCompletionModel.FireActivityStartedEvent(true, activityContext, null);
						syncMethod();
						monitoredActivityCompletionModel.FireActivityCompletedSuccessfullyEvent(true, activityContext, auditContext);
					}
					catch (Exception ex)
					{
						AsyncUtils.HandleActivityException(ex, monitoredActivityCompletionModel, shouldActivityEndWithSuccess, null, auditContext);
						throw;
					}
				}
				finally
				{
					if (workTicket != null)
					{
						((IDisposable)workTicket).Dispose();
					}
				}
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001ED68 File Offset: 0x0001CF68
		public static void ExecuteInMonitoredScope(ActivityType activityType, IActivityFactory activityFactory, IMonitoredActivityCompletionModelFactory monitoredActivityCompletionModelFactory, WorkTicket workTicket, Action syncMethod, Func<Exception, IMonitoredError> monitoredErrorProvider, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ActivityType>(activityType, "activityType");
			ExtendedDiagnostics.EnsureArgumentNotNull<IActivityFactory>(activityFactory, "activityFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<IMonitoredActivityCompletionModelFactory>(monitoredActivityCompletionModelFactory, "monitoredActivityCompletionModelFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<WorkTicket>(workTicket, "workTicket");
			ExtendedDiagnostics.EnsureArgumentNotNull<Action>(syncMethod, "syncMethod");
			using (activityFactory.CreateSyncActivity(activityType))
			{
				try
				{
					MonitoredActivityCompletionModel monitoredActivityCompletionModel = monitoredActivityCompletionModelFactory.CreateMonitoredActivityCompletionModel(activityType);
					try
					{
						monitoredActivityCompletionModel.FireActivityStartedEvent(true, activityContext, null);
						syncMethod();
						monitoredActivityCompletionModel.FireActivityCompletedSuccessfullyEvent(true, activityContext, auditContext);
					}
					catch (Exception ex)
					{
						AsyncUtils.HandleActivityException(ex, monitoredActivityCompletionModel, monitoredErrorProvider, activityContext, auditContext);
						throw;
					}
				}
				finally
				{
					if (workTicket != null)
					{
						((IDisposable)workTicket).Dispose();
					}
				}
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001EE28 File Offset: 0x0001D028
		internal static void HandleActivityException(Exception ex, MonitoredActivityCompletionModel completionModel)
		{
			AsyncUtils.HandleActivityException(ex, completionModel, null, null, null);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001EE34 File Offset: 0x0001D034
		private static void HandleActivityException(Exception ex, MonitoredActivityCompletionModel completionModel, Predicate<IMonitoredError> shouldActivityEndWithSuccess, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			if (ExceptionUtility.IsFatal(ex))
			{
				ExtendedEnvironment.FailSlow(null, ex);
			}
			IMonitoredError monitoredError = ex as IMonitoredError;
			if (monitoredError == null)
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceError("Activity Threw Non Monitored Exception: {0}", new object[] { ex });
				monitoredError = new MonitoredScopeNonMonitoredErrorWrapperException(string.Empty, ex);
			}
			if (monitoredError.IsBenign() || (shouldActivityEndWithSuccess != null && shouldActivityEndWithSuccess(monitoredError)))
			{
				completionModel.FireActivityCompletedSuccessfullyDespiteErrorEvent(monitoredError, activityContext, auditContext);
				return;
			}
			ExtendedEnvironment.CreateMemoryDump(ex, false);
			completionModel.FireActivityCompletedWithFailureEvent(monitoredError, activityContext, auditContext);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001EEB0 File Offset: 0x0001D0B0
		private static void HandleActivityException(Exception ex, MonitoredActivityCompletionModel completionModel, Func<Exception, IMonitoredError> monitoredErrorProvider, ActivityContextBase activityContext, IAuditContext auditContext = null)
		{
			if (ExceptionUtility.IsFatal(ex))
			{
				ExtendedEnvironment.FailSlow(null, ex);
			}
			IMonitoredError monitoredError = ex as IMonitoredError;
			if (monitoredError == null)
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceError("Activity Threw Non Monitored Exception: {0}", new object[] { ex });
				if (monitoredErrorProvider != null)
				{
					monitoredError = monitoredErrorProvider(ex);
				}
				if (monitoredError == null)
				{
					monitoredError = new MonitoredScopeNonMonitoredErrorWrapperException(string.Empty, ex);
				}
			}
			if (monitoredError.IsBenign())
			{
				completionModel.FireActivityCompletedSuccessfullyDespiteErrorEvent(monitoredError, activityContext, auditContext);
				return;
			}
			ExtendedEnvironment.CreateMemoryDump(ex, false);
			completionModel.FireActivityCompletedWithFailureEvent(monitoredError, activityContext, auditContext);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001EF30 File Offset: 0x0001D130
		public static Task<TResult> FromAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this TaskFactory factory, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, object state, TaskCreationOptions creationOptions = TaskCreationOptions.None, TaskScheduler scheduler = null)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, object, IAsyncResult>>(beginMethod, "beginMethod");
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<IAsyncResult, TResult>>(endMethod, "endMethod");
			scheduler = scheduler ?? TaskScheduler.Current;
			TaskCompletionSource<TResult> tcs = new TaskCompletionSource<TResult>(state, creationOptions);
			try
			{
				beginMethod(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, delegate(IAsyncResult iar)
				{
					tcs.TrySetResult(endMethod(iar));
				}, state);
			}
			catch
			{
				tcs.TrySetResult(default(TResult));
				throw;
			}
			return tcs.Task;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001EFF0 File Offset: 0x0001D1F0
		public static void RunWithRetries(this AsyncRetryPolicy retryPolicy, Action action)
		{
			int num = 0;
			for (;;)
			{
				try
				{
					num++;
					action();
					break;
				}
				catch (Exception ex)
				{
					TraceSourceBase<UtilsTrace>.Tracer.TraceError("'{0}' failed to run, attempt {1} of {2}. error: {3}", new object[] { "RunWithRetries", num, retryPolicy.NumberOfTries, ex });
					if (num == retryPolicy.NumberOfTries)
					{
						throw;
					}
					if (ExceptionUtility.IsFatal(ex) || retryPolicy.IsPermanentException(ex))
					{
						throw;
					}
				}
				if (retryPolicy.DelayBetweenRetries.TotalMilliseconds > 0.0)
				{
					Thread.Sleep((int)retryPolicy.DelayBetweenRetries.TotalMilliseconds);
				}
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001F0B8 File Offset: 0x0001D2B8
		public static async Task RunWithRetriesAsync(this AsyncRetryPolicy retryPolicy, Func<Task> action, bool exponentialBackoff = false)
		{
			int retryAttempt = 0;
			for (;;)
			{
				try
				{
					int num = retryAttempt;
					retryAttempt = num + 1;
					await action();
					break;
				}
				catch (Exception ex)
				{
					TraceSourceBase<UtilsTrace>.Tracer.TraceError("'{0}' failed to run, attempt {1} of {2}. error: {3}", new object[] { "RunWithRetries", retryAttempt, retryPolicy.NumberOfTries, ex });
					if (retryAttempt == retryPolicy.NumberOfTries)
					{
						throw;
					}
					if (ExceptionUtility.IsFatal(ex) || retryPolicy.IsPermanentException(ex))
					{
						throw;
					}
				}
				if (retryPolicy.DelayBetweenRetries.TotalMilliseconds > 0.0)
				{
					await Task.Delay((int)retryPolicy.DelayBetweenRetries.TotalMilliseconds);
				}
				if (exponentialBackoff)
				{
					retryPolicy.DelayBetweenRetries = TimeSpan.FromMilliseconds(retryPolicy.DelayBetweenRetries.TotalMilliseconds * 2.0);
				}
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001F110 File Offset: 0x0001D310
		public static async Task RunWithRetriesAsync(this AsyncRetryPolicy retryPolicy, Func<int, Task> action, bool exponentialBackoff = false, bool dynamicDelay = false)
		{
			int retryAttempt = 0;
			for (;;)
			{
				try
				{
					int num = retryAttempt;
					retryAttempt = num + 1;
					await action(retryAttempt);
					break;
				}
				catch (Exception ex)
				{
					TraceSourceBase<UtilsTrace>.Tracer.TraceError("'{0}' failed to run, attempt {1} of {2}. error: {3}", new object[] { "RunWithRetries", retryAttempt, retryPolicy.NumberOfTries, ex });
					if (retryAttempt == retryPolicy.NumberOfTries)
					{
						throw;
					}
					if (ExceptionUtility.IsFatal(ex) || retryPolicy.IsPermanentException(ex))
					{
						throw;
					}
					if (retryPolicy.UseExponentialBackoff != null && retryPolicy.UseExponentialBackoff(ex))
					{
						exponentialBackoff = true;
					}
					else if (dynamicDelay)
					{
						if (retryPolicy.DetermineDelayBetweenRetries == null)
						{
							TraceSourceBase<UtilsTrace>.Tracer.TraceError("'{0}' is 'true' but '{1}' does not have '{2}' defined. Throwing exception.", new object[] { "dynamicDelay", "retryPolicy", "DetermineDelayBetweenRetries" });
							throw new AggregateException(new Exception[]
							{
								new DetermineDelayBetweenRetriesUndefinedException(),
								ex
							});
						}
						TimeSpan? timeSpan = retryPolicy.DetermineDelayBetweenRetries(ex);
						if (timeSpan != null)
						{
							retryPolicy.DelayBetweenRetries = timeSpan.Value;
						}
					}
				}
				if (retryPolicy.DelayBetweenRetries.TotalMilliseconds > 0.0)
				{
					await Task.Delay((int)retryPolicy.DelayBetweenRetries.TotalMilliseconds);
				}
				if (exponentialBackoff)
				{
					retryPolicy.DelayBetweenRetries = TimeSpan.FromMilliseconds(retryPolicy.DelayBetweenRetries.TotalMilliseconds * 2.0);
				}
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001F170 File Offset: 0x0001D370
		public static async Task<T> RunWithRetriesAsync<T>(this AsyncRetryPolicy retryPolicy, Func<Task<T>> action, bool exponentialBackoff = false, bool dynamicDelay = false)
		{
			int retryAttempt = 0;
			T t;
			for (;;)
			{
				try
				{
					int num = retryAttempt;
					retryAttempt = num + 1;
					t = await action();
					break;
				}
				catch (Exception ex)
				{
					TraceSourceBase<UtilsTrace>.Tracer.TraceError("'{0}' failed to run, attempt {1} of {2}. error: {3}", new object[] { "RunWithRetries", retryAttempt, retryPolicy.NumberOfTries, ex });
					if (retryAttempt == retryPolicy.NumberOfTries)
					{
						throw;
					}
					if (ExceptionUtility.IsFatal(ex) || retryPolicy.IsPermanentException(ex))
					{
						throw;
					}
					if (retryPolicy.UseExponentialBackoff != null && retryPolicy.UseExponentialBackoff(ex))
					{
						exponentialBackoff = true;
					}
					else if (dynamicDelay)
					{
						if (retryPolicy.DetermineDelayBetweenRetries == null)
						{
							TraceSourceBase<UtilsTrace>.Tracer.TraceError("'{0}' is 'true' but '{1}' does not have '{2}' defined. Throwing exception.", new object[] { "dynamicDelay", "retryPolicy", "DetermineDelayBetweenRetries" });
							throw new AggregateException(new Exception[]
							{
								new DetermineDelayBetweenRetriesUndefinedException(),
								ex
							});
						}
						TimeSpan? timeSpan = retryPolicy.DetermineDelayBetweenRetries(ex);
						if (timeSpan != null)
						{
							retryPolicy.DelayBetweenRetries = timeSpan.Value;
						}
					}
				}
				if (retryPolicy.DelayBetweenRetries.TotalMilliseconds > 0.0)
				{
					await Task.Delay((int)retryPolicy.DelayBetweenRetries.TotalMilliseconds);
				}
				if (exponentialBackoff)
				{
					retryPolicy.DelayBetweenRetries = TimeSpan.FromMilliseconds(retryPolicy.DelayBetweenRetries.TotalMilliseconds * 2.0);
				}
			}
			return t;
		}
	}
}
