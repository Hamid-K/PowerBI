using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x020000A4 RID: 164
	public interface IODataResponseMessage
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600061C RID: 1564
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Users will never have to instantiate these; the rule does not apply.")]
		IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600061D RID: 1565
		// (set) Token: 0x0600061E RID: 1566
		int StatusCode { get; set; }

		// Token: 0x0600061F RID: 1567
		string GetHeader(string headerName);

		// Token: 0x06000620 RID: 1568
		void SetHeader(string headerName, string headerValue);

		// Token: 0x06000621 RID: 1569
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is intentionally a method.")]
		Stream GetStream();
	}
}
