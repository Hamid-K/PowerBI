using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Threading;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200021C RID: 540
	internal sealed class SchedulePollWorker : PollWorker
	{
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x000444BE File Offset: 0x000426BE
		public override bool Poll
		{
			get
			{
				return this.m_lastPoll == DateTime.MinValue;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x000444D5 File Offset: 0x000426D5
		public override InstrumentedSqlCommand PollCommand
		{
			get
			{
				InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("select count(*) from Schedule"));
				instrumentedSqlCommand.CommandType = CommandType.Text;
				return instrumentedSqlCommand;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x000444ED File Offset: 0x000426ED
		protected override string PollingTraceName
		{
			get
			{
				return "SchedulePolling";
			}
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x000444F4 File Offset: 0x000426F4
		public override void ProcessData(IDataReader reader)
		{
			this.m_lastPoll = DateTime.Now;
			if (reader.Read())
			{
				if (reader.GetInt32(0) == 0)
				{
					return;
				}
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.CheckScheduleConsistancy), new object());
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x0004452A File Offset: 0x0004272A
		public override bool IsStillWorking
		{
			get
			{
				return this.m_working;
			}
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00044534 File Offset: 0x00042734
		private void CheckScheduleConsistancy(object state)
		{
			DBPoll.PollItemStart();
			try
			{
				this.m_working = true;
				base.ExecuteStoredProcedure("ClearScheduleConsistancyFlags");
				while (base.ContinueWorking)
				{
					SchedulingDBInterface schedulingDBInterface = new SchedulingDBInterface();
					schedulingDBInterface.ConnectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
					schedulingDBInterface.ConnectionManager.WillDisconnectStorage();
					ArrayList arrayList = null;
					try
					{
						arrayList = schedulingDBInterface.ListTasksForMaintenance();
						schedulingDBInterface.Commit();
					}
					catch
					{
						schedulingDBInterface.AbortTransaction();
						throw;
					}
					finally
					{
						schedulingDBInterface.DisconnectStorage();
					}
					if (arrayList.Count == 0)
					{
						break;
					}
					SqlAgentScheduler sqlAgentScheduler = new SqlAgentScheduler();
					sqlAgentScheduler.ConnectionManager = schedulingDBInterface.ConnectionManager;
					foreach (object obj in arrayList)
					{
						Task task = (Task)obj;
						if (!base.ContinueWorking)
						{
							break;
						}
						try
						{
							if (task.ScheduleState != TaskState.Paused)
							{
								Task task2 = null;
								try
								{
									task2 = sqlAgentScheduler.GetTask(task.ID);
								}
								catch (InvalidSqlAgentJobException)
								{
									if (this.m_tracer.TraceError)
									{
										this.m_tracer.Trace(TraceLevel.Error, "Invalid SQL Agent Job {0} will be deleted and recreated", new object[] { task.ID });
									}
									sqlAgentScheduler.DeleteTask(task.ID);
								}
								if (task2 == null)
								{
									if (this.m_tracer.TraceWarning)
									{
										this.m_tracer.Trace("Schedule ({0}) not present, being added.", new object[] { task.ID });
									}
									sqlAgentScheduler.UpdateTask(task);
									task2 = sqlAgentScheduler.GetTask(task.ID);
									task2.Path = task.Path;
									RSEventLog.Current.WriteWarning(Event.ScheduleUpdated, Array.Empty<object>());
								}
								else if (!task2.EqualSqlSchedule(task))
								{
									if (this.m_tracer.TraceWarning)
									{
										this.m_tracer.Trace("Schedule ({0}) altered, being updated.", new object[] { task.ID });
									}
									sqlAgentScheduler.UpdateTask(task);
									task2 = sqlAgentScheduler.GetTask(task.ID);
									task2.Path = task.Path;
									RSEventLog.Current.WriteWarning(Event.ScheduleUpdated, Array.Empty<object>());
								}
								sqlAgentScheduler.ConnectionManager.CommitTransaction();
							}
						}
						catch (Exception)
						{
							sqlAgentScheduler.ConnectionManager.AbortTransaction();
							throw;
						}
						finally
						{
							sqlAgentScheduler.ConnectionManager.DisconnectStorage();
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (this.m_tracer.TraceError)
				{
					this.m_tracer.Trace("Unhandled exception caught in Scheduling maintenance thread: {0}", new object[] { ex.ToString() });
				}
			}
			finally
			{
				this.m_working = false;
				DBPoll.PollItemEnd();
			}
		}

		// Token: 0x040006DD RID: 1757
		private RSTrace m_tracer = RSTrace.ScheduleTracer;

		// Token: 0x040006DE RID: 1758
		private DateTime m_lastPoll = DateTime.MinValue;

		// Token: 0x040006DF RID: 1759
		private bool m_working;
	}
}
