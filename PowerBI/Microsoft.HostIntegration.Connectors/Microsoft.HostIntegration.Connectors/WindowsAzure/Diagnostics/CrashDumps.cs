using System;
using System.ComponentModel;

namespace Microsoft.WindowsAzure.Diagnostics
{
	// Token: 0x02000470 RID: 1136
	[Obsolete("This API is deprecated.")]
	public static class CrashDumps
	{
		// Token: 0x06002784 RID: 10116 RVA: 0x000036A9 File Offset: 0x000018A9
		public static void EnableCollection(bool enableFullDumps)
		{
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000036A9 File Offset: 0x000018A9
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void EnableCollectionToDirectory(string directory, bool enableFullDumps)
		{
		}
	}
}
