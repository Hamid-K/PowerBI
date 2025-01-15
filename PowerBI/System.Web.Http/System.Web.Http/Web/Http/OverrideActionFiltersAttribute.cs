using System;
using System.Web.Http.Filters;

namespace System.Web.Http
{
	// Token: 0x02000017 RID: 23
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public sealed class OverrideActionFiltersAttribute : Attribute, IOverrideFilter, IFilter
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003B5D File Offset: 0x00001D5D
		public bool AllowMultiple
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003B60 File Offset: 0x00001D60
		public Type FiltersToOverride
		{
			get
			{
				return typeof(IActionFilter);
			}
		}
	}
}
