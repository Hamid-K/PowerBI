using System;
using System.Web.Http.Filters;

namespace System.Web.Http
{
	// Token: 0x0200001A RID: 26
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public sealed class OverrideExceptionFiltersAttribute : Attribute, IOverrideFilter, IFilter
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003B5D File Offset: 0x00001D5D
		public bool AllowMultiple
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003B84 File Offset: 0x00001D84
		public Type FiltersToOverride
		{
			get
			{
				return typeof(IExceptionFilter);
			}
		}
	}
}
