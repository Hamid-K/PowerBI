using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost.Services;
using Microsoft.Mashup.Storage;
using Microsoft.Mashup.Storage.Memory;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001958 RID: 6488
	public class CredentialManager
	{
		// Token: 0x0600A46C RID: 42092 RVA: 0x00220E64 File Offset: 0x0021F064
		public CredentialManager(IEngineHost engineHost, CredentialsStorage credentialsStorage, bool allowAutomaticCredentials, bool allowWindowsAuthentication, SafeHandle threadIdentity = null)
		{
			this.credentialsStorage = credentialsStorage;
			this.credentials = new List<Credential>();
			this.refreshManager = new CredentialRefreshManager();
			this.credentials.AddRange(credentialsStorage.GetCredentials());
			this.credentials.Sort(CredentialManager.DescendingCredentialPathLengthComparer.Instance);
			this.engineHost = engineHost;
			this.allowAutomaticCredentials = allowAutomaticCredentials;
			this.allowWindowsAuthentication = allowWindowsAuthentication;
			this.threadIdentity = threadIdentity;
			this.objectLock = new object();
		}

		// Token: 0x0600A46D RID: 42093 RVA: 0x00220EDE File Offset: 0x0021F0DE
		public CredentialManager(Credential[] credentials, bool allowAutomaticCredentials, bool allowWindowsAuthentication = true)
			: this(null, new MemoryCredentialsStorage(credentials), allowAutomaticCredentials, allowWindowsAuthentication, null)
		{
		}

		// Token: 0x0600A46E RID: 42094 RVA: 0x00220EF0 File Offset: 0x0021F0F0
		public CredentialManager(Credential[] credentials)
			: this(credentials, true, true)
		{
		}

		// Token: 0x0600A46F RID: 42095 RVA: 0x00220EFC File Offset: 0x0021F0FC
		public bool TryGetCredentials(Resource resource, out ResourceCredentialCollection credentials)
		{
			if (this.TryGetConfiguredCredentials(resource, out credentials))
			{
				this.ValidateCredential(credentials);
				return true;
			}
			if (this.allowAutomaticCredentials && this.TryGetAutomaticCredentials(resource, out credentials))
			{
				this.ValidateCredential(credentials);
				return true;
			}
			CredentialDataCollection credentialDataCollection;
			if (this.TryGetDefaultCredentials(resource, out credentialDataCollection))
			{
				this.AddCredential(resource, credentialDataCollection);
				credentials = credentialDataCollection.ToResourceCredentials(resource, null, true);
				this.ValidateCredential(credentials);
				return true;
			}
			credentials = null;
			return false;
		}

		// Token: 0x0600A470 RID: 42096 RVA: 0x00220F68 File Offset: 0x0021F168
		public ResourceCredentialCollection RefreshCredential(Resource resource, bool forceRefresh = false)
		{
			object obj = this.objectLock;
			ResourceCredentialCollection resourceCredentialCollection;
			lock (obj)
			{
				Credential credential = this.GetConfiguredCredential(resource);
				CredentialDataCollection storageCollection = null;
				try
				{
					storageCollection = Xml<CredentialDataCollection>.DeserializeBytes(credential.CredentialData);
				}
				catch (Exception ex)
				{
					throw new InvalidResourceCredentialsException(resource, null, null, ex);
				}
				resourceCredentialCollection = this.refreshManager.RefreshIfNeeded(storageCollection.ToResourceCredentials(resource, null, true), forceRefresh, delegate
				{
					storageCollection = this.engineHost.QueryService<ICredentialRefreshService>().Refresh(credential.Resource, storageCollection);
					this.UpdateCredential(credential.Resource, storageCollection);
					return this.ValidateCredential(storageCollection.ToResourceCredentials(credential.Resource, null, true));
				});
			}
			return resourceCredentialCollection;
		}

		// Token: 0x0600A471 RID: 42097 RVA: 0x0022101C File Offset: 0x0021F21C
		public static bool HasDefaultAnonymousCredential(Resource resource)
		{
			if (resource.Kind == "Web" || resource.Kind == "OData")
			{
				Uri uri;
				return Uri.TryCreate(resource.Path, UriKind.Absolute, out uri) && StringComparer.OrdinalIgnoreCase.Compare(uri.Scheme, "http") == 0;
			}
			return resource.Kind == "Ftp";
		}

		// Token: 0x0600A472 RID: 42098 RVA: 0x00221090 File Offset: 0x0021F290
		internal void UpdateCredential(Resource resource, CredentialDataCollection credentialDataCollection)
		{
			byte[] array = CredentialDataSerializer.Serialize(credentialDataCollection);
			Credential credential = new Credential(resource, array);
			this.UpdateCredential(resource, credential);
			this.credentialsStorage.SetCredential(credential);
		}

		// Token: 0x0600A473 RID: 42099 RVA: 0x002210C0 File Offset: 0x0021F2C0
		private ResourceCredentialCollection ValidateCredential(ResourceCredentialCollection credentials)
		{
			if (!this.allowWindowsAuthentication)
			{
				if (credentials.Any((IResourceCredential rc) => rc is WindowsCredential))
				{
					throw new UnpermittedResourceAccessException(credentials.Resource, credentials.Resource, Strings.NoWindowsAuth, Strings.NoWindowsAuth, null);
				}
			}
			return credentials;
		}

		// Token: 0x0600A474 RID: 42100 RVA: 0x0022111C File Offset: 0x0021F31C
		private void UpdateCredential(Resource resource, Credential newCredential)
		{
			for (int i = 0; i < this.credentials.Count; i++)
			{
				if (this.credentials[i].Matches(resource))
				{
					this.credentials[i] = newCredential;
					return;
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600A475 RID: 42101 RVA: 0x00221168 File Offset: 0x0021F368
		private void AddCredential(Resource resource, CredentialDataCollection credentialDataCollection)
		{
			byte[] array = CredentialDataSerializer.Serialize(credentialDataCollection);
			Credential credential = new Credential(resource, array);
			this.credentials.Add(credential);
			this.credentials.Sort(CredentialManager.DescendingCredentialPathLengthComparer.Instance);
			this.credentialsStorage.EnsureCredential(credential);
		}

		// Token: 0x0600A476 RID: 42102 RVA: 0x002211AC File Offset: 0x0021F3AC
		private bool TryGetDefaultCredentials(Resource resource, out CredentialDataCollection credentialDataCollection)
		{
			if (this.allowAutomaticCredentials && CredentialManager.HasDefaultAnonymousCredential(resource))
			{
				credentialDataCollection = new CredentialDataCollection();
				return true;
			}
			credentialDataCollection = null;
			return false;
		}

		// Token: 0x0600A477 RID: 42103 RVA: 0x002211CC File Offset: 0x0021F3CC
		private bool TryGetAutomaticCredentials(Resource resource, out ResourceCredentialCollection credentials)
		{
			if (this.CanGetAutomaticCredentialsForFileSystemResource(resource) || resource.Kind == "R" || resource.Kind == "Python")
			{
				credentials = new ResourceCredentialCollection(resource, new IResourceCredential[]
				{
					new WindowsCredential()
				});
				return true;
			}
			if (resource.Equals(Resource.CurrentWorkbookResource))
			{
				credentials = new ResourceCredentialCollection(resource, Array.Empty<IResourceCredential>());
				return true;
			}
			credentials = null;
			return false;
		}

		// Token: 0x0600A478 RID: 42104 RVA: 0x00221240 File Offset: 0x0021F440
		private Credential GetConfiguredCredential(Resource resource)
		{
			Credential credential;
			if (!this.TryGetConfiguredCredentials(resource, out credential))
			{
				throw new UnpermittedResourceAccessException(resource, null, null, null);
			}
			return credential;
		}

		// Token: 0x0600A479 RID: 42105 RVA: 0x00221264 File Offset: 0x0021F464
		private bool TryGetConfiguredCredentials(Resource resource, out ResourceCredentialCollection credentials)
		{
			Credential credential;
			CredentialDataCollection credentialDataCollection;
			if (this.TryGetConfiguredCredentials(resource, out credential) && CredentialDataSerializer.TryDeserialize(credential.CredentialData, out credentialDataCollection))
			{
				Resource resource2 = ResourcePath.AdjustForNull(credential.Resource, resource.Path);
				credentials = credentialDataCollection.ToResourceCredentials(resource2, new IdentityContext(this.threadIdentity), true);
				return true;
			}
			credentials = null;
			return false;
		}

		// Token: 0x0600A47A RID: 42106 RVA: 0x002212B8 File Offset: 0x0021F4B8
		private bool TryGetConfiguredCredentials(Resource resource, out Credential credential)
		{
			foreach (Credential credential2 in this.credentials)
			{
				if (ResourcePath.StartsWith(credential2.Resource, resource))
				{
					credential = credential2;
					return true;
				}
			}
			credential = null;
			return false;
		}

		// Token: 0x0600A47B RID: 42107 RVA: 0x00221320 File Offset: 0x0021F520
		private bool CanGetAutomaticCredentialsForFileSystemResource(Resource resource)
		{
			return resource.Kind == "File" || resource.Kind == "Folder";
		}

		// Token: 0x040055A9 RID: 21929
		private readonly object objectLock;

		// Token: 0x040055AA RID: 21930
		private readonly CredentialsStorage credentialsStorage;

		// Token: 0x040055AB RID: 21931
		private readonly List<Credential> credentials;

		// Token: 0x040055AC RID: 21932
		private readonly CredentialRefreshManager refreshManager;

		// Token: 0x040055AD RID: 21933
		private readonly IEngineHost engineHost;

		// Token: 0x040055AE RID: 21934
		private readonly bool allowAutomaticCredentials;

		// Token: 0x040055AF RID: 21935
		private readonly bool allowWindowsAuthentication;

		// Token: 0x040055B0 RID: 21936
		private readonly SafeHandle threadIdentity;

		// Token: 0x02001959 RID: 6489
		private class DescendingCredentialPathLengthComparer : IComparer<Credential>
		{
			// Token: 0x0600A47C RID: 42108 RVA: 0x000020FD File Offset: 0x000002FD
			private DescendingCredentialPathLengthComparer()
			{
			}

			// Token: 0x0600A47D RID: 42109 RVA: 0x00221346 File Offset: 0x0021F546
			public int Compare(Credential left, Credential right)
			{
				return ResourcePath.Length(right.Resource.Path) - ResourcePath.Length(left.Resource.Path);
			}

			// Token: 0x040055B1 RID: 21937
			public static readonly CredentialManager.DescendingCredentialPathLengthComparer Instance = new CredentialManager.DescendingCredentialPathLengthComparer();
		}
	}
}
