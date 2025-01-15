using System;
using System.IO;
using System.IO.Packaging;

namespace Microsoft.Mashup.Client.Packaging
{
	// Token: 0x02000007 RID: 7
	public abstract class MashupReader
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002164 File Offset: 0x00000364
		public bool TryGetMashupBytes(Stream excelStream, out byte[] mashupBytes)
		{
			bool flag;
			using (Package package = Package.Open(excelStream))
			{
				flag = this.TryGetMashupBytes(package, out mashupBytes);
			}
			return flag;
		}

		// Token: 0x06000006 RID: 6
		public abstract bool TryGetMashupBytes(Package excelPackage, out byte[] mashupBytes);
	}
}
