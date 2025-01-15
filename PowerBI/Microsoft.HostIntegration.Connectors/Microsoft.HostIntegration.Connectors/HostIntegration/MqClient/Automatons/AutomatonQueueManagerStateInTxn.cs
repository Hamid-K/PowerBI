using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AA5 RID: 2725
	internal class AutomatonQueueManagerStateInTxn : StateAsCodeDriver
	{
		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x06005579 RID: 21881 RVA: 0x0015C2BE File Offset: 0x0015A4BE
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600557A RID: 21882 RVA: 0x0015C2D1 File Offset: 0x0015A4D1
		internal AutomatonQueueManagerStateInTxn(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600557B RID: 21883 RVA: 0x0015C2F0 File Offset: 0x0015A4F0
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
								automatonQueueManagerState = AutomatonQueueManagerState.OpenCloseInTxn;
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.FirstOpen;
								num = 6;
								break;
							case AutomatonQueueManagerEvent.QDetach:
								if (this.PreConditionDoClose())
								{
									this.ActionEnqueueClose();
									automatonQueueManagerState = AutomatonQueueManagerState.OpenCloseInTxn;
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.FirstClose;
									num = 7;
								}
								else
								{
									this.ActionDetached();
									num = 8;
								}
								break;
							case AutomatonQueueManagerEvent.Disconnect:
								this.ActionEnqueueDisconnect();
								num = 9;
								break;
							case AutomatonQueueManagerEvent.MqCommand:
								if (this.PreConditionAsyncStatus())
								{
									this.ActionSendMqStat();
									num = 10;
								}
								else if (this.PreConditionXaEnd())
								{
									this.ActionSendXaEnd();
									automatonQueueManagerState = AutomatonQueueManagerState.TxnEnding;
									num = 11;
								}
								else
								{
									if (!this.PreConditionXaRollback())
									{
										throw new InvalidOperationException("No Precondition returned true");
									}
									this.ActionSendXaRollback();
									automatonQueueManagerState = AutomatonQueueManagerState.TxnFinalizing;
									num = 12;
								}
								break;
							case AutomatonQueueManagerEvent.QueueDisconnected:
								num = 14;
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
						num = 13;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateInTxn.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: InTxn, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x0600557C RID: 21884 RVA: 0x0015C544 File Offset: 0x0015A744
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

		// Token: 0x0600557D RID: 21885 RVA: 0x0015C5FE File Offset: 0x0015A7FE
		public bool PreConditionMqStatReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqStatReply;
		}

		// Token: 0x0600557E RID: 21886 RVA: 0x0015C628 File Offset: 0x0015A828
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x0600557F RID: 21887 RVA: 0x0015C698 File Offset: 0x0015A898
		public bool PreConditionDoClose()
		{
			PassThroughData passThroughData = this.context.DataMessageFromQueue.Contents as PassThroughData;
			return passThroughData.Buffers != null && passThroughData.Buffers.Count != 0;
		}

		// Token: 0x06005580 RID: 21888 RVA: 0x0015C6D3 File Offset: 0x0015A8D3
		public bool PreConditionAsyncStatus()
		{
			return this.context.CommandType == MqCommandType.AsyncStatus;
		}

		// Token: 0x06005581 RID: 21889 RVA: 0x0015C6E3 File Offset: 0x0015A8E3
		public bool PreConditionXaEnd()
		{
			return this.context.CommandType == MqCommandType.XaEnd;
		}

		// Token: 0x06005582 RID: 21890 RVA: 0x0015C6F3 File Offset: 0x0015A8F3
		public bool PreConditionXaRollback()
		{
			return this.context.CommandType == MqCommandType.XaRollback;
		}

		// Token: 0x06005583 RID: 21891 RVA: 0x0015C704 File Offset: 0x0015A904
		public void ActionPassToTcp()
		{
			AsynchronousConnectionMessage dataMessageFromQueue = this.context.DataMessageFromQueue;
			this.context.DataMessageFromQueue = null;
			dataMessageFromQueue.Change((dataMessageFromQueue.SubType == 350) ? 104 : 102);
			this.automaton.SendToTcp(dataMessageFromQueue);
		}

		// Token: 0x06005584 RID: 21892 RVA: 0x0015C750 File Offset: 0x0015A950
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

		// Token: 0x06005585 RID: 21893 RVA: 0x0015C840 File Offset: 0x0015AA40
		public void ActionSetMqStatWaitEvent()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			this.automaton.ExtractAsyncStatus(passThroughData.Buffers[0]);
			passThroughData.ReturnBuffers();
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005586 RID: 21894 RVA: 0x0015C894 File Offset: 0x0015AA94
		public void ActionMakeAsyncSent()
		{
			AsynchronousConnectionMessage dataMessageFromTcp = this.context.DataMessageFromTcp;
			this.context.DataMessageFromTcp = null;
			dataMessageFromTcp.Change(204);
			this.context.MessageToQueue = dataMessageFromTcp;
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x0015C8D0 File Offset: 0x0015AAD0
		public void ActionSendMessageToQ()
		{
			this.automaton.SendToQueue(this.context.MessageToQueue);
		}

		// Token: 0x06005588 RID: 21896 RVA: 0x0015C8E8 File Offset: 0x0015AAE8
		public void ActionEnqueueOpen()
		{
			this.context.QueuedOpens.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x06005589 RID: 21897 RVA: 0x0015C91B File Offset: 0x0015AB1B
		public void ActionEnqueueClose()
		{
			this.context.QueuedCloses.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x0600558A RID: 21898 RVA: 0x0015C950 File Offset: 0x0015AB50
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

		// Token: 0x0600558B RID: 21899 RVA: 0x0015C9B7 File Offset: 0x0015ABB7
		public void ActionEnqueueDisconnect()
		{
			this.context.DisconnectRequested = true;
		}

		// Token: 0x0600558C RID: 21900 RVA: 0x0015C9C8 File Offset: 0x0015ABC8
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

		// Token: 0x0600558D RID: 21901 RVA: 0x0015CA2C File Offset: 0x0015AC2C
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

		// Token: 0x0600558E RID: 21902 RVA: 0x0015CA90 File Offset: 0x0015AC90
		public void ActionSendXaEnd()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaEnd(buffer, this.context.DeterminantForTcp);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Sending XA_END");
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x0600558F RID: 21903 RVA: 0x0015CAF8 File Offset: 0x0015ACF8
		public void ActionSendXaRollback()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaRollback(buffer, this.context.DeterminantForTcp);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Sending XA_ROLLBACK");
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x06005590 RID: 21904 RVA: 0x0015CB5F File Offset: 0x0015AD5F
		public bool PostConditionQueueFound()
		{
			return this.context.ConnectionFoundToQueue;
		}

		// Token: 0x040043BC RID: 17340
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.InTxn;

		// Token: 0x040043BD RID: 17341
		private AutomatonQueueManager automaton;

		// Token: 0x040043BE RID: 17342
		private AutomatonQueueManagerContext context;

		// Token: 0x040043BF RID: 17343
		private static string[] traceLines = new string[]
		{
			"State: InTxn, Evt: ClientData, Act: PassToTcp, Stop", "State: InTxn, Evt: ServerData, Pre: ForClient, Act: MakeClientMessage, Evt: MessageToQ", "State: InTxn, Evt: ServerData, Pre: MqStatReply, Act: SetMqStatWaitEvent, Stop", "State: InTxn, Evt: ServerData, Pre: ServerClosing, State: ServerClosing, Evt: InFailedState", "State: InTxn, Evt: AsyncSendDone, Act: MakeAsyncSent, Evt: MessageToQ", "State: InTxn, Evt: MessageToQ, Act: SendMessageToQ, Post: QueueFound, Stop", "State: InTxn, Evt: QAttach, Act: EnqueueOpen, State: OpenCloseInTxn, Evt: FirstOpen", "State: InTxn, Evt: QDetach, Pre: DoClose, Act: EnqueueClose, State: OpenCloseInTxn, Evt: FirstClose", "State: InTxn, Evt: QDetach, Act: Detached, Stop", "State: InTxn, Evt: Disconnect, Act: EnqueueDisconnect, Stop",
			"State: InTxn, Evt: MqCommand, Pre: AsyncStatus, Act: SendMqStat, Stop", "State: InTxn, Evt: MqCommand, Pre: XaEnd, Act: SendXaEnd, State: TxnEnding, Stop", "State: InTxn, Evt: MqCommand, Pre: XaRollback, Act: SendXaRollback, State: TxnFinalizing, Stop", "State: InTxn, Evt: TcpDisconnected, State: DataFlowFailed, Evt: TcpFailed", "State: InTxn, Evt: QueueDisconnected, Stop"
		};
	}
}
