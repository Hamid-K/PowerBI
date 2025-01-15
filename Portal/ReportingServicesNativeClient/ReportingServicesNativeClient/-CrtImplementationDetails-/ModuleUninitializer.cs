using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200002D RID: 45
	internal class ModuleUninitializer : Stack
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x0000B60C File Offset: 0x0000AA0C
		[SecuritySafeCritical]
		internal void AddHandler(EventHandler handler)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				Monitor.Enter(ModuleUninitializer.@lock, ref flag);
				RuntimeHelpers.PrepareDelegate(handler);
				this.Push(handler);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(ModuleUninitializer.@lock);
				}
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000BCC4 File Offset: 0x0000B0C4
		[SecuritySafeCritical]
		private ModuleUninitializer()
		{
			EventHandler eventHandler = new EventHandler(this.SingletonDomainUnload);
			AppDomain.CurrentDomain.DomainUnload += eventHandler;
			AppDomain.CurrentDomain.ProcessExit += eventHandler;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000B674 File Offset: 0x0000AA74
		[PrePrepareMethod]
		[SecurityCritical]
		private void SingletonDomainUnload(object source, EventArgs arguments)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				Monitor.Enter(ModuleUninitializer.@lock, ref flag);
				using (IEnumerator enumerator = this.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						((EventHandler)enumerator.Current)(source, arguments);
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(ModuleUninitializer.@lock);
				}
			}
		}

		// Token: 0x04000063 RID: 99
		private static object @lock = new object();

		// Token: 0x04000064 RID: 100
		internal static ModuleUninitializer _ModuleUninitializer = new ModuleUninitializer();
	}
}
