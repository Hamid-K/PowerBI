using System;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000515 RID: 1301
	public interface IConditionalExceptionThrower
	{
		// Token: 0x0600285C RID: 10332
		bool TryBeginThrowExceptionIfHooked(EntryPointIdentifier identifier, out IAsyncResult asyncResult, AsyncCallback callback, object context);

		// Token: 0x0600285D RID: 10333
		void EndThrowExceptionIfHooked(IAsyncResult asyncResult);

		// Token: 0x0600285E RID: 10334
		void ThrowExceptionIfHooked(EntryPointIdentifier identifier);
	}
}
