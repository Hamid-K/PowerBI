using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;
using Microsoft.PowerBI.DataMovement.Pipeline.Common;
using Microsoft.PowerBI.DataMovement.Pipeline.CompressionInterop;
using Microsoft.PowerBI.DataMovement.Pipeline.Dataflow;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.RelayPacketContracts;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline.Compression
{
	// Token: 0x0200001F RID: 31
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class RawDataCompressor
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000047EF File Offset: 0x000029EF
		internal RawDataCompressor(int compressionThreshold)
			: this(compressionThreshold, new DataflowBlockOptionsExtensions())
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000047FD File Offset: 0x000029FD
		internal RawDataCompressor(int compressionThreshold, Func<Exception, Exception> exceptionTransform)
			: this(compressionThreshold, new DataflowBlockOptionsExtensions())
		{
			this.m_exceptionTransform = exceptionTransform;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004814 File Offset: 0x00002A14
		internal RawDataCompressor(int compressionThreshold, DataflowBlockOptionsExtensions extraOptions)
		{
			this.m_compressionThreshold = ((compressionThreshold == -2) ? 65536 : compressionThreshold);
			IPropagatorBlock<RawDataFlowContext, RawDataFlowContext> propagatorBlock2;
			if (!extraOptions.AllowUnordered)
			{
				IPropagatorBlock<RawDataFlowContext, RawDataFlowContext> propagatorBlock = new TransformBlock<RawDataFlowContext, RawDataFlowContext>(new Func<RawDataFlowContext, RawDataFlowContext>(this.Compress));
				propagatorBlock2 = propagatorBlock;
			}
			else
			{
				propagatorBlock2 = TDFHelpers.CreateUnorderedTransformBlock<RawDataFlowContext, RawDataFlowContext>(new Func<RawDataFlowContext, RawDataFlowContext>(this.Compress));
			}
			this.CompressorEngine = propagatorBlock2;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000486E File Offset: 0x00002A6E
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00004876 File Offset: 0x00002A76
		internal IPropagatorBlock<RawDataFlowContext, RawDataFlowContext> CompressorEngine { get; private set; }

		// Token: 0x060000C3 RID: 195 RVA: 0x00004880 File Offset: 0x00002A80
		internal RawDataFlowContext Compress(RawDataFlowContext relayContext)
		{
			RawDataFlowContext rawDataFlowContext;
			try
			{
				rawDataFlowContext = DiagnosticsContext.TelemetryService.ExecuteInActivity<RawDataFlowContext>(PipelineActivityType.RDPC, () => this.CompressImpl(relayContext));
			}
			catch (Exception ex)
			{
				TraceSourceBase<PowerBIRawDataTraceSource>.Tracer.TraceError("Error compressing packet: {0}", new object[] { ex });
				if (this.m_exceptionTransform != null)
				{
					ex = this.m_exceptionTransform(ex);
				}
				rawDataFlowContext = new RawDataFlowContext
				{
					Packet = RelayPacketFactory.CreateJsonErrorPacket(ex)
				};
			}
			return rawDataFlowContext;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004914 File Offset: 0x00002B14
		private RawDataFlowContext CompressImpl(RawDataFlowContext relayContext)
		{
			RelayPacketHeader header = relayContext.Packet.Header;
			bool flag = header.CompressionAlgorithm > XPress9Level.None;
			if (this.m_compressionThreshold >= 0 && header.UncompressedDataSize > this.m_compressionThreshold && !flag)
			{
				int allocationSize = XPress9Compression.GetAllocationSize(header.UncompressedDataSize);
				byte[] array = new byte[RelayPacketHeader.Size + allocationSize];
				using (MemoryStream memoryStream = new MemoryStream(array, RelayPacketHeader.Size, allocationSize))
				{
					using (XPress9Stream xpress9Stream = new XPress9Stream(memoryStream, CompressionMode.Compress, true, XPress9Level.Level6))
					{
						xpress9Stream.Write(relayContext.Packet.Blob, RelayPacketHeader.Size, header.UncompressedDataSize);
					}
					int num = (int)memoryStream.Position;
					if (num >= header.UncompressedDataSize)
					{
						relayContext.Packet.Blob = relayContext.Packet.Blob.ShrinkByteArray(RelayPacket.GetPhysicalDataSize(header.UncompressedDataSize));
						TraceSourceBase<PowerBIRawDataTraceSource>.Tracer.TraceVerbose("Discarded compressed bytes {0} => {1}; sending uncompressed", new object[] { header.UncompressedDataSize, num });
						return relayContext;
					}
					header.CompressionAlgorithm = XPress9Level.Level6;
					header.CompressedDataSize = num;
					header.Serialize(array);
					relayContext.Packet.Blob = array.ShrinkByteArray(RelayPacket.GetPhysicalDataSize(header.CompressedDataSize));
					TraceSourceBase<PowerBIRawDataTraceSource>.Tracer.TraceVerbose("Compressed {0} => {1} bytes", new object[] { header.UncompressedDataSize, header.CompressedDataSize });
					return relayContext;
				}
			}
			if (!flag)
			{
				relayContext.Packet.Blob = relayContext.Packet.Blob.ShrinkByteArray(RelayPacket.GetPhysicalDataSize(header.UncompressedDataSize));
				TraceSourceBase<PowerBIRawDataTraceSource>.Tracer.TraceVerbose("Not compressing {0} bytes compressed with {1}", new object[] { header.UncompressedDataSize, header.CompressionAlgorithm });
			}
			else
			{
				TraceSourceBase<PowerBIRawDataTraceSource>.Tracer.TraceVerbose("Not compressing or shrinking {0} bytes compressed with {1}", new object[] { header.UncompressedDataSize, header.CompressionAlgorithm });
			}
			return relayContext;
		}

		// Token: 0x04000076 RID: 118
		internal const int c_defaultCompressionThreshold = 65536;

		// Token: 0x04000077 RID: 119
		private readonly int m_compressionThreshold;

		// Token: 0x04000078 RID: 120
		private readonly Func<Exception, Exception> m_exceptionTransform;
	}
}
