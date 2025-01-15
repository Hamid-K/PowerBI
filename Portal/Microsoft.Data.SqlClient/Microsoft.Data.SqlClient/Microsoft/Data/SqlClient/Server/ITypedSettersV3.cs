using System;
using System.Data.SqlTypes;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200011A RID: 282
	internal interface ITypedSettersV3
	{
		// Token: 0x06001625 RID: 5669
		void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData);

		// Token: 0x06001626 RID: 5670
		void SetDBNull(SmiEventSink sink, int ordinal);

		// Token: 0x06001627 RID: 5671
		void SetBoolean(SmiEventSink sink, int ordinal, bool value);

		// Token: 0x06001628 RID: 5672
		void SetByte(SmiEventSink sink, int ordinal, byte value);

		// Token: 0x06001629 RID: 5673
		int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x0600162A RID: 5674
		void SetBytesLength(SmiEventSink sink, int ordinal, long length);

		// Token: 0x0600162B RID: 5675
		int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x0600162C RID: 5676
		void SetCharsLength(SmiEventSink sink, int ordinal, long length);

		// Token: 0x0600162D RID: 5677
		void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length);

		// Token: 0x0600162E RID: 5678
		void SetInt16(SmiEventSink sink, int ordinal, short value);

		// Token: 0x0600162F RID: 5679
		void SetInt32(SmiEventSink sink, int ordinal, int value);

		// Token: 0x06001630 RID: 5680
		void SetInt64(SmiEventSink sink, int ordinal, long value);

		// Token: 0x06001631 RID: 5681
		void SetSingle(SmiEventSink sink, int ordinal, float value);

		// Token: 0x06001632 RID: 5682
		void SetDouble(SmiEventSink sink, int ordinal, double value);

		// Token: 0x06001633 RID: 5683
		void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value);

		// Token: 0x06001634 RID: 5684
		void SetDateTime(SmiEventSink sink, int ordinal, DateTime value);

		// Token: 0x06001635 RID: 5685
		void SetGuid(SmiEventSink sink, int ordinal, Guid value);
	}
}
