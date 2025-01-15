using System;
using System.IO;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B3C RID: 6972
	internal class StreamSharer
	{
		// Token: 0x0600AE73 RID: 44659 RVA: 0x0023B917 File Offset: 0x00239B17
		public StreamSharer(Stream stream)
		{
			this.refCount = 1;
			this.stream = stream;
		}

		// Token: 0x17002BC9 RID: 11209
		// (get) Token: 0x0600AE74 RID: 44660 RVA: 0x0023B938 File Offset: 0x00239B38
		public Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x0600AE75 RID: 44661 RVA: 0x0023B940 File Offset: 0x00239B40
		public Stream Open()
		{
			return this.Open(0L, this.stream.Length);
		}

		// Token: 0x0600AE76 RID: 44662 RVA: 0x0023B958 File Offset: 0x00239B58
		public Stream Open(long offset, long length)
		{
			object obj = this.syncRoot;
			Stream stream;
			lock (obj)
			{
				this.AddRef();
				stream = new BufferedStream(new StreamSharer.SubStream(this, offset, length, true));
			}
			return stream;
		}

		// Token: 0x0600AE77 RID: 44663 RVA: 0x0023B9A8 File Offset: 0x00239BA8
		public Stream Create()
		{
			object obj = this.syncRoot;
			Stream stream;
			lock (obj)
			{
				if (this.writer != null)
				{
					throw new InvalidOperationException();
				}
				this.AddRef();
				this.writer = new StreamSharer.SubStream(this, this.stream.Length, 0L, false);
				stream = new BufferedStream(this.writer);
			}
			return stream;
		}

		// Token: 0x0600AE78 RID: 44664 RVA: 0x0023BA20 File Offset: 0x00239C20
		public void Close()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (!this.closed)
				{
					this.Release();
					this.closed = true;
				}
			}
		}

		// Token: 0x0600AE79 RID: 44665 RVA: 0x0023BA70 File Offset: 0x00239C70
		private void MoveTo(StreamSharer.SubStream subStream, long position)
		{
			if (this.stream.Position != position)
			{
				this.stream.Position = position;
			}
		}

		// Token: 0x0600AE7A RID: 44666 RVA: 0x0023BA8C File Offset: 0x00239C8C
		private int ReadByteAt(StreamSharer.SubStream subStream, long position)
		{
			object obj = this.syncRoot;
			int num;
			lock (obj)
			{
				this.MoveTo(subStream, position);
				num = this.stream.ReadByte();
			}
			return num;
		}

		// Token: 0x0600AE7B RID: 44667 RVA: 0x0023BADC File Offset: 0x00239CDC
		private int ReadAt(StreamSharer.SubStream subStream, long position, byte[] buffer, int offset, int count)
		{
			object obj = this.syncRoot;
			int num;
			lock (obj)
			{
				this.MoveTo(subStream, position);
				num = this.stream.Read(buffer, offset, count);
			}
			return num;
		}

		// Token: 0x0600AE7C RID: 44668 RVA: 0x0023BB30 File Offset: 0x00239D30
		private void WriteByteAt(StreamSharer.SubStream subStream, long position, byte value)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.MoveTo(subStream, position);
				this.stream.WriteByte(value);
			}
		}

		// Token: 0x0600AE7D RID: 44669 RVA: 0x0023BB80 File Offset: 0x00239D80
		private void WriteAt(StreamSharer.SubStream subStream, long position, byte[] buffer, int offset, int count)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.MoveTo(subStream, position);
				this.stream.Write(buffer, offset, count);
			}
		}

		// Token: 0x0600AE7E RID: 44670 RVA: 0x0023BBD4 File Offset: 0x00239DD4
		private void Flush()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.stream.Flush();
			}
		}

		// Token: 0x0600AE7F RID: 44671 RVA: 0x0023BC1C File Offset: 0x00239E1C
		private void Close(StreamSharer.SubStream subStream)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (subStream == this.writer)
				{
					this.writer = null;
				}
				this.Release();
			}
		}

		// Token: 0x0600AE80 RID: 44672 RVA: 0x0023BC6C File Offset: 0x00239E6C
		private void AddRef()
		{
			this.refCount++;
		}

		// Token: 0x0600AE81 RID: 44673 RVA: 0x0023BC7C File Offset: 0x00239E7C
		private void Release()
		{
			if (this.stream != null)
			{
				this.refCount--;
				if (this.refCount == 0)
				{
					this.stream.Close();
					this.stream = null;
				}
			}
		}

		// Token: 0x040059FA RID: 23034
		private Stream stream;

		// Token: 0x040059FB RID: 23035
		private int refCount;

		// Token: 0x040059FC RID: 23036
		private StreamSharer.SubStream writer;

		// Token: 0x040059FD RID: 23037
		private bool closed;

		// Token: 0x040059FE RID: 23038
		private readonly object syncRoot = new object();

		// Token: 0x02001B3D RID: 6973
		private class SubStream : Stream
		{
			// Token: 0x0600AE82 RID: 44674 RVA: 0x0023BCAE File Offset: 0x00239EAE
			public SubStream(StreamSharer sharer, long offset, long length, bool readOnly)
			{
				this.sharer = sharer;
				this.position = 0L;
				this.offset = offset;
				this.length = length;
				this.readOnly = readOnly;
			}

			// Token: 0x17002BCA RID: 11210
			// (get) Token: 0x0600AE83 RID: 44675 RVA: 0x00002139 File Offset: 0x00000339
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17002BCB RID: 11211
			// (get) Token: 0x0600AE84 RID: 44676 RVA: 0x00002139 File Offset: 0x00000339
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17002BCC RID: 11212
			// (get) Token: 0x0600AE85 RID: 44677 RVA: 0x0023BCDB File Offset: 0x00239EDB
			public override bool CanWrite
			{
				get
				{
					return !this.readOnly;
				}
			}

			// Token: 0x17002BCD RID: 11213
			// (get) Token: 0x0600AE86 RID: 44678 RVA: 0x0023BCE6 File Offset: 0x00239EE6
			public override long Length
			{
				get
				{
					return this.length;
				}
			}

			// Token: 0x17002BCE RID: 11214
			// (get) Token: 0x0600AE87 RID: 44679 RVA: 0x0023BCEE File Offset: 0x00239EEE
			// (set) Token: 0x0600AE88 RID: 44680 RVA: 0x0023BCF6 File Offset: 0x00239EF6
			public override long Position
			{
				get
				{
					return this.position;
				}
				set
				{
					this.position = value;
				}
			}

			// Token: 0x0600AE89 RID: 44681 RVA: 0x0023BCFF File Offset: 0x00239EFF
			public override void Close()
			{
				if (this.sharer != null)
				{
					this.sharer.Close(this);
					this.sharer = null;
				}
			}

			// Token: 0x0600AE8A RID: 44682 RVA: 0x0023BD1C File Offset: 0x00239F1C
			public override void Flush()
			{
				if (this.readOnly)
				{
					throw new InvalidOperationException();
				}
				this.sharer.Flush();
			}

			// Token: 0x0600AE8B RID: 44683 RVA: 0x0023BD37 File Offset: 0x00239F37
			public override int ReadByte()
			{
				if (this.position >= this.length)
				{
					return -1;
				}
				int num = this.sharer.ReadByteAt(this, this.offset + this.position);
				if (num != -1)
				{
					this.position += 1L;
				}
				return num;
			}

			// Token: 0x0600AE8C RID: 44684 RVA: 0x0023BD78 File Offset: 0x00239F78
			public override int Read(byte[] buffer, int offset, int count)
			{
				if (this.position >= this.length)
				{
					return 0;
				}
				int num = this.sharer.ReadAt(this, this.offset + this.position, buffer, offset, (int)Math.Min((long)count, this.length - this.position));
				this.position += (long)num;
				return num;
			}

			// Token: 0x0600AE8D RID: 44685 RVA: 0x0023BDD8 File Offset: 0x00239FD8
			public override void WriteByte(byte value)
			{
				if (this.readOnly)
				{
					throw new InvalidOperationException();
				}
				this.sharer.WriteByteAt(this, this.offset + this.position, value);
				this.position += 1L;
				if (this.position > this.length)
				{
					this.length = this.position;
				}
			}

			// Token: 0x0600AE8E RID: 44686 RVA: 0x0023BE38 File Offset: 0x0023A038
			public override void Write(byte[] buffer, int offset, int count)
			{
				if (this.readOnly)
				{
					throw new InvalidOperationException();
				}
				this.sharer.WriteAt(this, this.offset + this.position, buffer, offset, count);
				this.position += (long)count;
				if (this.position > this.length)
				{
					this.length = this.position;
				}
			}

			// Token: 0x0600AE8F RID: 44687 RVA: 0x0023BE98 File Offset: 0x0023A098
			public override long Seek(long offset, SeekOrigin origin)
			{
				switch (origin)
				{
				case SeekOrigin.Begin:
					this.position = offset;
					break;
				case SeekOrigin.Current:
					this.position += offset;
					break;
				case SeekOrigin.End:
					this.position = this.Length + offset;
					break;
				default:
					throw new InvalidOperationException();
				}
				return this.position;
			}

			// Token: 0x0600AE90 RID: 44688 RVA: 0x000091AE File Offset: 0x000073AE
			public override void SetLength(long value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x040059FF RID: 23039
			private StreamSharer sharer;

			// Token: 0x04005A00 RID: 23040
			private long position;

			// Token: 0x04005A01 RID: 23041
			private long offset;

			// Token: 0x04005A02 RID: 23042
			private long length;

			// Token: 0x04005A03 RID: 23043
			private bool readOnly;
		}
	}
}
