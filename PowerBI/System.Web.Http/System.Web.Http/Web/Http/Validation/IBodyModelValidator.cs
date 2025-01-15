using System;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace System.Web.Http.Validation
{
	// Token: 0x0200008F RID: 143
	public interface IBodyModelValidator
	{
		// Token: 0x06000380 RID: 896
		bool Validate(object model, Type type, ModelMetadataProvider metadataProvider, HttpActionContext actionContext, string keyPrefix);
	}
}
