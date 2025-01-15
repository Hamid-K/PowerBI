using System;

namespace Microsoft.ProgramSynthesis.Utils.Interactive
{
	// Token: 0x0200069C RID: 1692
	internal interface IRefCountList<T>
	{
		// Token: 0x0600245E RID: 9310
		void Clear();

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x0600245F RID: 9311
		int Count { get; }

		// Token: 0x17000628 RID: 1576
		T this[int i] { get; }

		// Token: 0x06002461 RID: 9313
		void Add(T item);

		// Token: 0x06002462 RID: 9314
		void Done(int index);
	}
}
