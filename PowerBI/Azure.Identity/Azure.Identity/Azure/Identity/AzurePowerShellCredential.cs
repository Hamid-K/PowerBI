using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Identity
{
	// Token: 0x02000036 RID: 54
	public class AzurePowerShellCredential : TokenCredential
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00005D5C File Offset: 0x00003F5C
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00005D64 File Offset: 0x00003F64
		internal TimeSpan ProcessTimeout { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005D6D File Offset: 0x00003F6D
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00005D75 File Offset: 0x00003F75
		internal bool UseLegacyPowerShell { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00005D7E File Offset: 0x00003F7E
		internal TenantIdResolverBase TenantIdResolver { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00005D86 File Offset: 0x00003F86
		internal string TenantId { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005D8E File Offset: 0x00003F8E
		internal string[] AdditionallyAllowedTenantIds { get; }

		// Token: 0x06000166 RID: 358 RVA: 0x00005D96 File Offset: 0x00003F96
		public AzurePowerShellCredential()
			: this(null, null, null)
		{
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005DA1 File Offset: 0x00003FA1
		public AzurePowerShellCredential(AzurePowerShellCredentialOptions options)
			: this(options, null, null)
		{
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005DAC File Offset: 0x00003FAC
		internal AzurePowerShellCredential(AzurePowerShellCredentialOptions options, CredentialPipeline pipeline, IProcessService processService)
		{
			this.UseLegacyPowerShell = false;
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
			this.TenantId = Validations.ValidateTenantId((options != null) ? options.TenantId : null, "options.TenantId", true);
			this._pipeline = pipeline ?? CredentialPipeline.GetInstance(options, false);
			this._processService = processService ?? ProcessService.Default;
			this.TenantIdResolver = ((options != null) ? options.TenantIdResolver : null) ?? TenantIdResolverBase.Default;
			this.AdditionallyAllowedTenantIds = this.TenantIdResolver.ResolveAddionallyAllowedTenantIds((options != null) ? ((ISupportsAdditionallyAllowedTenants)options).AdditionallyAllowedTenants : null);
			this.ProcessTimeout = ((options != null) ? options.ProcessTimeout : null) ?? TimeSpan.FromSeconds(10.0);
			this._isChainedCredential = options != null && options.IsChainedCredential;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005ED8 File Offset: 0x000040D8
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return this.GetTokenImplAsync(false, requestContext, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005EE8 File Offset: 0x000040E8
		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.GetTokenImplAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005F3C File Offset: 0x0000413C
		private ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			AzurePowerShellCredential.<GetTokenImplAsync>d__40 <GetTokenImplAsync>d__;
			<GetTokenImplAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenImplAsync>d__.<>4__this = this;
			<GetTokenImplAsync>d__.async = async;
			<GetTokenImplAsync>d__.requestContext = requestContext;
			<GetTokenImplAsync>d__.cancellationToken = cancellationToken;
			<GetTokenImplAsync>d__.<>1__state = -1;
			<GetTokenImplAsync>d__.<>t__builder.Start<AzurePowerShellCredential.<GetTokenImplAsync>d__40>(ref <GetTokenImplAsync>d__);
			return <GetTokenImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005F98 File Offset: 0x00004198
		private async ValueTask<AccessToken> RequestAzurePowerShellAccessTokenAsync(bool async, TokenRequestContext context, CancellationToken cancellationToken)
		{
			string text = ScopeUtilities.ScopesToResource(context.Scopes);
			string text2 = this.TenantIdResolver.Resolve(this.TenantId, context, this.AdditionallyAllowedTenantIds);
			Validations.ValidateTenantId(text2, "TenantId", true);
			ScopeUtilities.ValidateScope(text);
			string text3;
			string text4;
			this.GetFileNameAndArguments(text, text2, out text3, out text4);
			ProcessStartInfo azurePowerShellProcessStartInfo = AzurePowerShellCredential.GetAzurePowerShellProcessStartInfo(text3, text4);
			AccessToken accessToken;
			using (ProcessRunner processRunner = new ProcessRunner(this._processService.Create(azurePowerShellProcessStartInfo), this.ProcessTimeout, this._logPII, cancellationToken))
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
					AzurePowerShellCredential.CheckForErrors(text6, processRunner.ExitCode);
					AzurePowerShellCredential.ValidateResult(text6);
				}
				catch (OperationCanceledException obj) when (!cancellationToken.IsCancellationRequested)
				{
					throw new AuthenticationFailedException("Azure PowerShell authentication timed out.");
				}
				catch (InvalidOperationException ex)
				{
					AzurePowerShellCredential.CheckForErrors(ex.Message, processRunner.ExitCode);
					if (this._isChainedCredential)
					{
						throw new CredentialUnavailableException("Azure PowerShell authentication failed due to an unknown error. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/powershellcredential/troubleshoot " + ex.Message);
					}
					throw new AuthenticationFailedException("Azure PowerShell authentication failed due to an unknown error. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/powershellcredential/troubleshoot " + ex.Message);
				}
				accessToken = AzurePowerShellCredential.DeserializeOutput(text6);
			}
			return accessToken;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005FF4 File Offset: 0x000041F4
		private static void CheckForErrors(string output, int exitCode)
		{
			int num = (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? 9009 : 127);
			if ((exitCode == num || output.IndexOf("not found", StringComparison.OrdinalIgnoreCase) != -1 || output.IndexOf("is not recognized", StringComparison.OrdinalIgnoreCase) != -1) && output.IndexOf("AADSTS", StringComparison.OrdinalIgnoreCase) == -1)
			{
				throw new CredentialUnavailableException("PowerShell is not installed.");
			}
			if (output.IndexOf("NoAzAccountModule", StringComparison.OrdinalIgnoreCase) != -1)
			{
				throw new CredentialUnavailableException("Az.Accounts module >= 2.2.0 is not installed.");
			}
			if (output.IndexOf("Run Connect-AzAccount to login", StringComparison.OrdinalIgnoreCase) != -1 || output.IndexOf("No accounts were found in the cache", StringComparison.OrdinalIgnoreCase) != -1 || output.IndexOf("cannot retrieve access token", StringComparison.OrdinalIgnoreCase) != -1)
			{
				throw new CredentialUnavailableException("Please run 'Connect-AzAccount' to set up account.");
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000060B2 File Offset: 0x000042B2
		private static void ValidateResult(string output)
		{
			if (output.IndexOf("<Property Name=\"Token\" Type=\"System.String\">", StringComparison.OrdinalIgnoreCase) < 0)
			{
				throw new CredentialUnavailableException("PowerShell did not return a valid response.");
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000060D0 File Offset: 0x000042D0
		private static ProcessStartInfo GetAzurePowerShellProcessStartInfo(string fileName, string argument)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = fileName;
			processStartInfo.Arguments = argument;
			processStartInfo.UseShellExecute = false;
			processStartInfo.ErrorDialog = false;
			processStartInfo.CreateNoWindow = true;
			processStartInfo.WorkingDirectory = AzurePowerShellCredential.DefaultWorkingDir;
			processStartInfo.Environment["POWERSHELL_UPDATECHECK"] = "Off";
			return processStartInfo;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006128 File Offset: 0x00004328
		private void GetFileNameAndArguments(string resource, string tenantId, out string fileName, out string argument)
		{
			string text = "pwsh -NoProfile -NonInteractive -EncodedCommand";
			if (this.UseLegacyPowerShell)
			{
				text = "powershell -NoProfile -NonInteractive -EncodedCommand";
			}
			string text2 = ((tenantId == null) ? string.Empty : (" -TenantId " + tenantId));
			string text3 = AzurePowerShellCredential.Base64Encode(string.Concat(new string[] { "\r\n$ErrorActionPreference = 'Stop'\r\n[version]$minimumVersion = '2.2.0'\r\n\r\n$m = Import-Module Az.Accounts -MinimumVersion $minimumVersion -PassThru -ErrorAction SilentlyContinue\r\n\r\nif (! $m) {\r\n    Write-Output 'NoAzAccountModule'\r\n    exit\r\n}\r\n\r\n$token = Get-AzAccessToken -ResourceUrl '", resource, "'", text2, "\r\n$customToken = New-Object -TypeName psobject\r\n$customToken | Add-Member -MemberType NoteProperty -Name Token -Value $token.Token\r\n$customToken | Add-Member -MemberType NoteProperty -Name ExpiresOn -Value $token.ExpiresOn.ToUnixTimeSeconds()\r\n\r\n$x = $customToken | ConvertTo-Xml\r\nreturn $x.Objects.FirstChild.OuterXml\r\n" }));
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				fileName = Path.Combine(AzurePowerShellCredential.DefaultWorkingDirWindows, "cmd.exe");
				argument = string.Concat(new string[] { "/d /c \"", text, " \"", text3, "\" \" & exit" });
				return;
			}
			fileName = "/bin/sh";
			argument = string.Concat(new string[] { "-c \"", text, " \"", text3, "\" \"" });
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006214 File Offset: 0x00004414
		private static AccessToken DeserializeOutput(string output)
		{
			XDocument xdocument = XDocument.Parse(output);
			string text = null;
			DateTimeOffset dateTimeOffset = default(DateTimeOffset);
			if (((xdocument != null) ? xdocument.Root : null) == null)
			{
				throw new CredentialUnavailableException("Error parsing token response.");
			}
			foreach (XElement xelement in xdocument.Root.Elements())
			{
				XAttribute xattribute = xelement.Attribute("Name");
				string text2 = ((xattribute != null) ? xattribute.Value : null);
				if (!(text2 == "Token"))
				{
					if (text2 == "ExpiresOn")
					{
						dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(xelement.Value));
					}
				}
				else
				{
					text = xelement.Value;
				}
				if (dateTimeOffset != default(DateTimeOffset) && text != null)
				{
					break;
				}
			}
			if (text == null)
			{
				throw new CredentialUnavailableException("Error parsing token response.");
			}
			return new AccessToken(text, dateTimeOffset);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000630C File Offset: 0x0000450C
		private static string Base64Encode(string text)
		{
			return Convert.ToBase64String(Encoding.Unicode.GetBytes(text));
		}

		// Token: 0x040000FE RID: 254
		private readonly CredentialPipeline _pipeline;

		// Token: 0x040000FF RID: 255
		private readonly IProcessService _processService;

		// Token: 0x04000103 RID: 259
		private const string Troubleshooting = "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/powershellcredential/troubleshoot";

		// Token: 0x04000104 RID: 260
		internal const string AzurePowerShellFailedError = "Azure PowerShell authentication failed due to an unknown error. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/powershellcredential/troubleshoot";

		// Token: 0x04000105 RID: 261
		private const string RunConnectAzAccountToLogin = "Run Connect-AzAccount to login";

		// Token: 0x04000106 RID: 262
		private const string NoAccountsWereFoundInTheCache = "No accounts were found in the cache";

		// Token: 0x04000107 RID: 263
		private const string CannotRetrieveAccessToken = "cannot retrieve access token";

		// Token: 0x04000108 RID: 264
		private const string AzurePowerShellNoAzAccountModule = "NoAzAccountModule";

		// Token: 0x04000109 RID: 265
		private static readonly string DefaultWorkingDirWindows = Environment.GetFolderPath(Environment.SpecialFolder.System);

		// Token: 0x0400010A RID: 266
		private const string DefaultWorkingDirNonWindows = "/bin/";

		// Token: 0x0400010B RID: 267
		private static readonly string DefaultWorkingDir = (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? AzurePowerShellCredential.DefaultWorkingDirWindows : "/bin/");

		// Token: 0x0400010E RID: 270
		private readonly bool _logPII;

		// Token: 0x0400010F RID: 271
		private readonly bool _logAccountDetails;

		// Token: 0x04000110 RID: 272
		internal readonly bool _isChainedCredential;

		// Token: 0x04000111 RID: 273
		internal const string AzurePowerShellNotLogInError = "Please run 'Connect-AzAccount' to set up account.";

		// Token: 0x04000112 RID: 274
		internal const string AzurePowerShellModuleNotInstalledError = "Az.Accounts module >= 2.2.0 is not installed.";

		// Token: 0x04000113 RID: 275
		internal const string PowerShellNotInstalledError = "PowerShell is not installed.";

		// Token: 0x04000114 RID: 276
		internal const string AzurePowerShellTimeoutError = "Azure PowerShell authentication timed out.";
	}
}
