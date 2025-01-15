using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.EventLogging;
using Microsoft.HostIntegration.Tracing.MqClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AD0 RID: 2768
	public sealed class AutomatonTcp : AutomatonDriverAsCode
	{
		// Token: 0x060057FC RID: 22524 RVA: 0x0016976A File Offset: 0x0016796A
		static AutomatonTcp()
		{
			AutomatonDriverAsCode.RegisterAutomaton(3, 2, 1);
		}

		// Token: 0x060057FD RID: 22525 RVA: 0x00169774 File Offset: 0x00167974
		public AutomatonTcp(ConnectionTracePoint parentTracePoint, EventLogContainer eventLogContainer)
			: base(2)
		{
			this.InternalConstructor(parentTracePoint, "Tcp", "Tcp", eventLogContainer);
		}

		// Token: 0x060057FE RID: 22526 RVA: 0x0016978F File Offset: 0x0016798F
		public AutomatonTcp(ConnectionTracePoint parentTracePoint, string instanceName, EventLogContainer eventLogContainer)
			: base(2)
		{
			this.InternalConstructor(parentTracePoint, "Tcp", instanceName, eventLogContainer);
		}

		// Token: 0x060057FF RID: 22527 RVA: 0x001697A8 File Offset: 0x001679A8
		private void InternalConstructor(ConnectionTracePoint parentTracePoint, string automatonName, string instanceName, EventLogContainer eventLogContainer)
		{
			AutomatonTcpContext automatonTcpContext = new AutomatonTcpContext();
			List<StateAsCodeDriver> list = new List<StateAsCodeDriver>();
			list.Add(new AutomatonTcpStateUnConnected(this, automatonTcpContext));
			list.Add(new AutomatonTcpStateFailedConnect(this, automatonTcpContext));
			list.Add(new AutomatonTcpStateDoSslHandshake(this, automatonTcpContext));
			list.Add(new AutomatonTcpStateDataTransfer(this, automatonTcpContext));
			list.Add(new AutomatonTcpStateDataTransferSsl(this, automatonTcpContext));
			list.Add(new AutomatonTcpStateFailedData(this, automatonTcpContext));
			TcpAutomatonTracePoint tcpAutomatonTracePoint = new TcpAutomatonTracePoint(parentTracePoint);
			base.Automaton = new AutomatonDefinition(automatonName, instanceName, automatonTcpContext, list, tcpAutomatonTracePoint);
			AutomatonTcpContext automatonTcpContext2 = base.Automaton.Context as AutomatonTcpContext;
			automatonTcpContext2.TracePoint = tcpAutomatonTracePoint;
			automatonTcpContext2.ParentTracePoint = parentTracePoint;
			automatonTcpContext2.AutomatonEventLog = eventLogContainer;
			base.AddConnections(0, 0, new ProcessAsynchronousMessage(this.ProcessMessageFromQueueManager), new ProcessDisconnect(this.ProcessQueueManagerDisconnect), -1);
		}

		// Token: 0x06005800 RID: 22528 RVA: 0x00155786 File Offset: 0x00153986
		public void ProcessEvent(AutomatonTcpEvent eventToProcess)
		{
			base.ProcessEvent((int)eventToProcess);
		}

		// Token: 0x06005801 RID: 22529 RVA: 0x0016986C File Offset: 0x00167A6C
		public static int GetDeterminantToQueueManager(AsynchronousConnectionMessage message)
		{
			return (message.Contents as DeterminantMessage).QueueManagerDeterminant;
		}

		// Token: 0x06005802 RID: 22530 RVA: 0x0016987E File Offset: 0x00167A7E
		public void BroadcastToQueueManager(AsynchronousConnectionMessage message)
		{
			base.Send(0, message, -1);
		}

		// Token: 0x06005803 RID: 22531 RVA: 0x0016988C File Offset: 0x00167A8C
		public void SendToQueueManager(AsynchronousConnectionMessage message)
		{
			AutomatonTcpContext automatonTcpContext = base.Automaton.Context as AutomatonTcpContext;
			int determinantToQueueManager = AutomatonTcp.GetDeterminantToQueueManager(message);
			automatonTcpContext.ConnectionFoundToQueueManager = base.Send(0, message, determinantToQueueManager);
		}

		// Token: 0x06005804 RID: 22532 RVA: 0x001698BE File Offset: 0x00167ABE
		public void SendToQueueManager(int type, object contents)
		{
			this.SendToQueueManager(new AsynchronousConnectionMessage(type, contents));
		}

		// Token: 0x06005805 RID: 22533 RVA: 0x001698D0 File Offset: 0x00167AD0
		public void ProcessMessageFromQueueManager(AsynchronousConnectionMessage message)
		{
			AutomatonTcpContext automatonTcpContext = base.Automaton.Context as AutomatonTcpContext;
			switch (message.Type)
			{
			case 100:
				automatonTcpContext.Attach = message;
				this.ProcessEvent(AutomatonTcpEvent.QmAttach);
				return;
			case 101:
				automatonTcpContext.Detach = message;
				this.ProcessEvent(AutomatonTcpEvent.QmDetach);
				return;
			case 102:
				automatonTcpContext.MessageFromQmAutomaton = message;
				this.ProcessEvent(AutomatonTcpEvent.DataToSend);
				return;
			case 103:
				automatonTcpContext.ChannelParameters = (message.Contents as QmInitialDataInfo).ChannelParameters;
				this.ProcessEvent(AutomatonTcpEvent.InitialDataInfo);
				return;
			case 104:
				automatonTcpContext.MessageFromQmAutomaton = message;
				this.ProcessEvent(AutomatonTcpEvent.AsyncDataToSend);
				return;
			default:
				throw new InvalidOperationException("Unknown Message Type");
			}
		}

		// Token: 0x06005806 RID: 22534 RVA: 0x000036A9 File Offset: 0x000018A9
		public void ProcessQueueManagerDisconnect()
		{
		}

		// Token: 0x06005807 RID: 22535 RVA: 0x0016997C File Offset: 0x00167B7C
		public void ProcessAsynchronousRead(IAsyncResult ar)
		{
			AutomatonTcpContext automatonTcpContext = base.Automaton.Context as AutomatonTcpContext;
			AutomatonDefinition automaton = base.Automaton;
			lock (automaton)
			{
				if (automatonTcpContext.TcpClient != null)
				{
					automatonTcpContext.AsynchronousResult = ar;
					this.ProcessEvent(AutomatonTcpEvent.DataReceived);
				}
			}
		}

		// Token: 0x06005808 RID: 22536 RVA: 0x001699E0 File Offset: 0x00167BE0
		public void StartTimers()
		{
			AutomatonTcpContext automatonTcpContext = base.Automaton.Context as AutomatonTcpContext;
			int num = automatonTcpContext.ChannelParameters.HeartBeatInterval + 5;
			automatonTcpContext.HeartbeatTimespan = new TimeSpan((long)num * 10000000L);
			automatonTcpContext.LastSentTime = DateTime.Now;
			automatonTcpContext.LastReceivedTime = DateTime.Now;
			automatonTcpContext.SendTimer = new Timer(new TimerCallback(this.SendTimerProcedure), null, num * 1000, -1);
			automatonTcpContext.ReceiveTimer = new Timer(new TimerCallback(this.ReceiveTimerProcedure), null, num * 1000, -1);
		}

		// Token: 0x06005809 RID: 22537 RVA: 0x00169A74 File Offset: 0x00167C74
		private void SendTimerProcedure(object state)
		{
			this.ProcessEvent(AutomatonTcpEvent.SendTimer);
		}

		// Token: 0x0600580A RID: 22538 RVA: 0x00169A7E File Offset: 0x00167C7E
		private void ReceiveTimerProcedure(object state)
		{
			this.ProcessEvent(AutomatonTcpEvent.ReceiveTimer);
		}

		// Token: 0x0600580B RID: 22539 RVA: 0x00169A88 File Offset: 0x00167C88
		internal void SendDataToQueueManager()
		{
			AutomatonTcpContext automatonTcpContext = base.Automaton.Context as AutomatonTcpContext;
			SegmentHeaderInformation segmentHeaderInformation = SegmentHelpers.ExtractSegmentHeader(automatonTcpContext.DataToQmAutomaton);
			SegmentType segmentType = segmentHeaderInformation.SegmentType;
			if (segmentType == SegmentType.Heartbeat)
			{
				return;
			}
			int num = 1;
			int num2 = 0;
			if (segmentHeaderInformation.SegmentHeaderType == SegmentHeaderType.Tshm)
			{
				num = segmentHeaderInformation.ConversationId;
			}
			ApiHeaderInformation apiHeaderInformation = null;
			if (segmentType <= SegmentType.StatusData)
			{
				if (segmentType == SegmentType.InitialData || segmentType == SegmentType.StatusData)
				{
					goto IL_017D;
				}
			}
			else
			{
				switch (segmentType)
				{
				case SegmentType.Heartbeat:
					goto IL_017D;
				case SegmentType.ConAuth:
				case (SegmentType)11:
				case SegmentType.RequestMessages:
					break;
				case SegmentType.SocketAction:
					if (segmentHeaderInformation.SegmentHeaderType == SegmentHeaderType.Tshc)
					{
						num = ConversionHelpers.ExtractIntFromBuffer(automatonTcpContext.DataToQmAutomaton.Data, segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
						goto IL_017D;
					}
					goto IL_017D;
				case SegmentType.AsyncMessage:
				case SegmentType.Notification:
					num2 = ConversionHelpers.ExtractIntFromBuffer(automatonTcpContext.DataToQmAutomaton.Data, segmentHeaderInformation.LengthOfSegmentHeader + 4, segmentHeaderInformation.LittleEndian);
					goto IL_017D;
				default:
					switch (segmentType)
					{
					case SegmentType.MqConnectReply:
					case SegmentType.MqDisconnectReply:
					case SegmentType.MqOpenReply:
					case SegmentType.MqCloseReply:
					case SegmentType.MqPutReply:
					case SegmentType.MqStatReply:
						apiHeaderInformation = SegmentHelpers.ExtractApiHeader(automatonTcpContext.DataToQmAutomaton, segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
						num2 = apiHeaderInformation.ObjectHandle;
						goto IL_017D;
					case SegmentType.MqGetReply:
						if ((segmentHeaderInformation.ControlFlag1 & ControlFlag1.FirstSegment) == ControlFlag1.FirstSegment)
						{
							apiHeaderInformation = SegmentHelpers.ExtractApiHeader(automatonTcpContext.DataToQmAutomaton, segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
							num2 = apiHeaderInformation.ObjectHandle;
							goto IL_017D;
						}
						goto IL_017D;
					case (SegmentType)151:
					case (SegmentType)152:
					case (SegmentType)153:
					case (SegmentType)154:
					case (SegmentType)155:
					case (SegmentType)156:
						break;
					default:
						if (segmentType - SegmentType.XaStartReply <= 8)
						{
							goto IL_017D;
						}
						break;
					}
					break;
				}
			}
			throw new InvalidOperationException("Unknown Segment");
			IL_017D:
			PassThroughData passThroughData = new PassThroughData();
			passThroughData.SegmentHeaderInformation = segmentHeaderInformation;
			passThroughData.ApiHeaderInformation = apiHeaderInformation;
			passThroughData.QueueDeterminant = num2;
			passThroughData.QueueManagerDeterminant = num;
			passThroughData.Buffers = new List<DynamicDataBuffer> { automatonTcpContext.DataToQmAutomaton };
			automatonTcpContext.DataToQmAutomaton = null;
			this.SendToQueueManager(403, passThroughData);
		}
	}
}
