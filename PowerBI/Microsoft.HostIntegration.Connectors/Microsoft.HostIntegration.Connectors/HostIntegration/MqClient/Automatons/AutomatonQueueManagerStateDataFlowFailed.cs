using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A9C RID: 2716
	internal class AutomatonQueueManagerStateDataFlowFailed : StateAsCodeDriver
	{
		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x06005500 RID: 21760 RVA: 0x00158DC1 File Offset: 0x00156FC1
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005501 RID: 21761 RVA: 0x00158DD4 File Offset: 0x00156FD4
		internal AutomatonQueueManagerStateDataFlowFailed(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005502 RID: 21762 RVA: 0x00158DF4 File Offset: 0x00156FF4
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
					if (automatonQueueManagerEvent <= AutomatonQueueManagerEvent.ServerData)
					{
						if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.TcpDisconnected)
						{
							if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.ServerData)
							{
								goto IL_012B;
							}
							this.ActionIgnoreServerData();
							num = 11;
						}
						else
						{
							num = 1;
						}
					}
					else if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.TcpFailed)
					{
						switch (automatonQueueManagerEvent)
						{
						case AutomatonQueueManagerEvent.Detached:
							this.ActionDisconnectFromTcp();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.Disconnected;
							num = 9;
							break;
						case AutomatonQueueManagerEvent.Disconnected:
							this.ActionSetWaitDisconnectEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.UnConnected;
							num = 10;
							break;
						case AutomatonQueueManagerEvent.SendSA:
						case AutomatonQueueManagerEvent.SendConnect:
						case AutomatonQueueManagerEvent.AsyncSendDone:
						case AutomatonQueueManagerEvent.MessageToQ:
						case AutomatonQueueManagerEvent.MqCommand:
							goto IL_012B;
						case AutomatonQueueManagerEvent.ClientData:
							num = 12;
							break;
						case AutomatonQueueManagerEvent.QAttach:
							this.ActionReturnOpenFailed();
							num = 5;
							break;
						case AutomatonQueueManagerEvent.QDetach:
							this.ActionDetached();
							num = 6;
							break;
						case AutomatonQueueManagerEvent.Disconnect:
							this.ActionDetachTcp();
							num = 8;
							break;
						case AutomatonQueueManagerEvent.QueueDisconnected:
							num = 14;
							break;
						case AutomatonQueueManagerEvent.Enlist:
							this.ActionSetEnlistRcWaitEvent();
							num = 7;
							break;
						default:
							switch (automatonQueueManagerEvent)
							{
							case AutomatonQueueManagerEvent.UnknownHandle:
								this.ActionSetInFailedState();
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.InformQueues;
								num = 2;
								break;
							case AutomatonQueueManagerEvent.ChannelQuiesced:
								this.ActionSetInFailedState();
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.InformQueues;
								num = 3;
								break;
							case AutomatonQueueManagerEvent.InformQueues:
								this.ActionSendQmFailed();
								num = 4;
								break;
							case AutomatonQueueManagerEvent.RmError:
								this.ActionSetInFailedState();
								num = 13;
								break;
							default:
								goto IL_012B;
							}
							break;
						}
					}
					else
					{
						this.ActionSetInFailedState();
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.InformQueues;
						num = 0;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateDataFlowFailed.traceLines[num]);
						continue;
					}
					continue;
					IL_012B:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: DataFlowFailed, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x00159000 File Offset: 0x00157200
		public void ActionSetInFailedState()
		{
			this.context.InFailedState = true;
		}

		// Token: 0x06005504 RID: 21764 RVA: 0x0015900E File Offset: 0x0015720E
		public void ActionSendQmFailed()
		{
			this.automaton.BroadcastToQueue(new AsynchronousConnectionMessage(201));
			Pooling.FailQueueManager(this.context.WrappedQueueManager);
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x00159038 File Offset: 0x00157238
		public void ActionReturnOpenFailed()
		{
			PassThroughData passThroughData = this.context.DataMessageFromQueue.Contents as PassThroughData;
			this.context.DataMessageFromQueue = null;
			passThroughData.ReturnBuffers();
			QAttachFailed qattachFailed = new QAttachFailed();
			qattachFailed.QueueDeterminant = passThroughData.QueueDeterminant;
			qattachFailed.ReturnCode = ReturnCode.QOpenFailedQmFailed;
			this.automaton.SendToQueue(202, qattachFailed);
		}

		// Token: 0x06005506 RID: 21766 RVA: 0x0015909C File Offset: 0x0015729C
		public void ActionDetached()
		{
			AsynchronousConnectionMessage dataMessageFromQueue = this.context.DataMessageFromQueue;
			this.context.DataMessageFromQueue = null;
			(dataMessageFromQueue.Contents as PassThroughData).ReturnBuffers();
			dataMessageFromQueue.Change(203);
			this.automaton.SendToQueue(dataMessageFromQueue);
		}

		// Token: 0x06005507 RID: 21767 RVA: 0x001590E8 File Offset: 0x001572E8
		public void ActionSetEnlistRcWaitEvent()
		{
			this.context.EnlistQueued = false;
			this.context.EnlistReturnCode = ReturnCode.EnlistDataFlowFailed;
			this.context.EnlistEvent.Set();
		}

		// Token: 0x06005508 RID: 21768 RVA: 0x00159118 File Offset: 0x00157318
		public void ActionDetachTcp()
		{
			PassThroughData passThroughData = new PassThroughData();
			passThroughData.QueueManagerDeterminant = this.context.DeterminantForTcp;
			this.automaton.SendToTcp(101, passThroughData);
		}

		// Token: 0x06005509 RID: 21769 RVA: 0x0015914A File Offset: 0x0015734A
		public void ActionDisconnectFromTcp()
		{
			this.automaton.DisconnectFromTcp();
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x00159157 File Offset: 0x00157357
		public void ActionSetWaitDisconnectEvent()
		{
			this.context.DisconnectedEvent.Set();
		}

		// Token: 0x0600550B RID: 21771 RVA: 0x0015916A File Offset: 0x0015736A
		public void ActionIgnoreServerData()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			this.context.DataMessageFromTcp = null;
			passThroughData.ReturnBuffers();
		}

		// Token: 0x04004398 RID: 17304
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.DataFlowFailed;

		// Token: 0x04004399 RID: 17305
		private AutomatonQueueManager automaton;

		// Token: 0x0400439A RID: 17306
		private AutomatonQueueManagerContext context;

		// Token: 0x0400439B RID: 17307
		private static string[] traceLines = new string[]
		{
			"State: DataFlowFailed, Evt: TcpFailed, Act: SetInFailedState, Evt: InformQueues", "State: DataFlowFailed, Evt: TcpDisconnected, Stop", "State: DataFlowFailed, Evt: UnknownHandle, Act: SetInFailedState, Evt: InformQueues", "State: DataFlowFailed, Evt: ChannelQuiesced, Act: SetInFailedState, Evt: InformQueues", "State: DataFlowFailed, Evt: InformQueues, Act: SendQmFailed, Stop", "State: DataFlowFailed, Evt: QAttach, Act: ReturnOpenFailed, Stop", "State: DataFlowFailed, Evt: QDetach, Act: Detached, Stop", "State: DataFlowFailed, Evt: Enlist, Act: SetEnlistRcWaitEvent, Stop", "State: DataFlowFailed, Evt: Disconnect, Act: DetachTcp, Stop", "State: DataFlowFailed, Evt: Detached, Act: DisconnectFromTcp, Evt: Disconnected",
			"State: DataFlowFailed, Evt: Disconnected, Act: SetWaitDisconnectEvent, State: UnConnected, Stop", "State: DataFlowFailed, Evt: ServerData, Act: IgnoreServerData, Stop", "State: DataFlowFailed, Evt: ClientData, Stop", "State: DataFlowFailed, Evt: RmError, Act: SetInFailedState, Stop", "State: DataFlowFailed, Evt: QueueDisconnected, Stop"
		};
	}
}
