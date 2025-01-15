using System;
using System.Globalization;
using System.Net.Sockets;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AD6 RID: 2774
	internal class AutomatonTcpStateUnConnected : StateAsCodeDriver
	{
		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x06005860 RID: 22624 RVA: 0x0016C410 File Offset: 0x0016A610
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005861 RID: 22625 RVA: 0x0016C423 File Offset: 0x0016A623
		internal AutomatonTcpStateUnConnected(AutomatonTcp driver, AutomatonTcpContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005862 RID: 22626 RVA: 0x0016C43C File Offset: 0x0016A63C
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
					switch (automatonTcpEvent)
					{
					case AutomatonTcpEvent.Connect:
						this.ActionConnectSocket();
						if (this.PostConditionSucceededSsl())
						{
							automatonTcpState = AutomatonTcpState.DoSslHandshake;
							automatonTcpEvent2 = AutomatonTcpEvent.StartHandshake;
							num = 0;
						}
						else if (this.PostConditionSucceeded())
						{
							automatonTcpEvent2 = AutomatonTcpEvent.Connected;
							num = 1;
						}
						else
						{
							automatonTcpState = AutomatonTcpState.FailedConnect;
							automatonTcpEvent2 = AutomatonTcpEvent.Failed;
							num = 2;
						}
						break;
					case AutomatonTcpEvent.Connected:
						this.ActionSetWaitConnectEvent();
						automatonTcpState = AutomatonTcpState.DataTransfer;
						automatonTcpEvent2 = AutomatonTcpEvent.StartTransfer;
						num = 3;
						break;
					case AutomatonTcpEvent.DataReceived:
						num = 4;
						break;
					default:
						throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
					}
					automatonTcpEvent = automatonTcpEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonTcpStateUnConnected.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: UnConnected, Event: " + automatonTcpEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonTcpEvent;
			return (int)automatonTcpState;
		}

		// Token: 0x06005863 RID: 22627 RVA: 0x0016C574 File Offset: 0x0016A774
		public void ActionConnectSocket()
		{
			try
			{
				this.context.TcpClient = new TcpClient(this.context.ConnectionParameters.Server, this.context.ConnectionParameters.Port);
			}
			catch (SocketException ex)
			{
				this.context.TcpConnectException = ex;
			}
		}

		// Token: 0x06005864 RID: 22628 RVA: 0x0016C5D4 File Offset: 0x0016A7D4
		public void ActionSetWaitConnectEvent()
		{
			this.context.ConnectedEvent.Set();
			this.context.TcpClient.NoDelay = true;
		}

		// Token: 0x06005865 RID: 22629 RVA: 0x0016C5F8 File Offset: 0x0016A7F8
		public bool PostConditionSucceededSsl()
		{
			return this.context.TcpConnectException == null && this.context.ConnectionParameters.UseSsl;
		}

		// Token: 0x06005866 RID: 22630 RVA: 0x0016C61C File Offset: 0x0016A81C
		public bool PostConditionSucceeded()
		{
			return this.context.TcpConnectException == null;
		}

		// Token: 0x040044AC RID: 17580
		private AutomatonTcpState stateNumber;

		// Token: 0x040044AD RID: 17581
		private AutomatonTcp automaton;

		// Token: 0x040044AE RID: 17582
		private AutomatonTcpContext context;

		// Token: 0x040044AF RID: 17583
		private static string[] traceLines = new string[] { "State: UnConnected, Evt: Connect, Act: ConnectSocket, Post: SucceededSsl, State: DoSslHandshake, Evt: StartHandshake", "State: UnConnected, Evt: Connect, Act: ConnectSocket, Post: Succeeded, Evt: Connected", "State: UnConnected, Evt: Connect, Act: ConnectSocket, State: FailedConnect, Evt: Failed", "State: UnConnected, Evt: Connected, Act: SetWaitConnectEvent, State: DataTransfer, Evt: StartTransfer", "State: UnConnected, Evt: DataReceived, Stop" };
	}
}
