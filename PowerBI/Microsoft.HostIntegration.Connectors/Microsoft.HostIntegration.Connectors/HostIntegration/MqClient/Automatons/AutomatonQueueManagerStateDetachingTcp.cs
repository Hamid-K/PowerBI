using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A9D RID: 2717
	internal class AutomatonQueueManagerStateDetachingTcp : StateAsCodeDriver
	{
		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x0600550D RID: 21773 RVA: 0x0015922B File Offset: 0x0015742B
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600550E RID: 21774 RVA: 0x0015923E File Offset: 0x0015743E
		internal AutomatonQueueManagerStateDetachingTcp(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600550F RID: 21775 RVA: 0x00159260 File Offset: 0x00157460
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
					if (automatonQueueManagerEvent <= AutomatonQueueManagerEvent.Detached)
					{
						if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.TcpDisconnected)
						{
							if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.Detached)
							{
								goto IL_0050;
							}
							this.ActionDisconnectFromTcp();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.Disconnected;
							num = 0;
						}
						else
						{
							num = 2;
						}
					}
					else if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.Disconnected)
					{
						if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.QueueDisconnected)
						{
							goto IL_0050;
						}
						num = 3;
					}
					else
					{
						this.ActionSetWaitDisconnectEvent();
						automatonQueueManagerState = AutomatonQueueManagerState.UnConnected;
						num = 1;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateDetachingTcp.traceLines[num]);
						continue;
					}
					continue;
					IL_0050:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: DetachingTcp, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x00159384 File Offset: 0x00157584
		public void ActionDisconnectFromTcp()
		{
			this.automaton.DisconnectFromTcp();
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x00159391 File Offset: 0x00157591
		public void ActionSetWaitDisconnectEvent()
		{
			this.context.DisconnectedEvent.Set();
		}

		// Token: 0x0400439C RID: 17308
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.DetachingTcp;

		// Token: 0x0400439D RID: 17309
		private AutomatonQueueManager automaton;

		// Token: 0x0400439E RID: 17310
		private AutomatonQueueManagerContext context;

		// Token: 0x0400439F RID: 17311
		private static string[] traceLines = new string[] { "State: DetachingTcp, Evt: Detached, Act: DisconnectFromTcp, Evt: Disconnected", "State: DetachingTcp, Evt: Disconnected, Act: SetWaitDisconnectEvent, State: UnConnected, Stop", "State: DetachingTcp, Evt: TcpDisconnected, Stop", "State: DetachingTcp, Evt: QueueDisconnected, Stop" };
	}
}
