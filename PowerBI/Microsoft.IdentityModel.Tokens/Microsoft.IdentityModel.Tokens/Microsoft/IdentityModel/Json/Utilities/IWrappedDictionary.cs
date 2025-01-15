using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Utilities
{
	// Token: 0x0200004F RID: 79
	internal interface IWrappedDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060004AF RID: 1199
		[Nullable(1)]
		object UnderlyingDictionary
		{
			[NullableContext(1)]
			get;
		}
	}
}
