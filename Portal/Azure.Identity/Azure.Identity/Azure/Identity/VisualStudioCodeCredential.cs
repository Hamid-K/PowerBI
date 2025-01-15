using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x02000053 RID: 83
	public class VisualStudioCodeCredential : TokenCredential
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000093F1 File Offset: 0x000075F1
		internal string TenantId { get; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002EE RID: 750 RVA: 0x000093F9 File Offset: 0x000075F9
		internal string[] AdditionallyAllowedTenantIds { get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00009401 File Offset: 0x00007601
		internal MsalPublicClient Client { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00009409 File Offset: 0x00007609
		internal TenantIdResolverBase TenantIdResolver { get; }

		// Token: 0x060002F1 RID: 753 RVA: 0x00009411 File Offset: 0x00007611
		public VisualStudioCodeCredential()
			: this(null, null, null, null, null)
		{
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000941E File Offset: 0x0000761E
		public VisualStudioCodeCredential(VisualStudioCodeCredentialOptions options)
			: this(options, null, null, null, null)
		{
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000942C File Offset: 0x0000762C
		internal VisualStudioCodeCredential(VisualStudioCodeCredentialOptions options, CredentialPipeline pipeline, MsalPublicClient client, IFileSystemService fileSystem, IVisualStudioCodeAdapter vscAdapter)
		{
			this.TenantId = ((options != null) ? options.TenantId : null);
			this._pipeline = pipeline ?? CredentialPipeline.GetInstance(options, false);
			this.Client = client ?? new MsalPublicClient(this._pipeline, this.TenantId, "aebc6443-996d-45c2-90f0-388ff96faa56", null, options);
			this._fileSystem = fileSystem ?? FileSystemService.Default;
			this._vscAdapter = vscAdapter ?? VisualStudioCodeCredential.GetVscAdapter();
			this.TenantIdResolver = ((options != null) ? options.TenantIdResolver : null) ?? TenantIdResolverBase.Default;
			this.AdditionallyAllowedTenantIds = this.TenantIdResolver.ResolveAddionallyAllowedTenantIds((options != null) ? ((ISupportsAdditionallyAllowedTenants)options).AdditionallyAllowedTenants : null);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000094E4 File Offset: 0x000076E4
		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return await this.GetTokenImplAsync(requestContext, true, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00009537 File Offset: 0x00007737
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return this.GetTokenImplAsync(requestContext, false, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00009548 File Offset: 0x00007748
		private ValueTask<AccessToken> GetTokenImplAsync(TokenRequestContext requestContext, bool async, CancellationToken cancellationToken)
		{
			VisualStudioCodeCredential.<GetTokenImplAsync>d__24 <GetTokenImplAsync>d__;
			<GetTokenImplAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenImplAsync>d__.<>4__this = this;
			<GetTokenImplAsync>d__.requestContext = requestContext;
			<GetTokenImplAsync>d__.async = async;
			<GetTokenImplAsync>d__.cancellationToken = cancellationToken;
			<GetTokenImplAsync>d__.<>1__state = -1;
			<GetTokenImplAsync>d__.<>t__builder.Start<VisualStudioCodeCredential.<GetTokenImplAsync>d__24>(ref <GetTokenImplAsync>d__);
			return <GetTokenImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000095A4 File Offset: 0x000077A4
		private string GetStoredCredentials(string environmentName)
		{
			string text;
			try
			{
				string credentials = this._vscAdapter.GetCredentials("VS Code Azure", environmentName);
				if (!VisualStudioCodeCredential.IsRefreshTokenString(credentials))
				{
					throw new CredentialUnavailableException("Need to re-authenticate user in VSCode Azure Account.");
				}
				text = credentials;
			}
			catch (Exception ex) when (!(ex is OperationCanceledException) && !(ex is CredentialUnavailableException))
			{
				throw new CredentialUnavailableException("Stored credentials not found. Need to authenticate user in VSCode Azure Account. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/vscodecredential/troubleshoot", ex);
			}
			return text;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00009620 File Offset: 0x00007820
		private static bool IsRefreshTokenString(string str)
		{
			foreach (uint num in str)
			{
				if ((num < 48U || num > 57U) && (num < 65U || num > 90U) && (num < 97U || num > 122U) && num != 95U && num != 45U && num != 46U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00009678 File Offset: 0x00007878
		private void GetUserSettings(out string tenant, out string environmentName)
		{
			string userSettingsPath = this._vscAdapter.GetUserSettingsPath();
			tenant = this.TenantId ?? "common";
			environmentName = "AzureCloud";
			try
			{
				JsonElement rootElement = JsonDocument.Parse(this._fileSystem.ReadAllText(userSettingsPath), default(JsonDocumentOptions)).RootElement;
				JsonElement jsonElement;
				if (rootElement.TryGetProperty("azure.tenant", ref jsonElement))
				{
					tenant = jsonElement.GetString();
				}
				JsonElement jsonElement2;
				if (rootElement.TryGetProperty("azure.cloud", ref jsonElement2))
				{
					environmentName = jsonElement2.GetString();
				}
			}
			catch (IOException)
			{
			}
			catch (JsonException)
			{
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00009720 File Offset: 0x00007920
		private static IVisualStudioCodeAdapter GetVscAdapter()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				return new WindowsVisualStudioCodeAdapter();
			}
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				return new MacosVisualStudioCodeAdapter();
			}
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				return new LinuxVisualStudioCodeAdapter();
			}
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00009760 File Offset: 0x00007960
		private static AzureCloudInstance GetAzureCloudInstance(string name)
		{
			AzureCloudInstance azureCloudInstance;
			if (!(name == "AzureCloud"))
			{
				if (!(name == "AzureChina"))
				{
					if (!(name == "AzureGermanCloud"))
					{
						if (!(name == "AzureUSGovernment"))
						{
							azureCloudInstance = AzureCloudInstance.AzurePublic;
						}
						else
						{
							azureCloudInstance = AzureCloudInstance.AzureUsGovernment;
						}
					}
					else
					{
						azureCloudInstance = AzureCloudInstance.AzureGermany;
					}
				}
				else
				{
					azureCloudInstance = AzureCloudInstance.AzureChina;
				}
			}
			else
			{
				azureCloudInstance = AzureCloudInstance.AzurePublic;
			}
			return azureCloudInstance;
		}

		// Token: 0x040001D1 RID: 465
		private const string CredentialsSection = "VS Code Azure";

		// Token: 0x040001D2 RID: 466
		private const string ClientId = "aebc6443-996d-45c2-90f0-388ff96faa56";

		// Token: 0x040001D3 RID: 467
		private readonly IVisualStudioCodeAdapter _vscAdapter;

		// Token: 0x040001D4 RID: 468
		private readonly IFileSystemService _fileSystem;

		// Token: 0x040001D5 RID: 469
		private readonly CredentialPipeline _pipeline;

		// Token: 0x040001D8 RID: 472
		private const string _commonTenant = "common";

		// Token: 0x040001D9 RID: 473
		private const string Troubleshooting = "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/vscodecredential/troubleshoot";
	}
}
