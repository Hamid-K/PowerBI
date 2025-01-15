using System;
using System.Collections.Generic;
using System.Net;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000195 RID: 405
	internal interface ITcpChannel
	{
		// Token: 0x06000D28 RID: 3368
		OperationResult Send(IList<ArraySegment<byte>> data);

		// Token: 0x06000D29 RID: 3369
		OperationResult Send(IList<ArraySegment<byte>> buffers, TcpPacketSendTypes sendType, int? sequenceNo);

		// Token: 0x06000D2A RID: 3370
		int Receive(ArraySegment<byte> data);

		// Token: 0x06000D2B RID: 3371
		int Receive(ArraySegment<byte> data, TimeSpan timeout);

		// Token: 0x06000D2C RID: 3372
		void Initialize(TcpChnlClosed chnlClosed, OnMessageReceived msgReceived, GetRecvBuffers getBuffers, TcpTransportProperty property, IServerSocketProtocol protocol, string logPrefix);

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000D2D RID: 3373
		bool IsSecureChannel { get; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000D2E RID: 3374
		DateTime EndReceiveTime { get; }

		// Token: 0x170002F1 RID: 753
		// (set) Token: 0x06000D2F RID: 3375
		TcpChnlClosed TcpChannelClosed { set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000D30 RID: 3376
		bool IsOpened { get; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000D31 RID: 3377
		EndPoint RemoteEndpoint { get; }

		// Token: 0x06000D32 RID: 3378
		bool ReceiveMessage();

		// Token: 0x06000D33 RID: 3379
		void CloseGracefully();

		// Token: 0x06000D34 RID: 3380
		void Abort();
	}
}
