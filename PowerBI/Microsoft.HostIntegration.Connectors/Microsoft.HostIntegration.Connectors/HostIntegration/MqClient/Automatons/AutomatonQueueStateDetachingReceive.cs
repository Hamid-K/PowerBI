using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AC4 RID: 2756
	internal class AutomatonQueueStateDetachingReceive : StateAsCodeDriver
	{
		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x06005744 RID: 22340 RVA: 0x00165FFF File Offset: 0x001641FF
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005745 RID: 22341 RVA: 0x00166012 File Offset: 0x00164212
		internal AutomatonQueueStateDetachingReceive(AutomatonQueue driver, AutomatonQueueContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005746 RID: 22342 RVA: 0x00166034 File Offset: 0x00164234
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
					if (automatonQueueEvent <= AutomatonQueueEvent.ServerData)
					{
						if (automatonQueueEvent != AutomatonQueueEvent.Timer)
						{
							if (automatonQueueEvent != AutomatonQueueEvent.ServerData)
							{
								goto IL_00EC;
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
						}
						else
						{
							num = 11;
						}
					}
					else if (automatonQueueEvent != AutomatonQueueEvent.QmFailed)
					{
						if (automatonQueueEvent != AutomatonQueueEvent.Close)
						{
							switch (automatonQueueEvent)
							{
							case AutomatonQueueEvent.InFailedState:
								this.ActionSetInFailedState();
								num = 4;
								break;
							case AutomatonQueueEvent.Detached:
								if (this.PreConditionGetDone())
								{
									this.ActionSetWaitReceiveEvent();
									automatonQueueEvent2 = AutomatonQueueEvent.DisconnectQm;
									num = 6;
								}
								else
								{
									automatonQueueEvent2 = AutomatonQueueEvent.DisconnectQm;
									num = 7;
								}
								break;
							case AutomatonQueueEvent.DisconnectQm:
								this.ActionDisconnectFromQm();
								automatonQueueEvent2 = AutomatonQueueEvent.Disconnected;
								num = 8;
								break;
							case AutomatonQueueEvent.Disconnected:
								if (this.PreConditionCloseCalled())
								{
									this.ActionSetWaitCloseEvent();
									automatonQueueState = AutomatonQueueState.Closed;
									num = 9;
								}
								else
								{
									automatonQueueState = AutomatonQueueState.Closing;
									num = 10;
								}
								break;
							case AutomatonQueueEvent.QmFailedGet:
								this.ActionSetFailedGet();
								automatonQueueEvent2 = AutomatonQueueEvent.InFailedState;
								num = 3;
								break;
							default:
								goto IL_00EC;
							}
						}
						else
						{
							this.ActionSetClose();
							num = 5;
						}
					}
					else
					{
						automatonQueueEvent2 = AutomatonQueueEvent.InFailedState;
						num = 2;
					}
					automatonQueueEvent = automatonQueueEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueStateDetachingReceive.traceLines[num]);
						continue;
					}
					continue;
					IL_00EC:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: DetachingReceive, Event: " + automatonQueueEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueEvent;
			return (int)automatonQueueState;
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x00166200 File Offset: 0x00164400
		public bool PreConditionCloseReply()
		{
			return (this.context.MessageFromQueueManager.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqCloseReply;
		}

		// Token: 0x06005748 RID: 22344 RVA: 0x00166228 File Offset: 0x00164428
		public bool PreConditionGetDone()
		{
			return this.context.GetPerformed;
		}

		// Token: 0x06005749 RID: 22345 RVA: 0x00166235 File Offset: 0x00164435
		public bool PreConditionCloseCalled()
		{
			return this.context.CloseCalled;
		}

		// Token: 0x0600574A RID: 22346 RVA: 0x00166242 File Offset: 0x00164442
		public void ActionSetInFailedState()
		{
			this.context.InFailedState = true;
		}

		// Token: 0x0600574B RID: 22347 RVA: 0x00166250 File Offset: 0x00164450
		public void ActionIgnoreServerData()
		{
			(this.context.MessageFromQueueManager.Contents as PassThroughData).ReturnBuffers();
			this.context.MessageFromQueueManager = null;
		}

		// Token: 0x0600574C RID: 22348 RVA: 0x00166278 File Offset: 0x00164478
		public void ActionSetFailedGet()
		{
			this.context.ReceiveReturnCode = ReturnCode.QReceiveFailedQmFailed;
			this.context.GetPerformed = true;
		}

		// Token: 0x0600574D RID: 22349 RVA: 0x00166296 File Offset: 0x00164496
		public void ActionSetClose()
		{
			this.context.CloseCalled = true;
		}

		// Token: 0x0600574E RID: 22350 RVA: 0x001662A4 File Offset: 0x001644A4
		public void ActionSetWaitReceiveEvent()
		{
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x0600574F RID: 22351 RVA: 0x001662B7 File Offset: 0x001644B7
		public void ActionDisconnectFromQm()
		{
			this.automaton.DisconnectFromQueueManager();
		}

		// Token: 0x06005750 RID: 22352 RVA: 0x001662C4 File Offset: 0x001644C4
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

		// Token: 0x04004438 RID: 17464
		private AutomatonQueueState stateNumber = AutomatonQueueState.DetachingReceive;

		// Token: 0x04004439 RID: 17465
		private AutomatonQueue automaton;

		// Token: 0x0400443A RID: 17466
		private AutomatonQueueContext context;

		// Token: 0x0400443B RID: 17467
		private static string[] traceLines = new string[]
		{
			"State: DetachingReceive, Evt: ServerData, Pre: CloseReply, Act: IgnoreServerData, Evt: Detached", "State: DetachingReceive, Evt: ServerData, Act: IgnoreServerData, Stop", "State: DetachingReceive, Evt: QmFailed, Evt: InFailedState", "State: DetachingReceive, Evt: QmFailedGet, Act: SetFailedGet, Evt: InFailedState", "State: DetachingReceive, Evt: InFailedState, Act: SetInFailedState, Stop", "State: DetachingReceive, Evt: Close, Act: SetClose, Stop", "State: DetachingReceive, Evt: Detached, Pre: GetDone, Act: SetWaitReceiveEvent, Evt: DisconnectQm", "State: DetachingReceive, Evt: Detached, Evt: DisconnectQm", "State: DetachingReceive, Evt: DisconnectQm, Act: DisconnectFromQm, Evt: Disconnected", "State: DetachingReceive, Evt: Disconnected, Pre: CloseCalled, Act: SetWaitCloseEvent, State: Closed, Stop",
			"State: DetachingReceive, Evt: Disconnected, State: Closing, Stop", "State: DetachingReceive, Evt: Timer, Stop"
		};
	}
}
