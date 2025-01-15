using System;
using System.IO;

namespace Microsoft.Lucia.Common
{
	// Token: 0x02000026 RID: 38
	public static class StreamReaderFactory
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x000033EB File Offset: 0x000015EB
		public static StreamReader Create(Stream stream, bool leaveOpen = false)
		{
			return new StreamReader(stream, StreamDefaults.ReadEncoding, true, 1024, leaveOpen);
		}
	}
}
