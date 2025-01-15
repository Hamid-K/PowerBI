using System;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x020002EA RID: 746
	internal interface IValueReader<T> : IDisposable
	{
		// Token: 0x060010E0 RID: 4320
		void MoveNext();

		// Token: 0x060010E1 RID: 4321
		void Get(ref T value);

		// Token: 0x060010E2 RID: 4322
		void Read(T[] values, int index, int count);
	}
}
