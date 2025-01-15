using System;
using System.IO;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200014F RID: 335
	internal abstract class SmiStream
	{
		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06001A0C RID: 6668
		public abstract bool CanRead { get; }

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06001A0D RID: 6669
		public abstract bool CanSeek { get; }

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06001A0E RID: 6670
		public abstract bool CanWrite { get; }

		// Token: 0x06001A0F RID: 6671
		public abstract long GetLength(SmiEventSink sink);

		// Token: 0x06001A10 RID: 6672
		public abstract long GetPosition(SmiEventSink sink);

		// Token: 0x06001A11 RID: 6673
		public abstract void SetPosition(SmiEventSink sink, long position);

		// Token: 0x06001A12 RID: 6674
		public abstract void Flush(SmiEventSink sink);

		// Token: 0x06001A13 RID: 6675
		public abstract long Seek(SmiEventSink sink, long offset, SeekOrigin origin);

		// Token: 0x06001A14 RID: 6676
		public abstract void SetLength(SmiEventSink sink, long value);

		// Token: 0x06001A15 RID: 6677
		public abstract int Read(SmiEventSink sink, byte[] buffer, int offset, int count);

		// Token: 0x06001A16 RID: 6678
		public abstract void Write(SmiEventSink sink, byte[] buffer, int offset, int count);

		// Token: 0x06001A17 RID: 6679
		public abstract int Read(SmiEventSink sink, char[] buffer, int offset, int count);

		// Token: 0x06001A18 RID: 6680
		public abstract void Write(SmiEventSink sink, char[] buffer, int offset, int count);
	}
}
