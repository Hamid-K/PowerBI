using System;
using System.IO;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x020002EC RID: 748
	internal interface IValueCodec
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060010E9 RID: 4329
		string LoadName { get; }

		// Token: 0x060010EA RID: 4330
		int WriteParameterization(Stream stream);

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060010EB RID: 4331
		ColumnType Type { get; }
	}
}
