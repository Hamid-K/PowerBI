using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;
using Microsoft.SqlServer.Server;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200005B RID: 91
	[Serializable]
	public class ConnectionManager : IConnectionManager, IDisposable, ISerializable
	{
		// Token: 0x06000381 RID: 897 RVA: 0x000113F9 File Offset: 0x0000F5F9
		public ConnectionManager()
		{
			this.m_connections = new ConnectionCollection();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00011417 File Offset: 0x0000F617
		public ConnectionManager(string defaultConnectionString)
			: this()
		{
			this.m_connections.Add(new Connection
			{
				Name = "default",
				ConnectionString = defaultConnectionString
			});
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00011441 File Offset: 0x0000F641
		public ConnectionManager(ConnectionCollection connections)
		{
			this.m_connections = connections;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001145B File Offset: 0x0000F65B
		private ConnectionManager(SerializationInfo info, StreamingContext context)
		{
			this.m_openConnections = new Dictionary<string, SqlConnection>();
			this.m_connections = (ConnectionCollection)info.GetValue("m_connections", typeof(ConnectionCollection));
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00011499 File Offset: 0x0000F699
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_connections", this.m_connections);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000114AC File Offset: 0x0000F6AC
		public bool IsContextConnection(string connectionName)
		{
			if (string.IsNullOrEmpty(connectionName))
			{
				return SqlContext.IsAvailable;
			}
			return ConnectionManager.ContextConnectionName.Equals(connectionName) || (this.m_connections != null && SqlUtils.IsContextConnectionString(this.m_connections.GetConnection(connectionName).ConnectionString));
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000114EC File Offset: 0x0000F6EC
		public void Dispose()
		{
			foreach (SqlConnection sqlConnection in this.m_openConnections.Values)
			{
				sqlConnection.Close();
				sqlConnection.Dispose();
			}
			this.m_openConnections.Clear();
			Thread.Sleep(0);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00011558 File Offset: 0x0000F758
		public SqlConnection GetConnection(string connectionName)
		{
			if (string.IsNullOrEmpty(connectionName))
			{
				if (SqlContext.IsAvailable)
				{
					connectionName = ConnectionManager.ContextConnectionName;
				}
				else
				{
					if (this.m_connections.Count <= 0)
					{
						throw new ArgumentException("The connectionName may only be null if there is a suitable default connection available.");
					}
					connectionName = this.m_connections[0].Name;
				}
			}
			bool flag = this.IsContextConnection(connectionName);
			if (flag)
			{
				connectionName = ConnectionManager.ContextConnectionName;
			}
			SqlConnection sqlConnection;
			if (!this.m_openConnections.TryGetValue(connectionName, ref sqlConnection))
			{
				if (flag)
				{
					sqlConnection = new SqlConnection(ConnectionManager.ContextConnectionString);
				}
				else
				{
					sqlConnection = new SqlConnection(this.m_connections.GetConnection(connectionName).ConnectionString);
				}
				sqlConnection.Open();
				this.m_openConnections.Add(connectionName, sqlConnection);
			}
			return sqlConnection;
		}

		// Token: 0x04000130 RID: 304
		public static readonly string ContextConnectionName = "ContextConnection";

		// Token: 0x04000131 RID: 305
		public static readonly string ContextConnectionString = "context connection = true";

		// Token: 0x04000132 RID: 306
		private ConnectionCollection m_connections;

		// Token: 0x04000133 RID: 307
		[NonSerialized]
		private Dictionary<string, SqlConnection> m_openConnections = new Dictionary<string, SqlConnection>();
	}
}
