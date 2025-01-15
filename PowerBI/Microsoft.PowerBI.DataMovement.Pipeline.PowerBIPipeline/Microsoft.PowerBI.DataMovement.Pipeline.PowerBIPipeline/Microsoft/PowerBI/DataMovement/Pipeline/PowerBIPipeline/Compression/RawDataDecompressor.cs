using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;
using Microsoft.PowerBI.DataMovement.Pipeline.CompressionInterop;
using Microsoft.PowerBI.DataMovement.Pipeline.Dataflow;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.RelayPacketContracts;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline.Compression
{
	// Token: 0x02000020 RID: 32
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class RawDataDecompressor
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00004B5C File Offset: 0x00002D5C
		internal RawDataDecompressor()
			: this(new DataflowBlockOptionsExtensions())
		{
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004B6C File Offset: 0x00002D6C
		internal RawDataDecompressor(DataflowBlockOptionsExtensions extraOptions)
		{
			IPropagatorBlock<RawDataFlowContext, RawDataFlowContext> propagatorBlock2;
			if (!extraOptions.AllowUnordered)
			{
				IPropagatorBlock<RawDataFlowContext, RawDataFlowContext> propagatorBlock = new TransformBlock<RawDataFlowContext, RawDataFlowContext>(new Func<RawDataFlowContext, RawDataFlowContext>(this.Decompress));
				propagatorBlock2 = propagatorBlock;
			}
			else
			{
				propagatorBlock2 = TDFHelpers.CreateUnorderedTransformBlock<RawDataFlowContext, RawDataFlowContext>(new Func<RawDataFlowContext, RawDataFlowContext>(this.Decompress));
			}
			this.DecompressorEngine = propagatorBlock2;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004BB3 File Offset: 0x00002DB3
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00004BBB File Offset: 0x00002DBB
		internal IPropagatorBlock<RawDataFlowContext, RawDataFlowContext> DecompressorEngine { get; private set; }

		// Token: 0x060000C9 RID: 201 RVA: 0x00004BC4 File Offset: 0x00002DC4
		internal RawDataFlowContext Decompress(RawDataFlowContext relayContext)
		{
			return DiagnosticsContext.TelemetryService.ExecuteInActivity<RawDataFlowContext>(PipelineActivityType.RDRC, () => this.DecompressImpl(relayContext));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004C00 File Offset: 0x00002E00
		private RawDataFlowContext DecompressImpl(RawDataFlowContext relayContext)
		{
			RelayPacketHeader header = relayContext.Packet.Header;
			RawDataDecompressor.ValidatePacketHeader(relayContext);
			if (header.CompressionAlgorithm != XPress9Level.None)
			{
				TraceSourceBase<PowerBIRawDataTraceSource>.Tracer.TraceInformation("Decompressing {0} => {1} bytes", new object[] { header.CompressedDataSize, header.UncompressedDataSize });
				byte[] array = new byte[RelayPacketHeader.Size + header.UncompressedDataSize];
				using (MemoryStream memoryStream = new MemoryStream(relayContext.Packet.Blob, RelayPacketHeader.Size, relayContext.Packet.LogicalDataSize))
				{
					using (XPress9Stream xpress9Stream = new XPress9Stream(memoryStream, CompressionMode.Decompress, header.CompressionAlgorithm))
					{
						xpress9Stream.Read(array, RelayPacketHeader.Size, header.UncompressedDataSize);
						header.CompressionAlgorithm = XPress9Level.None;
						header.CompressedDataSize = header.UncompressedDataSize;
						header.Serialize(array);
						relayContext.Packet.Blob = array;
					}
				}
			}
			return relayContext;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004D08 File Offset: 0x00002F08
		private static void ValidatePacketHeader(RawDataFlowContext relayContext)
		{
			RelayPacketHeader header = relayContext.Packet.Header;
			if (header.CompressionAlgorithm != XPress9Level.None)
			{
				int num = header.CompressedDataSize + RelayPacketHeader.Size;
				if (relayContext.Packet.Blob.Length != num)
				{
					throw RawDataDecompressor.MismatchCompressedDataSizeInPacketHeaderException(string.Format(CultureInfo.InvariantCulture, "Header.CompressedDataSize ({0}) of a compressed packet does not match the expected blob size of {1}", relayContext.Packet.Blob.Length, num));
				}
				if (header.UncompressedDataSize > 104857600 + RelayPacketHeader.Size)
				{
					throw RawDataDecompressor.UncompressedDataSizeForCompressedPacketExceededException(string.Format(CultureInfo.InvariantCulture, "Header.UncompressedDataSize ({0}) of a compressed packet exceeds the maximum allowed uncompressed payload of {1}", header.UncompressedDataSize, 104857600 + RelayPacketHeader.Size));
				}
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004DB9 File Offset: 0x00002FB9
		private static MismatchCompressedDataSizeInPacketHeader MismatchCompressedDataSizeInPacketHeaderException(string reason)
		{
			throw new MismatchCompressedDataSizeInPacketHeader(reason, string.Empty, Array.Empty<PowerBIErrorDetail>());
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004DCB File Offset: 0x00002FCB
		private static UncompressedDataSizeForCompressedPacketExceeded UncompressedDataSizeForCompressedPacketExceededException(string reason)
		{
			throw new UncompressedDataSizeForCompressedPacketExceeded(reason, string.Empty, Array.Empty<PowerBIErrorDetail>());
		}

		// Token: 0x0400007A RID: 122
		internal const int c_maxUncompressedDataSize = 104857600;
	}
}
