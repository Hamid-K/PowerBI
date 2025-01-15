using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000229 RID: 553
	public interface IContainsPrivateInformation
	{
		// Token: 0x06000E85 RID: 3717
		string ToPrivateString();

		// Token: 0x06000E86 RID: 3718
		string ToInternalString();

		// Token: 0x06000E87 RID: 3719
		string ToOriginalString();
	}
}
