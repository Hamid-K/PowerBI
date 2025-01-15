using System;
using System.IO;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000152 RID: 338
	internal class SqlClientWrapperSmiStream : Stream
	{
		// Token: 0x06001A1A RID: 6682 RVA: 0x0006B6C4 File Offset: 0x000698C4
		internal SqlClientWrapperSmiStream(SmiEventSink_Default sink, SmiStream stream)
		{
			this._sink = sink;
			this._stream = stream;
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x0006B6DA File Offset: 0x000698DA
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x0006B6E7 File Offset: 0x000698E7
		public override bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x0006B6F4 File Offset: 0x000698F4
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x0006B704 File Offset: 0x00069904
		public override long Length
		{
			get
			{
				long length = this._stream.GetLength(this._sink);
				this._sink.ProcessMessagesAndThrow();
				return length;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x0006B730 File Offset: 0x00069930
		// (set) Token: 0x06001A20 RID: 6688 RVA: 0x0006B75B File Offset: 0x0006995B
		public override long Position
		{
			get
			{
				long position = this._stream.GetPosition(this._sink);
				this._sink.ProcessMessagesAndThrow();
				return position;
			}
			set
			{
				this._stream.SetPosition(this._sink, value);
				this._sink.ProcessMessagesAndThrow();
			}
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x0006B77A File Offset: 0x0006997A
		public override void Flush()
		{
			this._stream.Flush(this._sink);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x0006B798 File Offset: 0x00069998
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num = this._stream.Seek(this._sink, offset, origin);
			this._sink.ProcessMessagesAndThrow();
			return num;
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x0006B7C5 File Offset: 0x000699C5
		public override void SetLength(long value)
		{
			this._stream.SetLength(this._sink, value);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0006B7E4 File Offset: 0x000699E4
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this._stream.Read(this._sink, buffer, offset, count);
			this._sink.ProcessMessagesAndThrow();
			return num;
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0006B812 File Offset: 0x00069A12
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._stream.Write(this._sink, buffer, offset, count);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x04000A8D RID: 2701
		private SmiEventSink_Default _sink;

		// Token: 0x04000A8E RID: 2702
		private SmiStream _stream;
	}
}
