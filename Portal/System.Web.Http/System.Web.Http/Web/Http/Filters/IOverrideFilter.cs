using System;

namespace System.Web.Http.Filters
{
	// Token: 0x020000C2 RID: 194
	public interface IOverrideFilter : IFilter
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000545 RID: 1349
		Type FiltersToOverride { get; }
	}
}
