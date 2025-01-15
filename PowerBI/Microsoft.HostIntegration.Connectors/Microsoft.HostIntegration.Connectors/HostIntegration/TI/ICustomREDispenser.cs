using System;
using System.Reflection;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200073A RID: 1850
	public interface ICustomREDispenser
	{
		// Token: 0x060039F5 RID: 14837
		BaseRemoteEnvironment GetDynamicRE(Assembly TIAssembly, string namespaceDotClass, int DispID, object TIClientContext);
	}
}
