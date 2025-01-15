using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x02000017 RID: 23
	public interface IODataRequestMessage
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000097 RID: 151
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Users will never have to instantiate these; the rule does not apply.")]
		IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000098 RID: 152
		// (set) Token: 0x06000099 RID: 153
		Uri Url { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600009A RID: 154
		// (set) Token: 0x0600009B RID: 155
		string Method { get; set; }

		// Token: 0x0600009C RID: 156
		string GetHeader(string headerName);

		// Token: 0x0600009D RID: 157
		void SetHeader(string headerName, string headerValue);

		// Token: 0x0600009E RID: 158
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is intentionally a method.")]
		Stream GetStream();
	}
}
