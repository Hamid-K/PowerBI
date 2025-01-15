using System;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000CE RID: 206
	public class ScopedApplicationRootHost : IApplicationRootHost
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00014B8A File Offset: 0x00012D8A
		public ApplicationRoot ApplicationRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00014B8A File Offset: 0x00012D8A
		public void RequestShutdown(IBlock requestor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00014B8A File Offset: 0x00012D8A
		public void RequestShutdown(IBlock requestor, int returnCode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00014B8A File Offset: 0x00012D8A
		public void WaitForStateToComplete(BlockState stateToWaitFor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00014B8A File Offset: 0x00012D8A
		public void AlertDebugger()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00014B8A File Offset: 0x00012D8A
		public void InvokeCallbackIfInState(Action callback, BlockState state)
		{
			throw new NotImplementedException();
		}
	}
}
