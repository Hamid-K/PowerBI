using System;
using System.IO;
using Microsoft.Data.Common;
using Microsoft.Data.SqlTypes;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000153 RID: 339
	internal class SqlClientWrapperSmiStreamChars : SqlStreamChars
	{
		// Token: 0x06001A26 RID: 6694 RVA: 0x0006B833 File Offset: 0x00069A33
		internal SqlClientWrapperSmiStreamChars(SmiEventSink_Default sink, SmiStream stream)
		{
			this._sink = sink;
			this._stream = stream;
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x0006B849 File Offset: 0x00069A49
		public override bool IsNull
		{
			get
			{
				return this._stream == null;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x0006B854 File Offset: 0x00069A54
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x0006B861 File Offset: 0x00069A61
		public override bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x0006B86E File Offset: 0x00069A6E
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x0006B87C File Offset: 0x00069A7C
		public override long Length
		{
			get
			{
				long length = this._stream.GetLength(this._sink);
				this._sink.ProcessMessagesAndThrow();
				if (length > 0L)
				{
					return length / 2L;
				}
				return length;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06001A2C RID: 6700 RVA: 0x0006B8B4 File Offset: 0x00069AB4
		// (set) Token: 0x06001A2D RID: 6701 RVA: 0x0006B8E2 File Offset: 0x00069AE2
		public override long Position
		{
			get
			{
				long num = this._stream.GetPosition(this._sink) / 2L;
				this._sink.ProcessMessagesAndThrow();
				return num;
			}
			set
			{
				if (value < 0L)
				{
					throw ADP.ArgumentOutOfRange("Position");
				}
				this._stream.SetPosition(this._sink, value * 2L);
				this._sink.ProcessMessagesAndThrow();
			}
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0006B914 File Offset: 0x00069B14
		public override void Flush()
		{
			this._stream.Flush(this._sink);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0006B934 File Offset: 0x00069B34
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num = this._stream.Seek(this._sink, offset * 2L, origin);
			this._sink.ProcessMessagesAndThrow();
			return num;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0006B964 File Offset: 0x00069B64
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw ADP.ArgumentOutOfRange("value");
			}
			this._stream.SetLength(this._sink, value * 2L);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0006B998 File Offset: 0x00069B98
		public override int Read(char[] buffer, int offset, int count)
		{
			int num = this._stream.Read(this._sink, buffer, offset * 2, count);
			this._sink.ProcessMessagesAndThrow();
			return num;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0006B9C8 File Offset: 0x00069BC8
		public override void Write(char[] buffer, int offset, int count)
		{
			this._stream.Write(this._sink, buffer, offset, count);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0006B9EC File Offset: 0x00069BEC
		internal int Read(byte[] buffer, int offset, int count)
		{
			int num = this._stream.Read(this._sink, buffer, offset, count);
			this._sink.ProcessMessagesAndThrow();
			return num;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0006BA1A File Offset: 0x00069C1A
		internal void Write(byte[] buffer, int offset, int count)
		{
			this._stream.Write(this._sink, buffer, offset, count);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x04000A8F RID: 2703
		private SmiEventSink_Default _sink;

		// Token: 0x04000A90 RID: 2704
		private SmiStream _stream;
	}
}
