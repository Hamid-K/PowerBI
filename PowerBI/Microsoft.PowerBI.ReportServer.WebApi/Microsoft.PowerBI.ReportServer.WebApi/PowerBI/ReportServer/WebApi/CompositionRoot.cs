using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.PowerBI.ReportServer.AsServer;
using Microsoft.PowerBI.ReportServer.AsServer.Artifacts;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;
using Microsoft.PowerBI.ReportServer.WebApi.Catalog;
using Microsoft.PowerBI.ReportServer.WebApi.Error;
using Microsoft.PowerBI.ReportServer.WebApi.PbiApi;
using Microsoft.PowerBI.ReportServer.WebApi.Pbix;
using Microsoft.ReportingServices.CatalogAccess;

namespace Microsoft.PowerBI.ReportServer.WebApi
{
	// Token: 0x0200000B RID: 11
	public sealed class CompositionRoot : IHttpControllerActivator
	{
		// Token: 0x0600002F RID: 47 RVA: 0x0000299A File Offset: 0x00000B9A
		public CompositionRoot(AnalysisServicesServer analysisServicesServer)
		{
			this._analysisServicesServer = analysisServicesServer;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000029AC File Offset: 0x00000BAC
		public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
		{
			if (controllerType == typeof(PbiApiController))
			{
				IHttpService instance = HttpService.Instance;
				CatalogItemAccessor catalogItemAccessor = new CatalogItemAccessor();
				RSExploreHostFactory rsexploreHostFactory = new RSExploreHostFactory();
				DataModelArtifactsProvider dataModelArtifactsProvider = new DataModelArtifactsProvider(this._analysisServicesServer);
				CatalogDataModelDataSourceAccessor catalogDataModelDataSourceAccessor = new CatalogDataModelDataSourceAccessor();
				CatalogDataModelRoleAccessor catalogDataModelRoleAccessor = new CatalogDataModelRoleAccessor();
				CatalogItemAccessor catalogItemAccessor2 = new CatalogItemAccessor();
				PowerBIConfiguration powerBIConfiguration = new PowerBIConfiguration(new ConfigurationInfoDataAccessor());
				CheckConnectionServices checkConnectionServices = new CheckConnectionServices();
				IPbixParser instance2 = PbixParserV1.GetInstance(HostingState.Current.IsConfigSwitchEnabled(ConfigSwitches.EnableCustomVisuals, false));
				IPbixComponentsBuilder instance3 = PbixComponentsBuilder.GetInstance(dataModelArtifactsProvider);
				IPbixTelemetryLogger instance4 = PbixTelemetryLogger.GetInstance();
				return new PbiApiController(new CatalogService(instance, instance2, catalogItemAccessor, instance3), rsexploreHostFactory, new RSErrorExtractor(), this._analysisServicesServer, instance2, instance3, dataModelArtifactsProvider, catalogDataModelDataSourceAccessor, catalogDataModelRoleAccessor, checkConnectionServices, catalogItemAccessor2, powerBIConfiguration, new CatalogDataAccessor(), instance4);
			}
			return null;
		}

		// Token: 0x04000038 RID: 56
		private readonly AnalysisServicesServer _analysisServicesServer;
	}
}
