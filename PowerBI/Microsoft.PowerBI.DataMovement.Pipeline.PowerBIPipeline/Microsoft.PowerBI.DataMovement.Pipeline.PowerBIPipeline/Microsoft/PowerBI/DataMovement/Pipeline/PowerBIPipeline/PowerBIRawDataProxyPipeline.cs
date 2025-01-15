using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x02000010 RID: 16
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PowerBIRawDataProxyPipeline : IDisposable
	{
		// Token: 0x0600005B RID: 91 RVA: 0x0000398C File Offset: 0x00001B8C
		internal PowerBIRawDataProxyPipeline(Stream inputStream, Func<Exception, Exception> exceptionTransformCallback = null)
		{
			this.m_inputBlock = new BufferBlock<RawDataFlowContext>();
			this.m_streamReader = new PowerBIRelayPacketStreamReader<RawDataFlowContext>(inputStream, this.m_inputBlock);
			this.m_outputStream = new PowerBIRawDataRowsetReadStream<RawDataFlowContext>(this.m_streamReader, this.m_inputBlock, ActivityInfo.CreateRoot(), exceptionTransformCallback, true);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000039DA File Offset: 0x00001BDA
		internal Stream OutputStream
		{
			get
			{
				return this.m_outputStream;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000039E2 File Offset: 0x00001BE2
		internal Exception ErrorDetails
		{
			get
			{
				if (this.m_outputStream == null)
				{
					return null;
				}
				return this.m_outputStream.OperationException;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000039F9 File Offset: 0x00001BF9
		public void Dispose()
		{
			if (this.m_disposed)
			{
				return;
			}
			if (this.m_outputStream != null)
			{
				this.m_outputStream.Dispose();
			}
			this.m_disposed = true;
		}

		// Token: 0x04000046 RID: 70
		private readonly PowerBIRelayPacketStreamReader<RawDataFlowContext> m_streamReader;

		// Token: 0x04000047 RID: 71
		private readonly BufferBlock<RawDataFlowContext> m_inputBlock;

		// Token: 0x04000048 RID: 72
		private readonly PowerBIRawDataRowsetReadStream<RawDataFlowContext> m_outputStream;

		// Token: 0x04000049 RID: 73
		private bool m_disposed;
	}
}
