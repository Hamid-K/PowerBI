using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Routing;
using Microsoft.BIServer.Configuration.Http;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.Exceptions;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.ODataWebApi.Common;
using Model;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x02000017 RID: 23
	public class PowerBIReportsController : CatalogItemController<PowerBIReport>
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x000054BC File Offset: 0x000036BC
		public PowerBIReportsController(ICatalogRepository catalogRepository, IDataService dataService, ILogger logger, IPortalConfigurationManager portalConfigurationManager, ISystemService systemService)
			: base(catalogRepository, dataService, systemService, portalConfigurationManager, logger)
		{
			this._powerBIReportsControllerHelper = new PowerBIReportsControllerHelper(catalogRepository, this, portalConfigurationManager, logger);
			this._systemService = systemService;
			this._uploader = new BinaryUploader(Path.GetTempPath(), "PbixUpload_", 2147483647L, TimeSpan.FromDays((double)CatalogItemController<PowerBIReport>.DeleteTempFileDays));
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005514 File Offset: 0x00003714
		internal PowerBIReportsControllerHelper PowerBIReportsControllerHelper
		{
			get
			{
				return this._powerBIReportsControllerHelper;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000551C File Offset: 0x0000371C
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			CatalogItemController<PowerBIReport>.RegisterModel(builder, "PowerBIReports");
			builder.EntityType<PowerBIReport>().Action("CheckDataSourceConnection").Returns<DataSourceCheckResult>()
				.Parameter<string>("DataSourceName");
			builder.EntityType<DataModelRole>();
			builder.EntityType<DataModelRoleAssignment>();
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005557 File Offset: 0x00003757
		protected override bool EntityUsesStreamingStorage
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000555C File Offset: 0x0000375C
		protected override PowerBIReport GetEntity(string Id, string castName)
		{
			PowerBIReport entity = base.GetEntity(Id, castName);
			try
			{
				IList<DataModelParameter> dataModelParametersHelper = this.GetDataModelParametersHelper(entity.Id.ToString());
				entity.DataModelParameters = dataModelParametersHelper;
			}
			catch (AccessDeniedException ex)
			{
				base.Logger.Trace(TraceLevel.Error, string.Format("{0}: 'GetDataModelParameters' for report '{1}' ({2})", ex.Message, entity.Name, Id));
			}
			return entity;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000055D0 File Offset: 0x000037D0
		protected override bool AddEntity(PowerBIReport entity, out PowerBIReport createdEntity)
		{
			this.ValidatePbiFileIfRequired(entity);
			return base.AddEntity(entity, out createdEntity);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000055E4 File Offset: 0x000037E4
		[HttpPost]
		[ODataRoute("PowerBIReports({Id})/Model.Upload")]
		[ODataRoute("PowerBIReports(Path={Id})/Model.Upload")]
		public async Task<IHttpActionResult> UploadStream(string Id)
		{
			IHttpActionResult httpActionResult;
			using (ScopeMeter.Use(new string[] { "POST", "PowerBIReports({Id})/Model.Upload" }))
			{
				base.FileSizeRestrictions.ThrowIfSizeIsOutOfLimits(BinaryUploader.ApproximateUploadSize(base.Request));
				if (!this._systemService.IsBiServer())
				{
					httpActionResult = await base.UploadStreamInternal(Id);
				}
				else
				{
					PowerBIReport entity = this.GetOrCreatePowerBIReport(Id);
					try
					{
						using (PreShreddedPbixFiles preShreddedPbixFiles = await PreShreddedPbixFiles.UploadAndShred(this._uploader, base.Request))
						{
							this._powerBIReportsControllerHelper.PbiParse(entity, preShreddedPbixFiles);
							using (Stream stream = PreShreddedPbixFiles.CreateReadStreamFromPathIfExists(preShreddedPbixFiles.Original))
							{
								using (Stream stream2 = PreShreddedPbixFiles.CreateReadStreamFromPathIfExists(preShreddedPbixFiles.Pbix))
								{
									using (Stream stream3 = PreShreddedPbixFiles.CreateReadStreamFromPathIfExists(preShreddedPbixFiles.Model))
									{
										entity.SetPreShreddedReadStreams(stream, stream2, stream3);
										if (entity.Properties != null)
										{
											foreach (Property property in entity.Properties)
											{
												if (string.Compare(property.Name, "HasEmbeddedModels", StringComparison.InvariantCultureIgnoreCase) == 0)
												{
													property.Value = (stream3 != null).ToString();
													break;
												}
											}
										}
										base.CatalogRepository.UploadPowerBIReport(base.User, entity);
										entity.ClearContent();
										httpActionResult = this.Created<PowerBIReport>(entity);
									}
								}
							}
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
				}
			}
			return httpActionResult;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005634 File Offset: 0x00003834
		private PowerBIReport GetOrCreatePowerBIReport(string Id)
		{
			string text;
			if (base.CatalogItemControllerHelper.IsRequestByPath(Id, out text))
			{
				try
				{
					PowerBIReport powerBIReport = base.CatalogRepository.GetCatalogItem(base.User, text) as PowerBIReport;
					if (powerBIReport == null)
					{
						throw new WrongItemTypeException(text);
					}
					return powerBIReport;
				}
				catch (ItemNotFoundException)
				{
					return new PowerBIReport
					{
						Name = CatalogItem.GetNameFromFullPath(text),
						Path = CatalogItem.GetParentPathFromFullPath(text),
						Id = Guid.Empty
					};
				}
			}
			PowerBIReport powerBIReport2 = base.CatalogRepository.GetCatalogItem(base.User, Guid.Parse(Id)) as PowerBIReport;
			if (powerBIReport2 == null)
			{
				base.Logger.Trace(TraceLevel.Warning, string.Format("Attempting to re-upload a PowerBIReport to an invalid ID (GUID={0}) Either wrong GUID, or the object isn't a PowerBiReport", Id));
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			return powerBIReport2;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000056F4 File Offset: 0x000038F4
		protected override bool PutEntity(string Id, PowerBIReport entity)
		{
			this.ValidatePbiFileIfRequired(entity);
			return base.PutEntity(Id, entity);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005705 File Offset: 0x00003905
		protected override bool PatchEntity(string Id, PowerBIReport entity, string[] delta)
		{
			this.ValidatePbiFileIfRequired(entity);
			return base.PatchEntity(Id, entity, delta);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005717 File Offset: 0x00003917
		protected override bool DeleteEntity(string Id)
		{
			return base.DeleteEntity(Id);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005720 File Offset: 0x00003920
		private void ValidatePbiFileIfRequired(PowerBIReport entity)
		{
			if (entity.HasContent())
			{
				this._powerBIReportsControllerHelper.ValidatePbiReportRenderingIsSupportedAndSetProperties(entity);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005736 File Offset: 0x00003936
		[HttpGet]
		[ODataRoute("PowerBIReports({Id})/Policies")]
		[ODataRoute("PowerBIReports(Path={Id})/Policies")]
		public override IHttpActionResult GetPolicies(string Id)
		{
			return base.GetPolicies(Id);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000573F File Offset: 0x0000393F
		[HttpPut]
		[ODataRoute("PowerBIReports({Id})/Policies")]
		[ODataRoute("PowerBIReports(Path={Id})/Policies")]
		public override IHttpActionResult PutPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PutPolicies(Id, policies);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005749 File Offset: 0x00003949
		[HttpPatch]
		[ODataRoute("PowerBIReports({Id})/Policies")]
		[ODataRoute("PowerBIReports(Path={Id})/Policies")]
		public override IHttpActionResult PatchPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PatchPolicies(Id, policies);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005753 File Offset: 0x00003953
		[HttpPost]
		[ODataRoute("PowerBIReports({Id})/Policies")]
		[ODataRoute("PowerBIReports(Path={Id})/Policies")]
		public override IHttpActionResult PostPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PostPolicies(Id, policies);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000575D File Offset: 0x0000395D
		[HttpDelete]
		[ODataRoute("PowerBIReports({Id})/Policies")]
		[ODataRoute("PowerBIReports(Path={Id})/Policies")]
		public override IHttpActionResult DeletePolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.DeletePolicies(Id, policies);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005767 File Offset: 0x00003967
		[HttpPatch]
		[HttpPost]
		[HttpPut]
		[ODataRoute("PowerBIReports({Id})/DataSources({dataSourceId})")]
		[ODataRoute("PowerBIReports(Path={Id})/DataSources({dataSourceId})")]
		public IHttpActionResult PutItemDataSources(string Id, string dataSourceId, [FromBody] DataSource dataSource)
		{
			return base.NotAllowed();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000576F File Offset: 0x0000396F
		[HttpGet]
		[ODataRoute("PowerBIReports({Id})/DataSources({dataSourceId})")]
		[ODataRoute("PowerBIReports(Path={Id})/DataSources({dataSourceId})")]
		public IHttpActionResult GetItemDataSources(string Id, string dataSourceId)
		{
			if (!this._systemService.IsBiServer())
			{
				return base.NotAllowed();
			}
			return base.GetOnItemDataSources(Id, dataSourceId);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000578D File Offset: 0x0000398D
		[HttpPatch]
		[ODataRoute("PowerBIReports({Id})/DataSources")]
		[ODataRoute("PowerBIReports(Path={Id})/DataSources")]
		public Task<IHttpActionResult> PatchDataSources(string Id)
		{
			if (!this._systemService.IsBiServer())
			{
				return new Task<IHttpActionResult>(new Func<IHttpActionResult>(base.NotAllowed));
			}
			return base.PatchOnDataModelDataSourcesCollectionAsync(Id, base.Request);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000057BB File Offset: 0x000039BB
		[HttpPost]
		[HttpPut]
		[ODataRoute("PowerBIReports({Id})/DataSources")]
		[ODataRoute("PowerBIReports(Path={Id})/DataSources")]
		public IHttpActionResult NotAllowedDataSourcesMethods(string Id)
		{
			return base.StatusCode(HttpStatusCode.MethodNotAllowed);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005767 File Offset: 0x00003967
		[HttpPost]
		[HttpDelete]
		[HttpPatch]
		[ODataRoute("PowerBIReports({Id})/Properties")]
		[ODataRoute("PowerBIReports(Path={Id})/Properties")]
		public IHttpActionResult NotAllowedPropetiesMethods(string Id)
		{
			return base.NotAllowed();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000057C8 File Offset: 0x000039C8
		[HttpGet]
		[ODataRoute("PowerBIReports({Id})/Properties")]
		[ODataRoute("PowerBIReports(Path={Id})/Properties")]
		public override IHttpActionResult GetCatalogItemProperties(string Id, [FromUri] string properties)
		{
			return base.GetCatalogItemProperties(Id, properties);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000057D2 File Offset: 0x000039D2
		[HttpPut]
		[ODataRoute("PowerBIReports({Id})/Properties")]
		[ODataRoute("PowerBIReports(Path={Id})/Properties")]
		public override IHttpActionResult PutCatalogItemProperties(string Id)
		{
			return base.PutCatalogItemProperties(Id);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000057DC File Offset: 0x000039DC
		[HttpGet]
		[ODataRoute("PowerBIReports({Id})/DataModelParameters")]
		[ODataRoute("PowerBIReports(Path={Id})/DataModelParameters")]
		public IHttpActionResult GetDataModelParameters(string Id)
		{
			PowerBIReport entity = this.GetEntity(Id, null);
			if (this.IsModelV1(entity))
			{
				string text = "Only reports created with version October/2020 or later and with enhanced metadata enabled can have parameters updated.";
				base.Logger.Trace(TraceLevel.Error, text);
				return base.BadRequest(text);
			}
			return base.CreateOk(entity.DataModelParameters);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005824 File Offset: 0x00003A24
		private IList<DataModelParameter> GetDataModelParametersHelper(string id)
		{
			string dataModelParameters = base.CatalogRepository.GetDataModelParameters(base.User, Guid.Parse(id));
			if (!string.IsNullOrEmpty(dataModelParameters))
			{
				return JsonConvert.DeserializeObject<IList<DataModelParameter>>(dataModelParameters);
			}
			return new List<DataModelParameter>();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005860 File Offset: 0x00003A60
		private bool IsModelV1(PowerBIReport report)
		{
			IList<Property> properties = report.Properties;
			Property property;
			if (properties == null)
			{
				property = null;
			}
			else
			{
				property = properties.FirstOrDefault((Property p) => p.Name.Equals("ModelVersion", StringComparison.OrdinalIgnoreCase));
			}
			Property property2 = property;
			return property2 == null || property2.Value.Equals("PowerBI_V1", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000058B8 File Offset: 0x00003AB8
		[HttpPost]
		[ODataRoute("PowerBIReports({Id})/DataModelParameters")]
		[ODataRoute("PowerBIReports(Path={Id})/DataModelParameters")]
		public async Task<IHttpActionResult> PostDataModelParameters(string Id)
		{
			PowerBIReport item = this.GetEntity(Id, null);
			IDictionary<string, DataModelParameter> currentParams = item.DataModelParameters.ToDictionary((DataModelParameter p) => p.Name);
			string text = await base.Request.Content.ReadAsStringAsync();
			IList<DataModelParameter> list = new List<DataModelParameter>();
			IHttpActionResult httpActionResult;
			if (this.IsModelV1(item))
			{
				string text2 = "Only report created with version October/2020 or later and with enhanced metadata enabled can have parameters updated.";
				base.Logger.Trace(TraceLevel.Error, text2);
				httpActionResult = base.BadRequest(text2);
			}
			else
			{
				try
				{
					JsonConvert.PopulateObject(text, list);
				}
				catch (Exception ex)
				{
					string text3 = string.Format("Unable to parse exceptions : {0} due to {1}", list, ex);
					base.Logger.Trace(TraceLevel.Error, text3);
					return base.BadRequest(text3);
				}
				try
				{
					this.UpdateParametersMapWithOnlyExistingParameters(currentParams, list);
				}
				catch (InvalidDataModelParameterException ex2)
				{
					string text4 = string.Format("Unable to aggregate the updates for parameters: {0} due to {1}", list, ex2);
					base.Logger.Trace(TraceLevel.Error, text4);
					return base.BadRequest(text4);
				}
				string text5 = JsonConvert.SerializeObject(currentParams.Values.ToList<DataModelParameter>());
				try
				{
					base.CatalogRepository.SetDataModelParameters(base.User, item.Id, text5);
					IList<DataSource> list2 = this.PowerBIReportsControllerHelper.UpdateDataModelParametersInAS(item.Id, list);
					base.CatalogRepository.SetDataModelDataSourcesTrusted(base.User, item.Id, list2, true);
				}
				catch (InvalidDataModelParameterException ex3)
				{
					string text6 = string.Format("Unable to apply new parameters: {0} to the datastore due to {1}", list, ex3);
					base.Logger.Trace(TraceLevel.Error, text6);
					return base.BadRequest(text6);
				}
				httpActionResult = base.Ok();
			}
			return httpActionResult;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000057BB File Offset: 0x000039BB
		[HttpPatch]
		[HttpDelete]
		[HttpPut]
		[ODataRoute("PowerBIReports({Id})/DataModelParameters")]
		[ODataRoute("PowerBIReports(Path={Id})/DataModelParameters")]
		public IHttpActionResult NotAllowedDataModelParametersMethods(string Id)
		{
			return base.StatusCode(HttpStatusCode.MethodNotAllowed);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005908 File Offset: 0x00003B08
		private void UpdateParametersMapWithOnlyExistingParameters(IDictionary<string, DataModelParameter> currentParams, IList<DataModelParameter> updatedParams)
		{
			IList<string> list = new List<string>();
			foreach (DataModelParameter dataModelParameter in updatedParams)
			{
				if (!currentParams.ContainsKey(dataModelParameter.Name))
				{
					list.Add(dataModelParameter.Name);
				}
				else
				{
					currentParams[dataModelParameter.Name] = dataModelParameter;
				}
			}
			if (list.Count > 0)
			{
				string text = string.Format("Invalid UploadDataModelParameters payload: Cannot update parameters that did not already exist from upload. The following Parameter Names are not already present: {0}.", list);
				base.Logger.Trace(TraceLevel.Error, text);
				throw new InvalidDataModelParameterException(text);
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000059A4 File Offset: 0x00003BA4
		[HttpGet]
		[ODataRoute("PowerBIReports({Id})/AllowedActions")]
		[ODataRoute("PowerBIReports(Path={Id})/AllowedActions")]
		public override IHttpActionResult GetAllowedActions(string Id)
		{
			return base.GetAllowedActions(Id);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000059B0 File Offset: 0x00003BB0
		[HttpPost]
		public IHttpActionResult CheckDataSourceConnection(string key, ODataActionParameters actionParameters)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			if (actionParameters == null || !base.ModelState.IsValid || !actionParameters.ContainsKey("DataSourceName"))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			Guid guid2;
			if (!Guid.TryParse(actionParameters["DataSourceName"].ToString(), out guid2))
			{
				return base.NotFound();
			}
			DataSourceCheckResult dataSourceCheckResult;
			try
			{
				dataSourceCheckResult = this._powerBIReportsControllerHelper.CheckDataSourceConnection(guid, guid2);
			}
			catch (Exception ex)
			{
				if (ex is InvalidDataSourceCountException || ex is InvalidDataSourceTypeException)
				{
					return base.BadRequest(SR.InvalidDataSourceForTestConnectionPbi);
				}
				throw;
			}
			return this.Ok<DataSourceCheckResult>(dataSourceCheckResult);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005A68 File Offset: 0x00003C68
		[HttpGet]
		[ODataRoute("PowerBIReports({Id})/Comments")]
		[ODataRoute("PowerBIReports(Path={Id})/Comments")]
		public override IHttpActionResult GetComments(string Id)
		{
			return base.GetComments(Id);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005A71 File Offset: 0x00003C71
		[HttpPut]
		[ODataRoute("PowerBIReports({Id})/Comments({ItemID})")]
		[ODataRoute("PowerBIReports(Path={Id})/Comments({ItemID})")]
		public IHttpActionResult UpdateComment(string Id, string ItemId, Comment comment)
		{
			return base.UpdateComment(ItemId, comment);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005A7B File Offset: 0x00003C7B
		[HttpPatch]
		[ODataRoute("PowerBIReports({Id})/Comments({ItemId})")]
		[ODataRoute("PowerBIReports(Path={Id})/Comments({ItemId})")]
		public IHttpActionResult NotImplementedCommentsAction()
		{
			return base.StatusCode(HttpStatusCode.NotImplemented);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005A88 File Offset: 0x00003C88
		[HttpDelete]
		[ODataRoute("PowerBIReports({Id})/Comments({ItemId})")]
		[ODataRoute("PowerBIReports(Path={Id})/Comments({ItemId})")]
		public IHttpActionResult DeleteComment(string Id, string ItemId)
		{
			return base.DeleteComment(ItemId);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005A91 File Offset: 0x00003C91
		[HttpPost]
		[ODataRoute("PowerBIReports({Id})/Comments")]
		[ODataRoute("PowerBIReports(Path={Id})/Comments")]
		public override IHttpActionResult PostComment(string Id, Comment comment)
		{
			return base.PostComment(Id, comment);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005A9C File Offset: 0x00003C9C
		[HttpGet]
		[ODataRoute("PowerBIReports({Id})/DataModelRoles")]
		[ODataRoute("PowerBIReports(Path={Id})/DataModelRoles")]
		public async Task<IHttpActionResult> GetDataModelRoles(string Id)
		{
			IHttpActionResult httpActionResult;
			if (!this._systemService.IsBiServer())
			{
				httpActionResult = base.NotAllowed();
			}
			else
			{
				PowerBIReport entity = this.GetEntity(Id, null);
				IList<DataModelRole> list = await base.CatalogRepository.GetDataModelRolesAsync(base.User, entity.Id);
				httpActionResult = base.CreateOk(list);
			}
			return httpActionResult;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005AEC File Offset: 0x00003CEC
		[HttpGet]
		[ODataRoute("PowerBIReports({Id})/DataModelRoleAssignments")]
		[ODataRoute("PowerBIReports(Path={Id})/DataModelRoleAssignments")]
		public async Task<IHttpActionResult> GetDataModelRoleAssignments(string Id)
		{
			IHttpActionResult httpActionResult;
			if (!this._systemService.IsBiServer())
			{
				httpActionResult = base.NotAllowed();
			}
			else
			{
				PowerBIReport entity = this.GetEntity(Id, null);
				IList<DataModelRoleAssignment> list = await base.CatalogRepository.GetDataModelRoleAssignmentsAsync(base.User, entity.Id);
				httpActionResult = base.CreateOk(list);
			}
			return httpActionResult;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005B3C File Offset: 0x00003D3C
		[HttpPut]
		[ODataRoute("PowerBIReports({Id})/DataModelRoleAssignments")]
		[ODataRoute("PowerBIReports(Path={Id})/DataModelRoleAssignments")]
		public async Task<IHttpActionResult> PutDataModelRoleAssignments(string Id)
		{
			IHttpActionResult httpActionResult;
			if (!this._systemService.IsBiServer())
			{
				httpActionResult = base.NotAllowed();
			}
			else
			{
				PowerBIReport entity = this.GetEntity(Id, null);
				string result = base.Request.Content.ReadAsStringAsync().Result;
				List<DataModelRoleAssignment> list = new List<DataModelRoleAssignment>();
				try
				{
					JsonConvert.PopulateObject(result, list);
				}
				catch (Exception ex)
				{
					base.Logger.Trace(TraceLevel.Verbose, string.Format("Invalid payload: {0}", ex.Message));
					return base.StatusCode(HttpStatusCode.BadRequest);
				}
				await base.CatalogRepository.UpdateDataModelRoleAssignmentsAsync(base.User, entity.Id, list);
				httpActionResult = base.StatusCode(HttpStatusCode.OK);
			}
			return httpActionResult;
		}

		// Token: 0x0400005D RID: 93
		private readonly PowerBIReportsControllerHelper _powerBIReportsControllerHelper;

		// Token: 0x0400005E RID: 94
		private readonly ISystemService _systemService;

		// Token: 0x0400005F RID: 95
		private const string UploadPrefix = "PbixUpload_";

		// Token: 0x04000060 RID: 96
		private readonly BinaryUploader _uploader;
	}
}
