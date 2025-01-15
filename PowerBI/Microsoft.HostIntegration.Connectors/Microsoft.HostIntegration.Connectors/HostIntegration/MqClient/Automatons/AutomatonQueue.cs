using System;
using System.Collections.Generic;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.EventLogging;
using Microsoft.HostIntegration.Tracing.MqClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A92 RID: 2706
	public sealed class AutomatonQueue : AutomatonDriverAsCode
	{
		// Token: 0x060053F5 RID: 21493 RVA: 0x00155610 File Offset: 0x00153810
		static AutomatonQueue()
		{
			AutomatonDriverAsCode.RegisterAutomaton(3, 0, 1);
		}

		// Token: 0x060053F6 RID: 21494 RVA: 0x0015561A File Offset: 0x0015381A
		public AutomatonQueue(QueueTracePoint parentTracePoint, EventLogContainer eventLogContainer)
			: base(0)
		{
			this.InternalConstructor(parentTracePoint, "Queue", "Queue", eventLogContainer);
		}

		// Token: 0x060053F7 RID: 21495 RVA: 0x00155635 File Offset: 0x00153835
		public AutomatonQueue(QueueTracePoint parentTracePoint, string instanceName, EventLogContainer eventLogContainer)
			: base(0)
		{
			this.InternalConstructor(parentTracePoint, "Queue", instanceName, eventLogContainer);
		}

		// Token: 0x060053F8 RID: 21496 RVA: 0x0015564C File Offset: 0x0015384C
		private void InternalConstructor(QueueTracePoint parentTracePoint, string automatonName, string instanceName, EventLogContainer eventLogContainer)
		{
			AutomatonQueueContext automatonQueueContext = new AutomatonQueueContext();
			List<StateAsCodeDriver> list = new List<StateAsCodeDriver>();
			list.Add(new AutomatonQueueStateClosed(this, automatonQueueContext));
			list.Add(new AutomatonQueueStateOpening(this, automatonQueueContext));
			list.Add(new AutomatonQueueStateDataFlowSend(this, automatonQueueContext));
			list.Add(new AutomatonQueueStateDataFlowSent(this, automatonQueueContext));
			list.Add(new AutomatonQueueStateDataFlowSentAsync(this, automatonQueueContext));
			list.Add(new AutomatonQueueStateReadAhead(this, automatonQueueContext));
			list.Add(new AutomatonQueueStateReadAheadWait(this, automatonQueueContext));
			list.Add(new AutomatonQueueStateReadAheadWaited(this, automatonQueueContext));
			list.Add(new AutomatonQueueStateReadAheadReceiving(this, automatonQueueContext));
			list.Add(new AutomatonQueueStateDataFlowReceive(this, automatonQueueContext));
			list.Add(new AutomatonQueueStateDataFlowWait(this, automatonQueueContext));
			list.Add(new AutomatonQueueStateReceivingAsync(this, automatonQueueContext));
			list.Add(new AutomatonQueueStateReceivedAsync(this, automatonQueueContext));
			list.Add(new AutomatonQueueStateDetachingSend(this, automatonQueueContext));
			list.Add(new AutomatonQueueStateDetachingReceive(this, automatonQueueContext));
			list.Add(new AutomatonQueueStateClosing(this, automatonQueueContext));
			QAutomatonTracePoint qautomatonTracePoint = new QAutomatonTracePoint(parentTracePoint);
			base.Automaton = new AutomatonDefinition(automatonName, instanceName, automatonQueueContext, list, qautomatonTracePoint);
			AutomatonQueueContext automatonQueueContext2 = base.Automaton.Context as AutomatonQueueContext;
			automatonQueueContext2.TracePoint = qautomatonTracePoint;
			automatonQueueContext2.ParentTracePoint = parentTracePoint;
			automatonQueueContext2.AutomatonEventLog = eventLogContainer;
			base.AddConnections(0, 1, new ProcessAsynchronousMessage(this.ProcessMessageFromQueueManager), 1);
		}

		// Token: 0x060053F9 RID: 21497 RVA: 0x00155786 File Offset: 0x00153986
		public void ProcessEvent(AutomatonQueueEvent eventToProcess)
		{
			base.ProcessEvent((int)eventToProcess);
		}

		// Token: 0x060053FA RID: 21498 RVA: 0x00155790 File Offset: 0x00153990
		public void ConnectToQueueManager()
		{
			AutomatonQueueContext automatonQueueContext = base.Automaton.Context as AutomatonQueueContext;
			base.ConnectTo(0, automatonQueueContext.AutomatonQueueManager, automatonQueueContext.DeterminantForQueueManager);
		}

		// Token: 0x060053FB RID: 21499 RVA: 0x001557C1 File Offset: 0x001539C1
		public void DisconnectFromQueueManager()
		{
			this.DisconnectFromQueueManager(true);
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x001557CC File Offset: 0x001539CC
		public void DisconnectFromQueueManager(bool needToInformOtherEnd)
		{
			AutomatonQueueContext automatonQueueContext = base.Automaton.Context as AutomatonQueueContext;
			base.DisconnectFrom(0, automatonQueueContext.DeterminantForQueueManager, needToInformOtherEnd);
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x001557F8 File Offset: 0x001539F8
		public void SendToQueueManager(AsynchronousConnectionMessage message)
		{
			base.Send(0, message);
		}

		// Token: 0x060053FE RID: 21502 RVA: 0x00155802 File Offset: 0x00153A02
		public void SendToQueueManager(int type, object contents)
		{
			this.SendToQueueManager(new AsynchronousConnectionMessage(type, contents));
		}

		// Token: 0x060053FF RID: 21503 RVA: 0x00155814 File Offset: 0x00153A14
		public void ProcessMessageFromQueueManager(AsynchronousConnectionMessage message)
		{
			AutomatonQueueContext automatonQueueContext = base.Automaton.Context as AutomatonQueueContext;
			switch (message.Type)
			{
			case 200:
				automatonQueueContext.MessageFromQueueManager = message;
				this.ProcessEvent(AutomatonQueueEvent.ServerData);
				return;
			case 201:
				this.ProcessEvent(AutomatonQueueEvent.QmFailed);
				return;
			case 202:
				automatonQueueContext.MessageFromQueueManager = message;
				this.ProcessEvent(AutomatonQueueEvent.AttachFailed);
				return;
			case 203:
				automatonQueueContext.MessageFromQueueManager = message;
				this.ProcessEvent(AutomatonQueueEvent.Detached);
				return;
			case 204:
				this.ProcessEvent(AutomatonQueueEvent.AsyncPutDone);
				return;
			case 205:
				automatonQueueContext.MessageFromQueueManager = message;
				this.ProcessEvent(AutomatonQueueEvent.Quiesced);
				return;
			default:
				throw new InvalidOperationException("Unknown Message Type");
			}
		}

		// Token: 0x06005400 RID: 21504 RVA: 0x001558B8 File Offset: 0x00153AB8
		public void GenerateMqOpen(DynamicDataBuffer buffer, string queueName, OpenOption optionOption)
		{
			int num = 448;
			AutomatonQueueContext automatonQueueContext = base.Automaton.Context as AutomatonQueueContext;
			int num2 = AutomatonQueue.GenerateTshmMqOpen(buffer, automatonQueueContext.QueueManagerConversationId);
			num += num2;
			int num3 = num2;
			SegmentHelpers.GenerateApiHeader(buffer, num3, 1500, 0);
			num3 += 16;
			SegmentHelpers.GenerateObjectDescriptor(buffer, num3, ObjectType.Queue, queueName, automatonQueueContext.DynamicQueueNamePrefix);
			num3 += 400;
			SegmentHelpers.GenerateOpenOptions(buffer, num3, optionOption);
			num3 += 4;
			SegmentHelpers.GenerateFopa(buffer, num3);
			buffer.UsedLength = num;
			SegmentHelpers.FillLength(buffer.Data, num);
		}

		// Token: 0x06005401 RID: 21505 RVA: 0x0015593E File Offset: 0x00153B3E
		private static int GenerateTshmMqOpen(DynamicDataBuffer buffer, int conversationId)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.MqOpen, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x06005402 RID: 21506 RVA: 0x00155956 File Offset: 0x00153B56
		internal string ExtractQueueFromMqOpenReply(PassThroughData data)
		{
			return SegmentHelpers.ExtractObjectName(data.Buffers[0], data.SegmentHeaderInformation.LengthOfSegmentHeader + 16, data.SegmentHeaderInformation.LittleEndian, (int)data.SegmentHeaderInformation.Ccsid);
		}

		// Token: 0x06005403 RID: 21507 RVA: 0x00155990 File Offset: 0x00153B90
		public void GenerateMqClose(DynamicDataBuffer buffer)
		{
			AutomatonQueueContext automatonQueueContext = base.Automaton.Context as AutomatonQueueContext;
			int num = 20;
			int num2 = AutomatonQueue.GenerateTshmMqClose(buffer, automatonQueueContext.QueueManagerConversationId);
			num += num2;
			int num3 = num2;
			SegmentHelpers.GenerateApiHeader(buffer, num3, num - 8, automatonQueueContext.DeterminantForQueueManager);
			num3 += 16;
			SegmentHelpers.GenerateCloseOptions(buffer, num3, CloseOption.None);
			buffer.UsedLength = num;
			SegmentHelpers.FillLength(buffer.Data, num);
		}

		// Token: 0x06005404 RID: 21508 RVA: 0x001559F4 File Offset: 0x00153BF4
		private static int GenerateTshmMqClose(DynamicDataBuffer buffer, int conversationId)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.MqClose, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x06005405 RID: 21509 RVA: 0x00155A0C File Offset: 0x00153C0C
		public void SendRequestMessageToQueueManager(bool zeroTimeout)
		{
			AutomatonQueueContext automatonQueueContext = base.Automaton.Context as AutomatonQueueContext;
			int determinantForQueueManager = automatonQueueContext.DeterminantForQueueManager;
			int queueManagerConversationId = automatonQueueContext.QueueManagerConversationId;
			ReceiveOptions receiveOptions = (automatonQueueContext.OriginalReceiveOptionsIncluded ? automatonQueueContext.ReceiveOptions : new ReceiveOptions());
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			int num = AutomatonQueue.GenerateTshmRequestMessage(buffer, queueManagerConversationId, 3);
			int globalMessageIndex = (automatonQueueContext.AutomatonQueueManager.Automaton.Context as AutomatonQueueManagerContext).GlobalMessageIndex;
			bool flag = automatonQueueContext.OpenedWithReadAhead && automatonQueueContext.FollowUpRequest;
			int num2 = ReceiveMessage.GenerateRequestMessage(buffer.Data, num, determinantForQueueManager, automatonQueueContext.OpenedWithReadAhead, automatonQueueContext.MaximumMessageSize, receiveOptions, zeroTimeout, automatonQueueContext.AsynchronouslyReceivedBytes, automatonQueueContext.BytesToReceiveAsynchronously, globalMessageIndex, flag, automatonQueueContext.IsTransactional);
			automatonQueueContext.AsynchronouslyReceivedBytes = 0;
			automatonQueueContext.FollowUpRequest = true;
			num2 += num;
			buffer.UsedLength = num2;
			SegmentHelpers.FillLength(buffer.Data, num2);
			this.SendBufferAsPassthroughClientDataToQueueManager(buffer);
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x00155AFE File Offset: 0x00153CFE
		private static int GenerateTshmMqGet(DynamicDataBuffer buffer, int conversationId)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.MqGet, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x00155B16 File Offset: 0x00153D16
		private static int GenerateTshmRequestMessage(DynamicDataBuffer buffer, int conversationId, int requestId)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, requestId, SegmentType.RequestMessages, ControlFlag1.None, ControlFlag2.None);
		}

		// Token: 0x06005408 RID: 21512 RVA: 0x00155B2C File Offset: 0x00153D2C
		public void SendDataToQueueManager(bool async)
		{
			AutomatonQueueContext automatonQueueContext = base.Automaton.Context as AutomatonQueueContext;
			SendMessage clientData = automatonQueueContext.ClientData;
			int queueManagerConversationId = automatonQueueContext.QueueManagerConversationId;
			int determinantForQueueManager = automatonQueueContext.DeterminantForQueueManager;
			int num = 548;
			int totalHeaderLength = clientData.TotalHeaderLength;
			num += totalHeaderLength;
			int num2 = clientData.Data.Length;
			int num3 = 4 - num2 % 4;
			if (num3 == 4)
			{
				num3 = 0;
			}
			int num4 = num2 + num3;
			DynamicDataBuffer dynamicDataBuffer = base.GetBuffer(32768);
			int num5 = num + num4 - 8;
			int num6;
			int num7;
			if (num + num4 <= automatonQueueContext.UsedMaximumTransmissionSize)
			{
				num6 = num2;
				num7 = num4;
				AutomatonQueue.GenerateCompletePutHeaders(dynamicDataBuffer, clientData, queueManagerConversationId, determinantForQueueManager, num2, num5, totalHeaderLength, automatonQueueContext.IsTransactional);
			}
			else
			{
				num6 = automatonQueueContext.UsedMaximumTransmissionSize - num;
				num7 = num6;
				AutomatonQueue.GenerateFirstPutHeaders(dynamicDataBuffer, clientData, queueManagerConversationId, determinantForQueueManager, num2, num5, totalHeaderLength, automatonQueueContext.IsTransactional);
			}
			Array.Copy(clientData.Data, 0, dynamicDataBuffer.Data, num, num6);
			dynamicDataBuffer.UsedLength = num + num7;
			SegmentHelpers.FillLength(dynamicDataBuffer.Data, dynamicDataBuffer.UsedLength);
			PassThroughData passThroughData = new PassThroughData();
			passThroughData.QueueManagerDeterminant = queueManagerConversationId;
			passThroughData.QueueDeterminant = determinantForQueueManager;
			passThroughData.Buffers = new List<DynamicDataBuffer> { dynamicDataBuffer };
			if (num6 != num2)
			{
				num = 36;
				int num8 = num6;
				num2 -= num6;
				num4 = num2 + num3;
				bool flag = num2 + num <= automatonQueueContext.UsedMaximumTransmissionSize;
				while (!flag)
				{
					dynamicDataBuffer = base.GetBuffer(32768);
					AutomatonQueue.GenerateMiddlePutHeader(dynamicDataBuffer, queueManagerConversationId);
					num6 = automatonQueueContext.UsedMaximumTransmissionSize - num;
					Array.Copy(clientData.Data, num8, dynamicDataBuffer.Data, num, num6);
					dynamicDataBuffer.UsedLength = automatonQueueContext.UsedMaximumTransmissionSize;
					SegmentHelpers.FillLength(dynamicDataBuffer.Data, dynamicDataBuffer.UsedLength);
					passThroughData.Buffers.Add(dynamicDataBuffer);
					num8 += num6;
					num2 -= num6;
					flag = num2 + num <= automatonQueueContext.UsedMaximumTransmissionSize;
				}
				dynamicDataBuffer = base.GetBuffer(32768);
				AutomatonQueue.GenerateEndPutHeader(dynamicDataBuffer, queueManagerConversationId);
				num6 = num2;
				num4 = num2 + num3;
				Array.Copy(clientData.Data, num8, dynamicDataBuffer.Data, num, num6);
				dynamicDataBuffer.UsedLength = num4 + num;
				SegmentHelpers.FillLength(dynamicDataBuffer.Data, dynamicDataBuffer.UsedLength);
				passThroughData.Buffers.Add(dynamicDataBuffer);
			}
			this.SendToQueueManager(new AsynchronousConnectionMessage(302, async ? 350 : 0, passThroughData));
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x00155DAC File Offset: 0x00153FAC
		private static void GenerateCompletePutHeaders(DynamicDataBuffer buffer, SendMessage message, int conversationId, int objectHandle, int dataBytes, int replylength, int specifiedHeaderBytes, bool queueIsTransactional)
		{
			int num = SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.MqPut, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
			AutomatonQueue.GenerateRestOfPutHeaders(buffer, message, objectHandle, num, dataBytes, replylength, specifiedHeaderBytes, queueIsTransactional);
		}

		// Token: 0x0600540A RID: 21514 RVA: 0x00155DE4 File Offset: 0x00153FE4
		private static void GenerateFirstPutHeaders(DynamicDataBuffer buffer, SendMessage message, int conversationId, int objectHandle, int dataBytes, int replylength, int specifiedHeaderBytes, bool queueIsTransactional)
		{
			int num = SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.MqPut, ControlFlag1.FirstSegment, ControlFlag2.None);
			AutomatonQueue.GenerateRestOfPutHeaders(buffer, message, objectHandle, num, dataBytes, replylength, specifiedHeaderBytes, queueIsTransactional);
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x00155E1C File Offset: 0x0015401C
		private static void GenerateRestOfPutHeaders(DynamicDataBuffer buffer, SendMessage message, int objectHandle, int index, int dataBytes, int replylength, int specifiedHeaderBytes, bool queueIsTransactional)
		{
			SegmentHelpers.GenerateApiHeader(buffer, index, replylength, objectHandle);
			index += 16;
			message.GenerateMqmd(buffer.Data, index);
			index += 364;
			message.Options.GenerateBytes(buffer.Data, index, message, queueIsTransactional);
			index += 128;
			SegmentHelpers.GeneratePutGet(buffer, index, dataBytes + specifiedHeaderBytes);
			index += 4;
			message.GenerateHeaderBytes(buffer.Data, ref index);
		}

		// Token: 0x0600540C RID: 21516 RVA: 0x00155E8C File Offset: 0x0015408C
		private static void GenerateMiddlePutHeader(DynamicDataBuffer buffer, int conversationId)
		{
			SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.MqPut, ControlFlag1.None, ControlFlag2.None);
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x00155EA4 File Offset: 0x001540A4
		private static void GenerateEndPutHeader(DynamicDataBuffer buffer, int conversationId)
		{
			SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.MqPut, ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x00155EC0 File Offset: 0x001540C0
		internal unsafe static AsyncMessageInformation ExtractAsyncMessage(PassThroughData data)
		{
			DynamicDataBuffer dynamicDataBuffer = data.Buffers[0];
			AsyncMessageInformation asyncMessageInformation = new AsyncMessageInformation();
			bool littleEndian = data.SegmentHeaderInformation.LittleEndian;
			fixed (byte* ptr = &dynamicDataBuffer.Data[data.SegmentHeaderInformation.LengthOfSegmentHeader])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				if (ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian) != 1)
				{
					throw new InvalidOperationException("Can't handle Async Message Version");
				}
				asyncMessageInformation.ObjectHandle = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				ptr3 += 2;
				asyncMessageInformation.SegmentLength = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				ptr3++;
				byte* ptr4 = (byte*)ptr3;
				if ((data.SegmentHeaderInformation.ControlFlag1 & ControlFlag1.FirstSegment) == ControlFlag1.None)
				{
					asyncMessageInformation.ReasonCode = AsyncMessageReasonCode.Ok;
					asyncMessageInformation.TruncatedLength = -1;
					asyncMessageInformation.OriginalMessageLength = -1;
				}
				else
				{
					asyncMessageInformation.ReasonCode = (AsyncMessageReasonCode)ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
					asyncMessageInformation.TruncatedLength = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
					asyncMessageInformation.OriginalMessageLength = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
					ptr4 = (byte*)ptr3;
					ptr4 += 18;
					int num = (int)(*(ptr4++));
					ptr4 += num;
					int num2 = (3 + num) % 4;
					if (num2 != 0)
					{
						ptr4 += 4 - num2;
					}
				}
				asyncMessageInformation.Length = (int)((long)(ptr4 - ptr2));
			}
			return asyncMessageInformation;
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x00155FE0 File Offset: 0x001541E0
		public void SendBufferAsPassthroughClientDataToQueueManager(DynamicDataBuffer buffer)
		{
			this.SendClientDataToQueueManager(new PassThroughData
			{
				Buffers = new List<DynamicDataBuffer> { buffer }
			});
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x0015600C File Offset: 0x0015420C
		public void SendClientDataToQueueManager(object contents)
		{
			this.SendToQueueManager(302, contents);
		}
	}
}
