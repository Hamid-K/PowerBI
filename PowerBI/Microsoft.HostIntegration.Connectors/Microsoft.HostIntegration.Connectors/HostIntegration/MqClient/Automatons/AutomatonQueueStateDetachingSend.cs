using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AC5 RID: 2757
	internal class AutomatonQueueStateDetachingSend : StateAsCodeDriver
	{
		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x06005752 RID: 22354 RVA: 0x0016639C File Offset: 0x0016459C
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005753 RID: 22355 RVA: 0x001663AF File Offset: 0x001645AF
		internal AutomatonQueueStateDetachingSend(AutomatonQueue driver, AutomatonQueueContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005754 RID: 22356 RVA: 0x001663D0 File Offset: 0x001645D0
		public override int Process(ref int eventToProcess)
		{
			AutomatonQueueEvent automatonQueueEvent = (AutomatonQueueEvent)eventToProcess;
			AutomatonQueueState automatonQueueState = this.stateNumber;
			try
			{
				while (automatonQueueState == this.stateNumber && automatonQueueEvent != AutomatonQueueEvent.Stop)
				{
					AutomatonQueueEvent automatonQueueEvent2 = AutomatonQueueEvent.Stop;
					int num;
					if (automatonQueueEvent != AutomatonQueueEvent.ServerData)
					{
						switch (automatonQueueEvent)
						{
						case AutomatonQueueEvent.QmFailed:
							automatonQueueEvent2 = AutomatonQueueEvent.InFailedState;
							num = 2;
							goto IL_010E;
						case AutomatonQueueEvent.Close:
							this.ActionSetClose();
							num = 5;
							goto IL_010E;
						case AutomatonQueueEvent.AsyncPutDone:
							num = 11;
							goto IL_010E;
						case AutomatonQueueEvent.QmFailedPut:
							this.ActionSetFailedPut();
							automatonQueueEvent2 = AutomatonQueueEvent.InFailedState;
							num = 3;
							goto IL_010E;
						case AutomatonQueueEvent.InFailedState:
							this.ActionSetInFailedState();
							num = 4;
							goto IL_010E;
						case AutomatonQueueEvent.Detached:
							if (this.PreConditionPutDone())
							{
								this.ActionSetWaitSendEvent();
								automatonQueueEvent2 = AutomatonQueueEvent.DisconnectQm;
								num = 6;
								goto IL_010E;
							}
							automatonQueueEvent2 = AutomatonQueueEvent.DisconnectQm;
							num = 7;
							goto IL_010E;
						case AutomatonQueueEvent.DisconnectQm:
							this.ActionDisconnectFromQm();
							automatonQueueEvent2 = AutomatonQueueEvent.Disconnected;
							num = 8;
							goto IL_010E;
						case AutomatonQueueEvent.Disconnected:
							if (this.PreConditionCloseCalled())
							{
								this.ActionSetWaitCloseEvent();
								automatonQueueState = AutomatonQueueState.Closed;
								num = 9;
								goto IL_010E;
							}
							automatonQueueState = AutomatonQueueState.Closing;
							num = 10;
							goto IL_010E;
						}
						throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
					}
					if (this.PreConditionCloseReply())
					{
						this.ActionIgnoreServerData();
						automatonQueueEvent2 = AutomatonQueueEvent.Detached;
						num = 0;
					}
					else
					{
						this.ActionIgnoreServerData();
						num = 1;
					}
					IL_010E:
					automatonQueueEvent = automatonQueueEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueStateDetachingSend.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: DetachingSend, Event: " + automatonQueueEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueEvent;
			return (int)automatonQueueState;
		}

		// Token: 0x06005755 RID: 22357 RVA: 0x001665A4 File Offset: 0x001647A4
		public bool PreConditionCloseReply()
		{
			return (this.context.MessageFromQueueManager.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqCloseReply;
		}

		// Token: 0x06005756 RID: 22358 RVA: 0x001665CC File Offset: 0x001647CC
		public bool PreConditionPutDone()
		{
			return this.context.PutPerformed;
		}

		// Token: 0x06005757 RID: 22359 RVA: 0x001665D9 File Offset: 0x001647D9
		public bool PreConditionCloseCalled()
		{
			return this.context.CloseCalled;
		}

		// Token: 0x06005758 RID: 22360 RVA: 0x001665E6 File Offset: 0x001647E6
		public void ActionSetInFailedState()
		{
			this.context.InFailedState = true;
		}

		// Token: 0x06005759 RID: 22361 RVA: 0x001665F4 File Offset: 0x001647F4
		public void ActionIgnoreServerData()
		{
			(this.context.MessageFromQueueManager.Contents as PassThroughData).ReturnBuffers();
			this.context.MessageFromQueueManager = null;
		}

		// Token: 0x0600575A RID: 22362 RVA: 0x0016661C File Offset: 0x0016481C
		public void ActionSetFailedPut()
		{
			this.context.SendReturnCode = ReturnCode.QSendFailedQmFailed;
			this.context.PutPerformed = true;
		}

		// Token: 0x0600575B RID: 22363 RVA: 0x0016663A File Offset: 0x0016483A
		public void ActionSetClose()
		{
			this.context.CloseCalled = true;
		}

		// Token: 0x0600575C RID: 22364 RVA: 0x00166648 File Offset: 0x00164848
		public void ActionSetWaitSendEvent()
		{
			this.context.SentEvent.Set();
		}

		// Token: 0x0600575D RID: 22365 RVA: 0x0016665B File Offset: 0x0016485B
		public void ActionDisconnectFromQm()
		{
			this.automaton.DisconnectFromQueueManager();
		}

		// Token: 0x0600575E RID: 22366 RVA: 0x00166668 File Offset: 0x00164868
		public void ActionSetWaitCloseEvent()
		{
			this.context.ClosedEvent.Set();
			if (this.context.ReceiveTimer != null)
			{
				this.context.ReceiveTimer.Change(-1, -1);
				this.context.ReceiveTimer.Dispose();
				this.context.ReceiveTimer = null;
			}
		}

		// Token: 0x0400443C RID: 17468
		private AutomatonQueueState stateNumber = AutomatonQueueState.DetachingSend;

		// Token: 0x0400443D RID: 17469
		private AutomatonQueue automaton;

		// Token: 0x0400443E RID: 17470
		private AutomatonQueueContext context;

		// Token: 0x0400443F RID: 17471
		private static string[] traceLines = new string[]
		{
			"State: DetachingSend, Evt: ServerData, Pre: CloseReply, Act: IgnoreServerData, Evt: Detached", "State: DetachingSend, Evt: ServerData, Act: IgnoreServerData, Stop", "State: DetachingSend, Evt: QmFailed, Evt: InFailedState", "State: DetachingSend, Evt: QmFailedPut, Act: SetFailedPut, Evt: InFailedState", "State: DetachingSend, Evt: InFailedState, Act: SetInFailedState, Stop", "State: DetachingSend, Evt: Close, Act: SetClose, Stop", "State: DetachingSend, Evt: Detached, Pre: PutDone, Act: SetWaitSendEvent, Evt: DisconnectQm", "State: DetachingSend, Evt: Detached, Evt: DisconnectQm", "State: DetachingSend, Evt: DisconnectQm, Act: DisconnectFromQm, Evt: Disconnected", "State: DetachingSend, Evt: Disconnected, Pre: CloseCalled, Act: SetWaitCloseEvent, State: Closed, Stop",
			"State: DetachingSend, Evt: Disconnected, State: Closing, Stop", "State: DetachingSend, Evt: AsyncPutDone, Stop"
		};
	}
}
