using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Resources;

namespace Microsoft.Mashup.Engine1.Library.AdoDotNet
{
	// Token: 0x02000F49 RID: 3913
	internal class AdoDotNetResourceKindInfo : GenericProviderResourceKindInfo
	{
		// Token: 0x06006763 RID: 26467 RVA: 0x00163C24 File Offset: 0x00161E24
		private AdoDotNetResourceKindInfo()
			: base("AdoDotNet", Strings.AdoDotNetChallengeTitle, AdoDotNetEnvironment.ConnectionString, new AuthenticationInfo[]
			{
				new UsernamePasswordAuthenticationInfo
				{
					Description = Strings.AdoDotNetSqlAuth
				},
				new ImplicitAuthenticationInfo
				{
					Label = Strings.GenericProvidersNoneAuthLabel,
					Description = Strings.AdoDotNetNoneAuth
				},
				new WindowsAuthenticationInfo
				{
					Description = Strings.AdoDotNetWindowsAuth,
					SupportsAlternateCredentials = true
				}
			}, new DataSourceLocationFactory[] { AdoDotNetDataSourceLocation.Factory })
		{
		}

		// Token: 0x06006764 RID: 26468 RVA: 0x00163CC0 File Offset: 0x00161EC0
		public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
		{
			string text;
			string text2;
			if (this.TryGetProvider(resourcePath, out text, out text2))
			{
				return this.TryCreateResource(text, text2, out resource, out errorMessage);
			}
			errorMessage = Strings.GenericProvider_InvalidResourcePath;
			resource = null;
			return false;
		}

		// Token: 0x06006765 RID: 26469 RVA: 0x00163CF8 File Offset: 0x00161EF8
		public override bool TryGetHostName(string resourcePath, out string hostName)
		{
			string text;
			string text2;
			if (!this.TryGetProvider(resourcePath, out text, out text2))
			{
				hostName = null;
				return false;
			}
			return AdoDotNetEnvironment.ConnectionString.TryGetHostName(text2, out hostName);
		}

		// Token: 0x06006766 RID: 26470 RVA: 0x00163D24 File Offset: 0x00161F24
		public bool TryGetProvider(string resourcePath, out string providerName, out string connectionString)
		{
			int num = resourcePath.IndexOf('/');
			if (num != -1 && num < resourcePath.Length - 1)
			{
				providerName = resourcePath.Substring(0, num);
				connectionString = resourcePath.Substring(num + 1, resourcePath.Length - num - 1);
				return true;
			}
			providerName = null;
			connectionString = null;
			return false;
		}

		// Token: 0x06006767 RID: 26471 RVA: 0x00163D74 File Offset: 0x00161F74
		public bool TryCreateResource(string providerName, string connectionString, out IResource resource, out string errorMessage)
		{
			errorMessage = Strings.GenericProvider_InvalidResourcePath;
			string text;
			if (providerName.Length > 0 && connectionString.Length > 0 && base.TryNormalizeConnectionString(connectionString, out text, out errorMessage))
			{
				string text2 = providerName + "/" + text;
				string text3 = providerName + "/" + connectionString;
				resource = new Resource(base.Kind, text2, text3);
				return true;
			}
			resource = null;
			return false;
		}

		// Token: 0x040038D6 RID: 14550
		private const char Separator = '/';

		// Token: 0x040038D7 RID: 14551
		public static AdoDotNetResourceKindInfo Instance = new AdoDotNetResourceKindInfo();
	}
}
