using System;
using System.IO;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A19 RID: 6681
	internal class MemoryMappedViewStream : Stream
	{
		// Token: 0x0600A90E RID: 43278 RVA: 0x0022F9B3 File Offset: 0x0022DBB3
		public MemoryMappedViewStream(MemoryMappedView view)
		{
			this.view = view;
			this.singleByteBuffer = new byte[1];
		}

		// Token: 0x17002AF7 RID: 10999
		// (get) Token: 0x0600A90F RID: 43279 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002AF8 RID: 11000
		// (get) Token: 0x0600A910 RID: 43280 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002AF9 RID: 11001
		// (get) Token: 0x0600A911 RID: 43281 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002AFA RID: 11002
		// (get) Token: 0x0600A912 RID: 43282 RVA: 0x0022F9CE File Offset: 0x0022DBCE
		public override long Length
		{
			get
			{
				return (long)this.length;
			}
		}

		// Token: 0x17002AFB RID: 11003
		// (get) Token: 0x0600A913 RID: 43283 RVA: 0x0022F9D7 File Offset: 0x0022DBD7
		// (set) Token: 0x0600A914 RID: 43284 RVA: 0x0022F9E0 File Offset: 0x0022DBE0
		public override long Position
		{
			get
			{
				return (long)this.position;
			}
			set
			{
				if (value > (long)this.length)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.position = (int)value;
			}
		}

		// Token: 0x0600A915 RID: 43285 RVA: 0x0022F9FA File Offset: 0x0022DBFA
		public override void SetLength(long length)
		{
			if (length > (long)((ulong)this.view.Length))
			{
				throw new ArgumentOutOfRangeException();
			}
			this.length = (int)length;
		}

		// Token: 0x0600A916 RID: 43286 RVA: 0x0000336E File Offset: 0x0000156E
		public override void Flush()
		{
		}

		// Token: 0x0600A917 RID: 43287 RVA: 0x0022FA1C File Offset: 0x0022DC1C
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = Math.Min(count, this.length - this.position);
			this.view.Read(this.position, num, buffer, offset);
			this.position += num;
			return num;
		}

		// Token: 0x0600A918 RID: 43288 RVA: 0x0022FA60 File Offset: 0x0022DC60
		public override int ReadByte()
		{
			if (this.Read(this.singleByteBuffer, 0, 1) == 1)
			{
				return (int)this.singleByteBuffer[0];
			}
			return -1;
		}

		// Token: 0x0600A919 RID: 43289 RVA: 0x0022FA7D File Offset: 0x0022DC7D
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (origin == SeekOrigin.Begin && offset <= (long)this.length)
			{
				this.Position = offset;
				return offset;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600A91A RID: 43290 RVA: 0x0022FA9C File Offset: 0x0022DC9C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if ((long)(this.position + count) > (long)((ulong)this.view.Length))
			{
				throw new InvalidOperationException();
			}
			this.view.Write(this.position, count, buffer, offset);
			this.position += count;
			this.length = Math.Max(this.length, this.position);
		}

		// Token: 0x0600A91B RID: 43291 RVA: 0x0022FAFF File Offset: 0x0022DCFF
		public override void WriteByte(byte value)
		{
			this.singleByteBuffer[0] = value;
			this.Write(this.singleByteBuffer, 0, 1);
		}

		// Token: 0x040057F6 RID: 22518
		private readonly MemoryMappedView view;

		// Token: 0x040057F7 RID: 22519
		private readonly byte[] singleByteBuffer;

		// Token: 0x040057F8 RID: 22520
		private int position;

		// Token: 0x040057F9 RID: 22521
		private int length;
	}
}
