using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000047 RID: 71
	public interface IConnectionGovernanceService
	{
		// Token: 0x06000141 RID: 321
		ITask<IDisposable> BeginGetGovernedHandle(IResource resource, GlobalThreadId threadId);
	}
}
