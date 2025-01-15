using System;
using System.Web.Http.Controllers;

namespace System.Web.Http.Description
{
	// Token: 0x020000EA RID: 234
	public interface IDocumentationProvider
	{
		// Token: 0x0600061F RID: 1567
		string GetDocumentation(HttpControllerDescriptor controllerDescriptor);

		// Token: 0x06000620 RID: 1568
		string GetDocumentation(HttpActionDescriptor actionDescriptor);

		// Token: 0x06000621 RID: 1569
		string GetDocumentation(HttpParameterDescriptor parameterDescriptor);

		// Token: 0x06000622 RID: 1570
		string GetResponseDocumentation(HttpActionDescriptor actionDescriptor);
	}
}
