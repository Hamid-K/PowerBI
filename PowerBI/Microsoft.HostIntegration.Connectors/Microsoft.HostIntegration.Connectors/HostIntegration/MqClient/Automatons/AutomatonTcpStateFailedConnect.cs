using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AD4 RID: 2772
	internal class AutomatonTcpStateFailedConnect : StateAsCodeDriver
	{
		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x06005850 RID: 22608 RVA: 0x0016BF6D File Offset: 0x0016A16D
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005851 RID: 22609 RVA: 0x0016BF80 File Offset: 0x0016A180
		internal AutomatonTcpStateFailedConnect(AutomatonTcp driver, AutomatonTcpContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005852 RID: 22610 RVA: 0x0016BFA0 File Offset: 0x0016A1A0
		public override int Process(ref int eventToProcess)
		{
			AutomatonTcpEvent automatonTcpEvent = (AutomatonTcpEvent)eventToProcess;
			AutomatonTcpState automatonTcpState = this.stateNumber;
			try
			{
				while (automatonTcpState == this.stateNumber && automatonTcpEvent != AutomatonTcpEvent.Stop)
				{
					AutomatonTcpEvent automatonTcpEvent2 = AutomatonTcpEvent.Stop;
					int num;
					if (automatonTcpEvent != AutomatonTcpEvent.Failed)
					{
						if (automatonTcpEvent != AutomatonTcpEvent.SslFailed)
						{
							throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
						}
						this.ActionConnectFailedSslSetEvent();
						automatonTcpState = AutomatonTcpState.UnConnected;
						num = 1;
					}
					else
					{
						this.ActionConnectFailedSetEvent();
						automatonTcpState = AutomatonTcpState.UnConnected;
						num = 0;
					}
					automatonTcpEvent = automatonTcpEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonTcpStateFailedConnect.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: FailedConnect, Event: " + automatonTcpEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonTcpEvent;
			return (int)automatonTcpState;
		}

		// Token: 0x06005853 RID: 22611 RVA: 0x0016C0A4 File Offset: 0x0016A2A4
		public void ActionConnectFailedSetEvent()
		{
			this.context.ConnectReturnCode = ReturnCode.TcpConnectFailed;
			this.context.ConnectedEvent.Set();
		}

		// Token: 0x06005854 RID: 22612 RVA: 0x0016C0C7 File Offset: 0x0016A2C7
		public void ActionConnectFailedSslSetEvent()
		{
			this.context.ConnectReturnCode = ReturnCode.TcpSslConnectFailed;
			this.context.ConnectedEvent.Set();
		}

		// Token: 0x040044A4 RID: 17572
		private AutomatonTcpState stateNumber = AutomatonTcpState.FailedConnect;

		// Token: 0x040044A5 RID: 17573
		private AutomatonTcp automaton;

		// Token: 0x040044A6 RID: 17574
		private AutomatonTcpContext context;

		// Token: 0x040044A7 RID: 17575
		private static string[] traceLines = new string[] { "State: FailedConnect, Evt: Failed, Act: ConnectFailedSetEvent, State: UnConnected, Stop", "State: FailedConnect, Evt: SslFailed, Act: ConnectFailedSslSetEvent, State: UnConnected, Stop" };
	}
}
