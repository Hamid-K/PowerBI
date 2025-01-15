using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Identity
{
	// Token: 0x02000055 RID: 85
	public class VisualStudioCredential : TokenCredential
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000301 RID: 769 RVA: 0x000097F2 File Offset: 0x000079F2
		internal string TenantId { get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000302 RID: 770 RVA: 0x000097FA File Offset: 0x000079FA
		internal string[] AdditionallyAllowedTenantIds { get; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00009802 File Offset: 0x00007A02
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000980A File Offset: 0x00007A0A
		internal TimeSpan ProcessTimeout { get; private set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00009813 File Offset: 0x00007A13
		internal TenantIdResolverBase TenantIdResolver { get; }

		// Token: 0x06000306 RID: 774 RVA: 0x0000981B File Offset: 0x00007A1B
		public VisualStudioCredential()
			: this(null)
		{
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00009824 File Offset: 0x00007A24
		public VisualStudioCredential(VisualStudioCredentialOptions options)
			: this((options != null) ? options.TenantId : null, CredentialPipeline.GetInstance(options, false), null, null, options)
		{
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00009844 File Offset: 0x00007A44
		internal VisualStudioCredential(string tenantId, CredentialPipeline pipeline, IFileSystemService fileSystem, IProcessService processService, VisualStudioCredentialOptions options = null)
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
			this.TenantId = tenantId;
			this._pipeline = pipeline ?? CredentialPipeline.GetInstance(null, false);
			this._fileSystem = fileSystem ?? FileSystemService.Default;
			this._processService = processService ?? ProcessService.Default;
			this.TenantIdResolver = ((options != null) ? options.TenantIdResolver : null) ?? TenantIdResolverBase.Default;
			this.AdditionallyAllowedTenantIds = this.TenantIdResolver.ResolveAddionallyAllowedTenantIds((options != null) ? ((ISupportsAdditionallyAllowedTenants)options).AdditionallyAllowedTenants : null);
			this.ProcessTimeout = ((options != null) ? options.ProcessTimeout : null) ?? TimeSpan.FromSeconds(30.0);
			this._isChainedCredential = options != null && options.IsChainedCredential;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00009970 File Offset: 0x00007B70
		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return await this.GetTokenImplAsync(requestContext, true, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000099C3 File Offset: 0x00007BC3
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return this.GetTokenImplAsync(requestContext, false, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000099D4 File Offset: 0x00007BD4
		private ValueTask<AccessToken> GetTokenImplAsync(TokenRequestContext requestContext, bool async, CancellationToken cancellationToken)
		{
			VisualStudioCredential.<GetTokenImplAsync>d__27 <GetTokenImplAsync>d__;
			<GetTokenImplAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenImplAsync>d__.<>4__this = this;
			<GetTokenImplAsync>d__.requestContext = requestContext;
			<GetTokenImplAsync>d__.async = async;
			<GetTokenImplAsync>d__.cancellationToken = cancellationToken;
			<GetTokenImplAsync>d__.<>1__state = -1;
			<GetTokenImplAsync>d__.<>t__builder.Start<VisualStudioCredential.<GetTokenImplAsync>d__27>(ref <GetTokenImplAsync>d__);
			return <GetTokenImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00009A30 File Offset: 0x00007C30
		private static string GetTokenProviderPath()
		{
			string text;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				text = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				if (string.IsNullOrEmpty(text))
				{
					text = Environment.GetEnvironmentVariable("LOCALAPPDATA");
					if (string.IsNullOrEmpty(text))
					{
						throw new CredentialUnavailableException("Can't find the Local Application Data folder. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/vscredential/troubleshoot");
					}
				}
			}
			else
			{
				text = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
			}
			return Path.Combine(text, VisualStudioCredential.TokenProviderFilePath);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00009A8C File Offset: 0x00007C8C
		private async Task<AccessToken> RunProcessesAsync(List<ProcessStartInfo> processStartInfos, bool async, CancellationToken cancellationToken)
		{
			List<Exception> exceptions = new List<Exception>();
			foreach (ProcessStartInfo processStartInfo in processStartInfos)
			{
				string output = string.Empty;
				try
				{
					using (ProcessRunner processRunner = new ProcessRunner(this._processService.Create(processStartInfo), this.ProcessTimeout, this._logPII, cancellationToken))
					{
						string text;
						if (async)
						{
							text = await processRunner.RunAsync().ConfigureAwait(false);
						}
						else
						{
							text = processRunner.Run();
						}
						output = text;
						JsonElement rootElement = JsonDocument.Parse(output, default(JsonDocumentOptions)).RootElement;
						string @string = rootElement.GetProperty("access_token").GetString();
						DateTimeOffset dateTimeOffset = rootElement.GetProperty("expires_on").GetDateTimeOffset();
						return new AccessToken(@string, dateTimeOffset);
					}
				}
				catch (OperationCanceledException obj) when (!cancellationToken.IsCancellationRequested)
				{
					exceptions.Add(new CredentialUnavailableException(string.Format("Process \"{0}\" has failed to get access token in {1} seconds.", processStartInfo.FileName, this.ProcessTimeout.TotalSeconds)));
				}
				catch (JsonException ex)
				{
					exceptions.Add(new CredentialUnavailableException(string.Concat(new string[] { "Process \"", processStartInfo.FileName, "\" has non-json output: ", output, "." }), ex));
				}
				catch (Exception ex2) when (!(ex2 is OperationCanceledException))
				{
					if (this._isChainedCredential)
					{
						exceptions.Add(new CredentialUnavailableException(string.Concat(new string[] { "Process \"", processStartInfo.FileName, "\" has failed with unexpected error: ", ex2.Message, "." }), ex2));
					}
					else
					{
						exceptions.Add(new AuthenticationFailedException(string.Concat(new string[] { "Process \"", processStartInfo.FileName, "\" has failed with unexpected error: ", ex2.Message, "." }), ex2));
					}
				}
				output = null;
				processStartInfo = null;
			}
			List<ProcessStartInfo>.Enumerator enumerator = default(List<ProcessStartInfo>.Enumerator);
			int count = exceptions.Count;
			if (count == 0)
			{
				throw new CredentialUnavailableException("No installed instance of Visual Studio was able to get credentials.");
			}
			if (count != 1)
			{
				throw new AggregateException(exceptions);
			}
			ExceptionDispatchInfo.Capture(exceptions[0]).Throw();
			return default(AccessToken);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00009AE8 File Offset: 0x00007CE8
		private List<ProcessStartInfo> GetProcessStartInfos(VisualStudioCredential.VisualStudioTokenProvider[] visualStudioTokenProviders, string resource, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			List<ProcessStartInfo> list = new List<ProcessStartInfo>();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (VisualStudioCredential.VisualStudioTokenProvider visualStudioTokenProvider in visualStudioTokenProviders)
			{
				cancellationToken.ThrowIfCancellationRequested();
				if (this._fileSystem.FileExists(visualStudioTokenProvider.Path))
				{
					stringBuilder.Clear();
					string[] arguments = visualStudioTokenProvider.Arguments;
					if (arguments != null && arguments.Length != 0)
					{
						foreach (string text in visualStudioTokenProvider.Arguments)
						{
							stringBuilder.Append(text).Append(' ');
						}
					}
					stringBuilder.Append("--resource").Append(' ').Append(resource);
					string text2 = this.TenantIdResolver.Resolve(this.TenantId, requestContext, this.AdditionallyAllowedTenantIds);
					if (text2 != null)
					{
						stringBuilder.Append(' ').Append("--tenant").Append(' ')
							.Append(text2);
					}
					ProcessStartInfo processStartInfo = new ProcessStartInfo
					{
						FileName = visualStudioTokenProvider.Path,
						Arguments = stringBuilder.ToString(),
						ErrorDialog = false,
						CreateNoWindow = true
					};
					list.Add(processStartInfo);
				}
			}
			return list;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00009C1C File Offset: 0x00007E1C
		private VisualStudioCredential.VisualStudioTokenProvider[] GetTokenProviders(string tokenProviderPath)
		{
			string tokenProviderContent = this.GetTokenProviderContent(tokenProviderPath);
			VisualStudioCredential.VisualStudioTokenProvider[] array2;
			try
			{
				using (JsonDocument jsonDocument = JsonDocument.Parse(tokenProviderContent, default(JsonDocumentOptions)))
				{
					JsonElement property = jsonDocument.RootElement.GetProperty("TokenProviders");
					VisualStudioCredential.VisualStudioTokenProvider[] array = new VisualStudioCredential.VisualStudioTokenProvider[property.GetArrayLength()];
					for (int i = 0; i < array.Length; i++)
					{
						JsonElement jsonElement = property[i];
						string @string = jsonElement.GetProperty("Path").GetString();
						int @int = jsonElement.GetProperty("Preference").GetInt32();
						string[] stringArrayPropertyValue = VisualStudioCredential.GetStringArrayPropertyValue(jsonElement, "Arguments");
						array[i] = new VisualStudioCredential.VisualStudioTokenProvider(@string, stringArrayPropertyValue, @int);
					}
					Array.Sort<VisualStudioCredential.VisualStudioTokenProvider>(array);
					array2 = array;
				}
			}
			catch (JsonException ex)
			{
				throw new CredentialUnavailableException("File found at \"" + tokenProviderPath + "\" isn't a valid JSON file", ex);
			}
			catch (Exception ex2)
			{
				throw new CredentialUnavailableException("JSON file found at \"" + tokenProviderPath + "\" has invalid schema.", ex2);
			}
			return array2;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00009D44 File Offset: 0x00007F44
		private string GetTokenProviderContent(string tokenProviderPath)
		{
			string text;
			try
			{
				text = this._fileSystem.ReadAllText(tokenProviderPath);
			}
			catch (FileNotFoundException ex)
			{
				throw new CredentialUnavailableException("Visual Studio Token provider file not found at " + tokenProviderPath, ex);
			}
			catch (IOException ex2)
			{
				throw new CredentialUnavailableException("Visual Studio Token provider can't be accessed at " + tokenProviderPath, ex2);
			}
			return text;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00009DA4 File Offset: 0x00007FA4
		private static string[] GetStringArrayPropertyValue(JsonElement element, string name)
		{
			JsonElement property = element.GetProperty(name);
			string[] array = new string[property.GetArrayLength()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = property[i].GetString();
			}
			return array;
		}

		// Token: 0x040001DE RID: 478
		private static readonly string TokenProviderFilePath = Path.Combine(".IdentityService", "AzureServiceAuth", "tokenprovider.json");

		// Token: 0x040001DF RID: 479
		private const string ResourceArgumentName = "--resource";

		// Token: 0x040001E0 RID: 480
		private const string TenantArgumentName = "--tenant";

		// Token: 0x040001E1 RID: 481
		private readonly CredentialPipeline _pipeline;

		// Token: 0x040001E4 RID: 484
		private readonly IFileSystemService _fileSystem;

		// Token: 0x040001E5 RID: 485
		private readonly IProcessService _processService;

		// Token: 0x040001E6 RID: 486
		private readonly bool _logPII;

		// Token: 0x040001E7 RID: 487
		private readonly bool _logAccountDetails;

		// Token: 0x040001E8 RID: 488
		internal bool _isChainedCredential;

		// Token: 0x020000E6 RID: 230
		private readonly struct VisualStudioTokenProvider : IComparable<VisualStudioCredential.VisualStudioTokenProvider>
		{
			// Token: 0x17000154 RID: 340
			// (get) Token: 0x06000580 RID: 1408 RVA: 0x00015CA2 File Offset: 0x00013EA2
			public string Path { get; }

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x06000581 RID: 1409 RVA: 0x00015CAA File Offset: 0x00013EAA
			public string[] Arguments { get; }

			// Token: 0x06000582 RID: 1410 RVA: 0x00015CB2 File Offset: 0x00013EB2
			public VisualStudioTokenProvider(string path, string[] arguments, int preference)
			{
				this.Path = path;
				this.Arguments = arguments;
				this._preference = preference;
			}

			// Token: 0x06000583 RID: 1411 RVA: 0x00015CCC File Offset: 0x00013ECC
			public int CompareTo(VisualStudioCredential.VisualStudioTokenProvider other)
			{
				return this._preference.CompareTo(other._preference);
			}

			// Token: 0x0400048C RID: 1164
			private readonly int _preference;
		}
	}
}
