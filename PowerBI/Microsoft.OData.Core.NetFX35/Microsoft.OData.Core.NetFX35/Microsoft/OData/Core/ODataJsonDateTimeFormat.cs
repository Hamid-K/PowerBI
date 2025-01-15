using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Core
{
	// Token: 0x0200017C RID: 380
	internal enum ODataJsonDateTimeFormat
	{
		// Token: 0x04000608 RID: 1544
		ODataDateTime,
		// Token: 0x04000609 RID: 1545
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "ISO is a standards body and should be represented as all-uppercase in the API.")]
		ISO8601DateTime
	}
}
