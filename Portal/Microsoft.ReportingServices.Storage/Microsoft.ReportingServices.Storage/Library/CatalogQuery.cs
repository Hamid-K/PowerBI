using System;
using System.Collections;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Extensions;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200000A RID: 10
	internal class CatalogQuery : ICatalogQuery
	{
		// Token: 0x06000069 RID: 105 RVA: 0x000042AF File Offset: 0x000024AF
		public void ExecuteNonQuery(string query, Hashtable parameters, CommandType type)
		{
			this.ExecuteQuery(query, parameters, type, false);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000042BC File Offset: 0x000024BC
		public IDataReader ExecuteReader(string query, Hashtable parameters, CommandType type)
		{
			return this.ExecuteQuery(query, parameters, type, true);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000042C8 File Offset: 0x000024C8
		private IDataReader ExecuteQuery(string query, Hashtable parameters, CommandType type, bool getReader)
		{
			IDataReader dataReader2;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand(query)))
				{
					instrumentedSqlCommand.Connection = this.Connection;
					instrumentedSqlCommand.Transaction = this.Transaction;
					instrumentedSqlCommand.CommandType = type;
					instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCommandTimeoutSeconds;
					if (parameters != null)
					{
						foreach (object obj in parameters.Keys)
						{
							string text = (string)obj;
							object obj2 = parameters[text];
							if (obj2 is SqlParameter)
							{
								instrumentedSqlCommand.Parameters.Add((SqlParameter)obj2);
							}
							else
							{
								instrumentedSqlCommand.Parameters.AddWithValue(text, obj2);
							}
						}
					}
					IDataReader dataReader = null;
					if (getReader)
					{
						dataReader = instrumentedSqlCommand.ExecuteReader();
					}
					else
					{
						instrumentedSqlCommand.ExecuteNonQuery();
					}
					dataReader2 = dataReader;
				}
			}
			catch (Exception)
			{
				this.AbortTransaction();
				throw;
			}
			return dataReader2;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000043D4 File Offset: 0x000025D4
		internal void AbortTransaction()
		{
			if (this.AllowCommit && this.m_connManager != null)
			{
				this.m_connManager.AbortTransaction();
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000043F1 File Offset: 0x000025F1
		private SqlConnection Connection
		{
			get
			{
				return this.ConnectionManager.Connection;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000043FE File Offset: 0x000025FE
		internal ConnectionManager ConnectionManager
		{
			get
			{
				if (this.m_connManager == null)
				{
					this.m_connManager = new ConnectionManager();
					if (this.m_willClose)
					{
						this.m_connManager.WillDisconnectStorage();
					}
				}
				return this.m_connManager;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000442C File Offset: 0x0000262C
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00004434 File Offset: 0x00002634
		internal bool AllowCommit
		{
			get
			{
				return this.m_allowCommit;
			}
			set
			{
				this.m_allowCommit = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000443D File Offset: 0x0000263D
		private SqlTransaction Transaction
		{
			get
			{
				if (this.m_connManager.Transaction == null)
				{
					this.m_connManager.BeginTransaction();
				}
				return this.m_connManager.Transaction;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004462 File Offset: 0x00002662
		public void Commit()
		{
			if (this.AllowCommit && this.m_connManager != null)
			{
				this.m_connManager.CommitTransaction();
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000447F File Offset: 0x0000267F
		public void WillClose()
		{
			this.m_willClose = true;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004488 File Offset: 0x00002688
		public void Close()
		{
			if (this.m_connManager != null)
			{
				this.m_connManager.DisconnectStorage();
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000044A0 File Offset: 0x000026A0
		public void Run(CatalogQuery.Code mainAction, CatalogQuery.Code errorAction, bool completeTransaction)
		{
			try
			{
				if (mainAction != null)
				{
					mainAction();
				}
				if (completeTransaction)
				{
					this.Commit();
				}
			}
			catch
			{
				try
				{
					if (completeTransaction)
					{
						this.AbortTransaction();
					}
					if (errorAction != null)
					{
						errorAction();
					}
				}
				catch
				{
				}
				throw;
			}
			finally
			{
				if (completeTransaction)
				{
					this.Close();
				}
			}
		}

		// Token: 0x04000079 RID: 121
		internal ConnectionManager m_connManager;

		// Token: 0x0400007A RID: 122
		private bool m_allowCommit = true;

		// Token: 0x0400007B RID: 123
		private bool m_willClose;

		// Token: 0x02000049 RID: 73
		// (Invoke) Token: 0x06000280 RID: 640
		public delegate void Code();
	}
}
