using System;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010D4 RID: 4308
	internal interface IPool : IDisposable
	{
		// Token: 0x060070D7 RID: 28887
		bool TryGet(string key, out IPoolable poolable);

		// Token: 0x060070D8 RID: 28888
		void Add(IPoolable poolable);

		// Token: 0x060070D9 RID: 28889
		void Purge();

		// Token: 0x060070DA RID: 28890
		void Clear();
	}
}
