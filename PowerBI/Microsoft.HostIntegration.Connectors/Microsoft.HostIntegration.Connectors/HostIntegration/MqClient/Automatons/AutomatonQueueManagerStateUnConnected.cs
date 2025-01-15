using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AA4 RID: 2724
	internal class AutomatonQueueManagerStateUnConnected : StateAsCodeDriver
	{
		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x06005573 RID: 21875 RVA: 0x0015C0FB File Offset: 0x0015A2FB
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005574 RID: 21876 RVA: 0x0015C10E File Offset: 0x0015A30E
		internal AutomatonQueueManagerStateUnConnected(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005575 RID: 21877 RVA: 0x0015C128 File Offset: 0x0015A328
		public override int Process(ref int eventToProcess)
		{
			AutomatonQueueManagerEvent automatonQueueManagerEvent = (AutomatonQueueManagerEvent)eventToProcess;
			AutomatonQueueManagerState automatonQueueManagerState = this.stateNumber;
			try
			{
				while (automatonQueueManagerState == this.stateNumber && automatonQueueManagerEvent != AutomatonQueueManagerEvent.Stop)
				{
					AutomatonQueueManagerEvent automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.Stop;
					int num;
					if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.Connect)
					{
						if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.TcpDisconnected)
						{
							if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.Disconnect)
							{
								throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
							}
							num = 3;
						}
						else
						{
							num = 2;
						}
					}
					else if (this.PreConditionFirstConversation())
					{
						this.ActionConnectTcp();
						automatonQueueManagerState = AutomatonQueueManagerState.ConnectingTcp;
						num = 0;
					}
					else
					{
						this.ActionConnectTcp();
						automatonQueueManagerState = AutomatonQueueManagerState.AttachingTcp;
						num = 1;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateUnConnected.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: UnConnected, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x06005576 RID: 21878 RVA: 0x0015C244 File Offset: 0x0015A444
		public bool PreConditionFirstConversation()
		{
			return this.context.DeterminantForTcp == 1;
		}

		// Token: 0x06005577 RID: 21879 RVA: 0x0015C254 File Offset: 0x0015A454
		public void ActionConnectTcp()
		{
			this.automaton.ConnectToTcp();
			DeterminantMessage determinantMessage = new DeterminantMessage();
			determinantMessage.QueueManagerDeterminant = this.context.DeterminantForTcp;
			this.automaton.SendToTcp(100, determinantMessage);
		}

		// Token: 0x040043B8 RID: 17336
		private AutomatonQueueManagerState stateNumber;

		// Token: 0x040043B9 RID: 17337
		private AutomatonQueueManager automaton;

		// Token: 0x040043BA RID: 17338
		private AutomatonQueueManagerContext context;

		// Token: 0x040043BB RID: 17339
		private static string[] traceLines = new string[] { "State: UnConnected, Evt: Connect, Pre: FirstConversation, Act: ConnectTcp, State: ConnectingTcp, Stop", "State: UnConnected, Evt: Connect, Act: ConnectTcp, State: AttachingTcp, Stop", "State: UnConnected, Evt: TcpDisconnected, Stop", "State: UnConnected, Evt: Disconnect, Stop" };
	}
}
