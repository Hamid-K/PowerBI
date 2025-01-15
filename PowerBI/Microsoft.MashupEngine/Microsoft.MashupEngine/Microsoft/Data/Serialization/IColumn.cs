using System;

namespace Microsoft.Data.Serialization
{
	// Token: 0x0200014D RID: 333
	public interface IColumn
	{
		// Token: 0x060005D7 RID: 1495
		bool IsNull(int row);

		// Token: 0x060005D8 RID: 1496
		object GetObject(int row);

		// Token: 0x060005D9 RID: 1497
		bool GetBoolean(int row);

		// Token: 0x060005DA RID: 1498
		byte GetByte(int row);

		// Token: 0x060005DB RID: 1499
		short GetInt16(int row);

		// Token: 0x060005DC RID: 1500
		int GetInt32(int row);

		// Token: 0x060005DD RID: 1501
		long GetInt64(int row);

		// Token: 0x060005DE RID: 1502
		float GetFloat(int row);

		// Token: 0x060005DF RID: 1503
		Guid GetGuid(int row);

		// Token: 0x060005E0 RID: 1504
		double GetDouble(int row);

		// Token: 0x060005E1 RID: 1505
		decimal GetDecimal(int row);

		// Token: 0x060005E2 RID: 1506
		DateTime GetDateTime(int row);

		// Token: 0x060005E3 RID: 1507
		string GetString(int row);
	}
}
