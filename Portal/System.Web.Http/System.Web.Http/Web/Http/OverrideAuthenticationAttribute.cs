using System;
using System.Web.Http.Filters;

namespace System.Web.Http
{
	// Token: 0x02000018 RID: 24
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public sealed class OverrideAuthenticationAttribute : Attribute, IOverrideFilter, IFilter
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003B5D File Offset: 0x00001D5D
		public bool AllowMultiple
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003B6C File Offset: 0x00001D6C
		public Type FiltersToOverride
		{
			get
			{
				return typeof(IAuthenticationFilter);
			}
		}
	}
}
