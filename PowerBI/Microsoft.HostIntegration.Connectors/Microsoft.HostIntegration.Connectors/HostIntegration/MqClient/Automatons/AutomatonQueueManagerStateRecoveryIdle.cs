using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AB7 RID: 2743
	internal class AutomatonQueueManagerStateRecoveryIdle : StateAsCodeDriver
	{
		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x0600569E RID: 22174 RVA: 0x00162B9B File Offset: 0x00160D9B
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600569F RID: 22175 RVA: 0x00162BAE File Offset: 0x00160DAE
		internal AutomatonQueueManagerStateRecoveryIdle(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060056A0 RID: 22176 RVA: 0x00162BD0 File Offset: 0x00160DD0
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
								goto IL_0141;
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
							num = 11;
						}
					}
					else if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.Disconnect)
					{
						if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.MqCommand)
						{
							if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.XaReturnCode)
							{
								goto IL_0141;
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
						else if (this.PreConditionXaStart())
						{
							this.ActionSendXaStart();
							automatonQueueManagerState = AutomatonQueueManagerState.RecoveryStarting;
							num = 5;
						}
						else if (this.PreConditionXaPrepare())
						{
							this.ActionSendXaPrepare();
							automatonQueueManagerState = AutomatonQueueManagerState.RecoveryPreparing;
							num = 6;
						}
						else if (this.PreConditionXaCommit())
						{
							this.ActionSendXaCommit();
							automatonQueueManagerState = AutomatonQueueManagerState.RecoveryFinalizing;
							num = 7;
						}
						else if (this.PreConditionXaRollback())
						{
							this.ActionSendXaRollback();
							automatonQueueManagerState = AutomatonQueueManagerState.RecoveryFinalizing;
							num = 8;
						}
						else if (this.PreConditionXaOpen())
						{
							this.ActionSetXaOpenWaitEvent();
							num = 9;
						}
						else
						{
							if (!this.PreConditionXaRecover())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							this.ActionSendXaRecover();
							num = 10;
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
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateRecoveryIdle.traceLines[num]);
						continue;
					}
					continue;
					IL_0141:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: RecoveryIdle, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x060056A1 RID: 22177 RVA: 0x00162DF0 File Offset: 0x00160FF0
		public bool PreConditionXaRecoverReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.XaRecoverReply;
		}

		// Token: 0x060056A2 RID: 22178 RVA: 0x00162E18 File Offset: 0x00161018
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x060056A3 RID: 22179 RVA: 0x00162E86 File Offset: 0x00161086
		public bool PreConditionOk()
		{
			return this.context.XaReturnCode == XaReturnCode.Ok;
		}

		// Token: 0x060056A4 RID: 22180 RVA: 0x00162E96 File Offset: 0x00161096
		public bool PreConditionXaStart()
		{
			return this.context.CommandType == MqCommandType.XaStart;
		}

		// Token: 0x060056A5 RID: 22181 RVA: 0x00162EA6 File Offset: 0x001610A6
		public bool PreConditionXaPrepare()
		{
			return this.context.CommandType == MqCommandType.XaPrepare;
		}

		// Token: 0x060056A6 RID: 22182 RVA: 0x00162EB6 File Offset: 0x001610B6
		public bool PreConditionXaCommit()
		{
			return this.context.CommandType == MqCommandType.XaCommit;
		}

		// Token: 0x060056A7 RID: 22183 RVA: 0x00162EC6 File Offset: 0x001610C6
		public bool PreConditionXaRollback()
		{
			return this.context.CommandType == MqCommandType.XaRollback;
		}

		// Token: 0x060056A8 RID: 22184 RVA: 0x00162ED6 File Offset: 0x001610D6
		public bool PreConditionXaOpen()
		{
			return this.context.CommandType == MqCommandType.XaOpen;
		}

		// Token: 0x060056A9 RID: 22185 RVA: 0x00162EE6 File Offset: 0x001610E6
		public bool PreConditionXaRecover()
		{
			return this.context.CommandType == MqCommandType.XaRecover;
		}

		// Token: 0x060056AA RID: 22186 RVA: 0x00162EF8 File Offset: 0x001610F8
		public void ActionExtractReturnCode()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.context.XaReturnCode = SegmentHelpers.ExtractXaRecoverReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
		}

		// Token: 0x060056AB RID: 22187 RVA: 0x00162F4C File Offset: 0x0016114C
		public void ActionFillXidsSetEvent()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.automaton.ExtractXids(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
			this.context.CommandEvent.Set();
		}

		// Token: 0x060056AC RID: 22188 RVA: 0x00162FAA File Offset: 0x001611AA
		public void ActionSetEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x060056AD RID: 22189 RVA: 0x00162FC0 File Offset: 0x001611C0
		public void ActionSendXaClose()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaClose(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x060056AE RID: 22190 RVA: 0x00162FFC File Offset: 0x001611FC
		public void ActionSendXaStart()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaStart(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x060056AF RID: 22191 RVA: 0x00163038 File Offset: 0x00161238
		public void ActionSendXaPrepare()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaPrepare(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x060056B0 RID: 22192 RVA: 0x00163074 File Offset: 0x00161274
		public void ActionSendXaCommit()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaCommit(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x060056B1 RID: 22193 RVA: 0x001630B0 File Offset: 0x001612B0
		public void ActionSendXaRollback()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaRollback(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x060056B2 RID: 22194 RVA: 0x00162FAA File Offset: 0x001611AA
		public void ActionSetXaOpenWaitEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x060056B3 RID: 22195 RVA: 0x001630EC File Offset: 0x001612EC
		public void ActionSendXaRecover()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaRecover(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x04004404 RID: 17412
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.RecoveryIdle;

		// Token: 0x04004405 RID: 17413
		private AutomatonQueueManager automaton;

		// Token: 0x04004406 RID: 17414
		private AutomatonQueueManagerContext context;

		// Token: 0x04004407 RID: 17415
		private static string[] traceLines = new string[]
		{
			"State: RecoveryIdle, Evt: ServerData, Pre: XaRecoverReply, Act: ExtractReturnCode, Evt: XaReturnCode", "State: RecoveryIdle, Evt: ServerData, Pre: ServerClosing, State: ServerClosing, Evt: InFailedState", "State: RecoveryIdle, Evt: XaReturnCode, Pre: Ok, Act: FillXidsSetEvent, Stop", "State: RecoveryIdle, Evt: XaReturnCode, Act: SetEvent, State: DataFlowFailed, Evt: RmError", "State: RecoveryIdle, Evt: Disconnect, Act: SendXaClose, State: TxnClosing, Stop", "State: RecoveryIdle, Evt: MqCommand, Pre: XaStart, Act: SendXaStart, State: RecoveryStarting, Stop", "State: RecoveryIdle, Evt: MqCommand, Pre: XaPrepare, Act: SendXaPrepare, State: RecoveryPreparing, Stop", "State: RecoveryIdle, Evt: MqCommand, Pre: XaCommit, Act: SendXaCommit, State: RecoveryFinalizing, Stop", "State: RecoveryIdle, Evt: MqCommand, Pre: XaRollback, Act: SendXaRollback, State: RecoveryFinalizing, Stop", "State: RecoveryIdle, Evt: MqCommand, Pre: XaOpen, Act: SetXaOpenWaitEvent, Stop",
			"State: RecoveryIdle, Evt: MqCommand, Pre: XaRecover, Act: SendXaRecover, Stop", "State: RecoveryIdle, Evt: TcpDisconnected, State: DataFlowFailed, Evt: TcpFailed"
		};
	}
}
