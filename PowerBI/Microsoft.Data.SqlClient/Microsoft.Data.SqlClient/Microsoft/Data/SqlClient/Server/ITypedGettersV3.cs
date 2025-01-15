using System;
using System.Data.SqlTypes;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000118 RID: 280
	internal interface ITypedGettersV3
	{
		// Token: 0x060015F0 RID: 5616
		bool IsDBNull(SmiEventSink sink, int ordinal);

		// Token: 0x060015F1 RID: 5617
		SmiMetaData GetVariantType(SmiEventSink sink, int ordinal);

		// Token: 0x060015F2 RID: 5618
		bool GetBoolean(SmiEventSink sink, int ordinal);

		// Token: 0x060015F3 RID: 5619
		byte GetByte(SmiEventSink sink, int ordinal);

		// Token: 0x060015F4 RID: 5620
		long GetBytesLength(SmiEventSink sink, int ordinal);

		// Token: 0x060015F5 RID: 5621
		int GetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x060015F6 RID: 5622
		long GetCharsLength(SmiEventSink sink, int ordinal);

		// Token: 0x060015F7 RID: 5623
		int GetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x060015F8 RID: 5624
		string GetString(SmiEventSink sink, int ordinal);

		// Token: 0x060015F9 RID: 5625
		short GetInt16(SmiEventSink sink, int ordinal);

		// Token: 0x060015FA RID: 5626
		int GetInt32(SmiEventSink sink, int ordinal);

		// Token: 0x060015FB RID: 5627
		long GetInt64(SmiEventSink sink, int ordinal);

		// Token: 0x060015FC RID: 5628
		float GetSingle(SmiEventSink sink, int ordinal);

		// Token: 0x060015FD RID: 5629
		double GetDouble(SmiEventSink sink, int ordinal);

		// Token: 0x060015FE RID: 5630
		SqlDecimal GetSqlDecimal(SmiEventSink sink, int ordinal);

		// Token: 0x060015FF RID: 5631
		DateTime GetDateTime(SmiEventSink sink, int ordinal);

		// Token: 0x06001600 RID: 5632
		Guid GetGuid(SmiEventSink sink, int ordinal);
	}
}
