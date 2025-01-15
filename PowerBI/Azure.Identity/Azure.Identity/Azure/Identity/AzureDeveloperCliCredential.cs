using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Identity
{
	// Token: 0x02000034 RID: 52
	public class AzureDeveloperCliCredential : TokenCredential
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000058FA File Offset: 0x00003AFA
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00005902 File Offset: 0x00003B02
		internal TimeSpan ProcessTimeout { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000590B File Offset: 0x00003B0B
		internal string TenantId { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00005913 File Offset: 0x00003B13
		internal string[] AdditionallyAllowedTenantIds { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000591B File Offset: 0x00003B1B
		internal TenantIdResolverBase TenantIdResolver { get; }

		// Token: 0x0600014D RID: 333 RVA: 0x00005923 File Offset: 0x00003B23
		public AzureDeveloperCliCredential()
			: this(CredentialPipeline.GetInstance(null, false), null, null)
		{
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005934 File Offset: 0x00003B34
		public AzureDeveloperCliCredential(AzureDeveloperCliCredentialOptions options)
			: this(CredentialPipeline.GetInstance(null, false), null, options)
		{
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005948 File Offset: 0x00003B48
		internal AzureDeveloperCliCredential(CredentialPipeline pipeline, IProcessService processService, AzureDeveloperCliCredentialOptions options = null)
		{
			this._logPII = options != null && options.IsUnsafeSupportLoggingEnabled;
			bool? flag;
			if (options == null)
			{
				flag = null;
			}
			else
			{
				TokenCredentialDiagnosticsOptions diagnostics = options.Diagnostics;
				flag = ((diagnostics != null) ? new bool?(diagnostics.IsAccountIdentifierLoggingEnabled) : null);
			}
			bool? flag2 = flag;
			this._logAccountDetails = flag2.GetValueOrDefault();
			this._pipeline = pipeline;
			this._processService = processService ?? ProcessService.Default;
			this.TenantId = Validations.ValidateTenantId((options != null) ? options.TenantId : null, "options.TenantId", true);
			this.TenantIdResolver = ((options != null) ? options.TenantIdResolver : null) ?? TenantIdResolverBase.Default;
			this.AdditionallyAllowedTenantIds = this.TenantIdResolver.ResolveAddionallyAllowedTenantIds((options != null) ? ((ISupportsAdditionallyAllowedTenants)options).AdditionallyAllowedTenants : null);
			this.ProcessTimeout = ((options != null) ? options.ProcessTimeout : null) ?? TimeSpan.FromSeconds(13.0);
			this._isChainedCredential = options != null && options.IsChainedCredential;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005A62 File Offset: 0x00003C62
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetTokenImplAsync(false, requestContext, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005A74 File Offset: 0x00003C74
		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.GetTokenImplAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005AC8 File Offset: 0x00003CC8
		private ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			AzureDeveloperCliCredential.<GetTokenImplAsync>d__36 <GetTokenImplAsync>d__;
			<GetTokenImplAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenImplAsync>d__.<>4__this = this;
			<GetTokenImplAsync>d__.async = async;
			<GetTokenImplAsync>d__.requestContext = requestContext;
			<GetTokenImplAsync>d__.cancellationToken = cancellationToken;
			<GetTokenImplAsync>d__.<>1__state = -1;
			<GetTokenImplAsync>d__.<>t__builder.Start<AzureDeveloperCliCredential.<GetTokenImplAsync>d__36>(ref <GetTokenImplAsync>d__);
			return <GetTokenImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005B24 File Offset: 0x00003D24
		private async ValueTask<AccessToken> RequestCliAccessTokenAsync(bool async, TokenRequestContext context, CancellationToken cancellationToken)
		{
			string text = this.TenantIdResolver.Resolve(this.TenantId, context, this.AdditionallyAllowedTenantIds);
			Validations.ValidateTenantId(text, "TenantId", true);
			string[] scopes = context.Scopes;
			for (int i = 0; i < scopes.Length; i++)
			{
				ScopeUtilities.ValidateScope(scopes[i]);
			}
			string text2;
			string text3;
			AzureDeveloperCliCredential.GetFileNameAndArguments(context.Scopes, text, out text2, out text3);
			ProcessStartInfo azureDeveloperCliProcessStartInfo = AzureDeveloperCliCredential.GetAzureDeveloperCliProcessStartInfo(text2, text3);
			AccessToken accessToken2;
			using (ProcessRunner processRunner = new ProcessRunner(this._processService.Create(azureDeveloperCliProcessStartInfo), this.ProcessTimeout, this._logPII, cancellationToken))
			{
				string text5;
				try
				{
					string text4;
					if (async)
					{
						text4 = await processRunner.RunAsync().ConfigureAwait(false);
					}
					else
					{
						text4 = processRunner.Run();
					}
					text5 = text4;
				}
				catch (OperationCanceledException obj) when (!cancellationToken.IsCancellationRequested)
				{
					if (this._isChainedCredential)
					{
						throw new CredentialUnavailableException("Azure Developer CLI authentication timed out.");
					}
					throw new AuthenticationFailedException("Azure Developer CLI authentication timed out.");
				}
				catch (InvalidOperationException ex)
				{
					if (ex.Message.StartsWith("'azd' is not recognized", StringComparison.CurrentCultureIgnoreCase) | AzureDeveloperCliCredential.AzdNotFoundPattern.IsMatch(ex.Message))
					{
						throw new CredentialUnavailableException("Azure Developer CLI could not be found.");
					}
					bool flag = ex.Message.Contains("AADSTS");
					if (ex.Message.IndexOf("azd auth login", StringComparison.OrdinalIgnoreCase) != -1 && !flag)
					{
						throw new CredentialUnavailableException("Please run 'azd auth login' from a command prompt to authenticate before using this credential.");
					}
					if ((ex.Message.IndexOf("Azure Developer CLI authentication failed due to an unknown error.", StringComparison.OrdinalIgnoreCase) != -1 && ex.Message.IndexOf("The provided authorization code or refresh token has expired due to inactivity. Send a new interactive authorization request for this user and resource.", StringComparison.OrdinalIgnoreCase) != -1) || ex.Message.IndexOf("CLIInternalError", StringComparison.OrdinalIgnoreCase) != -1)
					{
						throw new CredentialUnavailableException("Azure Developer CLI could not login. Interactive login is required.");
					}
					if (this._isChainedCredential)
					{
						throw new CredentialUnavailableException("Azure Developer CLI authentication failed due to an unknown error. Please visit https://aka.ms/azure-dev for installation instructions and then, once installed, authenticate to your Azure account using 'azd auth login'. " + ex.Message);
					}
					throw new AuthenticationFailedException("Azure Developer CLI authentication failed due to an unknown error. Please visit https://aka.ms/azure-dev for installation instructions and then, once installed, authenticate to your Azure account using 'azd auth login'. " + ex.Message);
				}
				AccessToken accessToken = AzureDeveloperCliCredential.DeserializeOutput(text5);
				if (this._logAccountDetails)
				{
					ValueTuple<string, string, string, string> valueTuple = TokenHelper.ParseAccountInfoFromToken(accessToken.Token);
					AzureIdentityEventSource.Singleton.AuthenticatedAccountDetails(valueTuple.Item1, valueTuple.Item2 ?? this.TenantId, valueTuple.Item3, valueTuple.Item4);
				}
				accessToken2 = accessToken;
			}
			return accessToken2;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005B7F File Offset: 0x00003D7F
		private static ProcessStartInfo GetAzureDeveloperCliProcessStartInfo(string fileName, string argument)
		{
			return new ProcessStartInfo
			{
				FileName = fileName,
				Arguments = argument,
				UseShellExecute = false,
				ErrorDialog = false,
				CreateNoWindow = true,
				WorkingDirectory = AzureDeveloperCliCredential.DefaultWorkingDir
			};
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005BB4 File Offset: 0x00003DB4
		private static void GetFileNameAndArguments(string[] scopes, string tenantId, out string fileName, out string argument)
		{
			string text = string.Join(" ", scopes.Select((string scope) => "--scope " + scope));
			string text2;
			if (tenantId == null)
			{
				text2 = "azd auth token --output json " + text;
			}
			else
			{
				text2 = "azd auth token --output json " + text + " --tenant-id " + tenantId;
			}
			string text3 = text2;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "cmd.exe");
				argument = "/d /c \"" + text3 + "\"";
				return;
			}
			fileName = "/bin/sh";
			argument = "-c \"" + text3 + "\"";
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005C64 File Offset: 0x00003E64
		private static AccessToken DeserializeOutput(string output)
		{
			AccessToken accessToken;
			using (JsonDocument jsonDocument = JsonDocument.Parse(output, default(JsonDocumentOptions)))
			{
				JsonElement rootElement = jsonDocument.RootElement;
				string @string = rootElement.GetProperty("token").GetString();
				DateTimeOffset dateTimeOffset = rootElement.GetProperty("expiresOn").GetDateTimeOffset();
				accessToken = new AccessToken(@string, dateTimeOffset);
			}
			return accessToken;
		}

		// Token: 0x040000E5 RID: 229
		internal const string AzdCliNotInstalled = "Azure Developer CLI could not be found.";

		// Token: 0x040000E6 RID: 230
		internal const string AzdNotLogIn = "Please run 'azd auth login' from a command prompt to authenticate before using this credential.";

		// Token: 0x040000E7 RID: 231
		internal const string WinAzdCliError = "'azd' is not recognized";

		// Token: 0x040000E8 RID: 232
		internal const string AzdCliTimeoutError = "Azure Developer CLI authentication timed out.";

		// Token: 0x040000E9 RID: 233
		internal const string AzdCliFailedError = "Azure Developer CLI authentication failed due to an unknown error.";

		// Token: 0x040000EA RID: 234
		internal const string Troubleshoot = "Please visit https://aka.ms/azure-dev for installation instructions and then, once installed, authenticate to your Azure account using 'azd auth login'.";

		// Token: 0x040000EB RID: 235
		internal const string InteractiveLoginRequired = "Azure Developer CLI could not login. Interactive login is required.";

		// Token: 0x040000EC RID: 236
		internal const string AzdCLIInternalError = "AzdCLIInternalError: The command failed with an unexpected error. Here is the traceback:";

		// Token: 0x040000EE RID: 238
		private static readonly string DefaultWorkingDirWindows = Environment.GetFolderPath(Environment.SpecialFolder.System);

		// Token: 0x040000EF RID: 239
		private const string DefaultWorkingDirNonWindows = "/bin/";

		// Token: 0x040000F0 RID: 240
		private const string RefreshTokeExpired = "The provided authorization code or refresh token has expired due to inactivity. Send a new interactive authorization request for this user and resource.";

		// Token: 0x040000F1 RID: 241
		private static readonly string DefaultWorkingDir = (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? AzureDeveloperCliCredential.DefaultWorkingDirWindows : "/bin/");

		// Token: 0x040000F2 RID: 242
		private static readonly Regex AzdNotFoundPattern = new Regex("azd:(.*)not found");

		// Token: 0x040000F3 RID: 243
		private readonly CredentialPipeline _pipeline;

		// Token: 0x040000F4 RID: 244
		private readonly IProcessService _processService;

		// Token: 0x040000F5 RID: 245
		private readonly bool _logPII;

		// Token: 0x040000F6 RID: 246
		private readonly bool _logAccountDetails;

		// Token: 0x040000F9 RID: 249
		internal bool _isChainedCredential;
	}
}
