using System;
using System.Data;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Modeling.ModelGeneration.MSSQL;
using Microsoft.ReportingServices.SemanticQueryEngine;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration.MSSQLADW
{
	// Token: 0x0200000B RID: 11
	[CLSCompliant(false)]
	public sealed class MsSqlAdwDsvStatisticsProvider : MsSqlDsvStatisticsProvider
	{
		// Token: 0x06000089 RID: 137 RVA: 0x000044BC File Offset: 0x000026BC
		public MsSqlAdwDsvStatisticsProvider(IDbConnection connection)
			: base(connection)
		{
			if (MsSqlAdwDsvStatisticsProvider.m_ConnectionType == null)
			{
				object staticLock = MsSqlAdwDsvStatisticsProvider.StaticLock;
				lock (staticLock)
				{
					MsSqlAdwDsvStatisticsProvider.m_ConnectionType = Microsoft.ReportingServices.Common.DataExtensionsHelper.GetDataExtensionConnectionType("SqlDwConnectionWrapper", "GetDwConnectionType");
				}
			}
			if (connection.GetType() != MsSqlAdwDsvStatisticsProvider.m_ConnectionType)
			{
				throw SQEAssert.AssertFalseAndThrow("Connection must be SqlDwConnection.", Array.Empty<object>());
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004540 File Offset: 0x00002740
		internal static DsvCompareInfo GetCompareInfoStatic(IDbConnection connection)
		{
			return MsSqlAdwDsvGenerator.GetCompareInfoStatic(connection);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004548 File Offset: 0x00002748
		protected override DsvCompareInfo GetCompareInfo()
		{
			return MsSqlAdwDsvStatisticsProvider.GetCompareInfoStatic(base.Connection);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004555 File Offset: 0x00002755
		protected override bool IsSql
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003FB8 File Offset: 0x000021B8
		protected override void SetNoLock()
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004558 File Offset: 0x00002758
		protected override SqlDsvStatisticsProvider.IGetColumnDbTypeName GetColumnDbTypeNameDictionary()
		{
			SqlDsvStatisticsProvider.IGetColumnDbTypeName getColumnDbTypeName;
			try
			{
				getColumnDbTypeName = new MsSqlAdwDsvGenerator(base.Connection).GenerateColumnsExtendedInfo(base.DataSourceView.CompareInfo);
			}
			catch
			{
				getColumnDbTypeName = null;
			}
			return getColumnDbTypeName;
		}

		// Token: 0x04000048 RID: 72
		private static Type m_ConnectionType = null;

		// Token: 0x04000049 RID: 73
		private static object StaticLock = new object();
	}
}
