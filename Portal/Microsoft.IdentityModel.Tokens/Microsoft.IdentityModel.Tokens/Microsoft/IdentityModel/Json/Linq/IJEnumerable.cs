using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.IdentityModel.Json.Linq
{
	// Token: 0x020000BA RID: 186
	internal interface IJEnumerable<out T> : IEnumerable<T>, IEnumerable where T : JToken
	{
		// Token: 0x170001BA RID: 442
		IJEnumerable<JToken> this[object key] { get; }
	}
}
