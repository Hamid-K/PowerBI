using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace Microsoft.ReportingServices.Portal.Services.WebApi
{
	// Token: 0x02000023 RID: 35
	public sealed class StaticAssembliesResolver : IAssembliesResolver
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x0000CE27 File Offset: 0x0000B027
		public StaticAssembliesResolver(ICollection<Assembly> assemblies)
		{
			if (assemblies == null)
			{
				throw new ArgumentNullException("assemblies");
			}
			this._assemblies = assemblies;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000CE44 File Offset: 0x0000B044
		public ICollection<Assembly> GetAssemblies()
		{
			return this._assemblies;
		}

		// Token: 0x0400008C RID: 140
		private readonly ICollection<Assembly> _assemblies;
	}
}
