using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FDF RID: 4063
	internal class ActiveDirectoryServiceAccessor
	{
		// Token: 0x06006A8A RID: 27274 RVA: 0x0016E9CC File Offset: 0x0016CBCC
		public static ActiveDirectoryServiceAccessor New(IEngineHost host, IActiveDirectoryService service, string hostName)
		{
			string text = null;
			string text2;
			if (hostName == null)
			{
				text = service.ComputerDomainName;
				if (string.IsNullOrEmpty(text))
				{
					throw ActiveDirectoryExceptions.NewComputerIsNotDomainJoinedException();
				}
				text2 = text;
			}
			else
			{
				if (!ActiveDirectoryModule.IsValidDomainName(hostName))
				{
					throw ActiveDirectoryExceptions.NewInvalidDomainNameException(host, new Resource("ActiveDirectory", hostName, hostName));
				}
				text2 = hostName;
			}
			IResource resource = new Resource("ActiveDirectory", text2, text2);
			ActiveDirectoryTracingService activeDirectoryTracingService = new ActiveDirectoryTracingService(host, resource, service);
			ActiveDirectoryCachingService activeDirectoryCachingService = new ActiveDirectoryCachingService(host, activeDirectoryTracingService);
			ActiveDirectoryRootServiceEntry directoryRootServiceEntry = ActiveDirectoryServiceAccessor.GetDirectoryRootServiceEntry(host, activeDirectoryCachingService, text2);
			if (text == null)
			{
				text = LdapPath.GetDomainName(directoryRootServiceEntry.DefaultNamingContext);
			}
			LdapPath ldapPath = new LdapPath(text, directoryRootServiceEntry.SchemaNamingContext);
			LdapPath ldapPath2 = new LdapPath(text, directoryRootServiceEntry.ConfigurationNamingContext).AddPartToDistinguishedName("CN=Partitions");
			return new ActiveDirectoryServiceAccessor(host, resource, activeDirectoryCachingService, text, text2, ldapPath, ldapPath2);
		}

		// Token: 0x06006A8B RID: 27275 RVA: 0x0016EA89 File Offset: 0x0016CC89
		private ActiveDirectoryServiceAccessor(IEngineHost host, IResource resource, ActiveDirectoryCachingService service, string fullyQualifiedDomainName, string credentialsHostName, LdapPath schemaPath, LdapPath partitionPath)
		{
			this.host = host;
			this.resource = resource;
			this.service = service;
			this.fullyQualifiedDomainName = fullyQualifiedDomainName;
			this.credentialsHostName = credentialsHostName;
			this.schemaPath = schemaPath;
			this.partitionPath = partitionPath;
		}

		// Token: 0x17001E8C RID: 7820
		// (get) Token: 0x06006A8C RID: 27276 RVA: 0x0016EAC6 File Offset: 0x0016CCC6
		public string FullyQualifiedDomainName
		{
			get
			{
				return this.fullyQualifiedDomainName;
			}
		}

		// Token: 0x17001E8D RID: 7821
		// (get) Token: 0x06006A8D RID: 27277 RVA: 0x0016EACE File Offset: 0x0016CCCE
		public LdapPath SchemaPath
		{
			get
			{
				return this.schemaPath;
			}
		}

		// Token: 0x17001E8E RID: 7822
		// (get) Token: 0x06006A8E RID: 27278 RVA: 0x0016EAD6 File Offset: 0x0016CCD6
		public LdapPath PartitionPath
		{
			get
			{
				return this.partitionPath;
			}
		}

		// Token: 0x17001E8F RID: 7823
		// (get) Token: 0x06006A8F RID: 27279 RVA: 0x0016EADE File Offset: 0x0016CCDE
		public IEngineHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17001E90 RID: 7824
		// (get) Token: 0x06006A90 RID: 27280 RVA: 0x0016EAE6 File Offset: 0x0016CCE6
		public IResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x17001E91 RID: 7825
		// (get) Token: 0x06006A91 RID: 27281 RVA: 0x0016EAF0 File Offset: 0x0016CCF0
		public string[] ForestDomains
		{
			get
			{
				if (this.forestDomains == null)
				{
					List<string> list = new List<string>();
					using (IEnumerator<ActiveDirectoryServiceSearchResult> enumerator = this.FindAll(this.PartitionPath, "(&(objectCategory=crossRef)(systemFlags:1.2.840.113556.1.4.804:=1)(systemFlags:1.2.840.113556.1.4.804:=2))", new SortOption(), RowCount.Infinite, new string[] { "dnsRoot" }).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text;
							if (enumerator.Current.TryGetSingleValueAttribute<string>("dnsRoot", out text))
							{
								list.Add(text);
							}
						}
					}
					list.Sort();
					this.forestDomains = list.ToArray();
				}
				return this.forestDomains;
			}
		}

		// Token: 0x06006A92 RID: 27282 RVA: 0x0016EB94 File Offset: 0x0016CD94
		public bool TryGetObject(string searchHost, string distinguishedName, string[] propertiesToLoad, out ActiveDirectoryServiceSearchResult result)
		{
			IResource resource = null;
			ResourceCredentialCollection resourceCredentialCollection = null;
			bool flag;
			try
			{
				using (new ProgressRequest(ProgressService.GetHostProgress(this.host, this.resource.Kind, searchHost)))
				{
					LdapPath ldapPath = new LdapPath(searchHost, distinguishedName);
					DirectoryEntry directoryEntry = this.CreateDirectoryEntry(ldapPath, out resource, out resourceCredentialCollection);
					result = this.service.FindOne(resourceCredentialCollection, directoryEntry, null, SearchScope.Base, propertiesToLoad);
					flag = true;
				}
			}
			catch (ActiveDirectoryServiceException ex)
			{
				int? extendedErrorCode = ex.ExtendedErrorCode;
				int num = 8333;
				if (!((extendedErrorCode.GetValueOrDefault() == num) & (extendedErrorCode != null)) && ex.ErrorCode != -2147016694)
				{
					throw ActiveDirectoryServiceAccessor.HandleException(this.host, ex, searchHost, resource);
				}
				result = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06006A93 RID: 27283 RVA: 0x0016EC68 File Offset: 0x0016CE68
		public bool TryGetObject(string distinguishedName, string[] propertiesToLoad, out ActiveDirectoryServiceSearchResult result)
		{
			return this.TryGetObject(LdapPath.GetDomainName(distinguishedName), distinguishedName, propertiesToLoad, out result);
		}

		// Token: 0x06006A94 RID: 27284 RVA: 0x0016EC7C File Offset: 0x0016CE7C
		public ActiveDirectoryServiceSearchResult GetObject(string searchHost, string distinguishedName, string[] propertiesToLoad)
		{
			ActiveDirectoryServiceSearchResult activeDirectoryServiceSearchResult;
			if (!this.TryGetObject(searchHost, distinguishedName, propertiesToLoad, out activeDirectoryServiceSearchResult))
			{
				throw ActiveDirectoryExceptions.NewObjectNotFoundException(distinguishedName);
			}
			return activeDirectoryServiceSearchResult;
		}

		// Token: 0x06006A95 RID: 27285 RVA: 0x0016EC9E File Offset: 0x0016CE9E
		public IEnumerable<ActiveDirectoryServiceSearchResult> FindAll(LdapPath searchRoot, string filter, SortOption sortOption, RowCount rowCount, params string[] propertiesToLoad)
		{
			IResource resource = null;
			ResourceCredentialCollection resourceCredentialCollection = null;
			DirectoryEntry directoryEntry = this.CreateDirectoryEntry(searchRoot, out resource, out resourceCredentialCollection);
			IHostProgress hostProgress = ProgressService.GetHostProgress(this.host, this.resource.Kind, searchRoot.Host);
			using (new ProgressRequest(hostProgress))
			{
				IEnumerable<ActiveDirectoryServiceSearchResult> enumerable;
				try
				{
					enumerable = this.service.FindAll(resourceCredentialCollection, directoryEntry, filter, sortOption, rowCount, SearchScope.Subtree, propertiesToLoad);
				}
				catch (ActiveDirectoryServiceException ex)
				{
					throw ActiveDirectoryServiceAccessor.HandleException(this.host, ex, searchRoot.Host, resource);
				}
				catch (ArgumentException ex2)
				{
					throw ActiveDirectoryExceptions.NewQueryTooLargeException(this.host, ex2, resource);
				}
				using (IEnumerator<ActiveDirectoryServiceSearchResult> enumerator = enumerable.GetEnumerator())
				{
					for (;;)
					{
						try
						{
							if (!enumerator.MoveNext())
							{
								break;
							}
						}
						catch (ActiveDirectoryServiceException ex3)
						{
							if (ex3.ErrorCode == -2147016660)
							{
								throw;
							}
							throw ActiveDirectoryServiceAccessor.HandleException(this.host, ex3, searchRoot.Host, resource);
						}
						hostProgress.RecordRowRead();
						yield return enumerator.Current;
					}
				}
				IEnumerator<ActiveDirectoryServiceSearchResult> enumerator = null;
			}
			ProgressRequest progressRequest = null;
			yield break;
			yield break;
		}

		// Token: 0x06006A96 RID: 27286 RVA: 0x0016ECD4 File Offset: 0x0016CED4
		public void EnsureAccessToDomain(string domainName)
		{
			IResource resource;
			ResourceCredentialCollection resourceCredentialCollection;
			this.EnsureAccessToHost(domainName, out resource, out resourceCredentialCollection);
		}

		// Token: 0x06006A97 RID: 27287 RVA: 0x0016ECEC File Offset: 0x0016CEEC
		private void EnsureAccessToHost(string hostName, out IResource resource, out ResourceCredentialCollection credentials)
		{
			resource = Microsoft.Mashup.Engine1.Library.Resource.New("ActiveDirectory", hostName);
			if (!HostResourcePermissionService.IsResourceAccessPermitted(this.host, resource, out credentials))
			{
				if (this.fullyQualifiedDomainName.Equals(hostName, StringComparison.OrdinalIgnoreCase) || this.ForestDomains.Contains(hostName, StringComparer.OrdinalIgnoreCase))
				{
					hostName = this.credentialsHostName;
				}
				resource = Microsoft.Mashup.Engine1.Library.Resource.New("ActiveDirectory", hostName);
				credentials = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, resource, null);
			}
		}

		// Token: 0x06006A98 RID: 27288 RVA: 0x0016ED60 File Offset: 0x0016CF60
		private DirectoryEntry CreateDirectoryEntry(LdapPath ldapPath, out IResource resource, out ResourceCredentialCollection credentials)
		{
			this.EnsureAccessToHost(ldapPath.Host, out resource, out credentials);
			if (credentials.Count > 0)
			{
				WindowsCredential windowsCredential = credentials[0] as WindowsCredential;
				if (windowsCredential != null)
				{
					if (!windowsCredential.OverrideCurrentUser)
					{
						return new DirectoryEntry(ldapPath.ToString(), null, null, AuthenticationTypes.Secure | AuthenticationTypes.Signing | AuthenticationTypes.Sealing);
					}
					if (ActiveDirectoryServiceAccessor.ValidateCredentials(windowsCredential.Username, windowsCredential.Password))
					{
						return new DirectoryEntry(ldapPath.ToString(), windowsCredential.Username, windowsCredential.Password, AuthenticationTypes.Secure | AuthenticationTypes.Signing | AuthenticationTypes.Sealing);
					}
				}
				UsernamePasswordCredential usernamePasswordCredential = credentials[0] as UsernamePasswordCredential;
				if (usernamePasswordCredential != null && ActiveDirectoryServiceAccessor.ValidateCredentials(usernamePasswordCredential.Username, usernamePasswordCredential.Password))
				{
					return new DirectoryEntry(ldapPath.ToString(), usernamePasswordCredential.Username, usernamePasswordCredential.Password, AuthenticationTypes.Secure | AuthenticationTypes.Signing | AuthenticationTypes.Sealing);
				}
			}
			throw DataSourceException.NewInvalidCredentialsError(this.host, resource, null, null, null);
		}

		// Token: 0x06006A99 RID: 27289 RVA: 0x0016EE4C File Offset: 0x0016D04C
		private static bool ValidateCredentials(string domainNameUsername, string password)
		{
			string text;
			string text2;
			return ActiveDirectoryServiceAccessor.TryParseUsernameDomainName(domainNameUsername, out text, out text2) && text.Length <= 104 && text.Length > 0 && (password == null || password.Length <= 104) && text.IndexOfAny(ActiveDirectoryServiceAccessor.usernameIllegalCharacters) == -1 && ActiveDirectoryModule.IsValidDomainName(text2);
		}

		// Token: 0x06006A9A RID: 27290 RVA: 0x0016EE9C File Offset: 0x0016D09C
		private static bool TryParseUsernameDomainName(string domainNameUsername, out string username, out string domainName)
		{
			if (!string.IsNullOrEmpty(domainNameUsername))
			{
				string[] array = domainNameUsername.Split(new char[] { '\\' });
				if (array.Length == 2)
				{
					username = array[1];
					domainName = array[0];
					return true;
				}
				int num = domainNameUsername.LastIndexOf('@');
				if (num != -1)
				{
					username = domainNameUsername.Substring(0, num);
					domainName = domainNameUsername.Substring(num + 1, domainNameUsername.Length - num - 1);
					return true;
				}
			}
			username = null;
			domainName = null;
			return false;
		}

		// Token: 0x06006A9B RID: 27291 RVA: 0x0016EF0A File Offset: 0x0016D10A
		private static void EnsureAccessToHost(IEngineHost host, string hostName, out IResource resource, out ResourceCredentialCollection credentials)
		{
			resource = Microsoft.Mashup.Engine1.Library.Resource.New("ActiveDirectory", hostName);
			credentials = HostResourcePermissionService.VerifyPermissionAndGetCredentials(host, resource, null);
		}

		// Token: 0x06006A9C RID: 27292 RVA: 0x0016EF24 File Offset: 0x0016D124
		private static ActiveDirectoryRootServiceEntry GetDirectoryRootServiceEntry(IEngineHost host, ActiveDirectoryCachingService service, string hostName)
		{
			IResource resource;
			ResourceCredentialCollection resourceCredentialCollection;
			ActiveDirectoryServiceAccessor.EnsureAccessToHost(host, hostName, out resource, out resourceCredentialCollection);
			ActiveDirectoryRootServiceEntry rootServiceEntry;
			try
			{
				DirectoryEntry directoryEntry = new DirectoryEntry(new LdapPath(hostName, "RootDSE").ToString(), null, null, AuthenticationTypes.Anonymous);
				rootServiceEntry = service.GetRootServiceEntry(resourceCredentialCollection, directoryEntry);
			}
			catch (ActiveDirectoryServiceException ex)
			{
				throw ActiveDirectoryServiceAccessor.HandleException(host, ex, hostName, resource);
			}
			return rootServiceEntry;
		}

		// Token: 0x06006A9D RID: 27293 RVA: 0x0016EF8C File Offset: 0x0016D18C
		private static Exception HandleException(IEngineHost engineHost, ActiveDirectoryServiceException e, string hostName, IResource resource)
		{
			if (resource != null)
			{
				int? extendedErrorCode = e.ExtendedErrorCode;
				int num = -2146893044;
				if (((extendedErrorCode.GetValueOrDefault() == num) & (extendedErrorCode != null)) || e.ErrorCode == -2147023570)
				{
					return DataSourceException.NewAccessAuthorizationError(engineHost, resource, null, null, null);
				}
				if (e.ErrorCode == -2147016694)
				{
					return DataSourceException.NewAccessForbiddenError(engineHost, resource, null, null, null);
				}
				if (e.ErrorCode == -2147016646)
				{
					return ActiveDirectoryExceptions.NewDomainNotFoundException(hostName);
				}
			}
			return ActiveDirectoryExceptions.NewUnexpectedException(engineHost, e, resource);
		}

		// Token: 0x06006A9E RID: 27294 RVA: 0x0016F009 File Offset: 0x0016D209
		private IHostProgress CreateHostProgress(string hostName)
		{
			return ProgressService.GetHostProgress(this.host, this.resource.Kind, hostName);
		}

		// Token: 0x04003B2A RID: 15146
		private const int ServerNotOperational = -2147016646;

		// Token: 0x04003B2B RID: 15147
		private const int LogonDenied = -2146893044;

		// Token: 0x04003B2C RID: 15148
		private const int UnknownUsernamePassword = -2147023570;

		// Token: 0x04003B2D RID: 15149
		private const int AccessDeniedErrorCode = -2147016694;

		// Token: 0x04003B2E RID: 15150
		private const int UnavailableCriticalExtension = -2147016660;

		// Token: 0x04003B2F RID: 15151
		private const int ObjectNotFoundErrorCode = 8333;

		// Token: 0x04003B30 RID: 15152
		private const int MaxUsernamePasswordLength = 104;

		// Token: 0x04003B31 RID: 15153
		private static readonly char[] usernameIllegalCharacters = new char[]
		{
			'/', '\\', '[', ']', ':', ';', '|', '=', ',', '+',
			'*', '?', '<', '>'
		};

		// Token: 0x04003B32 RID: 15154
		private readonly string credentialsHostName;

		// Token: 0x04003B33 RID: 15155
		private readonly string fullyQualifiedDomainName;

		// Token: 0x04003B34 RID: 15156
		private readonly IEngineHost host;

		// Token: 0x04003B35 RID: 15157
		private readonly ActiveDirectoryCachingService service;

		// Token: 0x04003B36 RID: 15158
		private readonly LdapPath schemaPath;

		// Token: 0x04003B37 RID: 15159
		private readonly LdapPath partitionPath;

		// Token: 0x04003B38 RID: 15160
		private readonly IResource resource;

		// Token: 0x04003B39 RID: 15161
		private string[] forestDomains;
	}
}
