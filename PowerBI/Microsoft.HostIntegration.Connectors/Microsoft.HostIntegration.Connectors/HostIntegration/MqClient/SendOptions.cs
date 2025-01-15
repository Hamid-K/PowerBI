using System;
using System.Text;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B5C RID: 2908
	public class SendOptions
	{
		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x06005C72 RID: 23666 RVA: 0x0017D37C File Offset: 0x0017B57C
		// (set) Token: 0x06005C73 RID: 23667 RVA: 0x0017D384 File Offset: 0x0017B584
		public SendOption Options { get; set; }

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x06005C74 RID: 23668 RVA: 0x0017D38D File Offset: 0x0017B58D
		// (set) Token: 0x06005C75 RID: 23669 RVA: 0x0017D395 File Offset: 0x0017B595
		public bool AsynchronousResponse { get; set; }

		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x06005C76 RID: 23670 RVA: 0x0017D39E File Offset: 0x0017B59E
		// (set) Token: 0x06005C77 RID: 23671 RVA: 0x0017D3A6 File Offset: 0x0017B5A6
		public int LocalDestinationCount { get; private set; }

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x06005C78 RID: 23672 RVA: 0x0017D3AF File Offset: 0x0017B5AF
		// (set) Token: 0x06005C79 RID: 23673 RVA: 0x0017D3B7 File Offset: 0x0017B5B7
		public int RemoteDestinationCount { get; private set; }

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x06005C7A RID: 23674 RVA: 0x0017D3C0 File Offset: 0x0017B5C0
		// (set) Token: 0x06005C7B RID: 23675 RVA: 0x0017D3C8 File Offset: 0x0017B5C8
		public int InvalidDestinationCount { get; private set; }

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x06005C7C RID: 23676 RVA: 0x0017D3D1 File Offset: 0x0017B5D1
		// (set) Token: 0x06005C7D RID: 23677 RVA: 0x0017D3D9 File Offset: 0x0017B5D9
		public string ResolvedQueueManager { get; private set; }

		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x06005C7E RID: 23678 RVA: 0x0017D3E2 File Offset: 0x0017B5E2
		// (set) Token: 0x06005C7F RID: 23679 RVA: 0x0017D3EA File Offset: 0x0017B5EA
		public string ResolvedQueue { get; private set; }

		// Token: 0x06005C80 RID: 23680 RVA: 0x0017D3F3 File Offset: 0x0017B5F3
		public SendOptions()
			: this(SendOption.None)
		{
		}

		// Token: 0x06005C81 RID: 23681 RVA: 0x0017D3FC File Offset: 0x0017B5FC
		public SendOptions(SendOption options)
		{
			this.Options = options;
		}

		// Token: 0x06005C82 RID: 23682 RVA: 0x0017D40C File Offset: 0x0017B60C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			if (this.Options != SendOption.None)
			{
				stringBuilder.AppendFormat("Options: {0}, ", this.Options.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005C83 RID: 23683 RVA: 0x0017D450 File Offset: 0x0017B650
		public unsafe void ExtractFromPutReply(byte[] buffer, int offset, bool littleEndian, int ccsid)
		{
			HisEncoding encoding = HisEncoding.GetEncoding(ccsid);
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				ptr3 += 5;
				this.LocalDestinationCount = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				this.RemoteDestinationCount = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				this.InvalidDestinationCount = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				byte* ptr4 = (byte*)ptr3;
				int num = offset + (int)((long)(ptr4 - ptr2));
				this.ResolvedQueue = ConversionHelpers.GetStringOrNull(buffer, num, 48, encoding);
				ptr4 += 48;
				num = offset + (int)((long)(ptr4 - ptr2));
				this.ResolvedQueueManager = ConversionHelpers.GetStringOrNull(buffer, num, 48, encoding);
			}
		}

		// Token: 0x06005C84 RID: 23684 RVA: 0x0017D4EC File Offset: 0x0017B6EC
		public unsafe void GenerateBytes(byte[] buffer, int offset, SendMessage message, bool queueIsTransactional)
		{
			fixed (byte* ptr = &buffer[offset])
			{
				int* ptr2 = (int*)ptr;
				*(ptr2++) = 542068048;
				*(ptr2++) = 1;
				int num = (int)this.Options;
				if (queueIsTransactional)
				{
					num |= 2;
				}
				else
				{
					num |= 4;
				}
				if (this.AsynchronousResponse)
				{
					num |= 65536;
				}
				else
				{
					num |= 131072;
				}
				if (message.ContextOption != ContextOption.Default && message.ContextOption != ContextOption.None)
				{
					num |= (int)message.ContextOption;
				}
				*(ptr2++) = num;
				*(ptr2++) = -1;
				ReceiveMessage receivedMessage = message.ReceivedMessage;
				int num2 = 0;
				if (receivedMessage != null)
				{
					num2 = receivedMessage.ReceiveQueue.ObjectHandle;
				}
				*(ptr2++) = num2;
				*(ptr2++) = 0;
				*(ptr2++) = 0;
				*(ptr2++) = 0;
				byte* ptr3 = (byte*)ptr2;
				for (int i = 0; i < 48; i++)
				{
					*(ptr3++) = 0;
				}
				for (int j = 0; j < 48; j++)
				{
					*(ptr3++) = 0;
				}
			}
		}
	}
}
