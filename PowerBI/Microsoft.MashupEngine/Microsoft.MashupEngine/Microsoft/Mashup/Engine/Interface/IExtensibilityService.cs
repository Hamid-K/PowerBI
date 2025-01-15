using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200007B RID: 123
	public interface IExtensibilityService
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001CF RID: 463
		IResource CurrentResource { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001D0 RID: 464
		ResourceCredentialCollection CurrentCredentials { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001D1 RID: 465
		bool ImpersonateResource { get; }

		// Token: 0x060001D2 RID: 466
		void RefreshCredential(bool forceRefresh);
	}
}
