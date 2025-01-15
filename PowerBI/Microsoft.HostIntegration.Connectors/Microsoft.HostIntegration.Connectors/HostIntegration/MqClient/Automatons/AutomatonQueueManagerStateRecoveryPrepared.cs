using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AB9 RID: 2745
	internal class AutomatonQueueManagerStateRecoveryPrepared : StateAsCodeDriver
	{
		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x060056C6 RID: 22214 RVA: 0x00163661 File Offset: 0x00161861
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060056C7 RID: 22215 RVA: 0x00163674 File Offset: 0x00161874
		internal AutomatonQueueManagerStateRecoveryPrepared(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060056C8 RID: 22216 RVA: 0x00163694 File Offset: 0x00161894
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
								goto IL_010C;
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
							num = 9;
						}
					}
					else if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.Disconnect)
					{
						if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.MqCommand)
						{
							if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.XaReturnCode)
							{
								goto IL_010C;
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
						else if (this.PreConditionXaOpen())
						{
							this.ActionSetXaOpenWaitEvent();
							num = 7;
						}
						else
						{
							if (!this.PreConditionXaRecover())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							this.ActionSendXaRecover();
							num = 8;
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
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateRecoveryPrepared.traceLines[num]);
						continue;
					}
					continue;
					IL_010C:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: RecoveryPrepared, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x060056C9 RID: 22217 RVA: 0x00163880 File Offset: 0x00161A80
		public bool PreConditionXaRecoverReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.XaRecoverReply;
		}

		// Token: 0x060056CA RID: 22218 RVA: 0x001638A8 File Offset: 0x00161AA8
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x060056CB RID: 22219 RVA: 0x00163916 File Offset: 0x00161B16
		public bool PreConditionOk()
		{
			return this.context.XaReturnCode == XaReturnCode.Ok;
		}

		// Token: 0x060056CC RID: 22220 RVA: 0x00163926 File Offset: 0x00161B26
		public bool PreConditionXaCommit()
		{
			return this.context.CommandType == MqCommandType.XaCommit;
		}

		// Token: 0x060056CD RID: 22221 RVA: 0x00163936 File Offset: 0x00161B36
		public bool PreConditionXaRollback()
		{
			return this.context.CommandType == MqCommandType.XaRollback;
		}

		// Token: 0x060056CE RID: 22222 RVA: 0x00163946 File Offset: 0x00161B46
		public bool PreConditionXaOpen()
		{
			return this.context.CommandType == MqCommandType.XaOpen;
		}

		// Token: 0x060056CF RID: 22223 RVA: 0x00163956 File Offset: 0x00161B56
		public bool PreConditionXaRecover()
		{
			return this.context.CommandType == MqCommandType.XaRecover;
		}

		// Token: 0x060056D0 RID: 22224 RVA: 0x00163968 File Offset: 0x00161B68
		public void ActionExtractReturnCode()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.context.XaReturnCode = SegmentHelpers.ExtractXaRecoverReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
		}

		// Token: 0x060056D1 RID: 22225 RVA: 0x001639BC File Offset: 0x00161BBC
		public void ActionFillXidsSetEvent()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.automaton.ExtractXids(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
			this.context.CommandEvent.Set();
		}

		// Token: 0x060056D2 RID: 22226 RVA: 0x00163A1A File Offset: 0x00161C1A
		public void ActionSetEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x060056D3 RID: 22227 RVA: 0x00163A30 File Offset: 0x00161C30
		public void ActionSendXaClose()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaClose(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x060056D4 RID: 22228 RVA: 0x00163A6C File Offset: 0x00161C6C
		public void ActionSendXaCommit()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaCommit(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x060056D5 RID: 22229 RVA: 0x00163AA8 File Offset: 0x00161CA8
		public void ActionSendXaRollback()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaRollback(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x060056D6 RID: 22230 RVA: 0x00163A1A File Offset: 0x00161C1A
		public void ActionSetXaOpenWaitEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x060056D7 RID: 22231 RVA: 0x00163AE4 File Offset: 0x00161CE4
		public void ActionSendXaRecover()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaRecover(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x0400440C RID: 17420
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.RecoveryPrepared;

		// Token: 0x0400440D RID: 17421
		private AutomatonQueueManager automaton;

		// Token: 0x0400440E RID: 17422
		private AutomatonQueueManagerContext context;

		// Token: 0x0400440F RID: 17423
		private static string[] traceLines = new string[] { "State: RecoveryPrepared, Evt: ServerData, Pre: XaRecoverReply, Act: ExtractReturnCode, Evt: XaReturnCode", "State: RecoveryPrepared, Evt: ServerData, Pre: ServerClosing, State: ServerClosing, Evt: InFailedState", "State: RecoveryPrepared, Evt: XaReturnCode, Pre: Ok, Act: FillXidsSetEvent, Stop", "State: RecoveryPrepared, Evt: XaReturnCode, Act: SetEvent, State: DataFlowFailed, Evt: RmError", "State: RecoveryPrepared, Evt: Disconnect, Act: SendXaClose, State: TxnClosing, Stop", "State: RecoveryPrepared, Evt: MqCommand, Pre: XaCommit, Act: SendXaCommit, State: RecoveryFinalizing, Stop", "State: RecoveryPrepared, Evt: MqCommand, Pre: XaRollback, Act: SendXaRollback, State: RecoveryFinalizing, Stop", "State: RecoveryPrepared, Evt: MqCommand, Pre: XaOpen, Act: SetXaOpenWaitEvent, Stop", "State: RecoveryPrepared, Evt: MqCommand, Pre: XaRecover, Act: SendXaRecover, Stop", "State: RecoveryPrepared, Evt: TcpDisconnected, State: DataFlowFailed, Evt: TcpFailed" };
	}
}
