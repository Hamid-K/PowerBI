using System;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x02000018 RID: 24
	public interface IDataReader : IDisposable
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005A RID: 90
		int ColumnCount { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005B RID: 91
		bool IsOpen { get; }

		// Token: 0x0600005C RID: 92
		string GetColumnName(int index);

		// Token: 0x0600005D RID: 93
		Type GetColumnType(int index);

		// Token: 0x0600005E RID: 94
		int GetOrdinal(string name);

		// Token: 0x0600005F RID: 95
		object GetValue(int index);

		// Token: 0x06000060 RID: 96
		void GetValues(object[] values);

		// Token: 0x06000061 RID: 97
		bool MoveNext();

		// Token: 0x06000062 RID: 98
		bool NextResult();

		// Token: 0x06000063 RID: 99
		void Close();
	}
}
