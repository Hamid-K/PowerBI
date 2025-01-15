using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x0200007E RID: 126
	public interface IAssembliesResolver
	{
		// Token: 0x06000332 RID: 818
		ICollection<Assembly> GetAssemblies();
	}
}
