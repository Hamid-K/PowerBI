using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Common;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x020003A2 RID: 930
	public interface IEventingRepositoryContinuation
	{
		// Token: 0x06001C83 RID: 7299
		string ToSerializedString();

		// Token: 0x06001C84 RID: 7300
		IEnumerable<string> GetAllRoleInstances();

		// Token: 0x06001C85 RID: 7301
		IEnumerable<ElementId> GetAllElementInstances(string roleInstance);

		// Token: 0x06001C86 RID: 7302
		DateTime GetLastEventTimeForElementInstance(string roleInstance, ElementId instance);
	}
}
