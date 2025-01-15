using System;
using System.IO;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200006D RID: 109
	public sealed class SafeFile
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x0000A86C File Offset: 0x00008A6C
		internal SafeFile(string filePath)
		{
			this._fileData = new Lazy<byte[]>(() => File.ReadAllBytes(filePath));
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000A8A3 File Offset: 0x00008AA3
		internal byte[] GetAllBytes()
		{
			return this._fileData.Value;
		}

		// Token: 0x040000DF RID: 223
		private readonly Lazy<byte[]> _fileData;
	}
}
