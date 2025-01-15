using System;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000061 RID: 97
	// (Invoke) Token: 0x060002A7 RID: 679
	public delegate string ModelBinderErrorMessageProvider(HttpActionContext actionContext, ModelMetadata modelMetadata, object incomingValue);
}
