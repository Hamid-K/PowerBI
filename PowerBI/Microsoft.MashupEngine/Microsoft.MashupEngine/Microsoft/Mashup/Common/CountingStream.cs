using System;
using System.IO;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BDB RID: 7131
	public sealed class CountingStream : DelegatingStream
	{
		// Token: 0x0600B20B RID: 45579 RVA: 0x0000FF57 File Offset: 0x0000E157
		private CountingStream(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x0600B20C RID: 45580 RVA: 0x002450BB File Offset: 0x002432BB
		public static Stream New(Stream stream)
		{
			if (!stream.CanSeek)
			{
				return new CountingStream(stream);
			}
			return stream;
		}

		// Token: 0x17002CB9 RID: 11449
		// (get) Token: 0x0600B20D RID: 45581 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002CBA RID: 11450
		// (get) Token: 0x0600B20E RID: 45582 RVA: 0x002450CD File Offset: 0x002432CD
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17002CBB RID: 11451
		// (get) Token: 0x0600B20F RID: 45583 RVA: 0x000033E7 File Offset: 0x000015E7
		// (set) Token: 0x0600B210 RID: 45584 RVA: 0x000033E7 File Offset: 0x000015E7
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600B211 RID: 45585 RVA: 0x002450D8 File Offset: 0x002432D8
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = base.Read(buffer, offset, count);
			this.length += (long)num;
			return num;
		}

		// Token: 0x0600B212 RID: 45586 RVA: 0x002450FF File Offset: 0x002432FF
		public override int ReadByte()
		{
			int num = base.ReadByte();
			if (num >= 0)
			{
				this.length += 1L;
			}
			return num;
		}

		// Token: 0x0600B213 RID: 45587 RVA: 0x0024511A File Offset: 0x0024331A
		public override void Write(byte[] buffer, int offset, int count)
		{
			base.Write(buffer, offset, count);
			this.length += (long)count;
		}

		// Token: 0x0600B214 RID: 45588 RVA: 0x00245134 File Offset: 0x00243334
		public override void WriteByte(byte value)
		{
			base.WriteByte(value);
			this.length += 1L;
		}

		// Token: 0x04005B2C RID: 23340
		private long length;
	}
}
