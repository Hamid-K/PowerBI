using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Identity.Json.Linq
{
	// Token: 0x020000B9 RID: 185
	internal interface IJEnumerable<out T> : IEnumerable<T>, IEnumerable where T : JToken
	{
		// Token: 0x170001BA RID: 442
		IJEnumerable<JToken> this[object key] { get; }
	}
}
