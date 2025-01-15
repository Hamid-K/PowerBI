using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Routing;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.ODataWebApi.Common;
using Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x02000011 RID: 17
	public class CatalogItemsController : CatalogItemsController
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00004998 File Offset: 0x00002B98
		public CatalogItemsController(ICatalogRepository catalogRepository, IDataService dataService, ILogger logger, IEncryptionService encryptionService, ISystemService systemService, IPortalConfigurationManager portalConfigurationManager)
			: base(catalogRepository, dataService, systemService, portalConfigurationManager, logger)
		{
			this._catalogRepository = base.CatalogRepository;
			this._dataService = base.DataService;
			this._logger = base.Logger;
			this._encryptionService = encryptionService;
			this._systemService = systemService;
			this._portalConfigurationManager = portalConfigurationManager;
			this._powerBIReportsControllerHelper = new PowerBIReportsControllerHelper(this._catalogRepository, this, this._portalConfigurationManager, this._logger);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004A0C File Offset: 0x00002C0C
		public new static void RegisterModel(ODataConventionModelBuilder builder)
		{
			builder.EntityType<Property>();
			builder.EntityType<HistorySnapshotOptions>();
			builder.EntityType<CacheOptions>();
			builder.EntitySet<CatalogItem>("CatalogItems");
			builder.EntityType<CatalogItem>().Collection.Action("DeleteItems").Returns<BulkOperationsResult>().CollectionParameter<string>("CatalogItemPaths");
			ActionConfiguration actionConfiguration = builder.EntityType<CatalogItem>().Collection.Action("MoveItems").Returns<BulkOperationsResult>();
			actionConfiguration.Parameter<string>("TargetPath");
			actionConfiguration.CollectionParameter<string>("CatalogItemPaths");
			builder.EntityType<CatalogItem>().Function("AccessToken").Returns<CatalogItemAccessToken>();
			builder.EntityType<ItemPolicy>();
			builder.EntityType<CatalogItem>().Action("GetContentTrusted").Returns<string>()
				.Parameter<string>(CatalogItemControllerHelper<CatalogItem>.TrustedProcessTokenString);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004ACD File Offset: 0x00002CCD
		protected override IQueryable<CatalogItem> GetEntitySet(string castName)
		{
			return base.GetEntitySet(castName);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004AD8 File Offset: 0x00002CD8
		protected override CatalogItem GetEntity(string key, string castName)
		{
			string empty = string.Empty;
			bool flag = base.CatalogItemControllerHelper.IsRequestByPath(key, out empty);
			Guid guid = default(Guid);
			if (!flag && !Guid.TryParse(key, out guid))
			{
				return null;
			}
			if (castName == CatalogItemsController.DataSourceClassName)
			{
				if (!flag)
				{
					return this._catalogRepository.GetDataSource(base.User, guid);
				}
				return this._catalogRepository.GetDataSource(base.User, empty);
			}
			else
			{
				if (!(castName == CatalogItemsController.DataSetClassName))
				{
					return base.CatalogItemControllerHelper.GetCatalogItemByKey(key);
				}
				if (!flag)
				{
					return this._catalogRepository.GetDataSet(base.User, guid);
				}
				return this._catalogRepository.GetDataSet(base.User, empty);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004B8C File Offset: 0x00002D8C
		protected override bool DeleteEntity(string key)
		{
			string text;
			bool flag = base.CatalogItemControllerHelper.IsRequestByPath(key, out text);
			Guid guid = default(Guid);
			if (!flag && !Guid.TryParse(key, out guid))
			{
				return false;
			}
			if (!flag)
			{
				return this._catalogRepository.Delete(base.User, guid);
			}
			return this._catalogRepository.Delete(base.User, text);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004BE7 File Offset: 0x00002DE7
		protected override bool AddEntity(CatalogItem entity)
		{
			return base.AddEntity(entity);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004BF0 File Offset: 0x00002DF0
		protected override bool AddEntity(CatalogItem entity, out CatalogItem createdEntity)
		{
			this.ValidatePbiFile(entity);
			return base.AddEntity(entity, out createdEntity);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004C01 File Offset: 0x00002E01
		private void ValidatePbiFile(CatalogItem entity)
		{
			if (entity.Type == CatalogItemType.PowerBIReport && this._systemService.IsBiServer() && entity.HasContent())
			{
				this.ValidatePbiReportRenderingIsSupportedAndSetProperties((PowerBIReport)entity);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004C30 File Offset: 0x00002E30
		protected override bool PutEntity(string key, CatalogItem entity)
		{
			this.ValidatePbiFile(entity);
			string text;
			bool flag = base.CatalogItemControllerHelper.IsRequestByPath(key, out text);
			Guid guid = default(Guid);
			if (!flag && !Guid.TryParse(key, out guid))
			{
				return false;
			}
			if (!flag)
			{
				return this._catalogRepository.Update(base.User, guid, entity, null);
			}
			return this._catalogRepository.Update(base.User, text, entity, null);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004C98 File Offset: 0x00002E98
		protected override bool PatchEntity(string key, CatalogItem entity, string[] delta)
		{
			this.ValidatePbiFile(entity);
			string text;
			bool flag = base.CatalogItemControllerHelper.IsRequestByPath(key, out text);
			Guid guid = default(Guid);
			if (!flag && !Guid.TryParse(key, out guid))
			{
				return false;
			}
			string text2 = "Name";
			string text3 = "Path";
			if (delta.Contains(text2) && delta.Contains(text3) && !entity.Path.EndsWith(entity.Name))
			{
				base.Logger.Trace(TraceLevel.Error, "Invalid PATCH request, Path does not end with Name of catalog item.");
				throw new ArgumentException("Path does not end with Name of catalog item.");
			}
			if (delta.Contains(text2) && !delta.Contains(text3))
			{
				entity.Path = entity.Path.Substring(0, entity.Path.LastIndexOf("/")) + "/" + entity.Name;
			}
			if (!flag)
			{
				return this._catalogRepository.Update(base.User, guid, entity, delta);
			}
			return this._catalogRepository.Update(base.User, text, entity, delta);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004D98 File Offset: 0x00002F98
		[HttpGet]
		[ODataRoute("CatalogItems({key})/Content/$value")]
		[ODataRoute("CatalogItems(Path={key})/Content/$value")]
		public override IHttpActionResult GetContent(string key, ODataPath oDataPath)
		{
			string text;
			bool flag = base.CatalogItemControllerHelper.IsRequestByPath(key, out text);
			Guid guid = default(Guid);
			if (!flag && !Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			CatalogItem catalogItem = (flag ? this._catalogRepository.GetCatalogItemWithContent(base.User, text) : this._catalogRepository.GetCatalogItemWithContent(base.User, guid));
			return base.GenerateContent(catalogItem, oDataPath);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004E02 File Offset: 0x00003002
		[HttpPost]
		public virtual IHttpActionResult GetContentTrusted(string key, ODataActionParameters actionParameters)
		{
			return base.CatalogItemControllerHelper.GetContentTrusted(key, actionParameters);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004E11 File Offset: 0x00003011
		[HttpPost]
		public override IHttpActionResult DeleteItems(ODataActionParameters actionParameters)
		{
			return base.DeleteItems(actionParameters);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004E1A File Offset: 0x0000301A
		[HttpPost]
		public override IHttpActionResult MoveItems(ODataActionParameters actionParameters)
		{
			return base.MoveItems(actionParameters);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004E24 File Offset: 0x00003024
		[HttpGet]
		public virtual IHttpActionResult AccessToken(string key)
		{
			if (!this._systemService.IsBiServer())
			{
				return base.NotAllowed();
			}
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			return this.Ok<CatalogItemAccessToken>(this._catalogRepository.CreateCatalogItemAccessToken(this._encryptionService, base.User, guid));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004E74 File Offset: 0x00003074
		protected virtual void ValidatePbiReportRenderingIsSupportedAndSetProperties(PowerBIReport item)
		{
			this._powerBIReportsControllerHelper.ValidatePbiReportRenderingIsSupportedAndSetProperties(item);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004E82 File Offset: 0x00003082
		[HttpPatch]
		[ODataRoute("CatalogItems({itemId})/Properties")]
		[ODataRoute("CatalogItems(Path={itemId})/Properties")]
		public IHttpActionResult PutMultipleItemProperties(string itemId)
		{
			return base.NotAllowed();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004E8A File Offset: 0x0000308A
		[HttpGet]
		[ODataRoute("CatalogItems({itemId})/Properties")]
		[ODataRoute("CatalogItems(Path={itemId})/Properties")]
		public IHttpActionResult GetCatalogItemProperties(string itemId, [FromUri] string properties)
		{
			return base.CatalogItemControllerHelper.GetCatalogItemProperties(itemId, properties);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004E99 File Offset: 0x00003099
		[HttpPut]
		[ODataRoute("CatalogItems({itemId})/Properties")]
		[ODataRoute("CatalogItems(Path={itemId})/Properties")]
		public IHttpActionResult PutCatalogItemProperties(string itemId)
		{
			return base.CatalogItemControllerHelper.PutCatalogItemProperties(itemId);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004EA8 File Offset: 0x000030A8
		[HttpGet]
		[ODataRoute("CatalogItems({key})/Roles")]
		[ODataRoute("CatalogItems(Path={key})/Roles")]
		public override IHttpActionResult GetRoles()
		{
			IQueryable<Role> catalogRoles = this._catalogRepository.GetCatalogRoles(base.User);
			return base.CreateOk(catalogRoles);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004E82 File Offset: 0x00003082
		[HttpPost]
		[HttpPut]
		[HttpDelete]
		[HttpPatch]
		[ODataRoute("CatalogItems({key})/Roles")]
		[ODataRoute("CatalogItems(Path={key})/Roles")]
		public IHttpActionResult PostRoles()
		{
			return base.NotAllowed();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004ECE File Offset: 0x000030CE
		[HttpGet]
		[ODataRoute("CatalogItems({Id})/Policies")]
		[ODataRoute("CatalogItems(Path={Id})/Policies")]
		public override IHttpActionResult GetPolicies(string key)
		{
			return base.CatalogItemControllerHelper.GetPolicies(key);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004EDC File Offset: 0x000030DC
		[HttpPut]
		[ODataRoute("CatalogItems({key})/Policies")]
		[ODataRoute("CatalogItems(Path={key})/Policies")]
		public IHttpActionResult PutPolicies(string key, [FromBody] ItemPolicy policies)
		{
			CatalogItem entity = this.GetEntity(key, null);
			if (entity == null)
			{
				return base.StatusCode(HttpStatusCode.BadRequest);
			}
			this._catalogRepository.SetItemPolicy(base.User, entity.Path, policies);
			return base.Ok();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004F1F File Offset: 0x0000311F
		[HttpPatch]
		[ODataRoute("CatalogItems({key})/Policies")]
		[ODataRoute("CatalogItems(Path={key})/Policies")]
		public IHttpActionResult PatchPolicies(string key, [FromBody] ItemPolicy policies)
		{
			return base.NotFound();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004F1F File Offset: 0x0000311F
		[HttpPost]
		[ODataRoute("CatalogItems({key})/Policies")]
		[ODataRoute("CatalogItems(Path={key})/Policies")]
		public IHttpActionResult PostPolicies(string key, [FromBody] ItemPolicy policies)
		{
			return base.NotFound();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004F1F File Offset: 0x0000311F
		[HttpDelete]
		[ODataRoute("CatalogItems({key})/Policies")]
		[ODataRoute("CatalogItems(Path={key})/Policies")]
		public IHttpActionResult DeletePolicies(string key, [FromBody] ItemPolicy policies)
		{
			return base.NotFound();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004F27 File Offset: 0x00003127
		[HttpGet]
		[ODataRoute("CatalogItems({Id})/AllowedActions")]
		[ODataRoute("CatalogItems(Path={Id})/AllowedActions")]
		public IHttpActionResult GetAllowedActions(string Id)
		{
			return base.CatalogItemControllerHelper.GetAllowedActions(Id);
		}

		// Token: 0x0400004C RID: 76
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x0400004D RID: 77
		private readonly IEncryptionService _encryptionService;

		// Token: 0x0400004E RID: 78
		private readonly IDataService _dataService;

		// Token: 0x0400004F RID: 79
		private readonly ISystemService _systemService;

		// Token: 0x04000050 RID: 80
		private readonly ILogger _logger;

		// Token: 0x04000051 RID: 81
		private readonly IPortalConfigurationManager _portalConfigurationManager;

		// Token: 0x04000052 RID: 82
		private readonly PowerBIReportsControllerHelper _powerBIReportsControllerHelper;
	}
}
