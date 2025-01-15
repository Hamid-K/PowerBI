using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003B4 RID: 948
	public interface IPackageManager
	{
		// Token: 0x06001D40 RID: 7488
		void Register(IPackage package);

		// Token: 0x06001D41 RID: 7489
		IPackage TryRegister(IPackage package);

		// Token: 0x06001D42 RID: 7490
		void Unregister(IPackage package);

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06001D43 RID: 7491
		// (remove) Token: 0x06001D44 RID: 7492
		event EventHandler<PackageChangeEventArgs> PackagesChanged;

		// Token: 0x06001D45 RID: 7493
		EventMetadata GetEventMetadata(EventIdentifier eid);

		// Token: 0x06001D46 RID: 7494
		PackageMetadata GetPackageMetadata(Guid packageId);

		// Token: 0x06001D47 RID: 7495
		IList<IPackage> GetPackages();

		// Token: 0x06001D48 RID: 7496
		IPackage GetPackage(EventIdentifier eid);
	}
}
