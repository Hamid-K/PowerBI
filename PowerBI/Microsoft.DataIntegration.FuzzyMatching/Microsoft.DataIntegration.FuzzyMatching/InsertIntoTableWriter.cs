using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000081 RID: 129
	[Serializable]
	internal class InsertIntoTableWriter : ISqlTableWriter, IDisposable
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x00017F50 File Offset: 0x00016150
		public void Dispose()
		{
			if (this.m_refRowInsertCommand != null)
			{
				this.m_refRowInsertCommand.Dispose();
				this.m_refRowInsertCommand = null;
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00017F6C File Offset: 0x0001616C
		public void BeginUpdate(SqlConnection connection, SqlName tableName, DataTable schema)
		{
			this.m_refRowInsertCommand = SqlUtils.GenerateSqlInsertCommandFromSchema(schema, tableName, connection);
			this.m_refRowInsertCommand.Prepare();
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00017F88 File Offset: 0x00016188
		public void AddRecord(IDataRecord record)
		{
			for (int i = 0; i < this.m_refRowInsertCommand.Parameters.Count; i++)
			{
				this.m_refRowInsertCommand.Parameters[i].Value = record[i];
			}
			this.m_refRowInsertCommand.ExecuteNonQuery();
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00017FD9 File Offset: 0x000161D9
		public void EndUpdate()
		{
		}

		// Token: 0x040001C7 RID: 455
		[NonSerialized]
		private SqlCommand m_refRowInsertCommand;
	}
}
