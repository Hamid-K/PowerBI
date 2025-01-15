using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002D2 RID: 722
	internal class IOState
	{
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x0005172D File Offset: 0x0004F92D
		// (set) Token: 0x06001AD2 RID: 6866 RVA: 0x00051735 File Offset: 0x0004F935
		public int Length { get; private set; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x0005173E File Offset: 0x0004F93E
		// (set) Token: 0x06001AD4 RID: 6868 RVA: 0x00051746 File Offset: 0x0004F946
		public int ByteTransferred { get; set; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x0005174F File Offset: 0x0004F94F
		public ArraySegment<byte> CurrentBuffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00051757 File Offset: 0x0004F957
		public IOState(ArraySegment<byte> buffer)
		{
			this._buffer = buffer;
			this.Length = this._buffer.Count;
		}

		// Token: 0x04000E37 RID: 3639
		private readonly ArraySegment<byte> _buffer;
	}
}
