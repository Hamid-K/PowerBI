using System;
using System.IO;

namespace Microsoft.Mashup.Engine1.Library.BinaryFormat
{
	// Token: 0x02000EB4 RID: 3764
	internal abstract class ReadableStream : Stream
	{
		// Token: 0x17001D13 RID: 7443
		// (get) Token: 0x06006411 RID: 25617 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D14 RID: 7444
		// (get) Token: 0x06006412 RID: 25618 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D15 RID: 7445
		// (get) Token: 0x06006413 RID: 25619 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006414 RID: 25620 RVA: 0x000033E7 File Offset: 0x000015E7
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001D16 RID: 7446
		// (get) Token: 0x06006415 RID: 25621 RVA: 0x000033E7 File Offset: 0x000015E7
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D17 RID: 7447
		// (get) Token: 0x06006416 RID: 25622 RVA: 0x000033E7 File Offset: 0x000015E7
		// (set) Token: 0x06006417 RID: 25623 RVA: 0x000033E7 File Offset: 0x000015E7
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

		// Token: 0x06006418 RID: 25624 RVA: 0x000033E7 File Offset: 0x000015E7
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006419 RID: 25625 RVA: 0x000033E7 File Offset: 0x000015E7
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600641A RID: 25626 RVA: 0x000033E7 File Offset: 0x000015E7
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}
	}
}
