using System;
using System.IO;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200037B RID: 891
	public class SafeStreamWriter : StreamWriter
	{
		// Token: 0x06001F87 RID: 8071 RVA: 0x00060313 File Offset: 0x0005E513
		public SafeStreamWriter(Stream stream)
			: base(stream, new UTF8Encoding(false, false))
		{
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00060323 File Offset: 0x0005E523
		public SafeStreamWriter(string path)
			: base(path, false, new UTF8Encoding(false, false))
		{
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x00060334 File Offset: 0x0005E534
		public SafeStreamWriter(string path, bool append)
			: base(path, append, new UTF8Encoding(false, false))
		{
		}
	}
}
