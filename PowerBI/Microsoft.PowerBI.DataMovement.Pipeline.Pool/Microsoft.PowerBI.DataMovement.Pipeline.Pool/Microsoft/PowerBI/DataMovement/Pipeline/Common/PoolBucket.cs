using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x02000007 RID: 7
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class PoolBucket<[global::System.Runtime.CompilerServices.Nullable(2)] TObjectKey, [global::System.Runtime.CompilerServices.Nullable(0)] TObject> where TObject : IDisposable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020B3 File Offset: 0x000002B3
		internal ConcurrentQueue<PoolObject<TObjectKey, TObject>> Queue { get; } = new ConcurrentQueue<PoolObject<TObjectKey, TObject>>();
	}
}
