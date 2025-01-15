using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData
{
	// Token: 0x02000010 RID: 16
	[SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
	public enum ODataLibraryCompatibility
	{
		// Token: 0x04000029 RID: 41
		Version6 = 60000,
		// Token: 0x0400002A RID: 42
		Version7 = 70000,
		// Token: 0x0400002B RID: 43
		Latest = 2147483647
	}
}
