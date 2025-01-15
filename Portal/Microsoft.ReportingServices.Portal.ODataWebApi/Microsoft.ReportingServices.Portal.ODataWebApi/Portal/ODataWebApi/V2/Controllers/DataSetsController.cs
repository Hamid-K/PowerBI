using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Routing;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x0200001D RID: 29
	public class DataSetsController : CatalogItemController<DataSet>
	{
		// Token: 0x06000165 RID: 357 RVA: 0x000063B0 File Offset: 0x000045B0
		public DataSetsController(ICatalogRepository catalogRepository, IDataService dataService, ISystemService systemService, IPortalConfigurationManager portalConfigurationManager, ILogger logger)
			: base(catalogRepository, dataService, systemService, portalConfigurationManager, logger)
		{
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000063C0 File Offset: 0x000045C0
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			CatalogItemController<DataSet>.RegisterModel(builder, "DataSets");
			builder.EntityType<DataSet>().Function("GetSchema").Returns<DataSetSchema>();
			ActionConfiguration actionConfiguration = builder.EntityType<DataSet>().Action("GetData").Returns<string>();
			actionConfiguration.CollectionParameter<DataSetParameter>("Parameters");
			actionConfiguration.Parameter<int?>("maxRows");
			ActionConfiguration actionConfiguration2 = builder.EntityType<DataSet>().Action("GetKpiTrendsetData").Returns<string>();
			actionConfiguration2.CollectionParameter<DataSetParameter>("Parameters");
			actionConfiguration2.Parameter<string>("columnName");
			builder.EntityType<DataSet>().Action("GetAggregatedValue").Returns<string>()
				.CollectionParameter<DataSetParameter>("Parameters");
			builder.EntitySet<DataSetRow>("DataSetData");
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006474 File Offset: 0x00004674
		protected override DataSet GetEntity(string Id, string castName)
		{
			string text;
			bool flag = base.CatalogItemControllerHelper.IsRequestByPath(Id, out text);
			Guid guid = default(Guid);
			if (!flag && !Guid.TryParse(Id, out guid))
			{
				return null;
			}
			if (!flag)
			{
				return base.CatalogRepository.GetDataSet(base.User, guid);
			}
			return base.CatalogRepository.GetDataSet(base.User, text);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000064D0 File Offset: 0x000046D0
		[HttpPatch]
		[ODataRoute("DataSets({Id})/ParameterDefinitions")]
		[ODataRoute("DataSets(Path={Id})/ParameterDefinitions")]
		public IHttpActionResult PatchParameterDefinitions(string Id)
		{
			DataSet entityByGuid = this.GetEntityByGuid(Id);
			if (entityByGuid == null)
			{
				base.Logger.Trace(TraceLevel.Verbose, string.Format("Unknown guid: {0}", Id));
				return base.StatusCode(HttpStatusCode.BadRequest);
			}
			return this.PatchParameterDefinitionsByPath(entityByGuid.Path);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006518 File Offset: 0x00004718
		private IHttpActionResult PatchParameterDefinitionsByPath(string path)
		{
			string result = base.Request.Content.ReadAsStringAsync().Result;
			List<ReportParameterDefinitionPatch> list = new List<ReportParameterDefinitionPatch>();
			try
			{
				JsonConvert.PopulateObject(result, list);
			}
			catch (Exception ex)
			{
				base.Logger.Trace(TraceLevel.Verbose, string.Format("Invalid payload: {0}", ex.Message));
				return base.StatusCode(HttpStatusCode.BadRequest);
			}
			base.CatalogRepository.UpdateReportParameterDefinition(base.User, path, list);
			return base.StatusCode(HttpStatusCode.NoContent);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000065A8 File Offset: 0x000047A8
		[HttpPost]
		[ODataRoute("DataSets({Id})/Model.Upload")]
		[ODataRoute("DataSets(Path={Id})/Model.Upload")]
		public async Task<IHttpActionResult> UploadStream(string Id)
		{
			return await base.UploadStreamInternal(Id);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000065F5 File Offset: 0x000047F5
		[HttpGet]
		[ODataRoute("DataSets({Id})/Policies")]
		[ODataRoute("DataSets(Path={Id})/Policies")]
		public override IHttpActionResult GetPolicies(string Id)
		{
			return base.GetPolicies(Id);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000065FE File Offset: 0x000047FE
		[HttpPut]
		[ODataRoute("DataSets({Id})/Policies")]
		[ODataRoute("DataSets(Path={Id})/Policies")]
		public override IHttpActionResult PutPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PutPolicies(Id, policies);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006608 File Offset: 0x00004808
		[HttpPatch]
		[ODataRoute("DataSets({Id})/Policies")]
		[ODataRoute("DataSets(Path={Id})/Policies")]
		public override IHttpActionResult PatchPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PatchPolicies(Id, policies);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006612 File Offset: 0x00004812
		[HttpPost]
		[ODataRoute("DataSets({Id})/Policies")]
		[ODataRoute("DataSets(Path={Id})/Policies")]
		public override IHttpActionResult PostPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PostPolicies(Id, policies);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000661C File Offset: 0x0000481C
		[HttpDelete]
		[ODataRoute("DataSets({Id})/Policies")]
		[ODataRoute("DataSets(Path={Id})/Policies")]
		public override IHttpActionResult DeletePolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.DeletePolicies(Id, policies);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006626 File Offset: 0x00004826
		[HttpPost]
		[HttpDelete]
		[HttpPatch]
		[ODataRoute("DataSets({Id})/Properties")]
		[ODataRoute("DataSets(Path={Id})/Properties")]
		public IHttpActionResult NotAllowedPropetiesMethods(string Id)
		{
			return base.NotAllowed();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000662E File Offset: 0x0000482E
		[HttpGet]
		[ODataRoute("DataSets({Id})/Properties")]
		[ODataRoute("DataSets(Path={Id})/Properties")]
		public override IHttpActionResult GetCatalogItemProperties(string Id, [FromUri] string properties)
		{
			return base.GetCatalogItemProperties(Id, properties);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006638 File Offset: 0x00004838
		[HttpPut]
		[ODataRoute("DataSets({Id})/Properties")]
		[ODataRoute("DataSets(Path={Id})/Properties")]
		public override IHttpActionResult PutCatalogItemProperties(string Id)
		{
			return base.PutCatalogItemProperties(Id);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006641 File Offset: 0x00004841
		[HttpPut]
		[ODataRoute("DataSets({Id})/DataSources({dataSourceId})")]
		[ODataRoute("DataSets(Path={Id})/DataSources({dataSourceId})")]
		public IHttpActionResult PutItemDataSources(string Id, string dataSourceId, [FromBody] DataSource dataSource)
		{
			return base.PutOnItemDataSources(Id, dataSourceId, dataSource);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000664C File Offset: 0x0000484C
		[HttpGet]
		[ODataRoute("DataSets({Id})/DataSources({dataSourceId})")]
		[ODataRoute("DataSets(Path={Id})/DataSources({dataSourceId})")]
		public IHttpActionResult GetItemDataSources(string Id, string dataSourceId)
		{
			return base.GetOnItemDataSources(Id, dataSourceId);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006656 File Offset: 0x00004856
		[HttpPut]
		[ODataRoute("DataSets({Id})/DataSources")]
		[ODataRoute("DataSets(Path={Id})/DataSources")]
		public IHttpActionResult PutDataSources(string Id)
		{
			return base.PutOnDataSourcesCollection(Id, base.Request);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006665 File Offset: 0x00004865
		[HttpPost]
		[ODataRoute("Reports({Id})/DataSources")]
		[ODataRoute("Reports(Path={Id})/DataSources")]
		public IHttpActionResult PostDataSources(string Id)
		{
			return base.StatusCode(HttpStatusCode.NotImplemented);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006665 File Offset: 0x00004865
		[HttpPatch]
		[ODataRoute("DataSets({Id})/DataSources")]
		[ODataRoute("DataSets(Path={Id})/DataSources")]
		public IHttpActionResult PatchDataSources(string Id)
		{
			return base.StatusCode(HttpStatusCode.NotImplemented);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006672 File Offset: 0x00004872
		[HttpGet]
		[ODataRoute("DataSets({Id})/AllowedActions")]
		[ODataRoute("DataSets(Path={Id})/AllowedActions")]
		public override IHttpActionResult GetAllowedActions(string Id)
		{
			return base.GetAllowedActions(Id);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000667B File Offset: 0x0000487B
		[HttpGet]
		[ODataRoute("DataSets({Id})/CacheOptions")]
		[ODataRoute("DataSets(Path={Id})/CacheOptions")]
		public override IHttpActionResult GetCacheOptions(string Id)
		{
			return base.GetCacheOptions(Id);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006684 File Offset: 0x00004884
		[HttpPut]
		[ODataRoute("DataSets({Id})/CacheOptions")]
		[ODataRoute("DataSets(Path={Id})/CacheOptions")]
		public override IHttpActionResult SetCacheOptions(string Id, [FromBody] CacheOptions cacheOptions)
		{
			return base.SetCacheOptions(Id, cacheOptions);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00006690 File Offset: 0x00004890
		[HttpPost]
		public IHttpActionResult GetAggregatedValue(string key, ODataPath path, ODataActionParameters actionParameters, [FromODataUri] string columnName, [FromODataUri] KpiSharedDataItemAggregation aggregation)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			if (string.IsNullOrEmpty(columnName))
			{
				return base.BadRequest(SR.PropertyNullException);
			}
			if (!Enum.IsDefined(typeof(KpiSharedDataItemAggregation), aggregation) || aggregation == KpiSharedDataItemAggregation.None)
			{
				return base.BadRequest(SR.InvalidAggregation);
			}
			actionParameters = actionParameters ?? new ODataActionParameters();
			IEnumerable<DataSetParameter> enumerable2;
			if (!actionParameters.ContainsKey("Parameters"))
			{
				IEnumerable<DataSetParameter> enumerable = new DataSetParameter[0];
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = actionParameters["Parameters"] as IEnumerable<DataSetParameter>;
			}
			IEnumerable<DataSetParameter> enumerable3 = enumerable2;
			string dataSetAggregatedValuesJson = base.DataService.GetDataSetAggregatedValuesJson(base.User, guid, enumerable3, columnName, aggregation);
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(dataSetAggregatedValuesJson)
			};
			httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			return base.ResponseMessage(httpResponseMessage);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006778 File Offset: 0x00004978
		private IHttpActionResult GetData(string key, ODataActionParameters actionParameters, int? maxRows)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK
			};
			actionParameters = actionParameters ?? new ODataActionParameters();
			IEnumerable<DataSetParameter> enumerable2;
			if (!actionParameters.ContainsKey("Parameters"))
			{
				IEnumerable<DataSetParameter> enumerable = new DataSetParameter[0];
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = actionParameters["Parameters"] as IEnumerable<DataSetParameter>;
			}
			IEnumerable<DataSetParameter> enumerable3 = enumerable2;
			bool flag;
			if (base.Request.GetOwinContext().Get<bool>("CanCompress"))
			{
				byte[] compressedDataSetTableJson = base.DataService.GetCompressedDataSetTableJson(base.User, guid, enumerable3, maxRows, out flag);
				httpResponseMessage.Content = new ByteArrayContent(compressedDataSetTableJson);
				httpResponseMessage.Content.Headers.ContentEncoding.Add("gzip");
			}
			else
			{
				string dataSetTableJson = base.DataService.GetDataSetTableJson(base.User, guid, enumerable3, maxRows, out flag);
				httpResponseMessage.Content = new StringContent(dataSetTableJson);
			}
			httpResponseMessage.Headers.Add("DataSetJsonCached", flag.ToString().ToLowerInvariant());
			httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			return base.ResponseMessage(httpResponseMessage);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006898 File Offset: 0x00004A98
		[HttpGet]
		[ODataRoute("DataSets({Id})/Data")]
		[ODataRoute("DataSets(Path={Id})/Data")]
		public IHttpActionResult GetDataSetData(string Id)
		{
			string empty = string.Empty;
			Guid id;
			if (base.CatalogItemControllerHelper.IsRequestByPath(Id, out empty))
			{
				CatalogItem catalogItem = base.CatalogRepository.GetCatalogItem(base.User, empty);
				if (catalogItem == null)
				{
					return base.NotFound();
				}
				id = catalogItem.Id;
			}
			else if (!Guid.TryParse(Id, out id))
			{
				return base.NotFound();
			}
			List<KeyValuePair<string, string>> list = base.Request.GetQueryNameValuePairs().ToList<KeyValuePair<string, string>>();
			List<DataSetParameter> dataSetParameters = new List<DataSetParameter>();
			if (list.Count<KeyValuePair<string, string>>() > 0)
			{
				list.ForEach(delegate(KeyValuePair<string, string> keyValuePair)
				{
					dataSetParameters.Add(new DataSetParameter
					{
						Name = keyValuePair.Key,
						Value = keyValuePair.Value
					});
				});
			}
			bool flag = false;
			JToken jtoken = JToken.Parse(base.DataService.GetDataSetTableJson(base.User, id, dataSetParameters, null, out flag));
			List<DataSetRow> list2 = new List<DataSetRow>();
			if (jtoken["Rows"] != null && jtoken["Rows"].Count<JToken>() > 0)
			{
				JToken jtoken2 = jtoken["Columns"];
				foreach (JToken jtoken3 in ((IEnumerable<JToken>)jtoken["Rows"]))
				{
					DataSetRow dataSetRow = new DataSetRow
					{
						Id = Guid.NewGuid()
					};
					for (int i = 0; i < jtoken2.Count<JToken>(); i++)
					{
						dataSetRow.Properties.Add(jtoken2[i]["Name"].ToString(), jtoken3[i].ToString());
					}
					list2.Add(dataSetRow);
				}
			}
			return base.CreateOk(list2);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006A58 File Offset: 0x00004C58
		[HttpPost]
		public IHttpActionResult GetData(string key, ODataPath path, ODataActionParameters actionParameters)
		{
			return this.GetData(key, actionParameters, null);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006A76 File Offset: 0x00004C76
		[HttpPost]
		public IHttpActionResult GetData(string key, ODataPath path, ODataActionParameters actionParameters, [FromODataUri] int? maxRows)
		{
			return this.GetData(key, actionParameters, maxRows);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006A84 File Offset: 0x00004C84
		[HttpPost]
		public IHttpActionResult GetKpiTrendsetData(string key, ODataPath path, ODataActionParameters actionParameters, [FromODataUri] string columnName)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			if (string.IsNullOrEmpty(columnName))
			{
				return base.BadRequest(SR.PropertyNullException);
			}
			actionParameters = actionParameters ?? new ODataActionParameters();
			IEnumerable<DataSetParameter> enumerable2;
			if (!actionParameters.ContainsKey("Parameters"))
			{
				IEnumerable<DataSetParameter> enumerable = new DataSetParameter[0];
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = actionParameters["Parameters"] as IEnumerable<DataSetParameter>;
			}
			IEnumerable<DataSetParameter> enumerable3 = enumerable2;
			string dataSetColumnJson = base.DataService.GetDataSetColumnJson(base.User, guid, enumerable3, columnName, 30);
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(dataSetColumnJson)
			};
			httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			return base.ResponseMessage(httpResponseMessage);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006B44 File Offset: 0x00004D44
		public IHttpActionResult GetSchema(string key)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			DataSetSchema dataSetSchema = base.DataService.GetDataSetSchema(base.User, guid);
			return base.CreateOk(dataSetSchema);
		}
	}
}
