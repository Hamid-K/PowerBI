using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000121 RID: 289
	public abstract class MonitoredBlock : Block
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x00010777 File Offset: 0x0000E977
		public MonitoredBlock(string name)
			: base(name)
		{
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0001ADA5 File Offset: 0x00018FA5
		protected IMonitoredActivityCompletionModelFactory MonitoredActivityCompletionModelFactory
		{
			get
			{
				return this.m_monitoredActivityCompletionModelFactory;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0001ADAD File Offset: 0x00018FAD
		protected IActivityFactory ActivityFactory
		{
			get
			{
				return this.m_activityFactory;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0001ADB5 File Offset: 0x00018FB5
		protected IEventsKitFactory EventsKitFactory
		{
			get
			{
				return this.m_eventsKitFactory;
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001ADBD File Offset: 0x00018FBD
		protected Task ExecuteInMonitoredScope(ActivityType activityType, Func<Task> asyncMethod, Predicate<IMonitoredError> shouldActivityEndWithSuccess = null, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			return AsyncUtils.ExecuteInMonitoredScope(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), asyncMethod, shouldActivityEndWithSuccess, activityContext, auditContext);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001ADE3 File Offset: 0x00018FE3
		protected Task ExecuteInMonitoredScope(ActivityType activityType, Func<Task> asyncMethod, Func<Exception, IMonitoredError> monitoredErrorProvider, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			return AsyncUtils.ExecuteInMonitoredScope(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), asyncMethod, monitoredErrorProvider, activityContext, auditContext);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001AE09 File Offset: 0x00019009
		protected Task<T> ExecuteInMonitoredScope<T>(ActivityType activityType, Func<Task<T>> asyncMethod, Predicate<IMonitoredError> shouldActivityEndWithSuccess = null, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			return AsyncUtils.ExecuteInMonitoredScope<T>(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), asyncMethod, shouldActivityEndWithSuccess, activityContext, auditContext);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001AE2F File Offset: 0x0001902F
		protected Task<T> ExecuteInMonitoredScope<T>(ActivityType activityType, SyncActivity activity, Func<Task<T>> asyncMethod, Predicate<IMonitoredError> shouldActivityEndWithSuccess = null, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			return AsyncUtils.ExecuteInMonitoredScope<T>(activityType, activity, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), asyncMethod, shouldActivityEndWithSuccess, activityContext, auditContext);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001AE51 File Offset: 0x00019051
		protected Task<T> ExecuteInMonitoredScope<T>(ActivityType activityType, Func<Task<T>> asyncMethod, Func<Exception, IMonitoredError> monitoredErrorProvider, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			return AsyncUtils.ExecuteInMonitoredScope<T>(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), asyncMethod, monitoredErrorProvider, activityContext, auditContext);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001AE78 File Offset: 0x00019078
		protected Task<T> ExecuteInMonitoredScopeWithDynamicRetries<T>(ActivityType activityType, AsyncRetryPolicy retryPolicy, Func<Task<T>> asyncMethod, ActivityContextBase activityContext = null)
		{
			MonitoredBlock.<>c__DisplayClass15_0<T> CS$<>8__locals1 = new MonitoredBlock.<>c__DisplayClass15_0<T>();
			CS$<>8__locals1.retryPolicy = retryPolicy;
			CS$<>8__locals1.asyncMethod = asyncMethod;
			return AsyncUtils.ExecuteInMonitoredScope<T>(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), delegate
			{
				AsyncRetryPolicy retryPolicy2 = CS$<>8__locals1.retryPolicy;
				Func<Task<T>> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate
					{
						MonitoredBlock.<>c__DisplayClass15_0<T>.<<ExecuteInMonitoredScopeWithDynamicRetries>b__1>d <<ExecuteInMonitoredScopeWithDynamicRetries>b__1>d;
						<<ExecuteInMonitoredScopeWithDynamicRetries>b__1>d.<>4__this = CS$<>8__locals1;
						<<ExecuteInMonitoredScopeWithDynamicRetries>b__1>d.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
						<<ExecuteInMonitoredScopeWithDynamicRetries>b__1>d.<>1__state = -1;
						AsyncTaskMethodBuilder<T> <>t__builder = <<ExecuteInMonitoredScopeWithDynamicRetries>b__1>d.<>t__builder;
						<>t__builder.Start<MonitoredBlock.<>c__DisplayClass15_0<T>.<<ExecuteInMonitoredScopeWithDynamicRetries>b__1>d>(ref <<ExecuteInMonitoredScopeWithDynamicRetries>b__1>d);
						return <<ExecuteInMonitoredScopeWithDynamicRetries>b__1>d.<>t__builder.Task;
					});
				}
				return retryPolicy2.RunWithRetriesAsync(func, false, true);
			}, null, activityContext, null);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001AEC8 File Offset: 0x000190C8
		protected Task ExecuteInMonitoredScopeWithRetries(ActivityType activityType, AsyncRetryPolicy retryPolicy, Func<Task> asyncMethod, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			MonitoredBlock.<>c__DisplayClass16_0 CS$<>8__locals1 = new MonitoredBlock.<>c__DisplayClass16_0();
			CS$<>8__locals1.retryPolicy = retryPolicy;
			CS$<>8__locals1.asyncMethod = asyncMethod;
			return AsyncUtils.ExecuteInMonitoredScope(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), delegate
			{
				AsyncRetryPolicy retryPolicy2 = CS$<>8__locals1.retryPolicy;
				Func<Task> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate
					{
						MonitoredBlock.<>c__DisplayClass16_0.<<ExecuteInMonitoredScopeWithRetries>b__1>d <<ExecuteInMonitoredScopeWithRetries>b__1>d;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>4__this = CS$<>8__locals1;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>1__state = -1;
						AsyncTaskMethodBuilder <>t__builder = <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder;
						<>t__builder.Start<MonitoredBlock.<>c__DisplayClass16_0.<<ExecuteInMonitoredScopeWithRetries>b__1>d>(ref <<ExecuteInMonitoredScopeWithRetries>b__1>d);
						return <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder.Task;
					});
				}
				return retryPolicy2.RunWithRetriesAsync(func, false);
			}, null, activityContext, null);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001AF18 File Offset: 0x00019118
		protected Task ExecuteInMonitoredScopeWithRetries(ActivityType activityType, SyncActivity syncActivity, AsyncRetryPolicy retryPolicy, Func<Task> asyncMethod, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			MonitoredBlock.<>c__DisplayClass17_0 CS$<>8__locals1 = new MonitoredBlock.<>c__DisplayClass17_0();
			CS$<>8__locals1.retryPolicy = retryPolicy;
			CS$<>8__locals1.asyncMethod = asyncMethod;
			return AsyncUtils.ExecuteInMonitoredScope(activityType, syncActivity, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), delegate
			{
				AsyncRetryPolicy retryPolicy2 = CS$<>8__locals1.retryPolicy;
				Func<Task> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate
					{
						MonitoredBlock.<>c__DisplayClass17_0.<<ExecuteInMonitoredScopeWithRetries>b__1>d <<ExecuteInMonitoredScopeWithRetries>b__1>d;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>4__this = CS$<>8__locals1;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>1__state = -1;
						AsyncTaskMethodBuilder <>t__builder = <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder;
						<>t__builder.Start<MonitoredBlock.<>c__DisplayClass17_0.<<ExecuteInMonitoredScopeWithRetries>b__1>d>(ref <<ExecuteInMonitoredScopeWithRetries>b__1>d);
						return <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder.Task;
					});
				}
				return retryPolicy2.RunWithRetriesAsync(func, false);
			}, null, activityContext, null);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001AF64 File Offset: 0x00019164
		protected Task ExecuteInMonitoredScopeWithRetries<T1, T2>(ActivityType activityType, AsyncRetryPolicy retryPolicy, Func<T1, T2, Task> asyncMethod, T1 t1, T2 t2, ActivityContextBase activityContext = null)
		{
			MonitoredBlock.<>c__DisplayClass18_0<T1, T2> CS$<>8__locals1 = new MonitoredBlock.<>c__DisplayClass18_0<T1, T2>();
			CS$<>8__locals1.retryPolicy = retryPolicy;
			CS$<>8__locals1.asyncMethod = asyncMethod;
			CS$<>8__locals1.t1 = t1;
			CS$<>8__locals1.t2 = t2;
			return AsyncUtils.ExecuteInMonitoredScope(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), delegate
			{
				AsyncRetryPolicy retryPolicy2 = CS$<>8__locals1.retryPolicy;
				Func<Task> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate
					{
						MonitoredBlock.<>c__DisplayClass18_0<T1, T2>.<<ExecuteInMonitoredScopeWithRetries>b__1>d <<ExecuteInMonitoredScopeWithRetries>b__1>d;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>4__this = CS$<>8__locals1;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>1__state = -1;
						AsyncTaskMethodBuilder <>t__builder = <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder;
						<>t__builder.Start<MonitoredBlock.<>c__DisplayClass18_0<T1, T2>.<<ExecuteInMonitoredScopeWithRetries>b__1>d>(ref <<ExecuteInMonitoredScopeWithRetries>b__1>d);
						return <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder.Task;
					});
				}
				return retryPolicy2.RunWithRetriesAsync(func, false);
			}, null, activityContext, null);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001AFC4 File Offset: 0x000191C4
		protected Task ExecuteInMonitoredScopeWithRetries<T1, T2, T3>(ActivityType activityType, AsyncRetryPolicy retryPolicy, Func<T1, T2, T3, Task> asyncMethod, T1 t1, T2 t2, T3 t3, ActivityContextBase activityContext = null)
		{
			MonitoredBlock.<>c__DisplayClass19_0<T1, T2, T3> CS$<>8__locals1 = new MonitoredBlock.<>c__DisplayClass19_0<T1, T2, T3>();
			CS$<>8__locals1.retryPolicy = retryPolicy;
			CS$<>8__locals1.asyncMethod = asyncMethod;
			CS$<>8__locals1.t1 = t1;
			CS$<>8__locals1.t2 = t2;
			CS$<>8__locals1.t3 = t3;
			return AsyncUtils.ExecuteInMonitoredScope(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), delegate
			{
				AsyncRetryPolicy retryPolicy2 = CS$<>8__locals1.retryPolicy;
				Func<Task> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate
					{
						MonitoredBlock.<>c__DisplayClass19_0<T1, T2, T3>.<<ExecuteInMonitoredScopeWithRetries>b__1>d <<ExecuteInMonitoredScopeWithRetries>b__1>d;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>4__this = CS$<>8__locals1;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>1__state = -1;
						AsyncTaskMethodBuilder <>t__builder = <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder;
						<>t__builder.Start<MonitoredBlock.<>c__DisplayClass19_0<T1, T2, T3>.<<ExecuteInMonitoredScopeWithRetries>b__1>d>(ref <<ExecuteInMonitoredScopeWithRetries>b__1>d);
						return <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder.Task;
					});
				}
				return retryPolicy2.RunWithRetriesAsync(func, false);
			}, null, activityContext, null);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001B02C File Offset: 0x0001922C
		protected Task ExecuteInMonitoredScopeWithRetries<T1, T2, T3, T4>(ActivityType activityType, AsyncRetryPolicy retryPolicy, Func<T1, T2, T3, T4, Task> asyncMethod, T1 t1, T2 t2, T3 t3, T4 t4, ActivityContextBase activityContext = null)
		{
			MonitoredBlock.<>c__DisplayClass20_0<T1, T2, T3, T4> CS$<>8__locals1 = new MonitoredBlock.<>c__DisplayClass20_0<T1, T2, T3, T4>();
			CS$<>8__locals1.retryPolicy = retryPolicy;
			CS$<>8__locals1.asyncMethod = asyncMethod;
			CS$<>8__locals1.t1 = t1;
			CS$<>8__locals1.t2 = t2;
			CS$<>8__locals1.t3 = t3;
			CS$<>8__locals1.t4 = t4;
			return AsyncUtils.ExecuteInMonitoredScope(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), delegate
			{
				AsyncRetryPolicy retryPolicy2 = CS$<>8__locals1.retryPolicy;
				Func<Task> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate
					{
						MonitoredBlock.<>c__DisplayClass20_0<T1, T2, T3, T4>.<<ExecuteInMonitoredScopeWithRetries>b__1>d <<ExecuteInMonitoredScopeWithRetries>b__1>d;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>4__this = CS$<>8__locals1;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>1__state = -1;
						AsyncTaskMethodBuilder <>t__builder = <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder;
						<>t__builder.Start<MonitoredBlock.<>c__DisplayClass20_0<T1, T2, T3, T4>.<<ExecuteInMonitoredScopeWithRetries>b__1>d>(ref <<ExecuteInMonitoredScopeWithRetries>b__1>d);
						return <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder.Task;
					});
				}
				return retryPolicy2.RunWithRetriesAsync(func, false);
			}, null, activityContext, null);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001B09C File Offset: 0x0001929C
		protected Task ExecuteInMonitoredScopeWithRetries<T1, T2, T3, T4, T5>(ActivityType activityType, AsyncRetryPolicy retryPolicy, Func<T1, T2, T3, T4, T5, Task> asyncMethod, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, ActivityContextBase activityContext = null)
		{
			MonitoredBlock.<>c__DisplayClass21_0<T1, T2, T3, T4, T5> CS$<>8__locals1 = new MonitoredBlock.<>c__DisplayClass21_0<T1, T2, T3, T4, T5>();
			CS$<>8__locals1.retryPolicy = retryPolicy;
			CS$<>8__locals1.asyncMethod = asyncMethod;
			CS$<>8__locals1.t1 = t1;
			CS$<>8__locals1.t2 = t2;
			CS$<>8__locals1.t3 = t3;
			CS$<>8__locals1.t4 = t4;
			CS$<>8__locals1.t5 = t5;
			return AsyncUtils.ExecuteInMonitoredScope(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), delegate
			{
				AsyncRetryPolicy retryPolicy2 = CS$<>8__locals1.retryPolicy;
				Func<Task> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate
					{
						MonitoredBlock.<>c__DisplayClass21_0<T1, T2, T3, T4, T5>.<<ExecuteInMonitoredScopeWithRetries>b__1>d <<ExecuteInMonitoredScopeWithRetries>b__1>d;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>4__this = CS$<>8__locals1;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>1__state = -1;
						AsyncTaskMethodBuilder <>t__builder = <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder;
						<>t__builder.Start<MonitoredBlock.<>c__DisplayClass21_0<T1, T2, T3, T4, T5>.<<ExecuteInMonitoredScopeWithRetries>b__1>d>(ref <<ExecuteInMonitoredScopeWithRetries>b__1>d);
						return <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder.Task;
					});
				}
				return retryPolicy2.RunWithRetriesAsync(func, false);
			}, null, activityContext, null);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001B114 File Offset: 0x00019314
		protected Task ExecuteInMonitoredScopeWithRetries<T1, T2, T3, T4, T5, T6>(ActivityType activityType, AsyncRetryPolicy retryPolicy, Func<T1, T2, T3, T4, T5, T6, Task> asyncMethod, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, ActivityContextBase activityContext = null)
		{
			MonitoredBlock.<>c__DisplayClass22_0<T1, T2, T3, T4, T5, T6> CS$<>8__locals1 = new MonitoredBlock.<>c__DisplayClass22_0<T1, T2, T3, T4, T5, T6>();
			CS$<>8__locals1.retryPolicy = retryPolicy;
			CS$<>8__locals1.asyncMethod = asyncMethod;
			CS$<>8__locals1.t1 = t1;
			CS$<>8__locals1.t2 = t2;
			CS$<>8__locals1.t3 = t3;
			CS$<>8__locals1.t4 = t4;
			CS$<>8__locals1.t5 = t5;
			CS$<>8__locals1.t6 = t6;
			return AsyncUtils.ExecuteInMonitoredScope(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), delegate
			{
				AsyncRetryPolicy retryPolicy2 = CS$<>8__locals1.retryPolicy;
				Func<Task> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate
					{
						MonitoredBlock.<>c__DisplayClass22_0<T1, T2, T3, T4, T5, T6>.<<ExecuteInMonitoredScopeWithRetries>b__1>d <<ExecuteInMonitoredScopeWithRetries>b__1>d;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>4__this = CS$<>8__locals1;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>1__state = -1;
						AsyncTaskMethodBuilder <>t__builder = <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder;
						<>t__builder.Start<MonitoredBlock.<>c__DisplayClass22_0<T1, T2, T3, T4, T5, T6>.<<ExecuteInMonitoredScopeWithRetries>b__1>d>(ref <<ExecuteInMonitoredScopeWithRetries>b__1>d);
						return <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder.Task;
					});
				}
				return retryPolicy2.RunWithRetriesAsync(func, false);
			}, null, activityContext, null);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001B194 File Offset: 0x00019394
		protected Task<T> ExecuteInMonitoredScopeWithRetries<T>(ActivityType activityType, AsyncRetryPolicy retryPolicy, Func<Task<T>> asyncMethod, ActivityContextBase activityContext = null)
		{
			MonitoredBlock.<>c__DisplayClass23_0<T> CS$<>8__locals1 = new MonitoredBlock.<>c__DisplayClass23_0<T>();
			CS$<>8__locals1.retryPolicy = retryPolicy;
			CS$<>8__locals1.asyncMethod = asyncMethod;
			return AsyncUtils.ExecuteInMonitoredScope<T>(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), delegate
			{
				AsyncRetryPolicy retryPolicy2 = CS$<>8__locals1.retryPolicy;
				Func<Task<T>> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate
					{
						MonitoredBlock.<>c__DisplayClass23_0<T>.<<ExecuteInMonitoredScopeWithRetries>b__1>d <<ExecuteInMonitoredScopeWithRetries>b__1>d;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>4__this = CS$<>8__locals1;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>1__state = -1;
						AsyncTaskMethodBuilder<T> <>t__builder = <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder;
						<>t__builder.Start<MonitoredBlock.<>c__DisplayClass23_0<T>.<<ExecuteInMonitoredScopeWithRetries>b__1>d>(ref <<ExecuteInMonitoredScopeWithRetries>b__1>d);
						return <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder.Task;
					});
				}
				return retryPolicy2.RunWithRetriesAsync(func, false, false);
			}, null, activityContext, null);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001B1E4 File Offset: 0x000193E4
		protected Task<TReturn> ExecuteInMonitoredScopeWithRetries<TReturn, T1>(ActivityType activityType, AsyncRetryPolicy retryPolicy, Func<T1, Task<TReturn>> asyncMethod, T1 t1, ActivityContextBase activityContext = null)
		{
			MonitoredBlock.<>c__DisplayClass24_0<TReturn, T1> CS$<>8__locals1 = new MonitoredBlock.<>c__DisplayClass24_0<TReturn, T1>();
			CS$<>8__locals1.retryPolicy = retryPolicy;
			CS$<>8__locals1.asyncMethod = asyncMethod;
			CS$<>8__locals1.t1 = t1;
			return AsyncUtils.ExecuteInMonitoredScope<TReturn>(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), delegate
			{
				AsyncRetryPolicy retryPolicy2 = CS$<>8__locals1.retryPolicy;
				Func<Task<TReturn>> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate
					{
						MonitoredBlock.<>c__DisplayClass24_0<TReturn, T1>.<<ExecuteInMonitoredScopeWithRetries>b__1>d <<ExecuteInMonitoredScopeWithRetries>b__1>d;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>4__this = CS$<>8__locals1;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder = AsyncTaskMethodBuilder<TReturn>.Create();
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>1__state = -1;
						AsyncTaskMethodBuilder<TReturn> <>t__builder = <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder;
						<>t__builder.Start<MonitoredBlock.<>c__DisplayClass24_0<TReturn, T1>.<<ExecuteInMonitoredScopeWithRetries>b__1>d>(ref <<ExecuteInMonitoredScopeWithRetries>b__1>d);
						return <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder.Task;
					});
				}
				return retryPolicy2.RunWithRetriesAsync(func, false, false);
			}, null, activityContext, null);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001B23C File Offset: 0x0001943C
		protected Task<TReturn> ExecuteInMonitoredScopeWithRetries<TReturn, T1, T2>(ActivityType activityType, AsyncRetryPolicy retryPolicy, Func<T1, T2, Task<TReturn>> asyncMethod, T1 t1, T2 t2, ActivityContextBase activityContext = null)
		{
			MonitoredBlock.<>c__DisplayClass25_0<TReturn, T1, T2> CS$<>8__locals1 = new MonitoredBlock.<>c__DisplayClass25_0<TReturn, T1, T2>();
			CS$<>8__locals1.retryPolicy = retryPolicy;
			CS$<>8__locals1.asyncMethod = asyncMethod;
			CS$<>8__locals1.t1 = t1;
			CS$<>8__locals1.t2 = t2;
			return AsyncUtils.ExecuteInMonitoredScope<TReturn>(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), delegate
			{
				AsyncRetryPolicy retryPolicy2 = CS$<>8__locals1.retryPolicy;
				Func<Task<TReturn>> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate
					{
						MonitoredBlock.<>c__DisplayClass25_0<TReturn, T1, T2>.<<ExecuteInMonitoredScopeWithRetries>b__1>d <<ExecuteInMonitoredScopeWithRetries>b__1>d;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>4__this = CS$<>8__locals1;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder = AsyncTaskMethodBuilder<TReturn>.Create();
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>1__state = -1;
						AsyncTaskMethodBuilder<TReturn> <>t__builder = <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder;
						<>t__builder.Start<MonitoredBlock.<>c__DisplayClass25_0<TReturn, T1, T2>.<<ExecuteInMonitoredScopeWithRetries>b__1>d>(ref <<ExecuteInMonitoredScopeWithRetries>b__1>d);
						return <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder.Task;
					});
				}
				return retryPolicy2.RunWithRetriesAsync(func, false, false);
			}, null, activityContext, null);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001B29C File Offset: 0x0001949C
		protected Task<TReturn> ExecuteInMonitoredScopeWithRetries<TReturn, T1, T2, T3>(ActivityType activityType, AsyncRetryPolicy retryPolicy, Func<T1, T2, T3, Task<TReturn>> asyncMethod, T1 t1, T2 t2, T3 t3, ActivityContextBase activityContext = null)
		{
			MonitoredBlock.<>c__DisplayClass26_0<TReturn, T1, T2, T3> CS$<>8__locals1 = new MonitoredBlock.<>c__DisplayClass26_0<TReturn, T1, T2, T3>();
			CS$<>8__locals1.retryPolicy = retryPolicy;
			CS$<>8__locals1.asyncMethod = asyncMethod;
			CS$<>8__locals1.t1 = t1;
			CS$<>8__locals1.t2 = t2;
			CS$<>8__locals1.t3 = t3;
			return AsyncUtils.ExecuteInMonitoredScope<TReturn>(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), delegate
			{
				AsyncRetryPolicy retryPolicy2 = CS$<>8__locals1.retryPolicy;
				Func<Task<TReturn>> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate
					{
						MonitoredBlock.<>c__DisplayClass26_0<TReturn, T1, T2, T3>.<<ExecuteInMonitoredScopeWithRetries>b__1>d <<ExecuteInMonitoredScopeWithRetries>b__1>d;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>4__this = CS$<>8__locals1;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder = AsyncTaskMethodBuilder<TReturn>.Create();
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>1__state = -1;
						AsyncTaskMethodBuilder<TReturn> <>t__builder = <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder;
						<>t__builder.Start<MonitoredBlock.<>c__DisplayClass26_0<TReturn, T1, T2, T3>.<<ExecuteInMonitoredScopeWithRetries>b__1>d>(ref <<ExecuteInMonitoredScopeWithRetries>b__1>d);
						return <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder.Task;
					});
				}
				return retryPolicy2.RunWithRetriesAsync(func, false, false);
			}, null, activityContext, null);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001B304 File Offset: 0x00019504
		protected Task<TReturn> ExecuteInMonitoredScopeWithRetries<TReturn, T1, T2, T3, T4>(ActivityType activityType, AsyncRetryPolicy retryPolicy, Func<T1, T2, T3, T4, Task<TReturn>> asyncMethod, T1 t1, T2 t2, T3 t3, T4 t4, ActivityContextBase activityContext = null)
		{
			MonitoredBlock.<>c__DisplayClass27_0<TReturn, T1, T2, T3, T4> CS$<>8__locals1 = new MonitoredBlock.<>c__DisplayClass27_0<TReturn, T1, T2, T3, T4>();
			CS$<>8__locals1.retryPolicy = retryPolicy;
			CS$<>8__locals1.asyncMethod = asyncMethod;
			CS$<>8__locals1.t1 = t1;
			CS$<>8__locals1.t2 = t2;
			CS$<>8__locals1.t3 = t3;
			CS$<>8__locals1.t4 = t4;
			return AsyncUtils.ExecuteInMonitoredScope<TReturn>(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), delegate
			{
				AsyncRetryPolicy retryPolicy2 = CS$<>8__locals1.retryPolicy;
				Func<Task<TReturn>> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate
					{
						MonitoredBlock.<>c__DisplayClass27_0<TReturn, T1, T2, T3, T4>.<<ExecuteInMonitoredScopeWithRetries>b__1>d <<ExecuteInMonitoredScopeWithRetries>b__1>d;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>4__this = CS$<>8__locals1;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder = AsyncTaskMethodBuilder<TReturn>.Create();
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>1__state = -1;
						AsyncTaskMethodBuilder<TReturn> <>t__builder = <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder;
						<>t__builder.Start<MonitoredBlock.<>c__DisplayClass27_0<TReturn, T1, T2, T3, T4>.<<ExecuteInMonitoredScopeWithRetries>b__1>d>(ref <<ExecuteInMonitoredScopeWithRetries>b__1>d);
						return <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder.Task;
					});
				}
				return retryPolicy2.RunWithRetriesAsync(func, false, false);
			}, null, activityContext, null);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001B374 File Offset: 0x00019574
		protected Task<TReturn> ExecuteInMonitoredScopeWithRetries<TReturn, T1, T2, T3, T4, T5>(ActivityType activityType, AsyncRetryPolicy retryPolicy, Func<T1, T2, T3, T4, T5, Task<TReturn>> asyncMethod, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, ActivityContextBase activityContext = null)
		{
			MonitoredBlock.<>c__DisplayClass28_0<TReturn, T1, T2, T3, T4, T5> CS$<>8__locals1 = new MonitoredBlock.<>c__DisplayClass28_0<TReturn, T1, T2, T3, T4, T5>();
			CS$<>8__locals1.retryPolicy = retryPolicy;
			CS$<>8__locals1.asyncMethod = asyncMethod;
			CS$<>8__locals1.t1 = t1;
			CS$<>8__locals1.t2 = t2;
			CS$<>8__locals1.t3 = t3;
			CS$<>8__locals1.t4 = t4;
			CS$<>8__locals1.t5 = t5;
			return AsyncUtils.ExecuteInMonitoredScope<TReturn>(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), delegate
			{
				AsyncRetryPolicy retryPolicy2 = CS$<>8__locals1.retryPolicy;
				Func<Task<TReturn>> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate
					{
						MonitoredBlock.<>c__DisplayClass28_0<TReturn, T1, T2, T3, T4, T5>.<<ExecuteInMonitoredScopeWithRetries>b__1>d <<ExecuteInMonitoredScopeWithRetries>b__1>d;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>4__this = CS$<>8__locals1;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder = AsyncTaskMethodBuilder<TReturn>.Create();
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>1__state = -1;
						AsyncTaskMethodBuilder<TReturn> <>t__builder = <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder;
						<>t__builder.Start<MonitoredBlock.<>c__DisplayClass28_0<TReturn, T1, T2, T3, T4, T5>.<<ExecuteInMonitoredScopeWithRetries>b__1>d>(ref <<ExecuteInMonitoredScopeWithRetries>b__1>d);
						return <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder.Task;
					});
				}
				return retryPolicy2.RunWithRetriesAsync(func, false, false);
			}, null, activityContext, null);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001B3EC File Offset: 0x000195EC
		protected Task<TReturn> ExecuteInMonitoredScopeWithRetries<TReturn, T1, T2, T3, T4, T5, T6>(ActivityType activityType, AsyncRetryPolicy retryPolicy, Func<T1, T2, T3, T4, T5, T6, Task<TReturn>> asyncMethod, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, ActivityContextBase activityContext = null)
		{
			MonitoredBlock.<>c__DisplayClass29_0<TReturn, T1, T2, T3, T4, T5, T6> CS$<>8__locals1 = new MonitoredBlock.<>c__DisplayClass29_0<TReturn, T1, T2, T3, T4, T5, T6>();
			CS$<>8__locals1.retryPolicy = retryPolicy;
			CS$<>8__locals1.asyncMethod = asyncMethod;
			CS$<>8__locals1.t1 = t1;
			CS$<>8__locals1.t2 = t2;
			CS$<>8__locals1.t3 = t3;
			CS$<>8__locals1.t4 = t4;
			CS$<>8__locals1.t5 = t5;
			CS$<>8__locals1.t6 = t6;
			return AsyncUtils.ExecuteInMonitoredScope<TReturn>(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), delegate
			{
				AsyncRetryPolicy retryPolicy2 = CS$<>8__locals1.retryPolicy;
				Func<Task<TReturn>> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate
					{
						MonitoredBlock.<>c__DisplayClass29_0<TReturn, T1, T2, T3, T4, T5, T6>.<<ExecuteInMonitoredScopeWithRetries>b__1>d <<ExecuteInMonitoredScopeWithRetries>b__1>d;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>4__this = CS$<>8__locals1;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder = AsyncTaskMethodBuilder<TReturn>.Create();
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>1__state = -1;
						AsyncTaskMethodBuilder<TReturn> <>t__builder = <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder;
						<>t__builder.Start<MonitoredBlock.<>c__DisplayClass29_0<TReturn, T1, T2, T3, T4, T5, T6>.<<ExecuteInMonitoredScopeWithRetries>b__1>d>(ref <<ExecuteInMonitoredScopeWithRetries>b__1>d);
						return <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder.Task;
					});
				}
				return retryPolicy2.RunWithRetriesAsync(func, false, false);
			}, null, activityContext, null);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001B46C File Offset: 0x0001966C
		protected Task<TReturn> ExecuteInMonitoredScopeWithRetries<TReturn, T1, T2, T3, T4, T5, T6, T7>(ActivityType activityType, AsyncRetryPolicy retryPolicy, Func<T1, T2, T3, T4, T5, T6, T7, Task<TReturn>> asyncMethod, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, ActivityContextBase activityContext = null)
		{
			MonitoredBlock.<>c__DisplayClass30_0<TReturn, T1, T2, T3, T4, T5, T6, T7> CS$<>8__locals1 = new MonitoredBlock.<>c__DisplayClass30_0<TReturn, T1, T2, T3, T4, T5, T6, T7>();
			CS$<>8__locals1.retryPolicy = retryPolicy;
			CS$<>8__locals1.asyncMethod = asyncMethod;
			CS$<>8__locals1.t1 = t1;
			CS$<>8__locals1.t2 = t2;
			CS$<>8__locals1.t3 = t3;
			CS$<>8__locals1.t4 = t4;
			CS$<>8__locals1.t5 = t5;
			CS$<>8__locals1.t6 = t6;
			CS$<>8__locals1.t7 = t7;
			return AsyncUtils.ExecuteInMonitoredScope<TReturn>(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), delegate
			{
				AsyncRetryPolicy retryPolicy2 = CS$<>8__locals1.retryPolicy;
				Func<Task<TReturn>> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate
					{
						MonitoredBlock.<>c__DisplayClass30_0<TReturn, T1, T2, T3, T4, T5, T6, T7>.<<ExecuteInMonitoredScopeWithRetries>b__1>d <<ExecuteInMonitoredScopeWithRetries>b__1>d;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>4__this = CS$<>8__locals1;
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder = AsyncTaskMethodBuilder<TReturn>.Create();
						<<ExecuteInMonitoredScopeWithRetries>b__1>d.<>1__state = -1;
						AsyncTaskMethodBuilder<TReturn> <>t__builder = <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder;
						<>t__builder.Start<MonitoredBlock.<>c__DisplayClass30_0<TReturn, T1, T2, T3, T4, T5, T6, T7>.<<ExecuteInMonitoredScopeWithRetries>b__1>d>(ref <<ExecuteInMonitoredScopeWithRetries>b__1>d);
						return <<ExecuteInMonitoredScopeWithRetries>b__1>d.<>t__builder.Task;
					});
				}
				return retryPolicy2.RunWithRetriesAsync(func, false, false);
			}, null, activityContext, null);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001B4F3 File Offset: 0x000196F3
		protected T ExecuteInMonitoredScope<T>(ActivityType activityType, Func<T> syncMethod, Predicate<IMonitoredError> shouldActivityEndWithSuccess = null, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			return AsyncUtils.ExecuteInMonitoredScope<T>(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), syncMethod, shouldActivityEndWithSuccess, activityContext, auditContext);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001B519 File Offset: 0x00019719
		protected Task ExecuteInMonitoredScope(ActivityType activityType, SyncActivity activity, Func<Task> asyncMethod, Predicate<IMonitoredError> shouldActivityEndWithSuccess = null, IAuditContext auditContext = null)
		{
			return AsyncUtils.ExecuteInMonitoredScope(activityType, activity, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), asyncMethod, shouldActivityEndWithSuccess, null, auditContext);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001B53A File Offset: 0x0001973A
		protected T ExecuteInMonitoredScope<T>(ActivityType activityType, Func<T> syncMethod, Func<Exception, IMonitoredError> monitoredErrorProvider, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			return AsyncUtils.ExecuteInMonitoredScope<T>(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), syncMethod, monitoredErrorProvider, activityContext, auditContext);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001B560 File Offset: 0x00019760
		protected void ExecuteInMonitoredScope(ActivityType activityType, Action syncMethod, Predicate<IMonitoredError> shouldActivityEndWithSuccess = null, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			AsyncUtils.ExecuteInMonitoredScope(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), syncMethod, shouldActivityEndWithSuccess, activityContext, auditContext);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001B586 File Offset: 0x00019786
		protected void ExecuteInMonitoredScope(ActivityType activityType, Action syncMethod, Func<Exception, IMonitoredError> monitoredErrorProvider, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			AsyncUtils.ExecuteInMonitoredScope(activityType, this.ActivityFactory, this.MonitoredActivityCompletionModelFactory, base.WorkTicketManager.CreateWorkTicket(this), syncMethod, monitoredErrorProvider, activityContext, auditContext);
		}

		// Token: 0x040002C5 RID: 709
		[BlockServiceDependency]
		private readonly IMonitoredActivityCompletionModelFactory m_monitoredActivityCompletionModelFactory;

		// Token: 0x040002C6 RID: 710
		[BlockServiceDependency]
		private readonly IActivityFactory m_activityFactory;

		// Token: 0x040002C7 RID: 711
		[BlockServiceDependency]
		private readonly IEventsKitFactory m_eventsKitFactory;
	}
}
