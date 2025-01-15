using System;
using System.Runtime.CompilerServices;

namespace Azure.Core.Pipeline
{
	// Token: 0x02000098 RID: 152
	[NullableContext(1)]
	[Nullable(0)]
	internal class ReadClientRequestIdPolicy : HttpPipelineSynchronousPolicy
	{
		// Token: 0x060004CE RID: 1230 RVA: 0x0000ED3F File Offset: 0x0000CF3F
		protected ReadClientRequestIdPolicy()
		{
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000ED47 File Offset: 0x0000CF47
		public static ReadClientRequestIdPolicy Shared { get; } = new ReadClientRequestIdPolicy();

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000ED50 File Offset: 0x0000CF50
		public override void OnSendingRequest(HttpMessage message)
		{
			string text;
			if (message.Request.Headers.TryGetValue("x-ms-client-request-id", out text))
			{
				message.Request.ClientRequestId = text;
				return;
			}
			object obj;
			if (!message.TryGetProperty("x-ms-client-request-id", out obj))
			{
				return;
			}
			string text2 = obj as string;
			if (text2 != null)
			{
				message.Request.ClientRequestId = text2;
				return;
			}
			throw new ArgumentException(string.Format("{0} http message property must be a string but was {1}", "x-ms-client-request-id", (obj != null) ? obj.GetType() : null));
		}

		// Token: 0x04000200 RID: 512
		public const string MessagePropertyKey = "x-ms-client-request-id";
	}
}
