using System;
using System.Text;

namespace Microsoft.Spatial
{
	// Token: 0x02000040 RID: 64
	internal static class OrcasExtensions
	{
		// Token: 0x0600018B RID: 395 RVA: 0x0000466B File Offset: 0x0000286B
		internal static void Clear(this StringBuilder builder)
		{
			builder.Length = 0;
			builder.Capacity = 0;
		}
	}
}
