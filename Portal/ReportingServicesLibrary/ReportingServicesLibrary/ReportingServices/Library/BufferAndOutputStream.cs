using System;
using System.IO;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000246 RID: 582
	internal sealed class BufferAndOutputStream : MemoryThenFileStream
	{
		// Token: 0x0600153B RID: 5435 RVA: 0x000545A0 File Offset: 0x000527A0
		public BufferAndOutputStream(Stream outputStream)
		{
			this.m_outputStream = outputStream;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x000545AF File Offset: 0x000527AF
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_outputStream.Write(buffer, offset, count);
			base.Write(buffer, offset, count);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x000545C8 File Offset: 0x000527C8
		public override void WriteByte(byte value)
		{
			this.m_outputStream.WriteByte(value);
			base.WriteByte(value);
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x000545DD File Offset: 0x000527DD
		public override long Length
		{
			get
			{
				return base.Length;
			}
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x000545E8 File Offset: 0x000527E8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.m_outputStream != null)
				{
					this.m_outputStream.Close();
				}
			}
			finally
			{
				this.m_outputStream = null;
				base.Dispose(disposing);
			}
		}

		// Token: 0x040007BD RID: 1981
		private Stream m_outputStream;
	}
}
