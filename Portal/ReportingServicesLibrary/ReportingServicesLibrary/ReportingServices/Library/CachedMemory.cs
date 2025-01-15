using System;
using System.IO;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200024B RID: 587
	internal class CachedMemory
	{
		// Token: 0x06001589 RID: 5513 RVA: 0x0005511C File Offset: 0x0005331C
		public CachedMemory(Stream cachedStream)
		{
			this.m_cachedBytes = new byte[cachedStream.Length];
			cachedStream.Position = 0L;
			cachedStream.Read(this.m_cachedBytes, 0, (int)cachedStream.Length);
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00055153 File Offset: 0x00053353
		public MemoryStream NewMemoryStream()
		{
			return new MemoryStream(this.m_cachedBytes);
		}

		// Token: 0x040007D4 RID: 2004
		private byte[] m_cachedBytes;
	}
}
