using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Internal.Broker;
using Microsoft.Identity.Client.Kerberos;
using Microsoft.Identity.Client.PlatformsCommon.Factories;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200013F RID: 319
	public sealed class PublicClientApplicationBuilder : AbstractApplicationBuilder<PublicClientApplicationBuilder>
	{
		// Token: 0x06000FFB RID: 4091 RVA: 0x0003A522 File Offset: 0x00038722
		internal PublicClientApplicationBuilder(ApplicationConfiguration configuration)
			: base(configuration)
		{
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0003A52B File Offset: 0x0003872B
		public static PublicClientApplicationBuilder CreateWithApplicationOptions(PublicClientApplicationOptions options)
		{
			return new PublicClientApplicationBuilder(new ApplicationConfiguration(MsalClientType.PublicClient)).WithOptions(options).WithKerberosTicketClaim(options.KerberosServicePrincipalName, options.TicketContainer);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0003A54F File Offset: 0x0003874F
		public static PublicClientApplicationBuilder Create(string clientId)
		{
			return new PublicClientApplicationBuilder(new ApplicationConfiguration(MsalClientType.PublicClient)).WithClientId(clientId);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0003A562 File Offset: 0x00038762
		internal PublicClientApplicationBuilder WithUserTokenLegacyCachePersistenceForTest(ILegacyCachePersistence legacyCachePersistence)
		{
			base.Config.UserTokenLegacyCachePersistenceForTest = legacyCachePersistence;
			return this;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0003A571 File Offset: 0x00038771
		public PublicClientApplicationBuilder WithDefaultRedirectUri()
		{
			base.Config.UseRecommendedDefaultRedirectUri = true;
			return this;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0003A580 File Offset: 0x00038780
		[EditorBrowsable(EditorBrowsableState.Never)]
		public PublicClientApplicationBuilder WithMultiCloudSupport(bool enableMultiCloudSupport)
		{
			base.Config.MultiCloudSupportEnabled = enableMultiCloudSupport;
			return this;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0003A58F File Offset: 0x0003878F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public PublicClientApplicationBuilder WithIosKeychainSecurityGroup(string keychainSecurityGroup)
		{
			return this;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0003A592 File Offset: 0x00038792
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("The desktop broker is not directly available in the MSAL package. Install the NuGet package Microsoft.Identity.Client.Broker and call the extension method .WithBroker(BrokerOptions). For details, see https://aka.ms/msal-net-wam", true)]
		public PublicClientApplicationBuilder WithBroker(bool enableBroker = true)
		{
			throw new PlatformNotSupportedException("The desktop broker is not directly available in the Microsoft.Identity.Client package. \n\rTo use it, install the NuGet package named Microsoft.Identity.Client.Broker and call the extension method .WithBroker(BrokerOptions) from namespace Microsoft.Identity.Client.Broker\n\rFor details see https://aka.ms/msal-net-wam ");
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0003A5A0 File Offset: 0x000387A0
		[Obsolete("This API has been replaced with WithBroker(BrokerOptions), which can be found in Microsoft.Identity.Client.Broker package. See https://aka.ms/msal-net-wam for details.", false)]
		public PublicClientApplicationBuilder WithWindowsBrokerOptions(WindowsBrokerOptions options)
		{
			WindowsBrokerOptions.ValidatePlatformAvailability();
			BrokerOptions brokerOptions = BrokerOptions.CreateFromWindowsOptions(options);
			base.Config.BrokerOptions = brokerOptions;
			return this;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0003A5C6 File Offset: 0x000387C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public PublicClientApplicationBuilder WithParentActivityOrWindow(Func<object> parentActivityOrWindowFunc)
		{
			return this.WithParentFunc(parentActivityOrWindowFunc);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0003A5CF File Offset: 0x000387CF
		private PublicClientApplicationBuilder WithParentFunc(Func<object> parentFunc)
		{
			base.Config.ParentActivityOrWindowFunc = parentFunc;
			return this;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0003A5E0 File Offset: 0x000387E0
		public PublicClientApplicationBuilder WithOidcAuthority(string authorityUri)
		{
			base.ValidateUseOfExperimentalFeature("WithOidcAuthority");
			AuthorityInfo authorityInfo = AuthorityInfo.FromGenericAuthority(authorityUri);
			base.Config.Authority = Authority.CreateAuthority(authorityInfo);
			return this;
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0003A611 File Offset: 0x00038811
		[CLSCompliant(false)]
		public PublicClientApplicationBuilder WithParentActivityOrWindow(Func<IWin32Window> windowFunc)
		{
			if (windowFunc == null)
			{
				throw new ArgumentNullException("windowFunc");
			}
			return this.WithParentFunc(windowFunc);
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0003A628 File Offset: 0x00038828
		[CLSCompliant(false)]
		public PublicClientApplicationBuilder WithParentActivityOrWindow(Func<IntPtr> windowFunc)
		{
			if (windowFunc == null)
			{
				throw new ArgumentNullException("windowFunc");
			}
			return this.WithParentFunc(() => windowFunc());
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0003A667 File Offset: 0x00038867
		public PublicClientApplicationBuilder WithKerberosTicketClaim(string servicePrincipalName, KerberosTicketContainer ticketContainer)
		{
			base.Config.KerberosServicePrincipalName = servicePrincipalName;
			base.Config.TicketContainer = ticketContainer;
			return this;
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0003A684 File Offset: 0x00038884
		public bool IsBrokerAvailable()
		{
			IBroker broker = PlatformProxyFactory.CreatePlatformProxy(null).CreateBroker(base.Config, null);
			Authority authority = base.Config.Authority;
			AuthorityType? authorityType;
			if (authority == null)
			{
				authorityType = null;
			}
			else
			{
				AuthorityInfo authorityInfo = authority.AuthorityInfo;
				authorityType = ((authorityInfo != null) ? new AuthorityType?(authorityInfo.AuthorityType) : null);
			}
			AuthorityType? authorityType2 = authorityType;
			return broker.IsBrokerInstalledAndInvokable(authorityType2.GetValueOrDefault());
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0003A6E8 File Offset: 0x000388E8
		public IPublicClientApplication Build()
		{
			return this.BuildConcrete();
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0003A6F0 File Offset: 0x000388F0
		internal PublicClientApplication BuildConcrete()
		{
			return new PublicClientApplication(this.BuildConfiguration());
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0003A700 File Offset: 0x00038900
		internal override void Validate()
		{
			base.Validate();
			if (string.IsNullOrWhiteSpace(base.Config.RedirectUri))
			{
				base.Config.RedirectUri = PlatformProxyFactory.CreatePlatformProxy(null).GetDefaultRedirectUri(base.Config.ClientId, base.Config.UseRecommendedDefaultRedirectUri);
			}
			Uri uri;
			if (!Uri.TryCreate(base.Config.RedirectUri, UriKind.Absolute, out uri))
			{
				throw new InvalidOperationException(MsalErrorMessage.InvalidRedirectUriReceived(base.Config.RedirectUri));
			}
		}
	}
}
