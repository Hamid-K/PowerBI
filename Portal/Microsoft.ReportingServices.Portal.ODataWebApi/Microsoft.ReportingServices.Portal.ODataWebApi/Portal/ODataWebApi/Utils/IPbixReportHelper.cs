using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Utils
{
	// Token: 0x0200003A RID: 58
	internal interface IPbixReportHelper
	{
		// Token: 0x060002DC RID: 732
		bool ShouldReShred(PowerBIReport entity, Uri basePortalUrl, ILogger logger, IPrincipal userPrincipal, string reportServerHostName);

		// Token: 0x060002DD RID: 733
		IsRenderingSupportedResult ValidateRenderingIsSupportedAndSetProperties(PowerBIReport entity, string powerBiUri, ILogger logger, IPrincipal userPrincipal, string reportServerHostName);

		// Token: 0x060002DE RID: 734
		DataSourceCheckResult TestDataSource(string powerBiUri, ILogger logger, IPrincipal userPrincipal, DataSource dataSource, string reportServerHostName);

		// Token: 0x060002DF RID: 735
		bool CanBeTestedByMashup(DataSource dataSource);

		// Token: 0x060002E0 RID: 736
		IsRenderingSupportedResult PbiParse(PowerBIReport entity, PreShreddedPbixFiles files, string powerBiUri, ILogger logger, IPrincipal userPrincipal, string reportServerHostName);

		// Token: 0x060002E1 RID: 737
		IList<DataSource> UpdateDataModelParametersInPowerBI(Guid catalogId, IList<DataModelParameter> dataModelParameters, string powerBiUri, ILogger logger, IPrincipal userPrincipal, string reportServerHostName);
	}
}
