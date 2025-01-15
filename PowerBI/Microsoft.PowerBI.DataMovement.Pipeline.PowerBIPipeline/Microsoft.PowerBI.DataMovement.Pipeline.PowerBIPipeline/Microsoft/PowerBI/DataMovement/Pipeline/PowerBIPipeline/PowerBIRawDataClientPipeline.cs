using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline.Compression;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x0200000F RID: 15
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PowerBIRawDataClientPipeline : IDisposable
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00003878 File Offset: 0x00001A78
		internal PowerBIRawDataClientPipeline(Stream inputStream, Func<IPageReader, IRawDataPageReader> pageReaderAdapter = null, Func<Exception, Exception> exceptionTransformCallback = null)
		{
			this.m_inputBlock = new BufferBlock<RawDataFlowContext>();
			this.m_decompressorBlock = new RawDataDecompressor();
			this.m_inputBlock.LinkTo(this.m_decompressorBlock.DecompressorEngine);
			this.m_streamReader = new PowerBIRelayPacketStreamReader<RawDataFlowContext>(inputStream, this.m_inputBlock);
			this.m_outputStream = new PowerBIRawDataRowsetReadStream<RawDataFlowContext>(this.m_streamReader, this.m_decompressorBlock.DecompressorEngine, ActivityInfo.CreateRoot(), exceptionTransformCallback, false);
			this.m_pageReaderAdapter = pageReaderAdapter ?? RawDataPageReaderAdapter.PageReaderAdapter;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003900 File Offset: 0x00001B00
		internal IRawDataPageReader PageReader
		{
			get
			{
				IRawDataPageReader rawDataPageReader;
				if ((rawDataPageReader = this.m_pageReader) == null)
				{
					rawDataPageReader = (this.m_pageReader = this.CreatePageReader());
				}
				return rawDataPageReader;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003926 File Offset: 0x00001B26
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

		// Token: 0x06000059 RID: 89 RVA: 0x00003940 File Offset: 0x00001B40
		private IRawDataPageReader CreatePageReader()
		{
			IPageReader pageReader = new OleDbPageReader(this.m_outputStream, false);
			return this.m_pageReaderAdapter(pageReader);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003966 File Offset: 0x00001B66
		public void Dispose()
		{
			if (this.m_disposed)
			{
				return;
			}
			if (this.m_pageReader != null)
			{
				this.m_pageReader.Dispose();
			}
			this.m_disposed = true;
		}

		// Token: 0x0400003F RID: 63
		private readonly PowerBIRelayPacketStreamReader<RawDataFlowContext> m_streamReader;

		// Token: 0x04000040 RID: 64
		private readonly BufferBlock<RawDataFlowContext> m_inputBlock;

		// Token: 0x04000041 RID: 65
		private readonly RawDataDecompressor m_decompressorBlock;

		// Token: 0x04000042 RID: 66
		private readonly Func<IPageReader, IRawDataPageReader> m_pageReaderAdapter;

		// Token: 0x04000043 RID: 67
		private readonly PowerBIRawDataRowsetReadStream<RawDataFlowContext> m_outputStream;

		// Token: 0x04000044 RID: 68
		private IRawDataPageReader m_pageReader;

		// Token: 0x04000045 RID: 69
		private bool m_disposed;
	}
}
