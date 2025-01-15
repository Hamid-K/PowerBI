using System;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000A1 RID: 161
	public interface IApplicationRootHost
	{
		// Token: 0x0600047A RID: 1146
		void RequestShutdown(IBlock requestor);

		// Token: 0x0600047B RID: 1147
		void RequestShutdown(IBlock requestor, int returnCode);

		// Token: 0x0600047C RID: 1148
		void AlertDebugger();

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600047D RID: 1149
		ApplicationRoot ApplicationRoot { get; }

		// Token: 0x0600047E RID: 1150
		void WaitForStateToComplete(BlockState stateToWaitFor);

		// Token: 0x0600047F RID: 1151
		void InvokeCallbackIfInState(Action callback, BlockState state);
	}
}
