using System;

namespace Microsoft.InfoNav.Utils
{
	// Token: 0x0200002E RID: 46
	public interface IContainsPrivateInformation
	{
		// Token: 0x06000239 RID: 569
		string ToPrivateString();

		// Token: 0x0600023A RID: 570
		string ToInternalString();

		// Token: 0x0600023B RID: 571
		string ToOriginalString();
	}
}
