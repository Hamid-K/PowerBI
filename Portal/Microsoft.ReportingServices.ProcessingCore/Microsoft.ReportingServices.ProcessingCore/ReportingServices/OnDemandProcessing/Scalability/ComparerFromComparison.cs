using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200089C RID: 2204
	internal class ComparerFromComparison<T> : IComparer
	{
		// Token: 0x060078D4 RID: 30932 RVA: 0x001F21C0 File Offset: 0x001F03C0
		internal ComparerFromComparison(Comparison<T> comparison)
		{
			this.m_comparison = comparison;
		}

		// Token: 0x060078D5 RID: 30933 RVA: 0x001F21CF File Offset: 0x001F03CF
		public int Compare(object x, object y)
		{
			if (!(x is T) || !(y is T))
			{
				Global.Tracer.Assert(false, "Cannot compare other types than the comparison's types");
			}
			return this.m_comparison((T)((object)x), (T)((object)y));
		}

		// Token: 0x04003CB2 RID: 15538
		private Comparison<T> m_comparison;
	}
}
