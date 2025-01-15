using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020020D0 RID: 8400
	[NullableContext(1)]
	internal interface IHashKeyCollection<[Nullable(2)] in TKey>
	{
		// Token: 0x17002F50 RID: 12112
		// (get) Token: 0x06011A09 RID: 72201
		IEqualityComparer<TKey> KeyComparer { get; }
	}
}
