using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.Data.Sql
{
	// Token: 0x0200015A RID: 346
	public sealed class SqlDataSourceEnumerator : DbDataSourceEnumerator
	{
		// Token: 0x06001A52 RID: 6738 RVA: 0x0006BB98 File Offset: 0x00069D98
		private SqlDataSourceEnumerator()
		{
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x0006BBA0 File Offset: 0x00069DA0
		public static SqlDataSourceEnumerator Instance
		{
			get
			{
				return SqlDataSourceEnumerator.s_singletonInstance.Value;
			}
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x0006BBAC File Offset: 0x00069DAC
		public override DataTable GetDataSources()
		{
			return this.GetDataSourcesInternal();
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x0006BBB4 File Offset: 0x00069DB4
		private DataTable GetDataSourcesInternal()
		{
			return SqlDataSourceEnumeratorNativeHelper.GetDataSources();
		}

		// Token: 0x04000AA4 RID: 2724
		private static readonly Lazy<SqlDataSourceEnumerator> s_singletonInstance = new Lazy<SqlDataSourceEnumerator>(() => new SqlDataSourceEnumerator());
	}
}
