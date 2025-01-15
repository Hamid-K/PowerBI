using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200080E RID: 2062
	public interface IConfiguration
	{
		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06004121 RID: 16673
		Dictionary<ManagerCodePoint, ManagerElement> Managers { get; }

		// Token: 0x17000F3F RID: 3903
		ManagerElement this[ManagerCodePoint cp] { get; }

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06004123 RID: 16675
		List<TypeElement> CommunicationManagers { get; }

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06004124 RID: 16676
		TypeElement ExceptionManager { get; }

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06004125 RID: 16677
		List<TypeElement> CustomLoggers { get; }

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06004126 RID: 16678
		List<TypeElement> PackageBindListeners { get; }

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06004127 RID: 16679
		TypeElement Database { get; }

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06004128 RID: 16680
		List<ApplicationEncoding> ApplicationEncodings { get; }

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06004129 RID: 16681
		List<DateTimeMask> DateTimeMasks { get; }

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x0600412A RID: 16682
		Dictionary<SqlSetOptions, string> SqlSets { get; }

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x0600412B RID: 16683
		DatabaseAlias DatabaseAliases { get; }

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x0600412C RID: 16684
		Dictionary<string, string> CollationMappings { get; }

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x0600412D RID: 16685
		string ServiceName { get; }
	}
}
