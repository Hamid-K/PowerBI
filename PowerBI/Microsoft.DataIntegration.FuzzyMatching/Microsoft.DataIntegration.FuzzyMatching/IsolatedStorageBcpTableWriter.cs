using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.IsolatedStorage;
using System.Security.Principal;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;
using Microsoft.SqlServer.Server;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000080 RID: 128
	internal class IsolatedStorageBcpTableWriter : ISqlTableWriter, IDisposable
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x00017D1C File Offset: 0x00015F1C
		public void Dispose()
		{
			using (WindowsImpersonationContext windowsImpersonationContext = (SqlContext.IsAvailable ? SqlContext.WindowsIdentity.Impersonate() : null))
			{
				if (this.m_fileStream != null)
				{
					this.m_fileStream.Dispose();
					this.m_fileStream = null;
				}
				try
				{
					File.Delete(this.m_filePath);
				}
				catch (IOException)
				{
				}
				catch (UnauthorizedAccessException)
				{
				}
				this.m_connection = null;
				this.m_writer = null;
				this.m_tableName = null;
				if (windowsImpersonationContext != null)
				{
					windowsImpersonationContext.Undo();
				}
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00017DC0 File Offset: 0x00015FC0
		public void BeginUpdate(SqlConnection connection, SqlName tableName, DataTable schema)
		{
			this.m_connection = connection;
			this.m_tableName = tableName;
			using (WindowsImpersonationContext windowsImpersonationContext = (SqlContext.IsAvailable ? SqlContext.WindowsIdentity.Impersonate() : null))
			{
				IsolatedStorageFile userStoreForAssembly = IsolatedStorageFile.GetUserStoreForAssembly();
				string text = userStoreForAssembly.GetType().GetField("m_RootDir", 36).GetValue(userStoreForAssembly)
					.ToString();
				this.m_filePath = text + Utilities.GenerateUniqueName("FL3_", 250) + ".tmp";
				this.m_fileStream = File.Create(this.m_filePath, 10485760);
				File.SetAttributes(this.m_filePath, File.GetAttributes(this.m_filePath) | 256);
				this.m_writer = new BulkInsertStreamWriter(this.m_fileStream, schema);
				if (windowsImpersonationContext != null)
				{
					windowsImpersonationContext.Undo();
				}
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00017EA0 File Offset: 0x000160A0
		public void AddRecord(IDataRecord record)
		{
			this.m_writer.Write(record);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00017EB0 File Offset: 0x000160B0
		public void EndUpdate()
		{
			this.m_writer.Flush();
			this.m_fileStream.Flush();
			this.m_fileStream.Close();
			using (SqlCommand sqlCommand = this.m_connection.CreateCommand())
			{
				sqlCommand.CommandTimeout = this.m_connection.ConnectionTimeout;
				sqlCommand.CommandText = string.Format("BULK INSERT {0} FROM '{1}' WITH (DATAFILETYPE='widenative')", this.m_tableName.QualifiedName, this.m_filePath);
				sqlCommand.ExecuteNonQuery();
			}
			this.Dispose();
		}

		// Token: 0x040001C2 RID: 450
		private string m_filePath;

		// Token: 0x040001C3 RID: 451
		private FileStream m_fileStream;

		// Token: 0x040001C4 RID: 452
		private BulkInsertStreamWriter m_writer;

		// Token: 0x040001C5 RID: 453
		private SqlConnection m_connection;

		// Token: 0x040001C6 RID: 454
		private SqlName m_tableName;
	}
}
