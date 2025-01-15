using System;
using System.Reflection;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000046 RID: 70
	internal sealed class AssemblyLocationResolver : MarshalByRefObject
	{
		// Token: 0x06000206 RID: 518 RVA: 0x00007AAC File Offset: 0x00005CAC
		public static AssemblyLocationResolver CreateResolver(AppDomain tempAppDomain)
		{
			if (tempAppDomain == null)
			{
				return new AssemblyLocationResolver(true);
			}
			Type typeFromHandle = typeof(AssemblyLocationResolver);
			return (AssemblyLocationResolver)tempAppDomain.CreateInstanceFromAndUnwrap(typeFromHandle.Assembly.CodeBase, typeFromHandle.FullName);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007AEA File Offset: 0x00005CEA
		public string LoadAssemblyAndResolveLocation(string name)
		{
			if (this.m_fullLoad)
			{
				return Assembly.Load(name).Location;
			}
			return Assembly.ReflectionOnlyLoad(name).Location;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007B0B File Offset: 0x00005D0B
		public AssemblyLocationResolver()
			: this(false)
		{
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007B14 File Offset: 0x00005D14
		private AssemblyLocationResolver(bool fullLoad)
		{
			this.m_fullLoad = fullLoad;
		}

		// Token: 0x040000FF RID: 255
		private readonly bool m_fullLoad;
	}
}
