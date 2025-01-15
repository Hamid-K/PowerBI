using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200085A RID: 2138
	public abstract class IScopeReference : Reference<IScope>
	{
		// Token: 0x06007717 RID: 30487 RVA: 0x001ED666 File Offset: 0x001EB866
		internal IScopeReference()
		{
		}

		// Token: 0x06007718 RID: 30488 RVA: 0x001ED66E File Offset: 0x001EB86E
		[DebuggerStepThrough]
		public IScope Value()
		{
			return (IScope)base.InternalValue();
		}
	}
}
