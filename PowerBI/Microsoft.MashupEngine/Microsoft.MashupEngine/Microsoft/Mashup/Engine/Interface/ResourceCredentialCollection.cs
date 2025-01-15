using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000103 RID: 259
	public sealed class ResourceCredentialCollection : ReadOnlyCollection<IResourceCredential>
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x0000543B File Offset: 0x0000363B
		public ResourceCredentialCollection()
			: this(null, Array.Empty<IResourceCredential>())
		{
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00005449 File Offset: 0x00003649
		public ResourceCredentialCollection(IResource resource, params IResourceCredential[] credentials)
			: this(resource, new List<IResourceCredential>(credentials))
		{
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00005458 File Offset: 0x00003658
		public ResourceCredentialCollection(IResource resource, IList<IResourceCredential> credentials)
			: base(credentials)
		{
			this.resource = resource;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00005468 File Offset: 0x00003668
		public IResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00005470 File Offset: 0x00003670
		public bool TryGetDatabasePattern(out IResourceCredential resourceCredential, out EncryptedConnectionAdornment adornment)
		{
			ApplicationCredentialPropertiesAdornment applicationCredentialPropertiesAdornment;
			ConnectionStringPropertiesAdornment connectionStringPropertiesAdornment;
			return this.TryGetDatabasePattern(out resourceCredential, out adornment, out applicationCredentialPropertiesAdornment, out connectionStringPropertiesAdornment);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000548C File Offset: 0x0000368C
		public bool TryGetDatabasePattern(out IResourceCredential resourceCredential, out EncryptedConnectionAdornment encryptedConnectionAdornment, out ApplicationCredentialPropertiesAdornment applicationCredentialPropertiesAdornment, out ConnectionStringPropertiesAdornment connectionStringPropertiesAdornment)
		{
			encryptedConnectionAdornment = null;
			applicationCredentialPropertiesAdornment = null;
			connectionStringPropertiesAdornment = null;
			resourceCredential = null;
			if (base.Count < 1 || base.Count > 4)
			{
				return false;
			}
			foreach (IResourceCredential resourceCredential2 in this)
			{
				bool flag = false;
				if ((!this.SetIfType<EncryptedConnectionAdornment>(resourceCredential2, ref encryptedConnectionAdornment, ref flag) && !this.SetIfType<ApplicationCredentialPropertiesAdornment>(resourceCredential2, ref applicationCredentialPropertiesAdornment, ref flag) && !this.SetIfType<ConnectionStringPropertiesAdornment>(resourceCredential2, ref connectionStringPropertiesAdornment, ref flag) && !this.SetIfType<IResourceCredential>(resourceCredential2, ref resourceCredential, ref flag)) || flag)
				{
					resourceCredential = null;
					break;
				}
			}
			if (resourceCredential == null)
			{
				encryptedConnectionAdornment = null;
				applicationCredentialPropertiesAdornment = null;
				connectionStringPropertiesAdornment = null;
				return false;
			}
			encryptedConnectionAdornment = encryptedConnectionAdornment ?? new EncryptedConnectionAdornment(false);
			return true;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00005550 File Offset: 0x00003750
		private bool SetIfType<T>(IResourceCredential credential, ref T assigned, ref bool alreadyAssigned) where T : class
		{
			T t = credential as T;
			if (t != null)
			{
				if (assigned != null)
				{
					alreadyAssigned |= true;
				}
				assigned = t;
				return true;
			}
			return false;
		}

		// Token: 0x0400023E RID: 574
		private readonly IResource resource;
	}
}
