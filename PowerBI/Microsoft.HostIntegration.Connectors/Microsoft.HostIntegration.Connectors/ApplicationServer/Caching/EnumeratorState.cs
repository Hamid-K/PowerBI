using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001FF RID: 511
	[DataContract]
	internal abstract class EnumeratorState
	{
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060010A0 RID: 4256
		internal abstract EnumeratorStateType EnumeratorStateType { get; }
	}
}
