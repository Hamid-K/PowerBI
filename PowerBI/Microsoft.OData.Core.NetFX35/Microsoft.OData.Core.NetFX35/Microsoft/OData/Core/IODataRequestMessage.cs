using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x020000A3 RID: 163
	public interface IODataRequestMessage
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000614 RID: 1556
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Users will never have to instantiate these; the rule does not apply.")]
		IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000615 RID: 1557
		// (set) Token: 0x06000616 RID: 1558
		Uri Url { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000617 RID: 1559
		// (set) Token: 0x06000618 RID: 1560
		string Method { get; set; }

		// Token: 0x06000619 RID: 1561
		string GetHeader(string headerName);

		// Token: 0x0600061A RID: 1562
		void SetHeader(string headerName, string headerValue);

		// Token: 0x0600061B RID: 1563
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is intentionally a method.")]
		Stream GetStream();
	}
}
