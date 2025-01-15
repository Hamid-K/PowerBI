using System;
using System.IO;

namespace Microsoft.Lucia.Common
{
	// Token: 0x02000027 RID: 39
	public static class StreamWriterFactory
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x000033FF File Offset: 0x000015FF
		public static StreamWriter Create(Stream stream, bool leaveOpen = false)
		{
			return new StreamWriter(stream, StreamDefaults.WriteEncoding, 1024, leaveOpen);
		}
	}
}
