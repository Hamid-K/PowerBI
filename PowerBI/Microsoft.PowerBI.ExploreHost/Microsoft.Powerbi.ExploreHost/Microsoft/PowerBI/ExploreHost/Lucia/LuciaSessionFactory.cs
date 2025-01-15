using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.BusinessIntelligence;
using Microsoft.Lucia.Core;
using Microsoft.Lucia.Core.Runtime;
using Microsoft.Lucia.Diagnostics;
using Microsoft.Lucia.Languages.English;
using Microsoft.PowerBI.Data.ModelSchemaAnalysis;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.Lucia.Hosting;
using Microsoft.PowerBI.ReportingServicesHost;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200006B RID: 107
	internal sealed class LuciaSessionFactory : ILuciaSessionFactory, IDisposable
	{
		// Token: 0x060002FA RID: 762 RVA: 0x00009C6D File Offset: 0x00007E6D
		static LuciaSessionFactory()
		{
			SchemaValidationLicense.RegisterPowerBILicenseForNewtonsoftJsonSchema();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00009C74 File Offset: 0x00007E74
		internal LuciaSessionFactory(IStreamBasedStorage indexStreamStorage, string rootPath = null)
		{
			this.m_naturalLanguageServiceFactory = new Lazy<INaturalLanguageServicesFactory>(() => LuciaRuntime.Create(new LuciaRuntimeSettings(new EnglishLanguageProvider[]
			{
				new EnglishLanguageProvider(null, null, null)
			}, ExploreTracer.Instance.AsTracingProvider())).LegacyFactory);
			this.m_indexStreamStorage = indexStreamStorage;
			this.m_rootPath = rootPath;
			this.m_dataIndexDisposer = new DataIndexDisposer();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00009CCC File Offset: 0x00007ECC
		public ILuciaSession CreateLuciaSession(IConnectionFactory connectionFactory, IConnectionPool connectionPool, IDataSourceInfo dataSourceInfo, Func<string> getConceptualSchemaXml, Func<string, IReadOnlyDictionary<string, object>, DataSet> getSchemaDataset, LuciaSessionParameters luciaSessionParameters, FeatureSwitches featureSwitches)
		{
			Lazy<INaturalLanguageServicesFactory> naturalLanguageServiceFactory = this.m_naturalLanguageServiceFactory;
			Func<string> getLinguisticSchemaJson = luciaSessionParameters.GetLinguisticSchemaJson;
			Func<string, Task<string>> getDaxTemplate = luciaSessionParameters.GetDaxTemplate;
			IBulkMeasureExpressionProvider measureExpressionProvider = luciaSessionParameters.MeasureExpressionProvider;
			string luciaWorkingDirectory = LuciaSessionFactory.GetLuciaWorkingDirectory(this.m_rootPath);
			IStreamBasedStorage indexStreamStorage = this.m_indexStreamStorage;
			return new LuciaSession(naturalLanguageServiceFactory, connectionFactory, connectionPool, dataSourceInfo, getConceptualSchemaXml, getSchemaDataset, getLinguisticSchemaJson, getDaxTemplate, measureExpressionProvider, luciaWorkingDirectory, this.m_dataIndexDisposer, indexStreamStorage, luciaSessionParameters.Options, featureSwitches);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00009D22 File Offset: 0x00007F22
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00009D2B File Offset: 0x00007F2B
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!this.m_naturalLanguageServiceFactory.IsValueCreated)
				{
					return;
				}
				this.m_naturalLanguageServiceFactory.Value.Dispose();
				this.m_dataIndexDisposer.WaitForCompletion();
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00009D59 File Offset: 0x00007F59
		private static string GetLuciaWorkingDirectory(string localAppDataPath)
		{
			return Path.Combine(localAppDataPath, "LuciaIndex");
		}

		// Token: 0x0400015E RID: 350
		private const string LuciaIndexFolderName = "LuciaIndex";

		// Token: 0x0400015F RID: 351
		private readonly Lazy<INaturalLanguageServicesFactory> m_naturalLanguageServiceFactory;

		// Token: 0x04000160 RID: 352
		private readonly IStreamBasedStorage m_indexStreamStorage;

		// Token: 0x04000161 RID: 353
		private readonly string m_rootPath;

		// Token: 0x04000162 RID: 354
		private readonly IDataIndexDisposer m_dataIndexDisposer;
	}
}
