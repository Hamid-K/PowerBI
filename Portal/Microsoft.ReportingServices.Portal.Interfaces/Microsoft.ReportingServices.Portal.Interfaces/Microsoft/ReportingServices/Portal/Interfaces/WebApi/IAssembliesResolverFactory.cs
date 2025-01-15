using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace Microsoft.ReportingServices.Portal.Interfaces.WebApi
{
	// Token: 0x02000083 RID: 131
	public interface IAssembliesResolverFactory
	{
		// Token: 0x0600040E RID: 1038
		IAssembliesResolver Create(ICollection<Assembly> assemblies);
	}
}
