using System;
using System.Diagnostics;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000BC RID: 188
	internal class BlockStateTransitionSynchronizer
	{
		// Token: 0x06000568 RID: 1384 RVA: 0x00013CE3 File Offset: 0x00011EE3
		internal BlockStateTransitionSynchronizer()
		{
			this.m_state = BlockState.Uninitialized;
			this.m_stateLocker = new object();
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00013CFD File Offset: 0x00011EFD
		internal void AdvanceToState(BlockState curState, BlockState nextState)
		{
			this.AdvanceToState(curState, nextState, PulseOrNot.Pulse);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00013D08 File Offset: 0x00011F08
		internal void AdvanceToState(BlockState curState, BlockState nextState, PulseOrNot pulseOrNot)
		{
			object stateLocker = this.m_stateLocker;
			lock (stateLocker)
			{
				this.m_state = nextState;
				if (pulseOrNot == PulseOrNot.Pulse)
				{
					Monitor.PulseAll(this.m_stateLocker);
				}
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00013D58 File Offset: 0x00011F58
		internal void WaitForState(BlockState stateToWaitFor)
		{
			DebuggableMonitorWaiter debuggableMonitorWaiter = new DebuggableMonitorWaiter(this.m_stateLocker, -1);
			object stateLocker = this.m_stateLocker;
			lock (stateLocker)
			{
				while (this.m_state != stateToWaitFor)
				{
					debuggableMonitorWaiter.TryWait();
				}
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00013DB4 File Offset: 0x00011FB4
		internal void WaitForStateAndAdvanceToState(BlockState stateToWaitFor, BlockState nextState, PulseOrNot pulseOrNot)
		{
			object stateLocker = this.m_stateLocker;
			lock (stateLocker)
			{
				this.WaitForState(stateToWaitFor);
				this.AdvanceToState(stateToWaitFor, nextState, pulseOrNot);
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00013E00 File Offset: 0x00012000
		internal void WaitForStateAndAdvanceToState(BlockState stateToWaitFor, BlockState nextState)
		{
			this.WaitForStateAndAdvanceToState(stateToWaitFor, nextState, PulseOrNot.Pulse);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00013E0C File Offset: 0x0001200C
		internal bool TryInvokeCallbackIfInState(BlockState expectedState, [NotNull] Action callback)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action>(callback, "callback");
			object stateLocker = this.m_stateLocker;
			bool flag2;
			lock (stateLocker)
			{
				if (this.m_state != expectedState)
				{
					flag2 = false;
				}
				else
				{
					callback();
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00013E68 File Offset: 0x00012068
		[Conditional("DEBUG")]
		internal void AssertState(BlockState curState)
		{
			object stateLocker = this.m_stateLocker;
			lock (stateLocker)
			{
			}
		}

		// Token: 0x040001DD RID: 477
		private BlockState m_state;

		// Token: 0x040001DE RID: 478
		private object m_stateLocker;
	}
}
