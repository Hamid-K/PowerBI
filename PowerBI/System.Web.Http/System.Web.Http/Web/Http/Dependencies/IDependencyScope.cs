using System;
using System.Collections.Generic;

namespace System.Web.Http.Dependencies
{
	// Token: 0x02000087 RID: 135
	public interface IDependencyScope : IDisposable
	{
		// Token: 0x0600035B RID: 859
		object GetService(Type serviceType);

		// Token: 0x0600035C RID: 860
		IEnumerable<object> GetServices(Type serviceType);
	}
}
