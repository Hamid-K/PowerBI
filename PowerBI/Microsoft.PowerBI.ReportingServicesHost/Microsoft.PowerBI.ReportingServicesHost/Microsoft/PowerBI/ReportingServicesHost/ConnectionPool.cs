using System;
using System.Collections.Generic;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Utils;
using Microsoft.PowerBI.Telemetry;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000032 RID: 50
	internal sealed class ConnectionPool : IDbConnectionPool, IDisposable
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00004134 File Offset: 0x00002334
		internal ConnectionPool()
		{
			this.poolableConnectionsDictionary = new Dictionary<string, LinkedList<IDbPoolableConnection>>();
			this.m_connectionCount = 0;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000414E File Offset: 0x0000234E
		public int ConnectionCount
		{
			get
			{
				return this.m_connectionCount;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004158 File Offset: 0x00002358
		public IDbConnection GetConnection(ConnectionKey connectionKey)
		{
			if (this.IsEmpty() || connectionKey == null)
			{
				return null;
			}
			bool flag = connectionKey.ShouldCheckIsAlive();
			string keyString = connectionKey.GetKeyString();
			bool flag2 = false;
			bool flag3 = false;
			IDbPoolableConnection dbPoolableConnection = null;
			while (!flag2 && !flag3)
			{
				Dictionary<string, LinkedList<IDbPoolableConnection>> dictionary = this.poolableConnectionsDictionary;
				lock (dictionary)
				{
					LinkedList<IDbPoolableConnection> linkedList;
					if (this.poolableConnectionsDictionary.TryGetValue(keyString, out linkedList))
					{
						if (linkedList.Count != 0)
						{
							dbPoolableConnection = linkedList.First.Value;
							flag2 = true;
							linkedList.RemoveFirst();
							this.m_connectionCount--;
						}
						if (linkedList.Count == 0)
						{
							this.poolableConnectionsDictionary.Remove(keyString);
							flag3 = true;
						}
					}
					if (linkedList == null)
					{
						flag3 = true;
					}
				}
				if (flag && dbPoolableConnection != null)
				{
					try
					{
						if (!dbPoolableConnection.IsAlive)
						{
							flag2 = false;
							dbPoolableConnection.Close();
						}
					}
					catch (Exception ex)
					{
						TelemetryService.Instance.TraceError(StringUtil.FormatInvariant("Exception occurred getting the connection from the pool: {0}", ex.ToString().MarkAsPrivate()));
						flag2 = false;
						dbPoolableConnection.Close();
					}
				}
			}
			if (flag2)
			{
				return dbPoolableConnection;
			}
			return null;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004280 File Offset: 0x00002480
		public bool PoolConnection(IDbPoolableConnection connection, ConnectionKey connectionKey)
		{
			Dictionary<string, LinkedList<IDbPoolableConnection>> dictionary = this.poolableConnectionsDictionary;
			lock (dictionary)
			{
				string keyString = connectionKey.GetKeyString();
				LinkedList<IDbPoolableConnection> linkedList;
				if (!this.poolableConnectionsDictionary.TryGetValue(keyString, out linkedList))
				{
					linkedList = new LinkedList<IDbPoolableConnection>();
					this.poolableConnectionsDictionary.Add(keyString, linkedList);
				}
				linkedList.AddLast(connection);
				this.m_connectionCount++;
			}
			return true;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000042FC File Offset: 0x000024FC
		public void CloseConnections()
		{
			Dictionary<string, LinkedList<IDbPoolableConnection>> dictionary = this.poolableConnectionsDictionary;
			lock (dictionary)
			{
				foreach (KeyValuePair<string, LinkedList<IDbPoolableConnection>> keyValuePair in this.poolableConnectionsDictionary)
				{
					foreach (IDbPoolableConnection dbPoolableConnection in keyValuePair.Value)
					{
						dbPoolableConnection.Close();
						dbPoolableConnection.Dispose();
					}
				}
				this.poolableConnectionsDictionary.Clear();
				this.m_connectionCount = 0;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000043CC File Offset: 0x000025CC
		public void Dispose()
		{
			this.CloseConnections();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000043D4 File Offset: 0x000025D4
		private bool IsEmpty()
		{
			return this.m_connectionCount == 0;
		}

		// Token: 0x040000E1 RID: 225
		private readonly Dictionary<string, LinkedList<IDbPoolableConnection>> poolableConnectionsDictionary;

		// Token: 0x040000E2 RID: 226
		private int m_connectionCount;
	}
}
