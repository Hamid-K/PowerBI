using System;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000117 RID: 279
	internal interface ITypedGetters
	{
		// Token: 0x060015CD RID: 5581
		bool IsDBNull(int ordinal);

		// Token: 0x060015CE RID: 5582
		SqlDbType GetVariantType(int ordinal);

		// Token: 0x060015CF RID: 5583
		bool GetBoolean(int ordinal);

		// Token: 0x060015D0 RID: 5584
		byte GetByte(int ordinal);

		// Token: 0x060015D1 RID: 5585
		long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x060015D2 RID: 5586
		char GetChar(int ordinal);

		// Token: 0x060015D3 RID: 5587
		long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x060015D4 RID: 5588
		short GetInt16(int ordinal);

		// Token: 0x060015D5 RID: 5589
		int GetInt32(int ordinal);

		// Token: 0x060015D6 RID: 5590
		long GetInt64(int ordinal);

		// Token: 0x060015D7 RID: 5591
		float GetFloat(int ordinal);

		// Token: 0x060015D8 RID: 5592
		double GetDouble(int ordinal);

		// Token: 0x060015D9 RID: 5593
		string GetString(int ordinal);

		// Token: 0x060015DA RID: 5594
		decimal GetDecimal(int ordinal);

		// Token: 0x060015DB RID: 5595
		DateTime GetDateTime(int ordinal);

		// Token: 0x060015DC RID: 5596
		Guid GetGuid(int ordinal);

		// Token: 0x060015DD RID: 5597
		SqlBoolean GetSqlBoolean(int ordinal);

		// Token: 0x060015DE RID: 5598
		SqlByte GetSqlByte(int ordinal);

		// Token: 0x060015DF RID: 5599
		SqlInt16 GetSqlInt16(int ordinal);

		// Token: 0x060015E0 RID: 5600
		SqlInt32 GetSqlInt32(int ordinal);

		// Token: 0x060015E1 RID: 5601
		SqlInt64 GetSqlInt64(int ordinal);

		// Token: 0x060015E2 RID: 5602
		SqlSingle GetSqlSingle(int ordinal);

		// Token: 0x060015E3 RID: 5603
		SqlDouble GetSqlDouble(int ordinal);

		// Token: 0x060015E4 RID: 5604
		SqlMoney GetSqlMoney(int ordinal);

		// Token: 0x060015E5 RID: 5605
		SqlDateTime GetSqlDateTime(int ordinal);

		// Token: 0x060015E6 RID: 5606
		SqlDecimal GetSqlDecimal(int ordinal);

		// Token: 0x060015E7 RID: 5607
		SqlString GetSqlString(int ordinal);

		// Token: 0x060015E8 RID: 5608
		SqlBinary GetSqlBinary(int ordinal);

		// Token: 0x060015E9 RID: 5609
		SqlGuid GetSqlGuid(int ordinal);

		// Token: 0x060015EA RID: 5610
		SqlChars GetSqlChars(int ordinal);

		// Token: 0x060015EB RID: 5611
		SqlBytes GetSqlBytes(int ordinal);

		// Token: 0x060015EC RID: 5612
		SqlXml GetSqlXml(int ordinal);

		// Token: 0x060015ED RID: 5613
		SqlBytes GetSqlBytesRef(int ordinal);

		// Token: 0x060015EE RID: 5614
		SqlChars GetSqlCharsRef(int ordinal);

		// Token: 0x060015EF RID: 5615
		SqlXml GetSqlXmlRef(int ordinal);
	}
}
