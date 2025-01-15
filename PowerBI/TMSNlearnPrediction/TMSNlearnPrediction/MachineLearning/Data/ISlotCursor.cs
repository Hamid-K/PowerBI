using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000D6 RID: 214
	public interface ISlotCursor : ICursor, ICounted, IDisposable
	{
		// Token: 0x06000480 RID: 1152
		VectorType GetSlotType();

		// Token: 0x06000481 RID: 1153
		ValueGetter<VBuffer<TValue>> GetGetter<TValue>();
	}
}
