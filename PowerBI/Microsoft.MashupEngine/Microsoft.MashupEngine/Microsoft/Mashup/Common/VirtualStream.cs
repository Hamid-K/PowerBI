using System;
using System.IO;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C37 RID: 7223
	public abstract class VirtualStream : Stream
	{
		// Token: 0x17002D1B RID: 11547
		// (get) Token: 0x0600B452 RID: 46162 RVA: 0x00249609 File Offset: 0x00247809
		public override bool CanRead
		{
			get
			{
				return this.Stream.CanRead;
			}
		}

		// Token: 0x17002D1C RID: 11548
		// (get) Token: 0x0600B453 RID: 46163 RVA: 0x00249616 File Offset: 0x00247816
		public override bool CanSeek
		{
			get
			{
				return this.Stream.CanSeek;
			}
		}

		// Token: 0x17002D1D RID: 11549
		// (get) Token: 0x0600B454 RID: 46164 RVA: 0x00249623 File Offset: 0x00247823
		public override bool CanTimeout
		{
			get
			{
				return this.Stream.CanTimeout;
			}
		}

		// Token: 0x17002D1E RID: 11550
		// (get) Token: 0x0600B455 RID: 46165 RVA: 0x00249630 File Offset: 0x00247830
		public override bool CanWrite
		{
			get
			{
				return this.Stream.CanWrite;
			}
		}

		// Token: 0x17002D1F RID: 11551
		// (get) Token: 0x0600B456 RID: 46166 RVA: 0x0024963D File Offset: 0x0024783D
		public override long Length
		{
			get
			{
				return this.Stream.Length;
			}
		}

		// Token: 0x17002D20 RID: 11552
		// (get) Token: 0x0600B457 RID: 46167 RVA: 0x0024964A File Offset: 0x0024784A
		// (set) Token: 0x0600B458 RID: 46168 RVA: 0x00249657 File Offset: 0x00247857
		public override long Position
		{
			get
			{
				return this.Stream.Position;
			}
			set
			{
				this.Stream.Position = value;
			}
		}

		// Token: 0x17002D21 RID: 11553
		// (get) Token: 0x0600B459 RID: 46169 RVA: 0x00249665 File Offset: 0x00247865
		// (set) Token: 0x0600B45A RID: 46170 RVA: 0x00249672 File Offset: 0x00247872
		public override int ReadTimeout
		{
			get
			{
				return this.Stream.ReadTimeout;
			}
			set
			{
				this.Stream.ReadTimeout = value;
			}
		}

		// Token: 0x17002D22 RID: 11554
		// (get) Token: 0x0600B45B RID: 46171 RVA: 0x00249680 File Offset: 0x00247880
		// (set) Token: 0x0600B45C RID: 46172 RVA: 0x0024968D File Offset: 0x0024788D
		public override int WriteTimeout
		{
			get
			{
				return this.Stream.WriteTimeout;
			}
			set
			{
				this.Stream.WriteTimeout = value;
			}
		}

		// Token: 0x17002D23 RID: 11555
		// (get) Token: 0x0600B45D RID: 46173
		protected abstract Stream Stream { get; }

		// Token: 0x0600B45E RID: 46174 RVA: 0x0024969B File Offset: 0x0024789B
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.Stream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x0600B45F RID: 46175 RVA: 0x002496AF File Offset: 0x002478AF
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.Stream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x0600B460 RID: 46176 RVA: 0x002496C3 File Offset: 0x002478C3
		public override void Close()
		{
			this.Stream.Close();
		}

		// Token: 0x0600B461 RID: 46177 RVA: 0x002496D0 File Offset: 0x002478D0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Stream.Dispose();
			}
		}

		// Token: 0x0600B462 RID: 46178 RVA: 0x002496E0 File Offset: 0x002478E0
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.Stream.EndRead(asyncResult);
		}

		// Token: 0x0600B463 RID: 46179 RVA: 0x002496EE File Offset: 0x002478EE
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.Stream.EndWrite(asyncResult);
		}

		// Token: 0x0600B464 RID: 46180 RVA: 0x002496FC File Offset: 0x002478FC
		public override void Flush()
		{
			this.Stream.Flush();
		}

		// Token: 0x0600B465 RID: 46181 RVA: 0x00249709 File Offset: 0x00247909
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.Stream.Read(buffer, offset, count);
		}

		// Token: 0x0600B466 RID: 46182 RVA: 0x00249719 File Offset: 0x00247919
		public override int ReadByte()
		{
			return this.Stream.ReadByte();
		}

		// Token: 0x0600B467 RID: 46183 RVA: 0x00249726 File Offset: 0x00247926
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.Stream.Seek(offset, origin);
		}

		// Token: 0x0600B468 RID: 46184 RVA: 0x00249735 File Offset: 0x00247935
		public override void SetLength(long value)
		{
			this.Stream.SetLength(value);
		}

		// Token: 0x0600B469 RID: 46185 RVA: 0x00249743 File Offset: 0x00247943
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.Stream.Write(buffer, offset, count);
		}

		// Token: 0x0600B46A RID: 46186 RVA: 0x00249753 File Offset: 0x00247953
		public override void WriteByte(byte value)
		{
			this.Stream.WriteByte(value);
		}
	}
}
