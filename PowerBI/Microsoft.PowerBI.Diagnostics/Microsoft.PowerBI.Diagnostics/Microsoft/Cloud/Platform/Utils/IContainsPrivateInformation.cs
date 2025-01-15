using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000006 RID: 6
	public interface IContainsPrivateInformation
	{
		// Token: 0x06000003 RID: 3
		string ToPrivateString();

		// Token: 0x06000004 RID: 4
		string ToInternalString();

		// Token: 0x06000005 RID: 5
		string ToOriginalString();
	}
}
