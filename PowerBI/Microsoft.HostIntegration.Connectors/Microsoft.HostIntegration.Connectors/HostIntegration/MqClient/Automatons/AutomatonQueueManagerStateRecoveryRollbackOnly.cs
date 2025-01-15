using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000ABB RID: 2747
	internal class AutomatonQueueManagerStateRecoveryRollbackOnly : StateAsCodeDriver
	{
		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x060056E6 RID: 22246 RVA: 0x00163F24 File Offset: 0x00162124
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060056E7 RID: 22247 RVA: 0x00163F37 File Offset: 0x00162137
		internal AutomatonQueueManagerStateRecoveryRollbackOnly(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060056E8 RID: 22248 RVA: 0x00163F58 File Offset: 0x00162158
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
								goto IL_00F3;
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
							num = 8;
						}
					}
					else if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.Disconnect)
					{
						if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.MqCommand)
						{
							if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.XaReturnCode)
							{
								goto IL_00F3;
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
						else if (this.PreConditionXaRollback())
						{
							this.ActionSendXaRollback();
							automatonQueueManagerState = AutomatonQueueManagerState.RecoveryFinalizing;
							num = 5;
						}
						else if (this.PreConditionXaOpen())
						{
							this.ActionSetXaOpenWaitEvent();
							num = 6;
						}
						else
						{
							if (!this.PreConditionXaRecover())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							this.ActionSendXaRecover();
							num = 7;
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
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateRecoveryRollbackOnly.traceLines[num]);
						continue;
					}
					continue;
					IL_00F3:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: RecoveryRollbackOnly, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x060056E9 RID: 22249 RVA: 0x0016412C File Offset: 0x0016232C
		public bool PreConditionXaRecoverReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.XaRecoverReply;
		}

		// Token: 0x060056EA RID: 22250 RVA: 0x00164154 File Offset: 0x00162354
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x060056EB RID: 22251 RVA: 0x001641C2 File Offset: 0x001623C2
		public bool PreConditionOk()
		{
			return this.context.XaReturnCode == XaReturnCode.Ok;
		}

		// Token: 0x060056EC RID: 22252 RVA: 0x001641D2 File Offset: 0x001623D2
		public bool PreConditionXaRollback()
		{
			return this.context.CommandType == MqCommandType.XaRollback;
		}

		// Token: 0x060056ED RID: 22253 RVA: 0x001641E2 File Offset: 0x001623E2
		public bool PreConditionXaOpen()
		{
			return this.context.CommandType == MqCommandType.XaOpen;
		}

		// Token: 0x060056EE RID: 22254 RVA: 0x001641F2 File Offset: 0x001623F2
		public bool PreConditionXaRecover()
		{
			return this.context.CommandType == MqCommandType.XaRecover;
		}

		// Token: 0x060056EF RID: 22255 RVA: 0x00164204 File Offset: 0x00162404
		public void ActionExtractReturnCode()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.context.XaReturnCode = SegmentHelpers.ExtractXaRecoverReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
		}

		// Token: 0x060056F0 RID: 22256 RVA: 0x00164258 File Offset: 0x00162458
		public void ActionFillXidsSetEvent()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.automaton.ExtractXids(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
			this.context.CommandEvent.Set();
		}

		// Token: 0x060056F1 RID: 22257 RVA: 0x001642B6 File Offset: 0x001624B6
		public void ActionSetEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x060056F2 RID: 22258 RVA: 0x001642CC File Offset: 0x001624CC
		public void ActionSendXaClose()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaClose(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x060056F3 RID: 22259 RVA: 0x00164308 File Offset: 0x00162508
		public void ActionSendXaRollback()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaRollback(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x060056F4 RID: 22260 RVA: 0x001642B6 File Offset: 0x001624B6
		public void ActionSetXaOpenWaitEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x060056F5 RID: 22261 RVA: 0x00164344 File Offset: 0x00162544
		public void ActionSendXaRecover()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaRecover(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x04004414 RID: 17428
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.RecoveryRollbackOnly;

		// Token: 0x04004415 RID: 17429
		private AutomatonQueueManager automaton;

		// Token: 0x04004416 RID: 17430
		private AutomatonQueueManagerContext context;

		// Token: 0x04004417 RID: 17431
		private static string[] traceLines = new string[] { "State: RecoveryRollbackOnly, Evt: ServerData, Pre: XaRecoverReply, Act: ExtractReturnCode, Evt: XaReturnCode", "State: RecoveryRollbackOnly, Evt: ServerData, Pre: ServerClosing, State: ServerClosing, Evt: InFailedState", "State: RecoveryRollbackOnly, Evt: XaReturnCode, Pre: Ok, Act: FillXidsSetEvent, Stop", "State: RecoveryRollbackOnly, Evt: XaReturnCode, Act: SetEvent, State: DataFlowFailed, Evt: RmError", "State: RecoveryRollbackOnly, Evt: Disconnect, Act: SendXaClose, State: TxnClosing, Stop", "State: RecoveryRollbackOnly, Evt: MqCommand, Pre: XaRollback, Act: SendXaRollback, State: RecoveryFinalizing, Stop", "State: RecoveryRollbackOnly, Evt: MqCommand, Pre: XaOpen, Act: SetXaOpenWaitEvent, Stop", "State: RecoveryRollbackOnly, Evt: MqCommand, Pre: XaRecover, Act: SendXaRecover, Stop", "State: RecoveryRollbackOnly, Evt: TcpDisconnected, State: DataFlowFailed, Evt: TcpFailed" };
	}
}
