using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000D5 RID: 213
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	public interface IColumn
	{
		// Token: 0x060003E1 RID: 993
		bool IsNull(int row);

		// Token: 0x060003E2 RID: 994
		object GetObject(int row);

		// Token: 0x060003E3 RID: 995
		bool GetBoolean(int row);

		// Token: 0x060003E4 RID: 996
		byte GetByte(int row);

		// Token: 0x060003E5 RID: 997
		short GetInt16(int row);

		// Token: 0x060003E6 RID: 998
		int GetInt32(int row);

		// Token: 0x060003E7 RID: 999
		long GetInt64(int row);

		// Token: 0x060003E8 RID: 1000
		float GetFloat(int row);

		// Token: 0x060003E9 RID: 1001
		Guid GetGuid(int row);

		// Token: 0x060003EA RID: 1002
		double GetDouble(int row);

		// Token: 0x060003EB RID: 1003
		decimal GetDecimal(int row);

		// Token: 0x060003EC RID: 1004
		DateTime GetDateTime(int row);

		// Token: 0x060003ED RID: 1005
		string GetString(int row);

		// Token: 0x060003EE RID: 1006
		unsafe DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength);
	}
}
