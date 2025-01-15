using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.AnalysisServices.AzureClient.Hosting;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x02000024 RID: 36
	internal static class AsPaasHelper
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00005B88 File Offset: 0x00003D88
		public static string GetResponseFromWebException(WebException ex)
		{
			string text;
			if (((ex != null) ? ex.Response : null) == null)
			{
				text = string.Empty;
			}
			else
			{
				Stream responseStream = ex.Response.GetResponseStream();
				try
				{
					if (responseStream != null)
					{
						using (TextReader textReader = new StreamReader(responseStream))
						{
							return textReader.ReadToEnd();
						}
					}
					text = string.Empty;
				}
				finally
				{
					if (responseStream != null)
					{
						responseStream.Close();
					}
				}
			}
			return text;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005C04 File Offset: 0x00003E04
		public static string GetTechnicalDetailsFromPbiSharedResponse(WebResponse response)
		{
			if (response == null)
			{
				return string.Empty;
			}
			string text = response.Headers["RequestId"];
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}
			return RuntimeSR.AsPaasHelper_TechnicalDetails_PbiShared(text, Environment.NewLine);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005C44 File Offset: 0x00003E44
		public static string GetTechnicalDetailsWithWorkspaceInfo(string technicalDetails, string workspaceName)
		{
			if (string.IsNullOrEmpty(technicalDetails))
			{
				return technicalDetails;
			}
			string text = ClientHostingManager.MarkAsRestrictedInformation(workspaceName ?? "null", InfoRestrictionType.CCON);
			return RuntimeSR.AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo(technicalDetails, text, Environment.NewLine);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005C78 File Offset: 0x00003E78
		public static string GetTechnicalDetailsFromDataverseResponse(WebResponse response, IDictionary<string, string> requestHeaders)
		{
			string text = ((response != null) ? response.Headers["x-ms-service-request-id"] : null) ?? string.Empty;
			string empty;
			if (requestHeaders == null || !requestHeaders.TryGetValue("x-ms-client-request-id", out empty))
			{
				empty = string.Empty;
			}
			return RuntimeSR.AsPaasHelper_TechnicalDetails_Dataverse(empty, text, Environment.NewLine);
		}

		// Token: 0x02000068 RID: 104
		public static class XmlaHttpHeaders
		{
			// Token: 0x040001E6 RID: 486
			public const string WrongNode = "x-ms-wrong-node";

			// Token: 0x040001E7 RID: 487
			public const string Server = "x-ms-xmlaserver";

			// Token: 0x040001E8 RID: 488
			public const string Database = "x-ms-xmladatabase";

			// Token: 0x040001E9 RID: 489
			public const string BypassAuthorization = "x-ms-xmlabypassauthorization";

			// Token: 0x040001EA RID: 490
			public const string User = "x-ms-xmlauser";

			// Token: 0x040001EB RID: 491
			public const string WorkspaceObjectId = "x-ms-xmlaworkspaceobjectid";

			// Token: 0x040001EC RID: 492
			public const string Roles = "x-ms-xmlaroles";

			// Token: 0x040001ED RID: 493
			public const string IntendedUsage = "x-ms-xmlaintendedusage";

			// Token: 0x040001EE RID: 494
			public const string ContextualIdentity = "x-ms-xmlacontextualidentity";

			// Token: 0x040001EF RID: 495
			public const string CapabilitiesNegotiationFlags = "x-ms-xmlacaps-negotiation-flags";

			// Token: 0x040001F0 RID: 496
			public const string SessionId = "x-ms-xmlasession-id";

			// Token: 0x040001F1 RID: 497
			public const string AppGeneralInfo = "x-ms-xmlaapp-general-info";

			// Token: 0x040001F2 RID: 498
			public const string ParentActivityId = "x-ms-parent-activity-id";

			// Token: 0x040001F3 RID: 499
			public const string RootActivityId = "x-ms-root-activity-id";

			// Token: 0x040001F4 RID: 500
			public const string RequestRegistrationId = "x-ms-request-registration-id";

			// Token: 0x040001F5 RID: 501
			public const string AcceptsContinuations = "x-ms-accepts-continuations";

			// Token: 0x040001F6 RID: 502
			public const string RoundTripId = "x-ms-round-trip-id";

			// Token: 0x040001F7 RID: 503
			public const string DedicatedConnection = "x-ms-xmladedicatedconnection";

			// Token: 0x040001F8 RID: 504
			public const string WorkloadResourceMoniker = "x-ms-workload-resource-moniker";

			// Token: 0x040001F9 RID: 505
			public const string RoutingScenario = "x-ms-routing-scenario";

			// Token: 0x040001FA RID: 506
			public const string RoutingHint = "x-ms-routing-hint";

			// Token: 0x040001FB RID: 507
			public const string TransientModelMode = "x-ms-xmlatransientmodelmode";

			// Token: 0x040001FC RID: 508
			public const string ReadOnly = "x-ms-xmlareadonly";

			// Token: 0x040001FD RID: 509
			public const string ServiceToServiceToken = "x-ms-xls2stoken";

			// Token: 0x040001FE RID: 510
			public const string CurrentUtcDate = "x-ms-current-utc-date";

			// Token: 0x040001FF RID: 511
			public const string ErrorDetails = "x-ms-xmlaerror-extended";

			// Token: 0x04000200 RID: 512
			public const string XmlaClientTraits = "x-ms-xmlaclienttraits";

			// Token: 0x04000201 RID: 513
			public const string ScaleOutAutoSync = "x-ms-xmlascaleoutautosync";

			// Token: 0x04000202 RID: 514
			public const string SourceCapacityObjectId = "x-ms-src-capacity-id";

			// Token: 0x04000203 RID: 515
			public const string ServicePrincipalProfileId = "X-PowerBI-profile-id";
		}

		// Token: 0x02000069 RID: 105
		public static class XmlaHttpHeaderValues
		{
			// Token: 0x04000204 RID: 516
			public const string WrongNodeIndication = "1";
		}

		// Token: 0x0200006A RID: 106
		public static class PbiHttpHeaders
		{
			// Token: 0x04000205 RID: 517
			public const string RequestId = "RequestId";
		}

		// Token: 0x0200006B RID: 107
		public static class DataverseHttpHeaders
		{
			// Token: 0x04000206 RID: 518
			public const string ServiceRequestId = "x-ms-service-request-id";

			// Token: 0x04000207 RID: 519
			public const string ClientRequestId = "x-ms-client-request-id";
		}
	}
}
