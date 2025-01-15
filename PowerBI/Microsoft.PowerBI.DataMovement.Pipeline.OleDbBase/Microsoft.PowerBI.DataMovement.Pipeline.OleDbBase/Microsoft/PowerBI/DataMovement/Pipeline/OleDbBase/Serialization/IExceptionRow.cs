using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000D6 RID: 214
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	public interface IExceptionRow
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003EF RID: 1007
		IDictionary<int, IDictionary<string, string>> Exceptions { get; }
	}
}
