using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004CB RID: 1227
	internal interface INamedDataModelItem
	{
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06003CC5 RID: 15557
		string Name { get; }

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06003CC6 RID: 15558
		string Identity { get; }
	}
}
