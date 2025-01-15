using System;
using System.IO;

namespace Microsoft.Internal
{
	// Token: 0x020001C7 RID: 455
	internal abstract class ReadOnlyStream : Stream
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void Flush()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void SetLength(long value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008CD RID: 2253
		public abstract override int ReadByte();

		// Token: 0x060008CE RID: 2254
		public abstract override int Read(byte[] buffer, int offset, int count);
	}
}
