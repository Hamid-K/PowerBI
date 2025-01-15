using System;
using System.Collections;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x02000045 RID: 69
	internal interface IWrappedCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600044A RID: 1098
		object UnderlyingCollection { get; }
	}
}
