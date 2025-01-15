using System;
using System.Runtime.CompilerServices;

namespace Azure.Core.Pipeline
{
	// Token: 0x02000088 RID: 136
	public sealed class DisposableHttpPipeline : HttpPipeline, IDisposable
	{
		// Token: 0x06000456 RID: 1110 RVA: 0x0000CF4E File Offset: 0x0000B14E
		[NullableContext(1)]
		internal DisposableHttpPipeline(HttpPipelineTransport transport, int perCallIndex, int perRetryIndex, HttpPipelinePolicy[] policies, ResponseClassifier responseClassifier, bool isTransportOwnedInternally)
			: base(transport, perCallIndex, perRetryIndex, policies, responseClassifier)
		{
			this.isTransportOwnedInternally = isTransportOwnedInternally;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000CF65 File Offset: 0x0000B165
		public void Dispose()
		{
			if (this.isTransportOwnedInternally)
			{
				IDisposable disposable = this._transport as IDisposable;
				if (disposable == null)
				{
					return;
				}
				disposable.Dispose();
			}
		}

		// Token: 0x040001CB RID: 459
		private bool isTransportOwnedInternally;
	}
}
