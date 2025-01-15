using System;
using System.IO;

namespace Microsoft.Internal
{
	// Token: 0x020001AC RID: 428
	internal abstract class ForwardWriteOnlyStream : WriteOnlyStream
	{
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0000EE09 File Offset: 0x0000D009
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x0000EE09 File Offset: 0x0000D009
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

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override long Length
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void SetLength(long value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new InvalidOperationException();
		}
	}
}
