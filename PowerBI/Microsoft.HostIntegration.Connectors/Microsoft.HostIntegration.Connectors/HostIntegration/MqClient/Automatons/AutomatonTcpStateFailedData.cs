using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AD5 RID: 2773
	internal class AutomatonTcpStateFailedData : StateAsCodeDriver
	{
		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x06005856 RID: 22614 RVA: 0x0016C107 File Offset: 0x0016A307
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005857 RID: 22615 RVA: 0x0016C11A File Offset: 0x0016A31A
		internal AutomatonTcpStateFailedData(AutomatonTcp driver, AutomatonTcpContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005858 RID: 22616 RVA: 0x0016C138 File Offset: 0x0016A338
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
					if (automatonTcpEvent != AutomatonTcpEvent.DataReceived)
					{
						switch (automatonTcpEvent)
						{
						case AutomatonTcpEvent.DataToSend:
							this.ActionIgnoreClientData();
							num = 4;
							goto IL_00B6;
						case AutomatonTcpEvent.SendTimer:
							num = 6;
							goto IL_00B6;
						case AutomatonTcpEvent.ReceiveTimer:
							num = 7;
							goto IL_00B6;
						case AutomatonTcpEvent.QmAttach:
							this.ActionAttachFailed();
							num = 1;
							goto IL_00B6;
						case AutomatonTcpEvent.QmDetach:
							this.ActionDetached();
							num = 2;
							goto IL_00B6;
						case AutomatonTcpEvent.Disconnect:
							this.ActionSetWaitEvent();
							automatonTcpState = AutomatonTcpState.UnConnected;
							num = 3;
							goto IL_00B6;
						case AutomatonTcpEvent.TcpFailed:
							this.ActionTcpDisconnected();
							num = 0;
							goto IL_00B6;
						}
						throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
					}
					this.ActionIgnoreServerData();
					num = 5;
					IL_00B6:
					automatonTcpEvent = automatonTcpEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonTcpStateFailedData.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: FailedData, Event: " + automatonTcpEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonTcpEvent;
			return (int)automatonTcpState;
		}

		// Token: 0x06005859 RID: 22617 RVA: 0x0016C2A8 File Offset: 0x0016A4A8
		public void ActionTcpDisconnected()
		{
			this.automaton.BroadcastToQueueManager(new AsynchronousConnectionMessage(404));
			Pooling.FailConnection(this.context.ChannelQueueManager);
		}

		// Token: 0x0600585A RID: 22618 RVA: 0x0016C2D0 File Offset: 0x0016A4D0
		public void ActionAttachFailed()
		{
			AsynchronousConnectionMessage attach = this.context.Attach;
			attach.Change(401, 452);
			this.automaton.SendToQueueManager(attach);
		}

		// Token: 0x0600585B RID: 22619 RVA: 0x0016C308 File Offset: 0x0016A508
		public void ActionDetached()
		{
			AsynchronousConnectionMessage detach = this.context.Detach;
			this.context.Detach = null;
			detach.Change(402);
			this.automaton.SendToQueueManager(detach);
		}

		// Token: 0x0600585C RID: 22620 RVA: 0x0016C344 File Offset: 0x0016A544
		public void ActionSetWaitEvent()
		{
			this.context.DisconnectedEvent.Set();
		}

		// Token: 0x0600585D RID: 22621 RVA: 0x0016C357 File Offset: 0x0016A557
		public void ActionIgnoreClientData()
		{
			PassThroughData passThroughData = this.context.MessageFromQmAutomaton.Contents as PassThroughData;
			if (passThroughData == null)
			{
				throw new InvalidOperationException("Data from Qm is not in the right format");
			}
			passThroughData.ReturnBuffers();
			this.context.MessageFromQmAutomaton = null;
		}

		// Token: 0x0600585E RID: 22622 RVA: 0x0016C38D File Offset: 0x0016A58D
		public void ActionIgnoreServerData()
		{
			if (this.context.DataToQmAutomaton != null)
			{
				this.context.DataToQmAutomaton.ReturnToOwner();
				this.context.DataToQmAutomaton = null;
			}
		}

		// Token: 0x040044A8 RID: 17576
		private AutomatonTcpState stateNumber = AutomatonTcpState.FailedData;

		// Token: 0x040044A9 RID: 17577
		private AutomatonTcp automaton;

		// Token: 0x040044AA RID: 17578
		private AutomatonTcpContext context;

		// Token: 0x040044AB RID: 17579
		private static string[] traceLines = new string[] { "State: FailedData, Evt: TcpFailed, Act: TcpDisconnected, Stop", "State: FailedData, Evt: QmAttach, Act: AttachFailed, Stop", "State: FailedData, Evt: QmDetach, Act: Detached, Stop", "State: FailedData, Evt: Disconnect, Act: SetWaitEvent, State: UnConnected, Stop", "State: FailedData, Evt: DataToSend, Act: IgnoreClientData, Stop", "State: FailedData, Evt: DataReceived, Act: IgnoreServerData, Stop", "State: FailedData, Evt: SendTimer, Stop", "State: FailedData, Evt: ReceiveTimer, Stop" };
	}
}
