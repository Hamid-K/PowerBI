using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AAD RID: 2733
	internal class AutomatonQueueManagerStateTxnOpen : StateAsCodeDriver
	{
		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x0600560B RID: 22027 RVA: 0x0015FD7D File Offset: 0x0015DF7D
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600560C RID: 22028 RVA: 0x0015FD90 File Offset: 0x0015DF90
		internal AutomatonQueueManagerStateTxnOpen(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600560D RID: 22029 RVA: 0x0015FDB0 File Offset: 0x0015DFB0
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
						if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.ServerData)
						{
							switch (automatonQueueManagerEvent)
							{
							case AutomatonQueueManagerEvent.QAttach:
								this.ActionEnqueueOpen();
								automatonQueueManagerState = AutomatonQueueManagerState.OpenCloseTxnOpen;
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartOpen;
								num = 2;
								break;
							case AutomatonQueueManagerEvent.QDetach:
								if (this.PreConditionDoClose())
								{
									this.ActionEnqueueClose();
									automatonQueueManagerState = AutomatonQueueManagerState.OpenCloseTxnOpen;
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartClose;
									num = 3;
								}
								else
								{
									this.ActionDetached();
									num = 4;
								}
								break;
							case AutomatonQueueManagerEvent.Disconnect:
								this.ActionSendXaClose();
								automatonQueueManagerState = AutomatonQueueManagerState.TxnClosing;
								num = 5;
								break;
							case AutomatonQueueManagerEvent.MqCommand:
								if (this.PreConditionAsyncStatus())
								{
									this.ActionSendMqStat();
									num = 6;
								}
								else
								{
									if (!this.PreConditionXaStart())
									{
										throw new InvalidOperationException("No Precondition returned true");
									}
									this.ActionSendXaStart();
									automatonQueueManagerState = AutomatonQueueManagerState.TxnStarting;
									num = 7;
								}
								break;
							case AutomatonQueueManagerEvent.QueueDisconnected:
								num = 10;
								break;
							case AutomatonQueueManagerEvent.Enlist:
								this.ActionSetEnlistOkWaitEvent();
								num = 8;
								break;
							default:
								throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
							}
						}
						else if (this.PreConditionMqStatReply())
						{
							this.ActionSetMqStatWaitEvent();
							num = 0;
						}
						else
						{
							if (!this.PreConditionServerClosing())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							automatonQueueManagerState = AutomatonQueueManagerState.ServerClosing;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.InFailedState;
							num = 1;
						}
					}
					else
					{
						automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 9;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateTxnOpen.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: TxnOpen, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x0015FF98 File Offset: 0x0015E198
		public bool PreConditionMqStatReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqStatReply;
		}

		// Token: 0x0600560F RID: 22031 RVA: 0x0015FFC0 File Offset: 0x0015E1C0
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x06005610 RID: 22032 RVA: 0x00160030 File Offset: 0x0015E230
		public bool PreConditionDoClose()
		{
			PassThroughData passThroughData = this.context.DataMessageFromQueue.Contents as PassThroughData;
			return passThroughData.Buffers != null && passThroughData.Buffers.Count != 0;
		}

		// Token: 0x06005611 RID: 22033 RVA: 0x0016006B File Offset: 0x0015E26B
		public bool PreConditionAsyncStatus()
		{
			return this.context.CommandType == MqCommandType.AsyncStatus;
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x0016007B File Offset: 0x0015E27B
		public bool PreConditionXaStart()
		{
			return this.context.CommandType == MqCommandType.XaStart;
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x0016008C File Offset: 0x0015E28C
		public void ActionSetMqStatWaitEvent()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			this.automaton.ExtractAsyncStatus(passThroughData.Buffers[0]);
			passThroughData.ReturnBuffers();
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x001600DD File Offset: 0x0015E2DD
		public void ActionEnqueueOpen()
		{
			this.context.QueuedOpens.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x00160110 File Offset: 0x0015E310
		public void ActionEnqueueClose()
		{
			this.context.QueuedCloses.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x06005616 RID: 22038 RVA: 0x00160144 File Offset: 0x0015E344
		public void ActionDetached()
		{
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Queue Closed");
			}
			AsynchronousConnectionMessage dataMessageFromQueue = this.context.DataMessageFromQueue;
			this.context.DataMessageFromQueue = null;
			dataMessageFromQueue.Change(203);
			this.automaton.SendToQueue(dataMessageFromQueue);
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x001601AC File Offset: 0x0015E3AC
		public void ActionSendXaClose()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaClose(buffer, this.context.DeterminantForTcp);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Closing the XA transaction", Array.Empty<object>()));
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x06005618 RID: 22040 RVA: 0x00160224 File Offset: 0x0015E424
		public void ActionSendMqStat()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			AutomatonQueueManager.GenerateMqStat(buffer, this.context.DeterminantForTcp);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Sending MQ STAT");
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x06005619 RID: 22041 RVA: 0x00160288 File Offset: 0x0015E488
		public void ActionSendXaStart()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaStart(buffer, this.context.DeterminantForTcp);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Sending XA_START");
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x0600561A RID: 22042 RVA: 0x001602EF File Offset: 0x0015E4EF
		public void ActionSetEnlistOkWaitEvent()
		{
			this.context.EnlistQueued = false;
			this.context.EnlistReturnCode = ReturnCode.Ok;
			this.context.EnlistEvent.Set();
		}

		// Token: 0x040043DC RID: 17372
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.TxnOpen;

		// Token: 0x040043DD RID: 17373
		private AutomatonQueueManager automaton;

		// Token: 0x040043DE RID: 17374
		private AutomatonQueueManagerContext context;

		// Token: 0x040043DF RID: 17375
		private static string[] traceLines = new string[]
		{
			"State: TxnOpen, Evt: ServerData, Pre: MqStatReply, Act: SetMqStatWaitEvent, Stop", "State: TxnOpen, Evt: ServerData, Pre: ServerClosing, State: ServerClosing, Evt: InFailedState", "State: TxnOpen, Evt: QAttach, Act: EnqueueOpen, State: OpenCloseTxnOpen, Evt: StartOpen", "State: TxnOpen, Evt: QDetach, Pre: DoClose, Act: EnqueueClose, State: OpenCloseTxnOpen, Evt: StartClose", "State: TxnOpen, Evt: QDetach, Act: Detached, Stop", "State: TxnOpen, Evt: Disconnect, Act: SendXaClose, State: TxnClosing, Stop", "State: TxnOpen, Evt: MqCommand, Pre: AsyncStatus, Act: SendMqStat, Stop", "State: TxnOpen, Evt: MqCommand, Pre: XaStart, Act: SendXaStart, State: TxnStarting, Stop", "State: TxnOpen, Evt: Enlist, Act: SetEnlistOkWaitEvent, Stop", "State: TxnOpen, Evt: TcpDisconnected, State: DataFlowFailed, Evt: TcpFailed",
			"State: TxnOpen, Evt: QueueDisconnected, Stop"
		};
	}
}
