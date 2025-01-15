using System;
using System.Data.SqlTypes;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000119 RID: 281
	internal interface ITypedSetters
	{
		// Token: 0x06001601 RID: 5633
		void SetDBNull(int ordinal);

		// Token: 0x06001602 RID: 5634
		void SetBoolean(int ordinal, bool value);

		// Token: 0x06001603 RID: 5635
		void SetByte(int ordinal, byte value);

		// Token: 0x06001604 RID: 5636
		void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06001605 RID: 5637
		void SetChar(int ordinal, char value);

		// Token: 0x06001606 RID: 5638
		void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06001607 RID: 5639
		void SetInt16(int ordinal, short value);

		// Token: 0x06001608 RID: 5640
		void SetInt32(int ordinal, int value);

		// Token: 0x06001609 RID: 5641
		void SetInt64(int ordinal, long value);

		// Token: 0x0600160A RID: 5642
		void SetFloat(int ordinal, float value);

		// Token: 0x0600160B RID: 5643
		void SetDouble(int ordinal, double value);

		// Token: 0x0600160C RID: 5644
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetString(int ordinal, string value);

		// Token: 0x0600160D RID: 5645
		void SetString(int ordinal, string value, int offset);

		// Token: 0x0600160E RID: 5646
		void SetDecimal(int ordinal, decimal value);

		// Token: 0x0600160F RID: 5647
		void SetDateTime(int ordinal, DateTime value);

		// Token: 0x06001610 RID: 5648
		void SetGuid(int ordinal, Guid value);

		// Token: 0x06001611 RID: 5649
		void SetSqlBoolean(int ordinal, SqlBoolean value);

		// Token: 0x06001612 RID: 5650
		void SetSqlByte(int ordinal, SqlByte value);

		// Token: 0x06001613 RID: 5651
		void SetSqlInt16(int ordinal, SqlInt16 value);

		// Token: 0x06001614 RID: 5652
		void SetSqlInt32(int ordinal, SqlInt32 value);

		// Token: 0x06001615 RID: 5653
		void SetSqlInt64(int ordinal, SqlInt64 value);

		// Token: 0x06001616 RID: 5654
		void SetSqlSingle(int ordinal, SqlSingle value);

		// Token: 0x06001617 RID: 5655
		void SetSqlDouble(int ordinal, SqlDouble value);

		// Token: 0x06001618 RID: 5656
		void SetSqlMoney(int ordinal, SqlMoney value);

		// Token: 0x06001619 RID: 5657
		void SetSqlDateTime(int ordinal, SqlDateTime value);

		// Token: 0x0600161A RID: 5658
		void SetSqlDecimal(int ordinal, SqlDecimal value);

		// Token: 0x0600161B RID: 5659
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlString(int ordinal, SqlString value);

		// Token: 0x0600161C RID: 5660
		void SetSqlString(int ordinal, SqlString value, int offset);

		// Token: 0x0600161D RID: 5661
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlBinary(int ordinal, SqlBinary value);

		// Token: 0x0600161E RID: 5662
		void SetSqlBinary(int ordinal, SqlBinary value, int offset);

		// Token: 0x0600161F RID: 5663
		void SetSqlGuid(int ordinal, SqlGuid value);

		// Token: 0x06001620 RID: 5664
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlChars(int ordinal, SqlChars value);

		// Token: 0x06001621 RID: 5665
		void SetSqlChars(int ordinal, SqlChars value, int offset);

		// Token: 0x06001622 RID: 5666
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlBytes(int ordinal, SqlBytes value);

		// Token: 0x06001623 RID: 5667
		void SetSqlBytes(int ordinal, SqlBytes value, int offset);

		// Token: 0x06001624 RID: 5668
		void SetSqlXml(int ordinal, SqlXml value);
	}
}
