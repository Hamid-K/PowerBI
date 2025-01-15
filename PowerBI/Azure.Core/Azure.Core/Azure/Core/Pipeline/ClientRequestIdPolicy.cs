using System;
using System.Runtime.CompilerServices;

namespace Azure.Core.Pipeline
{
	// Token: 0x02000092 RID: 146
	[NullableContext(1)]
	[Nullable(0)]
	internal class ClientRequestIdPolicy : HttpPipelineSynchronousPolicy
	{
		// Token: 0x060004B4 RID: 1204 RVA: 0x0000E668 File Offset: 0x0000C868
		protected ClientRequestIdPolicy()
		{
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000E670 File Offset: 0x0000C870
		public static ClientRequestIdPolicy Shared { get; } = new ClientRequestIdPolicy();

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000E678 File Offset: 0x0000C878
		public override void OnSendingRequest(HttpMessage message)
		{
			message.Request.Headers.SetValue("x-ms-client-request-id", message.Request.ClientRequestId);
			message.Request.Headers.SetValue("x-ms-return-client-request-id", "true");
		}

		// Token: 0x040001E3 RID: 483
		internal const string ClientRequestIdHeader = "x-ms-client-request-id";

		// Token: 0x040001E4 RID: 484
		internal const string EchoClientRequestId = "x-ms-return-client-request-id";
	}
}
