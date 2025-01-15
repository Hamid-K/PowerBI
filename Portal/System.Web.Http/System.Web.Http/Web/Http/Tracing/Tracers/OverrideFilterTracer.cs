using System;
using System.Web.Http.Filters;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000124 RID: 292
	internal class OverrideFilterTracer : FilterTracer, IOverrideFilter, IFilter, IDecorator<IOverrideFilter>
	{
		// Token: 0x060007C0 RID: 1984 RVA: 0x00013904 File Offset: 0x00011B04
		public OverrideFilterTracer(IOverrideFilter innerFilter, ITraceWriter traceWriter)
			: base(innerFilter, traceWriter)
		{
			this._innerFilter = innerFilter;
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00013915 File Offset: 0x00011B15
		public new IOverrideFilter Inner
		{
			get
			{
				return this._innerFilter;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0001391D File Offset: 0x00011B1D
		public Type FiltersToOverride
		{
			get
			{
				return this._innerFilter.FiltersToOverride;
			}
		}

		// Token: 0x04000211 RID: 529
		private readonly IOverrideFilter _innerFilter;
	}
}
