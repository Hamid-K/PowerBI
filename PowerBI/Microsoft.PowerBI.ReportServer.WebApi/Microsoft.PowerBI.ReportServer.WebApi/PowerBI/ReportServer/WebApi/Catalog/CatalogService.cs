using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.Contracts;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;
using Microsoft.PowerBI.ReportServer.WebApi.Pbix;
using Microsoft.ReportingServices.CatalogAccess;
using Microsoft.ReportingServices.Portal.ODataClient.V2;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.ReportServer.WebApi.Catalog
{
	// Token: 0x0200003F RID: 63
	internal sealed class CatalogService : ICatalogService
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00007292 File Offset: 0x00005492
		public CatalogService(IHttpService httpService, IPbixParser pbixParser, ICatalogItemAccessor catalogItemAccessor, IPbixComponentsBuilder pbixComponentsBuilder)
		{
			ContractExtensions.NotNull<IHttpService>(httpService, "httpService");
			ContractExtensions.NotNull<ICatalogItemAccessor>(catalogItemAccessor, "dataSources");
			this._httpService = httpService;
			this._pbixParser = pbixParser;
			this._catalogItemAccessor = catalogItemAccessor;
			this._pbixComponentsBuilder = pbixComponentsBuilder;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000072D0 File Offset: 0x000054D0
		public async Task<PbixComponents> GetPbixComponentsAsync(IPrincipal userPrincipal, Guid catalogItemId, string requestId, string sessionId, PbixReportComponents pbixReportComponents)
		{
			Stopwatch sw = Stopwatch.StartNew();
			Logger.Verbose("GetPbixComponentsAsync(Composite) shredding started for catalogItem={0}, duration={1} ms", new object[] { catalogItemId, sw.ElapsedMilliseconds });
			sw.Restart();
			PbixComponents components = null;
			Stream stream2 = await this.GetPowerBIReportDefinitionStream(catalogItemId);
			using (Stream stream = stream2)
			{
				int num2;
				int num = num2 - 1;
				try
				{
					PbixReportElements pbixReportElements = this._pbixParser.ParsePbixFileIntoParts(stream, requestId, sessionId);
					bool flag = false;
					components = await this._pbixComponentsBuilder.BuildPbixComponentsFromPbixReportElements(pbixReportElements, requestId, sessionId, flag);
					bool isExtendedContentAvailable = await this._catalogItemAccessor.IsExtendedContentAvailable(catalogItemId, ExtendedContentType.DataModel);
					if (pbixReportComponents == PbixReportComponents.PbixMetadataAndModel && isExtendedContentAvailable)
					{
						PbixComponents pbixComponents = components;
						pbixComponents.DataModel = await this.GetModelFromCatalog(catalogItemId);
						pbixComponents = null;
					}
					components.HasEmbeddedModels = isExtendedContentAvailable || (components.DataModel != null && components.DataModel.Length != 0);
					components.DatabaseType = (components.HasEmbeddedModels ? DataModelDataSourceType.Unknown : DataModelDataSourceType.Live);
				}
				catch (Exception ex)
				{
					Logger.Error(ex, "Exception occured in GetPbixComponentsAsync(Composite) during Shredding.", Array.Empty<object>());
					throw new CatalogAccessException("Exception occured in GetPbixComponentsAsync(Composite) during Shredding", CatalogAccessExceptionErrorCode.General);
				}
			}
			Stream stream = null;
			Logger.Verbose("GetPbixComponentsAsync(Composite) Shredding completed for catalogItem={0}, duration={1} ms", new object[] { catalogItemId, sw.ElapsedMilliseconds });
			return components;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00007338 File Offset: 0x00005538
		private async Task<Stream> GetPowerBIReportDefinitionStream(Guid catalogItemId)
		{
			Stream stream2;
			using (Stream stream = this._catalogItemAccessor.GetExtendedContentReadable(catalogItemId, ExtendedContentType.PowerBIReportDefinition))
			{
				byte[] definitionBytes = new byte[stream.Length];
				await stream.ReadAsync(definitionBytes, 0, definitionBytes.Length);
				stream2 = new MemoryStream(definitionBytes);
			}
			return stream2;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00007388 File Offset: 0x00005588
		private async Task<byte[]> GetModelFromCatalog(Guid catalogItemId)
		{
			byte[] array;
			using (Stream stream = this._catalogItemAccessor.GetExtendedContentReadable(catalogItemId, ExtendedContentType.DataModel))
			{
				byte[] modelBytes = new byte[stream.Length];
				await stream.ReadAsync(modelBytes, 0, modelBytes.Length);
				array = modelBytes;
			}
			return array;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000073D8 File Offset: 0x000055D8
		public async Task<PowerBIReport> GetPbixReportMetadataAsync(IPrincipal userPrincipal, Guid catalogItemId)
		{
			HttpResponseMessage httpResponseMessage = await this.GetPowerBIItemMetadataAsync(userPrincipal, catalogItemId);
			if (!httpResponseMessage.IsSuccessStatusCode)
			{
				Logger.Error("GetPbixReportMetadataAsync RSPortal returned error code {0} for catalogItem {1}. Check Portal logs.", new object[] { httpResponseMessage.StatusCode, catalogItemId });
				throw new CatalogAccessException(httpResponseMessage.ReasonPhrase, CatalogAccessExceptionErrorCode.CannotRetrievePBIX, httpResponseMessage.StatusCode);
			}
			return JObject.Parse(await httpResponseMessage.Content.ReadAsStringAsync()).ToObject<PowerBIReport>();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007430 File Offset: 0x00005630
		private async Task<HttpResponseMessage> GetPowerBIItemMetadataAsync(IPrincipal userPrincipal, Guid catalogItemId)
		{
			return await this._httpService.InvokeApi(userPrincipal, this.GetPowerBIItemMetadataUrl(catalogItemId));
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007485 File Offset: 0x00005685
		private Uri GetPowerBIItemMetadataUrl(Guid catalogItemId)
		{
			return new Uri(string.Format("{0}/api/v2.0/CatalogItems({1})?$expand=Properties", HostingState.Current.PortalUrl, catalogItemId));
		}

		// Token: 0x040000B9 RID: 185
		private readonly IHttpService _httpService;

		// Token: 0x040000BA RID: 186
		private readonly IPbixParser _pbixParser;

		// Token: 0x040000BB RID: 187
		private readonly ICatalogItemAccessor _catalogItemAccessor;

		// Token: 0x040000BC RID: 188
		private readonly IPbixComponentsBuilder _pbixComponentsBuilder;
	}
}
