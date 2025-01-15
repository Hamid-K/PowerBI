using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000D5 RID: 213
	public interface ISqlObjectManager : IObjectManager
	{
		// Token: 0x06000876 RID: 2166
		bool Contains(string objectName);

		// Token: 0x06000877 RID: 2167
		IEnumerable<KeyValuePair<string, ObjectReference>> Objects();

		// Token: 0x06000878 RID: 2168
		void Commit(string objectName, object obj, SqlXml xmlDefinition, bool overwriteExisting);

		// Token: 0x06000879 RID: 2169
		void Commit(int objectHandle);

		// Token: 0x0600087A RID: 2170
		void Commit(int objectHandle, SqlXml xmlDefinition);

		// Token: 0x0600087B RID: 2171
		void Commit(int objectHandle, SqlXml xmlDefinition, ConnectionManager connectionManager, string connectionName);

		// Token: 0x0600087C RID: 2172
		void Commit(int objectHandle, SqlXml xmlDefinition, ConnectionManager connectionManager, string connectionName, bool useTempDb);

		// Token: 0x0600087D RID: 2173
		void CommitToTempDb(int objectHandle);

		// Token: 0x0600087E RID: 2174
		int Rollback(string objectName);

		// Token: 0x0600087F RID: 2175
		void Drop(string objectName);

		// Token: 0x06000880 RID: 2176
		void Drop(string objectName, ConnectionManager connectionManager, bool dropTables, bool useTempDb);

		// Token: 0x06000881 RID: 2177
		void Drop(string objectName, ConnectionManager connectionManager);

		// Token: 0x06000882 RID: 2178
		void DropAll();
	}
}
