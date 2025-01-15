using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AA3 RID: 2723
	internal class AutomatonQueueManagerStateSocketAction : StateAsCodeDriver
	{
		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x0600556D RID: 21869 RVA: 0x0015BEDF File Offset: 0x0015A0DF
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600556E RID: 21870 RVA: 0x0015BEF2 File Offset: 0x0015A0F2
		internal AutomatonQueueManagerStateSocketAction(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600556F RID: 21871 RVA: 0x0015BF10 File Offset: 0x0015A110
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
							if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.SendSA)
							{
								throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
							}
							this.ActionSendSocketAction1();
							num = 0;
						}
						else
						{
							if (!this.PreConditionSocketAction8())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							automatonQueueManagerState = AutomatonQueueManagerState.ConnectingQm;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.SendConnect;
							num = 1;
						}
					}
					else
					{
						automatonQueueManagerState = AutomatonQueueManagerState.HandshakeFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 2;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateSocketAction.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: SocketAction, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x06005570 RID: 21872 RVA: 0x0015C034 File Offset: 0x0015A234
		public bool PreConditionSocketAction8()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			if (passThroughData.SegmentHeaderInformation.SegmentHeaderType != SegmentHeaderType.Tshm)
			{
				return false;
			}
			DynamicDataBuffer dynamicDataBuffer = passThroughData.Buffers[0];
			SegmentHeaderInformation segmentHeaderInformation = SegmentHelpers.ExtractSegmentHeader(dynamicDataBuffer);
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction(dynamicDataBuffer, segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.NewQmResponse;
		}

		// Token: 0x06005571 RID: 21873 RVA: 0x0015C0A4 File Offset: 0x0015A2A4
		public void ActionSendSocketAction1()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateSocketAction(buffer, SocketActionType.NewQmRequest);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x040043B4 RID: 17332
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.SocketAction;

		// Token: 0x040043B5 RID: 17333
		private AutomatonQueueManager automaton;

		// Token: 0x040043B6 RID: 17334
		private AutomatonQueueManagerContext context;

		// Token: 0x040043B7 RID: 17335
		private static string[] traceLines = new string[] { "State: SocketAction, Evt: SendSA, Act: SendSocketAction1, Stop", "State: SocketAction, Evt: ServerData, Pre: SocketAction8, State: ConnectingQm, Evt: SendConnect", "State: SocketAction, Evt: TcpDisconnected, State: HandshakeFailed, Evt: TcpFailed" };
	}
}
