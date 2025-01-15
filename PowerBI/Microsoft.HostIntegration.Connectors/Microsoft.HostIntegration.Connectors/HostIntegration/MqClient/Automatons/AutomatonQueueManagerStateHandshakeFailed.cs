using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AA0 RID: 2720
	internal class AutomatonQueueManagerStateHandshakeFailed : StateAsCodeDriver
	{
		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x0600552B RID: 21803 RVA: 0x0015A165 File Offset: 0x00158365
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600552C RID: 21804 RVA: 0x0015A178 File Offset: 0x00158378
		internal AutomatonQueueManagerStateHandshakeFailed(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600552D RID: 21805 RVA: 0x0015A198 File Offset: 0x00158398
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
					if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.TcpDisconnected)
					{
						switch (automatonQueueManagerEvent)
						{
						case AutomatonQueueManagerEvent.Rejected:
							this.ActionSetRejected();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.DetachTcp;
							num = 0;
							break;
						case AutomatonQueueManagerEvent.TcpFailed:
							this.ActionSetDisconnected();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.DetachTcp;
							num = 1;
							break;
						case AutomatonQueueManagerEvent.StatusData:
							this.ActionSetStatusDataRc();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.DetachTcp;
							num = 3;
							break;
						case AutomatonQueueManagerEvent.QmConnError:
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.DetachTcp;
							num = 4;
							break;
						case AutomatonQueueManagerEvent.XaOpenError:
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.DetachTcp;
							num = 5;
							break;
						case AutomatonQueueManagerEvent.DetachTcp:
							this.ActionSendDetach();
							num = 6;
							break;
						case AutomatonQueueManagerEvent.Detached:
							this.ActionDisconnectFromTcp();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.Disconnected;
							num = 7;
							break;
						case AutomatonQueueManagerEvent.Disconnected:
							this.ActionSetWaitConnectEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.UnConnected;
							num = 8;
							break;
						default:
							throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
						}
					}
					else
					{
						num = 2;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateHandshakeFailed.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: HandshakeFailed, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x0015A308 File Offset: 0x00158508
		public void ActionSetRejected()
		{
			switch (this.context.RejectedReason)
			{
			case RejectedReason.Encoding:
				this.context.ConnectReturnCode = ReturnCode.QmConnectInitialDataRejectedEncoding;
				return;
			case RejectedReason.Fap:
				this.context.ConnectReturnCode = ReturnCode.QmConnectInitialDataRejectedFap;
				return;
			case RejectedReason.TransmissionSize:
				this.context.ConnectReturnCode = ReturnCode.QmConnectInitialDataRejectedTransmissionSize;
				return;
			case RejectedReason.Conversations0:
				this.context.ConnectReturnCode = ReturnCode.QmConnectInitialDataRejectedConversations0;
				return;
			case RejectedReason.ErrorFlag2:
				this.context.ConnectReturnCode = ReturnCode.QmConnectInitialDataRejectedErrorFlag2;
				return;
			case RejectedReason.Ccsid:
				this.context.ConnectReturnCode = ReturnCode.QmConnectInitialDataRejectedCcsid;
				return;
			default:
				throw new InvalidOperationException("invalid rejection reason code");
			}
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x0015A3B4 File Offset: 0x001585B4
		public void ActionSetStatusDataRc()
		{
			StatusDataType statusDataType = this.context.StatusDataType;
			if (statusDataType != StatusDataType.NoChannel)
			{
				switch (statusDataType)
				{
				case StatusDataType.QueueManagerUnavailable:
					this.context.ConnectReturnCode = (ReturnCode)2059;
					return;
				case StatusDataType.CipherSpec:
					this.context.ConnectReturnCode = ((this.context.AutomatonTcp.Automaton.Context as AutomatonTcpContext).ConnectionParameters.UseSsl ? ReturnCode.QmConnectCipherSpec : ReturnCode.QmConnectSslExpected);
					return;
				case StatusDataType.SslCertificateRequired:
					this.context.ConnectReturnCode = (ReturnCode)2059;
					return;
				}
				throw new InvalidOperationException("unknown status data type");
			}
			this.context.ConnectReturnCode = ReturnCode.QmConnectNoChannel;
		}

		// Token: 0x06005530 RID: 21808 RVA: 0x0015A46B File Offset: 0x0015866B
		public void ActionSetDisconnected()
		{
			this.context.ConnectReturnCode = ReturnCode.QmConnectTcpDisconnected;
		}

		// Token: 0x06005531 RID: 21809 RVA: 0x0015A480 File Offset: 0x00158680
		public void ActionSendDetach()
		{
			DeterminantMessage determinantMessage = new DeterminantMessage();
			determinantMessage.QueueManagerDeterminant = this.context.DeterminantForTcp;
			this.automaton.SendToTcp(101, determinantMessage);
		}

		// Token: 0x06005532 RID: 21810 RVA: 0x0015A4B2 File Offset: 0x001586B2
		public void ActionDisconnectFromTcp()
		{
			this.automaton.DisconnectFromTcp();
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x0015A4BF File Offset: 0x001586BF
		public void ActionSetWaitConnectEvent()
		{
			this.context.ConnectedEvent.Set();
		}

		// Token: 0x040043A8 RID: 17320
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.HandshakeFailed;

		// Token: 0x040043A9 RID: 17321
		private AutomatonQueueManager automaton;

		// Token: 0x040043AA RID: 17322
		private AutomatonQueueManagerContext context;

		// Token: 0x040043AB RID: 17323
		private static string[] traceLines = new string[] { "State: HandshakeFailed, Evt: Rejected, Act: SetRejected, Evt: DetachTcp", "State: HandshakeFailed, Evt: TcpFailed, Act: SetDisconnected, Evt: DetachTcp", "State: HandshakeFailed, Evt: TcpDisconnected, Stop", "State: HandshakeFailed, Evt: StatusData, Act: SetStatusDataRc, Evt: DetachTcp", "State: HandshakeFailed, Evt: QmConnError, Evt: DetachTcp", "State: HandshakeFailed, Evt: XaOpenError, Evt: DetachTcp", "State: HandshakeFailed, Evt: DetachTcp, Act: SendDetach, Stop", "State: HandshakeFailed, Evt: Detached, Act: DisconnectFromTcp, Evt: Disconnected", "State: HandshakeFailed, Evt: Disconnected, Act: SetWaitConnectEvent, State: UnConnected, Stop" };
	}
}
