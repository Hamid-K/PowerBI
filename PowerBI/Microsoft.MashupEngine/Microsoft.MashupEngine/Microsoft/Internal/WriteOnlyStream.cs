using System;
using System.IO;

namespace Microsoft.Internal
{
	// Token: 0x020001CB RID: 459
	internal abstract class WriteOnlyStream : Stream
	{
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override int ReadByte()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008DC RID: 2268
		public abstract override void Flush();

		// Token: 0x060008DD RID: 2269
		public abstract override void WriteByte(byte value);

		// Token: 0x060008DE RID: 2270
		public abstract override void Write(byte[] buffer, int offset, int count);
	}
}
