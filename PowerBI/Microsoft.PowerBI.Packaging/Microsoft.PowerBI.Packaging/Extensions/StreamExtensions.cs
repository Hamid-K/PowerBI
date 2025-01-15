using System;
using System.IO;

namespace Microsoft.PowerBI.Packaging.Extensions
{
	// Token: 0x02000094 RID: 148
	public static class StreamExtensions
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x0000A9EC File Offset: 0x00008BEC
		public static byte[] ReadAllBytes(this Stream stream)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				stream.CopyTo(memoryStream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x0400022E RID: 558
		private const int streamBufferSize = 4096;
	}
}
