using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200204C RID: 8268
	public class Credential : IEquatable<Credential>
	{
		// Token: 0x0600CA51 RID: 51793 RVA: 0x00286F96 File Offset: 0x00285196
		public Credential()
		{
			this.resource = new Resource();
			this.credentialName = "Default";
		}

		// Token: 0x0600CA52 RID: 51794 RVA: 0x00286FB4 File Offset: 0x002851B4
		public Credential(Resource resource, byte[] credentialData)
		{
			this.resource = resource;
			this.credentialName = "Default";
			this.credentialData = credentialData;
		}

		// Token: 0x170030AA RID: 12458
		// (get) Token: 0x0600CA53 RID: 51795 RVA: 0x00286FD5 File Offset: 0x002851D5
		// (set) Token: 0x0600CA54 RID: 51796 RVA: 0x00286FDD File Offset: 0x002851DD
		public Resource Resource
		{
			get
			{
				return this.resource;
			}
			set
			{
				this.resource = value;
			}
		}

		// Token: 0x170030AB RID: 12459
		// (get) Token: 0x0600CA55 RID: 51797 RVA: 0x00286FE6 File Offset: 0x002851E6
		// (set) Token: 0x0600CA56 RID: 51798 RVA: 0x00286FEE File Offset: 0x002851EE
		public string CredentialName
		{
			get
			{
				return this.credentialName;
			}
			set
			{
				this.credentialName = value;
			}
		}

		// Token: 0x170030AC RID: 12460
		// (get) Token: 0x0600CA57 RID: 51799 RVA: 0x00286FF7 File Offset: 0x002851F7
		// (set) Token: 0x0600CA58 RID: 51800 RVA: 0x00286FFF File Offset: 0x002851FF
		public byte[] CredentialData
		{
			get
			{
				return this.credentialData;
			}
			set
			{
				this.credentialData = value;
			}
		}

		// Token: 0x0600CA59 RID: 51801 RVA: 0x00287008 File Offset: 0x00285208
		public bool Matches(Resource resource)
		{
			return this.Resource.Equals(resource);
		}

		// Token: 0x0600CA5A RID: 51802 RVA: 0x00287018 File Offset: 0x00285218
		public static Credential Lookup(IEnumerable<Credential> credentials, Resource resource)
		{
			foreach (Credential credential in credentials)
			{
				if (credential.Matches(resource))
				{
					return credential;
				}
			}
			return null;
		}

		// Token: 0x0600CA5B RID: 51803 RVA: 0x0028706C File Offset: 0x0028526C
		public static bool TryFindCredential(IList<Credential> credentials, Resource resource, out int index)
		{
			for (int i = 0; i < credentials.Count; i++)
			{
				if (credentials[i].Matches(resource))
				{
					index = i;
					return true;
				}
			}
			index = -1;
			return false;
		}

		// Token: 0x0600CA5C RID: 51804 RVA: 0x002870A4 File Offset: 0x002852A4
		public override int GetHashCode()
		{
			return ((this.Resource.Kind != null) ? this.Resource.Kind.GetHashCode() : 0) ^ ((this.Resource.Path != null) ? this.Resource.Path.GetHashCode() : 0);
		}

		// Token: 0x0600CA5D RID: 51805 RVA: 0x002870F2 File Offset: 0x002852F2
		public override bool Equals(object other)
		{
			return this.Equals(other as Credential);
		}

		// Token: 0x0600CA5E RID: 51806 RVA: 0x00287100 File Offset: 0x00285300
		public bool Equals(Credential other)
		{
			return other != null && (this.Resource.Kind == other.Resource.Kind && this.Resource.Path == other.Resource.Path) && Credential.Equals(this.CredentialData, other.CredentialData);
		}

		// Token: 0x0600CA5F RID: 51807 RVA: 0x00287160 File Offset: 0x00285360
		private static bool Equals(byte[] x, byte[] y)
		{
			if (x == null && y == null)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.Length != y.Length)
			{
				return false;
			}
			for (int i = 0; i < x.Length; i++)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040066E1 RID: 26337
		private Resource resource;

		// Token: 0x040066E2 RID: 26338
		private string credentialName;

		// Token: 0x040066E3 RID: 26339
		private byte[] credentialData;

		// Token: 0x040066E4 RID: 26340
		private const string DefaultName = "Default";
	}
}
