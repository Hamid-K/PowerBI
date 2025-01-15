using System;
using System.IO;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C05 RID: 7173
	public sealed class OffsetStream : DelegatingStream
	{
		// Token: 0x0600B30C RID: 45836 RVA: 0x00246F75 File Offset: 0x00245175
		public OffsetStream(Stream stream, long offset)
			: base(stream)
		{
			this.offset = offset;
		}

		// Token: 0x17002CE3 RID: 11491
		// (get) Token: 0x0600B30D RID: 45837 RVA: 0x00246F85 File Offset: 0x00245185
		public override long Length
		{
			get
			{
				return base.Length - this.offset;
			}
		}

		// Token: 0x17002CE4 RID: 11492
		// (get) Token: 0x0600B30E RID: 45838 RVA: 0x00246F94 File Offset: 0x00245194
		// (set) Token: 0x0600B30F RID: 45839 RVA: 0x00246FA3 File Offset: 0x002451A3
		public override long Position
		{
			get
			{
				return base.Position - this.offset;
			}
			set
			{
				base.Position = value + this.offset;
			}
		}

		// Token: 0x0600B310 RID: 45840 RVA: 0x00246FB4 File Offset: 0x002451B4
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this.Position + offset;
				break;
			case SeekOrigin.End:
				num = this.Length + offset;
				break;
			default:
				throw new InvalidOperationException();
			}
			if (num < 0L)
			{
				throw new EndOfStreamException();
			}
			return base.Seek(this.offset + num, SeekOrigin.Begin) - this.offset;
		}

		// Token: 0x0600B311 RID: 45841 RVA: 0x00247016 File Offset: 0x00245216
		public override void SetLength(long value)
		{
			base.SetLength(this.offset + value);
		}

		// Token: 0x04005B63 RID: 23395
		private readonly long offset;
	}
}
