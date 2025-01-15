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
	// Token: 0x02000014 RID: 20
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PowerBIRelayPacketStreamReader<[global::System.Runtime.CompilerServices.Nullable(0)] T> where T : RawDataFlowContext, new()
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003D93 File Offset: 0x00001F93
		internal PowerBIRelayPacketStreamReader(Stream stream, ITargetBlock<T> block)
		{
			this.m_stream = stream;
			this.m_block = block;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003DA9 File Offset: 0x00001FA9
		internal Task ReadNextPacketAsync()
		{
			return DiagnosticsContext.TelemetryService.ExecuteInActivity(PipelineActivityType.RDRS, async delegate
			{
				await this.ReadNextPacketImpl();
			});
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003DC8 File Offset: 0x00001FC8
		private async Task ReadNextPacketImpl()
		{
			if (!this.m_block.Completion.IsCompleted)
			{
				byte[] currentPacketSizeByteArray = new byte[RelayPacket.HeaderSize];
				await this.FillBufferAsync(currentPacketSizeByteArray, 0);
				RelayPacketHeader relayPacketHeader = RelayPacketHeader.Deserialize(currentPacketSizeByteArray);
				byte[] currentPacketBlob = new byte[relayPacketHeader.CompressedDataSize + RelayPacket.HeaderSize];
				Buffer.BlockCopy(currentPacketSizeByteArray, 0, currentPacketBlob, 0, RelayPacket.HeaderSize);
				await this.FillBufferAsync(currentPacketBlob, RelayPacket.HeaderSize);
				RelayPacket currentPacket = new RelayPacket
				{
					Blob = currentPacketBlob
				};
				T t = new T();
				t.Packet = currentPacket;
				await this.m_block.SendRequestAsync(t, null);
				if (currentPacket.Header.IsLast)
				{
					this.m_block.Complete();
				}
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003E0C File Offset: 0x0000200C
		private async Task FillBufferAsync(byte[] buffer, int currentBufferOffset = 0)
		{
			while (currentBufferOffset < buffer.Length)
			{
				int num = await this.m_stream.ReadAsync(buffer, currentBufferOffset, buffer.Length - currentBufferOffset);
				if (num == 0)
				{
					throw new InvalidDataException();
				}
				currentBufferOffset += num;
			}
		}

		// Token: 0x04000054 RID: 84
		private readonly Stream m_stream;

		// Token: 0x04000055 RID: 85
		private readonly ITargetBlock<T> m_block;
	}
}
