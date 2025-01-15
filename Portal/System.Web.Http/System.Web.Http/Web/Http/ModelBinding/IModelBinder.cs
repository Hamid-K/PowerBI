using System;
using System.Web.Http.Controllers;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x0200005F RID: 95
	public interface IModelBinder
	{
		// Token: 0x06000298 RID: 664
		bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext);
	}
}
