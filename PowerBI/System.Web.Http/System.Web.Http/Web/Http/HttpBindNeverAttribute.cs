using System;
using System.Web.Http.ModelBinding;

namespace System.Web.Http
{
	// Token: 0x02000033 RID: 51
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class HttpBindNeverAttribute : HttpBindingBehaviorAttribute
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00004E1C File Offset: 0x0000301C
		public HttpBindNeverAttribute()
			: base(HttpBindingBehavior.Never)
		{
		}
	}
}
