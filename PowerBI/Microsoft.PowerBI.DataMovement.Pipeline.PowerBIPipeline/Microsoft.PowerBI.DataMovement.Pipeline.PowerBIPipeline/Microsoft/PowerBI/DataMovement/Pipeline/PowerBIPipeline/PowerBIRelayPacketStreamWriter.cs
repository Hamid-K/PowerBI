using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.PowerBI.DataMovement.Pipeline.Dataflow;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.RelayPacketContracts;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x02000015 RID: 21
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PowerBIRelayPacketStreamWriter<[global::System.Runtime.CompilerServices.Nullable(0)] T> where T : RawDataFlowContext
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00003EA3 File Offset: 0x000020A3
		internal PowerBIRelayPacketStreamWriter(Stream stream, IPropagatorBlock<T, T> block)
		{
			this.m_stream = stream;
			this.m_block = block;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003EB9 File Offset: 0x000020B9
		internal Task FillStreamAsync()
		{
			return DiagnosticsContext.TelemetryService.ExecuteInActivity(PipelineActivityType.RDFS, async delegate
			{
				await this.FillStreamImpl();
			});
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003ED8 File Offset: 0x000020D8
		private async Task FillStreamImpl()
		{
			RelayPacket packet;
			do
			{
				TaskAwaiter<T> taskAwaiter = this.m_block.GetNextResponseAsync<T>().GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<T> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<T>);
				}
				packet = taskAwaiter.GetResult().Packet;
				this.m_stream.Write(packet.Blob, 0, packet.PhysicalDataSize);
			}
			while (!packet.Header.IsLast);
		}

		// Token: 0x04000056 RID: 86
		private readonly Stream m_stream;

		// Token: 0x04000057 RID: 87
		private readonly IPropagatorBlock<T, T> m_block;
	}
}
