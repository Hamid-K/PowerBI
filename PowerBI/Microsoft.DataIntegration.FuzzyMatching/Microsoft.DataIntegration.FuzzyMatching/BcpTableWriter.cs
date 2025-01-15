using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200007F RID: 127
	internal class BcpTableWriter : ISqlTableWriter, IDisposable
	{
		// Token: 0x0600051E RID: 1310 RVA: 0x00017BBC File Offset: 0x00015DBC
		public void Dispose()
		{
			if (this.m_fileStream != null)
			{
				this.m_fileStream.Dispose();
				this.m_fileStream = null;
			}
			if (this.m_temporaryFile != null)
			{
				this.m_temporaryFile.Dispose();
				this.m_temporaryFile = null;
			}
			this.m_connection = null;
			this.m_writer = null;
			this.m_tableName = null;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00017C14 File Offset: 0x00015E14
		public void BeginUpdate(SqlConnection connection, SqlName tableName, DataTable schema)
		{
			this.m_connection = connection;
			this.m_tableName = tableName;
			this.m_temporaryFile = new TemporaryFile(true);
			this.m_fileStream = File.Create(this.m_temporaryFile.Path, 10485760);
			this.m_writer = new BulkInsertStreamWriter(this.m_fileStream, schema);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00017C68 File Offset: 0x00015E68
		public void AddRecord(IDataRecord record)
		{
			this.m_writer.Write(record);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00017C78 File Offset: 0x00015E78
		public void EndUpdate()
		{
			this.m_writer.Flush();
			this.m_fileStream.Flush();
			this.m_fileStream.Close();
			using (SqlCommand sqlCommand = this.m_connection.CreateCommand())
			{
				sqlCommand.CommandTimeout = this.m_connection.ConnectionTimeout;
				sqlCommand.CommandText = string.Format("BULK INSERT {0} FROM '{1}' WITH (DATAFILETYPE='widenative')", this.m_tableName.QualifiedName, this.m_temporaryFile.Path);
				sqlCommand.ExecuteNonQuery();
			}
			this.Dispose();
		}

		// Token: 0x040001BD RID: 445
		private TemporaryFile m_temporaryFile;

		// Token: 0x040001BE RID: 446
		private FileStream m_fileStream;

		// Token: 0x040001BF RID: 447
		private BulkInsertStreamWriter m_writer;

		// Token: 0x040001C0 RID: 448
		private SqlConnection m_connection;

		// Token: 0x040001C1 RID: 449
		private SqlName m_tableName;
	}
}
