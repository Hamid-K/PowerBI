using System;
using System.Collections;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x0200004E RID: 78
	internal interface IWrappedDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060004A7 RID: 1191
		object UnderlyingDictionary { get; }
	}
}
