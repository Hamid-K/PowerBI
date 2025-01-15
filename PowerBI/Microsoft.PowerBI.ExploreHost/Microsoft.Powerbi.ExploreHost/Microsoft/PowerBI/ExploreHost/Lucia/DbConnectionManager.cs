using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.InfoNav;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.DataExtension;
using Microsoft.PowerBI.Lucia.Hosting;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000057 RID: 87
	[ImmutableObject(true)]
	internal class DbConnectionManager : IDbConnectionManager
	{
		// Token: 0x06000285 RID: 645 RVA: 0x000085B6 File Offset: 0x000067B6
		internal DbConnectionManager(IDataSourceInfo dataSourceInfo, ConnectionProvider connectionProvider)
		{
			this.m_dataSourceInfo = dataSourceInfo;
			this.m_connectionProvider = connectionProvider;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000085CC File Offset: 0x000067CC
		public IDbConnection GetOpenedConnection()
		{
			return this.m_connectionProvider.CreateOpenedConnectionAsync(this.m_dataSourceInfo).WaitAndUnwrapResult<IDbConnection>();
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000085E4 File Offset: 0x000067E4
		public void ReturnConnection(IDbConnection connection, bool shouldDiscard)
		{
			DbConnectionManager.<>c__DisplayClass4_0 CS$<>8__locals1 = new DbConnectionManager.<>c__DisplayClass4_0();
			CS$<>8__locals1.connection = connection;
			if (CS$<>8__locals1.connection == null)
			{
				return;
			}
			if (!shouldDiscard)
			{
				this.m_connectionProvider.ReleaseConnectionAsync(CS$<>8__locals1.connection, this.m_dataSourceInfo).WaitAndUnwrap();
				return;
			}
			Task.Run(delegate
			{
				DbConnectionManager.<>c__DisplayClass4_0.<<ReturnConnection>b__0>d <<ReturnConnection>b__0>d;
				<<ReturnConnection>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<ReturnConnection>b__0>d.<>4__this = CS$<>8__locals1;
				<<ReturnConnection>b__0>d.<>1__state = -1;
				<<ReturnConnection>b__0>d.<>t__builder.Start<DbConnectionManager.<>c__DisplayClass4_0.<<ReturnConnection>b__0>d>(ref <<ReturnConnection>b__0>d);
				return <<ReturnConnection>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x0400010B RID: 267
		private readonly IDataSourceInfo m_dataSourceInfo;

		// Token: 0x0400010C RID: 268
		private readonly ConnectionProvider m_connectionProvider;
	}
}
