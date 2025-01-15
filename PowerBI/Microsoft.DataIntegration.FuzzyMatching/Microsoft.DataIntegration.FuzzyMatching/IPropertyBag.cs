using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000052 RID: 82
	internal interface IPropertyBag
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600031D RID: 797
		IEnumerable<string> Properties { get; }

		// Token: 0x170000A5 RID: 165
		object this[string propertyName] { get; set; }
	}
}
