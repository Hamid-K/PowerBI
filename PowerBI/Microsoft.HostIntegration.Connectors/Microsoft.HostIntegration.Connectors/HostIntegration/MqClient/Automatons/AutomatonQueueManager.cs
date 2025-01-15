using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.EventLogging;
using Microsoft.HostIntegration.Nls;
using Microsoft.HostIntegration.Tracing.MqClient;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A96 RID: 2710
	public sealed class AutomatonQueueManager : AutomatonDriverAsCode
	{
		// Token: 0x0600549A RID: 21658 RVA: 0x00156504 File Offset: 0x00154704
		static AutomatonQueueManager()
		{
			AutomatonDriverAsCode.RegisterAutomaton(3, 1, 2);
		}

		// Token: 0x0600549B RID: 21659 RVA: 0x0015650E File Offset: 0x0015470E
		public AutomatonQueueManager(QueueManagerTracePoint parentTracePoint, EventLogContainer eventLogContainer)
			: base(1)
		{
			this.InternalConstructor(parentTracePoint, "QueueManager", "QueueManager", eventLogContainer);
		}

		// Token: 0x0600549C RID: 21660 RVA: 0x00156529 File Offset: 0x00154729
		public AutomatonQueueManager(QueueManagerTracePoint parentTracePoint, string instanceName, EventLogContainer eventLogContainer)
			: base(1)
		{
			this.InternalConstructor(parentTracePoint, "QueueManager", instanceName, eventLogContainer);
		}

		// Token: 0x0600549D RID: 21661 RVA: 0x00156540 File Offset: 0x00154740
		private void InternalConstructor(QueueManagerTracePoint parentTracePoint, string automatonName, string instanceName, EventLogContainer eventLogContainer)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = new AutomatonQueueManagerContext();
			List<StateAsCodeDriver> list = new List<StateAsCodeDriver>();
			list.Add(new AutomatonQueueManagerStateUnConnected(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateConnectingTcp(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateHandshake(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateAuthorization(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateHandshakeFailed(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateAttachingTcp(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateSocketAction(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateConnectingQm(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateDataFlow(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateTxnOpening(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateTxnOpen(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateTxnStarting(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateInTxn(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateTxnEnding(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateTxnEnded(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateTxnPreparing(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateTxnPrepared(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateTxnFinalizing(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateTxnCatchup(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateTxnClosing(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateRecoveryNoTxn(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateRecoveryStarting(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateRecoveryActive(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateRecoveryEnding(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateRecoveryIdle(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateRecoveryPreparing(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateRecoveryPrepared(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateRecoveryFinalizing(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateRecoveryRollbackOnly(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateRecoveryHeuristic(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateRecoveryForgetting(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateDisconnecting(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateDetachingTcp(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateOpeningClosing(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateOpenCloseTxnOpen(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateOpenCloseInTxn(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateServerClosing(this, automatonQueueManagerContext));
			list.Add(new AutomatonQueueManagerStateDataFlowFailed(this, automatonQueueManagerContext));
			QmAutomatonTracePoint qmAutomatonTracePoint = new QmAutomatonTracePoint(parentTracePoint);
			base.Automaton = new AutomatonDefinition(automatonName, instanceName, automatonQueueManagerContext, list, qmAutomatonTracePoint);
			AutomatonQueueManagerContext automatonQueueManagerContext2 = base.Automaton.Context as AutomatonQueueManagerContext;
			automatonQueueManagerContext2.TracePoint = qmAutomatonTracePoint;
			automatonQueueManagerContext2.ParentTracePoint = parentTracePoint;
			automatonQueueManagerContext2.AutomatonEventLog = eventLogContainer;
			base.AddConnections(0, 0, new ProcessAsynchronousMessage(this.ProcessMessageFromTcp), 1);
			base.AddConnections(1, 0, new ProcessAsynchronousMessage(this.ProcessMessageFromQueue), new ProcessDisconnect(this.ProcessQueueDisconnect), -1);
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x00155786 File Offset: 0x00153986
		public void ProcessEvent(AutomatonQueueManagerEvent eventToProcess)
		{
			base.ProcessEvent((int)eventToProcess);
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x001567BC File Offset: 0x001549BC
		public void ConnectToTcp()
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			base.ConnectTo(0, automatonQueueManagerContext.AutomatonTcp, automatonQueueManagerContext.DeterminantForTcp);
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x001567ED File Offset: 0x001549ED
		public void DisconnectFromTcp()
		{
			this.DisconnectFromTcp(true);
		}

		// Token: 0x060054A1 RID: 21665 RVA: 0x001567F8 File Offset: 0x001549F8
		public void DisconnectFromTcp(bool needToInformOtherEnd)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			base.DisconnectFrom(0, automatonQueueManagerContext.DeterminantForTcp, needToInformOtherEnd);
		}

		// Token: 0x060054A2 RID: 21666 RVA: 0x001557F8 File Offset: 0x001539F8
		public void SendToTcp(AsynchronousConnectionMessage message)
		{
			base.Send(0, message);
		}

		// Token: 0x060054A3 RID: 21667 RVA: 0x00156824 File Offset: 0x00154A24
		public void SendToTcp(int type, object contents)
		{
			this.SendToTcp(new AsynchronousConnectionMessage(type, contents));
		}

		// Token: 0x060054A4 RID: 21668 RVA: 0x00156833 File Offset: 0x00154A33
		public static int GetDeterminantToQueue(AsynchronousConnectionMessage message)
		{
			return (message.Contents as DeterminantMessage).QueueDeterminant;
		}

		// Token: 0x060054A5 RID: 21669 RVA: 0x00156845 File Offset: 0x00154A45
		public void BroadcastToQueue(AsynchronousConnectionMessage message)
		{
			base.Send(1, message, -1);
		}

		// Token: 0x060054A6 RID: 21670 RVA: 0x00156854 File Offset: 0x00154A54
		public void SendToQueue(AsynchronousConnectionMessage message)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			int determinantToQueue = AutomatonQueueManager.GetDeterminantToQueue(message);
			automatonQueueManagerContext.ConnectionFoundToQueue = base.Send(1, message, determinantToQueue);
		}

		// Token: 0x060054A7 RID: 21671 RVA: 0x00156886 File Offset: 0x00154A86
		public void SendToQueue(int type, object contents)
		{
			this.SendToQueue(new AsynchronousConnectionMessage(type, contents));
		}

		// Token: 0x060054A8 RID: 21672 RVA: 0x00156898 File Offset: 0x00154A98
		public void ProcessMessageFromTcp(AsynchronousConnectionMessage message)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			switch (message.Type)
			{
			case 400:
				this.ProcessEvent(AutomatonQueueManagerEvent.Attached);
				return;
			case 401:
				this.ProcessEvent(AutomatonQueueManagerEvent.AttachFailed);
				return;
			case 402:
				this.ProcessEvent(AutomatonQueueManagerEvent.Detached);
				return;
			case 403:
				automatonQueueManagerContext.DataMessageFromTcp = message;
				this.ProcessEvent(AutomatonQueueManagerEvent.ServerData);
				return;
			case 404:
				this.ProcessEvent(AutomatonQueueManagerEvent.TcpDisconnected);
				return;
			case 405:
				automatonQueueManagerContext.DataMessageFromTcp = message;
				this.ProcessEvent(AutomatonQueueManagerEvent.AsyncSendDone);
				return;
			default:
				throw new InvalidOperationException("Unknown Message Type");
			}
		}

		// Token: 0x060054A9 RID: 21673 RVA: 0x00156930 File Offset: 0x00154B30
		public void ProcessMessageFromQueue(AsynchronousConnectionMessage message)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			switch (message.Type)
			{
			case 300:
				automatonQueueManagerContext.DataMessageFromQueue = message;
				this.ProcessEvent(AutomatonQueueManagerEvent.QAttach);
				return;
			case 301:
				automatonQueueManagerContext.DataMessageFromQueue = message;
				this.ProcessEvent(AutomatonQueueManagerEvent.QDetach);
				return;
			case 302:
				automatonQueueManagerContext.DataMessageFromQueue = message;
				this.ProcessEvent(AutomatonQueueManagerEvent.ClientData);
				return;
			default:
				throw new InvalidOperationException("Unknown Message Type");
			}
		}

		// Token: 0x060054AA RID: 21674 RVA: 0x001569A9 File Offset: 0x00154BA9
		public void ProcessQueueDisconnect()
		{
			this.ProcessEvent(AutomatonQueueManagerEvent.QueueDisconnected);
		}

		// Token: 0x060054AB RID: 21675 RVA: 0x001569B4 File Offset: 0x00154BB4
		public unsafe void GenerateInitialData(bool isFirstInitialData, DynamicDataBuffer buffer)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			if (isFirstInitialData)
			{
				automatonQueueManagerContext.FapLevel = 12;
				automatonQueueManagerContext.MaximumMessageBatch = 50;
				automatonQueueManagerContext.MaximumTransmissionSize = 32758;
				automatonQueueManagerContext.MaximumMessageSize = 104857600;
				automatonQueueManagerContext.SequenceNumberWrap = 999999999;
				automatonQueueManagerContext.HeartBeatInterval = 300;
				automatonQueueManagerContext.CapabilityFlag2 = CapabilityFlag2.FastMessageRequest | CapabilityFlag2.SpiRequest | CapabilityFlag2.TraceRouteCapable;
				automatonQueueManagerContext.NumberOfConversationsPerSocket = 10;
			}
			int num = 208;
			int num2;
			if (isFirstInitialData)
			{
				num2 = AutomatonQueueManager.GenerateTshInitialData(buffer);
			}
			else
			{
				num2 = AutomatonQueueManager.GenerateTshmInitialData(buffer);
			}
			num += num2;
			fixed (byte* ptr = &buffer.Data[num2])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = 538985545;
				byte* ptr4 = (byte*)ptr3;
				*(ptr4++) = automatonQueueManagerContext.FapLevel;
				byte b = 37;
				if (automatonQueueManagerContext.SrvConSecurityBitSet)
				{
					b |= 64;
				}
				*(ptr4++) = b;
				*(ptr4++) = 0;
				*(ptr4++) = 0;
				*(ptr4++) = 0;
				*(ptr4++) = 0;
				short* ptr5 = (short*)ptr4;
				*(ptr5++) = automatonQueueManagerContext.MaximumMessageBatch;
				ptr3 = (int*)ptr5;
				*(ptr3++) = automatonQueueManagerContext.MaximumTransmissionSize;
				*(ptr3++) = automatonQueueManagerContext.MaximumMessageSize;
				*(ptr3++) = automatonQueueManagerContext.SequenceNumberWrap;
				ptr4 = (byte*)ptr3;
				ConversionHelpers.MoveStringToBufferAscii(buffer.Data, (int)((long)(ptr4 - ptr2) + (long)num2), automatonQueueManagerContext.ConnectionParameters.Channel, 20, true);
				ptr4 += 20;
				*(ptr4++) = (byte)automatonQueueManagerContext.CapabilityFlag2;
				*(ptr4++) = 0;
				ptr5 = (short*)ptr4;
				*(ptr5++) = 1252;
				ptr4 = (byte*)ptr5;
				ConversionHelpers.MoveStringToBufferAscii(buffer.Data, (int)((long)(ptr4 - ptr2) + (long)num2), automatonQueueManagerContext.ConnectionParameters.Name, 48, true);
				ptr4 += 48;
				ptr3 = (int*)ptr4;
				*(ptr3++) = automatonQueueManagerContext.HeartBeatInterval;
				ptr5 = (short*)ptr3;
				*(ptr5++) = 106;
				ptr4 = (byte*)ptr5;
				*(ptr4++) = 0;
				*(ptr4++) = 0;
				*(ptr4++) = 0;
				*(ptr4++) = byte.MaxValue;
				*(ptr4++) = 0;
				for (int i = 0; i < 15; i++)
				{
					*(ptr4++) = byte.MaxValue;
				}
				ptr5 = (short*)ptr4;
				*(ptr5++) = 0;
				ptr3 = (int*)ptr5;
				*(ptr3++) = 0;
				*(ptr3++) = automatonQueueManagerContext.NumberOfConversationsPerSocket;
				ptr4 = (byte*)ptr3;
				*(ptr4++) = 0;
				*(ptr4++) = 0;
				ptr5 = (short*)ptr4;
				*(ptr5++) = 0;
				ptr3 = (int*)ptr5;
				*(ptr3++) = Process.GetCurrentProcess().Id;
				*(ptr3++) = Thread.CurrentThread.ManagedThreadId;
				*(ptr3++) = 0;
				ptr4 = (byte*)ptr3;
				ConversionHelpers.MoveStringToBufferAscii(buffer.Data, (int)((long)(ptr4 - ptr2) + (long)num2), "MS HIS V11.0", 12, true);
				ptr4 += 12;
				for (int j = 0; j < 48; j++)
				{
					*(ptr4++) = 0;
				}
				buffer.UsedLength = num;
			}
			SegmentHelpers.FillLength(buffer.Data, num);
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x00156CC7 File Offset: 0x00154EC7
		private static int GenerateTshInitialData(DynamicDataBuffer buffer)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 541610836, SegmentHeaderType.Tsh, 1, 0, SegmentType.InitialData, ControlFlag1.ConfirmRequest, ControlFlag2.None);
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x00156CDA File Offset: 0x00154EDA
		private static int GenerateTshmInitialData(DynamicDataBuffer buffer)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, 1, 0, SegmentType.InitialData, ControlFlag1.ConfirmRequest, ControlFlag2.None);
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x00156CF0 File Offset: 0x00154EF0
		public unsafe void ExtractInitialData(DynamicDataBuffer buffer)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			SegmentHeaderInformation segmentHeaderInformation = (automatonQueueManagerContext.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			automatonQueueManagerContext.IsInitialData = false;
			automatonQueueManagerContext.IsStatusData = false;
			if (segmentHeaderInformation.SegmentType == SegmentType.StatusData)
			{
				automatonQueueManagerContext.IsStatusData = true;
				automatonQueueManagerContext.StatusDataType = StatusDataType.Unknown;
				if ((segmentHeaderInformation.ControlFlag1 & ControlFlag1.Error) == ControlFlag1.Error)
				{
					fixed (byte* ptr = &buffer.Data[segmentHeaderInformation.LengthOfSegmentHeader + 4])
					{
						short* ptr2 = (short*)ptr;
						automatonQueueManagerContext.StatusDataType = (StatusDataType)ConversionHelpers.ExtractShortFromAddress(ref ptr2, segmentHeaderInformation.LittleEndian);
					}
				}
				return;
			}
			if (segmentHeaderInformation.SegmentType != SegmentType.InitialData)
			{
				return;
			}
			automatonQueueManagerContext.ServerNumericEncoding = segmentHeaderInformation.NumericEncoding;
			fixed (byte* ptr = &buffer.Data[segmentHeaderInformation.LengthOfSegmentHeader])
			{
				byte* ptr3 = ptr;
				int* ptr4 = (int*)ptr3;
				int num = *(ptr4++);
				if (num != 538985545 && num != 1077986505)
				{
					return;
				}
				automatonQueueManagerContext.IsInitialData = true;
				byte* ptr5 = (byte*)ptr4;
				automatonQueueManagerContext.ServerFapLevel = *(ptr5++);
				byte b = *(ptr5++);
				if ((b & 64) == 64)
				{
					automatonQueueManagerContext.SrvConSecurityBitSet = true;
					b &= 191;
				}
				else
				{
					automatonQueueManagerContext.SrvConSecurityBitSet = false;
				}
				if (b != 37)
				{
					throw new InvalidOperationException("Initial Data contains different Capability Flag 1");
				}
				if ((*(ptr5++) & 191) != 0)
				{
					throw new InvalidOperationException("Initial Data contains different Extended Capability Flag 1");
				}
				byte b2 = *(ptr5++);
				if (b2 != 0 && segmentHeaderInformation.ControlFlag1 != ControlFlag1.Error)
				{
					throw new InvalidOperationException("Initial Data contains Error Flag 1, but Segment Header doesn't say Error");
				}
				automatonQueueManagerContext.ServerErrorFlag1 = (InitializationErrorFlag1)b2;
				ptr5++;
				ptr5++;
				short* ptr6 = (short*)ptr5;
				automatonQueueManagerContext.ServerMaximumMessageBatch = ConversionHelpers.ExtractShortFromAddress(ref ptr6, segmentHeaderInformation.LittleEndian);
				ptr4 = (int*)ptr6;
				automatonQueueManagerContext.ServerMaximumTransmissionSize = ConversionHelpers.ExtractIntFromAddress(ref ptr4, segmentHeaderInformation.LittleEndian);
				automatonQueueManagerContext.ServerMaximumMessageSize = ConversionHelpers.ExtractIntFromAddress(ref ptr4, segmentHeaderInformation.LittleEndian);
				automatonQueueManagerContext.ServerSequenceNumberWrap = ConversionHelpers.ExtractIntFromAddress(ref ptr4, segmentHeaderInformation.LittleEndian);
				ptr5 = (byte*)ptr4;
				ptr5 += 20;
				automatonQueueManagerContext.ServerCapabilityFlag2 = (CapabilityFlag2)(*(ptr5++));
				ptr5++;
				ptr6 = (short*)ptr5;
				automatonQueueManagerContext.ServerCcsid = ConversionHelpers.ExtractShortFromAddress(ref ptr6, segmentHeaderInformation.LittleEndian);
				ptr5 = (byte*)ptr6;
				ptr5 += 48;
				ptr4 = (int*)ptr5;
				automatonQueueManagerContext.ServerHeartBeatInterval = ConversionHelpers.ExtractIntFromAddress(ref ptr4, segmentHeaderInformation.LittleEndian);
				ptr6 = (short*)ptr4;
				ptr6++;
				ptr5 = (byte*)ptr6;
				byte b3 = *(ptr5++);
				if (b3 != 0 && segmentHeaderInformation.ControlFlag1 != ControlFlag1.Error)
				{
					throw new InvalidOperationException("Initial Data contains Error Flag 2, but Segment Header doesn't say Error");
				}
				automatonQueueManagerContext.ServerErrorFlag2 = (InitializationErrorFlag2)b3;
				ptr5++;
				ptr5 += 2;
				ptr5 += 16;
				ptr6 = (short*)ptr5;
				ptr6++;
				ptr4 = (int*)ptr6;
				ptr4++;
				automatonQueueManagerContext.ServerNumberOfConversationsPerSocket = ConversionHelpers.ExtractIntFromAddress(ref ptr4, segmentHeaderInformation.LittleEndian);
				ptr5 = (byte*)ptr4;
				ptr5++;
				ptr5++;
				ptr5 += 26;
				HisEncoding encoding = HisEncoding.GetEncoding((int)automatonQueueManagerContext.ServerCcsid);
				automatonQueueManagerContext.MqmId = encoding.GetString(buffer.Data, (int)((long)(ptr5 - ptr3) + (long)segmentHeaderInformation.LengthOfSegmentHeader), 48);
			}
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x00156FE4 File Offset: 0x001551E4
		public unsafe void GenerateUserId(DynamicDataBuffer buffer)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			WindowsIdentity windowsIdentity = automatonQueueManagerContext.WindowsIdentity;
			SecurityIdentifier user = windowsIdentity.User;
			byte[] array = new byte[user.BinaryLength];
			user.GetBinaryForm(array, 0);
			int num = ((array.Length < 28) ? 28 : array.Length);
			int num2 = 104 + num;
			int num3 = AutomatonQueueManager.GenerateTshmUserId(buffer);
			int num4 = num3 + num2;
			fixed (byte* ptr = &buffer.Data[num3])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = 541346133;
				byte* ptr4 = (byte*)ptr3;
				string text;
				string text2;
				automatonQueueManagerContext.OverrodeUserId = PooledQueueManager.GetEffectiveUserId(windowsIdentity, automatonQueueManagerContext.ConnectionParameters.ConnectAs, out text, out text2);
				ConversionHelpers.MoveStringToBufferAscii(buffer.Data, (int)((long)(ptr4 - ptr2) + (long)num3), text2, 12, true);
				ptr4 += 12;
				for (int i = 0; i < 12; i++)
				{
					*(ptr4++) = 32;
				}
				ConversionHelpers.MoveStringToBufferAscii(buffer.Data, (int)((long)(ptr4 - ptr2) + (long)num3), text, 64, true);
				ptr4 += 64;
				*(ptr4++) = (byte)(array.Length + 1);
				*(ptr4++) = 1;
				for (int j = 0; j < array.Length; j++)
				{
					if (automatonQueueManagerContext.OverrodeUserId)
					{
						*(ptr4++) = 0;
					}
					else
					{
						*(ptr4++) = array[j];
					}
				}
				if (num > array.Length)
				{
					for (int k = 0; k < num - array.Length; k++)
					{
						*(ptr4++) = 0;
					}
				}
				for (int l = 0; l < 10; l++)
				{
					*(ptr4++) = 0;
				}
			}
			buffer.UsedLength = num4;
			SegmentHelpers.FillLength(buffer.Data, num4);
		}

		// Token: 0x060054B0 RID: 21680 RVA: 0x0015718B File Offset: 0x0015538B
		private static int GenerateTshmUserId(DynamicDataBuffer buffer)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, 1, 0, SegmentType.UserId, ControlFlag1.ConfirmRequest, ControlFlag2.None);
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x001571A0 File Offset: 0x001553A0
		public unsafe void GenerateConAuth(DynamicDataBuffer buffer)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			int num = AutomatonQueueManager.GenerateTshmConAuth(buffer);
			string text = ((automatonQueueManagerContext.ConnectionParameters.AuthorizationUser != null) ? automatonQueueManagerContext.ConnectionParameters.AuthorizationUser : automatonQueueManagerContext.WindowsIdentity.Name);
			string text2 = ((automatonQueueManagerContext.ConnectionParameters.AuthorizationPassword != null) ? automatonQueueManagerContext.ConnectionParameters.AuthorizationPassword : string.Empty);
			int num2 = text.Length + text2.Length;
			int num3 = AutomatonQueueManager.MultipleOf4(num2);
			int num4 = num3 - num2;
			int num5 = 24 + num3;
			int num6 = num + num5;
			fixed (byte* ptr = &buffer.Data[num])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = 1414873411;
				*(ptr3++) = 1;
				*(ptr3++) = text.Length;
				*(ptr3++) = text2.Length;
				*(ptr3++) = text.Length;
				*(ptr3++) = text2.Length;
				byte* ptr4 = (byte*)ptr3;
				ConversionHelpers.MoveStringToBufferAscii(buffer.Data, (int)((long)(ptr4 - ptr2) + (long)num), text);
				ptr4 += text.Length;
				ConversionHelpers.MoveStringToBufferAscii(buffer.Data, (int)((long)(ptr4 - ptr2) + (long)num), text2);
				ptr4 += text2.Length;
				for (int i = 0; i < num4; i++)
				{
					*(ptr4++) = 32;
				}
			}
			buffer.UsedLength = num6;
			SegmentHelpers.FillLength(buffer.Data, num6);
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x0015731A File Offset: 0x0015551A
		private static int GenerateTshmConAuth(DynamicDataBuffer buffer)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, 1, 0, SegmentType.ConAuth, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x060054B3 RID: 21683 RVA: 0x00157330 File Offset: 0x00155530
		private static int MultipleOf4(int value)
		{
			int num = value % 4;
			if (num == 0)
			{
				return value;
			}
			return value + 4 - num;
		}

		// Token: 0x060054B4 RID: 21684 RVA: 0x0015734C File Offset: 0x0015554C
		public unsafe void GenerateMqConnect(DynamicDataBuffer buffer, int conversationId)
		{
			int num = 348;
			int num2 = AutomatonQueueManager.GenerateTshmMqConnect(buffer, conversationId);
			num += num2;
			SegmentHelpers.GenerateApiHeader(buffer, num2, num - 8, 0);
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			int num3 = num2 + 16;
			fixed (byte* ptr = &buffer.Data[num3])
			{
				byte* ptr2 = ptr;
				byte* ptr3 = ptr2;
				ConversionHelpers.MoveStringToBufferAscii(buffer.Data, num3, automatonQueueManagerContext.ConnectionParameters.Name, 48, true);
				ptr3 += 48;
				string text = Process.GetCurrentProcess().MainModule.FileName;
				if (text.Length > 28)
				{
					text = text.Substring(text.Length - 28);
				}
				ConversionHelpers.MoveStringToBufferAscii(buffer.Data, (int)((long)(ptr3 - ptr2) + (long)num3), text, 28, true);
				ptr3 += 28;
				int* ptr4 = (int*)ptr3;
				*(ptr4++) = 11;
				if (!automatonQueueManagerContext.OverrodeUserId)
				{
					SecurityIdentifier user = automatonQueueManagerContext.WindowsIdentity.User;
					byte[] array = new byte[user.BinaryLength];
					user.GetBinaryForm(array, 0);
					int num4 = array.Length - 6;
					if (num4 + 2 > 32)
					{
						throw new InvalidOperationException("SID is too long");
					}
					ptr3 = (byte*)ptr4;
					*(ptr3++) = (byte)num4;
					*(ptr3++) = array[0];
					*(ptr3++) = array[1];
					for (int i = 8; i < array.Length; i++)
					{
						*(ptr3++) = array[i];
					}
					int num5 = num4 + 1;
					for (int j = 0; j < 32 - num5 - 1; j++)
					{
						*(ptr3++) = 0;
					}
					*(ptr3++) = 11;
				}
				else
				{
					ptr3 = (byte*)ptr4;
					for (int k = 0; k < 32; k++)
					{
						*(ptr3++) = 0;
					}
				}
				ptr4 = (int*)ptr3;
				*(ptr4++) = 3;
				*(ptr4++) = 0;
				*(ptr4++) = 1330529094;
				*(ptr4++) = 2;
				*(ptr4++) = 1;
				ptr3 = (byte*)ptr4;
				for (int l = 0; l < 152; l++)
				{
					*(ptr3++) = 0;
				}
				ConversionHelpers.MoveStringToBufferAscii(buffer.Data, (int)((long)(ptr3 - ptr2) + (long)num3), automatonQueueManagerContext.MqmId, 48, true);
				ptr3 += 48;
				buffer.UsedLength = num;
			}
			SegmentHelpers.FillLength(buffer.Data, num);
		}

		// Token: 0x060054B5 RID: 21685 RVA: 0x00157594 File Offset: 0x00155794
		private static int GenerateTshmMqConnect(DynamicDataBuffer buffer, int conversationId)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.MqConnect, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x001575AC File Offset: 0x001557AC
		public static void GenerateMqDisconnect(DynamicDataBuffer buffer, int conversationId)
		{
			int num = 16;
			int num2 = AutomatonQueueManager.GenerateTshmMqDisconnect(buffer, conversationId);
			num += num2;
			SegmentHelpers.GenerateApiHeader(buffer, num2, num - 8, 0);
			buffer.UsedLength = num;
			SegmentHelpers.FillLength(buffer.Data, num);
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x001575E6 File Offset: 0x001557E6
		private static int GenerateTshmMqDisconnect(DynamicDataBuffer buffer, int conversationId)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.MqDisconnect, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x00157600 File Offset: 0x00155800
		public void GenerateSocketAction(DynamicDataBuffer buffer, SocketActionType socketActionType)
		{
			int num = 20;
			int num2 = AutomatonQueueManager.GenerateTshcSocketAction(buffer);
			num += num2;
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			SegmentHelpers.GenerateSocketAction(buffer, num2, automatonQueueManagerContext.DeterminantForTcp, socketActionType);
			buffer.UsedLength = num;
			SegmentHelpers.FillLength(buffer.Data, num);
		}

		// Token: 0x060054B9 RID: 21689 RVA: 0x0015764D File Offset: 0x0015584D
		private static int GenerateTshcSocketAction(DynamicDataBuffer buffer)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1128813396, SegmentHeaderType.Tshc, 0, 0, SegmentType.SocketAction, ControlFlag1.None, ControlFlag2.None);
		}

		// Token: 0x060054BA RID: 21690 RVA: 0x00157664 File Offset: 0x00155864
		public static void GenerateMqStat(DynamicDataBuffer buffer, int conversationId)
		{
			int num = 244;
			int num2 = AutomatonQueueManager.GenerateTshmMqStat(buffer, conversationId);
			num += num2;
			int num3 = num2;
			SegmentHelpers.GenerateApiHeader(buffer, num3, num, 0);
			num3 += 16;
			SegmentHelpers.GenerateMqStat(buffer, num3);
			buffer.UsedLength = num;
			SegmentHelpers.FillLength(buffer.Data, num);
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x001576AD File Offset: 0x001558AD
		private static int GenerateTshmMqStat(DynamicDataBuffer buffer, int conversationId)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.MqStat, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x001576C8 File Offset: 0x001558C8
		public unsafe void ExtractAsyncStatus(DynamicDataBuffer buffer)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			SegmentHeaderInformation segmentHeaderInformation = (automatonQueueManagerContext.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			bool littleEndian = segmentHeaderInformation.LittleEndian;
			fixed (byte* ptr = &buffer.Data[segmentHeaderInformation.LengthOfSegmentHeader + 16])
			{
				int* ptr2 = (int*)ptr;
				ptr2 += 5;
				automatonQueueManagerContext.SuccessfulPutCount += ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
				automatonQueueManagerContext.WarningPutCount += ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
				automatonQueueManagerContext.FailedPutCount += ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
			}
		}

		// Token: 0x060054BD RID: 21693 RVA: 0x00157764 File Offset: 0x00155964
		public void GenerateXaOpen(DynamicDataBuffer buffer, int conversationId)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			int num = AutomatonQueueManager.GenerateTshmXaOpen(buffer, conversationId);
			SegmentHelpers.GenerateXaOpen(buffer, num, automatonQueueManagerContext.ResourceManagerId);
			num += 16;
			string text = automatonQueueManagerContext.ConnectionParameters.Name;
			if (text.Length < 48)
			{
				text += new string(' ', 48 - text.Length);
			}
			string text2 = "qmname=" + text;
			int num2 = SegmentHelpers.GenerateXaInfo(buffer, num, text2);
			buffer.UsedLength = num + num2;
			SegmentHelpers.FillLength(buffer.Data, buffer.UsedLength);
		}

		// Token: 0x060054BE RID: 21694 RVA: 0x001577FA File Offset: 0x001559FA
		private static int GenerateTshmXaOpen(DynamicDataBuffer buffer, int conversationId)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.XaOpen, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x060054BF RID: 21695 RVA: 0x00157814 File Offset: 0x00155A14
		public void GenerateXaClose(DynamicDataBuffer buffer, int conversationId)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			AutomatonTcpContext automatonTcpContext = automatonQueueManagerContext.AutomatonTcp.Automaton.Context as AutomatonTcpContext;
			int num = AutomatonQueueManager.GenerateTshmXaClose(buffer, conversationId);
			SegmentHelpers.GenerateXaClose(buffer, num, automatonQueueManagerContext.ResourceManagerId);
			num += 16;
			string text = string.Format("qmname={0},channel={1},trptype=TCP,conname={2}({3})", new object[]
			{
				automatonQueueManagerContext.ConnectionParameters.Name,
				automatonQueueManagerContext.ConnectionParameters.Channel,
				automatonTcpContext.ConnectionParameters.Server,
				automatonTcpContext.ConnectionParameters.Port
			});
			int num2 = SegmentHelpers.GenerateXaInfo(buffer, num, text);
			buffer.UsedLength = num + num2;
			SegmentHelpers.FillLength(buffer.Data, buffer.UsedLength);
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x001578D5 File Offset: 0x00155AD5
		private static int GenerateTshmXaClose(DynamicDataBuffer buffer, int conversationId)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.XaClose, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x001578F0 File Offset: 0x00155AF0
		public void GenerateXaStart(DynamicDataBuffer buffer, int conversationId)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			int num = AutomatonQueueManager.GenerateTshmXaStart(buffer, conversationId);
			SegmentHelpers.GenerateXaStart(buffer, num, automatonQueueManagerContext.ResourceManagerId, automatonQueueManagerContext.XaFlags);
			num += 16;
			int num2 = SegmentHelpers.GenerateXaXid(buffer, num, automatonQueueManagerContext.Xid);
			buffer.UsedLength = num + num2;
			SegmentHelpers.FillLength(buffer.Data, buffer.UsedLength);
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x00157956 File Offset: 0x00155B56
		private static int GenerateTshmXaStart(DynamicDataBuffer buffer, int conversationId)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.XaStart, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x00157970 File Offset: 0x00155B70
		public void GenerateXaEnd(DynamicDataBuffer buffer, int conversationId)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			int num = AutomatonQueueManager.GenerateTshmXaEnd(buffer, conversationId);
			SegmentHelpers.GenerateXaEnd(buffer, num, automatonQueueManagerContext.ResourceManagerId, automatonQueueManagerContext.XaFlags);
			num += 16;
			int num2 = SegmentHelpers.GenerateXaXid(buffer, num, automatonQueueManagerContext.Xid);
			buffer.UsedLength = num + num2;
			SegmentHelpers.FillLength(buffer.Data, buffer.UsedLength);
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x001579D6 File Offset: 0x00155BD6
		private static int GenerateTshmXaEnd(DynamicDataBuffer buffer, int conversationId)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.XaEnd, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x060054C5 RID: 21701 RVA: 0x001579F0 File Offset: 0x00155BF0
		public void GenerateXaPrepare(DynamicDataBuffer buffer, int conversationId)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			int num = AutomatonQueueManager.GenerateTshmXaPrepare(buffer, conversationId);
			SegmentHelpers.GenerateXaPrepare(buffer, num, automatonQueueManagerContext.ResourceManagerId, automatonQueueManagerContext.XaFlags);
			num += 16;
			int num2 = SegmentHelpers.GenerateXaXid(buffer, num, automatonQueueManagerContext.Xid);
			buffer.UsedLength = num + num2;
			SegmentHelpers.FillLength(buffer.Data, buffer.UsedLength);
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x00157A56 File Offset: 0x00155C56
		private static int GenerateTshmXaPrepare(DynamicDataBuffer buffer, int conversationId)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.XaPrepare, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x060054C7 RID: 21703 RVA: 0x00157A70 File Offset: 0x00155C70
		public void GenerateXaCommit(DynamicDataBuffer buffer, int conversationId)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			int num = AutomatonQueueManager.GenerateTshmXaCommit(buffer, conversationId);
			SegmentHelpers.GenerateXaCommit(buffer, num, automatonQueueManagerContext.ResourceManagerId, automatonQueueManagerContext.XaFlags);
			num += 16;
			int num2 = SegmentHelpers.GenerateXaXid(buffer, num, automatonQueueManagerContext.Xid);
			buffer.UsedLength = num + num2;
			SegmentHelpers.FillLength(buffer.Data, buffer.UsedLength);
		}

		// Token: 0x060054C8 RID: 21704 RVA: 0x00157AD6 File Offset: 0x00155CD6
		private static int GenerateTshmXaCommit(DynamicDataBuffer buffer, int conversationId)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.XaCommit, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x060054C9 RID: 21705 RVA: 0x00157AF0 File Offset: 0x00155CF0
		public void GenerateXaRollback(DynamicDataBuffer buffer, int conversationId)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			int num = AutomatonQueueManager.GenerateTshmXaRollback(buffer, conversationId);
			SegmentHelpers.GenerateXaRollback(buffer, num, automatonQueueManagerContext.ResourceManagerId, automatonQueueManagerContext.XaFlags);
			num += 16;
			int num2 = SegmentHelpers.GenerateXaXid(buffer, num, automatonQueueManagerContext.Xid);
			buffer.UsedLength = num + num2;
			SegmentHelpers.FillLength(buffer.Data, buffer.UsedLength);
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x00157B56 File Offset: 0x00155D56
		private static int GenerateTshmXaRollback(DynamicDataBuffer buffer, int conversationId)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.XaRollback, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x00157B70 File Offset: 0x00155D70
		public void GenerateXaForget(DynamicDataBuffer buffer, int conversationId)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			int num = AutomatonQueueManager.GenerateTshmXaForget(buffer, conversationId);
			SegmentHelpers.GenerateXaForget(buffer, num, automatonQueueManagerContext.ResourceManagerId, automatonQueueManagerContext.XaFlags);
			num += 16;
			int num2 = SegmentHelpers.GenerateXaXid(buffer, num, automatonQueueManagerContext.Xid);
			buffer.UsedLength = num + num2;
			SegmentHelpers.FillLength(buffer.Data, buffer.UsedLength);
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x00157BD6 File Offset: 0x00155DD6
		private static int GenerateTshmXaForget(DynamicDataBuffer buffer, int conversationId)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.XaForget, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x00157BF0 File Offset: 0x00155DF0
		public void GenerateXaRecover(DynamicDataBuffer buffer, int conversationId)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			int num = AutomatonQueueManager.GenerateTshmXaRecover(buffer, conversationId);
			SegmentHelpers.GenerateXaRecover(buffer, num, automatonQueueManagerContext.ResourceManagerId, automatonQueueManagerContext.XaFlags, automatonQueueManagerContext.MaximumNumberOfXids);
			num += 20;
			buffer.UsedLength = num;
			SegmentHelpers.FillLength(buffer.Data, buffer.UsedLength);
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x00157C4C File Offset: 0x00155E4C
		private static int GenerateTshmXaRecover(DynamicDataBuffer buffer, int conversationId)
		{
			return SegmentHelpers.GenerateSegmentHeader(buffer, 1296585556, SegmentHeaderType.Tshm, conversationId, 0, SegmentType.XaRecover, ControlFlag1.FirstSegment | ControlFlag1.LastSegment, ControlFlag2.None);
		}

		// Token: 0x060054CF RID: 21711 RVA: 0x00157C64 File Offset: 0x00155E64
		public void ExtractXids(DynamicDataBuffer buffer, int lengthOfSegmentHeader, bool littleEndian)
		{
			AutomatonQueueManagerContext automatonQueueManagerContext = base.Automaton.Context as AutomatonQueueManagerContext;
			int num = SegmentHelpers.ExtractXaRecoverXidCount(buffer, lengthOfSegmentHeader, littleEndian);
			if (num == 0)
			{
				return;
			}
			Xid[] array = new Xid[num];
			int num2 = lengthOfSegmentHeader + 20;
			for (int i = 0; i < num; i++)
			{
				Xid xid = SegmentHelpers.ExtractXaXid(buffer, num2, littleEndian);
				num2 += 6 + xid.BranchQualifierLength + xid.TransactionIdLength;
				array[i] = xid;
			}
			automatonQueueManagerContext.RecoveredXids = array;
		}

		// Token: 0x060054D0 RID: 21712 RVA: 0x00157CD8 File Offset: 0x00155ED8
		public void SendBufferAsPassthroughClientDataToTcp(DynamicDataBuffer buffer)
		{
			this.SendClientDataToTcp(new PassThroughData
			{
				Buffers = new List<DynamicDataBuffer> { buffer }
			});
		}

		// Token: 0x060054D1 RID: 21713 RVA: 0x00157D04 File Offset: 0x00155F04
		public void SendClientDataToTcp(object contents)
		{
			this.SendToTcp(102, contents);
		}
	}
}
