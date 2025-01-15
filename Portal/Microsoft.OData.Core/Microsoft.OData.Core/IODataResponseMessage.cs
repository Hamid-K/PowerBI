using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x02000040 RID: 64
	public interface IODataResponseMessage
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000227 RID: 551
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Users will never have to instantiate these; the rule does not apply.")]
		IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000228 RID: 552
		// (set) Token: 0x06000229 RID: 553
		int StatusCode { get; set; }

		// Token: 0x0600022A RID: 554
		string GetHeader(string headerName);

		// Token: 0x0600022B RID: 555
		void SetHeader(string headerName, string headerValue);

		// Token: 0x0600022C RID: 556
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is intentionally a method.")]
		Stream GetStream();
	}
}
