using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004AC RID: 1196
	public interface IServiceConfiguration
	{
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060024AC RID: 9388
		string ServiceName { get; }

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060024AD RID: 9389
		IEnumerable<Type> KnownTypes { get; }

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060024AE RID: 9390
		IEnumerable<Type> KnownExceptions { get; }
	}
}
