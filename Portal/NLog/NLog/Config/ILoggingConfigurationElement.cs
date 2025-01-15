using System;
using System.Collections.Generic;

namespace NLog.Config
{
	// Token: 0x02000187 RID: 391
	public interface ILoggingConfigurationElement
	{
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060011C3 RID: 4547
		string Name { get; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060011C4 RID: 4548
		IEnumerable<KeyValuePair<string, string>> Values { get; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060011C5 RID: 4549
		IEnumerable<ILoggingConfigurationElement> Children { get; }
	}
}
