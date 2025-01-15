using System;
using System.Text;

namespace System.Spatial
{
	// Token: 0x02000044 RID: 68
	internal static class OrcasExtensions
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x00005239 File Offset: 0x00003439
		internal static void Clear(this StringBuilder builder)
		{
			builder.Length = 0;
			builder.Capacity = 0;
		}
	}
}
