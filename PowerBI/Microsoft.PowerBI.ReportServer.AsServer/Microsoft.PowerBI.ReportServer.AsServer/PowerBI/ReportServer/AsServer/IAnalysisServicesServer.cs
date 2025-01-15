using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.PowerBI.ReportServer.AsServer.Artifacts;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x0200001E RID: 30
	public interface IAnalysisServicesServer
	{
		// Token: 0x0600009D RID: 157
		Task<LoadDatabaseResult> LoadDatabaseAsync(string databaseName, Lazy<Stream> dataModel, string requestId, string clientSessionId);

		// Token: 0x0600009E RID: 158
		Task<LoadDatabaseResult> LoadDatabaseForRefreshAsync(string databaseName, Stream dataModel, string requestId, string clientSessionId);

		// Token: 0x0600009F RID: 159
		Task<LoadDatabaseResult> LoadDatabaseForExecutionAsync(IPrincipal currentUser, string databaseName, Lazy<Stream> dataModel, string requestId, string clientSessionId, IEnumerable<PbixDataSource> dataSources, IEnumerable<PbixModelParameter> parameters);

		// Token: 0x060000A0 RID: 160
		IEnumerable<PbixDataSource> GetDataSources(string databaseName);

		// Token: 0x060000A1 RID: 161
		IEnumerable<PbixModelRole> GetModelRoles(string databaseName);

		// Token: 0x060000A2 RID: 162
		IEnumerable<PbixModelParameter> GetModelParameters(string databaseName);

		// Token: 0x060000A3 RID: 163
		string GetModelVersionName(string databaseName);

		// Token: 0x060000A4 RID: 164
		IEnumerable<PbixDataSource> SetModelParameters(string databaseName, IEnumerable<PbixModelParameter> parameters);

		// Token: 0x060000A5 RID: 165
		string GetConnectionString(long databaseId, Guid catalogId);

		// Token: 0x060000A6 RID: 166
		string BuildDatabaseName(Guid catalogId, DateTime lastModified);

		// Token: 0x060000A7 RID: 167
		string GetMostRecentDatabaseName(Guid catalogId, DateTime lastModified, out bool wasInMemory);

		// Token: 0x060000A8 RID: 168
		bool IsDatabaseDirectQuery(string databaseName);

		// Token: 0x060000A9 RID: 169
		void DeleteDatabase(long databaseId);

		// Token: 0x060000AA RID: 170
		void RefreshDatabase(string databaseName, IEnumerable<PbixDataSource> dataSources, IEnumerable<PbixModelParameter> parameters, string clientSessionId);

		// Token: 0x060000AB RID: 171
		bool IsDatabaseLoaded(string databaseName);

		// Token: 0x060000AC RID: 172
		void RemoveCredentials(string databaseName);

		// Token: 0x060000AD RID: 173
		void SaveDatabaseToStream(string databaseName, Stream targetDbStream);

		// Token: 0x060000AE RID: 174
		string ResolveDatabaseName(long databaseId);

		// Token: 0x060000AF RID: 175
		DateTime? GetDatabaseLastProcessed(string databaseName);
	}
}
