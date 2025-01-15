using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Host;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x0200050E RID: 1294
	internal static class ResourceKinds
	{
		// Token: 0x060029F9 RID: 10745 RVA: 0x0007D978 File Offset: 0x0007BB78
		static ResourceKinds()
		{
			ResourceKinds.AddResourceKind(ResourceKinds.environment, null);
			ResourceKinds.AddResourceKind(ResourceKinds.microsoftInformationProtection, null);
			Modules.InitializeBuiltinResources();
			ResourceKinds.protocols["soda"] = SodaDataSourceLocation.Factory;
			ResourceKinds.protocols["xmla"] = XmlaDataSourceLocation.Factory;
			ResourceKinds.protocols["x-datasource"] = InternalDataSourceDataSourceLocation.Factory;
			ResourceKinds.protocols["exchange"] = EwsDataSourceLocation.Factory;
			ResourceKinds.protocols["sql-anywhere"] = SybaseDataSourceLocation.Factory;
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x0007DA67 File Offset: 0x0007BC67
		public static ResourceKinds.Operations OperationSet()
		{
			return new ResourceKinds.Operations(ResourceKinds.lockObject);
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x0007DA74 File Offset: 0x0007BC74
		public static bool Lookup(string resourceKind, out ResourceKindInfo info)
		{
			object obj = ResourceKinds.lockObject;
			bool flag2;
			lock (obj)
			{
				flag2 = ResourceKinds.kinds.TryGetValue(resourceKind, out info);
			}
			return flag2;
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x0007DABC File Offset: 0x0007BCBC
		public static bool Lookup(string resourceKind, out ResourceKindInfo info, out string moduleName)
		{
			object obj = ResourceKinds.lockObject;
			bool flag2;
			lock (obj)
			{
				if (ResourceKinds.kinds.TryGetValue(resourceKind, out info))
				{
					if (!ResourceKinds.moduleMap.TryGetValue(resourceKind, out moduleName))
					{
						moduleName = null;
					}
					flag2 = true;
				}
				else
				{
					moduleName = null;
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x0007DB20 File Offset: 0x0007BD20
		private static void AddResourceKind(ResourceKindInfo info, string moduleName)
		{
			Exception ex;
			if (!ResourceKinds.TryAddResourceKindInternal(info, moduleName, out ex))
			{
				throw ex;
			}
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x0007DB3C File Offset: 0x0007BD3C
		public static bool TryAddResourceKind(ResourceKindInfo info, string moduleName, out Exception error)
		{
			object obj = ResourceKinds.lockObject;
			bool flag2;
			lock (obj)
			{
				flag2 = ResourceKinds.TryAddResourceKindInternal(info, moduleName, out error);
			}
			return flag2;
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x0007DB80 File Offset: 0x0007BD80
		private static bool TryAddResourceKindInternal(ResourceKindInfo info, string moduleName, out Exception error)
		{
			ResourceKindInfo resourceKindInfo;
			if (ResourceKinds.kinds.TryGetValue(info.Kind, out resourceKindInfo) && !(resourceKindInfo is ResourceKinds.DelayedResourceKindInfo))
			{
				error = new InvalidOperationException(Strings.ResourceAlreadyRegistered(info.Kind));
				return false;
			}
			if (!ResourceKinds.IsValidResourceKindText(info.Kind))
			{
				error = ValueException.NewDataFormatError<Message0>(Strings.Extensibility_MissingOrInvalidResourceKind, TextValue.New(info.Kind), null);
				return false;
			}
			foreach (IDataSourceLocationFactory dataSourceLocationFactory in info.DslFactories)
			{
				IDataSourceLocationFactory dataSourceLocationFactory2;
				if (!new HashSet<string>().Add(dataSourceLocationFactory.Protocol) || (ResourceKinds.protocols.TryGetValue(dataSourceLocationFactory.Protocol, out dataSourceLocationFactory2) && !(dataSourceLocationFactory2 is DelayedDataSourceLocationFactory)))
				{
					error = new InvalidOperationException(Strings.DuplicateProtocol(dataSourceLocationFactory.Protocol));
					return false;
				}
			}
			foreach (IDataSourceLocationFactory dataSourceLocationFactory3 in info.DslFactories)
			{
				ResourceKinds.protocols[dataSourceLocationFactory3.Protocol] = dataSourceLocationFactory3;
			}
			ResourceKinds.kinds[info.Kind] = info;
			ResourceKinds.moduleMap[info.Kind] = moduleName;
			error = null;
			return true;
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x0007DCE0 File Offset: 0x0007BEE0
		public static bool AddDelayedResourceKind(string moduleName, ResourceKindInfo info, out Exception error)
		{
			IList<IDataSourceLocationFactory> list = EmptyArray<IDataSourceLocationFactory>.Instance;
			if (info.DslFactories != null && info.DslFactories.Count > 0)
			{
				list = new List<IDataSourceLocationFactory>(info.DslFactories.Count);
				foreach (IDataSourceLocationFactory dataSourceLocationFactory in info.DslFactories)
				{
					list.Add(new DelayedDataSourceLocationFactory(dataSourceLocationFactory.Protocol, moduleName, info.Kind));
				}
			}
			return ResourceKinds.TryAddResourceKind(new ResourceKinds.DelayedResourceKindInfo(moduleName, info, list), moduleName, out error);
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x0007DD7C File Offset: 0x0007BF7C
		public static bool RemoveResourceKind(string resourceKind)
		{
			object obj = ResourceKinds.lockObject;
			bool flag2;
			lock (obj)
			{
				ResourceKindInfo resourceKindInfo;
				if (ResourceKinds.kinds.TryGetValue(resourceKind, out resourceKindInfo))
				{
					foreach (IDataSourceLocationFactory dataSourceLocationFactory in resourceKindInfo.DslFactories)
					{
						ResourceKinds.protocols.Remove(dataSourceLocationFactory.Protocol);
					}
					ResourceKinds.kinds.Remove(resourceKind);
					ResourceKinds.moduleMap.Remove(resourceKind);
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x0007DE30 File Offset: 0x0007C030
		public static bool TryGetDataSourceLocationFactory(string protocol, out IDataSourceLocationFactory factory)
		{
			object obj = ResourceKinds.lockObject;
			bool flag2;
			lock (obj)
			{
				IDataSourceLocationFactory dataSourceLocationFactory;
				if (protocol != null && ResourceKinds.protocols.TryGetValue(protocol, out dataSourceLocationFactory))
				{
					factory = dataSourceLocationFactory;
					flag2 = true;
				}
				else
				{
					factory = null;
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x0007DE88 File Offset: 0x0007C088
		public static IDataSourceLocationFactory GetDataSourceLocationFactory(string protocol)
		{
			IDataSourceLocationFactory dataSourceLocationFactory;
			if (!ResourceKinds.TryGetDataSourceLocationFactory(protocol, out dataSourceLocationFactory))
			{
				return UnrecognizedDataSourceLocation.Factory(protocol);
			}
			return dataSourceLocationFactory;
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x0007DEAC File Offset: 0x0007C0AC
		public static bool TryCreateLocationFromResource(IResource resource, bool normalize, out IDataSourceLocation location)
		{
			ResourceKindInfo resourceKindInfo;
			if (ResourceKinds.Lookup(resource.Kind, out resourceKindInfo))
			{
				using (IEnumerator<IDataSourceLocationFactory> enumerator = resourceKindInfo.DslFactories.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.TryCreateFromResource(resource, normalize, out location))
						{
							if (string.IsNullOrEmpty(location.Protocol))
							{
								throw new InvalidOperationException("Protocol shouldn't be null or empty.");
							}
							return true;
						}
					}
				}
			}
			location = null;
			return false;
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x0007DF30 File Offset: 0x0007C130
		public static ResourceKindInfo[] GetAll()
		{
			object obj = ResourceKinds.lockObject;
			ResourceKindInfo[] array;
			lock (obj)
			{
				array = ResourceKinds.kinds.Values.ToArray<ResourceKindInfo>();
			}
			return array;
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x0007DF7C File Offset: 0x0007C17C
		private static bool IsValidResourceKindText(string text)
		{
			if (text.Length == 0 || char.IsWhiteSpace(text[0]) || char.IsWhiteSpace(text[text.Length - 1]))
			{
				return false;
			}
			foreach (char c in text)
			{
				char.GetUnicodeCategory(c);
				if (char.IsControl(c) || char.IsHighSurrogate(c) || char.IsLowSurrogate(c) || char.IsSurrogate(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001242 RID: 4674
		private static readonly object lockObject = new object();

		// Token: 0x04001243 RID: 4675
		private static readonly Dictionary<string, ResourceKindInfo> kinds = new Dictionary<string, ResourceKindInfo>();

		// Token: 0x04001244 RID: 4676
		private static readonly Dictionary<string, IDataSourceLocationFactory> protocols = new Dictionary<string, IDataSourceLocationFactory>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001245 RID: 4677
		private static readonly Dictionary<string, string> moduleMap = new Dictionary<string, string>();

		// Token: 0x04001246 RID: 4678
		private static ResourceKindInfo environment = new SingletonResourceKindInfo("Environment", "Environment", "Environment", new AuthenticationInfo[] { ResourceHelpers.AnonymousAuth }, null, null, false, false, null);

		// Token: 0x04001247 RID: 4679
		private static readonly ResourceKindInfo microsoftInformationProtection = new ResourceKinds.InformationProtectionResourceKindInfo();

		// Token: 0x0200050F RID: 1295
		public struct Operations : IDisposable
		{
			// Token: 0x06002A07 RID: 10759 RVA: 0x0007DFFB File Offset: 0x0007C1FB
			public Operations(object lockObject)
			{
				this.lockObject = lockObject;
				Monitor.Enter(lockObject);
			}

			// Token: 0x06002A08 RID: 10760 RVA: 0x0007E00A File Offset: 0x0007C20A
			public void AddResourceKind(ResourceKindInfo resourceKindInfo, string moduleName)
			{
				if (this.lockObject == null)
				{
					throw new InvalidOperationException();
				}
				ResourceKinds.AddResourceKind(resourceKindInfo, moduleName);
			}

			// Token: 0x06002A09 RID: 10761 RVA: 0x0007E021 File Offset: 0x0007C221
			void IDisposable.Dispose()
			{
				if (this.lockObject != null)
				{
					Monitor.Exit(this.lockObject);
					this.lockObject = null;
				}
			}

			// Token: 0x04001248 RID: 4680
			private object lockObject;
		}

		// Token: 0x02000510 RID: 1296
		private class DelayedResourceKindInfo : ResourceKindInfo
		{
			// Token: 0x06002A0A RID: 10762 RVA: 0x0007E040 File Offset: 0x0007C240
			public DelayedResourceKindInfo(string moduleName, ResourceKindInfo initial, IEnumerable<IDataSourceLocationFactory> dslFactories)
				: base(initial.Kind, initial.Label, initial.IsUri, initial.IsDatabase, initial.IsSingleton, initial.SupportsEncryptedConnection, initial.SupportsConnectionString, initial.SupportsNativeQueryChallenge, null, null, initial.PermissionKinds, null, dslFactories)
			{
				this.moduleName = moduleName;
				base.InitializeAuthenticationInfo(initial.AuthenticationInfo.Select(new Func<AuthenticationInfo, AuthenticationInfo>(this.WrapForOAuth)));
			}

			// Token: 0x17001010 RID: 4112
			// (get) Token: 0x06002A0B RID: 10763 RVA: 0x0007E0B1 File Offset: 0x0007C2B1
			private ResourceKindInfo ResourceKindInfo
			{
				get
				{
					if (this.resourceKindInfo == null)
					{
						this.resourceKindInfo = this.LoadResourceKindInfo();
					}
					return this.resourceKindInfo;
				}
			}

			// Token: 0x06002A0C RID: 10764 RVA: 0x0007E0CD File Offset: 0x0007C2CD
			public override string CreateTestFormula(string resourcePath)
			{
				return this.ResourceKindInfo.CreateTestFormula(resourcePath);
			}

			// Token: 0x06002A0D RID: 10765 RVA: 0x0007E0DB File Offset: 0x0007C2DB
			public override IEnumerable<string> EnumerateKnownSupersets(string resourcePath)
			{
				return this.ResourceKindInfo.EnumerateKnownSupersets(resourcePath);
			}

			// Token: 0x06002A0E RID: 10766 RVA: 0x0007E0E9 File Offset: 0x0007C2E9
			public override IEnumerable<KeyValuePair<string, string>> GetPartLabels(string resourcePath)
			{
				return this.ResourceKindInfo.GetPartLabels(resourcePath);
			}

			// Token: 0x06002A0F RID: 10767 RVA: 0x0007E0F7 File Offset: 0x0007C2F7
			public override bool IsSubset(string permittedResourcePath, string attemptedResourcePath)
			{
				return this.ResourceKindInfo.IsSubset(permittedResourcePath, attemptedResourcePath);
			}

			// Token: 0x06002A10 RID: 10768 RVA: 0x0007E106 File Offset: 0x0007C306
			public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
			{
				return this.ResourceKindInfo.Validate(resourcePath, out resource, out errorMessage);
			}

			// Token: 0x06002A11 RID: 10769 RVA: 0x0007E116 File Offset: 0x0007C316
			public override bool TryGetHostName(string resourcePath, out string hostName)
			{
				return this.ResourceKindInfo.TryGetHostName(resourcePath, out hostName);
			}

			// Token: 0x06002A12 RID: 10770 RVA: 0x0007E125 File Offset: 0x0007C325
			public override bool CanRefresh(IResourceCredential credential)
			{
				return this.ResourceKindInfo.CanRefresh(credential);
			}

			// Token: 0x06002A13 RID: 10771 RVA: 0x0007E134 File Offset: 0x0007C334
			private ResourceKindInfo LoadResourceKindInfo()
			{
				Modules.EnsureLoaded(EngineHost.Empty, this.moduleName);
				ResourceKindInfo resourceKindInfo;
				if (!ResourceKinds.Lookup(base.Kind, out resourceKindInfo))
				{
					throw new InvalidOperationException("DelayedResourceKindInfo.LoadResourceKindInfo");
				}
				return resourceKindInfo;
			}

			// Token: 0x06002A14 RID: 10772 RVA: 0x0007E16C File Offset: 0x0007C36C
			private AuthenticationInfo WrapForOAuth(AuthenticationInfo info)
			{
				if (info.AuthenticationKind != AuthenticationKind.OAuth2 || info is AadAuthenticationInfo)
				{
					return info;
				}
				OAuth2AuthenticationInfo oauth2AuthenticationInfo = (OAuth2AuthenticationInfo)info;
				return new OAuth2AuthenticationInfo
				{
					ClientApplicationType = oauth2AuthenticationInfo.ClientApplicationType,
					Description = oauth2AuthenticationInfo.Description,
					Label = oauth2AuthenticationInfo.Label,
					ProviderFactory = new ResourceKinds.DelayedResourceKindInfo.DelayedOAuthFactory(this)
				};
			}

			// Token: 0x04001249 RID: 4681
			private readonly string moduleName;

			// Token: 0x0400124A RID: 4682
			private ResourceKindInfo resourceKindInfo;

			// Token: 0x02000511 RID: 1297
			private class DelayedOAuthFactory : IOAuthFactory
			{
				// Token: 0x17001011 RID: 4113
				// (get) Token: 0x06002A15 RID: 10773 RVA: 0x0007E1C8 File Offset: 0x0007C3C8
				private IOAuthFactory OAuthFactory
				{
					get
					{
						if (this.oauthFactory == null)
						{
							OAuth2AuthenticationInfo oauth2AuthenticationInfo = this.resourceKindInfo.ResourceKindInfo.AuthenticationInfo.FirstOrDefault((AuthenticationInfo a) => a.AuthenticationKind == AuthenticationKind.OAuth2) as OAuth2AuthenticationInfo;
							if (oauth2AuthenticationInfo == null)
							{
								throw new InvalidOperationException();
							}
							this.oauthFactory = oauth2AuthenticationInfo.ProviderFactory;
						}
						return this.oauthFactory;
					}
				}

				// Token: 0x06002A16 RID: 10774 RVA: 0x0007E232 File Offset: 0x0007C432
				public DelayedOAuthFactory(ResourceKinds.DelayedResourceKindInfo resourceKindInfo)
				{
					this.resourceKindInfo = resourceKindInfo;
				}

				// Token: 0x06002A17 RID: 10775 RVA: 0x0007E241 File Offset: 0x0007C441
				public object CreateProvider(IEngineHost engineHost, IEngine engine, object clientApplication, string resourceUrl)
				{
					return this.OAuthFactory.CreateProvider(engineHost, engine, clientApplication, resourceUrl);
				}

				// Token: 0x06002A18 RID: 10776 RVA: 0x0007E253 File Offset: 0x0007C453
				public object CreateResource(IEngineHost engineHost, IEngine engine, string resourceUrl)
				{
					return this.OAuthFactory.CreateResource(engineHost, engine, resourceUrl);
				}

				// Token: 0x0400124B RID: 4683
				private readonly ResourceKinds.DelayedResourceKindInfo resourceKindInfo;

				// Token: 0x0400124C RID: 4684
				private IOAuthFactory oauthFactory;
			}
		}

		// Token: 0x02000513 RID: 1299
		private class InformationProtectionResourceKindInfo : ResourceKindInfo
		{
			// Token: 0x06002A1C RID: 10780 RVA: 0x0007E27C File Offset: 0x0007C47C
			public InformationProtectionResourceKindInfo()
				: base("MicrosoftInformationProtection", "MicrosoftInformationProtection", false, false, false, false, false, false, new AuthenticationInfo[]
				{
					new AadAuthenticationInfo
					{
						ClientApplicationType = OAuthClientApplicationType.Required,
						ProviderFactory = new ResourceKinds.InformationProtectionResourceKindInfo.MipOAuthFactory()
					}
				}, null, null, null, null)
			{
			}

			// Token: 0x06002A1D RID: 10781 RVA: 0x0007D355 File Offset: 0x0007B555
			public override bool TryGetHostName(string resourcePath, out string hostName)
			{
				hostName = null;
				return false;
			}

			// Token: 0x06002A1E RID: 10782 RVA: 0x0007E2C3 File Offset: 0x0007C4C3
			public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
			{
				resource = new Resource("MicrosoftInformationProtection", resourcePath, resourcePath);
				errorMessage = null;
				return true;
			}

			// Token: 0x02000514 RID: 1300
			private class MipOAuthFactory : IOAuthFactory
			{
				// Token: 0x06002A1F RID: 10783 RVA: 0x0007E2D8 File Offset: 0x0007C4D8
				public object CreateProvider(IEngineHost engineHost, IEngine engine, object clientApplication, string resourceUrl)
				{
					OAuthServices oauthServices = OAuthFactory.CreateServices(engineHost);
					return new AadOAuthProvider(oauthServices, (OAuthClientApplication)clientApplication, this.CreateResource(engineHost, oauthServices, resourceUrl));
				}

				// Token: 0x06002A20 RID: 10784 RVA: 0x0007E302 File Offset: 0x0007C502
				public object CreateResource(IEngineHost engineHost, IEngine engine, string resourceUrl)
				{
					return this.CreateResource(engineHost, OAuthFactory.CreateServices(engineHost), resourceUrl);
				}

				// Token: 0x06002A21 RID: 10785 RVA: 0x0007E314 File Offset: 0x0007C514
				private OAuthResource CreateResource(IEngineHost engineHost, OAuthServices services, string resourceUrl)
				{
					OAuthResource oauthResource;
					try
					{
						RecordValue asRecord = JsonParser.Parse(new StringReader(resourceUrl), null).AsRecord;
						string asString = asRecord["Authority"].AsText.AsString;
						string asString2 = asRecord["Resource"].AsText.AsString;
						oauthResource = OAuthResource.CreateResource(new Uri(asString + "/oauth2/authorize"), new Uri(asString + "/oauth2/token"), new Uri(asString + "/oauth2/logout"), asString2, string.Empty);
					}
					catch (ValueException)
					{
						oauthResource = null;
					}
					return oauthResource;
				}
			}
		}
	}
}
