using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200007E RID: 126
	[Serializable]
	internal class SqlBulkCopyTableWriter : ISqlTableWriter, IDisposable
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x00017AEE File Offset: 0x00015CEE
		public void Dispose()
		{
			if (this.m_refRowBulkWriter != null)
			{
				this.m_refRowBulkWriter.Close();
				this.m_refRowBulkWriter = null;
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00017B0C File Offset: 0x00015D0C
		public void BeginUpdate(SqlConnection connection, SqlName tableName, DataTable schema)
		{
			this.m_referenceDataReaderForWriter = new RecordQueueDataReader(schema);
			this.m_refRowBulkWriter = new SqlBulkCopy(connection, 4, null);
			this.m_refRowBulkWriter.DestinationTableName = tableName.QualifiedName;
			this.m_refRowBulkWriter.BulkCopyTimeout = 0;
			this.m_refRowBulkWriter.BatchSize = 5000;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00017B60 File Offset: 0x00015D60
		public void AddRecord(IDataRecord record)
		{
			this.m_referenceDataReaderForWriter.Add(record);
			if (this.m_referenceDataReaderForWriter.Count > 5000)
			{
				this.m_refRowBulkWriter.WriteToServer(this.m_referenceDataReaderForWriter);
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00017B91 File Offset: 0x00015D91
		public void EndUpdate()
		{
			if (this.m_referenceDataReaderForWriter.Count > 0)
			{
				this.m_refRowBulkWriter.WriteToServer(this.m_referenceDataReaderForWriter);
			}
		}

		// Token: 0x040001BA RID: 442
		public const int BatchSize = 5000;

		// Token: 0x040001BB RID: 443
		private RecordQueueDataReader m_referenceDataReaderForWriter;

		// Token: 0x040001BC RID: 444
		[NonSerialized]
		private SqlBulkCopy m_refRowBulkWriter;
	}
}
