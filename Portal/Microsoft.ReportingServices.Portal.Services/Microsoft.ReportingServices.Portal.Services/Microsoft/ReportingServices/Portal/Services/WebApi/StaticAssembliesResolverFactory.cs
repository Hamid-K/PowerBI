using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;
using Microsoft.ReportingServices.Portal.Interfaces.WebApi;

namespace Microsoft.ReportingServices.Portal.Services.WebApi
{
	// Token: 0x02000022 RID: 34
	public sealed class StaticAssembliesResolverFactory : IAssembliesResolverFactory
	{
		// Token: 0x060001CE RID: 462 RVA: 0x0000CE1F File Offset: 0x0000B01F
		public IAssembliesResolver Create(ICollection<Assembly> assemblies)
		{
			return new StaticAssembliesResolver(assemblies);
		}
	}
}
