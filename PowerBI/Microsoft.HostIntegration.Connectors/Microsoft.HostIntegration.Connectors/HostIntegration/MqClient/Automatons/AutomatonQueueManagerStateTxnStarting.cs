using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AB1 RID: 2737
	internal class AutomatonQueueManagerStateTxnStarting : StateAsCodeDriver
	{
		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x06005645 RID: 22085 RVA: 0x001612B6 File Offset: 0x0015F4B6
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005646 RID: 22086 RVA: 0x001612C9 File Offset: 0x0015F4C9
		internal AutomatonQueueManagerStateTxnStarting(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005647 RID: 22087 RVA: 0x001612E8 File Offset: 0x0015F4E8
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
							if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.QueueDisconnected)
							{
								throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
							}
							num = 3;
						}
						else if (this.PreConditionXaStartReplyOk())
						{
							this.ActionSetXaStartWaitEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.InTxn;
							num = 0;
						}
						else
						{
							if (!this.PreConditionServerClosing())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							this.ActionXaStartClosingEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.ServerClosing;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.InFailedState;
							num = 1;
						}
					}
					else
					{
						this.ActionXaStartDisconnectedEvent();
						automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 2;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateTxnStarting.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: TxnStarting, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x06005648 RID: 22088 RVA: 0x00161428 File Offset: 0x0015F628
		public bool PreConditionXaStartReplyOk()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			if (segmentHeaderInformation.SegmentType != SegmentType.XaStartReply)
			{
				return false;
			}
			if (SegmentHelpers.ExtractXaStartReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian) == XaReturnCode.Ok)
			{
				this.context.DataMessageFromTcp = null;
				passThroughData.ReturnBuffers();
				return true;
			}
			return false;
		}

		// Token: 0x06005649 RID: 22089 RVA: 0x00161498 File Offset: 0x0015F698
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x0600564A RID: 22090 RVA: 0x00161506 File Offset: 0x0015F706
		public void ActionSetXaStartWaitEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x0600564B RID: 22091 RVA: 0x00161519 File Offset: 0x0015F719
		public void ActionXaStartClosingEvent()
		{
			this.context.CommandReturnCode = ReturnCode.XaStartClosing;
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x0600564C RID: 22092 RVA: 0x00161549 File Offset: 0x0015F749
		public void ActionXaStartDisconnectedEvent()
		{
			this.context.CommandReturnCode = ReturnCode.XaStartDisconnecting;
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x040043EC RID: 17388
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.TxnStarting;

		// Token: 0x040043ED RID: 17389
		private AutomatonQueueManager automaton;

		// Token: 0x040043EE RID: 17390
		private AutomatonQueueManagerContext context;

		// Token: 0x040043EF RID: 17391
		private static string[] traceLines = new string[] { "State: TxnStarting, Evt: ServerData, Pre: XaStartReplyOk, Act: SetXaStartWaitEvent, State: InTxn, Stop", "State: TxnStarting, Evt: ServerData, Pre: ServerClosing, Act: XaStartClosingEvent, State: ServerClosing, Evt: InFailedState", "State: TxnStarting, Evt: TcpDisconnected, Act: XaStartDisconnectedEvent, State: DataFlowFailed, Evt: TcpFailed", "State: TxnStarting, Evt: QueueDisconnected, Stop" };
	}
}
