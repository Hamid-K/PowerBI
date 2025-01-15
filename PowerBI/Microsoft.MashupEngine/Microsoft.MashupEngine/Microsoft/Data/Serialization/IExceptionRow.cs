using System;
using System.Collections.Generic;

namespace Microsoft.Data.Serialization
{
	// Token: 0x02000150 RID: 336
	public interface IExceptionRow
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060005E8 RID: 1512
		IDictionary<int, ISerializedException> Exceptions { get; }
	}
}
