using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000176 RID: 374
	public interface ISafeWarmedUpObjectMarshaler : IDisposable
	{
		// Token: 0x060009D2 RID: 2514
		void WarmUp();

		// Token: 0x060009D3 RID: 2515
		bool TryInitializeUnSafeObject(object creationData);

		// Token: 0x060009D4 RID: 2516
		SafeWarmedUpObjectManager.ExecuteStatus ExecuteFunction(int operationId, out string result, params string[] args);
	}
}
