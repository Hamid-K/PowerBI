using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000144 RID: 324
	[BlockServiceProvider(typeof(ITaskScheduler), BlockServiceProviderIdentity.Implementation)]
	public class MonitoredTaskScheduler : Block, ITaskScheduler, IRescheduleTaskExecution
	{
		// Token: 0x0600087E RID: 2174 RVA: 0x0001CEA4 File Offset: 0x0001B0A4
		public MonitoredTaskScheduler()
			: base(typeof(MonitoredTaskScheduler).Name)
		{
			this.m_locker = new object();
			this.m_tasks = new Dictionary<string, MonitoredTaskScheduler.ScheduledTask>();
			this.m_timers = new Dictionary<string, OneShotTimer>();
			this.m_readyTasks = new LinkedList<string>();
			this.m_groupsMgr = new MonitoredTaskScheduler.ExlcusionGroupsManager();
			this.m_timerFactory = new TimerFactory(base.Name, ExtendedEnvironment.CrashOnUnhandledTimerException ? TimerCreationFlags.Crash : TimerCreationFlags.NoCrash);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001CF1C File Offset: 0x0001B11C
		public IScheduledTaskHandle RegisterScheduledTask([NotNull] string policyName, [NotNull] IScheduledTask task)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(policyName, "policyName");
			ExtendedDiagnostics.EnsureArgumentNotNull<IScheduledTask>(task, "task");
			TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Retrieving CCS policy ({0}) for task {1}", new object[] { policyName, task.Name });
			CcsScheduledTaskPolicyProvider ccsScheduledTaskPolicyProvider = new CcsScheduledTaskPolicyProvider(policyName, this.m_configurationManagerFactory.GetConfigurationManager(), task.Name, this);
			return this.RegisterScheduledTask(ccsScheduledTaskPolicyProvider, task);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001CF84 File Offset: 0x0001B184
		public IScheduledTaskHandle RegisterScheduledTask([NotNull] IScheduledTaskPolicyProvider policy, [NotNull] IScheduledTask task)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IScheduledTask>(task, "task");
			ExtendedDiagnostics.EnsureArgumentNotNull<IScheduledTaskPolicyProvider>(policy, "policy");
			TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Registering task {0} with policy of type {1}", new object[]
			{
				task.Name,
				policy.GetType().Name
			});
			object locker = this.m_locker;
			IScheduledTaskHandle scheduledTaskHandle;
			lock (locker)
			{
				MonitoredTaskScheduler.ScheduledTask scheduledTask;
				if (this.m_tasks.TryGetValue(task.Name, out scheduledTask))
				{
					TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Task {0} already exists", new object[] { task.Name });
					throw new ArgumentException("Task " + task.Name + " already exists", task.Name);
				}
				WorkTicket workTicket = base.WorkTicketManager.CreateWorkTicket(this);
				using (DisposeController disposeController = new DisposeController(workTicket))
				{
					scheduledTask = new MonitoredTaskScheduler.ScheduledTask(task, policy, this, this.m_activityFactory, this.m_monitoredActivityCompletionModelFactory, this.m_ekFactory.CreateEventsKit<IMonitoredSchedulerEventsKit>(task.Name, PerformanceCounterPrefixSetting.ElementName), workTicket);
					this.m_tasks.Add(task.Name, scheduledTask);
					disposeController.PreventDispose();
				}
				scheduledTaskHandle = scheduledTask;
			}
			return scheduledTaskHandle;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001D0CC File Offset: 0x0001B2CC
		public void RescheduleNextExecutionTime([NotNull] string taskName, DateTime now)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(taskName, "taskName");
			object locker = this.m_locker;
			lock (locker)
			{
				this.ScheduleNextExecutionTime(taskName, now);
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001D11C File Offset: 0x0001B31C
		private void StartTask([NotNull] string taskName)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(taskName, "taskName");
			object locker = this.m_locker;
			lock (locker)
			{
				this.ScheduleNextExecutionTime(taskName, ExtendedDateTime.UtcNow);
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001D170 File Offset: 0x0001B370
		private void DisposeTask([NotNull] string taskName)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(taskName, "taskName");
			object locker = this.m_locker;
			lock (locker)
			{
				MonitoredTaskScheduler.ScheduledTask scheduledTask;
				if (!this.m_tasks.TryGetValue(taskName, out scheduledTask))
				{
					TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Task {0} is already disposed", new object[] { taskName });
					return;
				}
				this.m_tasks.Remove(taskName);
				if (!this.m_readyTasks.Remove(taskName))
				{
					OneShotTimer oneShotTimer;
					if (this.m_timers.TryGetValue(taskName, out oneShotTimer))
					{
						oneShotTimer.Dispose();
						this.m_timers.Remove(taskName);
					}
					return;
				}
				if (this.m_groupsMgr.IsHoldingResources(taskName))
				{
					this.m_groupsMgr.FreeExclusionGroupsForTask(taskName);
				}
			}
			this.ExecuteReadyTasks();
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001D244 File Offset: 0x0001B444
		private void OnTaskCompleted([NotNull] string taskName)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(taskName, "taskName");
			TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Task {0} completed execution", new object[] { taskName });
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_groupsMgr.FreeExclusionGroupsForTask(taskName);
				MonitoredTaskScheduler.ScheduledTask scheduledTask;
				if (!this.m_tasks.TryGetValue(taskName, out scheduledTask))
				{
					TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "Task {0} was disposed while it completed", new object[] { taskName });
					return;
				}
				if (scheduledTask.State != MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState.Running)
				{
					throw new InvalidOperationException();
				}
				scheduledTask.State = MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState.Sleeping;
				this.ScheduleNextExecutionTime(taskName, ExtendedDateTime.UtcNow);
			}
			this.ExecuteReadyTasks();
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001D304 File Offset: 0x0001B504
		private void ScheduleNextExecutionTime(string taskName, DateTime now)
		{
			TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Scheduling task {0} next execution time", new object[] { taskName });
			MonitoredTaskScheduler.ScheduledTask scheduledTask;
			if (!this.m_tasks.TryGetValue(taskName, out scheduledTask))
			{
				TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "Task {0} is already disposed or wasn't started yet.", new object[] { taskName });
				return;
			}
			MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState scheduledTaskState = scheduledTask.State;
			if (scheduledTaskState == MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState.Running || scheduledTaskState == MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState.Unstarted)
			{
				return;
			}
			if (scheduledTaskState == MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState.Ready)
			{
				this.RemovingTaskReady(taskName);
			}
			scheduledTaskState = scheduledTask.State;
			DateTime nextRunTime = scheduledTask.Policy.GetNextRunTime(now);
			TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Task {0} next execution time is {1}", new object[] { taskName, nextRunTime });
			int num = Math.Max((int)(nextRunTime - now).TotalMilliseconds, 1);
			try
			{
				OneShotTimer oneShotTimer = this.m_timerFactory.ScheduleOneShotTimer(taskName, num, delegate(object state)
				{
					this.OnTaskReady(taskName);
				}, scheduledTask);
				if (this.m_timers.ContainsKey(taskName))
				{
					this.m_timers[taskName].Dispose();
				}
				this.m_timers[taskName] = oneShotTimer;
			}
			catch (ShutdownSequenceStartedException)
			{
				scheduledTask.State = MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState.Unstarted;
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001D468 File Offset: 0x0001B668
		private void OnTaskReady(string taskName)
		{
			TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Task {0} becomes ready to run", new object[] { taskName });
			object locker = this.m_locker;
			lock (locker)
			{
				if (!this.m_tasks.ContainsKey(taskName))
				{
					TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Task {0} is already disposed (not moving to ready list)", new object[] { taskName });
					return;
				}
				Ensure.IsTrue(this.m_timers.Remove(taskName), string.Format(CultureInfo.InvariantCulture, "timer for {0} was removed before.", new object[] { taskName }));
				Ensure.IsTrue(this.m_tasks[taskName].State == MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState.Sleeping, string.Format(CultureInfo.InvariantCulture, "task {0} not sleeping", new object[] { taskName }));
				this.m_tasks[taskName].State = MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState.Ready;
				this.m_readyTasks.AddLast(taskName);
			}
			this.ExecuteReadyTasks();
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001D56C File Offset: 0x0001B76C
		private void RemovingTaskReady(string taskName)
		{
			TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Task {0} was ready to run when rescheduling", new object[] { taskName });
			object locker = this.m_locker;
			lock (locker)
			{
				if (!this.m_tasks.ContainsKey(taskName))
				{
					TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Task {0} is already disposed (shouldn't be in ready state)", new object[] { taskName });
				}
				else
				{
					this.m_tasks[taskName].State = MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState.Sleeping;
					this.m_timers[taskName].Dispose();
					this.m_timers.Remove(taskName);
					this.m_readyTasks.Remove(taskName);
				}
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001D628 File Offset: 0x0001B828
		private void ExecuteReadyTasks()
		{
			List<string> list = new List<string>();
			object locker = this.m_locker;
			lock (locker)
			{
				foreach (string text in this.m_readyTasks)
				{
					if (this.m_groupsMgr.TryAllocateExclusionGroupsForTask(this.m_tasks[text]))
					{
						list.Add(text);
					}
				}
				list.ForEach(delegate(string taskName)
				{
					this.m_readyTasks.Remove(taskName);
				});
			}
			list.ForEach(delegate(string taskName)
			{
				this.ExecuteReadyTask(taskName);
			});
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001D6EC File Offset: 0x0001B8EC
		private void ExecuteReadyTask(string taskName)
		{
			object locker = this.m_locker;
			MonitoredTaskScheduler.ScheduledTask scheduledTask;
			lock (locker)
			{
				if (!this.m_tasks.TryGetValue(taskName, out scheduledTask))
				{
					this.m_groupsMgr.FreeExclusionGroupsForTask(taskName);
					return;
				}
			}
			scheduledTask.State = MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState.Running;
			try
			{
				WorkTicket ticket = base.WorkTicketManager.CreateWorkTicket(scheduledTask.Task);
				using (DisposeController disposeController = new DisposeController(ticket))
				{
					AsyncInvoker.InvokeMethodAsynchronously(delegate
					{
						scheduledTask.Execute(ticket);
					}, null, "Execute(WorkTicket)");
					disposeController.PreventDispose();
				}
			}
			catch (ShutdownSequenceStartedException)
			{
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001D7E4 File Offset: 0x0001B9E4
		[Conditional("DEBUG")]
		private void AssertTaskExists(string taskName)
		{
			"Task " + taskName + " doesn't exists";
		}

		// Token: 0x04000327 RID: 807
		private object m_locker;

		// Token: 0x04000328 RID: 808
		private Dictionary<string, MonitoredTaskScheduler.ScheduledTask> m_tasks;

		// Token: 0x04000329 RID: 809
		private Dictionary<string, OneShotTimer> m_timers;

		// Token: 0x0400032A RID: 810
		private LinkedList<string> m_readyTasks;

		// Token: 0x0400032B RID: 811
		private MonitoredTaskScheduler.ExlcusionGroupsManager m_groupsMgr;

		// Token: 0x0400032C RID: 812
		[AutoShuttable]
		private TimerFactory m_timerFactory;

		// Token: 0x0400032D RID: 813
		[BlockServiceDependency]
		private IConfigurationManagerFactory m_configurationManagerFactory;

		// Token: 0x0400032E RID: 814
		[BlockServiceDependency]
		private IActivityFactory m_activityFactory;

		// Token: 0x0400032F RID: 815
		[BlockServiceDependency]
		private IMonitoredActivityCompletionModelFactory m_monitoredActivityCompletionModelFactory;

		// Token: 0x04000330 RID: 816
		[BlockServiceDependency]
		private IEventsKitFactory m_ekFactory;

		// Token: 0x0200060C RID: 1548
		internal class ScheduledTask : IScheduledTaskHandle, IDisposable
		{
			// Token: 0x06002C4C RID: 11340 RVA: 0x0009C984 File Offset: 0x0009AB84
			public ScheduledTask(IScheduledTask task, IScheduledTaskPolicyProvider policy, MonitoredTaskScheduler parent, IActivityFactory activityFactory, IMonitoredActivityCompletionModelFactory monitoredActivityCompletionModelFactory, IMonitoredSchedulerEventsKit eventsKit, WorkTicket workTicket)
			{
				this.m_locker = new object();
				this.Task = task;
				this.Policy = policy;
				this.m_parent = parent;
				this.m_activityFactory = activityFactory;
				this.m_monitoredActivityCompletionModelFactory = monitoredActivityCompletionModelFactory;
				this.Information = null;
				this.m_eventsKit = eventsKit;
				this.m_workTicket = workTicket;
				this.m_state = MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState.Unstarted;
			}

			// Token: 0x06002C4D RID: 11341 RVA: 0x0009C9E8 File Offset: 0x0009ABE8
			public void Start()
			{
				TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Starts task {0}", new object[] { this.Task.Name });
				object locker = this.m_locker;
				lock (locker)
				{
					if (this.m_state != MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState.Unstarted)
					{
						TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Task {0} already started", new object[] { this.Task.Name });
						throw new InvalidOperationException("Task '" + this.Task.Name + "' already started");
					}
					this.m_state = MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState.Sleeping;
				}
				this.m_parent.StartTask(this.Task.Name);
			}

			// Token: 0x06002C4E RID: 11342 RVA: 0x0009CAB0 File Offset: 0x0009ACB0
			public void Dispose()
			{
				TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Task {0} is disposing", new object[] { this.Task.Name });
				WorkTicket workTicket = null;
				object locker = this.m_locker;
				lock (locker)
				{
					if (this.m_workTicket != null)
					{
						workTicket = this.m_workTicket;
						this.m_workTicket = null;
					}
				}
				if (workTicket != null)
				{
					this.m_parent.DisposeTask(this.Task.Name);
					workTicket.Dispose();
				}
			}

			// Token: 0x1700070C RID: 1804
			// (get) Token: 0x06002C4F RID: 11343 RVA: 0x0009CB48 File Offset: 0x0009AD48
			// (set) Token: 0x06002C50 RID: 11344 RVA: 0x0009CB50 File Offset: 0x0009AD50
			public IScheduledTask Task { get; private set; }

			// Token: 0x1700070D RID: 1805
			// (get) Token: 0x06002C51 RID: 11345 RVA: 0x0009CB59 File Offset: 0x0009AD59
			// (set) Token: 0x06002C52 RID: 11346 RVA: 0x0009CB61 File Offset: 0x0009AD61
			public IScheduledTaskPolicyProvider Policy { get; private set; }

			// Token: 0x1700070E RID: 1806
			// (get) Token: 0x06002C53 RID: 11347 RVA: 0x0009CB6A File Offset: 0x0009AD6A
			// (set) Token: 0x06002C54 RID: 11348 RVA: 0x0009CB72 File Offset: 0x0009AD72
			public ScheduledTaskInformation Information { get; private set; }

			// Token: 0x1700070F RID: 1807
			// (get) Token: 0x06002C55 RID: 11349 RVA: 0x0009CB7C File Offset: 0x0009AD7C
			// (set) Token: 0x06002C56 RID: 11350 RVA: 0x0009CBC0 File Offset: 0x0009ADC0
			public MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState State
			{
				get
				{
					object locker = this.m_locker;
					MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState state;
					lock (locker)
					{
						state = this.m_state;
					}
					return state;
				}
				set
				{
					object locker = this.m_locker;
					lock (locker)
					{
						this.m_state = value;
					}
				}
			}

			// Token: 0x06002C57 RID: 11351 RVA: 0x0009CC04 File Offset: 0x0009AE04
			public void Execute(WorkTicket ticket)
			{
				UtilsContext.Current.RunWithClearContext(delegate
				{
					AsyncActivity asyncActivity = this.m_activityFactory.CreateAsyncActivity(SingletonActivityType<SchedulerActivityType>.Instance);
					MonitoredTaskScheduler.ScheduledTaskRunner scheduledTaskRunner = new MonitoredTaskScheduler.ScheduledTaskRunner(this.m_eventsKit, asyncActivity, this.m_monitoredActivityCompletionModelFactory, this.Task, this.Policy.GetTimeout(), this.Policy.GetIsCrashAllowedOnTimeout(), this.Information, ticket);
					scheduledTaskRunner.BeginExecute(new AsyncCallback(this.ExecutionCompleted), scheduledTaskRunner);
				});
			}

			// Token: 0x06002C58 RID: 11352 RVA: 0x0009CC3C File Offset: 0x0009AE3C
			private void ExecutionCompleted(IAsyncResult asyncResult)
			{
				MonitoredTaskScheduler.ScheduledTaskRunner sequencer = (MonitoredTaskScheduler.ScheduledTaskRunner)asyncResult.AsyncState;
				TopLevelHandler.Run(this, delegate
				{
					sequencer.EndExecute(asyncResult);
				});
				if (this.Information == null)
				{
					this.Information = new ScheduledTaskInformation();
				}
				this.Information.AppendLastExecutionInfo(sequencer.StartTime, sequencer.ExecutionDuration, sequencer.Result);
				if (sequencer.Result != ScheduledTaskResult.Skipped)
				{
					int num = Convert.ToInt32(sequencer.ExecutionDuration.TotalSeconds);
					if (sequencer.Result == ScheduledTaskResult.Succeeded)
					{
						this.m_eventsKit.NotifyTaskSucceeded(this.Task.Name, num);
					}
					else if (sequencer.Result == ScheduledTaskResult.Failed)
					{
						this.m_eventsKit.NotifyTaskFailed(this.Task.Name, num);
					}
				}
				else
				{
					this.m_eventsKit.NotifyTaskIngored(this.Task.Name);
				}
				this.m_parent.OnTaskCompleted(this.Task.Name);
			}

			// Token: 0x040010B3 RID: 4275
			private object m_locker;

			// Token: 0x040010B4 RID: 4276
			private MonitoredTaskScheduler m_parent;

			// Token: 0x040010B5 RID: 4277
			private IActivityFactory m_activityFactory;

			// Token: 0x040010B6 RID: 4278
			private IMonitoredActivityCompletionModelFactory m_monitoredActivityCompletionModelFactory;

			// Token: 0x040010B7 RID: 4279
			private IMonitoredSchedulerEventsKit m_eventsKit;

			// Token: 0x040010B8 RID: 4280
			private WorkTicket m_workTicket;

			// Token: 0x040010B9 RID: 4281
			private MonitoredTaskScheduler.ScheduledTask.ScheduledTaskState m_state;

			// Token: 0x0200086F RID: 2159
			internal enum ScheduledTaskState
			{
				// Token: 0x040019D6 RID: 6614
				Unstarted,
				// Token: 0x040019D7 RID: 6615
				Sleeping,
				// Token: 0x040019D8 RID: 6616
				Ready,
				// Token: 0x040019D9 RID: 6617
				Running
			}
		}

		// Token: 0x0200060D RID: 1549
		private class ExlcusionGroupsManager
		{
			// Token: 0x06002C59 RID: 11353 RVA: 0x0009CD5E File Offset: 0x0009AF5E
			public ExlcusionGroupsManager()
			{
				this.m_exclusionGroupAllocationState = new Dictionary<ExclusionGroupIdentifier, MonitoredTaskScheduler.ExlcusionGroupsManager.ExclusionGroupAllocationState>();
				this.m_allocatedExclusionGroupsByTask = new Dictionary<string, List<ExclusionGroupIdentifier>>();
			}

			// Token: 0x06002C5A RID: 11354 RVA: 0x0009CD7C File Offset: 0x0009AF7C
			public bool TryAllocateExclusionGroupsForTask(MonitoredTaskScheduler.ScheduledTask scheduledTask)
			{
				string name = scheduledTask.Task.Name;
				List<ExclusionGroupIdentifier> list;
				if (!this.m_allocatedExclusionGroupsByTask.TryGetValue(name, out list))
				{
					list = new List<ExclusionGroupIdentifier>(scheduledTask.Policy.GetExclusionGroups());
					this.m_allocatedExclusionGroupsByTask.Add(name, list);
				}
				List<MonitoredTaskScheduler.ExlcusionGroupsManager.ExclusionGroupAllocationState> list2 = new List<MonitoredTaskScheduler.ExlcusionGroupsManager.ExclusionGroupAllocationState>();
				bool flag = true;
				foreach (ExclusionGroupIdentifier exclusionGroupIdentifier in list)
				{
					MonitoredTaskScheduler.ExlcusionGroupsManager.ExclusionGroupAllocationState exclusionGroupAllocationState;
					if (!this.m_exclusionGroupAllocationState.TryGetValue(exclusionGroupIdentifier, out exclusionGroupAllocationState))
					{
						exclusionGroupAllocationState = new MonitoredTaskScheduler.ExlcusionGroupsManager.ExclusionGroupAllocationState();
						this.m_exclusionGroupAllocationState.Add(exclusionGroupIdentifier, exclusionGroupAllocationState);
					}
					if (exclusionGroupAllocationState.TryAllocateFor(name, list))
					{
						list2.Add(exclusionGroupAllocationState);
					}
					else
					{
						flag = false;
					}
				}
				return flag;
			}

			// Token: 0x06002C5B RID: 11355 RVA: 0x0009CE48 File Offset: 0x0009B048
			public void FreeExclusionGroupsForTask([NotNull] string taskName)
			{
				ExtendedDiagnostics.EnsureStringNotNullOrEmpty(taskName, "taskName");
				foreach (MonitoredTaskScheduler.ExlcusionGroupsManager.ExclusionGroupAllocationState exclusionGroupAllocationState in this.m_exclusionGroupAllocationState.Values.Where((MonitoredTaskScheduler.ExlcusionGroupsManager.ExclusionGroupAllocationState eg) => eg.TakenByTask != null && taskName.Equals(eg.TakenByTask, StringComparison.CurrentCulture)))
				{
					exclusionGroupAllocationState.Free();
				}
				this.m_allocatedExclusionGroupsByTask.Remove(taskName);
			}

			// Token: 0x06002C5C RID: 11356 RVA: 0x0009CED8 File Offset: 0x0009B0D8
			public bool IsHoldingResources(string taskName)
			{
				return this.m_allocatedExclusionGroupsByTask.ContainsKey(taskName);
			}

			// Token: 0x040010BD RID: 4285
			private Dictionary<ExclusionGroupIdentifier, MonitoredTaskScheduler.ExlcusionGroupsManager.ExclusionGroupAllocationState> m_exclusionGroupAllocationState;

			// Token: 0x040010BE RID: 4286
			private Dictionary<string, List<ExclusionGroupIdentifier>> m_allocatedExclusionGroupsByTask;

			// Token: 0x02000872 RID: 2162
			private class ExclusionGroupAllocationState
			{
				// Token: 0x1700078A RID: 1930
				// (get) Token: 0x06003391 RID: 13201 RVA: 0x000ACBE4 File Offset: 0x000AADE4
				// (set) Token: 0x06003392 RID: 13202 RVA: 0x000ACBEC File Offset: 0x000AADEC
				public string TakenByTask { get; private set; }

				// Token: 0x06003393 RID: 13203 RVA: 0x000ACBF5 File Offset: 0x000AADF5
				public bool TryAllocateFor(string taskName, List<ExclusionGroupIdentifier> exclusionGroups)
				{
					if (this.TakenByTask == null)
					{
						this.TakenByTask = taskName;
						return true;
					}
					return this.TakenByTask.Equals(taskName);
				}

				// Token: 0x06003394 RID: 13204 RVA: 0x000ACC19 File Offset: 0x000AAE19
				public void Free()
				{
					this.TakenByTask = null;
				}
			}
		}

		// Token: 0x0200060E RID: 1550
		private class ScheduledTaskRunner : MonitoredActivitySequencer
		{
			// Token: 0x06002C5D RID: 11357 RVA: 0x0009CEE8 File Offset: 0x0009B0E8
			public ScheduledTaskRunner(IMonitoredSchedulerEventsKit eventsKit, AsyncActivity activity, IMonitoredActivityCompletionModelFactory factory, IScheduledTask task, TimeSpan timeout, bool isCrashAllowedOnTimeout, ScheduledTaskInformation context, WorkTicket wt)
				: base(activity, factory, true, null)
			{
				this.m_eventsKit = eventsKit;
				this.m_task = task;
				this.m_context = context;
				this.m_workTicket = wt;
				this.m_timeout = timeout;
				this.m_isCrashAllowedOnTimeout = isCrashAllowedOnTimeout;
				this.Result = ScheduledTaskResult.Failed;
			}

			// Token: 0x06002C5E RID: 11358 RVA: 0x0009CF35 File Offset: 0x0009B135
			protected override IEnumerable<IFlowStep> Run()
			{
				this.m_eventsKit.NotifyTaskStarted(this.m_task.Name);
				this.StartTime = ExtendedDateTime.UtcNow;
				SequencerWithTimeout sequencerWithTimeout = new SequencerWithTimeout(new MonitoredTaskScheduler.ScheduledTaskRunner.Executor(this), this.m_timeout);
				yield return base.RunAsyncStep("ScheduledTaskRunner {0} (timeout: {1})".FormatWithInvariantCulture(new object[]
				{
					this.m_task.Name,
					this.m_timeout
				}), delegate(string step, Exception ex)
				{
					if (ex is SequencerTimeoutException)
					{
						if (!this.m_isCrashAllowedOnTimeout)
						{
							TraceSourceBase<MonitoredUtilsTrace>.Tracer.TraceWarning("Scheduled task: '{0}'; exceeded timeout {1}; swallowing '{2}' and sending request to abort task", new object[]
							{
								this.m_task.Name,
								this.m_timeout,
								ex.GetType().ToString()
							});
							try
							{
								this.m_task.Abort();
								return null;
							}
							catch (Exception ex2)
							{
								TraceSourceBase<MonitoredUtilsTrace>.Tracer.TraceError("Failed to abort scheduled task '{0}' due to exception: {1}", new object[]
								{
									this.m_task.Name,
									ex2.Message
								});
								if (ex2.IsFatal())
								{
									throw;
								}
							}
						}
						ExtendedEnvironment.FailSlow(this, "Scheduled task: '{0}; exceeded timeout {1}; failing slow".FormatWithInvariantCulture(new object[]
						{
							this.m_task.Name,
							this.m_timeout
						}));
					}
					return ex;
				}, new Sequencer.AsyncBeginFunction(sequencerWithTimeout.BeginExecute), new Sequencer.AsyncEndFunction(sequencerWithTimeout.EndExecute));
				yield break;
			}

			// Token: 0x06002C5F RID: 11359 RVA: 0x0009CF48 File Offset: 0x0009B148
			public override void EndExecute(IAsyncResult asyncResult)
			{
				try
				{
					base.EndExecute(asyncResult);
				}
				finally
				{
					this.m_workTicket.Dispose();
					this.m_workTicket = null;
				}
			}

			// Token: 0x17000710 RID: 1808
			// (get) Token: 0x06002C60 RID: 11360 RVA: 0x0009CF84 File Offset: 0x0009B184
			// (set) Token: 0x06002C61 RID: 11361 RVA: 0x0009CF8C File Offset: 0x0009B18C
			public ScheduledTaskResult Result { get; private set; }

			// Token: 0x17000711 RID: 1809
			// (get) Token: 0x06002C62 RID: 11362 RVA: 0x0009CF95 File Offset: 0x0009B195
			// (set) Token: 0x06002C63 RID: 11363 RVA: 0x0009CF9D File Offset: 0x0009B19D
			public DateTime StartTime { get; private set; }

			// Token: 0x17000712 RID: 1810
			// (get) Token: 0x06002C64 RID: 11364 RVA: 0x0009CFA8 File Offset: 0x0009B1A8
			public TimeSpan ExecutionDuration
			{
				get
				{
					return DateTime.UtcNow.Subtract(this.StartTime);
				}
			}

			// Token: 0x040010C1 RID: 4289
			private IMonitoredSchedulerEventsKit m_eventsKit;

			// Token: 0x040010C2 RID: 4290
			private IScheduledTask m_task;

			// Token: 0x040010C3 RID: 4291
			private ScheduledTaskInformation m_context;

			// Token: 0x040010C4 RID: 4292
			private WorkTicket m_workTicket;

			// Token: 0x040010C5 RID: 4293
			private readonly TimeSpan m_timeout;

			// Token: 0x040010C6 RID: 4294
			private readonly bool m_isCrashAllowedOnTimeout;

			// Token: 0x02000874 RID: 2164
			private sealed class Executor : Sequencer
			{
				// Token: 0x06003398 RID: 13208 RVA: 0x000ACC40 File Offset: 0x000AAE40
				public Executor(MonitoredTaskScheduler.ScheduledTaskRunner owner)
				{
					this.m_owner = owner;
				}

				// Token: 0x06003399 RID: 13209 RVA: 0x000ACC4F File Offset: 0x000AAE4F
				protected override IEnumerable<IFlowStep> Run()
				{
					yield return base.RunAsyncStep<ScheduledTaskInformation>("ScheduledTaskRunner:" + this.m_owner.m_task.Name, new Sequencer.AsyncBeginFunction<ScheduledTaskInformation>(this.m_owner.m_task.BeginExecute), delegate(IAsyncResult ar)
					{
						this.m_owner.Result = this.m_owner.m_task.EndExecute(ar);
					}, this.m_owner.m_context);
					yield break;
				}

				// Token: 0x040019E0 RID: 6624
				private readonly MonitoredTaskScheduler.ScheduledTaskRunner m_owner;
			}
		}
	}
}
