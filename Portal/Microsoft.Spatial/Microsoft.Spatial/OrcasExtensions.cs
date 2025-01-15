using System;
using System.Text;

namespace Microsoft.Spatial
{
	// Token: 0x02000045 RID: 69
	internal static class OrcasExtensions
	{
		// Token: 0x06000201 RID: 513 RVA: 0x0000533F File Offset: 0x0000353F
		internal static void Clear(this StringBuilder builder)
		{
			builder.Length = 0;
			builder.Capacity = 0;
		}
	}
}
