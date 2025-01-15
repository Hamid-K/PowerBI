using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Azure.Core.Pipeline
{
	// Token: 0x02000096 RID: 150
	[NullableContext(1)]
	[Nullable(0)]
	internal class HttpPipelineTransportPolicy : HttpPipelinePolicy
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x0000EBB1 File Offset: 0x0000CDB1
		public HttpPipelineTransportPolicy(HttpPipelineTransport transport, HttpMessageSanitizer sanitizer, [Nullable(2)] RequestFailedDetailsParser failureContentExtractor = null)
		{
			this._transport = transport;
			this._sanitizer = sanitizer;
			this._errorParser = failureContentExtractor;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		public override async ValueTask ProcessAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			await this._transport.ProcessAsync(message).ConfigureAwait(false);
			message.Response.RequestFailedDetailsParser = this._errorParser;
			message.Response.Sanitizer = this._sanitizer;
			message.Response.IsError = message.ResponseClassifier.IsErrorResponse(message);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000EC1C File Offset: 0x0000CE1C
		public override void Process(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			this._transport.Process(message);
			message.Response.RequestFailedDetailsParser = this._errorParser;
			message.Response.Sanitizer = this._sanitizer;
			message.Response.IsError = message.ResponseClassifier.IsErrorResponse(message);
		}

		// Token: 0x040001F7 RID: 503
		private readonly HttpPipelineTransport _transport;

		// Token: 0x040001F8 RID: 504
		private readonly HttpMessageSanitizer _sanitizer;

		// Token: 0x040001F9 RID: 505
		[Nullable(2)]
		private readonly RequestFailedDetailsParser _errorParser;
	}
}
