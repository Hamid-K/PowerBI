using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;

namespace System.Data.Entity.Core
{
	// Token: 0x020002D7 RID: 727
	public interface IExtendedDataRecord : IDataRecord
	{
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x0600231B RID: 8987
		DataRecordInfo DataRecordInfo { get; }

		// Token: 0x0600231C RID: 8988
		DbDataRecord GetDataRecord(int i);

		// Token: 0x0600231D RID: 8989
		DbDataReader GetDataReader(int i);
	}
}
