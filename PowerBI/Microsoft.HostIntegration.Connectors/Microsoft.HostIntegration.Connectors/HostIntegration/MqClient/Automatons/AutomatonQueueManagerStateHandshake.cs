using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A9F RID: 2719
	internal class AutomatonQueueManagerStateHandshake : StateAsCodeDriver
	{
		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x0600551A RID: 21786 RVA: 0x0015964B File Offset: 0x0015784B
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x0015965E File Offset: 0x0015785E
		internal AutomatonQueueManagerStateHandshake(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x0015967C File Offset: 0x0015787C
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
					switch (automatonQueueManagerEvent)
					{
					case AutomatonQueueManagerEvent.TcpDisconnected:
						automatonQueueManagerState = AutomatonQueueManagerState.HandshakeFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 12;
						break;
					case AutomatonQueueManagerEvent.Attached:
					case AutomatonQueueManagerEvent.AttachFailed:
						goto IL_0119;
					case AutomatonQueueManagerEvent.Start:
						this.ActionSendFirstInitialData();
						num = 0;
						break;
					case AutomatonQueueManagerEvent.ServerData:
						this.ActionExtractInitialData();
						if (this.PostConditionIsInitialData())
						{
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.InitialData;
							num = 1;
						}
						else if (this.PostConditionIsStatusDataFailed())
						{
							automatonQueueManagerState = AutomatonQueueManagerState.HandshakeFailed;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StatusData;
							num = 2;
						}
						else
						{
							if (!this.PostConditionIsStatusData())
							{
								throw new InvalidOperationException("No Postcondition returned true");
							}
							automatonQueueManagerState = AutomatonQueueManagerState.Authorization;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.Authorize;
							num = 3;
						}
						break;
					case AutomatonQueueManagerEvent.InitialData:
						this.ActionParseInitialDataReply();
						if (this.PostConditionIsOK())
						{
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.InitialDataInfo;
							num = 4;
						}
						else if (this.PostConditionIsFixableByResend())
						{
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.ReSendInitialData;
							num = 5;
						}
						else
						{
							automatonQueueManagerState = AutomatonQueueManagerState.HandshakeFailed;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.Rejected;
							num = 6;
						}
						break;
					case AutomatonQueueManagerEvent.InitialDataInfo:
						this.ActionSendInitialDataInfo();
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.SendUserId;
						num = 7;
						break;
					case AutomatonQueueManagerEvent.ReSendInitialData:
						this.ActionSendInitialData();
						num = 8;
						break;
					case AutomatonQueueManagerEvent.SendUserId:
						this.ActionSendUserId();
						if (this.PostConditionStatusDataExpected())
						{
							num = 9;
						}
						else if (this.PostConditionNeedsPassword())
						{
							automatonQueueManagerState = AutomatonQueueManagerState.Authorization;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.Authorize;
							num = 10;
						}
						else
						{
							automatonQueueManagerState = AutomatonQueueManagerState.ConnectingQm;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.SendConnect;
							num = 11;
						}
						break;
					default:
						goto IL_0119;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateHandshake.traceLines[num]);
						continue;
					}
					continue;
					IL_0119:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: Handshake, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x00159874 File Offset: 0x00157A74
		public void ActionSendFirstInitialData()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateInitialData(true, buffer);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Sending Initial Data");
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x001598D1 File Offset: 0x00157AD1
		public void ActionExtractInitialData()
		{
			this.automaton.ExtractInitialData((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0]);
		}

		// Token: 0x0600551F RID: 21791 RVA: 0x00159900 File Offset: 0x00157B00
		public void ActionParseInitialDataReply()
		{
			this.context.UsedMaximumTransmissionSize = this.context.MaximumTransmissionSize;
			if (this.context.ServerNumberOfConversationsPerSocket != this.context.NumberOfConversationsPerSocket)
			{
				if (this.context.ServerNumberOfConversationsPerSocket == 0)
				{
					this.context.InitialDataStatus = InitialDataStatus.Broken;
					this.context.RejectedReason = RejectedReason.Conversations0;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
					{
						this.context.TracePoint.Trace(TraceFlags.Error, string.Format(CultureInfo.InvariantCulture, "Rejecting Server value of 0 for Conversations per Socket", Array.Empty<object>()));
					}
					return;
				}
				if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.context.TracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Conversations per socket changed to Server value: " + this.context.ServerNumberOfConversationsPerSocket.ToString(CultureInfo.InvariantCulture), Array.Empty<object>()));
				}
				this.context.NumberOfConversationsPerSocket = this.context.ServerNumberOfConversationsPerSocket;
			}
			if (((this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.ControlFlag1 & ControlFlag1.Error) != ControlFlag1.Error)
			{
				this.context.InitialDataStatus = InitialDataStatus.Ok;
				if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.context.TracePoint.Trace(TraceFlags.Verbose, "Initial Data accepted by Server");
				}
				return;
			}
			if (this.context.ServerErrorFlag2 != InitializationErrorFlag2.None)
			{
				this.context.InitialDataStatus = InitialDataStatus.Broken;
				this.context.RejectedReason = RejectedReason.ErrorFlag2;
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, string.Format(CultureInfo.InvariantCulture, "Initial Data rejected with ServerErrorFlag2: {0}", (byte)this.context.ServerErrorFlag2));
				}
				return;
			}
			if ((this.context.ServerErrorFlag1 & InitializationErrorFlag1.Ccsid) == InitializationErrorFlag1.Ccsid)
			{
				this.context.InitialDataStatus = InitialDataStatus.Broken;
				this.context.RejectedReason = RejectedReason.Ccsid;
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, "Initial Data rejected: CCSID");
				}
				return;
			}
			if ((this.context.ServerErrorFlag1 & InitializationErrorFlag1.Encoding) == InitializationErrorFlag1.Encoding)
			{
				this.context.InitialDataStatus = InitialDataStatus.Broken;
				this.context.RejectedReason = RejectedReason.Encoding;
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, "Initial Data rejected: Encoding");
				}
				return;
			}
			if ((this.context.ServerErrorFlag1 & InitializationErrorFlag1.FapLevel) == InitializationErrorFlag1.FapLevel && this.context.ServerFapLevel < 11)
			{
				this.context.InitialDataStatus = InitialDataStatus.Broken;
				this.context.RejectedReason = RejectedReason.Fap;
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, string.Format(CultureInfo.InvariantCulture, "Initial Data rejected with FAP Level, Server Level: {0}", this.context.ServerFapLevel));
				}
				return;
			}
			if ((this.context.ServerErrorFlag1 & InitializationErrorFlag1.MaximumTransmissionSize) == InitializationErrorFlag1.MaximumTransmissionSize && this.context.ServerMaximumTransmissionSize > 32768)
			{
				this.context.InitialDataStatus = InitialDataStatus.Broken;
				this.context.RejectedReason = RejectedReason.TransmissionSize;
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, string.Format(CultureInfo.InvariantCulture, "Initial Data rejected with Max Transmission Size, Server value: {0}", this.context.ServerMaximumTransmissionSize));
				}
				return;
			}
			this.context.InitialDataStatus = InitialDataStatus.FixableByResend;
			if ((this.context.ServerErrorFlag1 & InitializationErrorFlag1.FapLevel) == InitializationErrorFlag1.FapLevel)
			{
				this.context.FapLevel = this.context.ServerFapLevel;
				if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.context.TracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Initial Data rejected by Server: FAP Level. Changing to Server value: {0}", this.context.ServerFapLevel));
				}
			}
			if ((this.context.ServerErrorFlag1 & InitializationErrorFlag1.MaximumMessageBatch) == InitializationErrorFlag1.MaximumMessageBatch)
			{
				this.context.MaximumMessageBatch = this.context.ServerMaximumMessageBatch;
				if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.context.TracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Initial Data rejected by Server: Max Message Batch. Changing to Server value: {0}", this.context.ServerMaximumMessageBatch));
				}
			}
			if ((this.context.ServerErrorFlag1 & InitializationErrorFlag1.MaximumTransmissionSize) == InitializationErrorFlag1.MaximumTransmissionSize)
			{
				this.context.MaximumTransmissionSize = this.context.ServerMaximumTransmissionSize;
				if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.context.TracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Initial Data rejected by Server: Max Transmission Size. Changing to Server value: {0}", this.context.ServerMaximumTransmissionSize));
				}
				this.context.UsedMaximumTransmissionSize = this.context.MaximumTransmissionSize;
			}
			if ((this.context.ServerErrorFlag1 & InitializationErrorFlag1.MessageSize) == InitializationErrorFlag1.MessageSize)
			{
				this.context.MaximumMessageSize = this.context.ServerMaximumMessageSize;
				if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.context.TracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Initial Data rejected by Server: Max Message Size. Changing to Server value: {0}", this.context.ServerMaximumMessageSize));
				}
			}
			if ((this.context.ServerErrorFlag1 & InitializationErrorFlag1.SequenceWrap) == InitializationErrorFlag1.SequenceWrap)
			{
				this.context.SequenceNumberWrap = this.context.ServerSequenceNumberWrap;
				if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.context.TracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Initial Data rejected by Server: Sequence Number Wrap. Changing to Server value: {0}", this.context.ServerSequenceNumberWrap));
				}
			}
			if ((this.context.ServerErrorFlag1 & InitializationErrorFlag1.HeartBeatInterval) == InitializationErrorFlag1.HeartBeatInterval)
			{
				this.context.HeartBeatInterval = this.context.ServerHeartBeatInterval;
				if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.context.TracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Initial Data rejected by Server: Heartbeat Interval. Changing to Server value: {0}", this.context.ServerHeartBeatInterval));
				}
			}
			if ((this.context.ServerCapabilityFlag2 & CapabilityFlag2.FastMessageRequest) == CapabilityFlag2.None)
			{
				this.context.CapabilityFlag2 = CapabilityFlag2.ResponderConversion | CapabilityFlag2.SpiRequest;
				if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.context.TracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Server doesn't support FastMessageRequest, removing from our value", Array.Empty<object>()));
				}
			}
		}

		// Token: 0x06005520 RID: 21792 RVA: 0x00159F74 File Offset: 0x00158174
		public void ActionSendInitialData()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateInitialData(false, buffer);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Resending Initial Data");
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x06005521 RID: 21793 RVA: 0x00159FD4 File Offset: 0x001581D4
		public void ActionSendInitialDataInfo()
		{
			QmInitialDataInfo qmInitialDataInfo = new QmInitialDataInfo();
			qmInitialDataInfo.ChannelParameters = new ChannelParameters(this.context);
			this.automaton.SendToTcp(103, qmInitialDataInfo);
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x0015A008 File Offset: 0x00158208
		public void ActionSendUserId()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateUserId(buffer);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Sending User ID");
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x06005523 RID: 21795 RVA: 0x0015A064 File Offset: 0x00158264
		public bool PostConditionIsInitialData()
		{
			return this.context.IsInitialData;
		}

		// Token: 0x06005524 RID: 21796 RVA: 0x0015A071 File Offset: 0x00158271
		public bool PostConditionIsStatusDataFailed()
		{
			return this.context.IsStatusData && this.context.StatusDataType > StatusDataType.Unknown;
		}

		// Token: 0x06005525 RID: 21797 RVA: 0x0015A090 File Offset: 0x00158290
		public bool PostConditionIsStatusData()
		{
			return this.context.IsStatusData;
		}

		// Token: 0x06005526 RID: 21798 RVA: 0x0015A09D File Offset: 0x0015829D
		public bool PostConditionIsOK()
		{
			return this.context.InitialDataStatus == InitialDataStatus.Ok;
		}

		// Token: 0x06005527 RID: 21799 RVA: 0x0015A0AD File Offset: 0x001582AD
		public bool PostConditionIsFixableByResend()
		{
			return this.context.InitialDataStatus == InitialDataStatus.FixableByResend;
		}

		// Token: 0x06005528 RID: 21800 RVA: 0x0015A0BD File Offset: 0x001582BD
		public bool PostConditionStatusDataExpected()
		{
			return this.context.SrvConSecurityBitSet;
		}

		// Token: 0x06005529 RID: 21801 RVA: 0x0015A0CA File Offset: 0x001582CA
		public bool PostConditionNeedsPassword()
		{
			return this.context.ConnectionParameters.AuthorizationPassword != null;
		}

		// Token: 0x040043A4 RID: 17316
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.Handshake;

		// Token: 0x040043A5 RID: 17317
		private AutomatonQueueManager automaton;

		// Token: 0x040043A6 RID: 17318
		private AutomatonQueueManagerContext context;

		// Token: 0x040043A7 RID: 17319
		private static string[] traceLines = new string[]
		{
			"State: Handshake, Evt: Start, Act: SendFirstInitialData, Stop", "State: Handshake, Evt: ServerData, Act: ExtractInitialData, Post: IsInitialData, Evt: InitialData", "State: Handshake, Evt: ServerData, Act: ExtractInitialData, Post: IsStatusDataFailed, State: HandshakeFailed, Evt: StatusData", "State: Handshake, Evt: ServerData, Act: ExtractInitialData, Post: IsStatusData, State: Authorization, Evt: Authorize", "State: Handshake, Evt: InitialData, Act: ParseInitialDataReply, Post: IsOK, Evt: InitialDataInfo", "State: Handshake, Evt: InitialData, Act: ParseInitialDataReply, Post: IsFixableByResend, Evt: ReSendInitialData", "State: Handshake, Evt: InitialData, Act: ParseInitialDataReply, State: HandshakeFailed, Evt: Rejected", "State: Handshake, Evt: InitialDataInfo, Act: SendInitialDataInfo, Evt: SendUserId", "State: Handshake, Evt: ReSendInitialData, Act: SendInitialData, Stop", "State: Handshake, Evt: SendUserId, Act: SendUserId, Post: StatusDataExpected, Stop",
			"State: Handshake, Evt: SendUserId, Act: SendUserId, Post: NeedsPassword, State: Authorization, Evt: Authorize", "State: Handshake, Evt: SendUserId, Act: SendUserId, State: ConnectingQm, Evt: SendConnect", "State: Handshake, Evt: TcpDisconnected, State: HandshakeFailed, Evt: TcpFailed"
		};
	}
}
