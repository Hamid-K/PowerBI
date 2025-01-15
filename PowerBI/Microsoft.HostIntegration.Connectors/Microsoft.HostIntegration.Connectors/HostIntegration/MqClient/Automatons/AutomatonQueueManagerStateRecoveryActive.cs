using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AB2 RID: 2738
	internal class AutomatonQueueManagerStateRecoveryActive : StateAsCodeDriver
	{
		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x0600564E RID: 22094 RVA: 0x001615A6 File Offset: 0x0015F7A6
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600564F RID: 22095 RVA: 0x001615B9 File Offset: 0x0015F7B9
		internal AutomatonQueueManagerStateRecoveryActive(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005650 RID: 22096 RVA: 0x001615D8 File Offset: 0x0015F7D8
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
						else if (this.PreConditionXaEnd())
						{
							this.ActionSendXaEnd();
							automatonQueueManagerState = AutomatonQueueManagerState.RecoveryEnding;
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
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateRecoveryActive.traceLines[num]);
						continue;
					}
					continue;
					IL_00F3:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: RecoveryActive, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x06005651 RID: 22097 RVA: 0x001617AC File Offset: 0x0015F9AC
		public bool PreConditionXaRecoverReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.XaRecoverReply;
		}

		// Token: 0x06005652 RID: 22098 RVA: 0x001617D4 File Offset: 0x0015F9D4
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x06005653 RID: 22099 RVA: 0x00161842 File Offset: 0x0015FA42
		public bool PreConditionOk()
		{
			return this.context.XaReturnCode == XaReturnCode.Ok;
		}

		// Token: 0x06005654 RID: 22100 RVA: 0x00161852 File Offset: 0x0015FA52
		public bool PreConditionXaEnd()
		{
			return this.context.CommandType == MqCommandType.XaEnd;
		}

		// Token: 0x06005655 RID: 22101 RVA: 0x00161862 File Offset: 0x0015FA62
		public bool PreConditionXaOpen()
		{
			return this.context.CommandType == MqCommandType.XaOpen;
		}

		// Token: 0x06005656 RID: 22102 RVA: 0x00161872 File Offset: 0x0015FA72
		public bool PreConditionXaRecover()
		{
			return this.context.CommandType == MqCommandType.XaRecover;
		}

		// Token: 0x06005657 RID: 22103 RVA: 0x00161884 File Offset: 0x0015FA84
		public void ActionExtractReturnCode()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.context.XaReturnCode = SegmentHelpers.ExtractXaRecoverReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
		}

		// Token: 0x06005658 RID: 22104 RVA: 0x001618D8 File Offset: 0x0015FAD8
		public void ActionFillXidsSetEvent()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.automaton.ExtractXids(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005659 RID: 22105 RVA: 0x00161936 File Offset: 0x0015FB36
		public void ActionSetEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x0600565A RID: 22106 RVA: 0x0016194C File Offset: 0x0015FB4C
		public void ActionSendXaClose()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaClose(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x0600565B RID: 22107 RVA: 0x00161988 File Offset: 0x0015FB88
		public void ActionSendXaEnd()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaEnd(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x0600565C RID: 22108 RVA: 0x00161936 File Offset: 0x0015FB36
		public void ActionSetXaOpenWaitEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x0600565D RID: 22109 RVA: 0x001619C4 File Offset: 0x0015FBC4
		public void ActionSendXaRecover()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaRecover(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x040043F0 RID: 17392
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.RecoveryActive;

		// Token: 0x040043F1 RID: 17393
		private AutomatonQueueManager automaton;

		// Token: 0x040043F2 RID: 17394
		private AutomatonQueueManagerContext context;

		// Token: 0x040043F3 RID: 17395
		private static string[] traceLines = new string[] { "State: RecoveryActive, Evt: ServerData, Pre: XaRecoverReply, Act: ExtractReturnCode, Evt: XaReturnCode", "State: RecoveryActive, Evt: ServerData, Pre: ServerClosing, State: ServerClosing, Evt: InFailedState", "State: RecoveryActive, Evt: XaReturnCode, Pre: Ok, Act: FillXidsSetEvent, Stop", "State: RecoveryActive, Evt: XaReturnCode, Act: SetEvent, State: DataFlowFailed, Evt: RmError", "State: RecoveryActive, Evt: Disconnect, Act: SendXaClose, State: TxnClosing, Stop", "State: RecoveryActive, Evt: MqCommand, Pre: XaEnd, Act: SendXaEnd, State: RecoveryEnding, Stop", "State: RecoveryActive, Evt: MqCommand, Pre: XaOpen, Act: SetXaOpenWaitEvent, Stop", "State: RecoveryActive, Evt: MqCommand, Pre: XaRecover, Act: SendXaRecover, Stop", "State: RecoveryActive, Evt: TcpDisconnected, State: DataFlowFailed, Evt: TcpFailed" };
	}
}
