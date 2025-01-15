using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http.Internal
{
	// Token: 0x02000031 RID: 49
	internal abstract class DelegatingStream : Stream
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x000067A8 File Offset: 0x000049A8
		protected DelegatingStream(Stream innerStream)
		{
			if (innerStream == null)
			{
				throw Error.ArgumentNull("innerStream");
			}
			this._innerStream = innerStream;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000067C5 File Offset: 0x000049C5
		protected Stream InnerStream
		{
			get
			{
				return this._innerStream;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000067CD File Offset: 0x000049CD
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000067DA File Offset: 0x000049DA
		public override bool CanSeek
		{
			get
			{
				return this._innerStream.CanSeek;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000067E7 File Offset: 0x000049E7
		public override bool CanWrite
		{
			get
			{
				return this._innerStream.CanWrite;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000067F4 File Offset: 0x000049F4
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00006801 File Offset: 0x00004A01
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000680E File Offset: 0x00004A0E
		public override long Position
		{
			get
			{
				return this._innerStream.Position;
			}
			set
			{
				this._innerStream.Position = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000681C File Offset: 0x00004A1C
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00006829 File Offset: 0x00004A29
		public override int ReadTimeout
		{
			get
			{
				return this._innerStream.ReadTimeout;
			}
			set
			{
				this._innerStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00006837 File Offset: 0x00004A37
		public override bool CanTimeout
		{
			get
			{
				return this._innerStream.CanTimeout;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00006844 File Offset: 0x00004A44
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00006851 File Offset: 0x00004A51
		public override int WriteTimeout
		{
			get
			{
				return this._innerStream.WriteTimeout;
			}
			set
			{
				this._innerStream.WriteTimeout = value;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000685F File Offset: 0x00004A5F
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._innerStream.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006876 File Offset: 0x00004A76
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._innerStream.Seek(offset, origin);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006885 File Offset: 0x00004A85
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._innerStream.Read(buffer, offset, count);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006895 File Offset: 0x00004A95
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._innerStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000068A7 File Offset: 0x00004AA7
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this._innerStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000068BB File Offset: 0x00004ABB
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this._innerStream.EndRead(asyncResult);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000068C9 File Offset: 0x00004AC9
		public override int ReadByte()
		{
			return this._innerStream.ReadByte();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000068D6 File Offset: 0x00004AD6
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000068E3 File Offset: 0x00004AE3
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._innerStream.FlushAsync(cancellationToken);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000068F1 File Offset: 0x00004AF1
		public override void SetLength(long value)
		{
			this._innerStream.SetLength(value);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000068FF File Offset: 0x00004AFF
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000690F File Offset: 0x00004B0F
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._innerStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006921 File Offset: 0x00004B21
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this._innerStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006935 File Offset: 0x00004B35
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this._innerStream.EndWrite(asyncResult);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006943 File Offset: 0x00004B43
		public override void WriteByte(byte value)
		{
			this._innerStream.WriteByte(value);
		}

		// Token: 0x04000095 RID: 149
		private readonly Stream _innerStream;
	}
}
