using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData
{
	// Token: 0x0200006C RID: 108
	internal enum ODataJsonDateTimeFormat
	{
		// Token: 0x040001D5 RID: 469
		ODataDateTime,
		// Token: 0x040001D6 RID: 470
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "ISO is a standards body and should be represented as all-uppercase in the API.")]
		ISO8601DateTime
	}
}
