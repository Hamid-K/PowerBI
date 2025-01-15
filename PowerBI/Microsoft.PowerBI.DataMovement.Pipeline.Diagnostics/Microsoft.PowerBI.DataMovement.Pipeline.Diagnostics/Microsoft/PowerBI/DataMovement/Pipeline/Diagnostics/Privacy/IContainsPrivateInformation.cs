using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Privacy
{
	// Token: 0x020000CF RID: 207
	[NullableContext(1)]
	public interface IContainsPrivateInformation
	{
		// Token: 0x0600107B RID: 4219
		string ToPrivateString();

		// Token: 0x0600107C RID: 4220
		string ToOriginalString();
	}
}
