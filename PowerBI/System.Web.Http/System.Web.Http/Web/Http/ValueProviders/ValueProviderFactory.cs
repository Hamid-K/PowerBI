using System;
using System.Web.Http.Controllers;

namespace System.Web.Http.ValueProviders
{
	// Token: 0x0200003B RID: 59
	public abstract class ValueProviderFactory
	{
		// Token: 0x0600019E RID: 414
		public abstract IValueProvider GetValueProvider(HttpActionContext actionContext);
	}
}
