using System;
using System.Globalization;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000F1 RID: 241
	internal sealed class ServerInfo
	{
		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x0004C302 File Offset: 0x0004A502
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x0004C30A File Offset: 0x0004A50A
		internal string ExtendedServerName { get; private set; }

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x0004C313 File Offset: 0x0004A513
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x0004C31B File Offset: 0x0004A51B
		internal string ResolvedServerName { get; private set; }

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x0004C324 File Offset: 0x0004A524
		// (set) Token: 0x060012C7 RID: 4807 RVA: 0x0004C32C File Offset: 0x0004A52C
		internal string ResolvedDatabaseName { get; private set; }

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x0004C335 File Offset: 0x0004A535
		// (set) Token: 0x060012C9 RID: 4809 RVA: 0x0004C33D File Offset: 0x0004A53D
		internal string UserProtocol { get; private set; }

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x0004C346 File Offset: 0x0004A546
		// (set) Token: 0x060012CB RID: 4811 RVA: 0x0004C34E File Offset: 0x0004A54E
		internal string ServerSPN { get; private set; }

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x0004C357 File Offset: 0x0004A557
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x0004C35F File Offset: 0x0004A55F
		internal string UserServerName
		{
			get
			{
				return this.m_userServerName;
			}
			private set
			{
				this.m_userServerName = value;
			}
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0004C368 File Offset: 0x0004A568
		internal ServerInfo(SqlConnectionString userOptions)
			: this(userOptions, userOptions.DataSource, userOptions.ServerSPN)
		{
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0004C37D File Offset: 0x0004A57D
		internal ServerInfo(SqlConnectionString userOptions, string serverName, string serverSPN)
			: this(userOptions, serverName)
		{
			this.ServerSPN = serverSPN;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0004C38E File Offset: 0x0004A58E
		private ServerInfo(SqlConnectionString userOptions, string serverName)
		{
			this.UserServerName = serverName ?? string.Empty;
			this.UserProtocol = userOptions.NetworkLibrary;
			this.ResolvedDatabaseName = userOptions.InitialCatalog;
			this.PreRoutingServerName = null;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0004C3C8 File Offset: 0x0004A5C8
		internal ServerInfo(SqlConnectionString userOptions, RoutingInfo routing, string preRoutingServerName, string serverSPN)
		{
			if (routing == null || routing.ServerName == null)
			{
				this.UserServerName = string.Empty;
			}
			else
			{
				this.UserServerName = string.Format(CultureInfo.InvariantCulture, "{0},{1}", routing.ServerName, routing.Port);
			}
			this.PreRoutingServerName = preRoutingServerName;
			this.UserProtocol = "tcp";
			this.SetDerivedNames(this.UserProtocol, this.UserServerName);
			this.ResolvedDatabaseName = userOptions.InitialCatalog;
			this.ServerSPN = serverSPN;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0004C451 File Offset: 0x0004A651
		internal void SetDerivedNames(string protocol, string serverName)
		{
			if (!ADP.IsEmpty(protocol))
			{
				this.ExtendedServerName = protocol + ":" + serverName;
			}
			else
			{
				this.ExtendedServerName = serverName;
			}
			this.ResolvedServerName = serverName;
		}

		// Token: 0x040007D2 RID: 2002
		private string m_userServerName;

		// Token: 0x040007D3 RID: 2003
		internal readonly string PreRoutingServerName;
	}
}
