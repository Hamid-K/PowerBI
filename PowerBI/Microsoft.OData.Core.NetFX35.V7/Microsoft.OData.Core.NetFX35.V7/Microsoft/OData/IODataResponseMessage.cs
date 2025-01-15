using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x02000018 RID: 24
	public interface IODataResponseMessage
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009F RID: 159
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Users will never have to instantiate these; the rule does not apply.")]
		IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A0 RID: 160
		// (set) Token: 0x060000A1 RID: 161
		int StatusCode { get; set; }

		// Token: 0x060000A2 RID: 162
		string GetHeader(string headerName);

		// Token: 0x060000A3 RID: 163
		void SetHeader(string headerName, string headerValue);

		// Token: 0x060000A4 RID: 164
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is intentionally a method.")]
		Stream GetStream();
	}
}
