using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.PowerBI.ReportServer.ExploreHost.Error;
using Microsoft.PowerBI.ReportServer.WebApi.Catalog;

namespace Microsoft.PowerBI.ReportServer.WebApi.Error
{
	// Token: 0x0200003C RID: 60
	public sealed class RSErrorExtractor : IServiceErrorExtractor
	{
		// Token: 0x06000107 RID: 263 RVA: 0x00006F88 File Offset: 0x00005188
		public bool CanExtractFromException(Exception ex)
		{
			return this.ExtractError(ex) != null;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006F94 File Offset: 0x00005194
		public bool TryExtractServiceError(Exception ex, ServiceErrorExtractor extractor, out ServiceError error)
		{
			ServiceError serviceError = this.ExtractError(ex);
			if (serviceError == null)
			{
				error = new ServiceError();
				return false;
			}
			error = serviceError;
			return true;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006FBC File Offset: 0x000051BC
		private ServiceError ExtractError(Exception ex)
		{
			RSExploreHostException ex2 = ex as RSExploreHostException;
			if (ex2 != null && ex2.Code == "rsErrorOpeningConnection")
			{
				if (ex2.ExceptionStackContains((Exception e) => e.GetType() == typeof(SocketException) && ((SocketException)e).SocketErrorCode == SocketError.HostNotFound))
				{
					return new ServiceError
					{
						PowerBIErrorCode = "OnPremises_Connection_Error_General",
						ErrorDetails = this.CreateErrorDetails("OnPremises_Connection_Error_Connection_String")
					};
				}
				if (ex2.ExceptionStackContains((Exception e) => e.GetType() == typeof(SocketException) && ((SocketException)e).SocketErrorCode == SocketError.ConnectionReset))
				{
					return new ServiceError
					{
						PowerBIErrorCode = "OnPremises_Connection_Error_General",
						ErrorDetails = this.CreateErrorDetails("OnPremises_Connection_Error_Connection_Closed")
					};
				}
				return new ServiceError
				{
					PowerBIErrorCode = "OnPremises_Connection_Error_General"
				};
			}
			else
			{
				CatalogAccessException ex3 = ex as CatalogAccessException;
				if (ex3 != null)
				{
					if (ex3.ErrorCode == CatalogAccessExceptionErrorCode.StoredCredentialsIncorrect)
					{
						return new ServiceError
						{
							PowerBIErrorCode = "OnPremises_Connection_Error_General",
							ErrorDetails = this.CreateErrorDetails("OnPremises_Impersonation_Error")
						};
					}
					if (ex3.ErrorCode == CatalogAccessExceptionErrorCode.StoredConnectionStringIncorrect)
					{
						return new ServiceError
						{
							PowerBIErrorCode = "OnPremises_Connection_Error_General",
							ErrorDetails = this.CreateErrorDetails("OnPremises_Connection_Error_Connection_String")
						};
					}
					if (ex3.ErrorCode == CatalogAccessExceptionErrorCode.UnsupportedCredentialsType)
					{
						return new ServiceError
						{
							PowerBIErrorCode = "OnPremises_Connection_Error_General",
							ErrorDetails = this.CreateErrorDetails("OnPremises_Connection_Error_AS_DbCredentials")
						};
					}
				}
				PowerBIExploreException ex4 = ex as PowerBIExploreException;
				if (ex4 != null && this.PBIConnectionStringErrorCodes.Contains(ex4.ErrorCode))
				{
					return new ServiceError
					{
						PowerBIErrorCode = "OnPremises_Connection_Error_General",
						ErrorDetails = this.CreateErrorDetails("OnPremises_Connection_Error_Connection_String")
					};
				}
				return null;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000715C File Offset: 0x0000535C
		private List<ErrorDetail> CreateErrorDetails(string errorDetailsMessageResourceCode)
		{
			return new List<ErrorDetail>
			{
				new ErrorDetail(errorDetailsMessageResourceCode, new ErrorDetailValue(ErrorResourceType.EmbeddedString, ""))
			};
		}

		// Token: 0x040000AF RID: 175
		private readonly HashSet<string> PBIConnectionStringErrorCodes = new HashSet<string> { "CannotRetrieveModelError", "OpenConnectionError", "DataExtensionError", "QueryUserError", "QuerySystemError" };
	}
}
