using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Storage.Memory
{
	// Token: 0x0200209D RID: 8349
	public class MemoryCredentialsStorage : CredentialsStorage
	{
		// Token: 0x0600CC7A RID: 52346 RVA: 0x0028AA76 File Offset: 0x00288C76
		public MemoryCredentialsStorage()
		{
			this.credentials = new List<Credential>();
		}

		// Token: 0x0600CC7B RID: 52347 RVA: 0x0028AA89 File Offset: 0x00288C89
		public MemoryCredentialsStorage(IEnumerable<Credential> credentials)
		{
			this.credentials = new List<Credential>(credentials);
		}

		// Token: 0x0600CC7C RID: 52348 RVA: 0x0028AA9D File Offset: 0x00288C9D
		public override Credential[] GetCredentials()
		{
			return this.credentials.ToArray<Credential>();
		}

		// Token: 0x0600CC7D RID: 52349 RVA: 0x0028AAAC File Offset: 0x00288CAC
		public override void SetCredential(Credential credential)
		{
			int num;
			if (Credential.TryFindCredential(this.credentials, credential.Resource, out num))
			{
				this.credentials[num] = credential;
				return;
			}
			this.credentials.Add(credential);
		}

		// Token: 0x0600CC7E RID: 52350 RVA: 0x0028AAE8 File Offset: 0x00288CE8
		public override void EnsureCredential(Credential credential)
		{
			int num;
			if (!Credential.TryFindCredential(this.credentials, credential.Resource, out num))
			{
				this.credentials.Add(credential);
			}
		}

		// Token: 0x0600CC7F RID: 52351 RVA: 0x0028AB18 File Offset: 0x00288D18
		public override void ClearCredential(Resource resource)
		{
			int num;
			if (Credential.TryFindCredential(this.credentials, resource, out num))
			{
				this.credentials.RemoveAt(num);
			}
		}

		// Token: 0x0600CC80 RID: 52352 RVA: 0x0028AB44 File Offset: 0x00288D44
		public override void ClearCredentials(Resource[] resources)
		{
			for (int i = 0; i < resources.Length; i++)
			{
				this.ClearCredential(resources[i]);
			}
		}

		// Token: 0x0600CC81 RID: 52353 RVA: 0x0028AB68 File Offset: 0x00288D68
		public override void ClearCredentials()
		{
			this.credentials.Clear();
		}

		// Token: 0x04006792 RID: 26514
		private readonly IList<Credential> credentials;
	}
}
