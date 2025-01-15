using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Routing;
using Microsoft.BIServer.Configuration;
using Microsoft.BIServer.Configuration.Http;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.OData.UriParser;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.Exceptions;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.ODataWebApi.Common;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection;
using Model;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x02000010 RID: 16
	public class CatalogItemController<T> : EntitySetReflectionODataController<T> where T : CatalogItem, new()
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003B88 File Offset: 0x00001D88
		protected ICatalogRepository CatalogRepository
		{
			get
			{
				return this._catalogRepository;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003B90 File Offset: 0x00001D90
		protected IDataService DataService
		{
			get
			{
				return this._dataService;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003B98 File Offset: 0x00001D98
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00003BA0 File Offset: 0x00001DA0
		private protected IFileSizeRestrictions FileSizeRestrictions { protected get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003BA9 File Offset: 0x00001DA9
		internal CatalogItemControllerHelper<T> CatalogItemControllerHelper
		{
			get
			{
				return this._catalogItemControllerHelper;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public CatalogItemController(ICatalogRepository catalogRepository, IDataService dataService, ISystemService systemService, IPortalConfigurationManager portalConfigurationManager, ILogger logger)
			: base(logger)
		{
			if (catalogRepository == null)
			{
				throw new ArgumentNullException("catalogRepository");
			}
			if (dataService == null)
			{
				throw new ArgumentNullException("dataService");
			}
			if (systemService == null)
			{
				throw new ArgumentNullException("systemService");
			}
			if (portalConfigurationManager == null)
			{
				throw new ArgumentNullException("portalConfigurationManager");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._uploader = new BinaryUploader(Path.GetTempPath(), "CatalogUpload_", 2147483647L, TimeSpan.FromDays((double)CatalogItemController<T>.DeleteTempFileDays));
			this._catalogRepository = catalogRepository;
			this._dataService = dataService;
			this._systemService = systemService;
			this._portalConfigurationManager = portalConfigurationManager;
			this._logger = logger;
			this._catalogItemControllerHelper = new CatalogItemControllerHelper<T>(catalogRepository, this, systemService, portalConfigurationManager, logger);
			this.FileSizeRestrictions = portalConfigurationManager.Current.FileSizeRestrictions;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003C80 File Offset: 0x00001E80
		public static void RegisterModel(ODataConventionModelBuilder builder, string name)
		{
			builder.EntitySet<T>(name);
			builder.EntityType<T>().Action("Upload").ReturnsFromEntitySet<T>(name);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003CA1 File Offset: 0x00001EA1
		protected override IQueryable<T> GetEntitySet(string castName)
		{
			return this._catalogRepository.GetCatalogItems<T>(base.User);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003CB4 File Offset: 0x00001EB4
		protected override T GetEntity(string key, string castName)
		{
			T t;
			try
			{
				t = (T)((object)this.CatalogItemControllerHelper.GetCatalogItemByKey(key));
			}
			catch (InvalidCastException)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}
			return t;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003CF4 File Offset: 0x00001EF4
		protected virtual T GetEntityByPath(string path)
		{
			return (T)((object)this.CatalogItemControllerHelper.GetCatalogItemByKey(path));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003CF4 File Offset: 0x00001EF4
		protected virtual T GetEntityByGuid(string guidStr)
		{
			return (T)((object)this.CatalogItemControllerHelper.GetCatalogItemByKey(guidStr));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003D07 File Offset: 0x00001F07
		protected virtual T GetEntityByGuid(Guid guid)
		{
			return (T)((object)this.CatalogItemControllerHelper.GetCatalogItemByKey(guid.ToString()));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003D28 File Offset: 0x00001F28
		protected override bool DeleteEntity(string key)
		{
			string text;
			bool flag = this.CatalogItemControllerHelper.IsRequestByPath(key, out text);
			Guid guid = default(Guid);
			if (!flag && !Guid.TryParse(key, out guid))
			{
				return false;
			}
			this.ThrowIfWrongType(key);
			if (!flag)
			{
				return this._catalogRepository.Delete(base.User, guid);
			}
			return this._catalogRepository.Delete(base.User, text);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003D8C File Offset: 0x00001F8C
		protected override bool AddEntity(T entity)
		{
			T t;
			return this._catalogRepository.Create<T>(base.User, entity, out t);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003DAD File Offset: 0x00001FAD
		protected override bool AddEntity(T entity, out T createdEntity)
		{
			return this._catalogRepository.Create<T>(base.User, entity, out createdEntity);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003DC2 File Offset: 0x00001FC2
		protected virtual bool EntityUsesStreamingStorage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003DC8 File Offset: 0x00001FC8
		public async Task<IHttpActionResult> UploadStreamInternal(string Id)
		{
			this.FileSizeRestrictions.ThrowIfSizeIsOutOfLimits(BinaryUploader.ApproximateUploadSize(base.Request));
			T entity = this.GetOrCreateItem(Id);
			bool creating = entity.Id == Guid.Empty;
			IHttpActionResult httpActionResult;
			try
			{
				TaskAwaiter<FileInfo> taskAwaiter = this._uploader.Upload(base.Request).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<FileInfo> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<FileInfo>);
				}
				using (ScopedFileDelete scopedFileDelete = new ScopedFileDelete(taskAwaiter.GetResult()))
				{
					if (this.EntityUsesStreamingStorage)
					{
						using (Stream stream = new FileStream(scopedFileDelete.FullPath, FileMode.Open))
						{
							entity.SetContent(stream);
							return this.Persist(Id, creating, entity);
						}
					}
					entity.Content = File.ReadAllBytes(scopedFileDelete.FullPath);
					httpActionResult = this.Persist(Id, creating, entity);
				}
			}
			catch (IOException ex)
			{
				Exception baseException = ex.GetBaseException();
				if (baseException is HttpListenerException)
				{
					throw new UploadFileCanceledException(baseException.Message, ex);
				}
				throw;
			}
			return httpActionResult;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003E15 File Offset: 0x00002015
		private IHttpActionResult Persist(string Id, bool creating, T entity)
		{
			if (creating)
			{
				this._catalogRepository.Create<T>(base.User, entity, out entity);
				return this.Created<T>(entity);
			}
			if (this.PutEntity(Id, entity))
			{
				return this.Updated<T>(entity);
			}
			return base.BadRequest();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003E50 File Offset: 0x00002050
		private T GetOrCreateItem(string Id)
		{
			string text;
			if (this.CatalogItemControllerHelper.IsRequestByPath(Id, out text))
			{
				try
				{
					T t = this.CatalogRepository.GetCatalogItem(base.User, text) as T;
					if (t == null)
					{
						throw new WrongItemTypeException(text);
					}
					return t;
				}
				catch (ItemNotFoundException)
				{
					T t2 = new T();
					t2.Name = CatalogItem.GetNameFromFullPath(text);
					t2.Path = CatalogItem.GetParentPathFromFullPath(text);
					t2.Id = Guid.Empty;
					return t2;
				}
			}
			T t3 = this.CatalogRepository.GetCatalogItem(base.User, Guid.Parse(Id)) as T;
			if (t3 == null)
			{
				base.Logger.Trace(TraceLevel.Warning, string.Format("Attempting to re-upload a CatalogItem to an invalid ID (GUID={0}) Bad GUID", Id));
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			return t3;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003F38 File Offset: 0x00002138
		protected override bool PutEntity(string key, T entity)
		{
			string text;
			bool flag = this.CatalogItemControllerHelper.IsRequestByPath(key, out text);
			Guid guid = default(Guid);
			if (!flag && !Guid.TryParse(key, out guid))
			{
				return false;
			}
			this.ThrowIfWrongType(key);
			if (!flag)
			{
				return this._catalogRepository.Update(base.User, guid, entity, null);
			}
			return this._catalogRepository.Update(base.User, text, entity, null);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003FA8 File Offset: 0x000021A8
		protected override bool PatchEntity(string key, T entity, string[] delta)
		{
			this.ThrowIfWrongType(key);
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return false;
			}
			string text = "Name";
			string text2 = "Path";
			if (delta.Contains(text) && delta.Contains(text2) && !entity.Path.EndsWith(entity.Name))
			{
				base.Logger.Trace(TraceLevel.Error, "Invalid PATCH request, Path does not end with Name of catalog item.");
				throw new ArgumentException("Path does not end with Name of catalog item.");
			}
			if (delta.Contains(text) && !delta.Contains(text2))
			{
				entity.Path = entity.Path.Substring(0, entity.Path.LastIndexOf("/")) + "/" + entity.Name;
			}
			return this._catalogRepository.Update(base.User, guid, entity, delta);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004093 File Offset: 0x00002293
		protected void ThrowIfWrongType(string key)
		{
			this.GetEntity(key, null);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000040A0 File Offset: 0x000022A0
		protected IHttpActionResult GenerateContent(CatalogItem item, Microsoft.AspNet.OData.Routing.ODataPath oDataPath)
		{
			if (item != null)
			{
				if (item.Content != null)
				{
					MediaTypeHeaderValue mediaTypeHeaderValue = null;
					MediaTypeHeaderValue.TryParse(item.ContentType, out mediaTypeHeaderValue);
					HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
					httpResponseMessage.StatusCode = HttpStatusCode.OK;
					httpResponseMessage.Content = new ByteArrayContent(item.Content);
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
						return base.ResponseMessage(httpResponseMessage);
					}
				}
				return base.StatusCode(HttpStatusCode.NoContent);
			}
			return base.NotFound();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004170 File Offset: 0x00002370
		public virtual IHttpActionResult GetContent(string key, Microsoft.AspNet.OData.Routing.ODataPath oDataPath)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			CatalogItem catalogItemWithContent = this._catalogRepository.GetCatalogItemWithContent(base.User, guid);
			return this.GenerateContent(catalogItemWithContent, oDataPath);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000041AC File Offset: 0x000023AC
		public IHttpActionResult GetDependentItems(string key, CatalogItemType catalogItemType)
		{
			string text;
			bool flag = this.CatalogItemControllerHelper.IsRequestByPath(key, out text);
			Guid guid = default(Guid);
			if (!flag && !Guid.TryParse(key, out guid))
			{
				return base.BadRequest();
			}
			if (catalogItemType != CatalogItemType.Unknown)
			{
				CatalogItem catalogItem = (flag ? this._catalogRepository.GetCatalogItem(base.User, text) : this._catalogRepository.GetCatalogItem(base.User, guid));
				if (catalogItem == null)
				{
					return base.BadRequest();
				}
				if (catalogItem.Type != catalogItemType)
				{
					return base.BadRequest();
				}
			}
			IQueryable<CatalogItem> queryable = (flag ? this._catalogRepository.GetDependentItems(base.User, text) : this._catalogRepository.GetDependentItems(base.User, guid));
			return base.CreateOk(queryable);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000425F File Offset: 0x0000245F
		public virtual IHttpActionResult GetAllowedActions(string Id)
		{
			return this.CatalogItemControllerHelper.GetAllowedActions(Id);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000426D File Offset: 0x0000246D
		[HttpPost]
		public virtual IHttpActionResult GetContentTrusted(string key, ODataActionParameters actionParameters)
		{
			return this.CatalogItemControllerHelper.GetContentTrusted(key, actionParameters);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000427C File Offset: 0x0000247C
		[HttpPost]
		public virtual IHttpActionResult SetPolicies([FromODataUri] Guid key, ODataActionParameters actionParameters)
		{
			return this.CatalogItemControllerHelper.SetPolicies(key, actionParameters);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000428B File Offset: 0x0000248B
		[HttpGet]
		public virtual IHttpActionResult GetPolicies(string Id)
		{
			return this.CatalogItemControllerHelper.GetPolicies(Id);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000429C File Offset: 0x0000249C
		public virtual IHttpActionResult PutPolicies(string key, [FromBody] ItemPolicy policies)
		{
			CatalogItem catalogItem = this.GetEntity(key, null);
			if (catalogItem == null)
			{
				return base.StatusCode(HttpStatusCode.BadRequest);
			}
			this._catalogRepository.SetItemPolicy(base.User, catalogItem.Path, policies);
			return base.Ok();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000042E4 File Offset: 0x000024E4
		public virtual IHttpActionResult PatchPolicies(string key, [FromBody] ItemPolicy policies)
		{
			return base.NotFound();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000042E4 File Offset: 0x000024E4
		public virtual IHttpActionResult PostPolicies(string key, [FromBody] ItemPolicy policies)
		{
			return base.NotFound();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000042E4 File Offset: 0x000024E4
		public virtual IHttpActionResult DeletePolicies(string key, [FromBody] ItemPolicy policies)
		{
			return base.NotFound();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000042EC File Offset: 0x000024EC
		public virtual IHttpActionResult GetCatalogItemProperties(string itemId, [FromUri] string properties)
		{
			return this.CatalogItemControllerHelper.GetCatalogItemProperties(itemId, properties);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000042FB File Offset: 0x000024FB
		public virtual IHttpActionResult PutCatalogItemProperties(string itemId)
		{
			return this.CatalogItemControllerHelper.PutCatalogItemProperties(itemId);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000430C File Offset: 0x0000250C
		protected IHttpActionResult PutOnItemDataSources(string itemId, string dataSourceId, DataSource dataSource)
		{
			if (Guid.Parse(dataSourceId) != dataSource.Id)
			{
				return base.StatusCode(HttpStatusCode.BadRequest);
			}
			CatalogItem catalogItem = this.GetEntity(itemId, null);
			List<DataSource> list = new List<DataSource>();
			list.Add(dataSource);
			this.CatalogRepository.SetItemDataSources(base.User, catalogItem.Path, list);
			return base.StatusCode(HttpStatusCode.NoContent);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004378 File Offset: 0x00002578
		protected IHttpActionResult GetOnItemDataSources(string itemId, string dataSourceId)
		{
			T entity = this.GetEntity(itemId, null);
			IList<DataSource> dataSourcesForCatalogItem = this.CatalogRepository.GetDataSourcesForCatalogItem(base.User, entity.Path);
			Guid guid = default(Guid);
			if (!Guid.TryParse(dataSourceId, out guid))
			{
				return base.StatusCode(HttpStatusCode.BadRequest);
			}
			if (dataSourcesForCatalogItem != null)
			{
				foreach (DataSource dataSource in dataSourcesForCatalogItem)
				{
					if (dataSource.Id == guid)
					{
						return this.Ok<DataSource>(dataSource);
					}
				}
			}
			return base.StatusCode(HttpStatusCode.NotFound);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000442C File Offset: 0x0000262C
		protected IHttpActionResult PutOnDataSourcesCollection(string itemId, HttpRequestMessage request)
		{
			T entity = this.GetEntity(itemId, null);
			string result = request.Content.ReadAsStringAsync().Result;
			List<DataSource> list = new List<DataSource>();
			try
			{
				JsonConvert.PopulateObject(result, list);
			}
			catch (Exception ex)
			{
				base.Logger.Trace(TraceLevel.Verbose, string.Format("Invalid payload: {0}", ex.Message));
				return base.StatusCode(HttpStatusCode.BadRequest);
			}
			this.CatalogRepository.SetItemDataSources(base.User, entity.Path, list);
			return base.StatusCode(HttpStatusCode.OK);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000044CC File Offset: 0x000026CC
		protected async Task<IHttpActionResult> PatchOnDataModelDataSourcesCollectionAsync(string itemId, HttpRequestMessage request)
		{
			T entity = this.GetEntity(itemId, null);
			string result = request.Content.ReadAsStringAsync().Result;
			List<DataSource> list = new List<DataSource>();
			try
			{
				JsonConvert.PopulateObject(result, list);
			}
			catch (Exception ex)
			{
				base.Logger.Trace(TraceLevel.Verbose, string.Format("Invalid payload: {0}", ex.Message));
				return base.StatusCode(HttpStatusCode.BadRequest);
			}
			IHttpActionResult httpActionResult;
			if (list.Any((DataSource ds) => ds.DataModelDataSource == null))
			{
				base.Logger.Trace(TraceLevel.Verbose, "There is no DataModel in one of the data sources");
				httpActionResult = base.StatusCode(HttpStatusCode.BadRequest);
			}
			else
			{
				try
				{
					await this.CatalogRepository.UpdateDataModelDataSourcesAsync(base.User, entity.Id, list);
				}
				catch (AccessDeniedException)
				{
					throw new HttpResponseException(HttpStatusCode.Forbidden);
				}
				catch (CatalogItemContentInvalidException)
				{
					throw new HttpResponseException(HttpStatusCode.BadRequest);
				}
				httpActionResult = base.StatusCode(HttpStatusCode.OK);
			}
			return httpActionResult;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004524 File Offset: 0x00002724
		protected IHttpActionResult PostOnHistorySnapshots(string reportId)
		{
			bool flag = false;
			try
			{
				T entity = this.GetEntity(reportId, null);
				this.CatalogRepository.CreateItemHistorySnapshot(base.User, entity.Path);
				flag = true;
			}
			catch (AccessDeniedException)
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			return this.Ok<bool>(flag);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004580 File Offset: 0x00002780
		protected IHttpActionResult GetOnHistorySnapshots(string reportId, string historyId)
		{
			try
			{
				T entity = this.GetEntity(reportId, null);
				foreach (HistorySnapshot historySnapshot in this.CatalogRepository.GetHistorySnapshots(base.User, entity.Path))
				{
					if (historySnapshot.Id.ToString() == historyId)
					{
						return this.Ok<HistorySnapshot>(historySnapshot);
					}
				}
			}
			catch (AccessDeniedException)
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			return base.NotFound();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004638 File Offset: 0x00002838
		protected IHttpActionResult DeleteOnHistorySnapshots(string reportId, string historyId)
		{
			bool flag = false;
			try
			{
				T entity = this.GetEntity(reportId, null);
				flag = this.CatalogRepository.DeleteItemHistorySnapshotByHistoryId(base.User, entity.Path, historyId);
			}
			catch (AccessDeniedException)
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			return this.Ok<bool>(flag);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004694 File Offset: 0x00002894
		protected IHttpActionResult PutOnHistorySnapshotOptions(string reportId, HistorySnapshotOptions historySnapshotOptions)
		{
			try
			{
				T entity = this.GetEntity(reportId, null);
				this.CatalogRepository.SetReportHistorySnapshotOptions(base.User, entity.Path, historySnapshotOptions.HistorySnapshotsOptions);
			}
			catch (AccessDeniedException)
			{
				return base.StatusCode(HttpStatusCode.Forbidden);
			}
			return base.Ok();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000046F8 File Offset: 0x000028F8
		public virtual IHttpActionResult GetCacheOptions(string key)
		{
			CatalogItem catalogItemByKey = this.CatalogItemControllerHelper.GetCatalogItemByKey(key);
			if (catalogItemByKey == null)
			{
				return base.BadRequest();
			}
			CacheOptions itemCacheOptions = this._catalogRepository.GetItemCacheOptions(base.User, catalogItemByKey.Path);
			return base.CreateOk(itemCacheOptions);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000473C File Offset: 0x0000293C
		public virtual IHttpActionResult SetCacheOptions(string key, CacheOptions cacheOptions)
		{
			if (!base.ModelState.IsValid || cacheOptions == null)
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			CatalogItem catalogItemByKey = this.CatalogItemControllerHelper.GetCatalogItemByKey(key);
			if (catalogItemByKey == null)
			{
				return base.BadRequest();
			}
			this._catalogRepository.SetItemCacheOptions(base.User, catalogItemByKey.Path, cacheOptions);
			return base.StatusCode(HttpStatusCode.NoContent);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000047A0 File Offset: 0x000029A0
		public virtual IHttpActionResult PostComment(string Id, Comment comment)
		{
			IHttpActionResult httpActionResult;
			try
			{
				Comment comment2;
				this._catalogRepository.CreateComment(base.User, comment, out comment2);
				httpActionResult = this.Ok<Comment>(comment2);
			}
			catch (AccessDeniedException)
			{
				this._logger.Trace(TraceLevel.Info, string.Format("User {0} doesn't have permission to write comments on an item they don't have permissions to view ({1}).", base.User.Identity.Name, Id));
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			catch (ItemNotFoundException)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			catch (CatalogItemContentInvalidException)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}
			catch (MaxCountCommentsException)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}
			return httpActionResult;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004858 File Offset: 0x00002A58
		public virtual IHttpActionResult GetComments(string key)
		{
			CatalogItem catalogItem = this.GetEntity(key, null);
			IList<Comment> commentsByItem = this._catalogRepository.GetCommentsByItem(base.User, catalogItem.Id);
			return this.Ok<IList<Comment>>(commentsByItem);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004894 File Offset: 0x00002A94
		public virtual IHttpActionResult DeleteComment(string key)
		{
			long num = long.Parse(key);
			bool flag = this._catalogRepository.IsUserContextOwner(base.User, num);
			IHttpActionResult httpActionResult;
			try
			{
				this._catalogRepository.DeleteComment(base.User, num, !flag);
				httpActionResult = base.StatusCode(HttpStatusCode.NoContent);
			}
			catch (AccessDeniedException)
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			return httpActionResult;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004900 File Offset: 0x00002B00
		public virtual IHttpActionResult UpdateComment(string key, Comment comment)
		{
			comment.Id = long.Parse(key);
			if (!this._catalogRepository.IsUserContextOwner(base.User, comment.Id))
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			IHttpActionResult httpActionResult;
			try
			{
				this._catalogRepository.UpdateComment(base.User, comment, null);
				httpActionResult = base.StatusCode(HttpStatusCode.NoContent);
			}
			catch (AccessDeniedException)
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			return httpActionResult;
		}

		// Token: 0x04000042 RID: 66
		private const string UploadPrefix = "CatalogUpload_";

		// Token: 0x04000043 RID: 67
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x04000044 RID: 68
		private readonly IDataService _dataService;

		// Token: 0x04000045 RID: 69
		private readonly ISystemService _systemService;

		// Token: 0x04000046 RID: 70
		private readonly IPortalConfigurationManager _portalConfigurationManager;

		// Token: 0x04000047 RID: 71
		private readonly ILogger _logger;

		// Token: 0x04000048 RID: 72
		private readonly CatalogItemControllerHelper<T> _catalogItemControllerHelper;

		// Token: 0x04000049 RID: 73
		private readonly BinaryUploader _uploader;

		// Token: 0x0400004A RID: 74
		protected static readonly int DeleteTempFileDays = StaticConfig.Current.GetIntOrDefault("DeletePbixUploadTempFileDays", 1);
	}
}
