using System;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B2A RID: 2858
	public class ReceiveMessage : Message
	{
		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x060059EA RID: 23018 RVA: 0x00172E38 File Offset: 0x00171038
		// (set) Token: 0x060059EB RID: 23019 RVA: 0x00172E40 File Offset: 0x00171040
		public ReceiveOptions Options { get; set; }

		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x060059EC RID: 23020 RVA: 0x00172E49 File Offset: 0x00171049
		// (set) Token: 0x060059ED RID: 23021 RVA: 0x00172E51 File Offset: 0x00171051
		public byte[] Token { get; internal set; }

		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x060059EE RID: 23022 RVA: 0x00172E5A File Offset: 0x0017105A
		// (set) Token: 0x060059EF RID: 23023 RVA: 0x00172E62 File Offset: 0x00171062
		public int OriginalLength { get; set; }

		// Token: 0x060059F0 RID: 23024 RVA: 0x00172E6B File Offset: 0x0017106B
		public ReceiveMessage(ReceiveOptions options)
			: base(MessageType.Datagram, null)
		{
			this.Options = options;
		}

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x060059F1 RID: 23025 RVA: 0x00172E7C File Offset: 0x0017107C
		// (set) Token: 0x060059F2 RID: 23026 RVA: 0x00172E84 File Offset: 0x00171084
		internal Queue ReceiveQueue { get; set; }

		// Token: 0x060059F3 RID: 23027 RVA: 0x00172E90 File Offset: 0x00171090
		public unsafe static int GenerateRequestMessage(byte[] buffer, int offset, int objectHandle, bool openedWithReadAhead, int maximumMessageSize, ReceiveOptions options, bool forceZeroTimeout, int bytesReceived, int bytesRequested, int globalMessageId, bool followUpRequest, bool queueIsTransactional)
		{
			ReceiveOption receiveOption = ReceiveOption.FailOnQuiesce;
			int num = -1;
			int num2 = 1;
			int num3 = 0;
			MatchOption matchOption = MatchOption.None;
			if (options != null)
			{
				receiveOption = options.Options;
				if (queueIsTransactional)
				{
					receiveOption |= (ReceiveOption)2;
				}
				else
				{
					receiveOption |= (ReceiveOption)4;
				}
				receiveOption |= (ReceiveOption)1;
				num = ((!forceZeroTimeout && options.Wait) ? options.Timeout : 0);
				num2 = options.SequenceNumber;
				num3 = options.Offset;
				matchOption = options.MatchOptions;
				if (options.TruncationSize != -1)
				{
					receiveOption |= (ReceiveOption)64;
					maximumMessageSize = options.TruncationSize;
				}
			}
			else
			{
				receiveOption |= (ReceiveOption)4;
			}
			if ((receiveOption & ReceiveOption.Unlock) == ReceiveOption.None)
			{
				receiveOption |= (ReceiveOption)33554432;
			}
			bool flag = matchOption == MatchOption.None;
			int num4;
			if (openedWithReadAhead)
			{
				num4 = (forceZeroTimeout ? 10 : 8);
				if (followUpRequest && !forceZeroTimeout)
				{
					num4 = 0;
				}
			}
			else
			{
				num4 = ((num == 0) ? 14 : 8);
			}
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = 1;
				*(ptr3++) = objectHandle;
				*(ptr3++) = bytesReceived;
				*(ptr3++) = bytesRequested;
				*(ptr3++) = maximumMessageSize;
				*(ptr3++) = (int)receiveOption;
				*(ptr3++) = num;
				*(ptr3++) = 0;
				if (!flag)
				{
					num4 += 16;
				}
				*(ptr3++) = num4;
				*(ptr3++) = globalMessageId;
				byte* ptr4 = (byte*)ptr3;
				if (flag)
				{
					return (int)((long)(ptr4 - ptr2));
				}
				short* ptr5 = (short*)ptr3;
				*(ptr5++) = 1;
				*(ptr5++) = 2;
				ptr3 = (int*)ptr5;
				*(ptr3++) = 0;
				*(ptr3++) = 546;
				*(ptr3++) = num2;
				*(ptr3++) = num3;
				*(ptr3++) = (int)matchOption;
				ptr4 = (byte*)ptr3;
				if ((matchOption & MatchOption.MessageId) == MatchOption.MessageId)
				{
					if (options.MessageId == null)
					{
						for (int i = 0; i < 24; i++)
						{
							*(ptr4++) = 0;
						}
					}
					else
					{
						for (int j = 0; j < 24; j++)
						{
							*(ptr4++) = options.MessageId[j];
						}
					}
				}
				if ((matchOption & MatchOption.Correlator) == MatchOption.Correlator)
				{
					if (options.Correlator == null)
					{
						for (int k = 0; k < 24; k++)
						{
							*(ptr4++) = 0;
						}
					}
					else
					{
						for (int l = 0; l < 24; l++)
						{
							*(ptr4++) = options.Correlator[l];
						}
					}
				}
				if ((matchOption & MatchOption.GroupId) == MatchOption.GroupId)
				{
					if (options.GroupId == null)
					{
						for (int m = 0; m < 24; m++)
						{
							*(ptr4++) = 0;
						}
					}
					else
					{
						for (int n = 0; n < 24; n++)
						{
							*(ptr4++) = options.GroupId[n];
						}
					}
				}
				if ((matchOption & MatchOption.Token) == MatchOption.Token)
				{
					if (options.Token == null)
					{
						for (int num5 = 0; num5 < 16; num5++)
						{
							*(ptr4++) = 0;
						}
					}
					else
					{
						for (int num6 = 0; num6 < 16; num6++)
						{
							*(ptr4++) = options.Token[num6];
						}
					}
				}
				return (int)((long)(ptr4 - ptr2));
			}
		}

		// Token: 0x060059F4 RID: 23028 RVA: 0x0017317C File Offset: 0x0017137C
		public unsafe static int GenerateMqmd(byte[] buffer, int offset, ReceiveOptions options)
		{
			int num = 2;
			byte[] array = null;
			byte[] array2 = null;
			byte[] array3 = null;
			int num2 = 1;
			int num3 = 0;
			if (options != null)
			{
				MatchOption matchOptions = options.MatchOptions;
				if ((matchOptions & MatchOption.GroupId) == MatchOption.GroupId)
				{
					array = options.GroupId;
				}
				if ((matchOptions & MatchOption.SequenceNumber) == MatchOption.SequenceNumber)
				{
					num2 = options.SequenceNumber;
				}
				if ((matchOptions & MatchOption.Offset) == MatchOption.Offset)
				{
					num3 = options.Offset;
				}
				if ((matchOptions & MatchOption.MessageId) == MatchOption.MessageId)
				{
					array2 = options.MessageId;
				}
				if ((matchOptions & MatchOption.Correlator) == MatchOption.Correlator)
				{
					array3 = options.Correlator;
				}
			}
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = 538985549;
				*(ptr3++) = num;
				*(ptr3++) = 0;
				*(ptr3++) = 8;
				*(ptr3++) = -1;
				*(ptr3++) = 0;
				*(ptr3++) = 546;
				*(ptr3++) = 0;
				byte* ptr4 = (byte*)ptr3;
				for (int i = 0; i < 8; i++)
				{
					*(ptr4++) = 32;
				}
				ptr3 = (int*)ptr4;
				*(ptr3++) = (int)options.Priority;
				*(ptr3++) = 2;
				ptr4 = (byte*)ptr3;
				if (array2 == null)
				{
					for (int j = 0; j < 24; j++)
					{
						*(ptr4++) = 0;
					}
				}
				else
				{
					for (int k = 0; k < 24; k++)
					{
						*(ptr4++) = array2[k];
					}
				}
				if (array3 == null)
				{
					for (int l = 0; l < 24; l++)
					{
						*(ptr4++) = 0;
					}
				}
				else
				{
					for (int m = 0; m < 24; m++)
					{
						*(ptr4++) = array3[m];
					}
				}
				ptr3 = (int*)ptr4;
				*(ptr3++) = 0;
				ptr4 = (byte*)ptr3;
				*ptr4 = 0;
				ptr4 += 48;
				*ptr4 = 0;
				ptr4 += 48;
				ptr4 += IdentityContext.GenerateMqmdBytes(null, buffer, (int)((long)(ptr4 - ptr2) + (long)offset));
				ptr4 += OriginContext.GenerateMqmdBytes(null, buffer, (int)((long)(ptr4 - ptr2) + (long)offset));
				if (array == null)
				{
					for (int n = 0; n < 24; n++)
					{
						*(ptr4++) = 0;
					}
				}
				else
				{
					for (int num4 = 0; num4 < 24; num4++)
					{
						*(ptr4++) = array[num4];
					}
				}
				ptr3 = (int*)ptr4;
				*(ptr3++) = num2;
				*(ptr3++) = num3;
				*(ptr3++) = 0;
				*(ptr3++) = -1;
				ptr4 = (byte*)ptr3;
				return (int)((long)(ptr4 - ptr2));
			}
		}

		// Token: 0x060059F5 RID: 23029 RVA: 0x001733C8 File Offset: 0x001715C8
		public unsafe int ExtractMqmd(byte[] buffer, int numberOfBytesAvailable, int offset, bool littleEndian, int ccsid, out int originalLengthOfMessage)
		{
			HisEncoding encoding = HisEncoding.GetEncoding(ccsid);
			numberOfBytesAvailable -= offset;
			numberOfBytesAvailable -= 324;
			if (numberOfBytesAvailable < 0)
			{
				throw new InvalidOperationException("MQMD V1 doesn't fit in the remaining data!");
			}
			int num4;
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				ptr3++;
				int num = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				base.Report = (Report)ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				base.MessageType = (MessageType)ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				base.ExpirationInterval = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				ptr3++;
				this.numericEncodingToUse = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				this.ccsidToUse = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				byte* ptr4 = (byte*)ptr3;
				int num2 = offset + (int)((long)(ptr4 - ptr2));
				base.Format = ConversionHelpers.GetStringOrNull(buffer, num2, 8, encoding);
				ptr4 += 8;
				ptr3 = (int*)ptr4;
				base.Priority = (Priority)ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				base.Persistence = (Persistence)ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				ptr4 = (byte*)ptr3;
				byte[] array = new byte[24];
				bool flag = false;
				for (int i = 0; i < 24; i++)
				{
					array[i] = *(ptr4++);
					if (array[i] != 0)
					{
						flag = true;
					}
				}
				base.MessageId = (flag ? array : null);
				byte[] array2 = (flag ? new byte[24] : array);
				flag = false;
				for (int j = 0; j < 24; j++)
				{
					array2[j] = *(ptr4++);
					if (array2[j] != 0)
					{
						flag = true;
					}
				}
				base.Correlator = (flag ? array2 : null);
				ptr3 = (int*)ptr4;
				ptr3++;
				ptr4 = (byte*)ptr3;
				num2 = offset + (int)((long)(ptr4 - ptr2));
				base.ReplyToQueue = ConversionHelpers.GetStringOrNull(buffer, num2, 48, encoding);
				ptr4 += 48;
				num2 = offset + (int)((long)(ptr4 - ptr2));
				base.ReplyToQueueManager = ConversionHelpers.GetStringOrNull(buffer, num2, 48, encoding);
				ptr4 += 48;
				int num3;
				this.identityContext = IdentityContext.ExtractMqmd(buffer, (int)((long)(ptr4 - ptr2) + (long)offset), littleEndian, encoding, out num3);
				ptr4 += num3;
				this.originContext = OriginContext.ExtractMqmd(buffer, (int)((long)(ptr4 - ptr2) + (long)offset), littleEndian, encoding, out num3);
				ptr4 += num3;
				if (num > 1)
				{
					numberOfBytesAvailable -= 40;
					if (numberOfBytesAvailable < 0)
					{
						throw new InvalidOperationException("MQMD V2 doesn't fit in the remaining data!");
					}
					byte[] array3 = (flag ? new byte[24] : array2);
					flag = false;
					for (int k = 0; k < 24; k++)
					{
						array3[k] = *(ptr4++);
						if (array3[k] != 0)
						{
							flag = true;
						}
					}
					base.GroupId = (flag ? array3 : null);
					ptr3 = (int*)ptr4;
					base.SequenceNumber = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
					base.Offset = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
					base.MessageFlags = (MessageFlags)ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
					originalLengthOfMessage = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
					ptr4 = (byte*)ptr3;
				}
				else if (base.Format == "MQHMDE" && numberOfBytesAvailable >= 72)
				{
					byte* ptr5 = ptr4;
					ptr3 = (int*)ptr5;
					ptr3 += 5;
					ptr5 = (byte*)ptr3;
					ptr5 += 8;
					ptr3 = (int*)ptr5;
					ptr3++;
					ptr5 = (byte*)ptr3;
					byte[] array4 = (flag ? new byte[24] : array2);
					flag = false;
					for (int l = 0; l < 24; l++)
					{
						array4[l] = *(ptr5++);
						if (array4[l] != 0)
						{
							flag = true;
						}
					}
					base.GroupId = (flag ? array4 : null);
					ptr3 = (int*)ptr5;
					bool flag2 = NumericEncoding.EncodingValueIsLittleEndian(this.numericEncodingToUse);
					base.SequenceNumber = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag2);
					base.Offset = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag2);
					base.MessageFlags = (MessageFlags)ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag2);
					originalLengthOfMessage = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag2);
				}
				else
				{
					base.GroupId = null;
					base.SequenceNumber = 1;
					base.Offset = 0;
					base.MessageFlags = MessageFlags.None;
					originalLengthOfMessage = -1;
				}
				num4 = (int)((long)(ptr4 - ptr2));
			}
			if (this.Options != null)
			{
				this.Options.Correlator = base.Correlator;
				this.Options.MessageId = base.MessageId;
				this.Options.SequenceNumber = base.SequenceNumber;
				this.Options.Offset = base.Offset;
				this.Options.SegmentationAllowed = (base.MessageFlags & MessageFlags.SegmentationAllowed) == MessageFlags.SegmentationAllowed;
				if ((base.MessageFlags & MessageFlags.LastMessageInAGroup) == MessageFlags.LastMessageInAGroup)
				{
					this.Options.GroupStatus = GroupStatus.LastMessageInAGroup;
				}
				else if ((base.MessageFlags & MessageFlags.MessageInAGroup) == MessageFlags.MessageInAGroup)
				{
					this.Options.GroupStatus = GroupStatus.MessageInAGroup;
				}
				else
				{
					this.Options.GroupStatus = GroupStatus.NotInAGroup;
				}
				if ((base.MessageFlags & MessageFlags.LastSegment) == MessageFlags.LastSegment)
				{
					this.Options.SegmentStatus = SegmentStatus.LastSegment;
					return num4;
				}
				if ((base.MessageFlags & MessageFlags.Segment) == MessageFlags.Segment)
				{
					this.Options.SegmentStatus = SegmentStatus.Segment;
					return num4;
				}
				this.Options.SegmentStatus = SegmentStatus.NotASegment;
			}
			return num4;
		}
	}
}
