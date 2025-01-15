using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.PowerBI.DataMovement.Pipeline.Dataflow;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline.Serialization
{
	// Token: 0x0200001D RID: 29
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class RawDataSerializer
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x0000469D File Offset: 0x0000289D
		internal RawDataSerializer()
			: this(null)
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000046A8 File Offset: 0x000028A8
		internal RawDataSerializer(Func<Exception, Exception> exceptionTransform)
		{
			this.m_serializer = new ActionBlock<RawDataSerializationContext>(new Func<RawDataSerializationContext, Task>(this.Serialize));
			this.m_dataReady = new BufferBlock<RawDataFlowContext>();
			this.SerializerEngine = TDFHelpers.ChainAndEncapsulate<RawDataSerializationContext, RawDataFlowContext>(this.m_serializer, this.m_dataReady);
			this.m_exceptionTransform = exceptionTransform;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000046FB File Offset: 0x000028FB
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00004703 File Offset: 0x00002903
		internal IPropagatorBlock<RawDataSerializationContext, RawDataFlowContext> SerializerEngine { get; private set; }

		// Token: 0x060000BB RID: 187 RVA: 0x0000470C File Offset: 0x0000290C
		private async Task Serialize(RawDataSerializationContext serverContext)
		{
			RawDataSerializer.<>c__DisplayClass9_0 CS$<>8__locals1 = new RawDataSerializer.<>c__DisplayClass9_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.serverContext = serverContext;
			try
			{
				await DiagnosticsContext.TelemetryService.ExecuteInActivity(PipelineActivityType.RDPS, delegate
				{
					RawDataSerializer.<>c__DisplayClass9_0.<<Serialize>b__0>d <<Serialize>b__0>d;
					<<Serialize>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<Serialize>b__0>d.<>4__this = CS$<>8__locals1;
					<<Serialize>b__0>d.<>1__state = -1;
					<<Serialize>b__0>d.<>t__builder.Start<RawDataSerializer.<>c__DisplayClass9_0.<<Serialize>b__0>d>(ref <<Serialize>b__0>d);
					return <<Serialize>b__0>d.<>t__builder.Task;
				});
			}
			catch (Exception ex)
			{
				TraceSourceBase<PowerBIRawDataTraceSource>.Tracer.TraceError("Error serializing response: {0}", new object[] { ex });
				if (this.m_exceptionTransform != null)
				{
					ex = this.m_exceptionTransform(ex);
				}
				RawDataFlowContext rawDataFlowContext = CS$<>8__locals1.serverContext.CloneForPipeline();
				rawDataFlowContext.Packet = RelayPacketFactory.CreateJsonErrorPacket(ex);
				this.m_dataReady.PostRequest(rawDataFlowContext);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004758 File Offset: 0x00002958
		private async Task SerializeImpl(RawDataSerializationContext serverContext)
		{
			if (serverContext.QueryResult != null)
			{
				await this.SerializeOleDbQueryResponse(serverContext);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000047A4 File Offset: 0x000029A4
		private async Task SerializeOleDbQueryResponse(RawDataSerializationContext serverContext)
		{
			OleDbQueryResult queryResult = null;
			try
			{
				queryResult = serverContext.QueryResult;
				if (queryResult != null)
				{
					new OleDbBinaryWriterSerializer(this.m_dataReady, queryResult.Reader).Serialize(serverContext);
					await Task.CompletedTask;
				}
			}
			finally
			{
				if (queryResult != null)
				{
					queryResult.Reader.Dispose();
					queryResult.Reader = null;
					queryResult.PooledConnectionLifetimeManager.Dispose();
					queryResult.PooledConnectionLifetimeManager = null;
					if (queryResult.DbCommand != null)
					{
						queryResult.DbCommand.Dispose();
						queryResult.DbCommand = null;
					}
				}
			}
		}

		// Token: 0x0400006F RID: 111
		private readonly Func<Exception, Exception> m_exceptionTransform;

		// Token: 0x04000070 RID: 112
		private readonly ActionBlock<RawDataSerializationContext> m_serializer;

		// Token: 0x04000071 RID: 113
		private readonly BufferBlock<RawDataFlowContext> m_dataReady;
	}
}
