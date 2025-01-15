using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A97 RID: 2711
	internal class AutomatonQueueManagerStateAttachingTcp : StateAsCodeDriver
	{
		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x060054D2 RID: 21714 RVA: 0x00157D0F File Offset: 0x00155F0F
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060054D3 RID: 21715 RVA: 0x00157D22 File Offset: 0x00155F22
		internal AutomatonQueueManagerStateAttachingTcp(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060054D4 RID: 21716 RVA: 0x00157D40 File Offset: 0x00155F40
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
					switch (automatonQueueManagerEvent)
					{
					case AutomatonQueueManagerEvent.TcpDisconnected:
						num = 2;
						break;
					case AutomatonQueueManagerEvent.Attached:
						automatonQueueManagerState = AutomatonQueueManagerState.SocketAction;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.SendSA;
						num = 0;
						break;
					case AutomatonQueueManagerEvent.AttachFailed:
						automatonQueueManagerState = AutomatonQueueManagerState.HandshakeFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.Detached;
						num = 1;
						break;
					default:
						throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateAttachingTcp.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: AttachingTcp, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x04004384 RID: 17284
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.AttachingTcp;

		// Token: 0x04004385 RID: 17285
		private AutomatonQueueManager automaton;

		// Token: 0x04004386 RID: 17286
		private AutomatonQueueManagerContext context;

		// Token: 0x04004387 RID: 17287
		private static string[] traceLines = new string[] { "State: AttachingTcp, Evt: Attached, State: SocketAction, Evt: SendSA", "State: AttachingTcp, Evt: AttachFailed, State: HandshakeFailed, Evt: Detached", "State: AttachingTcp, Evt: TcpDisconnected, Stop" };
	}
}
