using System;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F1B RID: 7963
	internal interface IConformingColumn<T> : IColumn
	{
		// Token: 0x06010C31 RID: 68657
		T GetValue(int row);
	}
}
