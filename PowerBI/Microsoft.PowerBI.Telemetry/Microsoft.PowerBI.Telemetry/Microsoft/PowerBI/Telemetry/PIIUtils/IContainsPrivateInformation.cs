using System;

namespace Microsoft.PowerBI.Telemetry.PIIUtils
{
	// Token: 0x02000033 RID: 51
	public interface IContainsPrivateInformation
	{
		// Token: 0x06000131 RID: 305
		string ToPrivateString();

		// Token: 0x06000132 RID: 306
		string ToInternalString();

		// Token: 0x06000133 RID: 307
		string ToOriginalString();
	}
}
