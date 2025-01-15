using System;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x02000027 RID: 39
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public abstract class ParameterBindingAttribute : Attribute
	{
		// Token: 0x060000DB RID: 219
		public abstract HttpParameterBinding GetBinding(HttpParameterDescriptor parameter);
	}
}
