using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Filters;
using System.Web.Services.Protocols;
using Microsoft.BIServer.Owin.Common.Enums;
using Microsoft.OData;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;
using Microsoft.ReportingServices.Portal.Interfaces.Exceptions;
using Microsoft.ReportingServices.Portal.ODataWebApi.V1.Filters;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Filters
{
	// Token: 0x0200000D RID: 13
	public class PortalExceptionFilter : PortalExceptionFilter
	{
		// Token: 0x06000029 RID: 41 RVA: 0x0000373C File Offset: 0x0000193C
		public override void OnException(HttpActionExecutedContext context)
		{
			Exception exception = context.Exception;
			if (exception is PowerBIReportNotSupportedException)
			{
				ODataError odataError = new ODataError
				{
					ErrorCode = ((PowerBIReportNotSupportedException)exception).ErrorCode.ToString("D"),
					Message = exception.Message
				};
				context.Response = PortalExceptionFilter.CreateErrorResponse(odataError, context, (HttpStatusCode)422, base.Logger);
				return;
			}
			if (exception is KeyNotFoundException)
			{
				ODataError odataError2 = new ODataError
				{
					ErrorCode = Microsoft.BIServer.Owin.Common.Enums.ErrorCode.NotFound.ToString("D"),
					Message = exception.Message
				};
				context.Response = PortalExceptionFilter.CreateErrorResponse(odataError2, context, HttpStatusCode.NotFound, base.Logger);
				return;
			}
			if (exception is PowerBIMigrateInvalidUrlException)
			{
				ODataError odataError3 = new ODataError
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.PowerBIMigrateInvalidUrlException.ToString("D"),
					Message = exception.Message
				};
				context.Response = PortalExceptionFilter.CreateErrorResponse(odataError3, context, HttpStatusCode.BadRequest, base.Logger);
				return;
			}
			base.OnException(context);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003840 File Offset: 0x00001A40
		internal new static string GetSoapDetailDataName(Microsoft.ReportingServices.Diagnostics.Utilities.ErrorCode code)
		{
			return PortalExceptionFilter.GetSoapDetailDataName(code);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003848 File Offset: 0x00001A48
		internal new static void AllowSoapDetailException(SoapException exception, Microsoft.ReportingServices.Diagnostics.Utilities.ErrorCode code)
		{
			PortalExceptionFilter.AllowSoapDetailException(exception, code);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003851 File Offset: 0x00001A51
		internal new static void GetRsLibDetailsFromSoapException(SoapException soapException, ODataError error, ref HttpStatusCode statusCode)
		{
			PortalExceptionFilter.GetRsLibDetailsFromSoapException(soapException, error, ref statusCode);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000385B File Offset: 0x00001A5B
		internal new static void OverrideErrorDetails(Exception e, ref HttpStatusCode statusCode, ref ODataError odataError)
		{
			PortalExceptionFilter.OverrideErrorDetails(e, ref statusCode, ref odataError);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003865 File Offset: 0x00001A65
		internal new static void AddSystemResourcePackageExceptionErrorDetails(Microsoft.ReportingServices.Diagnostics.Utilities.SystemResourcePackageException packageException, ref ODataError odataError)
		{
			PortalExceptionFilter.AddSystemResourcePackageExceptionErrorDetails(packageException, ref odataError);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000386E File Offset: 0x00001A6E
		internal new static void AddDataSetProcessingExceptionErrorDetails(Exception dataSetProcessingException, ref ODataError odataError)
		{
			PortalExceptionFilter.AddDataSetProcessingExceptionErrorDetails(dataSetProcessingException, ref odataError);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003877 File Offset: 0x00001A77
		private static void AddODataErrorDetail(ODataErrorDetail odataErrorDetail, ref ODataError odataError)
		{
			odataError.Details = odataError.Details ?? new List<ODataErrorDetail>();
			odataError.Details.Add(odataErrorDetail);
		}
	}
}
