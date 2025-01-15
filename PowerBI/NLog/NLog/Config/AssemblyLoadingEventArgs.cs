using System;
using System.ComponentModel;
using System.Reflection;

namespace NLog.Config
{
	// Token: 0x0200017D RID: 381
	public class AssemblyLoadingEventArgs : CancelEventArgs
	{
		// Token: 0x06001180 RID: 4480 RVA: 0x0002D5CD File Offset: 0x0002B7CD
		public AssemblyLoadingEventArgs(Assembly assembly)
		{
			this.Assembly = assembly;
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x0002D5DC File Offset: 0x0002B7DC
		// (set) Token: 0x06001182 RID: 4482 RVA: 0x0002D5E4 File Offset: 0x0002B7E4
		public Assembly Assembly { get; private set; }
	}
}
