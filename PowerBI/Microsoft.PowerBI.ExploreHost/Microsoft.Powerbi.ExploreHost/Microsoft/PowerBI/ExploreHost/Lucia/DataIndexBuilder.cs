using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Lucia.Core;
using Microsoft.Lucia.Core.TermIndex;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.DataExtension;
using Microsoft.PowerBI.ExploreHost.Utils;
using Microsoft.PowerBI.Lucia.Hosting;
using Microsoft.ReportingServices.Common;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200004C RID: 76
	internal class DataIndexBuilder : IDataIndexBuilder
	{
		// Token: 0x06000258 RID: 600 RVA: 0x00007B26 File Offset: 0x00005D26
		internal DataIndexBuilder(Lazy<INaturalLanguageServicesFactory> serviceFactory, string workingDirectoryRoot, IDataSourceInfo dataSourceInfo, ConnectionProvider connectionProvider, Version dataIndexVersion, LuciaSessionOptions luciaSessionOptions)
		{
			this.m_serviceFactory = serviceFactory;
			this.m_workingDirectoryRoot = workingDirectoryRoot;
			this.m_dataSourceInfo = dataSourceInfo;
			this.m_connectionProvider = connectionProvider;
			this.m_dataIndexVersion = dataIndexVersion;
			this.m_luciaSessionOptions = luciaSessionOptions;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00007B5C File Offset: 0x00005D5C
		public async Task<DataIndex> BuildIndexAsync(IDatabaseContext context, CancellationToken token)
		{
			IDbConnection connection = null;
			DataIndexBuilderTelemetry telemetry = new DataIndexBuilderTelemetry(this.m_luciaSessionOptions);
			object obj = null;
			int num = 0;
			DataIndex dataIndex;
			try
			{
				try
				{
					IDbConnection dbConnection = await this.m_connectionProvider.CreateOpenedConnectionAsync(this.m_dataSourceInfo);
					connection = dbConnection;
					DataInstanceProvider dataInstanceProvider = new DataInstanceProvider(connection, ExploreTracer.Instance, null, null, false);
					BuildDataIndexResult buildDataIndexResult = this.m_serviceFactory.Value.CreateManagementService(null, LinguisticSchemaServicesBuilderOptions.None).BuildDataIndex(context, dataInstanceProvider, LuciaUtils.CreateNewLuciaDataIndexWorkingDirectory(this.m_workingDirectoryRoot), token.WithTag(DataIndexBuilderCancelOption.Default), this.m_dataIndexVersion, null);
					if (buildDataIndexResult.Warnings != BuildDataIndexWarnings.None && buildDataIndexResult.Warnings != BuildDataIndexWarnings.IndexSizeLimitReached)
					{
						dataIndex = null;
					}
					else
					{
						telemetry.Statistics = buildDataIndexResult.Statistics;
						telemetry.Warnings = buildDataIndexResult.Warnings;
						dataIndex = buildDataIndexResult.DataIndex;
					}
				}
				catch (Exception ex)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex))
					{
						throw;
					}
					telemetry.AddExceptionDetails("BuildIndexAsync", ex);
					dataIndex = null;
				}
				num = 1;
			}
			catch (object obj)
			{
			}
			ExploreHostUtils.TraceDataIndexBuilderTelemetry(telemetry);
			if (connection != null)
			{
				await this.m_connectionProvider.ReleaseConnectionAsync(connection, this.m_dataSourceInfo);
			}
			object obj2 = obj;
			if (obj2 != null)
			{
				Exception ex2 = obj2 as Exception;
				if (ex2 == null)
				{
					throw obj2;
				}
				ExceptionDispatchInfo.Capture(ex2).Throw();
			}
			DataIndex dataIndex2;
			if (num == 1)
			{
				dataIndex2 = dataIndex;
			}
			else
			{
				obj = null;
				dataIndex = null;
				connection = null;
				telemetry = null;
			}
			return dataIndex2;
		}

		// Token: 0x040000EA RID: 234
		private readonly Lazy<INaturalLanguageServicesFactory> m_serviceFactory;

		// Token: 0x040000EB RID: 235
		private readonly string m_workingDirectoryRoot;

		// Token: 0x040000EC RID: 236
		private readonly IDataSourceInfo m_dataSourceInfo;

		// Token: 0x040000ED RID: 237
		private readonly ConnectionProvider m_connectionProvider;

		// Token: 0x040000EE RID: 238
		private readonly Version m_dataIndexVersion;

		// Token: 0x040000EF RID: 239
		private readonly LuciaSessionOptions m_luciaSessionOptions;
	}
}
