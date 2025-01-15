using System;
using System.IO;

namespace Microsoft.InfoNav
{
	// Token: 0x02000020 RID: 32
	internal sealed class NonClosingStreamProxy : Stream
	{
		// Token: 0x060001DA RID: 474 RVA: 0x00005BF0 File Offset: 0x00003DF0
		public NonClosingStreamProxy(Stream underlyingStream)
		{
			if (underlyingStream == null)
			{
				throw new ArgumentNullException("underlyingStream");
			}
			this.m_underlyingStream = underlyingStream;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00005C0D File Offset: 0x00003E0D
		public override bool CanRead
		{
			get
			{
				return this.m_underlyingStream.CanRead;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00005C1A File Offset: 0x00003E1A
		public override bool CanSeek
		{
			get
			{
				return this.m_underlyingStream.CanSeek;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00005C27 File Offset: 0x00003E27
		public override bool CanWrite
		{
			get
			{
				return this.m_underlyingStream.CanWrite;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00005C34 File Offset: 0x00003E34
		public override long Length
		{
			get
			{
				return this.m_underlyingStream.Length;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00005C41 File Offset: 0x00003E41
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00005C4E File Offset: 0x00003E4E
		public override long Position
		{
			get
			{
				return this.m_underlyingStream.Position;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00005C55 File Offset: 0x00003E55
		public override void Flush()
		{
			this.m_underlyingStream.Flush();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005C62 File Offset: 0x00003E62
		public override int ReadByte()
		{
			return this.m_underlyingStream.ReadByte();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005C6F File Offset: 0x00003E6F
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_underlyingStream.Read(buffer, offset, count);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005C7F File Offset: 0x00003E7F
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_underlyingStream.Seek(offset, origin);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00005C8E File Offset: 0x00003E8E
		public override void SetLength(long value)
		{
			this.m_underlyingStream.SetLength(value);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005C9C File Offset: 0x00003E9C
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_underlyingStream.Write(buffer, offset, count);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00005CAC File Offset: 0x00003EAC
		public override void Close()
		{
		}

		// Token: 0x0400004D RID: 77
		private readonly Stream m_underlyingStream;
	}
}
