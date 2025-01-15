using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using Microsoft.PowerBI.DataMovement.Pipeline.Common;
using Microsoft.PowerBI.DataMovement.Pipeline.Dataflow;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.RelayPacketContracts;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline.Serialization
{
	// Token: 0x0200001A RID: 26
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class OleDbBinaryPacketWriteStream : Stream
	{
		// Token: 0x06000093 RID: 147 RVA: 0x0000417E File Offset: 0x0000237E
		internal OleDbBinaryPacketWriteStream(RawDataSerializationContext rawDataSerializationContext, ITargetBlock<RawDataFlowContext> dataSinkBlock)
			: this(rawDataSerializationContext, dataSinkBlock, Math.Max(65536, OleDbBinaryPacketWriteStream.MinPageSize), Math.Max(8388608, OleDbBinaryPacketWriteStream.MinPageLargeSize))
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000041A8 File Offset: 0x000023A8
		internal OleDbBinaryPacketWriteStream(RawDataSerializationContext rawDataSerializationContext, ITargetBlock<RawDataFlowContext> dataSinkBlock, int bufferSize, int bufferLargeSize)
		{
			RuntimeChecks.CheckValue(dataSinkBlock, "dataSinkBlock");
			RuntimeChecks.CheckValue(rawDataSerializationContext, "serializationContext");
			this.m_rawDataSerializationContext = rawDataSerializationContext;
			this.m_dataSinkBlock = dataSinkBlock;
			this.m_bufferSize = bufferSize;
			this.m_bufferLargeSize = Math.Max(bufferSize, bufferLargeSize);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000041FB File Offset: 0x000023FB
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000041FE File Offset: 0x000023FE
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004201 File Offset: 0x00002401
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004204 File Offset: 0x00002404
		public override long Length
		{
			get
			{
				throw RuntimeChecks.UnsupportedCodepath("Getting the length of an OleDbBinaryPacketStream is not supported.", "/src/Gateway/Pipeline/PowerBIPipeline/Serialization/OleDbBinaryPacketWriteStream.cs", 85, "Unsupported code path reached");
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000421C File Offset: 0x0000241C
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00004234 File Offset: 0x00002434
		public override long Position
		{
			get
			{
				throw RuntimeChecks.UnsupportedCodepath("Getting the position of an OleDbBinaryPacketStream is not supported.", "/src/Gateway/Pipeline/PowerBIPipeline/Serialization/OleDbBinaryPacketWriteStream.cs", 90, "Unsupported code path reached");
			}
			set
			{
				throw RuntimeChecks.UnsupportedCodepath("Setting the position in an OleDbBinaryPacketStream is not supported.", "/src/Gateway/Pipeline/PowerBIPipeline/Serialization/OleDbBinaryPacketWriteStream.cs", 91, "Unsupported code path reached");
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000424C File Offset: 0x0000244C
		public void SendIsLastPacketOnClose()
		{
			this.m_sendIsLastPacketOnClose = true;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004255 File Offset: 0x00002455
		public override void WriteByte(byte value)
		{
			RuntimeChecks.CheckNotDisposed(this.m_disposed, this);
			this.EnsureBuffer(1);
			this.m_packetBuffer[this.m_currentBufferOffset] = value;
			this.m_currentBufferOffset++;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004286 File Offset: 0x00002486
		public override void Write(byte[] buffer, int offset, int count)
		{
			RuntimeChecks.CheckNotDisposed(this.m_disposed, this);
			if (count == 0)
			{
				return;
			}
			this.EnsureBuffer(count);
			Buffer.BlockCopy(buffer, offset, this.m_packetBuffer, this.m_currentBufferOffset, count);
			this.m_currentBufferOffset += count;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000042C1 File Offset: 0x000024C1
		public override void Flush()
		{
			RuntimeChecks.CheckNotDisposed(this.m_disposed, this);
			this.Flush(false);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000042D6 File Offset: 0x000024D6
		public override void Close()
		{
			if (!this.m_closed)
			{
				this.Flush(this.m_sendIsLastPacketOnClose);
				this.m_closed = true;
				base.Close();
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000042F9 File Offset: 0x000024F9
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.m_disposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000430C File Offset: 0x0000250C
		private void EnsureBuffer(int newByteCount)
		{
			int bufferSize = this.GetBufferSize(newByteCount);
			if (this.m_packetBuffer == null)
			{
				this.AcquireNewBuffer(bufferSize);
				return;
			}
			if (this.m_currentBufferOffset + newByteCount > this.m_packetBuffer.Length)
			{
				this.Flush(false);
				this.AcquireNewBuffer(bufferSize);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004354 File Offset: 0x00002554
		private void Flush(bool isLast)
		{
			if (this.m_packetBuffer == null)
			{
				if (!isLast)
				{
					return;
				}
				this.EnsureBuffer(0);
			}
			RawDataFlowContext rawDataFlowContext = this.m_rawDataSerializationContext.CloneForPipeline();
			rawDataFlowContext.Packet = RelayPacketFactory.CreateBinaryRowsetPacket(this.m_packetBuffer, this.m_currentBufferOffset, this.m_packetIndex, isLast);
			this.m_dataSinkBlock.SendRequestAsync(rawDataFlowContext, null).ExtendedWait(500, CancellationToken.None);
			this.m_packetBuffer = null;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000043CA File Offset: 0x000025CA
		internal void AcquireNewBuffer(int size)
		{
			this.m_packetBuffer = new byte[RelayPacket.HeaderSize + size];
			this.m_currentBufferOffset = RelayPacket.HeaderSize;
			this.m_packetIndex++;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000043F7 File Offset: 0x000025F7
		private int GetBufferSize(int newByteCount)
		{
			return Math.Max((this.m_packetIndex > -1) ? this.m_bufferLargeSize : this.m_bufferSize, newByteCount);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004416 File Offset: 0x00002616
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw RuntimeChecks.UnsupportedCodepath("Reading from an OleDbBinaryPacketStream is not supported.", "/src/Gateway/Pipeline/PowerBIPipeline/Serialization/OleDbBinaryPacketWriteStream.cs", 209, "Unsupported code path reached");
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004431 File Offset: 0x00002631
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw RuntimeChecks.UnsupportedCodepath("Seeking in an OleDbBinaryPacketStream is not supported.", "/src/Gateway/Pipeline/PowerBIPipeline/Serialization/OleDbBinaryPacketWriteStream.cs", 214, "Unsupported code path reached");
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000444C File Offset: 0x0000264C
		public override void SetLength(long value)
		{
			throw RuntimeChecks.UnsupportedCodepath("Setting the length of an OleDbBinaryPacketStream is not supported.", "/src/Gateway/Pipeline/PowerBIPipeline/Serialization/OleDbBinaryPacketWriteStream.cs", 219, "Unsupported code path reached");
		}

		// Token: 0x0400005C RID: 92
		private const int s_sendTimeout = 500;

		// Token: 0x0400005D RID: 93
		private static readonly int MinPageSize = 1024 + RelayPacketHeader.Size;

		// Token: 0x0400005E RID: 94
		private static readonly int MinPageLargeSize = 131072 + RelayPacketHeader.Size;

		// Token: 0x0400005F RID: 95
		private readonly ITargetBlock<RawDataFlowContext> m_dataSinkBlock;

		// Token: 0x04000060 RID: 96
		private readonly RawDataSerializationContext m_rawDataSerializationContext;

		// Token: 0x04000061 RID: 97
		private readonly int m_bufferSize;

		// Token: 0x04000062 RID: 98
		private readonly int m_bufferLargeSize;

		// Token: 0x04000063 RID: 99
		private byte[] m_packetBuffer;

		// Token: 0x04000064 RID: 100
		private int m_packetIndex = -1;

		// Token: 0x04000065 RID: 101
		private int m_currentBufferOffset;

		// Token: 0x04000066 RID: 102
		private bool m_disposed;

		// Token: 0x04000067 RID: 103
		private bool m_closed;

		// Token: 0x04000068 RID: 104
		private bool m_sendIsLastPacketOnClose;
	}
}
