using System;
using System.Text;

namespace Microsoft.Spatial
{
	// Token: 0x02000045 RID: 69
	internal static class OrcasExtensions
	{
		// Token: 0x060001CC RID: 460 RVA: 0x000051A9 File Offset: 0x000033A9
		internal static void Clear(this StringBuilder builder)
		{
			builder.Length = 0;
			builder.Capacity = 0;
		}
	}
}
