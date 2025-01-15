using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.IO
{
	// Token: 0x0200002D RID: 45
	public class EncapsulatedStream
	{
		// Token: 0x060000FC RID: 252 RVA: 0x000100A5 File Offset: 0x0000E2A5
		public EncapsulatedStream()
		{
			this.m_baseStream = new MemoryStream();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000100B8 File Offset: 0x0000E2B8
		public EncapsulatedStream(Stream baseStream)
		{
			this.m_baseStream = baseStream;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000100C7 File Offset: 0x0000E2C7
		public Stream BaseStream
		{
			get
			{
				return this.m_baseStream;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000100CF File Offset: 0x0000E2CF
		public bool CanRead
		{
			get
			{
				return this.m_baseStream.CanRead;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000100DC File Offset: 0x0000E2DC
		public bool CanSeek
		{
			get
			{
				return this.m_baseStream.CanSeek;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000100E9 File Offset: 0x0000E2E9
		[ComVisible(false)]
		public virtual bool CanTimeout
		{
			get
			{
				return this.m_baseStream.CanTimeout;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000100F6 File Offset: 0x0000E2F6
		public virtual bool CanWrite
		{
			get
			{
				return this.m_baseStream.CanWrite;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00010103 File Offset: 0x0000E303
		public virtual long Length
		{
			get
			{
				return this.m_baseStream.Length;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00010110 File Offset: 0x0000E310
		// (set) Token: 0x06000105 RID: 261 RVA: 0x0001011D File Offset: 0x0000E31D
		public virtual long Position
		{
			get
			{
				return this.m_baseStream.Position;
			}
			set
			{
				this.m_baseStream.Position = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0001012B File Offset: 0x0000E32B
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00010138 File Offset: 0x0000E338
		[ComVisible(false)]
		public virtual int ReadTimeout
		{
			get
			{
				return this.m_baseStream.ReadTimeout;
			}
			set
			{
				this.m_baseStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00010146 File Offset: 0x0000E346
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00010153 File Offset: 0x0000E353
		[ComVisible(false)]
		public virtual int WriteTimeout
		{
			get
			{
				return this.m_baseStream.WriteTimeout;
			}
			set
			{
				this.m_baseStream.WriteTimeout = value;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00010161 File Offset: 0x0000E361
		public virtual IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00010168 File Offset: 0x0000E368
		public virtual IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0001016F File Offset: 0x0000E36F
		public virtual void Close()
		{
			this.m_baseStream.Close();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0001017C File Offset: 0x0000E37C
		public virtual void CopyTo(Stream destination)
		{
			this.m_baseStream.CopyTo(destination);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0001018A File Offset: 0x0000E38A
		public virtual void CopyTo(Stream destination, int bufferSize)
		{
			this.m_baseStream.CopyTo(destination, bufferSize);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00010199 File Offset: 0x0000E399
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.m_baseStream.Dispose();
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000101A9 File Offset: 0x0000E3A9
		public virtual int EndRead(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000101B0 File Offset: 0x0000E3B0
		public virtual void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000101B7 File Offset: 0x0000E3B7
		public virtual void Flush()
		{
			this.m_baseStream.Flush();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000101C4 File Offset: 0x0000E3C4
		public virtual int Read(byte[] buffer, int offset, int count)
		{
			return this.m_baseStream.Read(buffer, offset, count);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000101D4 File Offset: 0x0000E3D4
		public virtual int ReadByte()
		{
			return this.m_baseStream.ReadByte();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000101E1 File Offset: 0x0000E3E1
		public virtual long Seek(long offset, SeekOrigin origin)
		{
			return this.m_baseStream.Seek(offset, origin);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000101F0 File Offset: 0x0000E3F0
		public virtual void SetLength(long value)
		{
			this.m_baseStream.SetLength(value);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000101FE File Offset: 0x0000E3FE
		public virtual void Write(byte[] buffer, int offset, int count)
		{
			this.m_baseStream.Write(buffer, offset, count);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0001020E File Offset: 0x0000E40E
		public virtual void WriteByte(byte value)
		{
			this.m_baseStream.WriteByte(value);
		}

		// Token: 0x0400002E RID: 46
		private Stream m_baseStream;
	}
}
