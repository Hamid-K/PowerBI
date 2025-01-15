using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common.Serialization
{
	// Token: 0x02000009 RID: 9
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class RowsetReadStreamBase : RowsetStreamBase
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002172 File Offset: 0x00000372
		protected RowsetReadStreamBase(ActivityInfo externalActivityInfo)
		{
			RuntimeChecks.CheckValue(externalActivityInfo, "externalActivityInfo");
			this.m_externalActivityInfo = externalActivityInfo;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12
		protected abstract int PacketHeaderSize { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13
		protected abstract bool HasCurrentPacket { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14
		protected abstract byte[] CurrentPacketBlob { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15
		protected abstract bool IsLastPacket { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16
		protected abstract bool RowsetPacketLimitReached { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000218C File Offset: 0x0000038C
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000218F File Offset: 0x0000038F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002192 File Offset: 0x00000392
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002195 File Offset: 0x00000395
		public override long Length
		{
			get
			{
				throw RuntimeChecks.UnsupportedCodepath("Length", "/src/Gateway/Pipeline/RowsetSerialization/RowsetReadStreamBase.cs", 58, "Unsupported code path reached");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021AD File Offset: 0x000003AD
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000021C5 File Offset: 0x000003C5
		public override long Position
		{
			get
			{
				throw RuntimeChecks.UnsupportedCodepath("Position", "/src/Gateway/Pipeline/RowsetSerialization/RowsetReadStreamBase.cs", 63, "Unsupported code path reached");
			}
			set
			{
				throw RuntimeChecks.UnsupportedCodepath("Position", "/src/Gateway/Pipeline/RowsetSerialization/RowsetReadStreamBase.cs", 64, "Unsupported code path reached");
			}
		}

		// Token: 0x06000017 RID: 23
		protected abstract Task ReceiveBinaryRowsetPacketAsync();

		// Token: 0x06000018 RID: 24 RVA: 0x000021DD File Offset: 0x000003DD
		public override void Flush()
		{
			throw RuntimeChecks.UnsupportedCodepath("Flush", "/src/Gateway/Pipeline/RowsetSerialization/RowsetReadStreamBase.cs", 71, "Unsupported code path reached");
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000021F5 File Offset: 0x000003F5
		public override IAsyncResult BeginRead(byte[] dstBuffer, int dstOffset, int dstCount, AsyncCallback callback, object state)
		{
			RuntimeChecks.CheckNotDisposed(this.m_disposed, this);
			return base.ReadAsync(dstBuffer, dstOffset, dstCount).ToApm(callback, state);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002215 File Offset: 0x00000415
		public override int EndRead(IAsyncResult asyncResult)
		{
			RuntimeChecks.CheckNotDisposed(this.m_disposed, this);
			return ((Task<int>)asyncResult).Result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000222E File Offset: 0x0000042E
		public override int Read(byte[] dstBuffer, int dstOffset, int dstCount)
		{
			RuntimeChecks.CheckNotDisposed(this.m_disposed, this);
			return base.ReadAsync(dstBuffer, dstOffset, dstCount).ExtendedResult<int>();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000224C File Offset: 0x0000044C
		public override async Task<int> ReadAsync(byte[] dstBuffer, int dstOffset, int dstCount, CancellationToken cancellationToken)
		{
			RuntimeChecks.CheckNotDisposed(this.m_disposed, this);
			int readCount = 0;
			while (readCount != dstCount)
			{
				if (this.HasCurrentPacket)
				{
					bool flag = this.IsLastPacket && this.m_nextPositionToRead == this.CurrentPacketBlob.Length;
					TraceSourceBase<RowsetSerializationTraceSource>.Tracer.TraceVerbose("DataMovement.Pipeline.OleDbBase.PageReaderMultipleResults: endOfData {0}, RowsetPacketLimitReached {1}", new object[] { flag, this.RowsetPacketLimitReached });
					if (flag || this.RowsetPacketLimitReached)
					{
						this.m_nextPositionToRead = this.CurrentPacketBlob.Length;
						break;
					}
				}
				if (!this.HasCurrentPacket || this.m_nextPositionToRead == this.CurrentPacketBlob.Length)
				{
					TraceSourceBase<RowsetSerializationTraceSource>.Tracer.TraceVerbose("DataMovement.Pipeline.OleDbBase.PageReaderMultipleResults: Try to get next rowset");
					if (DiagnosticsContext.TelemetryService.GetCurrentActivityInfo() == ActivityInfo.Empty)
					{
						using (DiagnosticsContext.TelemetryService.SetExternalActivity(this.m_externalActivityInfo))
						{
							await this.ReceiveBinaryRowsetPacketAsync();
						}
						IDisposable disposable = null;
					}
					else
					{
						await this.ReceiveBinaryRowsetPacketAsync();
					}
					this.m_nextPositionToRead = this.PacketHeaderSize;
				}
				int num = Math.Min(dstCount - readCount, this.CurrentPacketBlob.Length - this.m_nextPositionToRead);
				Buffer.BlockCopy(this.CurrentPacketBlob, this.m_nextPositionToRead, dstBuffer, dstOffset + readCount, num);
				TraceSourceBase<RowsetSerializationTraceSource>.Tracer.TraceVerbose("DataMovement.Pipeline.OleDbBase.PageReaderMultipleResults: try to read {0} bytes from gateway pipeline", new object[] { num });
				readCount += num;
				this.m_nextPositionToRead += num;
			}
			return readCount;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022A8 File Offset: 0x000004A8
		public override bool TryGetHasRowset(out bool hasRowset)
		{
			bool flag = false;
			hasRowset = false;
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) == 1)
			{
				hasRowset = array[0] > 0;
				flag = true;
			}
			if (this.HasCurrentPacket && this.m_nextPositionToRead == this.CurrentPacketBlob.Length)
			{
				flag = true;
			}
			TraceSourceBase<RowsetSerializationTraceSource>.Tracer.TraceVerbose("DataMovement.Pipeline.OleDbBase.PageReaderMultipleResults: hasRowset = {0}, hasRowsetFlag First Byte = {1}. Is Packet done = {2} ", new object[]
			{
				hasRowset,
				array[0],
				this.HasCurrentPacket && this.m_nextPositionToRead == this.CurrentPacketBlob.Length
			});
			return flag;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000233F File Offset: 0x0000053F
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw RuntimeChecks.UnsupportedCodepath("Seek", "/src/Gateway/Pipeline/RowsetSerialization/RowsetReadStreamBase.cs", 177, "Unsupported code path reached");
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000235A File Offset: 0x0000055A
		public override void SetLength(long value)
		{
			throw RuntimeChecks.UnsupportedCodepath("SetLength", "/src/Gateway/Pipeline/RowsetSerialization/RowsetReadStreamBase.cs", 182, "Unsupported code path reached");
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002375 File Offset: 0x00000575
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw RuntimeChecks.UnsupportedCodepath("Write", "/src/Gateway/Pipeline/RowsetSerialization/RowsetReadStreamBase.cs", 187, "Unsupported code path reached");
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002390 File Offset: 0x00000590
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.m_disposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000011 RID: 17
		private readonly ActivityInfo m_externalActivityInfo;

		// Token: 0x04000012 RID: 18
		private int m_nextPositionToRead;

		// Token: 0x04000013 RID: 19
		private bool m_disposed;
	}
}
