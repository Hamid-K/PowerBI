using System;
using System.Web.Http.ModelBinding;

namespace System.Web.Http
{
	// Token: 0x02000034 RID: 52
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class HttpBindRequiredAttribute : HttpBindingBehaviorAttribute
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00004E25 File Offset: 0x00003025
		public HttpBindRequiredAttribute()
			: base(HttpBindingBehavior.Required)
		{
		}
	}
}
