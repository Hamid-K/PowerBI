using System;
using System.IO;

namespace Microsoft.Internal
{
	// Token: 0x020001AB RID: 427
	internal abstract class ForwardReadOnlyStream : ReadOnlyStream
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0000EE09 File Offset: 0x0000D009
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override long Position
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override long Length
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new InvalidOperationException();
		}
	}
}
