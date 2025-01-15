using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A99 RID: 2713
	internal class AutomatonQueueManagerStateConnectingQm : StateAsCodeDriver
	{
		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x060054DC RID: 21724 RVA: 0x00158071 File Offset: 0x00156271
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060054DD RID: 21725 RVA: 0x00158084 File Offset: 0x00156284
		internal AutomatonQueueManagerStateConnectingQm(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x001580A4 File Offset: 0x001562A4
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
							if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.SendConnect)
							{
								throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
							}
							this.ActionSendMqConn();
							num = 0;
						}
						else if (this.PreConditionMqConnReplyOkTxn())
						{
							this.ActionSendXaOpen();
							automatonQueueManagerState = AutomatonQueueManagerState.TxnOpening;
							num = 1;
						}
						else if (this.PreConditionMqConnReplyOk())
						{
							this.ActionSetWaitConnectEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.DataFlow;
							num = 2;
						}
						else
						{
							if (!this.PreConditionMqConnReply())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							this.ActionSendSocketAction2();
							automatonQueueManagerState = AutomatonQueueManagerState.HandshakeFailed;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.QmConnError;
							num = 3;
						}
					}
					else
					{
						automatonQueueManagerState = AutomatonQueueManagerState.HandshakeFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 4;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateConnectingQm.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: ConnectingQm, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x060054DF RID: 21727 RVA: 0x001581F4 File Offset: 0x001563F4
		public bool PreConditionMqConnReplyOkTxn()
		{
			return this.context.ConnectionParameters.IsTransactional && this.PreConditionMqConnReplyOk();
		}

		// Token: 0x060054E0 RID: 21728 RVA: 0x00158210 File Offset: 0x00156410
		public bool PreConditionMqConnReplyOk()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			if (passThroughData.SegmentHeaderInformation.SegmentType != SegmentType.MqConnectReply)
			{
				return false;
			}
			ApiHeaderInformation apiHeaderInformation = passThroughData.ApiHeaderInformation;
			this.context.ConnectReturnCode = apiHeaderInformation.ReasonCode;
			if (apiHeaderInformation.CompletionCode == CompletionCode.Ok)
			{
				this.context.DataMessageFromTcp = null;
				passThroughData.ReturnBuffers();
				return true;
			}
			return false;
		}

		// Token: 0x060054E1 RID: 21729 RVA: 0x0015827C File Offset: 0x0015647C
		public bool PreConditionMqConnReply()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.context.DataMessageFromTcp = null;
			passThroughData.ReturnBuffers();
			return segmentHeaderInformation.SegmentType == SegmentType.MqConnectReply;
		}

		// Token: 0x060054E2 RID: 21730 RVA: 0x001582C4 File Offset: 0x001564C4
		public void ActionSendXaOpen()
		{
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Opening the XA transaction", Array.Empty<object>()));
			}
			if (this.context.ConnectionParameters.ResourceManagerId != 0)
			{
				this.context.ResourceManagerId = this.context.ConnectionParameters.ResourceManagerId;
			}
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaOpen(buffer, this.context.DeterminantForTcp);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x060054E3 RID: 21731 RVA: 0x00158368 File Offset: 0x00156568
		public void ActionSendMqConn()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateMqConnect(buffer, this.context.DeterminantForTcp);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Sending MQ Connect");
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x060054E4 RID: 21732 RVA: 0x001583CF File Offset: 0x001565CF
		public void ActionSetWaitConnectEvent()
		{
			this.context.ConnectedEvent.Set();
		}

		// Token: 0x060054E5 RID: 21733 RVA: 0x001583E4 File Offset: 0x001565E4
		public void ActionSendSocketAction2()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateSocketAction(buffer, SocketActionType.QmDisconnected);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x0400438C RID: 17292
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.ConnectingQm;

		// Token: 0x0400438D RID: 17293
		private AutomatonQueueManager automaton;

		// Token: 0x0400438E RID: 17294
		private AutomatonQueueManagerContext context;

		// Token: 0x0400438F RID: 17295
		private static string[] traceLines = new string[] { "State: ConnectingQm, Evt: SendConnect, Act: SendMqConn, Stop", "State: ConnectingQm, Evt: ServerData, Pre: MqConnReplyOkTxn, Act: SendXaOpen, State: TxnOpening, Stop", "State: ConnectingQm, Evt: ServerData, Pre: MqConnReplyOk, Act: SetWaitConnectEvent, State: DataFlow, Stop", "State: ConnectingQm, Evt: ServerData, Pre: MqConnReply, Act: SendSocketAction2, State: HandshakeFailed, Evt: QmConnError", "State: ConnectingQm, Evt: TcpDisconnected, State: HandshakeFailed, Evt: TcpFailed" };
	}
}
