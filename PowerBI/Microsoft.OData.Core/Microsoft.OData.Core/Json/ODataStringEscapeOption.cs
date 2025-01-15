using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Json
{
	// Token: 0x02000220 RID: 544
	public enum ODataStringEscapeOption
	{
		// Token: 0x04000AB1 RID: 2737
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Ascii is meaningful")]
		EscapeNonAscii,
		// Token: 0x04000AB2 RID: 2738
		EscapeOnlyControls
	}
}
