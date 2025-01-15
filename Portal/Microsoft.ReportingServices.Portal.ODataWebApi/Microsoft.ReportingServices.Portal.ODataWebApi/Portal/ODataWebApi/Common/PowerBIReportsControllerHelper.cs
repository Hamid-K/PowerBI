using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNet.OData;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;
using Microsoft.ReportingServices.Portal.Interfaces.Exceptions;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.ODataWebApi.Utils;
using Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Common
{
	// Token: 0x02000045 RID: 69
	internal sealed class PowerBIReportsControllerHelper
	{
		// Token: 0x06000336 RID: 822 RVA: 0x0000E230 File Offset: 0x0000C430
		internal PowerBIReportsControllerHelper(ICatalogRepository catalogRepository, ODataController catalogItemController, IPortalConfigurationManager portalConfigurationManager, ILogger logger)
		{
			this._catalogRepository = catalogRepository;
			this._catalogItemController = catalogItemController;
			this._logger = logger;
			this._portalConfigurationManager = portalConfigurationManager;
			this._pbixReportHelper = new PbixReportHelper();
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000E260 File Offset: 0x0000C460
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000E268 File Offset: 0x0000C468
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

		// Token: 0x06000339 RID: 825 RVA: 0x0000E274 File Offset: 0x0000C474
		internal void ValidatePbiReportRenderingIsSupportedAndSetProperties(PowerBIReport item)
		{
			try
			{
				IsRenderingSupportedResult isRenderingSupportedResult = this.PbixReportHelper.ValidateRenderingIsSupportedAndSetProperties(item, this._portalConfigurationManager.Current.PowerBIUrl, this._logger, this._catalogItemController.User, this._portalConfigurationManager.Current.ReportServerHostName);
				if (!isRenderingSupportedResult.IsSupported)
				{
					throw new PowerBIReportNotSupportedException(isRenderingSupportedResult.ErrorMessage, isRenderingSupportedResult.ErrorCode);
				}
			}
			catch (Exception ex)
			{
				this._logger.Trace(TraceLevel.Error, ex.Message);
				if (ex is PowerBIReportNotSupportedException)
				{
					throw;
				}
				throw new PowerBIReportNotSupportedException(SR.ErrorPbixUpload, ErrorCode.InvalidContent, ex);
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000E318 File Offset: 0x0000C518
		internal IList<DataSource> UpdateDataModelParametersInAS(Guid id, IList<DataModelParameter> parameters)
		{
			IList<DataSource> list;
			try
			{
				list = this.PbixReportHelper.UpdateDataModelParametersInPowerBI(id, parameters, this._portalConfigurationManager.Current.PowerBIUrl, this._logger, this._catalogItemController.User, this._portalConfigurationManager.Current.ReportServerHostName);
			}
			catch (Exception ex)
			{
				this._logger.Trace(TraceLevel.Error, ex.Message);
				throw new InvalidDataModelParameterException(SR.ErrorPbixUpload, ex);
			}
			return list;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000E398 File Offset: 0x0000C598
		internal void PbiParse(PowerBIReport item, PreShreddedPbixFiles files)
		{
			try
			{
				IsRenderingSupportedResult isRenderingSupportedResult = this.PbixReportHelper.PbiParse(item, files, this._portalConfigurationManager.Current.PowerBIUrl, this._logger, this._catalogItemController.User, this._portalConfigurationManager.Current.ReportServerHostName);
				if (!isRenderingSupportedResult.IsSupported)
				{
					throw new PowerBIReportNotSupportedException(isRenderingSupportedResult.ErrorMessage, isRenderingSupportedResult.ErrorCode);
				}
			}
			catch (Exception ex)
			{
				this._logger.Trace(TraceLevel.Error, ex.Message);
				if (ex is PowerBIReportNotSupportedException)
				{
					throw;
				}
				throw new PowerBIReportNotSupportedException(SR.ErrorPbixUpload, ErrorCode.InvalidContent, ex);
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000E43C File Offset: 0x0000C63C
		internal DataSourceCheckResult CheckDataSourceConnection(Guid key, Guid dataSourceId)
		{
			DataSource dataSourceForTestConnection = this._catalogRepository.GetDataSourceForTestConnection(this._catalogItemController.User, key, dataSourceId);
			if (this._pbixReportHelper.CanBeTestedByMashup(dataSourceForTestConnection))
			{
				return this._pbixReportHelper.TestDataSource(this._portalConfigurationManager.Current.PowerBIUrl, this._logger, this._catalogItemController.User, dataSourceForTestConnection, this._portalConfigurationManager.Current.ReportServerHostName);
			}
			return this._catalogRepository.TestDataSource(this._catalogItemController.User, dataSourceForTestConnection);
		}

		// Token: 0x040000CE RID: 206
		private ICatalogRepository _catalogRepository;

		// Token: 0x040000CF RID: 207
		private ODataController _catalogItemController;

		// Token: 0x040000D0 RID: 208
		private readonly ILogger _logger;

		// Token: 0x040000D1 RID: 209
		private readonly IPortalConfigurationManager _portalConfigurationManager;

		// Token: 0x040000D2 RID: 210
		private IPbixReportHelper _pbixReportHelper;
	}
}
