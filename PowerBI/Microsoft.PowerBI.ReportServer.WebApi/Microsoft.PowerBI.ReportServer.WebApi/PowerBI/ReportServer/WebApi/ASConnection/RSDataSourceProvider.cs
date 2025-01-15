using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.Request;
using Microsoft.BIServer.Telemetry.Services;
using Microsoft.PowerBI.ReportServer.AsServer;
using Microsoft.PowerBI.ReportServer.AsServer.Artifacts;
using Microsoft.PowerBI.ReportServer.ExploreHost;
using Microsoft.PowerBI.ReportServer.WebApi.Catalog;
using Microsoft.PowerBI.ReportServer.WebApi.Logon;
using Microsoft.ReportingServices.CatalogAccess;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;
using Microsoft.ReportingServices.Portal.ODataClient.V2;

namespace Microsoft.PowerBI.ReportServer.WebApi.ASConnection
{
	// Token: 0x02000045 RID: 69
	public sealed class RSDataSourceProvider : IRSDataSourceProvider
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00007A28 File Offset: 0x00005C28
		public RSDataSourceProvider(IPrincipal userPrincipal, Guid catalogItemId, RequestContext requestContext, ICatalogService catalogService, ICatalogDataModelDataSourceAccessor dataModelDataSourceAccessor, ICatalogDataModelRoleAccessor dataModelRoleAccessor, IUserService userService, IAnalysisServicesServer asServer, bool enableRls)
		{
			this._userPrincipal = userPrincipal;
			this._catalogItemId = catalogItemId;
			this._requestContext = requestContext;
			this._catalogService = catalogService;
			this._dataModelDataSourceAccessor = dataModelDataSourceAccessor;
			this._dataModelRoleAccessor = dataModelRoleAccessor;
			this._userService = userService;
			this._asServer = asServer;
			this._enableRls = enableRls;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007A80 File Offset: 0x00005C80
		private RSDataSourceConnection.ConnectionCredential GetDataSourceConnectionCredential(DataSource dataSource)
		{
			RSConnectionSecurity connectionSecurityForAsConnection = this.GetConnectionSecurityForAsConnection(dataSource);
			if (connectionSecurityForAsConnection == RSConnectionSecurity.Integrated)
			{
				return new RSDataSourceConnection.ConnectionCredential(null, null, false, connectionSecurityForAsConnection);
			}
			DataModelDataSource dataModelDataSource = dataSource.DataModelDataSource;
			if (dataModelDataSource == null)
			{
				throw new Exception("Integrated DataSource connection without credentials.");
			}
			return new RSDataSourceConnection.ConnectionCredential(dataModelDataSource.Username, dataModelDataSource.Secret.ToSecureString(), dataModelDataSource.AuthType == DataModelDataSourceAuthType.Windows || dataModelDataSource.AuthType == DataModelDataSourceAuthType.Impersonate, connectionSecurityForAsConnection);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007AE4 File Offset: 0x00005CE4
		private IIdentity GetIdentityForEmbeddedConnection(List<DataSource> dataSources)
		{
			bool flag = dataSources.Any((DataSource p) => p.DataModelDataSource.Type == DataModelDataSourceType.DirectQuery);
			bool flag2 = dataSources.Any((DataSource p) => p.DataModelDataSource.AuthType == DataModelDataSourceAuthType.Integrated);
			if (flag && flag2 && this._userService.IsWindowsIdentity(this._userPrincipal.Identity))
			{
				return this._userPrincipal.Identity;
			}
			return this._userService.GetCurrentWindowsIdentity();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007B70 File Offset: 0x00005D70
		private string GetActAsUserForEmbeddedConnection(List<DataSource> dataSources)
		{
			bool flag = dataSources.Any((DataSource p) => p.DataModelDataSource.AuthType == DataModelDataSourceAuthType.Integrated);
			Logger.Verbose(string.Format("GetActAsUserForEmbeddedConnection, isIntegrated={0}", flag), Array.Empty<object>());
			if (flag)
			{
				return null;
			}
			if (this._userService.IsWindowsIdentity(this._userPrincipal.Identity))
			{
				try
				{
					string userPrincipalName = this._userService.GetUserPrincipalName(this._userPrincipal.Identity);
					if (!string.IsNullOrEmpty(userPrincipalName))
					{
						return userPrincipalName;
					}
					return this._userPrincipal.Identity.Name;
				}
				catch (Exception ex)
				{
					Logger.Verbose("Exception occured when calling GetUserPrincipalName for {0}: {1}", new object[]
					{
						this._userPrincipal.Identity.Name,
						ex.Message
					});
					return this._userPrincipal.Identity.Name;
				}
			}
			return this._userPrincipal.Identity.Name;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007C74 File Offset: 0x00005E74
		private IIdentity GetIdentityForExternalASConnection(RSDataSourceConnection.ConnectionCredential credential)
		{
			IIdentity identity = this._userPrincipal.Identity;
			if (!credential.IsIntegratedCredential && credential.UseAsWindowsCredentials)
			{
				try
				{
					return this._userService.Logon(new UserCredentials(credential.UserName, credential.Password));
				}
				catch (Exception ex)
				{
					throw new CatalogAccessException(string.Format("Cannot logon user {0}", credential.UserName), CatalogAccessExceptionErrorCode.StoredCredentialsIncorrect, ex);
				}
			}
			if (credential.IsIntegratedCredential && !this._userService.IsWindowsIdentity(identity))
			{
				throw new CatalogAccessException(string.Format("Cannot use integrated security for user {0}, only windows credentials can be used as integrated", credential.UserName), CatalogAccessExceptionErrorCode.UnsupportedCredentialsType);
			}
			return identity;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007D14 File Offset: 0x00005F14
		internal RSConnectionSecurity GetConnectionSecurityForAsConnection(DataSource dataSource)
		{
			switch (dataSource.DataModelDataSource.AuthType)
			{
			case DataModelDataSourceAuthType.Integrated:
				return RSConnectionSecurity.Integrated;
			case DataModelDataSourceAuthType.Windows:
			case DataModelDataSourceAuthType.UsernamePassword:
			case DataModelDataSourceAuthType.Impersonate:
				return RSConnectionSecurity.StoredCredentials;
			}
			throw new Exception(string.Format("Invalid credentials retrival option {0} for PBIX datasource. Only integrated or stored credentials are supported", dataSource.CredentialRetrieval));
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007D6C File Offset: 0x00005F6C
		internal void ValidateConnection(DataSource datasourceInformation, ASConnectionStringBuilder builder)
		{
			if (datasourceInformation.DataModelDataSource.Type == DataModelDataSourceType.Live && datasourceInformation.DataModelDataSource.Kind == DataModelDataSourceKind.AnalysisServices && datasourceInformation.DataModelDataSource.AuthType != DataModelDataSourceAuthType.Windows && datasourceInformation.DataModelDataSource.AuthType != DataModelDataSourceAuthType.Integrated && datasourceInformation.DataModelDataSource.AuthType != DataModelDataSourceAuthType.Impersonate && !builder.IsASAzure)
			{
				throw new CatalogAccessException("Invalid auth type for PBIX datasource.", CatalogAccessExceptionErrorCode.UnsupportedCredentialsType);
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007DD4 File Offset: 0x00005FD4
		public async Task<RSDataSourceConnection> GetDataSourceAsync(long modelId)
		{
			if (this._rsDataSourceConnection == null)
			{
				Stopwatch sw = Stopwatch.StartNew();
				Logger.Verbose("GetDataSourceAsync for catalogItem={0}", new object[] { this._catalogItemId });
				await this._catalogService.GetPbixReportMetadataAsync(this._userPrincipal, this._catalogItemId);
				List<DataSource> list = (await this._dataModelDataSourceAccessor.GetDataModelDataSourcesByItemAsync(this._catalogItemId)).Select((DataModelDataSourceEntity ds) => ds.ToOData()).ToList<DataSource>();
				Logger.Verbose("GetDataSourceAsync completed for catalogItem={0}, ElapsedMs={1} ms", new object[] { this._catalogItemId, sw.ElapsedMilliseconds });
				DataSource dataSource = this.GetASLiveConection(list);
				string text = null;
				string text2 = null;
				RSDataSourceConnection.ConnectionCredential connectionCredential;
				IIdentity identity;
				if (dataSource == null)
				{
					Logger.Verbose(string.Format("GetDataSourceAsync for catalogItem={0} is for Embedded model...", this._catalogItemId), Array.Empty<object>());
					connectionCredential = new RSDataSourceConnection.ConnectionCredential(null, null, false, RSConnectionSecurity.Integrated);
					dataSource = this.BuildDataSourceForEmbedded(list, this._asServer.GetConnectionString(modelId, this._catalogItemId));
					identity = this.GetIdentityForEmbeddedConnection(list);
					if (this._enableRls)
					{
						text = this.GetRLSRoles(this._catalogItemId, list);
						text2 = this.GetActAsUserForEmbeddedConnection(list);
					}
				}
				else
				{
					Logger.Verbose(string.Format("GetDataSourceAsync for catalogItem={0} is for LiveAs model...", this._catalogItemId), Array.Empty<object>());
					connectionCredential = this.GetDataSourceConnectionCredential(dataSource);
					identity = this.GetIdentityForExternalASConnection(connectionCredential);
					TelemetryService.Current.TrackEvent("RS.PBI.DataSource", new Dictionary<string, string> { 
					{
						"type",
						(identity == null) ? "serviceaccount" : "storedcredentials"
					} }, null);
				}
				ASConnectionStringBuilder asconnectionStringBuilder;
				try
				{
					asconnectionStringBuilder = new ASConnectionStringBuilder(dataSource.ConnectionString);
				}
				catch (Exception ex)
				{
					throw new CatalogAccessException("Stored connection string is incorrect.", CatalogAccessExceptionErrorCode.StoredConnectionStringIncorrect, ex);
				}
				this.ValidateConnection(dataSource, asconnectionStringBuilder);
				string effectiveUserName = this.GetEffectiveUserName(dataSource, asconnectionStringBuilder.IsASAzure);
				string text3 = ((!string.IsNullOrEmpty(asconnectionStringBuilder.CustomData)) ? this.ReplaceUserId(asconnectionStringBuilder.CustomData, this._userPrincipal.Identity) : null);
				this._rsDataSourceConnection = new RSDataSourceConnection(this._requestContext.ClientSessionID, asconnectionStringBuilder.DataSource, asconnectionStringBuilder.InitialCatalog, asconnectionStringBuilder.CubeName, 0, effectiveUserName, identity, text3, connectionCredential, text, text2);
				Logger.Verbose(string.Format("GetDataSourceAsync connection for catalogItem={0}, clientSessionId={1}, servername={2}, effectiveUsername={3}, ActAsUser={4}, IdentityToImpersonate={5}, RlsEnabled={6}, RlsRoles={7}", new object[]
				{
					this._catalogItemId,
					this._requestContext.ClientSessionID,
					this._rsDataSourceConnection.ServerName,
					this._rsDataSourceConnection.EffectiveUserName,
					this._rsDataSourceConnection.ActAsUser,
					(identity != null) ? identity.Name : null,
					this._enableRls,
					text
				}), Array.Empty<object>());
				sw = null;
			}
			return this._rsDataSourceConnection;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007E21 File Offset: 0x00006021
		private DataSource GetASLiveConection(IEnumerable<DataSource> dataSources)
		{
			if (dataSources != null && dataSources.Count<DataSource>() == 1 && dataSources.First<DataSource>().DataModelDataSource.Type == DataModelDataSourceType.Live && dataSources.First<DataSource>().DataModelDataSource.Kind == DataModelDataSourceKind.AnalysisServices)
			{
				return dataSources.First<DataSource>();
			}
			return null;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007E60 File Offset: 0x00006060
		private DataSource BuildDataSourceForEmbedded(IEnumerable<DataSource> dataSources, string connectionString)
		{
			bool flag = dataSources.Any((DataSource p) => p.DataModelDataSource.Type == DataModelDataSourceType.DirectQuery);
			bool flag2 = dataSources.Any((DataSource p) => p.DataModelDataSource.AuthType == DataModelDataSourceAuthType.Integrated);
			if (flag && flag2 && !this._userService.IsWindowsIdentity(this._userPrincipal.Identity))
			{
				throw new CatalogAccessException(string.Format("Cannot use integrated security for user {0}, only windows credentials can be used as integrated", this._userPrincipal.Identity.Name), CatalogAccessExceptionErrorCode.UnsupportedCredentialsType);
			}
			return new DataSource
			{
				DataSourceType = RSDataSourceProvider.PbiEmbeddedModelType,
				ConnectionString = connectionString,
				DataModelDataSource = new DataModelDataSource
				{
					AuthType = DataModelDataSourceAuthType.Integrated
				}
			};
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007F20 File Offset: 0x00006120
		private string GetEffectiveUserName(DataSource datasourceInformation, bool isAsAzure)
		{
			string text = string.Empty;
			if (datasourceInformation.DataModelDataSource != null && datasourceInformation.DataModelDataSource.AuthType == DataModelDataSourceAuthType.Impersonate)
			{
				if (isAsAzure && this._userService.IsWindowsIdentity(this._userPrincipal.Identity))
				{
					text = this._userService.GetUserPrincipalName(this._userPrincipal.Identity);
				}
				else
				{
					text = this._userPrincipal.Identity.Name;
				}
			}
			return text;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007F8F File Offset: 0x0000618F
		private bool MatchesByNameNonWindows(string roleAssigment)
		{
			return !this._userService.IsWindowsIdentity(this._userPrincipal.Identity) && this._userPrincipal.Identity.Name.Equals(roleAssigment, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007FC4 File Offset: 0x000061C4
		private string GetRLSRoles(Guid catalogId, List<DataSource> dataSources)
		{
			bool flag = dataSources.Any((DataSource p) => p.DataModelDataSource.Type == DataModelDataSourceType.DirectQuery);
			bool flag2 = dataSources.Any((DataSource p) => p.DataModelDataSource.AuthType == DataModelDataSourceAuthType.Integrated);
			if (flag && flag2)
			{
				return null;
			}
			bool flag3;
			if (!this._asServer.GetModelRoles(this._asServer.GetMostRecentDatabaseName(catalogId, DateTime.MinValue, out flag3)).Any<PbixModelRole>())
			{
				return "RSReaderRole";
			}
			List<string> list = this._dataModelRoleAccessor.GetDataModelRoleAssignmentsByItemAsync(this._catalogItemId).Result.Where((DataModelRoleAssignmentEntity ra) => this.MatchesByNameNonWindows(ra.UserName) || this._userPrincipal.IsInRole(ra.UserName)).SelectMany((DataModelRoleAssignmentEntity ra) => ra.DataModelRoles.Select((DataModelRoleEntity r) => r.ModelRoleName)).Distinct(StringComparer.InvariantCultureIgnoreCase)
				.ToList<string>();
			if (list.Count == 0)
			{
				throw new RlsNotAuthorizedForModelException();
			}
			return string.Join(",", list);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000080C4 File Offset: 0x000062C4
		public RSDataSourceConnection GetDataSource(long modelId)
		{
			if (this._rsDataSourceConnection == null)
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				bool flag = true;
				try
				{
					Logger.Verbose("GetDataSource for catalogItem={0}", new object[] { this._catalogItemId });
					return this.GetDataSourceAsync(modelId).Result;
				}
				catch (AggregateException ex)
				{
					flag = false;
					Logger.Error("GetDataSource ERROR for catalogItem={0}, Exception={1}, ElapsedMs={2}", new object[]
					{
						this._catalogItemId,
						ex.Flatten().InnerException.Message,
						stopwatch.ElapsedMilliseconds
					});
					throw ex.InnerException;
				}
				catch (Exception ex2)
				{
					flag = false;
					Logger.Error("GetDataSource ERROR for catalogItem={0}, Exception={1}, ElapsedMs={2}", new object[] { this._catalogItemId, ex2.Message, stopwatch.ElapsedMilliseconds });
					throw;
				}
				finally
				{
					Logger.Verbose("GetDataSource Finished for catalogItem={0}, ElapsedMs={1}, Success={2}", new object[] { this._catalogItemId, stopwatch.ElapsedMilliseconds, flag });
				}
			}
			return this._rsDataSourceConnection;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00008200 File Offset: 0x00006400
		public string ReplaceUserId(string s, IIdentity identity)
		{
			return RSDataSourceProvider.UseridDetectionRegex.Replace(s, identity.Name);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00008213 File Offset: 0x00006413
		private static Regex UseridDetectionRegex
		{
			get
			{
				if (RSDataSourceProvider._useridDetectionRegex == null)
				{
					RSDataSourceProvider._useridDetectionRegex = new Regex("{{[\\s]*[uU][sS][eE][rR][iI][dD][\\s]*}}", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);
				}
				return RSDataSourceProvider._useridDetectionRegex;
			}
		}

		// Token: 0x040000C7 RID: 199
		private readonly Guid _catalogItemId;

		// Token: 0x040000C8 RID: 200
		private readonly RequestContext _requestContext;

		// Token: 0x040000C9 RID: 201
		private readonly ICatalogService _catalogService;

		// Token: 0x040000CA RID: 202
		private readonly ICatalogDataModelDataSourceAccessor _dataModelDataSourceAccessor;

		// Token: 0x040000CB RID: 203
		private readonly ICatalogDataModelRoleAccessor _dataModelRoleAccessor;

		// Token: 0x040000CC RID: 204
		private readonly IUserService _userService;

		// Token: 0x040000CD RID: 205
		private readonly IPrincipal _userPrincipal;

		// Token: 0x040000CE RID: 206
		private readonly IAnalysisServicesServer _asServer;

		// Token: 0x040000CF RID: 207
		private readonly bool _enableRls;

		// Token: 0x040000D0 RID: 208
		private RSDataSourceConnection _rsDataSourceConnection;

		// Token: 0x040000D1 RID: 209
		private static Regex _useridDetectionRegex = null;

		// Token: 0x040000D2 RID: 210
		private const string UseridPattern = "{{[\\s]*[uU][sS][eE][rR][iI][dD][\\s]*}}";

		// Token: 0x040000D3 RID: 211
		private const RegexOptions CompiledRegexOptions = RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline;

		// Token: 0x040000D4 RID: 212
		internal static readonly string PbiEmbeddedModelType = "PowerBIReportEmbedded";
	}
}
