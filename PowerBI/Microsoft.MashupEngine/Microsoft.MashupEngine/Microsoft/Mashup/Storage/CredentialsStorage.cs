using System;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002070 RID: 8304
	public abstract class CredentialsStorage
	{
		// Token: 0x0600CB3D RID: 52029
		public abstract Credential[] GetCredentials();

		// Token: 0x0600CB3E RID: 52030
		public abstract void SetCredential(Credential credential);

		// Token: 0x0600CB3F RID: 52031
		public abstract void EnsureCredential(Credential credential);

		// Token: 0x0600CB40 RID: 52032
		public abstract void ClearCredential(Resource resource);

		// Token: 0x0600CB41 RID: 52033
		public abstract void ClearCredentials();

		// Token: 0x0600CB42 RID: 52034
		public abstract void ClearCredentials(Resource[] resources);
	}
}
