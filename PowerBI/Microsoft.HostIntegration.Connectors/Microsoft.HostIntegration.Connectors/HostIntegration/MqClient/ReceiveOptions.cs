using System;
using System.Globalization;
using System.Text;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B5B RID: 2907
	public class ReceiveOptions
	{
		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x06005C4B RID: 23627 RVA: 0x0017CF8F File Offset: 0x0017B18F
		// (set) Token: 0x06005C4C RID: 23628 RVA: 0x0017CF97 File Offset: 0x0017B197
		public ReceiveOption Options { get; set; }

		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x06005C4D RID: 23629 RVA: 0x0017CFA0 File Offset: 0x0017B1A0
		// (set) Token: 0x06005C4E RID: 23630 RVA: 0x0017CFA8 File Offset: 0x0017B1A8
		public int Timeout { get; set; }

		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x06005C4F RID: 23631 RVA: 0x0017CFB1 File Offset: 0x0017B1B1
		// (set) Token: 0x06005C50 RID: 23632 RVA: 0x0017CFB9 File Offset: 0x0017B1B9
		public bool Wait { get; set; }

		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x06005C51 RID: 23633 RVA: 0x0017CFC2 File Offset: 0x0017B1C2
		// (set) Token: 0x06005C52 RID: 23634 RVA: 0x0017CFCA File Offset: 0x0017B1CA
		public MatchOption MatchOptions { get; set; }

		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x06005C53 RID: 23635 RVA: 0x0017CFD3 File Offset: 0x0017B1D3
		// (set) Token: 0x06005C54 RID: 23636 RVA: 0x0017CFDB File Offset: 0x0017B1DB
		public Priority Priority { get; set; }

		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06005C55 RID: 23637 RVA: 0x0017CFE4 File Offset: 0x0017B1E4
		// (set) Token: 0x06005C56 RID: 23638 RVA: 0x0017CFEC File Offset: 0x0017B1EC
		public byte[] Correlator
		{
			get
			{
				return this.correlator;
			}
			set
			{
				this.correlator = Globals.CheckExactLength(value, "Correlator", 24);
			}
		}

		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06005C57 RID: 23639 RVA: 0x0017D001 File Offset: 0x0017B201
		// (set) Token: 0x06005C58 RID: 23640 RVA: 0x0017D009 File Offset: 0x0017B209
		public byte[] MessageId
		{
			get
			{
				return this.messageId;
			}
			set
			{
				this.messageId = Globals.CheckExactLength(value, "MessageId", 24);
			}
		}

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x06005C59 RID: 23641 RVA: 0x0017D01E File Offset: 0x0017B21E
		// (set) Token: 0x06005C5A RID: 23642 RVA: 0x0017D026 File Offset: 0x0017B226
		public byte[] GroupId
		{
			get
			{
				return this.groupId;
			}
			set
			{
				this.groupId = Globals.CheckExactLength(value, "GroupId", 24);
			}
		}

		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x06005C5B RID: 23643 RVA: 0x0017D03B File Offset: 0x0017B23B
		// (set) Token: 0x06005C5C RID: 23644 RVA: 0x0017D043 File Offset: 0x0017B243
		public byte[] Token
		{
			get
			{
				return this.token;
			}
			set
			{
				this.token = Globals.CheckExactLength(value, "Token", 16);
			}
		}

		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x06005C5D RID: 23645 RVA: 0x0017D058 File Offset: 0x0017B258
		// (set) Token: 0x06005C5E RID: 23646 RVA: 0x0017D060 File Offset: 0x0017B260
		public int SequenceNumber
		{
			get
			{
				return this.sequenceNumber;
			}
			set
			{
				this.sequenceNumber = Globals.CheckRange(value, "SequenceNumber", 1, 999999999);
			}
		}

		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x06005C5F RID: 23647 RVA: 0x0017D079 File Offset: 0x0017B279
		// (set) Token: 0x06005C60 RID: 23648 RVA: 0x0017D081 File Offset: 0x0017B281
		public int Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = Globals.CheckRange(value, "Offset", 0, 999999999);
			}
		}

		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x06005C61 RID: 23649 RVA: 0x0017D09A File Offset: 0x0017B29A
		// (set) Token: 0x06005C62 RID: 23650 RVA: 0x0017D0A2 File Offset: 0x0017B2A2
		public int TruncationSize
		{
			get
			{
				return this.maximumSizeOfReceive;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("TruncationSize", SR.PositiveOrNoTruncation);
				}
				this.maximumSizeOfReceive = value;
			}
		}

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x06005C63 RID: 23651 RVA: 0x0017D0C3 File Offset: 0x0017B2C3
		// (set) Token: 0x06005C64 RID: 23652 RVA: 0x0017D0CB File Offset: 0x0017B2CB
		public string ResolvedQueue { get; private set; }

		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x06005C65 RID: 23653 RVA: 0x0017D0D4 File Offset: 0x0017B2D4
		// (set) Token: 0x06005C66 RID: 23654 RVA: 0x0017D0DC File Offset: 0x0017B2DC
		public GroupStatus GroupStatus { get; set; }

		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x06005C67 RID: 23655 RVA: 0x0017D0E5 File Offset: 0x0017B2E5
		// (set) Token: 0x06005C68 RID: 23656 RVA: 0x0017D0ED File Offset: 0x0017B2ED
		public SegmentStatus SegmentStatus { get; set; }

		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x06005C69 RID: 23657 RVA: 0x0017D0F6 File Offset: 0x0017B2F6
		// (set) Token: 0x06005C6A RID: 23658 RVA: 0x0017D0FE File Offset: 0x0017B2FE
		public bool SegmentationAllowed { get; set; }

		// Token: 0x06005C6B RID: 23659 RVA: 0x0017D107 File Offset: 0x0017B307
		public ReceiveOptions()
			: this(ReceiveOption.None, 0, false)
		{
		}

		// Token: 0x06005C6C RID: 23660 RVA: 0x0017D112 File Offset: 0x0017B312
		public ReceiveOptions(ReceiveOption options)
			: this(options, 0, false)
		{
		}

		// Token: 0x06005C6D RID: 23661 RVA: 0x0017D11D File Offset: 0x0017B31D
		public ReceiveOptions(ReceiveOption options, int timeoutInMilliseconds)
			: this(options, timeoutInMilliseconds, false)
		{
		}

		// Token: 0x06005C6E RID: 23662 RVA: 0x0017D128 File Offset: 0x0017B328
		public ReceiveOptions(ReceiveOption options, int timeoutInMilliseconds, bool wait)
		{
			this.Options = options;
			this.Timeout = timeoutInMilliseconds;
			this.Wait = wait;
		}

		// Token: 0x06005C6F RID: 23663 RVA: 0x0017D154 File Offset: 0x0017B354
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			if (this.Options != ReceiveOption.None)
			{
				stringBuilder.AppendFormat("Options: {0}, ", this.Options.ToString());
			}
			stringBuilder.AppendFormat("Timeout: {0}", this.Timeout.ToString(CultureInfo.InvariantCulture));
			if (this.Correlator != null && this.Correlator.Length != 0)
			{
				stringBuilder.AppendFormat(", Correlator: {0}", this.Correlator.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005C70 RID: 23664 RVA: 0x0017D1E0 File Offset: 0x0017B3E0
		public unsafe void ExtractFromAsyncMessage(byte[] buffer, int offset, bool littleEndian, int ccsid, ReceiveMessage receiveMessage)
		{
			HisEncoding encoding = HisEncoding.GetEncoding(ccsid);
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				ptr3 += 9;
				byte* ptr4 = (byte*)ptr3;
				byte[] array = new byte[16];
				bool flag = false;
				for (int i = 0; i < 16; i++)
				{
					array[i] = *(ptr4++);
					if (array[i] != 0)
					{
						flag = true;
					}
				}
				receiveMessage.Token = (flag ? array : null);
				ptr4 += 2;
				int num = (int)(*(ptr4++));
				int num2 = offset + (int)((long)(ptr4 - ptr2));
				this.ResolvedQueue = ConversionHelpers.GetStringOrNull(buffer, num2, num, encoding);
			}
		}

		// Token: 0x06005C71 RID: 23665 RVA: 0x0017D284 File Offset: 0x0017B484
		public unsafe int ExtractFromGetReply(byte[] buffer, int offset, bool littleEndian, int ccsid, ReceiveMessage receiveMessage)
		{
			HisEncoding encoding = HisEncoding.GetEncoding(ccsid);
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				ptr3++;
				int num = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				ptr3 += 4;
				byte* ptr4 = (byte*)ptr3;
				int num2 = offset + (int)((long)(ptr4 - ptr2));
				this.ResolvedQueue = ConversionHelpers.GetStringOrNull(buffer, num2, 48, encoding);
				ptr4 += 48;
				if (num == 1)
				{
					return (int)((long)(ptr4 - ptr2));
				}
				ptr3 = (int*)ptr4;
				ptr3++;
				ptr3++;
				ptr4 = (byte*)ptr3;
				if (num == 2)
				{
					return (int)((long)(ptr4 - ptr2));
				}
				byte[] array = new byte[16];
				bool flag = false;
				for (int i = 0; i < 16; i++)
				{
					array[i] = *(ptr4++);
					if (array[i] != 0)
					{
						flag = true;
					}
				}
				receiveMessage.Token = (flag ? array : null);
				ptr4 += 4;
				if (num == 3)
				{
					return (int)((long)(ptr4 - ptr2));
				}
				ptr4 += 8;
				return (int)((long)(ptr4 - ptr2));
			}
		}

		// Token: 0x0400484C RID: 18508
		public const int NoTruncation = -1;

		// Token: 0x0400484D RID: 18509
		public const int InfiniteTimeout = -1;

		// Token: 0x04004853 RID: 18515
		private byte[] correlator;

		// Token: 0x04004854 RID: 18516
		private byte[] messageId;

		// Token: 0x04004855 RID: 18517
		private byte[] groupId;

		// Token: 0x04004856 RID: 18518
		private byte[] token;

		// Token: 0x04004857 RID: 18519
		private int sequenceNumber = 1;

		// Token: 0x04004858 RID: 18520
		private int offset;

		// Token: 0x04004859 RID: 18521
		private int maximumSizeOfReceive = -1;
	}
}
