using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x0200008B RID: 139
	// (Invoke) Token: 0x060006B9 RID: 1721
	[return: Nullable(new byte[] { 2, 0, 1, 1 })]
	internal delegate IEnumerable<KeyValuePair<object, object>> ExtensionDataGetter(object o);
}
