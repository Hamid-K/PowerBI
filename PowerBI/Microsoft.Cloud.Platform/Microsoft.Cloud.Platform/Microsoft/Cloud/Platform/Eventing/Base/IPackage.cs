using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003CD RID: 973
	public interface IPackage
	{
		// Token: 0x06001E11 RID: 7697
		void Reconfigure(HashSet<EventIdentifier> events);

		// Token: 0x06001E12 RID: 7698
		bool IsEnabled(EventIdentifier eid);

		// Token: 0x06001E13 RID: 7699
		bool Contains(EventIdentifier eid);

		// Token: 0x06001E14 RID: 7700
		IEnumerable<EventMetadata> GetEvents();

		// Token: 0x06001E15 RID: 7701
		EventMetadata GetEventMetadata(EventIdentifier eid);

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001E16 RID: 7702
		PackageMetadata Metadata { get; }
	}
}
