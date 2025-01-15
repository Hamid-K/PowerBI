using System;
using System.Web.Http.Filters;

namespace System.Web.Http
{
	// Token: 0x02000019 RID: 25
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public sealed class OverrideAuthorizationAttribute : Attribute, IOverrideFilter, IFilter
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003B5D File Offset: 0x00001D5D
		public bool AllowMultiple
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003B78 File Offset: 0x00001D78
		public Type FiltersToOverride
		{
			get
			{
				return typeof(IAuthorizationFilter);
			}
		}
	}
}
