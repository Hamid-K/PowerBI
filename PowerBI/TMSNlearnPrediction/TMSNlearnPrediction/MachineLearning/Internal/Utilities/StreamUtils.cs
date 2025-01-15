using System;
using System.IO;
using Microsoft.TMSN.IO;

namespace Microsoft.MachineLearning.Internal.Utilities
{
	// Token: 0x020004DB RID: 1243
	public static class StreamUtils
	{
		// Token: 0x0600196B RID: 6507 RVA: 0x0008FAD4 File Offset: 0x0008DCD4
		public static Stream OpenInStream(string fileName)
		{
			return ZStreamIn.Open(fileName);
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0008FADC File Offset: 0x0008DCDC
		public static StreamWriter CreateWriter(string fileName)
		{
			return ZStreamWriter.Open(fileName);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0008FAE4 File Offset: 0x0008DCE4
		public static string[] ExpandWildCards(string path)
		{
			return IOUtil.ExpandWildcards(path);
		}
	}
}
