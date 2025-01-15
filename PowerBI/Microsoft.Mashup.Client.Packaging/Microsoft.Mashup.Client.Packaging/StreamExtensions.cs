using System;
using System.IO;

namespace Microsoft.Mashup.Client.Packaging
{
	// Token: 0x0200000C RID: 12
	internal static class StreamExtensions
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000028D0 File Offset: 0x00000AD0
		public static byte[] ReadAllBytes(this Stream stream)
		{
			byte[] array = new byte[4096];
			byte[] array2;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num;
				while ((num = stream.Read(array, 0, array.Length)) > 0)
				{
					memoryStream.Write(array, 0, num);
				}
				array2 = memoryStream.ToArray();
			}
			return array2;
		}

		// Token: 0x04000044 RID: 68
		private const int streamBufferSize = 4096;
	}
}
