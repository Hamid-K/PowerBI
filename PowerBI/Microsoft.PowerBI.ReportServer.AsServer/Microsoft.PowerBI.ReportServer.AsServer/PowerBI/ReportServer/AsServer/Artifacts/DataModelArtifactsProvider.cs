using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;

namespace Microsoft.PowerBI.ReportServer.AsServer.Artifacts
{
	// Token: 0x02000032 RID: 50
	public class DataModelArtifactsProvider : IDataModelArtifactsProvider
	{
		// Token: 0x06000113 RID: 275 RVA: 0x000055F6 File Offset: 0x000037F6
		public DataModelArtifactsProvider(IAnalysisServicesServer analysisServicesServerServer)
		{
			if (analysisServicesServerServer == null)
			{
				throw new ArgumentException("analysisServicesServerServer cannot be null.");
			}
			this._asServer = analysisServicesServerServer;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005614 File Offset: 0x00003814
		public async Task<DataModelArtifacts> RetrieveArtifactsAsync(Stream dataModel, string requestId, string sessionId)
		{
			DataModelArtifacts dataModelArtifacts;
			using (ScopeMeter.Use(new string[] { "EmbeddedDataSource", "get" }))
			{
				List<PbixDataSource> list = new List<PbixDataSource>();
				List<PbixModelRole> list2 = new List<PbixModelRole>();
				List<PbixModelParameter> list3 = new List<PbixModelParameter>();
				string text = string.Empty;
				string databaseName = "temp_" + Guid.NewGuid();
				Logger.Verbose("Loading database {0} to AS", new object[] { databaseName });
				Lazy<Stream> lazy = new Lazy<Stream>(() => dataModel);
				LoadDatabaseResult loadDatabaseResult = await this._asServer.LoadDatabaseAsync(databaseName, lazy, requestId, sessionId);
				try
				{
					list = this._asServer.GetDataSources(databaseName).ToList<PbixDataSource>();
					list2 = this._asServer.GetModelRoles(databaseName).ToList<PbixModelRole>();
					list3 = this._asServer.GetModelParameters(databaseName).ToList<PbixModelParameter>();
					text = this._asServer.GetModelVersionName(databaseName);
				}
				catch (Exception ex)
				{
					Logger.Error(ex, "Failed to parse the embedded model data sources", Array.Empty<object>());
					throw ex;
				}
				finally
				{
					this._asServer.DeleteDatabase(loadDatabaseResult.DatabaseId);
				}
				dataModelArtifacts = new DataModelArtifacts
				{
					ModelVersion = text,
					EmbeddedDataSources = list,
					DataModelRoles = list2,
					DataModelParameters = list3
				};
			}
			return dataModelArtifacts;
		}

		// Token: 0x0400007E RID: 126
		private readonly IAnalysisServicesServer _asServer;

		// Token: 0x0200004F RID: 79
		internal sealed class PbixDataSourceModelComparer : EqualityComparer<PbixDataSource>
		{
			// Token: 0x06000195 RID: 405 RVA: 0x00006B17 File Offset: 0x00004D17
			public override bool Equals(PbixDataSource x, PbixDataSource y)
			{
				return y != null && x != null && string.Equals(x.ConnectionString, y.ConnectionString, StringComparison.InvariantCultureIgnoreCase) && x.Kind == y.Kind;
			}

			// Token: 0x06000196 RID: 406 RVA: 0x00006B43 File Offset: 0x00004D43
			public override int GetHashCode(PbixDataSource obj)
			{
				if (obj != null && obj.ConnectionString != null)
				{
					return obj.ConnectionString.ToLower().GetHashCode();
				}
				return 0;
			}
		}
	}
}
