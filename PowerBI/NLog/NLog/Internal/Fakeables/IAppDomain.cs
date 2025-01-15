using System;
using System.Collections.Generic;
using System.Reflection;

namespace NLog.Internal.Fakeables
{
	// Token: 0x0200016A RID: 362
	public interface IAppDomain
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001103 RID: 4355
		string BaseDirectory { get; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001104 RID: 4356
		string ConfigurationFile { get; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001105 RID: 4357
		IEnumerable<string> PrivateBinPath { get; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001106 RID: 4358
		string FriendlyName { get; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001107 RID: 4359
		int Id { get; }

		// Token: 0x06001108 RID: 4360
		IEnumerable<Assembly> GetAssemblies();

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001109 RID: 4361
		// (remove) Token: 0x0600110A RID: 4362
		event EventHandler<EventArgs> ProcessExit;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600110B RID: 4363
		// (remove) Token: 0x0600110C RID: 4364
		event EventHandler<EventArgs> DomainUnload;
	}
}
