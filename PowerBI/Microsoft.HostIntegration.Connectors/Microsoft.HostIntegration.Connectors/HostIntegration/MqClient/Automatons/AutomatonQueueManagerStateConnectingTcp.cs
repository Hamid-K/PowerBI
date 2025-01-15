using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A9A RID: 2714
	internal class AutomatonQueueManagerStateConnectingTcp : StateAsCodeDriver
	{
		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x060054E7 RID: 21735 RVA: 0x0015844B File Offset: 0x0015664B
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060054E8 RID: 21736 RVA: 0x0015845E File Offset: 0x0015665E
		internal AutomatonQueueManagerStateConnectingTcp(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060054E9 RID: 21737 RVA: 0x0015847C File Offset: 0x0015667C
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
					if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.Attached)
					{
						if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.AttachFailed)
						{
							throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
						}
						this.ActionConnectFailedSetEvent();
						automatonQueueManagerState = AutomatonQueueManagerState.UnConnected;
						num = 1;
					}
					else
					{
						automatonQueueManagerState = AutomatonQueueManagerState.Handshake;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.Start;
						num = 0;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateConnectingTcp.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: ConnectingTcp, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x060054EA RID: 21738 RVA: 0x0015857C File Offset: 0x0015677C
		public void ActionConnectFailedSetEvent()
		{
			this.context.ConnectReturnCode = ReturnCode.QmConnectTcpConnectFailed;
			this.context.ConnectedEvent.Set();
		}

		// Token: 0x04004390 RID: 17296
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.ConnectingTcp;

		// Token: 0x04004391 RID: 17297
		private AutomatonQueueManager automaton;

		// Token: 0x04004392 RID: 17298
		private AutomatonQueueManagerContext context;

		// Token: 0x04004393 RID: 17299
		private static string[] traceLines = new string[] { "State: ConnectingTcp, Evt: Attached, State: Handshake, Evt: Start", "State: ConnectingTcp, Evt: AttachFailed, Act: ConnectFailedSetEvent, State: UnConnected, Stop" };
	}
}
