using System;
using System.Collections.Generic;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x02000086 RID: 134
	public interface IHttpControllerTypeResolver
	{
		// Token: 0x0600035A RID: 858
		ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver);
	}
}
