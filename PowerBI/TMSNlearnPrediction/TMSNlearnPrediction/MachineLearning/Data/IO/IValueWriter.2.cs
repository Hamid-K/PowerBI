using System;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x020002E8 RID: 744
	internal interface IValueWriter<T> : IValueWriter, IDisposable
	{
		// Token: 0x060010D7 RID: 4311
		void Write(ref T value);

		// Token: 0x060010D8 RID: 4312
		void Write(T[] values, int index, int count);
	}
}
