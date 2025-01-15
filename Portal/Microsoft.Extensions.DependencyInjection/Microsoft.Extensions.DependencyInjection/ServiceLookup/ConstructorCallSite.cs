using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x0200000F RID: 15
	internal class ConstructorCallSite : IServiceCallSite
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003565 File Offset: 0x00001765
		internal ConstructorInfo ConstructorInfo { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000356D File Offset: 0x0000176D
		internal IServiceCallSite[] ParameterCallSites { get; }

		// Token: 0x06000085 RID: 133 RVA: 0x00003575 File Offset: 0x00001775
		public ConstructorCallSite(ConstructorInfo constructorInfo, IServiceCallSite[] parameterCallSites)
		{
			this.ConstructorInfo = constructorInfo;
			this.ParameterCallSites = parameterCallSites;
		}
	}
}
