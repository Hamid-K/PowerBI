using System;
using System.IO;

namespace Microsoft.Internal
{
	// Token: 0x0200018C RID: 396
	internal class ContextAwareStream<T, U> : Stream where T : struct, IContext<U> where U : struct, IDisposable
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x0000E2F4 File Offset: 0x0000C4F4
		public ContextAwareStream(T context, Stream stream)
		{
			this.context = context;
			this.stream = stream;
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0000E30C File Offset: 0x0000C50C
		public override bool CanRead
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				bool canRead;
				try
				{
					canRead = this.stream.CanRead;
				}
				finally
				{
					u.Dispose();
				}
				return canRead;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0000E35C File Offset: 0x0000C55C
		public override bool CanSeek
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				bool canSeek;
				try
				{
					canSeek = this.stream.CanSeek;
				}
				finally
				{
					u.Dispose();
				}
				return canSeek;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		public override bool CanTimeout
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				bool canTimeout;
				try
				{
					canTimeout = this.stream.CanTimeout;
				}
				finally
				{
					u.Dispose();
				}
				return canTimeout;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0000E3FC File Offset: 0x0000C5FC
		public override bool CanWrite
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				bool canWrite;
				try
				{
					canWrite = this.stream.CanWrite;
				}
				finally
				{
					u.Dispose();
				}
				return canWrite;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0000E44C File Offset: 0x0000C64C
		public override long Length
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				long length;
				try
				{
					length = this.stream.Length;
				}
				finally
				{
					u.Dispose();
				}
				return length;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0000E49C File Offset: 0x0000C69C
		// (set) Token: 0x060007B2 RID: 1970 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		public override long Position
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				long position;
				try
				{
					position = this.stream.Position;
				}
				finally
				{
					u.Dispose();
				}
				return position;
			}
			set
			{
				T t = this.context;
				U u = t.Enter();
				try
				{
					this.stream.Position = value;
				}
				finally
				{
					u.Dispose();
				}
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0000E53C File Offset: 0x0000C73C
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x0000E58C File Offset: 0x0000C78C
		public override int ReadTimeout
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				int readTimeout;
				try
				{
					readTimeout = this.stream.ReadTimeout;
				}
				finally
				{
					u.Dispose();
				}
				return readTimeout;
			}
			set
			{
				T t = this.context;
				U u = t.Enter();
				try
				{
					this.stream.ReadTimeout = value;
				}
				finally
				{
					u.Dispose();
				}
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0000E5DC File Offset: 0x0000C7DC
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x0000E62C File Offset: 0x0000C82C
		public override int WriteTimeout
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				int writeTimeout;
				try
				{
					writeTimeout = this.stream.WriteTimeout;
				}
				finally
				{
					u.Dispose();
				}
				return writeTimeout;
			}
			set
			{
				T t = this.context;
				U u = t.Enter();
				try
				{
					this.stream.WriteTimeout = value;
				}
				finally
				{
					u.Dispose();
				}
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0000E67C File Offset: 0x0000C87C
		public override void Close()
		{
			T t = this.context;
			U u = t.Enter();
			try
			{
				this.stream.Close();
			}
			finally
			{
				u.Dispose();
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0000E6CC File Offset: 0x0000C8CC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				T t = this.context;
				U u = t.Enter();
				try
				{
					this.stream.Dispose();
				}
				finally
				{
					u.Dispose();
				}
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0000E71C File Offset: 0x0000C91C
		public override void Flush()
		{
			T t = this.context;
			U u = t.Enter();
			try
			{
				this.stream.Flush();
			}
			finally
			{
				u.Dispose();
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0000E76C File Offset: 0x0000C96C
		public override int Read(byte[] buffer, int offset, int count)
		{
			T t = this.context;
			U u = t.Enter();
			int num;
			try
			{
				num = this.stream.Read(buffer, offset, count);
			}
			finally
			{
				u.Dispose();
			}
			return num;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0000E7C0 File Offset: 0x0000C9C0
		public override int ReadByte()
		{
			T t = this.context;
			U u = t.Enter();
			int num;
			try
			{
				num = this.stream.ReadByte();
			}
			finally
			{
				u.Dispose();
			}
			return num;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0000E810 File Offset: 0x0000CA10
		public override long Seek(long offset, SeekOrigin origin)
		{
			T t = this.context;
			U u = t.Enter();
			long num;
			try
			{
				num = this.stream.Seek(offset, origin);
			}
			finally
			{
				u.Dispose();
			}
			return num;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0000E864 File Offset: 0x0000CA64
		public override void SetLength(long value)
		{
			T t = this.context;
			U u = t.Enter();
			try
			{
				this.stream.SetLength(value);
			}
			finally
			{
				u.Dispose();
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0000E8B4 File Offset: 0x0000CAB4
		public override void Write(byte[] buffer, int offset, int count)
		{
			T t = this.context;
			U u = t.Enter();
			try
			{
				this.stream.Write(buffer, offset, count);
			}
			finally
			{
				u.Dispose();
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0000E904 File Offset: 0x0000CB04
		public override void WriteByte(byte value)
		{
			T t = this.context;
			U u = t.Enter();
			try
			{
				this.stream.WriteByte(value);
			}
			finally
			{
				u.Dispose();
			}
		}

		// Token: 0x0400049A RID: 1178
		private readonly T context;

		// Token: 0x0400049B RID: 1179
		private readonly Stream stream;
	}
}
