using System;
using System.Text;

namespace Microsoft.Lucia.Common
{
	// Token: 0x02000025 RID: 37
	internal static class StreamDefaults
	{
		// Token: 0x0400004E RID: 78
		internal const int BufferSize = 1024;

		// Token: 0x0400004F RID: 79
		internal static readonly Encoding ReadEncoding = new UTF8Encoding(false, false);

		// Token: 0x04000050 RID: 80
		internal static readonly Encoding WriteEncoding = new UTF8Encoding(false, true);
	}
}
