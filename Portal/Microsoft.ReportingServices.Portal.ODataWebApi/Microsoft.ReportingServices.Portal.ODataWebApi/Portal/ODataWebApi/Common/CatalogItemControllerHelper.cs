using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Services.Protocols;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.Exceptions;
using Microsoft.BIServer.HostingEnvironment.Request;
using Microsoft.OData.UriParser;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection;
using Microsoft.ReportingServices.Portal.ODataWebApi.Utils;
using Microsoft.ReportingServices.Portal.ODataWebApi.V1.Filters;
using Microsoft.ReportingServices.Portal.Services.ODataExtensions;
using Model;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Common
{
	// Token: 0x02000044 RID: 68
	internal sealed class CatalogItemControllerHelper<T> where T : CatalogItem
	{
		// Token: 0x06000322 RID: 802 RVA: 0x0000D554 File Offset: 0x0000B754
		internal CatalogItemControllerHelper(ICatalogRepository catalogRepository, EntitySetReflectionODataController<T> catalogItemController, ISystemService systemService, IPortalConfigurationManager portalConfigurationManager, ILogger logger)
		{
			this._catalogRepository = catalogRepository;
			this._catalogItemController = catalogItemController;
			this._systemService = systemService;
			this._portalConfigurationManager = portalConfigurationManager;
			this._logger = logger;
			this._pbixReportHelper = new PbixReportHelper();
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000D5A2 File Offset: 0x0000B7A2
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000D5AA File Offset: 0x0000B7AA
		internal IPbixReportHelper PbixReportHelper
		{
			get
			{
				return this._pbixReportHelper;
			}
			set
			{
				this._pbixReportHelper = value;
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000D5B3 File Offset: 0x0000B7B3
		internal CatalogItem GetCatalogItemByKey(string key)
		{
			return this.GetItem(key);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000D5BC File Offset: 0x0000B7BC
		internal IHttpActionResult GenerateContent(T item, Microsoft.AspNet.OData.Routing.ODataPath oDataPath)
		{
			if (item != null)
			{
				if (item.Content != null || item.GetContentStream() != null)
				{
					MediaTypeHeaderValue mediaTypeHeaderValue = null;
					MediaTypeHeaderValue.TryParse(item.ContentType, out mediaTypeHeaderValue);
					HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
					httpResponseMessage.StatusCode = HttpStatusCode.OK;
					if (item.GetContentStream() != null)
					{
						httpResponseMessage.Content = new StreamContent(item.GetContentStream(), this.StreamResponseBufferSizeInBytes);
					}
					else
					{
						httpResponseMessage.Content = new ByteArrayContent(item.Content);
					}
					if (oDataPath.Segments.Last<ODataPathSegment>() is ValueSegment)
					{
						string text = "attachment";
						if (mediaTypeHeaderValue != null)
						{
							httpResponseMessage.Content.Headers.ContentType = mediaTypeHeaderValue;
						}
						if (item.Type == CatalogItemType.Resource)
						{
							text = "inline";
						}
						httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue(text)
						{
							FileName = this._catalogRepository.GetCatalogItemDownloadFileName(item)
						};
						return this._catalogItemController.ResponseMessage(httpResponseMessage);
					}
				}
				return this._catalogItemController.StatusCode(HttpStatusCode.NoContent);
			}
			return this._catalogItemController.NotFound();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000D6F0 File Offset: 0x0000B8F0
		internal IHttpActionResult GetContentTrusted(string key, ODataActionParameters actionParameters)
		{
			if (actionParameters == null)
			{
				return this._catalogItemController.BadRequest();
			}
			if (!this._catalogItemController.ModelState.IsValid || !actionParameters.ContainsKey(CatalogItemControllerHelper<T>.TrustedProcessTokenString))
			{
				return this._catalogItemController.BadRequest(this._catalogItemController.GetModelStateValidationErrors());
			}
			try
			{
				TrustedProcessToken.ValididateTokenOrException((string)actionParameters[CatalogItemControllerHelper<T>.TrustedProcessTokenString]);
			}
			catch (InvalidTrustedProcessTokenException ex)
			{
				return this._catalogItemController.BadRequest(ex.Message);
			}
			catch (TrustedProcessTokenExpiredException)
			{
				HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.RequestTimeout);
				return this._catalogItemController.ResponseMessage(httpResponseMessage);
			}
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return this._catalogItemController.NotFound();
			}
			T t = (T)((object)this._catalogRepository.GetCatalogItemWithContentTrusted(this._catalogItemController.User, guid));
			Microsoft.AspNet.OData.Routing.ODataPath odataPath = new Microsoft.AspNet.OData.Routing.ODataPath(new ODataPathSegment[]
			{
				new ValueSegment(null)
			});
			return this.GenerateContent(t, odataPath);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000D800 File Offset: 0x0000BA00
		internal IHttpActionResult GetPolicies(string Id)
		{
			CatalogItem catalogItemByKey = this.GetCatalogItemByKey(Id);
			if (catalogItemByKey == null)
			{
				return this._catalogItemController.BadRequest();
			}
			ItemPolicy itemPolicy = this._catalogRepository.GetItemPolicy(this._catalogItemController.User, catalogItemByKey.Path);
			return this._catalogItemController.CreateOk(itemPolicy);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000D850 File Offset: 0x0000BA50
		internal IHttpActionResult GetAllowedActions(string Id)
		{
			IHttpActionResult httpActionResult;
			try
			{
				CatalogItem catalogItemByKey = this.GetCatalogItemByKey(Id);
				if (catalogItemByKey == null)
				{
					httpActionResult = this._catalogItemController.BadRequest();
				}
				else
				{
					List<AllowedAction> list = (from s in this._catalogRepository.GetAllowedActions(this._catalogItemController.User, catalogItemByKey.Path)
						select new AllowedAction
						{
							Action = s
						}).ToList<AllowedAction>();
					httpActionResult = this._catalogItemController.CreateOk(list);
				}
			}
			catch (AccessDeniedException)
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			return httpActionResult;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000D8E8 File Offset: 0x0000BAE8
		internal bool IsRequestByPath(string key, out string path)
		{
			path = "Path='";
			bool flag = false;
			if (key.StartsWith(path, StringComparison.OrdinalIgnoreCase))
			{
				flag = true;
				path = key.Substring(path.Length, key.Length - path.Length - 1);
				path = path.Replace("''", "'");
			}
			if (!flag)
			{
				Guid guid = default(Guid);
				bool flag2 = Guid.TryParse(key, out guid);
				if (!flag2 && key.StartsWith("'/"))
				{
					path = key.Substring(1, key.Length - 2);
					flag = true;
					path = path.Replace("''", "'");
				}
				else if (!flag2 && key.StartsWith("/"))
				{
					path = key;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000D9A0 File Offset: 0x0000BBA0
		internal IHttpActionResult SetPolicies(Guid key, ODataActionParameters actionParameters)
		{
			if (actionParameters == null)
			{
				return this._catalogItemController.BadRequest();
			}
			if (!this._catalogItemController.ModelState.IsValid || !actionParameters.ContainsKey("Policy"))
			{
				return this._catalogItemController.BadRequest(this._catalogItemController.GetModelStateValidationErrors());
			}
			ItemPolicy itemPolicy = (ItemPolicy)actionParameters["Policy"];
			CatalogItem catalogItem = this._catalogRepository.GetCatalogItem(this._catalogItemController.User, key);
			if (catalogItem == null)
			{
				return this._catalogItemController.NotFound();
			}
			this._catalogRepository.SetItemPolicy(this._catalogItemController.User, catalogItem.Path, itemPolicy);
			return this._catalogItemController.Ok();
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000DA54 File Offset: 0x0000BC54
		private CatalogItem GetItem(string key)
		{
			string empty = string.Empty;
			bool flag = this.IsRequestByPath(key, out empty);
			Guid guid = default(Guid);
			if (!flag && !Guid.TryParse(key, out guid))
			{
				return null;
			}
			CatalogItem catalogItem = (flag ? this._catalogRepository.GetCatalogItem(this._catalogItemController.User, empty) : this._catalogRepository.GetCatalogItem(this._catalogItemController.User, guid));
			if (catalogItem != null && catalogItem.Type == CatalogItemType.PowerBIReport)
			{
				PowerBIReport powerBIReport = (PowerBIReport)catalogItem;
				if (this._systemService.IsBiServer() && this.PbixReportHelper.ShouldReShred(powerBIReport, new Uri(this._portalConfigurationManager.Current.PowerBIUrl), this._logger, this._catalogItemController.User, this._portalConfigurationManager.Current.ReportServerHostName))
				{
					PowerBIReport powerBIReport2 = null;
					try
					{
						powerBIReport2 = (PowerBIReport)this._catalogRepository.GetCatalogItemWithContentTrusted(this._catalogItemController.User, catalogItem.Id);
					}
					catch (AccessDeniedException)
					{
						return catalogItem;
					}
					if (this.PbixReportHelper.ValidateRenderingIsSupportedAndSetProperties(powerBIReport2, this._portalConfigurationManager.Current.PowerBIUrl, this._logger, this._catalogItemController.User, this._portalConfigurationManager.Current.ReportServerHostName).IsSupported)
					{
						powerBIReport.HasDataSources = true;
						this._catalogRepository.SetItemPropertiesTrusted(this._catalogItemController.User, powerBIReport2.Path, powerBIReport2.Properties);
						return catalogItem;
					}
					return catalogItem;
				}
			}
			return catalogItem;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000DBE4 File Offset: 0x0000BDE4
		public IHttpActionResult GetCatalogItemProperties(string itemId, [FromUri] string properties)
		{
			if (properties == null)
			{
				return this._catalogItemController.BadRequest();
			}
			CatalogItem item = this.GetItem(itemId);
			List<Property> list = (from x in properties.Replace("'", string.Empty).Split(new char[] { ',' })
				select new Property
				{
					Name = x,
					Value = string.Empty
				}).ToList<Property>();
			IQueryable<Property> itemProperties = this._catalogRepository.GetItemProperties(this._catalogItemController.User, item.Path, list);
			return this._catalogItemController.CreateOk(itemProperties);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000DC7C File Offset: 0x0000BE7C
		public IHttpActionResult PutCatalogItemProperties(string itemId)
		{
			List<Property> list = new List<Property>();
			string result = this._catalogItemController.Request.Content.ReadAsStringAsync().Result;
			list = new List<Property>();
			try
			{
				JsonConvert.PopulateObject(result, list);
			}
			catch (Exception ex)
			{
				Logger.Trace(string.Format("Invalid payload: {0}", ex.Message), Array.Empty<object>());
				return this._catalogItemController.BadRequest();
			}
			CatalogItem item = this.GetItem(itemId);
			this._catalogRepository.SetItemProperties(this._catalogItemController.User, item.Path, list);
			return this._catalogItemController.Ok();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000DD28 File Offset: 0x0000BF28
		internal IHttpActionResult CheckDataSourceConnection(string key, ODataActionParameters actionParameters)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return this._catalogItemController.NotFound();
			}
			if (actionParameters == null || !this._catalogItemController.ModelState.IsValid || !actionParameters.ContainsKey("DataSourceName"))
			{
				return this._catalogItemController.BadRequest(this._catalogItemController.GetModelStateValidationErrors());
			}
			DataSourceCheckResult dataSourceCheckResult = this._catalogRepository.TestDataSource(this._catalogItemController.User, guid, actionParameters["DataSourceName"].ToString());
			return this._catalogItemController.CreateOk(dataSourceCheckResult);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000DDB8 File Offset: 0x0000BFB8
		internal void ThrowExceptionIfFound<TException>(Exception exception) where TException : Exception
		{
			for (Exception ex = exception; ex != null; ex = ex.InnerException)
			{
				if (ex is TException)
				{
					throw ex;
				}
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		internal string EncodeFileNameForMimeHeader(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				return fileName;
			}
			StringBuilder stringBuilder = new StringBuilder(fileName.Length);
			char[] array = fileName.ToCharArray();
			for (int i = 0; i < fileName.Length; i++)
			{
				int num = Convert.ToInt32(array[i]);
				if ((num >= 65 && num <= 90) || (num >= 97 && num <= 122) || (num >= 48 && num <= 57) || num == 32 || num == 46)
				{
					stringBuilder.Append(array[i]);
				}
				else
				{
					bool flag = num >= 55296 && num <= 57343;
					foreach (byte b in Encoding.UTF8.GetBytes(array, i, flag ? 2 : 1))
					{
						stringBuilder.Append("%");
						stringBuilder.Append(b.ToString("X", CultureInfo.InvariantCulture));
					}
					if (flag)
					{
						i++;
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000DED8 File Offset: 0x0000C0D8
		public IHttpActionResult CheckConnection(ODataActionParameters actionParameters)
		{
			object obj = null;
			if (actionParameters == null || !actionParameters.TryGetValue("dataSource", out obj))
			{
				return this._catalogItemController.BadRequest(this._catalogItemController.GetModelStateValidationErrors());
			}
			DataSource dataSource = obj as DataSource;
			if (dataSource == null)
			{
				return this._catalogItemController.BadRequest(this._catalogItemController.GetModelStateValidationErrors());
			}
			if (string.Equals(dataSource.DataSourceSubType, DataModelDataSourceExtensions.DataSourceSubTypeName, StringComparison.OrdinalIgnoreCase))
			{
				if (dataSource.DataModelDataSource == null)
				{
					return this._catalogItemController.BadRequest(this._catalogItemController.GetModelStateValidationErrors());
				}
				bool flag = this.PbixReportHelper.CanBeTestedByMashup(dataSource);
				if (dataSource.DataModelDataSource.Type == DataModelDataSourceType.Import || flag)
				{
					DataSourceCheckResult dataSourceCheckResult = this._pbixReportHelper.TestDataSource(this._portalConfigurationManager.Current.PowerBIUrl, this._logger, this._catalogItemController.User, dataSource, this._portalConfigurationManager.Current.ReportServerHostName);
					return this._catalogItemController.CreateOk(dataSourceCheckResult);
				}
				dataSource = dataSource.ToDataSourceWithDecryptedSecret();
			}
			DataSourceCheckResult dataSourceCheckResult2 = this._catalogRepository.TestDataSource(this._catalogItemController.User, dataSource);
			return this._catalogItemController.CreateOk(dataSourceCheckResult2);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000E000 File Offset: 0x0000C200
		public IHttpActionResult CheckConnection(string key)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return this._catalogItemController.NotFound();
			}
			DataSourceCheckResult dataSourceCheckResult = this._catalogRepository.TestDataSource(this._catalogItemController.User, guid);
			return this._catalogItemController.CreateOk(dataSourceCheckResult);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000E048 File Offset: 0x0000C248
		public IHttpActionResult GetQueryFields(ODataActionParameters actionParameters)
		{
			if (!this._catalogItemController.ModelState.IsValid || actionParameters == null || !actionParameters.ContainsKey("query"))
			{
				return this._catalogItemController.BadRequest(this._catalogItemController.GetModelStateValidationErrors());
			}
			Query query = actionParameters["query"] as Query;
			DataSource dataSource = null;
			if (actionParameters.ContainsKey("dataSource"))
			{
				dataSource = actionParameters["dataSource"] as DataSource;
			}
			string text = string.Empty;
			if (actionParameters.ContainsKey("subscriptionId"))
			{
				text = actionParameters["subscriptionId"] as string;
			}
			if (query == null)
			{
				return this._catalogItemController.BadRequest(this._catalogItemController.GetModelStateValidationErrors());
			}
			if (dataSource != null && dataSource.ConnectionString == null && dataSource.DataSourceType == null && !dataSource.IsReference)
			{
				dataSource = null;
			}
			if (dataSource == null && string.IsNullOrEmpty(text))
			{
				return this._catalogItemController.BadRequest(this._catalogItemController.GetModelStateValidationErrors());
			}
			if (dataSource == null)
			{
				dataSource = this._catalogRepository.GetDataRetrievalPlanFromCatalog(this._catalogItemController.User, new Guid(text)).DataSource;
				if (!dataSource.IsReference && dataSource.CredentialRetrieval == CredentialRetrievalType.store && dataSource.CredentialsInServer.Password == null)
				{
					byte[] array = null;
					try
					{
						array = this._catalogRepository.GetDataSourcePasswordForSubscription(this._catalogItemController.User, new Guid(text));
					}
					catch (Exception)
					{
						Logger.Error("Retrieving the password from server failed.", Array.Empty<object>());
					}
					if (array != null)
					{
						dataSource.CredentialsInServer.Password = CatalogEncryption.Instance.DecryptToString(array, "Password");
					}
				}
			}
			IEnumerable<string> queryFields;
			try
			{
				queryFields = this._catalogRepository.GetQueryFields(this._catalogItemController.User, dataSource, query);
			}
			catch (SoapException ex)
			{
				PortalExceptionFilter.AllowSoapDetailException(ex, ErrorCode.rsCannotPrepareQuery);
				throw;
			}
			return this._catalogItemController.CreateOk(queryFields);
		}

		// Token: 0x040000C6 RID: 198
		private readonly ISystemService _systemService;

		// Token: 0x040000C7 RID: 199
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x040000C8 RID: 200
		private readonly IPortalConfigurationManager _portalConfigurationManager;

		// Token: 0x040000C9 RID: 201
		private readonly ILogger _logger;

		// Token: 0x040000CA RID: 202
		private IPbixReportHelper _pbixReportHelper;

		// Token: 0x040000CB RID: 203
		private EntitySetReflectionODataController<T> _catalogItemController;

		// Token: 0x040000CC RID: 204
		internal static string TrustedProcessTokenString = "TrustedProcessToken";

		// Token: 0x040000CD RID: 205
		internal readonly int StreamResponseBufferSizeInBytes = 1048576;
	}
}
