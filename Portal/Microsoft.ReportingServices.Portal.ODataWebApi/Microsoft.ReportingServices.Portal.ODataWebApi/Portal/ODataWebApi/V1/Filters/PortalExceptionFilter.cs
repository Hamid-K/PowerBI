using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.XPath;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.OData;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;
using Microsoft.ReportingServices.Portal.Interfaces.Exceptions;
using Microsoft.ReportingServices.Portal.ODataWebApi.Utils;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V1.Filters
{
	// Token: 0x02000029 RID: 41
	public class PortalExceptionFilter : ExceptionFilterAttribute
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00007B78 File Offset: 0x00005D78
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00007B80 File Offset: 0x00005D80
		public ILogger Logger { get; set; }

		// Token: 0x060001FC RID: 508 RVA: 0x00007B8C File Offset: 0x00005D8C
		public override void OnException(HttpActionExecutedContext context)
		{
			Exception exception = context.Exception;
			HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
			ODataError odataError = null;
			if (exception is PortalException)
			{
				PortalException ex = exception as PortalException;
				odataError = new ODataError
				{
					ErrorCode = ex.ErrCode.ToString("D"),
					Message = ex.Message
				};
			}
			else if (exception is SoapException)
			{
				SoapException ex2 = exception as SoapException;
				Microsoft.ReportingServices.Diagnostics.Utilities.ErrorCode errorCode = Microsoft.ReportingServices.Diagnostics.Utilities.ErrorCode.rsSuccess;
				string empty = string.Empty;
				odataError = new ODataError
				{
					ErrorCode = (errorCode + 1000).ToString("D"),
					Message = empty
				};
				PortalExceptionFilter.GetRsLibDetailsFromSoapException(ex2, odataError, ref httpStatusCode);
			}
			else if (exception is InvalidDataFormatException)
			{
				InvalidDataFormatException ex3 = exception as InvalidDataFormatException;
				odataError = new ODataError
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.InvalidDataFormat.ToString("D"),
					Message = ex3.Message
				};
			}
			else if (exception is Microsoft.ReportingServices.Diagnostics.Utilities.SystemResourcePackageException)
			{
				Microsoft.ReportingServices.Diagnostics.Utilities.SystemResourcePackageException ex4 = exception as Microsoft.ReportingServices.Diagnostics.Utilities.SystemResourcePackageException;
				odataError = new ODataError
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.SystemResourcePackageException.ToString("D"),
					Message = SR.SystemResourcePackageException
				};
				PortalExceptionFilter.AddSystemResourcePackageExceptionErrorDetails(ex4, ref odataError);
			}
			else if (exception is Microsoft.ReportingServices.Portal.Interfaces.Exceptions.SystemResourcePackageException)
			{
				odataError = new ODataError
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.SystemResourcePackageException.ToString("D"),
					Message = SR.SystemResourcePackageException
				};
			}
			else if (exception is SystemResourceProcessingException)
			{
				odataError = new ODataError
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.SystemResourceProcessingException.ToString("D"),
					Message = SR.SystemResourceProcessingException
				};
			}
			else if (exception is DataSetParameterValueNotSetException || exception is DataSetRenderingSoapException)
			{
				httpStatusCode = (HttpStatusCode)422;
				odataError = new ODataError
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.DataSetProcessingException.ToString("D"),
					Message = SR.DataSetProcessingException
				};
				PortalExceptionFilter.AddDataSetProcessingExceptionErrorDetails(exception, ref odataError);
			}
			else if (exception is RSException)
			{
				RSException ex5 = exception as RSException;
				odataError = new ODataError
				{
					ErrorCode = (ex5.Code + 1000).ToString("D"),
					Message = ex5.Message
				};
			}
			else if (exception is NotSupportedException)
			{
				odataError = new ODataError
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.NotSupportedException.ToString("D"),
					Message = SR.NotSupportedException
				};
			}
			else if (exception is NotImplementedException)
			{
				httpStatusCode = HttpStatusCode.MethodNotAllowed;
			}
			else if (exception is ExcelWorkbookWopiInvalidUrlException)
			{
				ExcelWorkbookWopiInvalidUrlException ex6 = exception as ExcelWorkbookWopiInvalidUrlException;
				httpStatusCode = HttpStatusCode.BadRequest;
				odataError = new ODataError
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.ExcelWorkbookWopiInvalidUrlException.ToString("D"),
					Message = ex6.Message
				};
				if (exception.InnerException != null)
				{
					odataError.InnerError = new ODataInnerError(exception.InnerException);
				}
			}
			else if (exception is UploadFileCanceledException)
			{
				httpStatusCode = HttpStatusCode.Gone;
				if (this.Logger != null)
				{
					this.Logger.Trace(TraceLevel.Verbose, SR.UploadFileCanceled);
				}
			}
			context.Response = PortalExceptionFilter.CreateErrorResponse(odataError, context, httpStatusCode, this.Logger);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00007EA8 File Offset: 0x000060A8
		protected static HttpResponseMessage CreateErrorResponse(ODataError odataError, HttpActionExecutedContext context, HttpStatusCode httpStatusCode, ILogger logger)
		{
			if (odataError == null)
			{
				odataError = new ODataError
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.Unknown.ToString("D")
				};
			}
			PortalExceptionFilter.OverrideErrorDetails(context.Exception, ref httpStatusCode, ref odataError);
			if (logger != null)
			{
				TraceLevel traceLevel = ((httpStatusCode >= HttpStatusCode.InternalServerError) ? TraceLevel.Error : TraceLevel.Verbose);
				logger.Trace(traceLevel, string.Format("OData exception occurred: {0}.", context.Exception));
			}
			return context.Request.CreateErrorResponse(httpStatusCode, odataError);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007F18 File Offset: 0x00006118
		internal static string GetSoapDetailDataName(Microsoft.ReportingServices.Diagnostics.Utilities.ErrorCode code)
		{
			return string.Format("ShowDetailOf({0})", code);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00007F2A File Offset: 0x0000612A
		internal static void AllowSoapDetailException(SoapException exception, Microsoft.ReportingServices.Diagnostics.Utilities.ErrorCode code)
		{
			exception.Data.Add(PortalExceptionFilter.GetSoapDetailDataName(code), true);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007F44 File Offset: 0x00006144
		internal static void GetRsLibDetailsFromSoapException(SoapException soapException, ODataError error, ref HttpStatusCode statusCode)
		{
			if (soapException.Detail != null)
			{
				Microsoft.ReportingServices.Diagnostics.Utilities.ErrorCode errorCode = Microsoft.ReportingServices.Diagnostics.Utilities.ErrorCode.rsSuccess;
				XPathNavigator xpathNavigator = soapException.Detail.CreateNavigator();
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xpathNavigator.NameTable);
				xmlNamespaceManager.AddNamespace("rs", "http://www.microsoft.com/sql/reportingservices");
				XPathNavigator xpathNavigator2 = xpathNavigator.SelectSingleNode("./rs:ErrorCode", xmlNamespaceManager);
				if (xpathNavigator2 != null && Enum.TryParse<Microsoft.ReportingServices.Diagnostics.Utilities.ErrorCode>(xpathNavigator2.Value, out errorCode))
				{
					error.ErrorCode = (errorCode + 1000).ToString("D");
					if (RsErrorCodeMapperUtility.HttpStatusCodeMap.ContainsKey(errorCode))
					{
						statusCode = RsErrorCodeMapperUtility.HttpStatusCodeMap[errorCode];
					}
				}
				string[] array = (from XPathNavigator n in xpathNavigator.Select(".//rs:Message", xmlNamespaceManager)
					select n.Value).Distinct<string>().ToArray<string>();
				string text = array.FirstOrDefault<string>();
				if (text != null)
				{
					error.Message = text;
				}
				if (soapException.Data.Contains(PortalExceptionFilter.GetSoapDetailDataName(errorCode)))
				{
					for (int i = 1; i < array.Length; i++)
					{
						PortalExceptionFilter.AddODataErrorDetail(new ODataErrorDetail
						{
							Message = array[i]
						}, ref error);
					}
				}
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008068 File Offset: 0x00006268
		internal static void OverrideErrorDetails(Exception e, ref HttpStatusCode statusCode, ref ODataError odataError)
		{
			if (e is AccessDeniedException)
			{
				statusCode = HttpStatusCode.Forbidden;
				odataError.ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.AccessDenied.ToString("D");
				return;
			}
			if (e is ItemNotFoundException || e is DataSetNotFoundException || e is InvalidParameterException || e is ScheduleNotFoundException || e is SubscriptionNotFoundException || e is UnknownUserNameException)
			{
				statusCode = HttpStatusCode.NotFound;
				odataError.ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.NotFound.ToString("D");
				return;
			}
			if (e is ItemAlreadyExistsException || e is InvalidPolicyDefinitionException)
			{
				statusCode = HttpStatusCode.Conflict;
				odataError.ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.AlreadyExists.ToString("D");
				return;
			}
			if (e is WrongItemTypeException)
			{
				statusCode = HttpStatusCode.Conflict;
				odataError.ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.WrongItemType.ToString("D");
				return;
			}
			if (e is CatalogItemContentInvalidException || e is ParameterTypeMismatchException)
			{
				statusCode = HttpStatusCode.BadRequest;
				odataError.ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.InvalidContent.ToString("D");
				return;
			}
			if (e is InvalidItemNameException || e is InvalidItemPathException)
			{
				statusCode = (HttpStatusCode)422;
				odataError.ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.InvalidName.ToString("D");
				return;
			}
			if (e is ItemPathLengthExceededException)
			{
				statusCode = (HttpStatusCode)422;
				odataError.ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.LongPath.ToString("D");
				return;
			}
			if (e is ReportServerDatabaseLogonFailedException || e is DatabaseUnavailableException || e is CannotValidateEncryptedDataException)
			{
				statusCode = HttpStatusCode.ServiceUnavailable;
				return;
			}
			if (e is FileSizeException || e is FileSizeNotSupportedException)
			{
				statusCode = (HttpStatusCode)422;
				odataError.ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.NotSupportedException.ToString("D");
				return;
			}
			if (e is ResourceFileFormatNotAllowedException)
			{
				statusCode = HttpStatusCode.BadRequest;
				odataError.ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.ResourceFileUploadNotSupported.ToString("D");
				return;
			}
			if (e is ResourceMimeTypeNotAllowedException)
			{
				statusCode = HttpStatusCode.BadRequest;
				odataError.ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.ResourceFileMimeTypeNotSupported.ToString("D");
				return;
			}
			if (e is ExcelFileExtensionChangeNotAllowedException)
			{
				statusCode = HttpStatusCode.BadRequest;
				odataError.ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.ExcelFileExtensionChangeNotAllowedException.ToString("D");
				return;
			}
			if (e is ReportCatalogException)
			{
				statusCode = HttpStatusCode.InternalServerError;
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000082A8 File Offset: 0x000064A8
		internal static void AddSystemResourcePackageExceptionErrorDetails(Microsoft.ReportingServices.Diagnostics.Utilities.SystemResourcePackageException packageException, ref ODataError odataError)
		{
			if (packageException is SystemResourcePackageMetadataInvalidException)
			{
				PortalExceptionFilter.AddODataErrorDetail(new ODataErrorDetail
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.SystemResourcePackageMetadataValidationFailure.ToString("D"),
					Message = packageException.Message
				}, ref odataError);
				return;
			}
			if (packageException is SystemResourcePackageMetadataNotFoundException)
			{
				PortalExceptionFilter.AddODataErrorDetail(new ODataErrorDetail
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.SystemResourcePackageMetadataNotFound.ToString("D"),
					Message = packageException.Message
				}, ref odataError);
				return;
			}
			if (packageException is SystemResourcePackageReferencedItemMissingException)
			{
				PortalExceptionFilter.AddODataErrorDetail(new ODataErrorDetail
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.SystemResourcePackageReferencedItemMissing.ToString("D"),
					Target = ((SystemResourcePackageReferencedItemMissingException)packageException).AdditionalTraceMessage,
					Message = packageException.Message
				}, ref odataError);
				return;
			}
			if (packageException is SystemResourcePackageRequiredItemMissingException)
			{
				PortalExceptionFilter.AddODataErrorDetail(new ODataErrorDetail
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.SystemResourcePackageRequiredItemMissing.ToString("D"),
					Target = ((SystemResourcePackageRequiredItemMissingException)packageException).AdditionalTraceMessage,
					Message = packageException.Message
				}, ref odataError);
				return;
			}
			if (packageException is SystemResourcePackageItemContentTypeMismatchException)
			{
				PortalExceptionFilter.AddODataErrorDetail(new ODataErrorDetail
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.SystemResourcePackageCannotValidateItemContentType.ToString("D"),
					Target = ((SystemResourcePackageItemContentTypeMismatchException)packageException).AdditionalTraceMessage,
					Message = packageException.Message
				}, ref odataError);
				return;
			}
			if (packageException is SystemResourcePackageItemExtensionMismatchException)
			{
				PortalExceptionFilter.AddODataErrorDetail(new ODataErrorDetail
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.SystemResourcePackageCannotValidateItemExtension.ToString("D"),
					Target = ((SystemResourcePackageItemExtensionMismatchException)packageException).AdditionalTraceMessage,
					Message = packageException.Message
				}, ref odataError);
				return;
			}
			if (packageException is SystemResourcePackageWrongTypeException)
			{
				PortalExceptionFilter.AddODataErrorDetail(new ODataErrorDetail
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.SystemResourcePackageWrongType.ToString("D"),
					Target = ((SystemResourcePackageWrongTypeException)packageException).AdditionalTraceMessage,
					Message = packageException.Message
				}, ref odataError);
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000084A0 File Offset: 0x000066A0
		internal static void AddDataSetProcessingExceptionErrorDetails(Exception dataSetProcessingException, ref ODataError odataError)
		{
			if (dataSetProcessingException is DataSetParameterValueNotSetException)
			{
				PortalExceptionFilter.AddODataErrorDetail(new ODataErrorDetail
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.DataSetParameterValueNotSetException.ToString("D"),
					Message = string.Format(CultureInfo.InvariantCulture, SR.ParameterValueNotSupplied, ((DataSetParameterValueNotSetException)dataSetProcessingException).AdditionalTraceMessage),
					Target = ((DataSetParameterValueNotSetException)dataSetProcessingException).AdditionalTraceMessage
				}, ref odataError);
				return;
			}
			if (dataSetProcessingException is DataSetRenderingSoapException)
			{
				PortalExceptionFilter.AddODataErrorDetail(new ODataErrorDetail
				{
					ErrorCode = Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.DataSetProcessingSoapError.ToString("D"),
					Message = string.Format(CultureInfo.InvariantCulture, SR.DataSetProcessingSoapException, ((DataSetRenderingSoapException)dataSetProcessingException).InnerException.Message)
				}, ref odataError);
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00003877 File Offset: 0x00001A77
		private static void AddODataErrorDetail(ODataErrorDetail odataErrorDetail, ref ODataError odataError)
		{
			odataError.Details = odataError.Details ?? new List<ODataErrorDetail>();
			odataError.Details.Add(odataErrorDetail);
		}

		// Token: 0x04000077 RID: 119
		protected const HttpStatusCode UnprocessableEntity = (HttpStatusCode)422;

		// Token: 0x04000078 RID: 120
		private const int RSErrorCodeOffset = 1000;
	}
}
