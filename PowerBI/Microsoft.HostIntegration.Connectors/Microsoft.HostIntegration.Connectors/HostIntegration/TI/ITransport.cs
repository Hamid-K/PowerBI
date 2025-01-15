using System;
using Microsoft.HostIntegration.Tracing.Common;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000742 RID: 1858
	public interface ITransport
	{
		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06003A60 RID: 14944
		string TransportName { get; }

		// Token: 0x06003A61 RID: 14945
		void StartListening(object StartObject);

		// Token: 0x06003A62 RID: 14946
		void Init(object RuntimeCallContext, bool IsLinkMode, int CodePage, out int RequiredHeaderLength, out int RequiredTrailerLength);

		// Token: 0x06003A63 RID: 14947
		void Connect();

		// Token: 0x06003A64 RID: 14948
		void Send(byte[] SendBuffer, int BufferStartSendPosition, int SendByteCount);

		// Token: 0x06003A65 RID: 14949
		void Receive(ref byte[] ReceiveBuffer, int BufferStartReceivePosition, int MaximumReceiveAmount, out int ReceivedDataLength, out bool DataComplete);

		// Token: 0x06003A66 RID: 14950
		void QueueAsyncReceive(ref RuntimeCallContext runtimeCallContext);

		// Token: 0x06003A67 RID: 14951
		void Disconnect(DisconnectType DisconnectType);

		// Token: 0x06003A68 RID: 14952
		void AbortConnection();

		// Token: 0x06003A69 RID: 14953
		bool IsConnectionViable();

		// Token: 0x06003A6A RID: 14954
		void DistribLink(BufferManager BufferManager, int bufferLength, int commareaLength, int optionalDataLength, int maxReceiveLength, out int actualReceivedDataLength);

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06003A6B RID: 14955
		// (set) Token: 0x06003A6C RID: 14956
		TransportTracePoint TracePoint { get; set; }
	}
}
