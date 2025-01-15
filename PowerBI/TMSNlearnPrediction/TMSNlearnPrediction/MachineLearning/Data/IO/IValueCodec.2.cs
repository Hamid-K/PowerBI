using System;
using System.IO;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x020002ED RID: 749
	internal interface IValueCodec<T> : IValueCodec
	{
		// Token: 0x060010EC RID: 4332
		IValueWriter<T> OpenWriter(Stream stream);

		// Token: 0x060010ED RID: 4333
		IValueReader<T> OpenReader(Stream stream, int items);
	}
}
