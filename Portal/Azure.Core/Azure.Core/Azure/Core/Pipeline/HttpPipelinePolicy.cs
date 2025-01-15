using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Azure.Core.Pipeline
{
	// Token: 0x0200008D RID: 141
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class HttpPipelinePolicy
	{
		// Token: 0x06000493 RID: 1171
		public abstract ValueTask ProcessAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline);

		// Token: 0x06000494 RID: 1172
		public abstract void Process(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline);

		// Token: 0x06000495 RID: 1173 RVA: 0x0000DED8 File Offset: 0x0000C0D8
		protected unsafe static ValueTask ProcessNextAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			return pipeline.Span[0]->ProcessAsync(message, pipeline.Slice(1));
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000DF04 File Offset: 0x0000C104
		protected unsafe static void ProcessNext(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			pipeline.Span[0]->Process(message, pipeline.Slice(1));
		}
	}
}
