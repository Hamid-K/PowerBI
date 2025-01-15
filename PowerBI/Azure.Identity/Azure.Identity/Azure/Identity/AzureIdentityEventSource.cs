using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Text;
using Azure.Core;
using Azure.Core.Diagnostics;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x02000029 RID: 41
	[EventSource(Name = "Azure-Identity")]
	internal sealed class AzureIdentityEventSource : AzureEventSource
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x000046E2 File Offset: 0x000028E2
		private AzureIdentityEventSource()
			: base("Azure-Identity")
		{
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000046EF File Offset: 0x000028EF
		public static AzureIdentityEventSource Singleton { get; } = new AzureIdentityEventSource();

		// Token: 0x060000DA RID: 218 RVA: 0x000046F6 File Offset: 0x000028F6
		[NonEvent]
		public void GetToken(string method, TokenRequestContext context)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				this.GetToken(method, AzureIdentityEventSource.FormatStringArray(context.Scopes), context.ParentRequestId);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000471D File Offset: 0x0000291D
		[Event(1, Level = EventLevel.Informational, Message = "{0} invoked. Scopes: {1} ParentRequestId: {2}")]
		public void GetToken(string method, string scopes, string parentRequestId)
		{
			base.WriteEvent(1, method, scopes, parentRequestId);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004729 File Offset: 0x00002929
		[NonEvent]
		public void GetTokenSucceeded(string method, TokenRequestContext context, DateTimeOffset expiresOn)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				this.GetTokenSucceeded(method, AzureIdentityEventSource.FormatStringArray(context.Scopes), context.ParentRequestId, expiresOn.ToString("O", CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004761 File Offset: 0x00002961
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Parameters to this method are primitive and are trimmer safe.")]
		[Event(2, Level = EventLevel.Informational, Message = "{0} succeeded. Scopes: {1} ParentRequestId: {2} ExpiresOn: {3}")]
		public void GetTokenSucceeded(string method, string scopes, string parentRequestId, string expiresOn)
		{
			base.WriteEvent(2, new object[] { method, scopes, parentRequestId, expiresOn });
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004781 File Offset: 0x00002981
		[NonEvent]
		public void GetTokenFailed(string method, TokenRequestContext context, Exception ex)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				this.GetTokenFailed(method, AzureIdentityEventSource.FormatStringArray(context.Scopes), context.ParentRequestId, AzureIdentityEventSource.FormatException(ex));
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000047AE File Offset: 0x000029AE
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Parameters to this method are primitive and are trimmer safe.")]
		[Event(3, Level = EventLevel.Informational, Message = "{0} was unable to retrieve an access token. Scopes: {1} ParentRequestId: {2} Exception: {3}")]
		public void GetTokenFailed(string method, string scopes, string parentRequestId, string exception)
		{
			base.WriteEvent(3, new object[] { method, scopes, parentRequestId, exception });
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000047CE File Offset: 0x000029CE
		[NonEvent]
		public void ProbeImdsEndpoint(Uri uri)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				this.ProbeImdsEndpoint(uri.AbsoluteUri);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000047E7 File Offset: 0x000029E7
		[Event(4, Level = EventLevel.Informational, Message = "Probing IMDS endpoint for availability. Endpoint: {0}")]
		public void ProbeImdsEndpoint(string uri)
		{
			base.WriteEvent(4, uri);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000047F1 File Offset: 0x000029F1
		[NonEvent]
		public void ImdsEndpointFound(Uri uri)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				this.ImdsEndpointFound(uri.AbsoluteUri);
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000480A File Offset: 0x00002A0A
		[Event(5, Level = EventLevel.Informational, Message = "IMDS endpoint is available. Endpoint: {0}")]
		public void ImdsEndpointFound(string uri)
		{
			base.WriteEvent(5, uri);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004814 File Offset: 0x00002A14
		[NonEvent]
		public void ImdsEndpointUnavailable(Uri uri, string error)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				this.ImdsEndpointUnavailable(uri.AbsoluteUri, error);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000482E File Offset: 0x00002A2E
		[NonEvent]
		public void ImdsEndpointUnavailable(Uri uri, Exception e)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				this.ImdsEndpointUnavailable(uri.AbsoluteUri, AzureIdentityEventSource.FormatException(e));
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000484D File Offset: 0x00002A4D
		[Event(6, Level = EventLevel.Informational, Message = "IMDS endpoint is not available. Endpoint: {0}. Error: {1}")]
		public void ImdsEndpointUnavailable(string uri, string error)
		{
			base.WriteEvent(6, uri, error);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004858 File Offset: 0x00002A58
		[NonEvent]
		private static string FormatException(Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			do
			{
				if (flag)
				{
					stringBuilder.AppendLine().Append(" ---> ");
				}
				stringBuilder.Append(ex.GetType().FullName).Append(" (0x").Append(ex.HResult.ToString("x", CultureInfo.InvariantCulture))
					.Append("): ")
					.Append(ex.Message);
				ex = ex.InnerException;
				flag = true;
			}
			while (ex != null);
			return stringBuilder.ToString();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000048E4 File Offset: 0x00002AE4
		[NonEvent]
		public void LogMsal(LogLevel level, string message)
		{
			switch (level)
			{
			case LogLevel.Error:
				if (base.IsEnabled(EventLevel.Error, EventKeywords.All))
				{
					this.LogMsalError(message);
					return;
				}
				break;
			case LogLevel.Warning:
				if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
				{
					this.LogMsalWarning(message);
					return;
				}
				break;
			case LogLevel.Info:
				if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
				{
					this.LogMsalInformational(message);
					return;
				}
				break;
			case LogLevel.Verbose:
				if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
				{
					this.LogMsalVerbose(message);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004955 File Offset: 0x00002B55
		[Event(10, Level = EventLevel.Error, Message = "{0}")]
		public void LogMsalError(string message)
		{
			base.WriteEvent(10, message);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004960 File Offset: 0x00002B60
		[Event(9, Level = EventLevel.Warning, Message = "{0}")]
		public void LogMsalWarning(string message)
		{
			base.WriteEvent(9, message);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000496B File Offset: 0x00002B6B
		[Event(8, Level = EventLevel.Informational, Message = "{0}")]
		public void LogMsalInformational(string message)
		{
			base.WriteEvent(8, message);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004975 File Offset: 0x00002B75
		[Event(7, Level = EventLevel.Verbose, Message = "{0}")]
		public void LogMsalVerbose(string message)
		{
			base.WriteEvent(7, message);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000497F File Offset: 0x00002B7F
		[NonEvent]
		private static string FormatStringArray(string[] array)
		{
			return new StringBuilder("[ ").Append(string.Join(", ", array)).Append(" ]").ToString();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000049AA File Offset: 0x00002BAA
		[Event(11, Level = EventLevel.Informational, Message = "Executing interactive authentication workflow via Task.Run.")]
		public void InteractiveAuthenticationExecutingOnThreadPool()
		{
			base.WriteEvent(11);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000049B4 File Offset: 0x00002BB4
		[Event(12, Level = EventLevel.Informational, Message = "Executing interactive authentication workflow inline.")]
		public void InteractiveAuthenticationExecutingInline()
		{
			base.WriteEvent(12);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000049BE File Offset: 0x00002BBE
		[Event(13, Level = EventLevel.Informational, Message = "DefaultAzureCredential credential selected: {0}")]
		public void DefaultAzureCredentialCredentialSelected(string credentialType)
		{
			base.WriteEvent(13, credentialType);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000049C9 File Offset: 0x00002BC9
		[NonEvent]
		public void ProcessRunnerError(string message)
		{
			if (base.IsEnabled(EventLevel.Error, EventKeywords.All))
			{
				this.LogProcessRunnerError(message);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000049DD File Offset: 0x00002BDD
		[Event(14, Level = EventLevel.Error, Message = "{0}")]
		public void LogProcessRunnerError(string message)
		{
			base.WriteEvent(14, message);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000049E8 File Offset: 0x00002BE8
		[NonEvent]
		public void ProcessRunnerInformational(string message)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				this.LogProcessRunnerInformational(message);
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000049FC File Offset: 0x00002BFC
		[Event(15, Level = EventLevel.Informational, Message = "{0}")]
		public void LogProcessRunnerInformational(string message)
		{
			base.WriteEvent(15, message);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004A07 File Offset: 0x00002C07
		[NonEvent]
		public void UsernamePasswordCredentialAcquireTokenSilentFailed(Exception e)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				this.UsernamePasswordCredentialAcquireTokenSilentFailed(AzureIdentityEventSource.FormatException(e));
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004A20 File Offset: 0x00002C20
		[Event(16, Level = EventLevel.Informational, Message = "UsernamePasswordCredential failed to acquire token silently. Error: {1}")]
		public void UsernamePasswordCredentialAcquireTokenSilentFailed(string error)
		{
			base.WriteEvent(16, error);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004A2B File Offset: 0x00002C2B
		[Event(17, Level = EventLevel.Informational, Message = "A token was request for a different tenant than was configured on the credential, but the configured value was used since multi tenant authentication has been disabled. Configured TenantId: {0}, Requested TenantId {1}")]
		public void TenantIdDiscoveredAndNotUsed(string explicitTenantId, string contextTenantId)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				base.WriteEvent(17, explicitTenantId, contextTenantId);
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004A42 File Offset: 0x00002C42
		[Event(18, Level = EventLevel.Informational, Message = "A token was requested for a different tenant than was configured on the credential, and the requested tenant id was used to authenticate. Configured TenantId: {0}, Requested TenantId {1}")]
		public void TenantIdDiscoveredAndUsed(string explicitTenantId, string contextTenantId)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				base.WriteEvent(18, explicitTenantId, contextTenantId);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004A5C File Offset: 0x00002C5C
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Parameters to this method are primitive and are trimmer safe.")]
		[Event(19, Level = EventLevel.Informational, Message = "Client ID: {0}. Tenant ID: {1}. User Principal Name: {2} Object ID: {3}")]
		public void AuthenticatedAccountDetails(string clientId, string tenantId, string upn, string objectId)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				base.WriteEvent(19, new object[]
				{
					clientId ?? "<not available>",
					tenantId ?? "<not available>",
					upn ?? "<not available>",
					objectId ?? "<not available>"
				});
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004AB7 File Offset: 0x00002CB7
		[Event(20, Level = EventLevel.Informational, Message = "Unable to parse account details from the Access Token")]
		internal void UnableToParseAccountDetailsFromToken()
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				base.WriteEvent(20);
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004ACC File Offset: 0x00002CCC
		[Event(21, Level = EventLevel.Warning, Message = "User assigned managed identities are not supported in the {0} environment.")]
		public void UserAssignedManagedIdentityNotSupported(string environment)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(21, environment);
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004AE2 File Offset: 0x00002CE2
		[Event(22, Level = EventLevel.Warning, Message = "Service Fabric user assigned managed identity ClientId or ResourceId is not configurable at runtime.")]
		public void ServiceFabricManagedIdentityRuntimeConfigurationNotSupported()
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(22);
			}
		}

		// Token: 0x0400007E RID: 126
		private const string EventSourceName = "Azure-Identity";

		// Token: 0x0400007F RID: 127
		private const int GetTokenEvent = 1;

		// Token: 0x04000080 RID: 128
		private const int GetTokenSucceededEvent = 2;

		// Token: 0x04000081 RID: 129
		private const int GetTokenFailedEvent = 3;

		// Token: 0x04000082 RID: 130
		private const int ProbeImdsEndpointEvent = 4;

		// Token: 0x04000083 RID: 131
		private const int ImdsEndpointFoundEvent = 5;

		// Token: 0x04000084 RID: 132
		private const int ImdsEndpointUnavailableEvent = 6;

		// Token: 0x04000085 RID: 133
		private const int MsalLogVerboseEvent = 7;

		// Token: 0x04000086 RID: 134
		private const int MsalLogInfoEvent = 8;

		// Token: 0x04000087 RID: 135
		private const int MsalLogWarningEvent = 9;

		// Token: 0x04000088 RID: 136
		private const int MsalLogErrorEvent = 10;

		// Token: 0x04000089 RID: 137
		private const int InteractiveAuthenticationThreadPoolExecutionEvent = 11;

		// Token: 0x0400008A RID: 138
		private const int InteractiveAuthenticationInlineExecutionEvent = 12;

		// Token: 0x0400008B RID: 139
		private const int DefaultAzureCredentialCredentialSelectedEvent = 13;

		// Token: 0x0400008C RID: 140
		private const int ProcessRunnerErrorEvent = 14;

		// Token: 0x0400008D RID: 141
		private const int ProcessRunnerInfoEvent = 15;

		// Token: 0x0400008E RID: 142
		private const int UsernamePasswordCredentialAcquireTokenSilentFailedEvent = 16;

		// Token: 0x0400008F RID: 143
		private const int TenantIdDiscoveredAndNotUsedEvent = 17;

		// Token: 0x04000090 RID: 144
		private const int TenantIdDiscoveredAndUsedEvent = 18;

		// Token: 0x04000091 RID: 145
		internal const int AuthenticatedAccountDetailsEvent = 19;

		// Token: 0x04000092 RID: 146
		internal const int UnableToParseAccountDetailsFromTokenEvent = 20;

		// Token: 0x04000093 RID: 147
		private const int UserAssignedManagedIdentityNotSupportedEvent = 21;

		// Token: 0x04000094 RID: 148
		private const int ServiceFabricManagedIdentityRuntimeConfigurationNotSupportedEvent = 22;

		// Token: 0x04000095 RID: 149
		internal const string TenantIdDiscoveredAndNotUsedEventMessage = "A token was request for a different tenant than was configured on the credential, but the configured value was used since multi tenant authentication has been disabled. Configured TenantId: {0}, Requested TenantId {1}";

		// Token: 0x04000096 RID: 150
		internal const string TenantIdDiscoveredAndUsedEventMessage = "A token was requested for a different tenant than was configured on the credential, and the requested tenant id was used to authenticate. Configured TenantId: {0}, Requested TenantId {1}";

		// Token: 0x04000097 RID: 151
		internal const string AuthenticatedAccountDetailsMessage = "Client ID: {0}. Tenant ID: {1}. User Principal Name: {2} Object ID: {3}";

		// Token: 0x04000098 RID: 152
		internal const string Unavailable = "<not available>";

		// Token: 0x04000099 RID: 153
		internal const string UnableToParseAccountDetailsFromTokenMessage = "Unable to parse account details from the Access Token";

		// Token: 0x0400009A RID: 154
		internal const string UserAssignedManagedIdentityNotSupportedMessage = "User assigned managed identities are not supported in the {0} environment.";

		// Token: 0x0400009B RID: 155
		internal const string ServiceFabricManagedIdentityRuntimeConfigurationNotSupportedMessage = "Service Fabric user assigned managed identity ClientId or ResourceId is not configurable at runtime.";
	}
}
