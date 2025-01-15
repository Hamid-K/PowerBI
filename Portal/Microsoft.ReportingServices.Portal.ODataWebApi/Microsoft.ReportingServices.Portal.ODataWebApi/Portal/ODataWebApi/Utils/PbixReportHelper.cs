using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.BIServer.HostingEnvironment.Request;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;
using Microsoft.ReportingServices.Portal.Interfaces.Exceptions;
using Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers;
using Microsoft.ReportingServices.Portal.Services.ODataExtensions;
using Microsoft.ReportingServices.Portal.Services.SoapProxy;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Utils
{
	// Token: 0x0200003B RID: 59
	internal sealed class PbixReportHelper : IPbixReportHelper
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x0000B904 File Offset: 0x00009B04
		public bool ShouldReShred(PowerBIReport entity, Uri basePortalUrl, ILogger logger, IPrincipal userPrincipal, string reportServerHostName)
		{
			if (PbixReportHelper.ShredderVersion.Equals(0.0))
			{
				string text = string.Format("{0}/api/servicestate", basePortalUrl);
				logger.Trace(TraceLevel.Verbose, string.Format("Getting service state using: {0}", text));
				HttpWebRequest httpWebRequest = SoapAuthenticationHelper.PrepareWebRequestWithCorrespondingAuthMechanism((HttpWebRequest)WebRequest.Create(text), userPrincipal, reportServerHostName);
				httpWebRequest.Method = "GET";
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				if (httpWebResponse.StatusCode != HttpStatusCode.OK)
				{
					throw new WebException("Failed to call PBIX servicestate API");
				}
				using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
				{
					JObject jobject = (JObject)JsonConvert.DeserializeObject(streamReader.ReadToEnd());
					if (!double.TryParse(jobject["PbixShredderVersion"].ToString(), out PbixReportHelper.ShredderVersion))
					{
						throw new WebException(string.Format("PBIX Server returned unexpected data for shredder property:jObject[PbixShredderPropertyName]={0}", jobject["PbixShredderVersion"]));
					}
				}
			}
			double num = 0.0;
			Property property = entity.Properties.FirstOrDefault((Property p) => p.Name == "PbixShredderVersion");
			if (property != null && !double.TryParse(property.Value, out num))
			{
				throw new WebException(string.Format("PBIX Catalog item has unexpected data for shredder property {0}, found {1}", "PbixShredderVersion", property));
			}
			bool flag = false;
			Property property2 = entity.Properties.FirstOrDefault((Property p) => p.Name == "HasEmbeddedModels");
			if (property2 != null && !bool.TryParse(property2.Value, out flag))
			{
				throw new WebException(string.Format("PBIX Catalog item has unexpected data for shredder property {0}, found {1}", "HasEmbeddedModels", property2));
			}
			return (num >= 3.0 || !flag) && PbixReportHelper.ShredderVersion > num;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000BAD0 File Offset: 0x00009CD0
		public IsRenderingSupportedResult ValidateRenderingIsSupportedAndSetProperties(PowerBIReport entity, string powerBiUri, ILogger logger, IPrincipal userPrincipal, string reportServerHostName)
		{
			if (!entity.HasContent())
			{
				throw new ArgumentException("entity does not have content");
			}
			Stream contentStream = entity.GetContentStream();
			RequestContext fromCallContext = RequestContext.GetFromCallContext();
			string text = string.Format("{0}/api/reportproperties", powerBiUri);
			logger.Trace(TraceLevel.Verbose, string.Format("Getting report properties if pbix file is supported using: {0}", text));
			HttpWebRequest httpWebRequest = SoapAuthenticationHelper.PrepareWebRequestWithCorrespondingAuthMechanism((HttpWebRequest)WebRequest.Create(text), userPrincipal, reportServerHostName);
			int num = ((contentStream != null) ? ((int)contentStream.Length) : entity.Content.Length);
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			httpWebRequest.ContentLength = (long)num;
			httpWebRequest.Headers.Add("RequestId", fromCallContext.RequestID);
			httpWebRequest.Headers.Add("X-SSRS-ClientSessionId", fromCallContext.ClientSessionID);
			Stream requestStream = httpWebRequest.GetRequestStream();
			if (contentStream != null)
			{
				contentStream.CopyTo(requestStream);
			}
			else
			{
				requestStream.Write(entity.Content, 0, num);
			}
			requestStream.Close();
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			if (httpWebResponse.StatusCode != HttpStatusCode.OK)
			{
				throw new WebException("Failed to call reportproperties API");
			}
			IsRenderingSupportedResult isRenderingSupportedResult;
			using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
			{
				JObject jobject = (JObject)JsonConvert.DeserializeObject(streamReader.ReadToEnd());
				HttpStatusCode httpStatusCode = jobject["StatusCode"].ToObject<HttpStatusCode>();
				string text2 = jobject["Body"].ToString();
				if (httpStatusCode == HttpStatusCode.OK)
				{
					JObject jobject2 = (JObject)JsonConvert.DeserializeObject(text2);
					JObject jobject3 = (JObject)JsonConvert.DeserializeObject(jobject2["Properties"].ToString());
					if (jobject3["DataModel"] != null)
					{
						if (!string.IsNullOrEmpty(jobject3["DataModel"].Value<string>()))
						{
							byte[] array = (byte[])jobject3["DataModel"];
							entity.SetModelStream(new MemoryStream(array));
						}
						jobject3.Remove("DataModel");
					}
					entity.Properties = (from p in jobject3.Properties()
						select new Property
						{
							Name = p.Name,
							Value = p.Value.ToString()
						}).ToArray<Property>();
					entity.DataModelParameters = this.DeserializeDataModelParameters(jobject2);
					this.DeserializeDataSources(entity, jobject2);
					this.DeserializeDataModelRoles(entity, jobject2);
					this.DetermineIsModelRefreshAllowed(entity);
					isRenderingSupportedResult = IsRenderingSupportedResult.RenderingSupported();
				}
				else
				{
					int num2 = (int)httpStatusCode;
					ErrorCode errorCode;
					if (num2 != 401)
					{
						if (num2 != 462)
						{
							throw new WebException(string.Format("isformatsupported call returned unkown status code {0}", httpStatusCode));
						}
						errorCode = ErrorCode.PowerBIReportNotSupportedVersion;
					}
					else
					{
						errorCode = ErrorCode.AccessDenied;
					}
					isRenderingSupportedResult = IsRenderingSupportedResult.RenderingUnsupported(errorCode, text2);
				}
			}
			return isRenderingSupportedResult;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000BD80 File Offset: 0x00009F80
		public IsRenderingSupportedResult PbiParse(PowerBIReport entity, PreShreddedPbixFiles files, string powerBiUri, ILogger logger, IPrincipal userPrincipal, string reportServerHostName)
		{
			TimeSpan timeSpan = TimeSpan.FromMinutes((double)StaticConfig.Current.GetIntOrException(ConfigSettings.InterProcessTimeoutMinutes.ToString()));
			if (files == null)
			{
				throw new ArgumentException("Content temp file doesn't exist");
			}
			RequestContext fromCallContext = RequestContext.GetFromCallContext();
			string text = string.Format("{0}/api/reportpropertiesfromlocalfiles", powerBiUri);
			logger.Trace(TraceLevel.Verbose, string.Format("Parsing DataSources for file {0}, using: {1}", files.ToString(), text));
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(text);
			httpWebRequest = SoapAuthenticationHelper.PrepareWebRequestWithCorrespondingAuthMechanism(httpWebRequest, userPrincipal, reportServerHostName);
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Headers.Add("RequestId", fromCallContext.RequestID);
			httpWebRequest.Headers.Add("X-SSRS-ClientSessionId", fromCallContext.ClientSessionID);
			httpWebRequest.ReadWriteTimeout = (int)timeSpan.TotalMilliseconds;
			httpWebRequest.Timeout = (int)timeSpan.TotalMilliseconds;
			string text2 = JsonConvert.SerializeObject(files);
			httpWebRequest.ContentLength = (long)text2.Length;
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				using (StreamWriter streamWriter = new StreamWriter(requestStream))
				{
					streamWriter.Write(text2);
				}
			}
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			if (httpWebResponse.StatusCode != HttpStatusCode.OK)
			{
				throw new WebException("Failed to call PBI Web API: " + text);
			}
			IsRenderingSupportedResult isRenderingSupportedResult;
			using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
			{
				JObject jobject = (JObject)JsonConvert.DeserializeObject(streamReader.ReadToEnd());
				HttpStatusCode httpStatusCode = jobject["StatusCode"].ToObject<HttpStatusCode>();
				string text3 = jobject["Body"].ToString();
				if (httpStatusCode == HttpStatusCode.OK)
				{
					JObject jobject2 = (JObject)JsonConvert.DeserializeObject(text3);
					JObject jobject3 = (JObject)JsonConvert.DeserializeObject(jobject2["Properties"].ToString());
					entity.Properties = (from p in jobject3.Properties()
						select new Property
						{
							Name = p.Name,
							Value = p.Value.ToString()
						}).ToArray<Property>();
					entity.DataModelParameters = this.DeserializeDataModelParameters(jobject2);
					this.DeserializeDataSources(entity, jobject2);
					this.DeserializeDataModelRoles(entity, jobject2);
					this.DetermineIsModelRefreshAllowed(entity);
					isRenderingSupportedResult = IsRenderingSupportedResult.RenderingSupported();
				}
				else
				{
					int num = (int)httpStatusCode;
					ErrorCode errorCode;
					if (num != 401)
					{
						if (num != 462)
						{
							throw new WebException(string.Format("isformatsupported call returned unkown status code {0}", httpStatusCode));
						}
						errorCode = ErrorCode.PowerBIReportNotSupportedVersion;
					}
					else
					{
						errorCode = ErrorCode.AccessDenied;
					}
					isRenderingSupportedResult = IsRenderingSupportedResult.RenderingUnsupported(errorCode, text3);
				}
			}
			return isRenderingSupportedResult;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000C054 File Offset: 0x0000A254
		public IList<DataSource> UpdateDataModelParametersInPowerBI(Guid catalogId, IList<DataModelParameter> dataModelParameters, string powerBiUri, ILogger logger, IPrincipal userPrincipal, string reportServerHostName)
		{
			HttpWebRequest httpWebRequest = this.BuildUpdateDataModelParametersWebRequest(catalogId, dataModelParameters, powerBiUri, logger, userPrincipal, reportServerHostName);
			return (from pbiDs in JsonConvert.DeserializeObject<IList<PowerBIDataSource>>(this.GetUpdateDataModelParametersResponse(httpWebRequest))
				select pbiDs.ToModelDataSource()).ToList<DataSource>();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
		public DataSourceCheckResult TestDataSource(string powerBiUri, ILogger logger, IPrincipal userPrincipal, DataSource dataSource, string reportServerHostName)
		{
			string text = JsonConvert.SerializeObject(dataSource);
			byte[] requestBytes = Encoding.UTF8.GetBytes(text);
			RequestContext requestContext = RequestContext.GetFromCallContext();
			string pbiUrl = string.Format("{0}/api/checkdatasourceconnection", powerBiUri);
			logger.Trace(TraceLevel.Verbose, string.Format("Testing Data Model Data Source using: {0}", pbiUrl));
			WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
			if (dataSource.CredentialRetrieval == CredentialRetrievalType.integrated && userPrincipal.Identity is WindowsIdentity)
			{
				windowsIdentity = (WindowsIdentity)userPrincipal.Identity;
			}
			HttpWebResponse httpWebResponse = WindowsIdentity.RunImpersonated<HttpWebResponse>(windowsIdentity.AccessToken, delegate
			{
				HttpWebRequest httpWebRequest = SoapAuthenticationHelper.PrepareWebRequestWithCorrespondingAuthMechanism((HttpWebRequest)WebRequest.Create(pbiUrl), userPrincipal, reportServerHostName);
				httpWebRequest.Method = "POST";
				httpWebRequest.ContentType = "application/json";
				httpWebRequest.ContentLength = (long)requestBytes.Length;
				httpWebRequest.Headers.Add("RequestId", requestContext.RequestID);
				httpWebRequest.Headers.Add("X-SSRS-ClientSessionId", requestContext.ClientSessionID);
				Stream requestStream = httpWebRequest.GetRequestStream();
				requestStream.Write(requestBytes, 0, requestBytes.Length);
				requestStream.Close();
				return (HttpWebResponse)httpWebRequest.GetResponse();
			});
			if (httpWebResponse.StatusCode != HttpStatusCode.OK)
			{
				throw new WebException("Failed to call checkdatasourceconnection API");
			}
			DataSourceCheckResult dataSourceCheckResult;
			using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
			{
				dataSourceCheckResult = JsonConvert.DeserializeObject<DataSourceCheckResult>(streamReader.ReadToEnd());
			}
			return dataSourceCheckResult;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000C1B0 File Offset: 0x0000A3B0
		private void DeserializeDataSources(PowerBIReport entity, JObject bodyJObj)
		{
			JArray jarray = (JArray)JsonConvert.DeserializeObject(bodyJObj["DataSources"].ToString());
			if (jarray != null)
			{
				entity.DataSources = new List<DataSource>();
				foreach (JToken jtoken in jarray)
				{
					JObject jobject = (JObject)JsonConvert.DeserializeObject(jtoken["DataModelDataSource"].ToString());
					DataModelDataSourceKind dataModelDataSourceKind;
					if (!Enum.TryParse<DataModelDataSourceKind>(jobject["Kind"].ToString(), out dataModelDataSourceKind))
					{
						throw new ArgumentOutOfRangeException("Invalid data source kind specified!");
					}
					DataModelDataSourceType dataModelDataSourceType;
					if (!Enum.TryParse<DataModelDataSourceType>(jobject["Type"].ToString(), out dataModelDataSourceType))
					{
						throw new ArgumentOutOfRangeException("Invalid data source type specified!");
					}
					DataModelDataSourceAuthType dataModelDataSourceAuthType;
					if (!Enum.TryParse<DataModelDataSourceAuthType>(jobject["AuthType"].ToString(), out dataModelDataSourceAuthType))
					{
						throw new ArgumentOutOfRangeException("Invalid data source authType specified!");
					}
					if (dataModelDataSourceAuthType == DataModelDataSourceAuthType.Unknown)
					{
						DataModelDataSourceAuthType[] supportedAuthKinds = dataModelDataSourceKind.GetSupportedAuthKinds(dataModelDataSourceType);
						if (supportedAuthKinds.Count<DataModelDataSourceAuthType>() > 0)
						{
							dataModelDataSourceAuthType = supportedAuthKinds.First<DataModelDataSourceAuthType>();
						}
					}
					string text = ((jobject["ModelConnectionName"] != null) ? jobject["ModelConnectionName"].ToString() : null);
					string text2 = jtoken["ConnectionString"].ToString();
					DataSource dataSource = new DataSource
					{
						ConnectionString = text2,
						DataModelDataSource = new DataModelDataSource
						{
							Kind = dataModelDataSourceKind,
							Type = dataModelDataSourceType,
							ModelConnectionName = text,
							AuthType = dataModelDataSourceAuthType
						}
					};
					entity.DataSources.Add(dataSource);
				}
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000C350 File Offset: 0x0000A550
		private void DeserializeDataModelRoles(PowerBIReport entity, JObject bodyJObj)
		{
			List<DataModelRole> list = bodyJObj["DataModelRoles"].ToObject<List<DataModelRole>>();
			if (list != null)
			{
				foreach (DataModelRole dataModelRole in list)
				{
					entity.DataModelRoles.Add(dataModelRole);
				}
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
		internal IList<DataModelParameter> DeserializeDataModelParameters(JObject bodyJObj)
		{
			if (bodyJObj == null)
			{
				throw new ArgumentNullException("bodyJObj");
			}
			JToken jtoken = ((bodyJObj == null) ? null : bodyJObj["DataModelParameters"]);
			return ((jtoken == null || jtoken.Type == JTokenType.Null) ? new JArray() : ((JArray)JsonConvert.DeserializeObject(jtoken.ToString()))).Select((JToken p) => new DataModelParameter
			{
				Name = p["Name"].ToString(),
				Value = p["Value"].ToString()
			}).ToList<DataModelParameter>();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000C434 File Offset: 0x0000A634
		private void DetermineIsModelRefreshAllowed(PowerBIReport entity)
		{
			entity.Properties.Where((Property x) => x.Name == "ModelRefreshAllowed").First<Property>().Value = entity.DataSources.All((DataSource x) => x.DataModelDataSource.Type == DataModelDataSourceType.Import && x.DataModelDataSource.AuthType != DataModelDataSourceAuthType.Unknown && x.DataModelDataSource.AuthType != DataModelDataSourceAuthType.Integrated && x.DataModelDataSource.AuthType != DataModelDataSourceAuthType.Impersonate && x.DataModelDataSource.AuthType != DataModelDataSourceAuthType.Windows && x.IsNotConnectionToLocalFile()).ToString();
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000C4A7 File Offset: 0x0000A6A7
		public bool CanBeTestedByMashup(DataSource dataSource)
		{
			return PbixReportHelper.CanBeTestedByMashupInternal(dataSource);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000C4B0 File Offset: 0x0000A6B0
		internal static bool CanBeTestedByMashupInternal(DataSource dataSource)
		{
			bool flag = (dataSource.DataModelDataSource.Kind == DataModelDataSourceKind.SQL || dataSource.DataModelDataSource.Kind == DataModelDataSourceKind.Teradata || dataSource.DataModelDataSource.Kind == DataModelDataSourceKind.Oracle) && PbixReportHelper.IsNativeDQConnectionString(dataSource.ConnectionString);
			return (dataSource.DataModelDataSource.Type != DataModelDataSourceType.DirectQuery || (!flag && !PbixReportHelper.UsesHanaOdbcProvider(dataSource.ConnectionString))) && dataSource.DataModelDataSource.Type != DataModelDataSourceType.Live;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000C52C File Offset: 0x0000A72C
		internal static bool IsNativeDQConnectionString(string connectionString)
		{
			bool flag;
			try
			{
				flag = !string.IsNullOrEmpty(new DbConnectionStringBuilder
				{
					ConnectionString = connectionString
				}["data source"] as string);
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000C578 File Offset: 0x0000A778
		internal static bool UsesHanaOdbcProvider(string connectionString)
		{
			connectionString = connectionString.ToLower();
			string text = "msdasql";
			string text2 = "msdasql.1";
			string text3 = "hdbodbc";
			return !string.IsNullOrEmpty(connectionString) && connectionString.Contains(text3) && (connectionString.Contains(text) || connectionString.Contains(text2));
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
		internal HttpWebRequest BuildUpdateDataModelParametersWebRequest(Guid catalogId, IList<DataModelParameter> dataModelParameters, string powerBiUri, ILogger logger, IPrincipal userPrincipal, string reportServerHostName)
		{
			string text = JsonConvert.SerializeObject(dataModelParameters);
			RequestContext fromCallContext = RequestContext.GetFromCallContext();
			string text2 = string.Format("{0}/api/report/{1}/parameters", powerBiUri, catalogId.ToString().ToUpper());
			logger.Trace(TraceLevel.Verbose, string.Format("Updating Data Model Data Source Parameters using: {0}", text2));
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(text2);
			httpWebRequest = SoapAuthenticationHelper.PrepareWebRequestWithCorrespondingAuthMechanism(httpWebRequest, userPrincipal, reportServerHostName);
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Headers.Add("RequestId", fromCallContext.RequestID);
			httpWebRequest.Headers.Add("X-SSRS-ClientSessionId", fromCallContext.ClientSessionID);
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				using (StreamWriter streamWriter = new StreamWriter(requestStream))
				{
					streamWriter.Write(text);
				}
			}
			return httpWebRequest;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
		internal string GetUpdateDataModelParametersResponse(HttpWebRequest request)
		{
			HttpWebResponse httpWebResponse = (HttpWebResponse)request.GetResponse();
			if (httpWebResponse.StatusCode != HttpStatusCode.OK)
			{
				throw new FailedToSetDataModelParameterException(string.Format("Failed to call PowerBI API: {0}", request.RequestUri.OriginalString));
			}
			string text;
			using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
			{
				text = streamReader.ReadToEnd();
			}
			return text;
		}

		// Token: 0x040000AF RID: 175
		private const string BodyJObjKey = "Body";

		// Token: 0x040000B0 RID: 176
		private const string DataModelJObjKey = "DataModel";

		// Token: 0x040000B1 RID: 177
		private const string DataModelDataSourceJTokenKey = "DataModelDataSource";

		// Token: 0x040000B2 RID: 178
		private const string DataModelDataSourceAuthTypeJObjKey = "AuthType";

		// Token: 0x040000B3 RID: 179
		private const string DataModelDataSourceConnectionStringJObjKey = "ConnectionString";

		// Token: 0x040000B4 RID: 180
		private const string DataModelDataSourceDataModelRolesJObjKey = "DataModelRoles";

		// Token: 0x040000B5 RID: 181
		private const string DataModelDataSourceKindJObjKey = "Kind";

		// Token: 0x040000B6 RID: 182
		private const string DataModelDataSourceModelConnectionNameJObjKey = "ModelConnectionName";

		// Token: 0x040000B7 RID: 183
		private const string DataModelDataSourceSecretJObjKey = "Secret";

		// Token: 0x040000B8 RID: 184
		private const string DataModelDataSourceTypeJObjKey = "Type";

		// Token: 0x040000B9 RID: 185
		private const string DataModelDataSourceUsernameJObjKey = "Username";

		// Token: 0x040000BA RID: 186
		private const string DataModelParametersJObjKey = "DataModelParameters";

		// Token: 0x040000BB RID: 187
		private const string DataModelParameterNameJObjKey = "Name";

		// Token: 0x040000BC RID: 188
		private const string DataModelParameterValueJObjKey = "Value";

		// Token: 0x040000BD RID: 189
		private const string DataSourceIdentifierJObjKey = "DataSourceIdentifier";

		// Token: 0x040000BE RID: 190
		private const string DataSourcesJObjKey = "DataSources";

		// Token: 0x040000BF RID: 191
		private const string PropertiesJObjKey = "Properties";

		// Token: 0x040000C0 RID: 192
		private const string StatusCodeJObjKey = "StatusCode";

		// Token: 0x040000C1 RID: 193
		internal static double ShredderVersion;
	}
}
