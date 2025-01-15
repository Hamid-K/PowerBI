using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
	// Token: 0x02000032 RID: 50
	public class AzureCliCredential : TokenCredential
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00005415 File Offset: 0x00003615
		// (set) Token: 0x06000132 RID: 306 RVA: 0x0000541D File Offset: 0x0000361D
		internal TimeSpan ProcessTimeout { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005426 File Offset: 0x00003626
		internal string TenantId { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000542E File Offset: 0x0000362E
		internal string[] AdditionallyAllowedTenantIds { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00005436 File Offset: 0x00003636
		internal TenantIdResolverBase TenantIdResolver { get; }

		// Token: 0x06000136 RID: 310 RVA: 0x0000543E File Offset: 0x0000363E
		public AzureCliCredential()
			: this(CredentialPipeline.GetInstance(null, false), null, null)
		{
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000544F File Offset: 0x0000364F
		public AzureCliCredential(AzureCliCredentialOptions options)
			: this(CredentialPipeline.GetInstance(null, false), null, options)
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005460 File Offset: 0x00003660
		internal AzureCliCredential(CredentialPipeline pipeline, IProcessService processService, AzureCliCredentialOptions options = null)
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
			this._path = ((!string.IsNullOrEmpty(EnvironmentVariables.Path)) ? EnvironmentVariables.Path : AzureCliCredential.DefaultPath);
			this._processService = processService ?? ProcessService.Default;
			this.TenantId = Validations.ValidateTenantId((options != null) ? options.TenantId : null, "options.TenantId", true);
			this.TenantIdResolver = ((options != null) ? options.TenantIdResolver : null) ?? TenantIdResolverBase.Default;
			this.AdditionallyAllowedTenantIds = this.TenantIdResolver.ResolveAddionallyAllowedTenantIds((options != null) ? ((ISupportsAdditionallyAllowedTenants)options).AdditionallyAllowedTenants : null);
			this.ProcessTimeout = ((options != null) ? options.ProcessTimeout : null) ?? TimeSpan.FromSeconds(13.0);
			this._isChainedCredential = options != null && options.IsChainedCredential;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005598 File Offset: 0x00003798
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetTokenImplAsync(false, requestContext, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000055A8 File Offset: 0x000037A8
		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.GetTokenImplAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000055FC File Offset: 0x000037FC
		private ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			AzureCliCredential.<GetTokenImplAsync>d__40 <GetTokenImplAsync>d__;
			<GetTokenImplAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenImplAsync>d__.<>4__this = this;
			<GetTokenImplAsync>d__.async = async;
			<GetTokenImplAsync>d__.requestContext = requestContext;
			<GetTokenImplAsync>d__.cancellationToken = cancellationToken;
			<GetTokenImplAsync>d__.<>1__state = -1;
			<GetTokenImplAsync>d__.<>t__builder.Start<AzureCliCredential.<GetTokenImplAsync>d__40>(ref <GetTokenImplAsync>d__);
			return <GetTokenImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005658 File Offset: 0x00003858
		private async ValueTask<AccessToken> RequestCliAccessTokenAsync(bool async, TokenRequestContext context, CancellationToken cancellationToken)
		{
			string text = ScopeUtilities.ScopesToResource(context.Scopes);
			string text2 = this.TenantIdResolver.Resolve(this.TenantId, context, this.AdditionallyAllowedTenantIds);
			Validations.ValidateTenantId(text2, "TenantId", true);
			ScopeUtilities.ValidateScope(text);
			string text3;
			string text4;
			AzureCliCredential.GetFileNameAndArguments(text, text2, out text3, out text4);
			ProcessStartInfo azureCliProcessStartInfo = this.GetAzureCliProcessStartInfo(text3, text4);
			AccessToken accessToken2;
			using (ProcessRunner processRunner = new ProcessRunner(this._processService.Create(azureCliProcessStartInfo), this.ProcessTimeout, this._logPII, cancellationToken))
			{
				string text6;
				try
				{
					string text5;
					if (async)
					{
						text5 = await processRunner.RunAsync().ConfigureAwait(false);
					}
					else
					{
						text5 = processRunner.Run();
					}
					text6 = text5;
				}
				catch (OperationCanceledException obj) when (!cancellationToken.IsCancellationRequested)
				{
					if (this._isChainedCredential)
					{
						throw new CredentialUnavailableException("Azure CLI authentication timed out.");
					}
					throw new AuthenticationFailedException("Azure CLI authentication timed out.");
				}
				catch (InvalidOperationException ex)
				{
					if (ex.Message.StartsWith("'az' is not recognized", StringComparison.CurrentCultureIgnoreCase) | AzureCliCredential.AzNotFoundPattern.IsMatch(ex.Message))
					{
						throw new CredentialUnavailableException("Azure CLI not installed");
					}
					bool flag = ex.Message.Contains("AADSTS");
					if ((ex.Message.IndexOf("az login", StringComparison.OrdinalIgnoreCase) != -1 || ex.Message.IndexOf("az account set", StringComparison.OrdinalIgnoreCase) != -1) && !flag)
					{
						throw new CredentialUnavailableException("Please run 'az login' to set up account");
					}
					if ((ex.Message.IndexOf("Azure CLI authentication failed due to an unknown error.", StringComparison.OrdinalIgnoreCase) != -1 && ex.Message.IndexOf("The provided authorization code or refresh token has expired due to inactivity. Send a new interactive authorization request for this user and resource.", StringComparison.OrdinalIgnoreCase) != -1) || ex.Message.IndexOf("CLIInternalError", StringComparison.OrdinalIgnoreCase) != -1)
					{
						throw new CredentialUnavailableException("Azure CLI could not login. Interactive login is required.");
					}
					if (this._isChainedCredential)
					{
						throw new CredentialUnavailableException("Azure CLI authentication failed due to an unknown error. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/azclicredential/troubleshoot " + ex.Message);
					}
					throw new AuthenticationFailedException("Azure CLI authentication failed due to an unknown error. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/azclicredential/troubleshoot " + ex.Message);
				}
				AccessToken accessToken = AzureCliCredential.DeserializeOutput(text6);
				if (this._logAccountDetails)
				{
					ValueTuple<string, string, string, string> valueTuple = TokenHelper.ParseAccountInfoFromToken(accessToken.Token);
					AzureIdentityEventSource.Singleton.AuthenticatedAccountDetails(valueTuple.Item1, valueTuple.Item2 ?? this.TenantId, valueTuple.Item3, valueTuple.Item4);
				}
				accessToken2 = accessToken;
			}
			return accessToken2;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000056B4 File Offset: 0x000038B4
		private ProcessStartInfo GetAzureCliProcessStartInfo(string fileName, string argument)
		{
			return new ProcessStartInfo
			{
				FileName = fileName,
				Arguments = argument,
				UseShellExecute = false,
				ErrorDialog = false,
				CreateNoWindow = true,
				WorkingDirectory = AzureCliCredential.DefaultWorkingDir,
				Environment = { { "PATH", this._path } }
			};
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000570C File Offset: 0x0000390C
		private static void GetFileNameAndArguments(string resource, string tenantId, out string fileName, out string argument)
		{
			string text;
			if (tenantId == null)
			{
				text = "az account get-access-token --output json --resource " + resource;
			}
			else
			{
				text = "az account get-access-token --output json --resource " + resource + " --tenant " + tenantId;
			}
			string text2 = text;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "cmd.exe");
				argument = "/d /c \"" + text2 + "\"";
				return;
			}
			fileName = "/bin/sh";
			argument = "-c \"" + text2 + "\"";
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000578C File Offset: 0x0000398C
		private static AccessToken DeserializeOutput(string output)
		{
			AccessToken accessToken;
			using (JsonDocument jsonDocument = JsonDocument.Parse(output, default(JsonDocumentOptions)))
			{
				JsonElement rootElement = jsonDocument.RootElement;
				string @string = rootElement.GetProperty("accessToken").GetString();
				JsonElement jsonElement;
				DateTimeOffset dateTimeOffset = (rootElement.TryGetProperty("expires_on", ref jsonElement) ? DateTimeOffset.FromUnixTimeSeconds(jsonElement.GetInt64()) : DateTimeOffset.ParseExact(rootElement.GetProperty("expiresOn").GetString(), "yyyy-MM-dd HH:mm:ss.ffffff", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal));
				accessToken = new AccessToken(@string, dateTimeOffset);
			}
			return accessToken;
		}

		// Token: 0x040000C8 RID: 200
		internal const string AzureCLINotInstalled = "Azure CLI not installed";

		// Token: 0x040000C9 RID: 201
		internal const string AzNotLogIn = "Please run 'az login' to set up account";

		// Token: 0x040000CA RID: 202
		internal const string WinAzureCLIError = "'az' is not recognized";

		// Token: 0x040000CB RID: 203
		internal const string AzureCliTimeoutError = "Azure CLI authentication timed out.";

		// Token: 0x040000CC RID: 204
		internal const string AzureCliFailedError = "Azure CLI authentication failed due to an unknown error.";

		// Token: 0x040000CD RID: 205
		internal const string Troubleshoot = "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/azclicredential/troubleshoot";

		// Token: 0x040000CE RID: 206
		internal const string InteractiveLoginRequired = "Azure CLI could not login. Interactive login is required.";

		// Token: 0x040000CF RID: 207
		internal const string CLIInternalError = "CLIInternalError: The command failed with an unexpected error. Here is the traceback:";

		// Token: 0x040000D1 RID: 209
		private static readonly string DefaultPathWindows = EnvironmentVariables.ProgramFilesX86 + "\\Microsoft SDKs\\Azure\\CLI2\\wbin;" + EnvironmentVariables.ProgramFiles + "\\Microsoft SDKs\\Azure\\CLI2\\wbin";

		// Token: 0x040000D2 RID: 210
		private static readonly string DefaultWorkingDirWindows = Environment.GetFolderPath(Environment.SpecialFolder.System);

		// Token: 0x040000D3 RID: 211
		private const string DefaultPathNonWindows = "/usr/bin:/usr/local/bin";

		// Token: 0x040000D4 RID: 212
		private const string DefaultWorkingDirNonWindows = "/bin/";

		// Token: 0x040000D5 RID: 213
		private const string RefreshTokeExpired = "The provided authorization code or refresh token has expired due to inactivity. Send a new interactive authorization request for this user and resource.";

		// Token: 0x040000D6 RID: 214
		private static readonly string DefaultPath = (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? AzureCliCredential.DefaultPathWindows : "/usr/bin:/usr/local/bin");

		// Token: 0x040000D7 RID: 215
		private static readonly string DefaultWorkingDir = (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? AzureCliCredential.DefaultWorkingDirWindows : "/bin/");

		// Token: 0x040000D8 RID: 216
		private static readonly Regex AzNotFoundPattern = new Regex("az:(.*)not found");

		// Token: 0x040000D9 RID: 217
		private readonly string _path;

		// Token: 0x040000DA RID: 218
		private readonly CredentialPipeline _pipeline;

		// Token: 0x040000DB RID: 219
		private readonly IProcessService _processService;

		// Token: 0x040000DC RID: 220
		private readonly bool _logPII;

		// Token: 0x040000DD RID: 221
		private readonly bool _logAccountDetails;

		// Token: 0x040000E0 RID: 224
		internal bool _isChainedCredential;
	}
}
