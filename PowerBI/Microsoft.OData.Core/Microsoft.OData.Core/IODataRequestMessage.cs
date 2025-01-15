using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x0200003E RID: 62
	public interface IODataRequestMessage
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600021E RID: 542
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Users will never have to instantiate these; the rule does not apply.")]
		IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600021F RID: 543
		// (set) Token: 0x06000220 RID: 544
		Uri Url { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000221 RID: 545
		// (set) Token: 0x06000222 RID: 546
		string Method { get; set; }

		// Token: 0x06000223 RID: 547
		string GetHeader(string headerName);

		// Token: 0x06000224 RID: 548
		void SetHeader(string headerName, string headerValue);

		// Token: 0x06000225 RID: 549
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is intentionally a method.")]
		Stream GetStream();
	}
}
