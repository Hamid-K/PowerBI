using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A9B RID: 2715
	internal class AutomatonQueueManagerStateDataFlow : StateAsCodeDriver
	{
		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x060054EC RID: 21740 RVA: 0x001585BC File Offset: 0x001567BC
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060054ED RID: 21741 RVA: 0x001585CF File Offset: 0x001567CF
		internal AutomatonQueueManagerStateDataFlow(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x001585F0 File Offset: 0x001567F0
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
							case AutomatonQueueManagerEvent.ClientData:
								this.ActionPassToTcp();
								num = 0;
								break;
							case AutomatonQueueManagerEvent.AsyncSendDone:
								this.ActionMakeAsyncSent();
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.MessageToQ;
								num = 4;
								break;
							case AutomatonQueueManagerEvent.MessageToQ:
								this.ActionSendMessageToQ();
								if (!this.PostConditionQueueFound())
								{
									throw new InvalidOperationException("No Postcondition returned true");
								}
								num = 5;
								break;
							case AutomatonQueueManagerEvent.QAttach:
								this.ActionEnqueueOpen();
								automatonQueueManagerState = AutomatonQueueManagerState.OpeningClosing;
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartOpen;
								num = 6;
								break;
							case AutomatonQueueManagerEvent.QDetach:
								if (this.PreConditionDoClose())
								{
									this.ActionEnqueueClose();
									automatonQueueManagerState = AutomatonQueueManagerState.OpeningClosing;
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartClose;
									num = 7;
								}
								else
								{
									this.ActionDetached();
									num = 8;
								}
								break;
							case AutomatonQueueManagerEvent.Disconnect:
								this.ActionSendMqDisc();
								automatonQueueManagerState = AutomatonQueueManagerState.Disconnecting;
								num = 9;
								break;
							case AutomatonQueueManagerEvent.MqCommand:
								if (!this.PreConditionAsyncStatus())
								{
									throw new InvalidOperationException("No Precondition returned true");
								}
								this.ActionSendMqStat();
								num = 10;
								break;
							case AutomatonQueueManagerEvent.QueueDisconnected:
								num = 12;
								break;
							default:
								throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
							}
						}
						else if (this.PreConditionForClient())
						{
							this.ActionMakeClientMessage();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.MessageToQ;
							num = 1;
						}
						else if (this.PreConditionMqStatReply())
						{
							this.ActionSetMqStatWaitEvent();
							num = 2;
						}
						else
						{
							if (!this.PreConditionServerClosing())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							automatonQueueManagerState = AutomatonQueueManagerState.ServerClosing;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.InFailedState;
							num = 3;
						}
					}
					else
					{
						automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 11;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateDataFlow.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: DataFlow, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x00158814 File Offset: 0x00156A14
		public bool PreConditionForClient()
		{
			SegmentType segmentType = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType;
			if (segmentType > SegmentType.StatusData)
			{
				switch (segmentType)
				{
				case SegmentType.SocketAction:
					return false;
				case SegmentType.AsyncMessage:
				case SegmentType.Notification:
					break;
				case SegmentType.RequestMessages:
					goto IL_00A3;
				default:
					switch (segmentType)
					{
					case SegmentType.MqConnectReply:
					case SegmentType.MqDisconnectReply:
						goto IL_008D;
					case SegmentType.MqOpenReply:
					case SegmentType.MqCloseReply:
						throw new InvalidOperationException("Open/Close replies in DataFlow");
					case SegmentType.MqGetReply:
					case SegmentType.MqPutReply:
						break;
					case (SegmentType)151:
					case (SegmentType)152:
					case (SegmentType)153:
					case (SegmentType)154:
					case (SegmentType)155:
					case (SegmentType)156:
						goto IL_00A3;
					case SegmentType.MqStatReply:
						return false;
					default:
						goto IL_00A3;
					}
					break;
				}
				return true;
			}
			if (segmentType != SegmentType.InitialData && segmentType != SegmentType.StatusData)
			{
				goto IL_00A3;
			}
			IL_008D:
			throw new InvalidOperationException("QueueManager data in DataFlow");
			IL_00A3:
			throw new InvalidOperationException("Unknown Segment");
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x001588CE File Offset: 0x00156ACE
		public bool PreConditionMqStatReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqStatReply;
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x001588F6 File Offset: 0x00156AF6
		public bool PreConditionAsyncStatus()
		{
			return this.context.CommandType == MqCommandType.AsyncStatus;
		}

		// Token: 0x060054F2 RID: 21746 RVA: 0x00158908 File Offset: 0x00156B08
		public bool PreConditionDoClose()
		{
			PassThroughData passThroughData = this.context.DataMessageFromQueue.Contents as PassThroughData;
			return passThroughData.Buffers != null && passThroughData.Buffers.Count != 0;
		}

		// Token: 0x060054F3 RID: 21747 RVA: 0x00158944 File Offset: 0x00156B44
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x001589B4 File Offset: 0x00156BB4
		public void ActionPassToTcp()
		{
			AsynchronousConnectionMessage dataMessageFromQueue = this.context.DataMessageFromQueue;
			this.context.DataMessageFromQueue = null;
			dataMessageFromQueue.Change((dataMessageFromQueue.SubType == 350) ? 104 : 102);
			this.automaton.SendToTcp(dataMessageFromQueue);
		}

		// Token: 0x060054F5 RID: 21749 RVA: 0x00158A00 File Offset: 0x00156C00
		public void ActionMakeClientMessage()
		{
			this.context.MessageToQueue = this.context.DataMessageFromTcp;
			this.context.DataMessageFromTcp = null;
			PassThroughData passThroughData = this.context.MessageToQueue.Contents as PassThroughData;
			if (passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.MqGetReply)
			{
				if ((passThroughData.SegmentHeaderInformation.ControlFlag1 & ControlFlag1.FirstSegment) == ControlFlag1.FirstSegment)
				{
					this.context.LastGetReplyObjectHandle = passThroughData.QueueDeterminant;
				}
				else
				{
					passThroughData.QueueDeterminant = this.context.LastGetReplyObjectHandle;
				}
			}
			else if (passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.AsyncMessage)
			{
				this.context.GlobalMessageIndex = ConversionHelpers.ExtractIntFromBuffer(passThroughData.Buffers[0].Data, passThroughData.SegmentHeaderInformation.LengthOfSegmentHeader + 12, passThroughData.SegmentHeaderInformation.LittleEndian);
			}
			this.context.MessageToQueue.Change(200);
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x00158AF0 File Offset: 0x00156CF0
		public void ActionMakeAsyncSent()
		{
			AsynchronousConnectionMessage dataMessageFromTcp = this.context.DataMessageFromTcp;
			this.context.DataMessageFromTcp = null;
			dataMessageFromTcp.Change(204);
			this.context.MessageToQueue = dataMessageFromTcp;
		}

		// Token: 0x060054F7 RID: 21751 RVA: 0x00158B2C File Offset: 0x00156D2C
		public void ActionEnqueueOpen()
		{
			this.context.QueuedOpens.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x060054F8 RID: 21752 RVA: 0x00158B5F File Offset: 0x00156D5F
		public void ActionEnqueueClose()
		{
			this.context.QueuedCloses.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x060054F9 RID: 21753 RVA: 0x00158B92 File Offset: 0x00156D92
		public void ActionSendMessageToQ()
		{
			this.automaton.SendToQueue(this.context.MessageToQueue);
		}

		// Token: 0x060054FA RID: 21754 RVA: 0x00158BAC File Offset: 0x00156DAC
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

		// Token: 0x060054FB RID: 21755 RVA: 0x00158C14 File Offset: 0x00156E14
		public void ActionSendMqDisc()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			AutomatonQueueManager.GenerateMqDisconnect(buffer, this.context.DeterminantForTcp);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Sending MQ Disconnect");
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x00158C78 File Offset: 0x00156E78
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

		// Token: 0x060054FD RID: 21757 RVA: 0x00158CDC File Offset: 0x00156EDC
		public void ActionSetMqStatWaitEvent()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			this.automaton.ExtractAsyncStatus(passThroughData.Buffers[0]);
			passThroughData.ReturnBuffers();
			this.context.CommandEvent.Set();
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x00158D2D File Offset: 0x00156F2D
		public bool PostConditionQueueFound()
		{
			return this.context.ConnectionFoundToQueue;
		}

		// Token: 0x04004394 RID: 17300
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.DataFlow;

		// Token: 0x04004395 RID: 17301
		private AutomatonQueueManager automaton;

		// Token: 0x04004396 RID: 17302
		private AutomatonQueueManagerContext context;

		// Token: 0x04004397 RID: 17303
		private static string[] traceLines = new string[]
		{
			"State: DataFlow, Evt: ClientData, Act: PassToTcp, Stop", "State: DataFlow, Evt: ServerData, Pre: ForClient, Act: MakeClientMessage, Evt: MessageToQ", "State: DataFlow, Evt: ServerData, Pre: MqStatReply, Act: SetMqStatWaitEvent, Stop", "State: DataFlow, Evt: ServerData, Pre: ServerClosing, State: ServerClosing, Evt: InFailedState", "State: DataFlow, Evt: AsyncSendDone, Act: MakeAsyncSent, Evt: MessageToQ", "State: DataFlow, Evt: MessageToQ, Act: SendMessageToQ, Post: QueueFound, Stop", "State: DataFlow, Evt: QAttach, Act: EnqueueOpen, State: OpeningClosing, Evt: StartOpen", "State: DataFlow, Evt: QDetach, Pre: DoClose, Act: EnqueueClose, State: OpeningClosing, Evt: StartClose", "State: DataFlow, Evt: QDetach, Act: Detached, Stop", "State: DataFlow, Evt: Disconnect, Act: SendMqDisc, State: Disconnecting, Stop",
			"State: DataFlow, Evt: MqCommand, Pre: AsyncStatus, Act: SendMqStat, Stop", "State: DataFlow, Evt: TcpDisconnected, State: DataFlowFailed, Evt: TcpFailed", "State: DataFlow, Evt: QueueDisconnected, Stop"
		};
	}
}
