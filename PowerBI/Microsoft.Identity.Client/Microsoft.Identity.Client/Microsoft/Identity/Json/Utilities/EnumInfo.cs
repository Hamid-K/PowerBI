using System;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x02000057 RID: 87
	internal class EnumInfo
	{
		// Token: 0x0600050A RID: 1290 RVA: 0x00015217 File Offset: 0x00013417
		internal EnumInfo(bool isFlags, ulong[] values, string[] names, string[] resolvedNames)
		{
			this.IsFlags = isFlags;
			this.Values = values;
			this.Names = names;
			this.ResolvedNames = resolvedNames;
		}

		// Token: 0x040001C3 RID: 451
		public readonly bool IsFlags;

		// Token: 0x040001C4 RID: 452
		public readonly ulong[] Values;

		// Token: 0x040001C5 RID: 453
		public readonly string[] Names;

		// Token: 0x040001C6 RID: 454
		public readonly string[] ResolvedNames;
	}
}
