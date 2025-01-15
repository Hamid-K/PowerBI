using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.PowerBI.DataMovement.Pipeline.Common;
using Microsoft.PowerBI.DataMovement.Pipeline.Dataflow;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline.Compression;
using Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.RelayPacketContracts;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x02000012 RID: 18
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PowerBIRawDataServerPipeline
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00003C00 File Offset: 0x00001E00
		internal PowerBIRawDataServerPipeline(IPageReader reader, Func<Exception, Exception> exceptionTransformCallback = null)
		{
			this.m_serializer = new RawDataSerializer(exceptionTransformCallback);
			this.m_compressor = new RawDataCompressor(-2, exceptionTransformCallback);
			this.m_serializer.SerializerEngine.LinkWithCompletionPropagation(this.m_compressor.CompressorEngine);
			RawDataSerializationContext rawDataSerializationContext = new RawDataSerializationContext();
			OleDbQueryResult oleDbQueryResult = new OleDbQueryResult();
			oleDbQueryResult.Reader = reader;
			oleDbQueryResult.PooledConnectionLifetimeManager = new LifetimeManager(delegate
			{
			});
			rawDataSerializationContext.QueryResult = oleDbQueryResult;
			this.m_gatewayRawDataSerializationContext = rawDataSerializationContext;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003C90 File Offset: 0x00001E90
		internal static PowerBIRawDataServerPipeline Create(IPageReader reader, Func<Exception, Exception> exceptionTransformCallback)
		{
			return DiagnosticsContext.TelemetryService.ExecuteInTopLevelActivity<PowerBIRawDataServerPipeline>(PipelineActivityType.RDSC, () => new PowerBIRawDataServerPipeline(reader, exceptionTransformCallback));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003CCC File Offset: 0x00001ECC
		internal static void WriteRawDataError(Stream stream, Exception ex)
		{
			RelayPacket relayPacket = RelayPacketFactory.CreateJsonErrorPacket(ex);
			stream.Write(relayPacket.Blob, 0, relayPacket.Blob.Length);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003CF8 File Offset: 0x00001EF8
		internal Task FillStreamAsync(Stream outputStream)
		{
			PowerBIRawDataServerPipeline.<>c__DisplayClass6_0 CS$<>8__locals1 = new PowerBIRawDataServerPipeline.<>c__DisplayClass6_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.outputStream = outputStream;
			return DiagnosticsContext.TelemetryService.ExecuteInTopLevelActivity(PipelineActivityType.RDSP, delegate
			{
				PowerBIRawDataServerPipeline.<>c__DisplayClass6_0.<<FillStreamAsync>b__0>d <<FillStreamAsync>b__0>d;
				<<FillStreamAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<FillStreamAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<FillStreamAsync>b__0>d.<>1__state = -1;
				<<FillStreamAsync>b__0>d.<>t__builder.Start<PowerBIRawDataServerPipeline.<>c__DisplayClass6_0.<<FillStreamAsync>b__0>d>(ref <<FillStreamAsync>b__0>d);
				return <<FillStreamAsync>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003D34 File Offset: 0x00001F34
		private async Task FillStreamImpl(Stream outputStream)
		{
			await this.m_serializer.SerializerEngine.SendRequestAsync(this.m_gatewayRawDataSerializationContext, null);
			await new PowerBIRelayPacketStreamWriter<RawDataFlowContext>(outputStream, this.m_compressor.CompressorEngine).FillStreamAsync();
		}

		// Token: 0x04000051 RID: 81
		private readonly RawDataSerializer m_serializer;

		// Token: 0x04000052 RID: 82
		private readonly RawDataCompressor m_compressor;

		// Token: 0x04000053 RID: 83
		private readonly RawDataSerializationContext m_gatewayRawDataSerializationContext;
	}
}
