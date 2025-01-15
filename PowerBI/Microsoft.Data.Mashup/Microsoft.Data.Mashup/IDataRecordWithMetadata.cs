using System;
using System.Data;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000044 RID: 68
	public interface IDataRecordWithMetadata : IDataRecord
	{
		// Token: 0x0600035A RID: 858
		IDataRecordWithMetadata GetMetadata(int ordinal);
	}
}
