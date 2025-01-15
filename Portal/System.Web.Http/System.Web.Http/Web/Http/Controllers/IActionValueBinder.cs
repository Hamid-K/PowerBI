using System;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000104 RID: 260
	public interface IActionValueBinder
	{
		// Token: 0x060006D5 RID: 1749
		HttpActionBinding GetBinding(HttpActionDescriptor actionDescriptor);
	}
}
