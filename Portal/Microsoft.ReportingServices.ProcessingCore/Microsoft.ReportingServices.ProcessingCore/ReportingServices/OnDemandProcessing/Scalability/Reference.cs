using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200084E RID: 2126
	public abstract class Reference<T> : BaseReference, IReference<T>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IDisposable where T : IStorable
	{
		// Token: 0x060076B2 RID: 30386 RVA: 0x001EB957 File Offset: 0x001E9B57
		internal Reference()
		{
		}

		// Token: 0x060076B3 RID: 30387 RVA: 0x001EB95F File Offset: 0x001E9B5F
		T IReference<T>.Value()
		{
			return (T)((object)base.InternalValue());
		}
	}
}
