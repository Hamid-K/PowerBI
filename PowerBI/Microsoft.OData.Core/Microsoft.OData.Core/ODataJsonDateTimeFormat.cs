using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData
{
	// Token: 0x02000092 RID: 146
	internal enum ODataJsonDateTimeFormat
	{
		// Token: 0x04000236 RID: 566
		ODataDateTime,
		// Token: 0x04000237 RID: 567
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "ISO is a standards body and should be represented as all-uppercase in the API.")]
		ISO8601DateTime
	}
}
