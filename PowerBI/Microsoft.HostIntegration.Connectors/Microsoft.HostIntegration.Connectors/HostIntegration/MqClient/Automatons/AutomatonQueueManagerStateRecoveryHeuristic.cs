using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AB6 RID: 2742
	internal class AutomatonQueueManagerStateRecoveryHeuristic : StateAsCodeDriver
	{
		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x06005689 RID: 22153 RVA: 0x00162602 File Offset: 0x00160802
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600568A RID: 22154 RVA: 0x00162615 File Offset: 0x00160815
		internal AutomatonQueueManagerStateRecoveryHeuristic(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600568B RID: 22155 RVA: 0x00162634 File Offset: 0x00160834
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
								goto IL_0128;
							}
							if (this.PreConditionXaRecoverReply())
							{
								this.ActionExtractReturnCode();
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.XaReturnCode;
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
							num = 10;
						}
					}
					else if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.Disconnect)
					{
						if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.MqCommand)
						{
							if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.XaReturnCode)
							{
								goto IL_0128;
							}
							if (this.PreConditionOk())
							{
								this.ActionFillXidsSetEvent();
								num = 2;
							}
							else
							{
								this.ActionSetEvent();
								automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.RmError;
								num = 3;
							}
						}
						else if (this.PreConditionXaCommit())
						{
							this.ActionSendXaCommit();
							automatonQueueManagerState = AutomatonQueueManagerState.RecoveryFinalizing;
							num = 5;
						}
						else if (this.PreConditionXaRollback())
						{
							this.ActionSendXaRollback();
							automatonQueueManagerState = AutomatonQueueManagerState.RecoveryFinalizing;
							num = 6;
						}
						else if (this.PreConditionXaForget())
						{
							this.ActionSendXaForget();
							automatonQueueManagerState = AutomatonQueueManagerState.RecoveryForgetting;
							num = 7;
						}
						else if (this.PreConditionXaOpen())
						{
							this.ActionSetXaOpenWaitEvent();
							num = 8;
						}
						else
						{
							if (!this.PreConditionXaRecover())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							this.ActionSendXaRecover();
							num = 9;
						}
					}
					else
					{
						this.ActionSendXaClose();
						automatonQueueManagerState = AutomatonQueueManagerState.TxnClosing;
						num = 4;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateRecoveryHeuristic.traceLines[num]);
						continue;
					}
					continue;
					IL_0128:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: RecoveryHeuristic, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x0600568C RID: 22156 RVA: 0x0016283C File Offset: 0x00160A3C
		public bool PreConditionXaRecoverReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.XaRecoverReply;
		}

		// Token: 0x0600568D RID: 22157 RVA: 0x00162864 File Offset: 0x00160A64
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x0600568E RID: 22158 RVA: 0x001628D2 File Offset: 0x00160AD2
		public bool PreConditionOk()
		{
			return this.context.XaReturnCode == XaReturnCode.Ok;
		}

		// Token: 0x0600568F RID: 22159 RVA: 0x001628E2 File Offset: 0x00160AE2
		public bool PreConditionXaCommit()
		{
			return this.context.CommandType == MqCommandType.XaCommit;
		}

		// Token: 0x06005690 RID: 22160 RVA: 0x001628F2 File Offset: 0x00160AF2
		public bool PreConditionXaRollback()
		{
			return this.context.CommandType == MqCommandType.XaRollback;
		}

		// Token: 0x06005691 RID: 22161 RVA: 0x00162902 File Offset: 0x00160B02
		public bool PreConditionXaForget()
		{
			return this.context.CommandType == MqCommandType.XaForget;
		}

		// Token: 0x06005692 RID: 22162 RVA: 0x00162913 File Offset: 0x00160B13
		public bool PreConditionXaOpen()
		{
			return this.context.CommandType == MqCommandType.XaOpen;
		}

		// Token: 0x06005693 RID: 22163 RVA: 0x00162923 File Offset: 0x00160B23
		public bool PreConditionXaRecover()
		{
			return this.context.CommandType == MqCommandType.XaRecover;
		}

		// Token: 0x06005694 RID: 22164 RVA: 0x00162934 File Offset: 0x00160B34
		public void ActionExtractReturnCode()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.context.XaReturnCode = SegmentHelpers.ExtractXaRecoverReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
		}

		// Token: 0x06005695 RID: 22165 RVA: 0x00162988 File Offset: 0x00160B88
		public void ActionFillXidsSetEvent()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.automaton.ExtractXids(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005696 RID: 22166 RVA: 0x001629E6 File Offset: 0x00160BE6
		public void ActionSetEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005697 RID: 22167 RVA: 0x001629FC File Offset: 0x00160BFC
		public void ActionSendXaClose()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaClose(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x06005698 RID: 22168 RVA: 0x00162A38 File Offset: 0x00160C38
		public void ActionSendXaCommit()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaCommit(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x06005699 RID: 22169 RVA: 0x00162A74 File Offset: 0x00160C74
		public void ActionSendXaRollback()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaRollback(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x0600569A RID: 22170 RVA: 0x00162AB0 File Offset: 0x00160CB0
		public void ActionSendXaForget()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaForget(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x0600569B RID: 22171 RVA: 0x001629E6 File Offset: 0x00160BE6
		public void ActionSetXaOpenWaitEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x0600569C RID: 22172 RVA: 0x00162AEC File Offset: 0x00160CEC
		public void ActionSendXaRecover()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaRecover(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x04004400 RID: 17408
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.RecoveryHeuristic;

		// Token: 0x04004401 RID: 17409
		private AutomatonQueueManager automaton;

		// Token: 0x04004402 RID: 17410
		private AutomatonQueueManagerContext context;

		// Token: 0x04004403 RID: 17411
		private static string[] traceLines = new string[]
		{
			"State: RecoveryHeuristic, Evt: ServerData, Pre: XaRecoverReply, Act: ExtractReturnCode, Evt: XaReturnCode", "State: RecoveryHeuristic, Evt: ServerData, Pre: ServerClosing, State: ServerClosing, Evt: InFailedState", "State: RecoveryHeuristic, Evt: XaReturnCode, Pre: Ok, Act: FillXidsSetEvent, Stop", "State: RecoveryHeuristic, Evt: XaReturnCode, Act: SetEvent, State: DataFlowFailed, Evt: RmError", "State: RecoveryHeuristic, Evt: Disconnect, Act: SendXaClose, State: TxnClosing, Stop", "State: RecoveryHeuristic, Evt: MqCommand, Pre: XaCommit, Act: SendXaCommit, State: RecoveryFinalizing, Stop", "State: RecoveryHeuristic, Evt: MqCommand, Pre: XaRollback, Act: SendXaRollback, State: RecoveryFinalizing, Stop", "State: RecoveryHeuristic, Evt: MqCommand, Pre: XaForget, Act: SendXaForget, State: RecoveryForgetting, Stop", "State: RecoveryHeuristic, Evt: MqCommand, Pre: XaOpen, Act: SetXaOpenWaitEvent, Stop", "State: RecoveryHeuristic, Evt: MqCommand, Pre: XaRecover, Act: SendXaRecover, Stop",
			"State: RecoveryHeuristic, Evt: TcpDisconnected, State: DataFlowFailed, Evt: TcpFailed"
		};
	}
}
